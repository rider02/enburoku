using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// マップの敵情報を持つEnemyのラッパークラス
/// PlayerModelに加えてプレイヤー検索機能、行動パターン、移動開始ターン等を持つ
/// </summary>
public class EnemyModel : MonoBehaviour
{
    public Enemy enemy;

    BattleMapManager battleMapManager;
    EnemyAIManager enemyAIManager;

    public GameObject damagePrefab;

    public GameObject player;

    //210212 行動前ならエフェクトをユニット周囲に表示させる
    public GameObject beforeAction;

    Main_Map map;

    //攻撃範囲を軽くする工夫
    public List<Main_Cell> attackableCellList;

    //攻撃範囲にユニットが侵入した時、再計算を行うユニット
    public bool isCancelReload;

    //アニメーションを変化させる
    private Animator animator;

    //200806 行動済みフラグ
    public bool isMoved { get; set; }

    //座標
    public int x { get; set; }
    public int y { get; set; }
    bool isFocused = false;

    //行動パターンがWAITの時、行動開始するターン
    public int actionTurn { get; set; }

    public EnemyAIPattern enemyAIPattern = EnemyAIPattern.SEARCH;

    //210221 攻撃可能範囲表示アルゴリズムで誰の攻撃可能範囲か識別する為、IDを持たせてセルに渡す
    //これは絶対にユニークでなければならない
    public int enemyId;

    //210221 既にハイライト済みかの判定フラグ
    public bool isHighLight;

    //アニメーションを変化させる
    public Animator Animator;


    //200808 初期化された時にUnitクラスを受け取る
    public void Init(Enemy enemy, BattleMapManager battleMapManager, EnemyAIManager enemyAIManager, Main_Map map, int id)
    {
        this.enemy = enemy;
        this.battleMapManager = battleMapManager;
        this.map = map;
        this.enemyAIManager = enemyAIManager;
        this.enemyAIPattern = enemy.enemyAIPattern;
        this.actionTurn = enemy.actionTurn;
        this.enemyId = id;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        //ダメージテキスト読み込み
        damagePrefab = (GameObject)Resources.Load("DamageText");
    }



    /// <summary>
    /// 移動可能距離を表示する
    /// </summary>
    public List<DestinationAndAttackDto> HighlightMovableCells()
    {
        int moveAmount = enemy.job.move;

        //210215 ボスはステータス画面に移動力は表示するが、移動しない設定を追加
        if(enemyAIPattern == EnemyAIPattern.BOSS)
        {
            moveAmount = 0;
        }


        //移動距離を表示しながら攻撃対象を選択
        //210221 4番目のboolは警戒範囲表示モードか、敵ターンの実際の移動距離表示かのモード切替
        List<DestinationAndAttackDto> DestinationAndAttackDtoList =
            map.HighlightEnemyMovableCells(x, y, moveAmount, enemy.equipWeapon, false, enemyId);

        return DestinationAndAttackDtoList;
    }

    //210221 敵の攻撃可能なセルを自軍ターンの時に表示する
    public void HighLightWarningCells()
    {
        int moveAmount = enemy.job.move;

        //210215 ボスはステータス画面に移動力は表示するが、移動しない設定を追加
        if (enemyAIPattern == EnemyAIPattern.BOSS)
        {
            moveAmount = 0;
        }
        attackableCellList = map.HighlightEnemyWarnCells(x, y, moveAmount, enemy.equipWeapon, enemyId);
        

    }

    //210214 引数のプレイヤー達を探して、最も自分から相手に近いセルを返す
    public Main_Cell Search(PlayerModel[] players)
    {
        int moveAmount = enemy.job.move;

        //210215 ボスはステータス画面に移動力は表示するが、移動しない設定を追加
        if (enemyAIPattern == EnemyAIPattern.BOSS)
        {
            moveAmount = 0;
        }
        return map.SearchPlayerNearestCell(x, y, moveAmount, players);
    }

    //攻撃可能範囲を表示する
    public void Attack()
    {

        map.HighlightAttacableCells(x, y, enemy.equipWeapon);
    }

    /// <summary>
    /// ユニットを対象のマスに移動させます
    /// </summary>
    /// <param name="cell">Cell.</param>
    public void MoveTo(Main_Cell cell, bool isAttack)
    {
        int moveAmount = enemy.job.move;

        //210215 ボスはステータス画面に移動力は表示するが、移動しない設定を追加
        if (enemyAIPattern == EnemyAIPattern.BOSS)
        {
            moveAmount = 0;
        }

        //210212 ここで移動中にする
        battleMapManager.isMoving = true;

        //移動可能セルを消す
        map.ResetMovableCells();
        map.ResetAttackableCells();

        //アニメーションを走る動きに変更
        animator.SetBool("isMoving", true);

        //移動ルートのセル配列を取得
        Main_Cell[] routeCells;
        routeCells = map.CalculateRouteCells(x, y, moveAmount, cell, CalculationMode.ENEMY_MOVE);

        
        var sequence = DOTween.Sequence();
        for (var i = 1; i < routeCells.Length; i++)
        {
            //1マスを0.15秒で移動する
            var routeCell = routeCells[i];
            sequence.Append(transform.DOLookAt(routeCell.transform.position, 0.0075f))
                .Append(transform.DOMove(routeCell.transform.position, 0.1f).SetEase(Ease.Linear));
        }
        sequence.OnComplete(() =>
        {
            //要素は0スタートなのでLengthから1引いた値となる
            x = routeCells[routeCells.Length - 1].X;
            y = routeCells[routeCells.Length - 1].Y;
            isFocused = false;
            Debug.Log($"移動後の敵の座標 : x={x} , y = {y}");

            //モーションを停止に
            animator.SetBool("isMoving", false);

            if (isAttack)
            {
                //攻撃可能なので攻撃実施
                enemyAIManager.enemyAIPhase = EnemyAIPhase.ATTACK;
                Debug.Log("enemyAIPhase : " + enemyAIManager.enemyAIPhase);
            }
            else
            {
                //攻撃可能ではないので待機
                isMoved = true;
                SetBeforeAction(false);
                Debug.Log($"待機 : {enemy.name}");

                enemyAIManager.enemyAIPhase = EnemyAIPhase.GET_ENEMY;
                Debug.Log("enemyAIPhase : " + enemyAIManager.enemyAIPhase);
            }

            //移動中ではないのでフラグを戻しておく
            enemyAIManager.isEnemyMove = false;
            battleMapManager.isMoving = false;


        });
    }

    //210211 ダメージを受けた動きをセットする
    public void SetDamageMotion(bool isDamage)
    {
        animator.SetTrigger("Damage");
    }

    //ターン終了時等に行動前に戻す
    public void SetBeforeAction(bool isBeforeAction)
    {
        if (isBeforeAction)
        {
            beforeAction.SetActive(true);
        }
        else
        {
            beforeAction.SetActive(false);
        }
    }
}

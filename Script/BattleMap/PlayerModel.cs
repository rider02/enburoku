using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// SRPGパートでのユニット
/// マップでの座標などを保持
/// 内部にUnitクラスを保持してラッパークラスとして使用する
/// </summary>
public class PlayerModel : MonoBehaviour
{
    //200808 初期化面倒過ぎるのでPlayerModelをUnitのラッパーとする
    public Unit unit;

    BattleMapManager battleMapManager;

    public GameObject damagePrefab;

    public GameObject player;

    //210212 行動前ならエフェクトをユニット周囲に表示させる
    public GameObject beforeAction;

    Main_Map map;

    //アニメーションを変化させる
    private Animator animator;

    //200806 行動済みフラグ
    public bool isMoved { get; set; }

    //座標
    public int x { get; set; }
    public int z { get; set; }
    int moveAmount = 4;
    bool isFocused = false;

    //200808 初期化された時にUnitクラスを受け取る
    public　void Init(Unit unit, BattleMapManager battleMapManager, Main_Map map)
    {
        this.unit = unit;
        this.battleMapManager = battleMapManager;
        this.map = map;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        //ダメージテキスト読み込み
        damagePrefab = (GameObject)Resources.Load("DamageText");
    }

    //攻撃を受ける
    public void damage(int damage)
    {
        //ダメージ数値Prefub精製
        GameObject damageText = Instantiate(damagePrefab, this.transform.position,
            Quaternion.identity, player.transform);


        //数値を設定
        damageText.GetComponent<DamageText>().Init(damage.ToString());
    }

    /// <summary>
    /// 自軍ターンに選択された時、移動可能距離を表示する
    /// 200725 直接Main_MapをManagerから呼ばず、PlayerModelから呼んで移動距離、座標を渡す
    /// </summary>
    public void HighlightMovableCells()
    {
        //210226 スキル、ステータスアップの移動力増加計算
        StatusCalculator statusCalculator = new StatusCalculator();
        moveAmount = statusCalculator.calcMove(unit);

        map.HighlightMovableCells(x, z, moveAmount, unit.equipWeapon , CalculationMode.PLAYER_MOVE);
    }

    /// <summary>
    /// 敵を攻撃するボタンを押した時に呼ばれ、攻撃可能範囲を表示する
    /// </summary>
    /// <param name="weaponRange"></param>
    public void Attack(Weapon weapon)
    {
        //セルをハイライト
        map.HighlightAttacableCells(x, z, weapon);

        //210220 敵が存在するセルへカーソルを移動させる
        battleMapManager.MoveCursorToAttackAbleCell(weapon);
    }

    /// <summary>
    /// ユニットを対象のマスに移動させる
    /// </summary>
    /// <param name="cell">Cell.</param>
    public void MoveTo(Main_Cell cell)
    {

        //アニメーションを走る動きに変更
        animator.SetBool("isMoving", true);

        //移動ルートのセル配列を取得
        var routeCells = map.CalculateRouteCells(x, z, moveAmount, cell,CalculationMode.PLAYER_MOVE);
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
            //プレイヤーモデルの座標を更新
            //要素は0スタートなのでLengthから1引いた値となる
            x = routeCells[routeCells.Length - 1].X;
            z = routeCells[routeCells.Length - 1].Y;
            isFocused = false;

            //モーションを停止に
            animator.SetBool("isMoving", false);

            //移動中はフラグで管理する
            battleMapManager.isMoving = false;

            //210221 敵の攻撃可能範囲を再計算
            map.ReloadHighLightCells(this);
            
            // 移動後ウィンドウ表示
            battleMapManager.PrepareMapmodeMovedMenu();
        });
    }

    //210211 ダメージを受けた動きをセットする
    public void SetDamageMotion(bool isDamage)
    {
        animator.SetTrigger("Damage");
    }

    /// <summary>
    /// 200725 戻るボタンを押された時、アニメーション無しで移動
    /// </summary>
    /// <param name="cell"></param>
    public void ReturnTo(Main_Cell beforeCell)
    {
        this.transform.position = beforeCell.transform.position;

        x = beforeCell.X;
        z = beforeCell.Y;
    }

    //ターン終了等で使用 行動前にする
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

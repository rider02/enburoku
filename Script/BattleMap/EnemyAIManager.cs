using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

/// <summary>
/// 210212 戦闘シーンの敵AI処理を行う
/// </summary>
public class EnemyAIManager : MonoBehaviour
{

    private Main_Map mainMap;
    private BattleMapManager battleMapManager;
    Transform unitContainer;
    Transform enemyContainer;
    BattleMapCursor battleMapCursor;
    BattleManager battleManager;
    BattleTalkManager battleTalkManager;

    //カーソルが移動中か
    private bool isCursorMove = false;

    //敵が移動中か
    public bool isEnemyMove = false;

    private bool isAttackSelect = false;

    //攻撃範囲内に敵が居なければ探索モード
    private bool isAttackable;

    public EnemyAIPhase enemyAIPhase = EnemyAIPhase.GET_ENEMY;

    private float deltaTime;

    private float wait = 1f;

    //移動先と攻撃対象が入ったDTO
    private DestinationAndAttackDto DestinationAndAttackDto;

    //初期化 BattleMapManagerから呼ばれて依存性注入
    public void Init(BattleMapManager battleMapManager, BattleManager battleManager, BattleTalkManager battleTalkManager, Main_Map mainMap, 
        Transform unitContainer, Transform enemyContainer,
        BattleMapCursor battleMapCursor)
    {

        this.mainMap = mainMap;
        this.battleMapManager = battleMapManager;
        this.battleTalkManager = battleTalkManager;
        this.unitContainer = unitContainer;
        this.enemyContainer = enemyContainer;
        this.battleMapCursor = battleMapCursor;
        this.battleManager = battleManager;
    }

    //BattleMapManagerから呼ばれ、各種一連の動作を行う
    public void EnemyAIUpdate()
    {
        deltaTime += Time.deltaTime;

        //未行動の敵を取得
        if (enemyAIPhase == EnemyAIPhase.GET_ENEMY)
        {
            GetBeforeMoveEnemy();
        }
        else if (enemyAIPhase == EnemyAIPhase.MOVE_CURSOR)
        {
            //カーソル移動中
            MoveCursor();
        }
        else if (enemyAIPhase == EnemyAIPhase.MOVE)
        {
            //移動可能セル表示
            HighlightMovableCells();
        }
        else if (enemyAIPhase == EnemyAIPhase.MOVING)
        {
            //移動実行
            MoveTo();
        }
        else if (enemyAIPhase == EnemyAIPhase.ATTACK)
        {
            //攻撃可能セルを表示して攻撃実行
            Attack();
        }
        else if (enemyAIPhase == EnemyAIPhase.END)
        {
            //敵のターンを終了して自軍ターンへ遷移する
            EnemyTurnEnd();
        }
    }

    //未行動の敵を取得する
    private void GetBeforeMoveEnemy()
    {
        EnemyModel beforeActionEnemy = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
                enemy => enemy.isMoved == false);

        //未行動の敵が存在するので処理へ
        if (beforeActionEnemy != null)
        {
            Debug.Log("選択した敵 : " + beforeActionEnemy.enemy.name);
            //アクティブな敵を設定
            mainMap.activeEnemyName = beforeActionEnemy.enemy.name;
            mainMap.activeEnemyid = beforeActionEnemy.enemyId;

            //フェーズ変更
            enemyAIPhase = EnemyAIPhase.MOVE_CURSOR;
            Debug.Log("enemyAIPhase : " + enemyAIPhase);
        }
        else
        {
            Debug.Log("敵が全員行動済み");
            //既に未行動の敵が存在しなければ終了
            enemyAIPhase = EnemyAIPhase.END;
            Debug.Log("enemyAIPhase : " + enemyAIPhase);
        }
    }

    //アクティブな敵へカーソルを移動させていく
    private void MoveCursor()
    {

        //既にカーソル移動中なら何もしない
        if (isCursorMove)
        {
            return;
        }

        //カーソル移動を実行
        battleMapCursor.transform.DOMove(new Vector3(
            mainMap.ActiveEnemy.transform.position.x,
            mainMap.ActiveEnemy.transform.position.y + 0.5f, 
            mainMap.ActiveEnemy.transform.position.z), 0.5f).SetEase(Ease.OutSine).OnComplete(() => {
            enemyAIPhase = EnemyAIPhase.MOVE;
            Debug.Log("enemyAIPhase : " + enemyAIPhase);
            isCursorMove = false;

        });
        isCursorMove = true;
    }

    //移動可能なセルを表示して、攻撃可能かの判定を同時に行う
    private void HighlightMovableCells()
    {
        if(deltaTime <= wait)
        {
            return;
        }
        deltaTime = 0;

        //移動可能かつプレイヤーに攻撃可能なセル表示
        List<DestinationAndAttackDto> DestinationAndAttackDtoList = mainMap.ActiveEnemy.HighlightMovableCells();

        if(DestinationAndAttackDtoList.Count == 0)
        {
            //攻撃可能範囲にプレイヤーが見つからないので探索モード
            isAttackable = false;
            Debug.Log("攻撃不可");
        }
        else
        {
            //攻撃範囲にプレイヤーが居るので攻撃モード
            isAttackable = true;

            //TODO とりあえず適当な要素を取得するので、また優先順位の判定を作成する
            DestinationAndAttackDto = DestinationAndAttackDtoList[0];
            Debug.Log("攻撃可");

        }

        enemyAIPhase = EnemyAIPhase.MOVING;
        Debug.Log("enemyAIPhase : " + enemyAIPhase);
    }

    //敵ユニットの移動
    private void MoveTo()
    {
        //既に移動中の場合は処理が入らない
        if (isEnemyMove)
        {
            return;
        }

        isEnemyMove = true;
        Main_Cell destCell;

        //攻撃可能か否かで行き先セルを分岐
        if (isAttackable)
        {
            //行き先の座標をセルに変換
            destCell = mainMap.Cells.FirstOrDefault(c => c.X == DestinationAndAttackDto.x && c.Y == DestinationAndAttackDto.y);

            //事前に確認しているので通常セルが無い事は有り得ない
            if (destCell == null)
            {
                Debug.Log($"Error : 移動可能なセルが有りません : X = {DestinationAndAttackDto.x} , Y = {DestinationAndAttackDto.y}");
            }

            //移動先 = 現在いる場所で攻撃する場合は移動を行わないで攻撃へ
            if(destCell.X == mainMap.ActiveEnemy.x && destCell.Y == mainMap.ActiveEnemy.y)
            {
                isEnemyMove = false;
                //移動範囲、攻撃範囲消す
                mainMap.ResetMovableCells();
                mainMap.ResetAttackableCells();

                enemyAIPhase = EnemyAIPhase.ATTACK;
                Debug.Log("enemyAIPhase : " + enemyAIPhase);
                return;
            }

            //カーソル移動を実行してからユニット移動
            battleMapCursor.transform.DOMove(new Vector3(
                destCell.transform.position.x,
                destCell.transform.position.y + 0.5f,
                destCell.transform.position.z), 0.3f).SetEase(Ease.OutSine).OnComplete(() => {
                deltaTime = 0;

                //敵ユニットの移動実行 第二引数は攻撃か否か
                mainMap.ActiveEnemy.MoveTo(destCell, true);
                });
        }
        else
        {
            //プレイヤーに攻撃出来ない場合
            //反撃タイプ、ボスの敵は攻撃範囲にプレイヤーが居なければ待機する
            if(mainMap.ActiveEnemy.enemyAIPattern != EnemyAIPattern.SEARCH)
            {
                //移動可能セルを消す
                mainMap.ResetMovableCells();
                mainMap.ResetAttackableCells();

                //行動済みにして次の敵を取得
                mainMap.ActiveEnemy.isMoved = true;
                mainMap.ActiveEnemy.SetBeforeAction(false);
                enemyAIPhase = EnemyAIPhase.GET_ENEMY;
                Debug.Log("enemyAIPhase : " + enemyAIPhase);
                Debug.Log($"敵のAIパターン : {mainMap.ActiveEnemy.enemyAIPattern}");
                isEnemyMove = false;
            }
            else
            {
                //プレイヤーを探索するAIの場合
                //プレイヤーのユニットを検索して取得してセルに変換
                PlayerModel[] players = unitContainer.GetComponentsInChildren<PlayerModel>();
                if (players == null)
                {
                    Debug.Log("Error : プレイヤーが存在しません");
                }

                //プレイヤーの座標までの最短経路を取得
                Main_Cell playerNearestCell = mainMap.ActiveEnemy.Search(players);


                //カーソル移動を実行してからユニット移動
                battleMapCursor.transform.DOMove(new Vector3(
                    playerNearestCell.transform.position.x,
                    playerNearestCell.transform.position.y + 0.5f,
                    playerNearestCell.transform.position.z), 0.3f).SetEase(Ease.OutSine).OnComplete(() => {

                        deltaTime = 0;
                    //敵ユニットの移動実行 第二引数は攻撃か否か
                    mainMap.ActiveEnemy.MoveTo(playerNearestCell, false);
                    });
            }
        }
    }

    //もう攻撃対象は決まっているが、攻撃範囲を表示してからカーソルを移動させる
    private void Attack()
    {
        //既に攻撃対象範囲表示済みなら処理が入らない
        if (isAttackSelect)
        {
            return;
        }

        isAttackSelect = true;

        //攻撃可能範囲を表示
        mainMap.ActiveEnemy.Attack();

        Coordinate attackCoodinate = DestinationAndAttackDto.AttackCoordinate;
        PlayerModel destinationUnit = unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(
                c => c.x == attackCoodinate.x && c.z == attackCoodinate.y);

        //攻撃対象にカーソルを移動
        battleMapCursor.transform.DOMove(new Vector3(
            destinationUnit.transform.position.x,
            destinationUnit.transform.position.y + 0.5f,
            destinationUnit.transform.position.z), 0.3f).SetEase(Ease.OutSine).OnComplete(() => {

                //activeUnitをここで更新
                mainMap.activeUnitName = destinationUnit.unit.name;

                //TODO 選択したプレイヤーの方向を向く
                //攻撃可能範囲を消す
                mainMap.ResetAttackableCells();

                //200814 戦闘画面を開く
                battleManager.MapOpenBattleView(mainMap.ActiveUnit, mainMap.ActiveEnemy, false);

                deltaTime = 0;

                isAttackSelect = false;
            });
    }

    //待機中の敵が指定ターン数経過していた場合は
    public void WaitEnemyControl()
    {
        foreach(EnemyModel ememyModel in enemyContainer.GetComponentsInChildren<EnemyModel>())
        {
            if(ememyModel.enemyAIPattern == EnemyAIPattern.WAIT)
            {
                //指定ターン以上経過していたら行動パターン変更
                if(ememyModel.actionTurn <= battleMapManager.turn)
                {
                    ememyModel.enemyAIPattern = EnemyAIPattern.SEARCH;
                    Debug.Log($"{ememyModel.name}: 行動パターンを探索に変更");
                }
            }
        }
        
    }

    //敵のターン終了
    private void EnemyTurnEnd()
    {
        //カーソル位置を合わせる
        battleMapManager.SnapCursor();

        //敵ターン終了時に敵が存在しなければクリア
        if(enemyContainer.childCount == 0)
        {
            battleMapManager.StageClear();
            return;
        }

        //ここで全ての敵を未行動にする
        foreach(EnemyModel enemy in enemyContainer.GetComponentsInChildren<EnemyModel>()){

            enemy.isMoved = false;
            enemy.SetBeforeAction(true);
        }

        //210520 ターン開始前会話が存在すればターン開始前会話へ
        battleMapManager.SetMapMode(MapMode.TURN_START);
        Debug.Log("mapMode = " + battleMapManager.mapMode);
    }

    //戦闘後等に敵を行動済みにする
    public void BattleEnd()
    {
        if (mainMap.ActiveEnemy != null)
        {
            mainMap.ActiveEnemy.isMoved = true;
            mainMap.ActiveEnemy.SetBeforeAction(false);
        }
        //再度未行動の敵を取得する処理へ 行動を行った敵は行動済みなので、次の敵が選ばれる
        battleMapManager.SetMapMode(MapMode.ENEMY_TURN);

        SetEnemyAIPhase(EnemyAIPhase.GET_ENEMY);
    }

    //敵ターンの状態変更とログ出力
    public void SetEnemyAIPhase(EnemyAIPhase enemyAIPhase)
    {
        this.enemyAIPhase = enemyAIPhase;
        Debug.Log("EnemyAIPhase : " + enemyAIPhase);
    }
}

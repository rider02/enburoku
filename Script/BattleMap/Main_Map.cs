using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

//戦闘マップのクラス
public class Main_Map : MonoBehaviour
{
    [SerializeField]
    Main_Cell mainCell;
    
    //PlayerModel、EnemyModelを格納しておいて検索用に使う
    [SerializeField]
    Transform unitContainer;
    
    [SerializeField]
    Transform enemyContainer;

    [SerializeField]
    Transform treasureContainer;

    List<Main_Cell> cells = new List<Main_Cell>();

    //200808 現在選択されているユニットの名前
    public string activeUnitName;
    public string activeEnemyName;
    public int activeEnemyid;   //現在選択されている敵ID


    /// <summary>
    /// 選択中のユニットを返します
    /// </summary>
    /// <value>The active unit.</value>
    public PlayerModel ActiveUnit
    {
        get{ return unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(unit => unit.unit.name.Equals(activeUnitName)); }
    }

    public EnemyModel ActiveEnemy
    {
        get { return enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(enemy => enemy.enemyId.Equals(activeEnemyid)); }
    }

    void Start()
    {
        //全てのprefabのActiveを無効に
        foreach (Main_Cell prefab in new Main_Cell[] { mainCell })
        {
            prefab.gameObject.SetActive(false);
        }
    }

    

    /// <summary>
    /// 移動可能なマスをハイライトします
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="moveAmount">Move amount.</param>
    public void HighlightMovableCells(int x, int y, int moveAmount, Weapon equipWeapon , CalculationMode mode)
    {
        //まず移動可能なセルのハイライトを消す
        ResetMovableCells();

        //200724 渡された座標が最初のセルとなる
        //FirstはNullが返って来ない物に使うこと
        Main_Cell startCell = cells.First(c => c.X == x && c.Y == y);

        MoveAmountInfo[] moveableInfo = GetRemainingMoveAmountInfos(startCell, moveAmount, mode);
        foreach (var info in moveableInfo)
        {
            //移動可能にしていく trueになるとハイライトが青になる
            cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable = true;

        }

        //210219 先に移動可能か確認ループしてからでないと移動可能かどうかは分からないので2回ループ・・・
        foreach (var info in moveableInfo)
        {
            //210219 攻撃可能範囲の表示を追加
            if (equipWeapon != null)
            {
                Main_Cell attackCell = cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y);
                foreach (var Attackinfo in GetAttackRenge(attackCell, equipWeapon.range, equipWeapon.isCloseAttack))
                {
                    //攻撃可能にしていく trueになるとハイライトが赤になる
                    Main_Cell attackableCell = cells.First(c => c.X == Attackinfo.coordinate.x && c.Y == Attackinfo.coordinate.y);
                    if (attackableCell != null)
                    {
                        if (attackableCell.IsMovable == false)
                        {
                            attackableCell.IsAttackable = true;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 敵関連
    /// </summary>

    /// <summary>
    /// 210212 敵専用移動可能セルを表示しながら、攻撃可能かどうかを判定
    /// </summary>
    /// <param name="x">X座標</param>
    /// <param name="y">Y座標</param>
    /// <param name="moveAmount">移動力</param>
    /// <param name="weapon">武器</param>
    /// <param name="isWarning">警戒範囲表示モード(紫表示)か</param>
    /// <param name="enemyId">敵のID</param>
    /// <returns></returns>
    public List<DestinationAndAttackDto> HighlightEnemyMovableCells(int x, int y, int moveAmount, Weapon weapon, bool isWarning, int enemyId)
    {
        //まず移動可能なセルのハイライトを消す
        ResetMovableCells();

        //200724 渡された座標が最初のセルとなる
        //FirstはNullが返って来ない物に使うこと
        var startCell = cells.First(c => c.X == x && c.Y == y);

        //攻撃可能なセル一覧
        List<DestinationAndAttackDto> attackableCellList = new List<DestinationAndAttackDto>();

        //移動可能なセルを取得してハイライト処理を行う
        MoveAmountInfo[] enemyMoveableInfo = GetRemainingMoveAmountInfos(startCell, moveAmount, CalculationMode.ENEMY_MOVE);

        //移動可能セルは攻撃可能表示したくないので、まず移動可能セルのみ青色にハイライトする
        foreach (var info in enemyMoveableInfo)
        {
            if (isWarning)
            {
                //味方ターンで、敵の攻撃可能範囲を表示する時は紫で表示する
                cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).SetIsWarning(enemyId);
            }
            else
            {
                //敵ターンに実際移動する時
                //移動可能にしていく trueになるとハイライトが青になる
                cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable = true;
            }
            
        }
        //移動可能セルから攻撃対象が存在するか確認を行う処理
        foreach (var info in enemyMoveableInfo)
        {
            //ここで移動先に既に自分以外の敵がいる場合は、ハイライトはされるが実際は移動しないよう、対象に含まない
            //でも、自分自身の座標には移動可能にしないとその場から攻撃が出来ない
            EnemyModel destinationEnemy = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
                            c => c.x == info.coordinate.x && c.y == info.coordinate.y);

            //移動先に敵がいる場合、自分自身以外ならそのセルには移動しない
            if(destinationEnemy!= null)
            {
                if (destinationEnemy != ActiveEnemy)
                {
                    Debug.Log($"既に敵が存在するので移動しないセル X={info.coordinate.x}, Y={info.coordinate.y}");
                    continue;
                }
            }

            //それぞれの移動可能なセルに対して攻撃可能なセルを確認
            DestinationAndAttackDto destinationAndAttackDto = GetAttackableCells(info.coordinate.x, info.coordinate.y, weapon, enemyId);

            if(destinationAndAttackDto != null)
            {
                //それぞれのセルから攻撃可能なセルを取得を表示
                attackableCellList.Add(destinationAndAttackDto);
            }
        }

        Debug.Log("攻撃可能なセルの数 : " + attackableCellList.Count());

        return attackableCellList;
    }

    //210211 敵が攻撃可能なセルを取得
    private DestinationAndAttackDto GetAttackableCells(int x, int y, Weapon weapon, int enemyId)
    {
        //現在のセルを取得
        var startCell = cells.First(c => c.X == x && c.Y == y);

        DestinationAndAttackDto destinationAndAttackDto = null;

        //現在のセル攻撃可能範囲 取得 第三引数 : 攻撃時なので、敵のセルを選択可能
        foreach (var info in GetAttackRenge(startCell, weapon.range, weapon.isCloseAttack))
        {
            //Debug.Log($"プレイヤーを攻撃出来るか確認するセル : X={info.coordinate.x}, Y={info.coordinate.y}");

            //210219 攻撃可能セルの色を赤色にする
            var attackableCell = cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y);
            //セルが存在して移動可能でなければ(移動可能なセルは赤色にしない)
            if (attackableCell != null && attackableCell.IsMovable == false)
            {

                attackableCell.IsAttackable = true;

            }

            //攻撃可能なユニットが存在するか確認
            PlayerModel destinationUnit = unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(
                c => c.x == info.coordinate.x && c.z == info.coordinate.y);

            //移動可能範囲に攻撃可能なユニットが居れば
            if (destinationUnit != null)
            {
                Debug.Log($"攻撃可能な座標 :X = {startCell.X} , Y = {startCell.Y} ");
                Debug.Log("攻撃対象 : " + destinationUnit.unit.name);

                //攻撃可能なセルを返す
                //攻撃可能な座標をセット
                destinationAndAttackDto = new DestinationAndAttackDto(x, y);

                //攻撃対象のセルをセット
                Coordinate attackCoordinate = new Coordinate(info.coordinate.x, info.coordinate.y);
                destinationAndAttackDto.AttackCoordinate = attackCoordinate;

            }
        }
        //移動可能範囲に攻撃可能なユニットが居ない場合はnullが返ってくる
        return destinationAndAttackDto;
    }

    /// <summary>
    /// 自軍ターン時の敵の攻撃可能セル確認(紫表示)表示を行う
    /// 210304 警告処理を軽くする為にメソッドを分けた
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="moveAmount"></param>
    /// <param name="weapon"></param>
    /// <param name="isWarning"></param>
    /// <param name="enemyId"></param>
    /// <returns></returns>
    public List<Main_Cell> HighlightEnemyWarnCells(int x, int y, int moveAmount, Weapon weapon, int enemyId)
    {

        List<Main_Cell> warnCellList = new List<Main_Cell>();

        //200724 渡された座標が最初のセルとなる
        //FirstはNullが返って来ない物に使うこと
        var startCell = cells.First(c => c.X == x && c.Y == y);

        //移動可能なセルを取得してハイライト処理を行う
        MoveAmountInfo[] moveableInfo = GetRemainingMoveAmountInfos(startCell, moveAmount, CalculationMode.ENEMY_MOVE);


        //移動可能セルから攻撃対象が存在するか確認を行う処理
        foreach (var info in moveableInfo)
        {
            //まず移動可能セルをハイライト
            Main_Cell warnCell = cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y);
            warnCell.SetIsWarning(enemyId);
            warnCellList.Add(warnCell);

            //ここで移動先に既に自分以外の敵がいる場合は、ハイライトはされるが実際は移動して攻撃範囲計算しないよう、対象に含まない
            //でも、自分自身の座標には移動可能にしないとその場から攻撃が出来ない
            EnemyModel destinationEnemy = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
                            c => c.x == info.coordinate.x && c.y == info.coordinate.y);
            //移動先に敵がいる場合、自分自身以外ならそのセルには移動しない
            if (destinationEnemy != null)
            {
                if (destinationEnemy != ActiveEnemy)
                {
                    Debug.Log($"既に敵が存在するので移動しないセル X={info.coordinate.x}, Y={info.coordinate.y}");
                    continue;
                }
            }

            //それぞれの移動可能なセルに対して攻撃可能なセルを確認
            List<Main_Cell> attackableCellList = GetWarnAttackableCells(info.coordinate.x, info.coordinate.y, weapon, enemyId);
            warnCellList.AddRange(attackableCellList);
        }

        return warnCellList;
    }

    /// <summary>
    /// 210304 敵が攻撃可能な範囲を紫色にする
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="weapon"></param>
    /// <param name="isWarning"></param>
    /// <param name="enemyId"></param>
    /// <returns></returns>
    private List<Main_Cell> GetWarnAttackableCells(int x, int y, Weapon weapon, int enemyId)
    {
        //現在のセルを取得
        var startCell = cells.First(c => c.X == x && c.Y == y);
        List<Main_Cell> attackableCells = new List<Main_Cell>();

        //現在のセル攻撃可能範囲 取得 第三引数 : 攻撃時なので、敵のセルを選択可能
        foreach (var info in GetAttackRenge(startCell, weapon.range, weapon.isCloseAttack))
        {

            //210219 攻撃可能セルの色を変えてみる
            Main_Cell attackableCell = cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y);
            //セルが存在して既にハイライト済みでなければ
            if (attackableCell != null && attackableCell.IsWarning == false)
            {
                attackableCell.SetIsWarning(enemyId);
                attackableCells.Add(attackableCell);
            }
        }
        //移動可能範囲に攻撃可能なユニットが居ない場合はnullが返ってくる
        return attackableCells;
    }

    //210214 最もプレイヤーに近いセルを返す
    public Main_Cell SearchPlayerNearestCell(int x, int y, int moveAmount, PlayerModel[] players)
    {
        //探索モード
        //探索モードでは向かう対象 = 移動可能ではなく、移動力が最大 = 敵の現在いる座標という事にはならないので、
        //まず、プレイヤーに一番近いセルを移動可能距離の中から見つめてそこを行き先にする
        Debug.Log("プレイヤーへの最短距離を検索");
        MoveAmountInfo nearestInfo = null;
        int nearestDistance = 0;

        //x,yを元に移動開始するセルを取得
        var startCell = cells.First(c => c.X == x && c.Y == y);

        //移動可能となるセルリストがMoveAmountInfo型で返ってくる
        var moveAbleInfos = GetRemainingMoveAmountInfos(startCell, moveAmount, CalculationMode.ENEMY_SEARCH);

        foreach (PlayerModel player in players)
        {
            //プレイヤーの座標を取得
            Main_Cell playerCell = Cells.FirstOrDefault(c => c.X == player.x && c.Y == player.z);
            Debug.Log($"探索したプレイヤー{player.unit.name} x= {player.x} , y= {player.z}");

            foreach (MoveAmountInfo moveAbleInfo in moveAbleInfos)
            {
                //最初の1ループは存在しないので、まず設定
                if (nearestInfo == null)
                {
                    //既に自分以外の敵が居れば除外
                    if (isDestinationEnemyExist(moveAbleInfo))
                    {
                        continue;
                    }

                    nearestInfo = moveAbleInfo;
                    nearestDistance = (Mathf.Abs(playerCell.X - moveAbleInfo.coordinate.x) + Mathf.Abs(playerCell.Y - moveAbleInfo.coordinate.y));
                }
                else
                {
                    //2回目以降のループ
                    //攻撃対象への距離 少ない程優先する
                    int distance = (Mathf.Abs(playerCell.X - moveAbleInfo.coordinate.x) + Mathf.Abs(playerCell.Y - moveAbleInfo.coordinate.y));
                    Debug.Log($"座標 :X={moveAbleInfo.coordinate.x}, Y={moveAbleInfo.coordinate.y},距離={distance}");

                    //更にプレイヤーに近いセルが有れば最短セルを更新
                    if (distance < nearestDistance)
                    {
                        //移動先に敵がいるか判定
                        if (isDestinationEnemyExist(moveAbleInfo))
                        {
                            continue;
                        }

                        nearestInfo = moveAbleInfo;
                        Debug.Log($"最もプレイヤーに近いセル更新 :X={nearestInfo.coordinate.x}, Y={nearestInfo.coordinate.y}, 距離={nearestDistance}");
                        nearestDistance = distance;
                    }
                }
            }
        }

        Main_Cell nearestCell = cells.First(c => c.X == nearestInfo.coordinate.x && c.Y == nearestInfo.coordinate.y);
        Debug.Log($"最もプレイヤーに近いセル :X={nearestInfo.coordinate.x}, Y={nearestInfo.coordinate.y}, 距離={nearestDistance}");
        return nearestCell;
    }

    /// <summary>
    /// 移動先セルに敵が居るか判定 主に敵の移動で使用
    /// </summary>
    /// <param name="moveAbleInfo"></param>
    /// <returns></returns>
    private bool isDestinationEnemyExist(MoveAmountInfo moveAbleInfo)
    {
        //移動先に自分以外の敵が存在するか判定
        EnemyModel destinationEnemy = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
            c => c.x == moveAbleInfo.coordinate.x && c.y == moveAbleInfo.coordinate.y);

        //移動先に敵がいる場合、自分自身以外ならそのセルには移動しない
        if (destinationEnemy != null)
        {
            if (destinationEnemy != ActiveEnemy)
            {
                Debug.Log($"既に敵が存在するので移動しないセル X={moveAbleInfo.coordinate.x}, Y={moveAbleInfo.coordinate.y}");
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 210221 自軍ターンで味方が動いた時などに呼んで、敵の攻撃予測範囲を再計算する
    /// </summary>
    public void ReloadHighLightCells(PlayerModel playerModel)
    {
        bool isReload = false;
        foreach (EnemyModel enemyModel in enemyContainer.GetComponentsInChildren<EnemyModel>())
        {
            //既にハイライト済みの敵のみが対象 ハイライトしてなければ関係無いので処理しない
            if (enemyModel.isHighLight)
            {
                //攻撃範囲にユニットが居る敵のみ範囲再計算
                foreach (Main_Cell warnCell in enemyModel.attackableCellList)
                {
                    if (warnCell.X == playerModel.x && warnCell.Y == playerModel.z)
                    {
                        //1人でも攻撃範囲にユニットが居れば再計算は確定するのでbreak
                        isReload = true;
                        //再計算フラグ
                        enemyModel.isCancelReload = true;
                        break;
                    }
                }

                if (isReload)
                {
                    Debug.Log($"攻撃範囲にユニットが存在するので再計算:{enemyModel.enemy.name}");
                    //一度消さないと無限に増え続けてしまう
                    RemoveWarningCells(enemyModel.enemyId);

                    //再表示
                    enemyModel.HighLightWarningCells();
                }
            }
        }
    }

    //ユニットが移動をキャンセルした場合に再計算
    public void CancelReloadHighLightCells()
    {
        foreach (EnemyModel enemyModel in enemyContainer.GetComponentsInChildren<EnemyModel>())
        {
            //キャンセル時再計算フラグが立っている敵が対象
            if (enemyModel.isCancelReload)
            {

                RemoveWarningCells(enemyModel.enemyId);

                //再表示
                enemyModel.HighLightWarningCells();
                enemyModel.isCancelReload = false;
            }
        }
    }

    /// <summary>
    /// プレイヤー関連
    /// </summary>

    //210211 プレイヤーの攻撃可能範囲表示
    public void HighlightAttacableCells(int x, int y, Weapon weapon)
    {
        //まずセルのハイライトを消す
        ResetMovableCells();
        var startCell = cells.First(c => c.X == x && c.Y == y);

        //攻撃可能範囲取得 第三引数 : 攻撃時なので、敵のセルを選択可能
        foreach (var info in GetAttackRenge(startCell, weapon.range, weapon.isCloseAttack))
        {
            //攻撃可能にしていく trueになるとハイライトが赤になる
            Main_Cell cell = cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y);

            //210219 武器の時は赤色、そうでない時は緑色にする
            if (weapon.type != WeaponType.HEAL)
            {
                cell.IsAttackable = true;
            }
            else
            {
                cell.IsHealable = true;
            }
        }

    }

    /// <summary>
    /// 移動可能なマスのハイライトを消す
    /// </summary>
    public void ResetMovableCells()
    {
        //リストから移動可能な物を全て取得して移動不可にしていく
        foreach (var cell in cells.Where(c => c.IsMovable))
        {
            cell.IsMovable = false;
        }
    }

    /// <summary>
    /// 攻撃可能、回復可能なセルを消す
    /// </summary>
    public void ResetAttackableCells()
    {
        //リストから移動可能な物を全て取得して移動不可にしていく
        foreach (var cell in cells.Where(c => c.IsAttackable))
        {
            cell.IsAttackable = false;
        }
        foreach (var cell in cells.Where(c => c.IsHealable))
        {
            cell.IsHealable = false;
        }
    }

    /// <summary>
    /// 210221 敵のIdをセルから除去する
    /// </summary>
    /// <param name="id"></param>
    public void RemoveWarningCells(int enemyId)
    {
        foreach (var cell in cells.Where(c => c.IsWarning))
        {
            //セルのハイライトした敵リストから引数の敵を除去
            //全て空にならなければ誰かの攻撃可能範囲なので、警告ハイライト(紫)は消えない
            cell.RemoveIsWarning(enemyId);
        }
    }

    /// <summary>
    /// 210221 警戒範囲セルを全て消す 主に自軍のターンエンド時実行
    /// </summary>
    public void ResetWarningCells()
    {
        foreach (var cell in cells.Where(c => c.IsWarning))
        {
            //セルの警戒範囲を全て消して、表示した敵ユニットのIDを空にする
            cell.IsWarning = false;
            cell.highLightEnemyidSet.Clear();
        }

        //全ての敵ユニットのハイライトフラグを切る
        foreach (EnemyModel enemyModel in enemyContainer.GetComponentsInChildren<EnemyModel>())
        {
            enemyModel.isHighLight = false;
            enemyModel.attackableCellList.Clear();
            enemyModel.isCancelReload = false;
        }
    }



    /// <summary>
    /// 移動経路となるマスを返します
    /// </summary>
    /// <returns>The route cells.</returns>
    /// <param name="startCell">Start cell.</param>
    /// <param name="moveAmount">Move amount.</param>
    /// <param name="endCell">End cell.</param>
    /// //endCellは移動先のセルらしい
    public Main_Cell[] CalculateRouteCells(int x, int y, int moveAmount, Main_Cell endCell , CalculationMode mode)
    {
        //x,yを元に移動開始するセルを取得
        var startCell = cells.First(c => c.X == x && c.Y == y);

        //移動可能となるセルリストがMoveAmountInfo型で返ってくる
        var moveAbleInfos = GetRemainingMoveAmountInfos(startCell, moveAmount, mode);

        //移動経路のセルリスト作る
        List<Main_Cell> routeCells = new List<Main_Cell>();

        Debug.Log($"移動経路計算モード =  {mode}");

        //攻撃モードと探索モードだと検索方法が違う
        if (mode == CalculationMode.ENEMY_MOVE || mode == CalculationMode.PLAYER_MOVE)
        {
            Debug.Log("移動先への最短距離を検索");
            //移動可能リストにクリックした移動先のセルが入ってなかったらおかしいのでエラー
            if (!moveAbleInfos.Any(info => info.coordinate.x == endCell.X && info.coordinate.y == endCell.Y))
            {
                Debug.Log(string.Format("endCell(x:{0}, y:{1}) is not movable.", endCell.X, endCell.Y));
                throw new ArgumentException(string.Format("endCell(x:{0}, y:{1}) is not movable.", endCell.X, endCell.Y));
            }

            //まず最終移動先セルを追加
            routeCells.Add(endCell);
            //無限ループ
            while (true)
            {
                //まず最終移動先セルの情報から開始 配列は0スタートでCountは要素数を返すので-1する
                var currentCellInfo = moveAbleInfos.First(moveAbleInfo => moveAbleInfo.coordinate.x == routeCells[routeCells.Count - 1].X && moveAbleInfo.coordinate.y == routeCells[routeCells.Count - 1].Y);

                //現在のセルを現在のセル情報(MoveAmountInfo)から取得する
                var currentCell = cells.First(cell => cell.X == currentCellInfo.coordinate.x && cell.Y == currentCellInfo.coordinate.y);

                //1つ前のセルの移動力を算出 当然0から始まる
                var previousMoveAmount = currentCellInfo.amount + currentCell.Cost;

                //1つ前のセルの中から、移動力が同じ物を探す
                //ABS : 絶対値を返すので、X方向、Y方向だろうが1なら隣接しているセルとなる
                var previousCellInfo = moveAbleInfos.FirstOrDefault(moveAbleInfo => (Mathf.Abs(moveAbleInfo.coordinate.x - currentCell.X) + Mathf.Abs(moveAbleInfo.coordinate.y - currentCell.Y)) == 1 && moveAbleInfo.amount == previousMoveAmount);

                //最終的にはプレイヤーが今居る座標の移動力になり、最大移動力以上のセルは存在しないのでnullとなる
                if (null == previousCellInfo)
                {
                    break;
                }
                //移動経路リストに最短経路を追加する
                routeCells.Add(cells.First(c => c.X == previousCellInfo.coordinate.x && c.Y == previousCellInfo.coordinate.y));
            }
        }

            //最終目的地から追加していってるので順番を逆にする
            routeCells.Reverse();

        //配列で返す
        return routeCells.ToArray();
    }


    /// <summary>
    /// 210301 主に移動不可判定に使用 移動先に宝箱があるかを返す
    /// </summary>
    /// <param name="moveAbleInfo"></param>
    /// <returns></returns>
    private bool IsDestinationTreasureExist(Coordinate coordinate)
    {
        //移動先に自分以外の敵が存在するか判定
        TreasureModel destinationTreasure = treasureContainer.GetComponentsInChildren<TreasureModel>().FirstOrDefault(
            c => c.x == coordinate.x && c.y == coordinate.y);

        //移動先に敵がいる場合、自分自身以外ならそのセルには移動しない
        if (destinationTreasure != null)
        {
            Debug.Log($"宝箱が存在するので移動出来ないセル X={coordinate.x}, Y={coordinate.y}");
            return true;
        }
        return false;
    }

    /// <summary>
    /// 指定座標にユニットを配置します
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="unitPrefab">Unit prefab.</param>
    public void PutUnit(int x, int z, PlayerModel unit)
    {
        unit.gameObject.SetActive(true);

        //200725 unitContainer配下に配置
        unit.transform.SetParent(unitContainer);
        //ユニットを表示する位置を設定 同じ座標のセルの位置をユニットに設定
        unit.transform.position = cells.First(c => c.X == x && c.Y == z).transform.position;

        //ユニットの居る座標を設定 これは判定などにも用いる実際の座標
        unit.x = x;
        unit.z = z;
    }

    public void PutEnemy(int x, int z, EnemyModel enemy)
    {
        enemy.gameObject.SetActive(true);

        enemy.transform.SetParent(enemyContainer);
        //ユニットを表示する位置を設定 同じ座標のセルの位置をユニットに設定
        enemy.transform.position = cells.First(c => c.X == x && c.Y == z).transform.position;

        //ユニットの居る座標を設定 これは判定などにも用いる実際の座標
        enemy.x = x;
        enemy.y = z;
    }

    //210301 宝箱を追加
    public void PutTreasure(int x, int z, TreasureModel treasure)
    {
        treasure.gameObject.SetActive(true);

        treasure.transform.SetParent(treasureContainer);
        //宝箱を表示する位置を設定
        treasure.transform.position = cells.First(c => c.X == x && c.Y == z).transform.position;

        //宝箱の有る座標を設定 これは判定などにも用いる実際の座標
        treasure.x = x;
        treasure.y = z;
    }

    /// <summary>
    /// 移動力を元に移動可能範囲の計算を行います
    /// </summary>
    /// <returns>The remaining move amount infos.</returns>
    /// <param name="startCell">Start cell.</param>
    /// <param name="moveAmount">Move amount.</param>
    MoveAmountInfo[] GetRemainingMoveAmountInfos(Main_Cell startCell, int moveAmount , CalculationMode mode)
    {

        //座標と移動可能距離を持つリストを用意する まずは現在位置で最大の移動可能距離
        var infos = new List<MoveAmountInfo>();
        infos.Add(new MoveAmountInfo(startCell.X, startCell.Y, moveAmount));

        //残り移動可能距離を1ずつ減らしていって、0になるまでループまわす
        for (var i = moveAmount; i >= 0; i--)
        {
            //追加していく移動可能距離リスト作成
            var appendInfos = new List<MoveAmountInfo>();

            //amountはそのセルでの残り移動可能距離 まずは現在位置からスタート
            foreach (var calcTargetInfo in infos.Where(info => info.amount == i))
            {
                // 四方のマスの座標配列を作成
                var calcTargetCoordinate = calcTargetInfo.coordinate;//TODO ???
                var aroundCellCoordinates = new Coordinate[]//上下左右の座標配列
                {
                    new Coordinate(calcTargetCoordinate.x - 1, calcTargetCoordinate.y),//左
                    new Coordinate(calcTargetCoordinate.x + 1, calcTargetCoordinate.y),//右
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y - 1),//上
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y + 1),//下
                };

                // 四方のマスの残移動力を計算 上下左右マスの座標を順に取り出し
                foreach (var aroundCellCoordinate in aroundCellCoordinates)
                {
                    //FirstOrDefaultとは？ 現在地から上下左右の座標を元にマップの座標を取得
                    var targetCell = cells.FirstOrDefault(c => c.X == aroundCellCoordinate.x && c.Y == aroundCellCoordinate.y);

                    //nullはマイナス座標などでマップの範囲外とか infoとappendInfosにもう同じ座標のセルが有ればスルー
                    //もうそのセルは移動可能にならないし
                    if (null == targetCell ||
                        infos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y) ||
                        appendInfos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y))
                    {
                        // マップに存在しない、または既に計算済みの座標はスルー
                        continue;
                    }

                    //移動先に宝箱がある場合、敵、味方共に移動不可
                    if (IsDestinationTreasureExist(aroundCellCoordinate))
                    {
                        continue;
                    }

                    if (mode == CalculationMode.PLAYER_MOVE)
                    {
                        //210212 プレイヤー移動時、敵の居るセルは移動不可能にする 攻撃時は選択可能
                        EnemyModel destinationEnemy = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
                            c => c.x == aroundCellCoordinate.x && c.y == aroundCellCoordinate.y);
                        if (destinationEnemy != null)
                        {
                            continue;
                        }
                    }
                    else if (mode == CalculationMode.ENEMY_MOVE || mode == CalculationMode.ENEMY_SEARCH)
                    {
                        //同じように、敵はプレイヤーが居るセルには移動出来ない
                        PlayerModel destinationPlayer = unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(
                            c => c.x == aroundCellCoordinate.x && c.z == aroundCellCoordinate.y);
                        if (destinationPlayer != null)
                        {
                            continue;
                        }
                    }

                    //残りの移動可能距離を取得、対象セルから移動可能コストを引く
                    int remainingMoveAmount;
                    remainingMoveAmount = i - targetCell.Cost;

                    appendInfos.Add(new MoveAmountInfo(aroundCellCoordinate.x, aroundCellCoordinate.y, remainingMoveAmount));

                }//四方のマス取得ループ
            }
            //上下左右の座標リストを取得し終わったらinfosにぶちこむ
            infos.AddRange(appendInfos);
        }

        // 残移動力が0以上（移動可能）なマスの情報だけを返す
        return infos.Where(x => x.amount >= 0).ToArray();
    }

    //210214 移動距離計算と攻撃範囲計算を分割
    //攻撃可能な座標を返す
    MoveAmountInfo[] GetAttackRenge(Main_Cell startCell, int attackRange, bool isCloseAttack)
    {
        //Debug.Log($"射程距離:{attackRange}, 近距離攻撃:{isCloseAttack}");

        //座標と移動可能距離を持つリストを用意する まずは現在位置で最大の移動可能距離
        var infos = new List<MoveAmountInfo>();
        infos.Add(new MoveAmountInfo(startCell.X, startCell.Y, attackRange));

        //残り移動可能距離を1ずつ減らしていって、0になるまでループまわす
        for (var i = attackRange; i >= 0; i--)
        {
            //追加していく移動可能距離リスト作成
            List<MoveAmountInfo> appendInfos = new List<MoveAmountInfo>();

            //amountはそのセルでの残り移動可能距離 まずは現在位置からスタート
            foreach (var calcTargetInfo in infos.Where(info => info.amount == i))
            {
                // 四方のマスの座標配列を作成
                var calcTargetCoordinate = calcTargetInfo.coordinate;//TODO ???
                var aroundCellCoordinates = new Coordinate[]//上下左右の座標配列
                {
                    new Coordinate(calcTargetCoordinate.x - 1, calcTargetCoordinate.y),//左
                    new Coordinate(calcTargetCoordinate.x + 1, calcTargetCoordinate.y),//右
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y - 1),//上
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y + 1),//下
                };

                // 四方のマスの残移動力を計算 上下左右マスの座標を順に取り出し
                foreach (var aroundCellCoordinate in aroundCellCoordinates)
                {
                    //FirstOrDefaultとは？ 現在地から上下左右の座標を元にマップの座標を取得
                    var targetCell = cells.FirstOrDefault(c => c.X == aroundCellCoordinate.x && c.Y == aroundCellCoordinate.y);

                    //nullはマイナス座標などでマップの範囲外とか infoとappendInfosにもう同じ座標のセルが有ればスルー
                    //もうそのセルは移動可能にならないし
                    if (null == targetCell ||
                        infos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y) ||
                        appendInfos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y))
                    {
                        // マップに存在しない、または既に計算済みの座標はスルー
                        continue;
                    }

                    //残りの攻撃可能範囲を取得
                    int remainingRangeAmount;

                    //プレイヤー、敵の攻撃範囲表示モードでは、移動コストは存在しないので常に1減る
                    remainingRangeAmount = i - 1;

                    appendInfos.Add(new MoveAmountInfo(aroundCellCoordinate.x, aroundCellCoordinate.y, remainingRangeAmount));

                }//四方のマス取得ループ
            }
            //上下左右の座標リストを取得し終わったらinfosにぶちこむ
            infos.AddRange(appendInfos);
        }

        if (!isCloseAttack)
        {
            //210211 攻撃範囲表示の時、近接武器でなければ自分のすぐ隣のセルは返さない
            //moveAmount-1は自分の一番近いセルを除くということ

            return infos.Where(x => x.amount >= 0 && x.amount < attackRange - 1).ToArray();
        }
        else
        {
            //近接攻撃可能　残射程が0以上（攻撃可能）なマスの情報だけを返す かつ自分自身は含まない
            return infos.Where(x => x.amount >= 0 && x.amount < attackRange).ToArray();
        }

    }

    /// <summary>
    /// 210301 周囲に回復出来るユニットが居るかを返す
    /// </summary>
    /// <param name="startCell"></param>
    /// <returns></returns>
    public List<PlayerModel> GetHealableUnit(Main_Cell startCell, Unit unit)
    {
        if (startCell == null)
        {
            Debug.Log("warn:セルが存在しません");
            //画面端だとセルが無い場合がある
            return null;
        }

        List<PlayerModel> healableUnitList = new List<PlayerModel>();

        //ユニットの持つ武器で最も長い射程を取得
        //TODO これ、射程距離 = 遠攻撃 / 2のリブローが有るのでその内実装する事

        int maxRange = 1 - 1;

        //座標と移動可能距離を持つリストを用意する まずは現在位置で最大の移動可能距離
        var infos = new List<MoveAmountInfo>();
        infos.Add(new MoveAmountInfo(startCell.X, startCell.Y, maxRange));

        //残り移動可能距離を1ずつ減らしていって、0になるまでループまわす
        for (var i = maxRange; i >= 0; i--)
        {
            //追加していく移動可能距離リスト作成
            var appendInfos = new List<MoveAmountInfo>();

            //amountはそのセルでの残り移動可能距離 まずは現在位置からスタート
            foreach (var calcTargetInfo in infos.Where(info => info.amount == i))
            {
                // 四方のマスの座標配列を作成
                var calcTargetCoordinate = calcTargetInfo.coordinate;//TODO ???
                var aroundCellCoordinates = new Coordinate[]//上下左右の座標配列
                {
                    new Coordinate(calcTargetCoordinate.x - 1, calcTargetCoordinate.y),//左
                    new Coordinate(calcTargetCoordinate.x + 1, calcTargetCoordinate.y),//右
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y - 1),//上
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y + 1),//下
                };

                // 四方のマスの残移動力を計算 上下左右マスの座標を順に取り出し
                foreach (var aroundCellCoordinate in aroundCellCoordinates)
                {
                    //FirstOrDefaultとは？ 現在地から上下左右の座標を元にマップの座標を取得
                    var targetCell = cells.FirstOrDefault(c => c.X == aroundCellCoordinate.x && c.Y == aroundCellCoordinate.y);

                    //nullはマイナス座標などでマップの範囲外とか infoとappendInfosにもう同じ座標のセルが有ればスルー
                    //もうそのセルは移動可能にならないし
                    if (null == targetCell ||
                        infos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y) ||
                        appendInfos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y))
                    {
                        // マップに存在しない、または既に計算済みの座標はスルー
                        continue;
                    }

                    PlayerModel playerModel = unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(
                        c => c.x == aroundCellCoordinate.x && c.z == aroundCellCoordinate.y);

                    if(i >= maxRange)
                    //仲間が存在したら
                    if (playerModel != null)
                    {
                        StatusCalculator statusCalc = new StatusCalculator();
                        int maxHp = playerModel.unit.maxhp + playerModel.unit.job.statusDto.jobHp;
                        maxHp = statusCalc.CalcHpBuff(maxHp, playerModel.unit.job.skills);

                        //回復出来る仲間が居ればリストに入れる
                        if (playerModel.unit.hp < maxHp)
                        {
                            healableUnitList.Add(playerModel);

                            Debug.Log($"回復出来る仲間を発見 X:{aroundCellCoordinate.x}, Y:{aroundCellCoordinate.y}");
                        }
                    }

                    //残りの移動可能距離を取得、対象セルから移動可能コストを引く
                    int remainingMoveAmount;
                    remainingMoveAmount = i - 1;

                    appendInfos.Add(new MoveAmountInfo(aroundCellCoordinate.x, aroundCellCoordinate.y, remainingMoveAmount));

                }//四方のマス取得ループ
            }

            infos.AddRange(appendInfos);
        }

        //ここまで処理が入れば攻撃可能範囲内に敵は存在しない
        return healableUnitList;
    }

    /// <summary>
    /// 210301 周囲に攻撃出来る敵が居るかを返す
    /// </summary>
    /// <param name="startCell"></param>
    /// <returns></returns>
    public List<EnemyModel> GetAttackableEnemy(Main_Cell startCell, Unit unit)
    {
        if (startCell == null)
        {
            Debug.Log("warn:セルが存在しません");
            //画面端だとセルが無い場合がある
            return null;
        }

        List<EnemyModel> attackableEnemyList = new List<EnemyModel>();

        //ユニットの持つ武器で最も長い射程を取得
        int maxRange = 0;
        
        foreach(Item item in unit.carryItem)
        {
            if(item.ItemType == ItemType.WEAPON)
            {
                if(item.weapon.range > maxRange)
                {
                    maxRange = item.weapon.range - 1;
                }
            }
        }

        Debug.Log($"最大射程:{maxRange}");

        //座標と移動可能距離を持つリストを用意する まずは現在位置で最大の移動可能距離
        var infos = new List<MoveAmountInfo>();
        infos.Add(new MoveAmountInfo(startCell.X, startCell.Y, maxRange));

        //残り移動可能距離を1ずつ減らしていって、0になるまでループまわす
        for (var i = maxRange; i >= 0; i--)
        {
            //追加していく移動可能距離リスト作成
            var appendInfos = new List<MoveAmountInfo>();

            //amountはそのセルでの残り移動可能距離 まずは現在位置からスタート
            foreach (var calcTargetInfo in infos.Where(info => info.amount == i))
            {
                // 四方のマスの座標配列を作成
                var calcTargetCoordinate = calcTargetInfo.coordinate;//TODO ???
                var aroundCellCoordinates = new Coordinate[]//上下左右の座標配列
                {
                    new Coordinate(calcTargetCoordinate.x - 1, calcTargetCoordinate.y),//左
                    new Coordinate(calcTargetCoordinate.x + 1, calcTargetCoordinate.y),//右
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y - 1),//上
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y + 1),//下
                };

                // 四方のマスの残移動力を計算 上下左右マスの座標を順に取り出し
                foreach (var aroundCellCoordinate in aroundCellCoordinates)
                {
                    //FirstOrDefaultとは？ 現在地から上下左右の座標を元にマップの座標を取得
                    var targetCell = cells.FirstOrDefault(c => c.X == aroundCellCoordinate.x && c.Y == aroundCellCoordinate.y);

                    //nullはマイナス座標などでマップの範囲外とか infoとappendInfosにもう同じ座標のセルが有ればスルー
                    //もうそのセルは移動可能にならないし
                    if (null == targetCell ||
                        infos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y) ||
                        appendInfos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y))
                    {
                        // マップに存在しない、または既に計算済みの座標はスルー
                        continue;
                    }

                    EnemyModel enemyModel = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
                            c => c.x == aroundCellCoordinate.x && c.y == aroundCellCoordinate.y);

                    if(enemyModel != null)
                    {
                        attackableEnemyList.Add(enemyModel);
                        Debug.Log($"敵を発見 X:{aroundCellCoordinate.x}, Y:{aroundCellCoordinate.y}");
                        

                    }

                    //残りの移動可能距離を取得、対象セルから移動可能コストを引く
                    int remainingMoveAmount;
                    remainingMoveAmount = i - 1;

                    appendInfos.Add(new MoveAmountInfo(aroundCellCoordinate.x, aroundCellCoordinate.y, remainingMoveAmount));

                }//四方のマス取得ループ
            }
            
            infos.AddRange(appendInfos);
        }

        return attackableEnemyList;
    }

    /// <summary>
    /// 210301 周囲に宝箱が存在するかを返す
    /// </summary>
    /// <param name="startCell"></param>
    /// <returns></returns>
    public Coordinate SearchTreasureBox(Main_Cell startCell)
    {
        if (startCell == null)
        {
            Debug.Log("warn:セルが存在しません");
            //画面端だとセルが無い場合がある
            return null;
        }

        // 四方のマスの座標配列を作成
        var aroundCellCoordinates = new Coordinate[]//上下左右の座標配列
        {
            new Coordinate(startCell.X - 1, startCell.Y),//左
            new Coordinate(startCell.X + 1, startCell.Y),//右
            new Coordinate(startCell.X, startCell.Y - 1),//上
            new Coordinate(startCell.X, startCell.Y + 1),//下
        };

        // 四方のマスの残移動力を計算 上下左右マスの座標を順に取り出し
        foreach (Coordinate aroundCellCoordinate in aroundCellCoordinates)
        {
            //FirstOrDefaultとは？ 現在地から上下左右の座標を元にマップの座標を取得
            TreasureModel treasureModel = treasureContainer.GetComponentsInChildren<TreasureModel>().FirstOrDefault(
                            c => c.x == aroundCellCoordinate.x && c.y == aroundCellCoordinate.y);

            //宝箱が有った時点で座標を返す
            //もし四方に宝箱が有るような状況なら、配列の先頭の要素、左から開け始める
            if (treasureModel != null)
            {
                //空の場合は無いとみなす
                if (treasureModel.treasureBox.isEmpty)
                {
                    continue;
                }
                Debug.Log($"宝箱を発見 X:{aroundCellCoordinate.x}, Y:{aroundCellCoordinate.y}");
                return aroundCellCoordinate;
            }
        }
        
        //周囲に宝箱が無ければnullを返す nullチェック注意
        return null;
    }

    /// <summary>
    /// 210301カーソルを移動させた時、宝箱が有るかどうか判定
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsTreasureExist(Main_Cell cell)
    {
        if(cell == null)
        {
            Debug.Log("warn:セルが存在しません");
            //画面端だとセルが無い場合がある
            return false;
        }

        TreasureModel treasureModel = treasureContainer.GetComponentsInChildren<TreasureModel>().FirstOrDefault(
                            c => c.x == cell.X && c.y == cell.Y);

        //宝箱が有った時点で座標を返す
        //もし四方に宝箱が有るような状況なら、配列の先頭の要素、左から開け始める
        if (treasureModel != null)
        {
            return true;
        }
        return false;
    }


    public List<Main_Cell> Cells
    {
        get { return cells; }
    }


    /// <summary>
    /// 200802 エディタモード追加の為、セルの種類を表示したり消したりする
    /// </summary>
    /// <param name="editMode"></param>
    public void ShowSellType(bool editMode)
    {
        foreach(var cell in cells)
        {
            if (editMode)
            {
                cell.typeText.gameObject.SetActive(true);
            }
            else
            {
                cell.typeText.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 200803 エディタの「保存」ボタンをクリックするとScriptableObjectにしてくれる
    /// </summary>
    public void SaveMap(Stage stage)
    {
        //210522 実行ファイルにする場合はコメントアウト
        //MapDataCreator.Create(cells, stage);

    }

    /// <summary>
    /// 200802 マップのロードを行う
    /// </summary>
    public void LoadMap(Stage stage)
    {
        //引数で読み込むファイルを分岐 霊夢ルートステージ1ならSTAGE1mapData
        MapData mapData = Resources.Load<MapData>($"{stage.chapter.ToString()}mapData");
        if(mapData == null)
        {
            Debug.Log("ERROR:地形データが存在しませんでした。");
            return;
        }
        foreach (var cell in cells)
        {
            //リスト内の全てのセルを初期化
            Destroy(cell.gameObject);
        }
        cells = new List<Main_Cell>();

        
        List<CellData> cellDataList = mapData.cells;

        foreach (CellData cellData in cellDataList)
        {
            //座標を入れると勝手に配置、種類を入れると勝手に他のパラメータも入る
            Main_Cell cell = Instantiate(mainCell);
            cell.gameObject.SetActive(true);
            cell.transform.SetParent(transform);
            cell.SetType(cellData.type);

            //座標と共に高さを設定
            cell.SetCoordinate(cellData.x, cellData.y,cell.Type);
            cell.IsMovable = false;
            cells.Add(cell);
        }

        //210302 trueにするとセルの種類が表示される
        ShowSellType(false);
        Debug.Log($"マップデータを読み込みました。");
    }

    /// <summary>
    /// 残移動力情報クラス
    /// </summary>
    class MoveAmountInfo
    {
        public readonly Coordinate coordinate;
        public readonly int amount;

        //コンストラクタ
        public MoveAmountInfo(int x, int y, int amount)
        {
            this.coordinate = new Coordinate(x, y);
            this.amount = amount;
        }
    }

    /// <summary>
    /// マップ自動精製
    /// 210522 ロードするようになったので、初回のマップ精製時のみ使用
    /// </summary>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    public void Generate(int width, int height)
    {
        foreach (var cell in cells)
        {
            //リスト内の全てのセルを初期化
            Destroy(cell.gameObject);
        }

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Main_Cell cell;
                //1～10の乱数でマップを生成
                //TODO 2次元配列でマップを実装出来るようにする
                var rand = UnityEngine.Random.Range(0, 10);

                //0～7の時は普通のフィールド
                if (rand <= 5)
                {
                    cell = Instantiate(mainCell);
                    cell.SetType(CellType.Field);

                }
                else if (rand >= 6 && rand <= 8)
                {
                    //1、2の時は茂み
                    cell = Instantiate(mainCell);
                    cell.SetType(CellType.Tree);
                }
                else
                {
                    //壁
                    cell = Instantiate(mainCell);
                    cell.SetType(CellType.Block);
                }
                //セルを表示
                cell.gameObject.SetActive(true);
                //cellの配下にオブジェクトが生成されるようになる
                cell.transform.SetParent(transform);
                //セルのxとyを設定
                cell.SetCoordinate(x, y, cell.Type);
                cells.Add(cell);
            }
        }
    }

}


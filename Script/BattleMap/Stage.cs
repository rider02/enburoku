using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの出撃可能人数、出撃画面フラグ、ユニットの座標を管理するクラス
/// アセットファイルStageDatabaseで管理
/// </summary>
[System.Serializable]
public class Stage
{
    //ステージ数
    public Chapter chapter;

    //出撃キャラ選択画面を表示するか
    public bool isUnitSelectRequired;

    //200829 霊夢ルートか
    public bool isReimuRoute;

    //出撃可能ユニットの人数
    public int entryUnitCount;

    //勝利条件
    public WinCondition winCondition;

    //敗北条件
    public LoseCondition loseCondition;

    //210513 あらすじ
    public string storyText;

    //出撃するユニット達が配置される座標
    public List<Coordinate> entryUnitCoordinates;

    public List<Enemy> enemyList;

    public List<TreasureBox> treasureList;

    //コンストラクタ
    public Stage(Chapter chapter, bool isUnitSelectRequired, int entryUnitCount, List<Coordinate> entryUnitCoordinates,
        List<Enemy> enemyList, WinCondition winCondition, LoseCondition loseCondition, string storyText ,bool isReimuRoute, List<TreasureBox> treasureList)
    {

        this.chapter = chapter;
        this.isUnitSelectRequired = isUnitSelectRequired;
        this.entryUnitCount = entryUnitCount;
        this.entryUnitCoordinates = entryUnitCoordinates;
        this.winCondition = winCondition;
        this.loseCondition = loseCondition;
        this.storyText = storyText;
        this.isReimuRoute = isReimuRoute;
        this.enemyList = enemyList;
        this.treasureList = treasureList;
    }
}

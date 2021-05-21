using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

/// <summary>
/// 210513 ステージ選択画面でのステージ概要表示用クラス
/// </summary>
public class StageSelectDetailWindow : MonoBehaviour
{
    //ステージ 第10章 紅魔館最上階(後編)
    [SerializeField] Text chapter;

    //章の名前
    [SerializeField] Text chapterName;

    //出撃人数
    [SerializeField] Text entryCount;

    //勝利条件
    [SerializeField] Text winCondition;

    //敗北条件
    [SerializeField] Text loseCondition;

    //敵の平均Lv
    [SerializeField] Text enemyLevelAverage;

    //あらすじ
    [SerializeField] Text storyText;

    public void UpdateText(Stage stage)
    {
        this.chapter.text = string.Format("第{0}章",
            (int)stage.chapter, stage.chapter.GetStringValue());

        this.chapterName.text = stage.chapter.GetChapterValue();

        //出撃人数、勝利敗北条件
        this.entryCount.text = string.Format("{0}人", stage.entryUnitCount);
        this.winCondition.text = stage.winCondition.GetStringValue();
        this.loseCondition.text = stage.loseCondition.GetStringValue();

        //敵の平均Lv
        List<int> enemyLevelList = new List<int>();

        //ステージの全敵のレベルを取得
        foreach (Enemy enemy in stage.enemyList)
        {
            enemyLevelList.Add(enemy.lv);
        }

        //四捨五入した敵の平均Lvを表示
        int enemyLevelAverageNum = Mathf.RoundToInt((float)enemyLevelList.Average());
        enemyLevelAverage.text = enemyLevelAverageNum.ToString();

        //あらすじ
        this.storyText.text = stage.storyText;
    }
}

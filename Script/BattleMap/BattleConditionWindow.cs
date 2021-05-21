using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//200816 マップのメニュー画面でターン数とか表示する
public class BattleConditionWindow : MonoBehaviour
{
    //ステージ 第10章 紅魔館最上階(後編)
    [SerializeField] Text chapter;

    [SerializeField] Text chapterName;

    [SerializeField] Text unitCount;

    [SerializeField] Text enemyCount;

    //勝利条件
    [SerializeField] Text winCondition;

    //敗北条件
    [SerializeField] Text loseCondition;

    [SerializeField] Text turn;

    public void Init(Stage stage)
    {

        this.chapter.text = string.Format("第{0}章",
            (int)stage.chapter, stage.chapter.GetStringValue());

        this.chapterName.text = stage.chapter.GetStringValue();

        this.winCondition.text = stage.winCondition.GetStringValue();
        this.loseCondition.text = stage.loseCondition.GetStringValue();

        this.turn.text = string.Format("{0}ターン","1");
    }

    //ターン数更新
    public void UpdateTurn(int turn)
    {
        this.turn.text = string.Format("{0}ターン", turn.ToString());
    }

    //210231 やっと実装 開いたタイミングで敵と味方のユニット数を更新
    public void UpdateUnitCount(int unitCount, int enemyCount)
    {
        this.unitCount.text = $"{unitCount.ToString()}人";
        this.enemyCount.text = $"{enemyCount.ToString()}人";
    }
}

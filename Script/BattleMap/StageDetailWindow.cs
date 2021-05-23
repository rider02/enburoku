using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージの情報を表示させるクラス
/// </summary>
public class StageDetailWindow : MonoBehaviour
{
    //ステージ 第10章 紅魔館最上階(後編)
    [SerializeField] Text chapter;

    [SerializeField] Text chapterName;

    //出撃人数
    [SerializeField] Text entryCount;

    //勝利条件
    [SerializeField] Text winCondition;

    //敗北条件
    [SerializeField] Text loseCondition;

    public void Init(Stage stage)
    {
        this.chapter.text = string.Format("第{0}章", 
            (int)stage.chapter, stage.chapter.GetStringValue());

        this.chapterName.text = stage.chapter.GetStringValue();

        this.entryCount.text = string.Format("{0}人", stage.entryUnitCount);

        this.winCondition.text = stage.winCondition.GetStringValue();
        this.loseCondition.text = stage.loseCondition.GetStringValue();
    }
}

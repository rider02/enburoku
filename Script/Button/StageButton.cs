using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 200808 ステージを選択するボタン
/// </summary>
public class StageButton : MonoBehaviour
{

    //ステージ名
    [SerializeField]
    Text buttonText;

    private Chapter chapter;

    private StageSelectManager stageSelectManager;

    //初期化メソッド
    public void Init(Chapter chapter, StageSelectManager stageSelectManager)
    {

        this.chapter = chapter;
        buttonText.text = chapter.GetStringValue();

        this.stageSelectManager = stageSelectManager;
    }

    public void Onclick()
    {

        stageSelectManager.ChangeSceneToMap(this.chapter);
    }

    public void OnSelect()
    {
        stageSelectManager.ChangeStageDetailWindowText(this.chapter);
    }
}

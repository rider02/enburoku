using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 200808 ステージを選択するボタン
/// </summary>
public class StageButton : MonoBehaviour
{

    [SerializeField] Text buttonText;   //ステージ名
    private Chapter chapter;            //章

    private StageSelectManager stageSelectManager;

    //初期化メソッド
    public void Init(Chapter chapter, StageSelectManager stageSelectManager)
    {

        this.chapter = chapter;
        buttonText.text = chapter.GetStringValue();

        this.stageSelectManager = stageSelectManager;
    }

    //制御クラスに章を渡して会話シーンに遷移する
    public void Onclick()
    {

        stageSelectManager.ChangeSceneToMap(this.chapter);
    }

    //選択 ステージ詳細をウィンドウに表示する
    public void OnSelect()
    {
        stageSelectManager.ChangeStageDetailWindowText(this.chapter);
    }
}

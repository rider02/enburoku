using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 200719 クラスチェンジボタン
/// </summary>
public class ClassChangeButton : MonoBehaviour
{
    //クラスチェンジ先
    [SerializeField] private Text classChangeDestinationText;

    private StatusManager statusManager;
    private ClassChangeManager classChangeManager;
    private ClassChangeDetailWindow classChangeDetailWindow;

    
    private Job job;            //職業 補正値表示の為持たせておく
    private string unitName;    //クラスチェンジ対象キャラ
    private bool isDisable;     //不活性だが選択出来る状態
    private string message;


    //初期化メソッド
    public void Init(Job job , Unit unit, ClassChangeManager classChangeManager,
        StatusManager statusManager, ClassChangeDetailWindow classChangeDetailWindow)
    {

        this.job = job;
        this.unitName = unit.name;
        this.classChangeManager = classChangeManager;
        this.statusManager = statusManager;
        this.classChangeDetailWindow = classChangeDetailWindow;

        //ボタンの文字を設定
        classChangeDestinationText.text = job.jobName.ToString();
    }

    //クラスチェンジ先が存在しない場合
    public void DisableInit()
    {
        classChangeDestinationText.text = "既に最上級職です。";
        
        classChangeDestinationText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
        this.gameObject.GetComponent<Button>().interactable = false;

    }

    // レベルが足りていない時に呼び出し
    // 文字を灰色にして無効フラグを立てる 選択は出来るがエラーメッセージを表示
    public void setDisable(string message)
    {
        this.message = message;
        classChangeDestinationText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
        isDisable = true;
    }

    public void Onclick()
    {

        //レベル不足でボタンが無効化されていたら
        if (isDisable)
        {
            statusManager.OpenMessageWindow(message);
            return;
        }

        //最上級職の場合は押しても何も起こらない
        if (job == null)
        {
            return;
        }

        //クラスチェンジ実行
        classChangeManager.ClassChange(unitName , job);
    }

    //選択された時に職業の詳細を表示する
    public void OnSelect()
    {


        //既に転職先が無ければ更新は行わない
        if (job == null)
        {
            return;
        }

        classChangeDetailWindow.UpdateText(job);
    }

}

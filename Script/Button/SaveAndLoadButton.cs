using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// セーブとロードの機能を持つボタン
/// </summary>
public class SaveAndLoadButton : MonoBehaviour
{
    //名前
    [SerializeField]
    public Text nameText;

    //プレー時間
    [SerializeField]
    public Text timeText;

    //難易度
    [SerializeField]
    public Text difficultyText;

    //モード
    [SerializeField]
    public Text modeText;

    //章
    [SerializeField]
    public Text chapterText;

    //数値
    [SerializeField]
    public Text chapterNamText;

    //セーブデータが存在する場合は上記テキスト群を表示する
    [SerializeField]
    public GameObject SaveDataExistView;

    [SerializeField]
    public GameObject SaveDataEmptyView;

    private SaveAndLoadManager saveAndLoadManager;

    private int id;

    //初期化
    public void Init(SaveAndLoadManager saveAndLoadManager, int id)
    {
        this.saveAndLoadManager = saveAndLoadManager;
        this.id = id;

    }

    //クリック時の処理
    public void OnClick()
    {
        //210205 効果音再生
        AudioSource audioSource = GameObject.Find("BGMManager").GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = (Instantiate(Resources.Load("Prefabs/BGMManager")) as GameObject).GetComponent<AudioSource>();
        }
        audioSource.Play();

        //ボタンのモードがSAVEの時
        if (saveAndLoadManager.mode == FileControlMode.SAVE)
        {
            //セーブ実行
            SavePlayerData saveData = saveAndLoadManager.Save(id);
            
            //セーブした内容をボタンに反映する
            UpdateText(saveData);

        }
        else if (saveAndLoadManager.mode == FileControlMode.LOAD)
        {
            //モードがLOADの時
            saveAndLoadManager.Load(id);
        }
        else{
            //210512 データ消去の時
            saveAndLoadManager.Delete(id);
            UpdateText();
        }
    }

    //ボタンのテキストを更新する
    public void UpdateText(SavePlayerData saveData)
    {
        //ルート、難易度、モード
        nameText.text = saveData.route.GetStringValue();
        difficultyText.text = saveData.difficulty.GetStringValue();
        modeText.text = saveData.mode.GetStringValue();

        //章saveData.chapterでenumのIDを取得してString型に変換
        int chapterNam = (int)saveData.chapter;

        //210512 10以上の章はレミリアルート 章追加したら訂正すること
        if(chapterNam > 10){
            chapterNam = (int)saveData.chapter - 10;
        }
        chapterNamText.text = string.Format("第{0}章", chapterNam.ToString());
        //章の名前 (int)
        chapterText.text = saveData.chapter.GetStringValue();

        //時間
        timeText.text = saveData.hour.ToString() + ":" + saveData.minute.ToString("00");

        //テキスト群を表示させる
        SaveDataExistView.SetActive(true);

        //セーブデータが存在しない表記を消す
        SaveDataEmptyView.SetActive(false);

    }

    //ボタンのテキスト更新(ファイル削除時)
    public void UpdateText()
    {
        //テキスト群を非表示に
        SaveDataExistView.SetActive(false);

        //セーブデータが存在しない表記にする
        SaveDataEmptyView.SetActive(true);
    }
}

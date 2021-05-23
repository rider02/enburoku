using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

/// <summary>
/// 200808 ステージを選択する
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    //このクラスに選択したステージを持たせてScene遷移する
    public static Chapter selectedChapter;

    [SerializeField] FadeInOutManager fadeInOutManager;
    [SerializeField] GameObject stageWindow;
    [SerializeField] GameObject stageSelectDetailWindow;

    BGMPlayer bgmPlayer;

    //210208 シーンをまたがる効果音再生用
    AudioSource audioSource;

    StageDatabase stageDatabase;

    //210207 暗転中はメニューを表示しない
    private bool isInitFinish = false;


    private void Start()
    {

        //ボタン作成
        stageDatabase = Resources.Load<StageDatabase>("stageDatabase");
        List<Stage> stageList = new List<Stage>();
        List<Stage> tmpStageList = stageDatabase.stageList;

        //210514 キーコンフィグを初期化
        if (KeyConfigManager.configMap == null)
        {
            string configFilePath = Application.persistentDataPath + "/keyConfig";
            KeyConfigManager.InitKeyConfig(configFilePath);
        }

        //紅魔ルートと霊夢ルートを分ける
        if (ModeManager.route == Route.REIMU)
        {
            stageList = stageDatabase.stageList.FindAll(stage => stage.isReimuRoute == true);
        }
        else
        {
            stageList = stageDatabase.stageList.FindAll(stage => stage.isReimuRoute == false);
        }
        
        //210304 ゲーム進行度を反映
        foreach (Stage stage in stageList)
        {
            //chapterは数字で管理しているので、現在の進行度以下のステージを表示していく
            //210522 テストで全ステージを表示する場合はここをコメントアウト
            if(stage.chapter <= ChapterManager.chapter)
            {
                //Resources配下からボタンをロード
                var itemButton = (Instantiate(Resources.Load("Prefabs/StageButton")) as GameObject).transform;
                //ボタン初期化 今はテキストのみ
                itemButton.GetComponent<StageButton>().Init(stage.chapter, this);
                itemButton.name = itemButton.name.Replace("(Clone)", "");

                //partyWindowオブジェクト配下にprefab作成
                itemButton.transform.SetParent(stageWindow.transform);
            }
        }

        //210206 BGM再生
        GameObject bgmManager = GameObject.Find("BGMManager");
        if (bgmManager == null)
        {
            bgmManager = (Instantiate(Resources.Load("Prefabs/BGMManager")) as GameObject);
            bgmManager.name = bgmManager.name.Replace("(Clone)", "");

        }
        bgmPlayer = bgmManager.GetComponent<BGMPlayer>();

        if (BGMType.STATUS != bgmPlayer.playingBGM)
        {
            bgmPlayer.ChangeBGM(BGMType.STATUS);
            bgmPlayer.PlayBGM();
        }

        //効果音再生用
        audioSource = GameObject.Find("BGMManager").GetComponent<AudioSource>();

        //フェードイン開始
        fadeInOutManager.FadeinStart();
    }

    private void Update()
    {

        //210207 暗転中は操作しない
        if (fadeInOutManager.isFadeinFadeout())
        {
            return;
        }

        //初期化完了時のみメニューウィンドウ表示
        if (!isInitFinish)
        {
            stageWindow.SetActive(true);
            stageSelectDetailWindow.SetActive(true);
            //最初のボタンを選択
            EventSystem.current.SetSelectedGameObject(stageWindow.transform.Find("StageButton").gameObject);
            isInitFinish = true;
        }

        //210513 決定ボタンを押したらUGUIのボタンをクリック
        if (KeyConfigManager.GetKeyDown(KeyConfigType.SUBMIT))
        {
            KeyConfigManager.ButtonClick();
        }

        //キャンセルボタンを押すとステータス画面へ
        if (KeyConfigManager.GetKeyDown(KeyConfigType.CANCEL))
        {
            stageWindow.SetActive(false);
            stageSelectDetailWindow.SetActive(false);
            fadeInOutManager.ChangeScene("Status");
        }
    }

    //ボタンを選択した時、ステージ詳細ウィンドウのテキスト更新
    public void ChangeStageDetailWindowText(Chapter chapter)
    {

        //ボタンがChaptorを保持しているのでステージを取得
        Stage selectedStage = stageDatabase.stageList.FirstOrDefault(c => c.chapter == chapter);

        if (selectedStage == null)
        {
            Debug.Log($"ステージが存在しません {chapter.ToString()}");
            return;
        }

        stageSelectDetailWindow.GetComponent<StageSelectDetailWindow>().UpdateText(selectedStage);

    }

    //ステージボタンが押されたら戦闘前会話シーンへ
    public void ChangeSceneToMap(Chapter chapter)
    {
        //効果音再生
        if (audioSource != null)
        {
            audioSource.Play();
        }

        //210207 ウィンドウを非表示にして操作を防ぐ
        stageWindow.SetActive(false);
        stageSelectDetailWindow.SetActive(false);

        //static変数に設定
        selectedChapter = chapter;

        //STAGE1～10の名前で章前の会話を設定
        TalkManager.sceneName = chapter.ToString();

        //200827 戦闘前会話を表示するTalk
        fadeInOutManager.ChangeScene("Talk");
    }

    
}

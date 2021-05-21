using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrepareGameStartManager : MonoBehaviour
{
    [SerializeField] FadeInOutManager fadeInOutManager;

    [SerializeField] GameObject menuWindow;
    [SerializeField] GameObject routeView;
    [SerializeField] GameObject difficultyView;
    [SerializeField] GameObject modeView;

    [SerializeField] GameObject textPanel;
    [SerializeField] Text mainCharacterText;
    [SerializeField] Text attributeText;
    [SerializeField] Text messageText;
    [SerializeField] Image characterImage;
    [SerializeField] Text modeText;

    //210205 シーンをまたがる効果音再生用
    AudioSource audioSource;
    BGMPlayer bgmPlayer;

    bool isInitFinish;

    PrepareGameStartMode prepareGameStartMode;


    GameObject selectedRoute;
    GameObject selectedDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        GameObject bgmManager = GameObject.Find("BGMManager");
        if (bgmManager == null)
        {
            bgmManager = (Instantiate(Resources.Load("Prefabs/BGMManager")) as GameObject);
            bgmManager.name = bgmManager.name.Replace("(Clone)", "");

        }

        bgmPlayer = bgmManager.GetComponent<BGMPlayer>();

        //210206 BGM変更 既にステータス画面の曲が流れてる場合は再生しない
        if (BGMType.TITLE != bgmPlayer.playingBGM)
        {
            bgmPlayer.ChangeBGM(BGMType.TITLE);
            bgmPlayer.PlayBGM();
        }
        //効果音再生用
        audioSource = GameObject.Find("BGMManager").GetComponent<AudioSource>();

        //210514 キーコンフィグを初期化
        if (KeyConfigManager.configMap == null)
        {
            string configFilePath = Application.persistentDataPath + "/keyConfig";
            KeyConfigManager.InitKeyConfig(configFilePath);
        }

        //フェードイン
        fadeInOutManager.FadeinStart();

        //状態 まずはルート設定
        prepareGameStartMode = PrepareGameStartMode.ROUTE;
    }

    // Update is called once per frame
    void Update()
    {
        //210207 暗転中は操作しない
        if (fadeInOutManager.isFadeinFadeout())
        {
            return;
        }

        //初期化完了時のみメニューウィンドウ表示
        if (!isInitFinish)
        {
            menuWindow.SetActive(true);
            textPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(menuWindow.transform.Find("RouteView/ReimuButton").gameObject);
            isInitFinish = true;
        }

        //210513 決定ボタンを押したらUGUIのボタンをクリック
        if (KeyConfigManager.GetKeyDown(KeyConfigType.SUBMIT))
        {
            KeyConfigManager.ButtonClick();
        }

        //キャンセルボタンを押されるとタイトル画面へ遷移する
        if (KeyConfigManager.GetKeyDown(KeyConfigType.CANCEL))
        {
            //ルート選択の時にキャンセルボタンでタイトル
            if(prepareGameStartMode == PrepareGameStartMode.ROUTE)
            {
                fadeInOutManager.ChangeScene("title");
            }
            else if (prepareGameStartMode == PrepareGameStartMode.DIFFICULTY)
            {
                //難易度選択の時
                //ルート選択に戻る
                prepareGameStartMode = PrepareGameStartMode.ROUTE;

                difficultyView.SetActive(false);
                routeView.SetActive(true);

                modeText.text = "ルート選択";

                EventSystem.current.SetSelectedGameObject(selectedRoute);
            }
            else if (prepareGameStartMode == PrepareGameStartMode.MODE)
            {
                //モード選択の時
                //難易度選択に戻る
                prepareGameStartMode = PrepareGameStartMode.DIFFICULTY;

                modeText.text = "難易度設定";

                modeView.SetActive(false);
                difficultyView.SetActive(true);

                EventSystem.current.SetSelectedGameObject(selectedDifficulty);
            }
            
        }
    }

    //霊夢にカーソルが合わさった時
    public void OnSelectReimu()
    {
        mainCharacterText.text = "主人公\u00A0:\u00A0霊夢";
        attributeText.enabled = true;
        attributeText.text = "職業\u00A0:\u00A0博麗神社の巫女";
        messageText.text = "【対象\u00A0:\u00A0初心者】\u00A0何度でも戦闘と資金稼ぎが可能で、自由な育成を楽しめるルートです。";
        this.characterImage.sprite = Resources.Load<Sprite>("Image/Charactors/Reimu/normal");

    }

    //レミリアにカーソルが合わさった時
    public void OnSelectRemilia()
    {
        mainCharacterText.text = "主人公\u00A0:\u00A0レミリア";
        attributeText.enabled = true;
        attributeText.text = "職業\u00A0:\u00A0紅魔の吸血鬼";
        messageText.text = "【対象\u00A0:\u00A0中級者】\u00A0戦闘回数が限られており、歯ごたえの有る難易度を楽しめるルートです。";
        this.characterImage.sprite = Resources.Load<Sprite>("Image/Charactors/Remilia/normal");
    }

    //難易度選択でフォーカスをした時のメソッド
    public void OnSelectNormalMode()
    {
        mainCharacterText.text = "難易度\u00A0:\u00A0ノーマル";
        attributeText.enabled = false;

        messageText.text = "【対象\u00A0:\u00A0初心者・中級者】\n SRPGが初めての方にもお楽しみ頂ける難易度です。";
    }

    public void OnSelectHardMode()
    {
        mainCharacterText.text = "難易度\u00A0:\u00A0ハード";
        attributeText.enabled = false;

        messageText.text = "【対象\u00A0:\u00A0上級者】\n SRPGに自信が有る方向けの難易度です。";
    }

    //モード選択でフォーカスをした時のメソッド
    public void OnSelectLunaticMode()
    {
        mainCharacterText.text = "モード\u00A0:\u00A0ルナティック";
        attributeText.enabled = false;

        messageText.text = "【誰にも向かない】\n 最難関を求める方向けの難易度です。";
    }

    public void OnSelectCasualMode()
    {
        mainCharacterText.text = "敗北したユニットの扱い\u00A0:\u00A0カジュアル";
        attributeText.enabled = false;

        messageText.text = "【対象\u00A0:\u00A0初心者】\n 倒れた仲間は戦闘終了時に復活します。";
    }

    public void OnSelectClassicMode()
    {
        mainCharacterText.text = "敗北したユニットの扱い\u00A0:\u00A0クラシック";
        attributeText.enabled = false;

        messageText.text = "【対象\u00A0:\u00A0中級者・上級者】\n 失った仲間は、決して戻らない。";
    }

    public void OnSelectMediumMode()
    {
        mainCharacterText.text = "敗北したユニットの扱い\u00A0:\u00A0ミディアム";
        attributeText.enabled = false;

        messageText.text = "【対象\u00A0:\u00A0初心者・中級者】\u00A0 倒れた仲間は1回出撃不可になった後に復活します。";
    }

    /// <summary>
    /// 210220 難易度選択ボタンが押された時
    /// </summary>
    public void OnClickDifficulty(string difficultyStr)
    {
        //UnityのInspectorは列挙型が使えないので引数はString
        if("normal" == difficultyStr)
        {
            ModeManager.difficulty = Difficulty.NORMAL;
            selectedDifficulty = menuWindow.transform.Find("DifficultyView/NormalButton").gameObject;
        }
        else if ("hard" == difficultyStr)
        {
            ModeManager.difficulty = Difficulty.HARD;
            selectedDifficulty = menuWindow.transform.Find("DifficultyView/HardButton").gameObject;
        }
        else if ("lunatic" == difficultyStr)
        {
            ModeManager.difficulty = Difficulty.LUNATIC;
            selectedDifficulty = menuWindow.transform.Find("DifficultyView/LunaticButton").gameObject;
        }
        //難易度を設定
        
        Debug.Log($"難易度:{ModeManager.difficulty}に設定しました");

        //表示テキスト変更
        modeText.text = "モード設定";

        //UI制御
        difficultyView.SetActive(false);
        modeView.SetActive(true);

        //フォーカス設定
        EventSystem.current.SetSelectedGameObject(menuWindow.transform.Find("ModeView/CasualButton").gameObject);

        //モード選択へ
        prepareGameStartMode = PrepareGameStartMode.MODE;
    }

    /// <summary>
    /// 210221 モード設定ボタンが押された時
    /// </summary>
    /// <param name="mode"></param>
    public void OnClickMode(string modeStr)
    {
        if ("classic" == modeStr)
        {
            ModeManager.mode = Mode.CLASSIC;
        }
        else if ("casual" == modeStr)
        {
            ModeManager.mode = Mode.CASUAL;
        }
        
        else if ("medium" == modeStr)
        {
            ModeManager.mode = Mode.MEDIUM;
        }

        Debug.Log($"モード:{ModeManager.mode}に設定しました");

        //設定が完了したのでステータス画面へ
        ChangeSceneToStatus();
    }

    public void OnClickReimu()
    {
        ModeManager.route = Route.REIMU;
        ChapterManager.chapter = Chapter.STAGE1;

        selectedRoute = menuWindow.transform.Find("RouteView/ReimuButton").gameObject;
        ChangeModeToDifficulty();
    }

    public void OnClickRemilia()
    {
        ModeManager.route = Route.REMILLIA;
        ChapterManager.chapter = Chapter.R_STAGE1;
        selectedRoute = menuWindow.transform.Find("RouteView/RemilliaButton").gameObject;
        ChangeModeToDifficulty();
    }

    //ルート確定時、モードを難易度設定に変更
    private void ChangeModeToDifficulty()
    {
        prepareGameStartMode = PrepareGameStartMode.DIFFICULTY;

        //テキスト更新
        modeText.text = "難易度設定";

        //UI表示切替
        routeView.SetActive(false);
        difficultyView.SetActive(true);

        //フォーカス設定
        EventSystem.current.SetSelectedGameObject(menuWindow.transform.Find("DifficultyView/NormalButton").gameObject);
    }

    

    /// <summary>
    /// 完了してウィンドウを消し、ステータス画面に遷移する
    /// </summary>
    public void ChangeSceneToStatus()
    {
        //210205 効果音再生とBGM停止
        audioSource.Play();
        StopBGM();

        ChapterManager.isChapterInit = true;

        //UIの表示を消す
        menuWindow.SetActive(false);
        textPanel.SetActive(false);
        fadeInOutManager.ChangeScene("Status");
    }

    //210205 BGMが流れていたら停止させる
    private void StopBGM()
    {
        if (bgmPlayer != null)
        {
            bgmPlayer.StopBGM();
        }
    }

    /// <summary>
    /// 210220 ゲーム設定画面の状態 絶対にここでしか使わないしここに設定してしまう
    /// </summary>
    enum PrepareGameStartMode
    {
        //ルート選択
        ROUTE,

        //難易度選択
        DIFFICULTY,

        //モード選択
        MODE
    }
}

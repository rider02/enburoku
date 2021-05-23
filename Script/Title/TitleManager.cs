using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//210204 タイトル画面制御
public class TitleManager : MonoBehaviour
{

    [SerializeField] GameObject pressAnyButtonText;
    [SerializeField] GameObject menuWindow;
    [SerializeField] FadeInOutManager fadeInOutManager;
    [SerializeField] SaveAndLoadManager saveAndLoadManager;
    
    [SerializeField] GameObject loadWindow;         //セーブデータのロード、削除ウィンドウ
    [SerializeField] GameObject keyConfigWindow;    //キーコンフィグ
    [SerializeField] GameObject KeyAssignWindow;    //キー割り当て

    //シーンをまたがる効果音再生用
    AudioSource audioSource;

    private KeyConfigManager keyConfigManager;

    private TitleMode mode = TitleMode.TITLE;

    //BGMプレイヤー
    BGMPlayer bgmPlayer;

    void Start()
    {
        GameObject bgmManager = GameObject.Find("BGMManager");
        if (bgmManager == null)
        {
            bgmManager = (Instantiate(Resources.Load("Prefabs/BGMManager")) as GameObject);
            bgmManager.name = bgmManager.name.Replace("(Clone)", "");

        }
        bgmPlayer = bgmManager.GetComponent<BGMPlayer>();

        //BGM再生
        if (BGMType.TITLE != bgmPlayer.playingBGM)
        {
            bgmPlayer.ChangeBGM(BGMType.TITLE);
            bgmPlayer.PlayBGM();
        }
        
        fadeInOutManager.FadeinStart();

        //複数シーンで存在するので、取得しておく
        audioSource = GameObject.Find("BGMManager").GetComponent<AudioSource>();

        //ロード、データ消去機能の初期化
        saveAndLoadManager.Init(fadeInOutManager);

        //続きからボタンを作成
        saveAndLoadManager.createSaveAndLoadButton();

        //210513 ファイルからキーコンフィグ初期化
        string configFilePath = Application.persistentDataPath + "/keyConfig";
        keyConfigManager = new KeyConfigManager(this, configFilePath);

        //キーコンフィグのUI初期化
        keyConfigManager.CreateConfigButtonList(keyConfigWindow);
        
    }

    // Update is called once per frame
    void Update()
    {
        //210207 暗転中は操作しない
        if (fadeInOutManager.isFadeinFadeout())
        {
            return;
        }

        //まだメニューが表示されていない時
        if (mode == TitleMode.TITLE)
        {

            //Start、決定、キャンセルボタンのいずれかが押されたら「PRESS ANY BUTTON」文字を非表示にしてメニュー表示
            if (KeyConfigManager.GetKeyDownAny() || Input.GetButtonDown("Submit"))
            {
                pressAnyButtonText.SetActive(false);
                menuWindow.SetActive(true);
                SetTitleMode(TitleMode.MENU);

                EventSystem.current.SetSelectedGameObject(menuWindow.transform.Find("StartButton").gameObject);
            }
        }
        else if(mode == TitleMode.KEY_ASSIGN_RECEIPT)
        {
            //キーコンフィグのアサイン受付中の場合
            keyConfigManager.KeyAssign();
        }
        else
        {
            //メニュー画面表示後

            //210513 キーコンフィグした決定ボタンを押したらUGUIのボタンをクリックする処理
            if (KeyConfigManager.GetKeyDown(KeyConfigType.SUBMIT))
            {
                KeyConfigManager.ButtonClick();
            }

            //ロード画面、キーコンフィグ表示中にキャンセルボタンを押すとタイトルメニューに戻る
            if (KeyConfigManager.GetKeyDown(KeyConfigType.CANCEL) || Input.GetButtonDown("Cancel"))
            {
                if(mode == TitleMode.LOAD || mode == TitleMode.DELETE)
                {
                    CloseLoadWindow();
                }
                else if (mode == TitleMode.KEY_CONFIG)
                {
                    CloseKeyConfigWindow();
                }

                SetTitleMode(TitleMode.MENU);
            }
        }
    }
    //ロードウィンドウを非表示にしてメニュー再表示
    private void CloseLoadWindow()
    {
        //ロードウィンドウ非表示、メニューウィンドウ表示
        loadWindow.SetActive(false);
        CommonCloseWindow();
    }

    //キーコンフィグを終了して、設定内容を保存
    private void CloseKeyConfigWindow()
    {
        //キーコンフィグ非表示、メニューウィンドウ表示
        keyConfigWindow.SetActive(false);

        //閉じる時にキーコンフィグ設定を保存
        string configFilePath = Application.persistentDataPath + "/keyConfig";
        keyConfigManager.SaveKeyConfig(configFilePath);

        CommonCloseWindow();
    }

    //ロード、コンフィグでウィンドウを閉じる時の共通処理
    private void CommonCloseWindow()
    {
        SetTitleMode(TitleMode.MENU);

        menuWindow.SetActive(true);
        //フォーカス変更
        EventSystem.current.SetSelectedGameObject(menuWindow.transform.Find("StartButton").gameObject);
    }

    //「続きから」ボタンクリック時 ロードウィンドウを表示する
    public void OpenLoadWindow()
    {
        //ボタンを押した時の処理をロードに変更
        saveAndLoadManager.mode = FileControlMode.LOAD;
        SetTitleMode(TitleMode.LOAD);

        CommonOpenWindow();
    }

    //「データ消去」ボタンクリック時
    public void OpenDeleteWindow()
    {
        //ボタンを押した時の処理をロードに変更
        saveAndLoadManager.mode = FileControlMode.DELETE;
        SetTitleMode(TitleMode.DELETE);

        CommonOpenWindow();
    }

    //ウィンドウ表示時の共通処理
    private void CommonOpenWindow()
    {
        //ウィンドウ表示、メニューは非表示
        loadWindow.SetActive(true);
        menuWindow.SetActive(false);

        EventSystem.current.SetSelectedGameObject(loadWindow.transform.Find("SaveAndLoadButton").gameObject);

    }

    //210513 キーコンフィグを開く
    public void OpenKeyConfigWindow()
    {
        //キーコンフィグ表示、メニューは非表示
        keyConfigWindow.SetActive(true);
        menuWindow.SetActive(false);

        EventSystem.current.SetSelectedGameObject(keyConfigWindow.transform.Find("ConfigButton0").gameObject);

        SetTitleMode(TitleMode.KEY_CONFIG);
    }

    //キー入力受付モードにする
    public void KeyAssignReceipt()
    {
        SetTitleMode(TitleMode.KEY_ASSIGN_RECEIPT);
        KeyAssignWindow.SetActive(true);
    }

    //キーの割り当て終了
    public void KeyAssignFinish(GameObject selectedButton)
    {
        //キー割り当てウィンドウを非表示にする
        KeyAssignWindow.SetActive(false);

        //モードを戻す
        SetTitleMode(TitleMode.KEY_CONFIG);

        //uGUIのフォーカスを戻す
        EventSystem.current.SetSelectedGameObject(selectedButton);

    }

    //タイトル画面のモードを設定する
    private void SetTitleMode(TitleMode destMode)
    {
        this.mode = destMode;
        Debug.Log($"mode : {mode.GetStringValue()}");
    }


    //「初めから」ボタンクリック時等 シーン変更
    private void ChangeScene(string scene)
    {
        menuWindow.SetActive(false);
        loadWindow.SetActive(false);

        //効果音再生
        audioSource.Play();

        //シーン変更
        fadeInOutManager.ChangeScene(scene);
    }

    //「終了ボタン」クリック時 ゲーム終了
    public void Quit()
    {
        //効果音再生
        audioSource.Play();

        fadeInOutManager.QuitFadeOut();
    }

}

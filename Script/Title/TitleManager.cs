using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//210204 タイトル画面制御
public class TitleManager : MonoBehaviour
{

    [SerializeField] BGMPlayer bgmPlayer;
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

    

    void Start()
    {
        //BGM再生
        bgmPlayer.ChangeBGM(BGMType.TITLE);
        bgmPlayer.PlayBGM();
        fadeInOutManager.FadeinStart();

        //複数シーンで存在するので、取得しておく
        audioSource = GameObject.Find("BGMManager").GetComponent<AudioSource>();

        //ロード、データ消去機能の初期化
        saveAndLoadManager.Init(fadeInOutManager);

        //続きからボタンを作成
        saveAndLoadManager.createSaveAndLoadButton();

        //210513 キーコンフィグ初期化
        string configFilePath = Application.persistentDataPath + "/keyConfig";
        keyConfigManager = new KeyConfigManager(this, configFilePath);

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

            //Start、決定、キャンセルボタンのいずれかが押されたら
            if (KeyConfigManager.GetKeyDownAny() || Input.GetButtonDown("Submit"))
            {
                //文字を非表示にしてメニュー表示
                pressAnyButtonText.SetActive(false);
                menuWindow.SetActive(true);
                mode = TitleMode.MENU;
                Debug.Log($"mode : {mode.GetStringValue()}");

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

            //210513 決定ボタンを押したらUGUIのボタンをクリック
            if (KeyConfigManager.GetKeyDown(KeyConfigType.SUBMIT))
            {
                KeyConfigManager.ButtonClick();
            }

            //ロード画面、キーコンフィグ表示中二キャンセルボタンを押すとタイトルメニューに戻る
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

                mode = TitleMode.MENU;
                Debug.Log($"mode : {mode.GetStringValue()}");
            }
        }
    }
    //ロードウィンドウを非表示にしてメニュー再表示
    private void CloseLoadWindow()
    {
        //ロードウィンドウ非表示、メニューウィンドウ表示
        loadWindow.SetActive(false);
        menuWindow.SetActive(true);

        CommonCloseWindow();
    }

    //キーコンフィグを終了して、設定内容を保存
    private void CloseKeyConfigWindow()
    {
        //キーコンフィグ非表示、メニューウィンドウ表示
        keyConfigWindow.SetActive(false);
        menuWindow.SetActive(true);

        //閉じる時にキーコンフィグ設定を保存
        string configFilePath = Application.persistentDataPath + "/keyConfig";
        keyConfigManager.SaveKeyConfig(configFilePath);

        CommonCloseWindow();
    }

    //ウィンドウを閉じる時の共通処理
    private void CommonCloseWindow()
    {
        mode = TitleMode.MENU;
        Debug.Log($"mode : {mode.GetStringValue()}");

        //フォーカス変更
        EventSystem.current.SetSelectedGameObject(menuWindow.transform.Find("StartButton").gameObject);
    }

    //「続きから」ボタンクリック時 ロードウィンドウを表示する
    public void OpenLoadWindow()
    {
        //ボタンを押した時の処理をロードに変更
        saveAndLoadManager.mode = FileControlMode.LOAD;
        mode = TitleMode.LOAD;
        Debug.Log($"mode : {mode.GetStringValue()}");

        CommonOpenWindow();
    }

    //「データ消去」ボタンクリック時
    public void OpenDeleteWindow()
    {
        //ボタンを押した時の処理をロードに変更
        saveAndLoadManager.mode = FileControlMode.DELETE;
        mode = TitleMode.DELETE;
        Debug.Log($"mode : {mode.GetStringValue()}");

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

        mode = TitleMode.KEY_CONFIG;
        Debug.Log($"mode : {mode.GetStringValue()}");
        
    }

    //キー入力受付モードにする
    public void KeyAssignReceipt()
    {
        mode = TitleMode.KEY_ASSIGN_RECEIPT;
        KeyAssignWindow.SetActive(true);
        Debug.Log($"mode : {mode.GetStringValue()}");
    }

    //キーの割り当て終了
    public void KeyAssignFinish(GameObject selectedButton)
    {
        //キー割り当てウィンドウを非表示にする
        KeyAssignWindow.SetActive(false);

        //モードを戻す
        mode = TitleMode.KEY_CONFIG;
        Debug.Log($"mode : {mode.GetStringValue()}");

        //uGUIのフォーカスを戻す
        EventSystem.current.SetSelectedGameObject(selectedButton);

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

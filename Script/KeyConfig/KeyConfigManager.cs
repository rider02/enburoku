using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

/// <summary>
/// 210513 キーコンフィグのファイル入出力、入力受付を行うクラス
/// </summary>
public class KeyConfigManager : MonoBehaviour
{
    private TitleManager titleManager;

    //Key : 機能名(決定、キャンセル等) Value : アサインしたゲームパッドのボタン
    public static Dictionary<KeyConfigType, KeyCode> configMap;

    //現在割り当てを行っている機能
    private KeyConfigType assignKeyConfigType;

    //割り当て処理が終わった時にフォーカスを戻すボタン
    private GameObject selectedButton;

    //初期化(タイトル画面のみ)
    public KeyConfigManager(TitleManager titleManager, string configFilePath)
    {
        configMap = new Dictionary<KeyConfigType, KeyCode>();
        this.titleManager = titleManager;

        //キーコンフィグファイルを読み込み
        LoadKeyConfig(configFilePath);
    }

    //キーコンフィグのロード(開発でタイトル画面以外から開始した時、本番は呼ばれない)
    public static void InitKeyConfig(string configFilePath)
    {
        configMap = new Dictionary<KeyConfigType, KeyCode>();

        //キーコンフィグファイルを読み込み
        LoadKeyConfig(configFilePath);
    }

    /// <summary>
    /// セーブ、ロード関連
    /// </summary>

    //キーコンフィグファイル取得 起動時等に実行
    private static void LoadKeyConfig(string configFilePath)
    {
        //存在すればロード実行
        if (File.Exists(configFilePath))
        {
            // 指定したパスのファイルストリームを開く
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(configFilePath, FileMode.Open);
            try
            {
                // 指定したファイルストリームをデシリアライズして取得
                configMap = (Dictionary<KeyConfigType, KeyCode>)bf.Deserialize(file);
                Debug.Log("キーコンフィグをロードしました");

            }
            finally
            {
                // ファイルの破棄
                if (file != null)
                    file.Close();
            }
        }
        else
        {
            //初回起動時等、キーコンフィグファイルが存在しない場合はデフォルト作成
            Debug.Log($"WARN : キーコンフィグファイルが存在しません path : {configFilePath}");
            InitKeyConfig(configFilePath);
        }
    }

    //キーコンフィグ保存 コンフィグUIを閉じる時のみ呼ばれる
    public void SaveKeyConfig(string configFilePath)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(configFilePath);

        try
        {
            // 指定したオブジェクトを上で作成したストリームにシリアル化する
            bf.Serialize(file, configMap);
        }
        finally
        {
            // ファイルの破棄
            if (file != null)
                file.Close();
        }
        Debug.Log("キーコンフィグを保存しました");
    }

    /// <summary>
    /// キー入力取得関連
    /// </summary>

    //キーの入力を確認する
    private static bool InputKeyCheck(KeyConfigType keyConfigType, Func<KeyCode, bool> predicate)
    {
        if (!configMap.ContainsKey(keyConfigType))
        {
            Debug.Log($"Error キーコンフィグDictionaryにキーが存在しません key : {keyConfigType.ToString()}");
            return false;
        }

        //key(機能)からValueのkeyCodeを取得
        KeyCode keyCode = configMap[keyConfigType];

        if (predicate(keyCode))
        {
            //ログが沢山出るのでコメントアウト
            //Debug.Log($"キーが押されました key : {keyConfigType.ToString()}");
            return true;
        }
                
        return false;
    }

    //引数の機能(決定、キャンセル等)にアサインされているキーが押下状態かを返す
    public static bool GetKey(KeyConfigType keyConfigType)
    {
        return InputKeyCheck(keyConfigType, Input.GetKey);
    }

    //引数の機能(決定、キャンセル等)にアサインされているキーが入力されたかを返す
    public static bool GetKeyDown(KeyConfigType keyConfigType)
    {
        return InputKeyCheck(keyConfigType, Input.GetKeyDown);
    }

    //引数の機能(決定、キャンセル等)にアサインされているキーが押下されていないかを返す
    public static bool GetKeyUp(KeyConfigType keyConfigType)
    {
        return InputKeyCheck(keyConfigType, Input.GetKeyUp);
    }

    //いずれかのボタンでもキーコンフィグにアサインされたキーが押されていればtrueを返す
    public static bool GetKeyDownAny()
    {
        foreach (KeyConfigType key in Enum.GetValues(typeof(KeyConfigType)))
        {
            if (GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }

    //UGUIのボタンをクリックをスクリプトから実行する処理
    public static void ButtonClick()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        if(obj == null)
        {
            Debug.Log("WARN : 選択中のGameObjectが有りません");
            return;
        }

        Button button = obj.GetComponent<Button>();
        if(button == null)
        {
            Debug.Log("WARN : 選択中のGameObjectはボタンではありません");
            return;
        }

        //ボタンクリック実行
        button.onClick.Invoke();
    }

    /// <summary>
    /// キー割り当て関連
    /// </summary>
    
    //ボタンを押したキーの割り当て入力受付開始
    public void KeyAssignReceipt(KeyConfigType keyConfigType)
    {
        //キーの割り当てを行っているボタンを取得
        assignKeyConfigType = keyConfigType;

        //フォーカスを戻すボタンを控えておく
        selectedButton = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);

        //モード変更
        titleManager.KeyAssignReceipt();
    }

    //Updateで実行され、キーが入力されたらそのキーを処理に割り当てる
    public void KeyAssign()
    {
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel") ||
            Input.GetButtonDown("Menu") || Input.GetButtonDown("Zoom") || Input.GetButtonDown("Speed"))
        {
            Debug.Log("既にInputManagerにアサインされているボタンは割り当て出来ません");
            return;
        }

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            Debug.Log("InputManagerにアサインされている上下左右ボタンは割り当て出来ません");
            return;
        }

        //何かキーが押されたら
        if (Input.anyKeyDown)
        {
            //全てのキーコードと付き合わせて、入力されたキーコードを特定する
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    //処理を書く
                    Debug.Log($"入力されたキーを機能にアサイン key : {code.ToString()}");

                    //アサイン上書き
                    configMap[assignKeyConfigType] = code;
                    //ボタンのテキスト更新
                    selectedButton.GetComponent<KeyConfigButton>().UpdateText(assignKeyConfigType, code);

                    break;
                }
            }

            //モードを戻して、キー割り当てウィンドウを非表示にする
            titleManager.KeyAssignFinish(selectedButton);
        }
    }

    /// <summary>
    /// 初期化関連
    /// </summary>

    //初回起動などでキーコンフィグファイルが無い場合、デフォルト作成
    private void InitKeyConfig()
    {
        Debug.Log("キーコンフィグファイルが無い為、デフォルト値を作成");

        //決定
        configMap.Add(KeyConfigType.SUBMIT, KeyCode.JoystickButton1);

        //キャンセル
        configMap.Add(KeyConfigType.CANCEL, KeyCode.JoystickButton2);

        //スタート
        configMap.Add(KeyConfigType.START, KeyCode.JoystickButton9);

        //メニュー(ステータス表示等)
        configMap.Add(KeyConfigType.MENU, KeyCode.JoystickButton0);

        //カーソル速度変更
        configMap.Add(KeyConfigType.SPEED, KeyCode.JoystickButton3);

        //カメラのズーム
        configMap.Add(KeyConfigType.ZOOM, KeyCode.JoystickButton7);
    }

    //タイトル画面起動時にキーコンフィグ設定UIのボタン一覧を作成する
    public void CreateConfigButtonList(GameObject keyConfigWindow)
    {
        int index = 0;

        //設定項目の数ボタンを生成
        foreach (KeyValuePair<KeyConfigType, KeyCode> config in configMap)
        {
            //ボタン作成と初期化
            var configButton = (Instantiate(Resources.Load("Prefabs/ConfigButton")) as GameObject).transform;
            configButton.GetComponent<KeyConfigButton>().Init(this, config.Key, config.Value);
            configButton.name = configButton.name.Replace("(Clone)", "");
            configButton.name += index;

            //keyConfigWindow配下にprefab作成
            configButton.transform.SetParent(keyConfigWindow.transform, false);
            
            index++;
        }

    }
}

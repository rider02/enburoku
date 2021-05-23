using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// 200711 セーブ、ロード、データ削除を行うクラス
/// タイトル画面、ステータス画面で使用する
/// </summary>
public class SaveAndLoadManager : MonoBehaviour
{
    //セーブ、ロード、削除するデータを表示するUI
    [SerializeField] private GameObject saveAndLoadWindow;

    FadeInOutManager fadeInOutManager;

    //ファイル名
    private string filename { get; set; }

    //ファイルのパス
    private string saveFilePath;

    //初期化メソッド
    public void Init(FadeInOutManager fadeInOutManager)
    {
        this.fadeInOutManager = fadeInOutManager;
    }

    //ボタンから参照して処理を切り替える
    public FileControlMode mode { get; set; }

    /// <summary>
    /// セーブボタン、ロードボタンを作る
    /// </summary>
    public void createSaveAndLoadButton()
    {
        //ボタン4つ作成
        for(int id = 1; id < 5; id++)
        {
            //Resources配下からボタンをロード
            var saveAndLoadButton = (Instantiate(Resources.Load("Prefabs/SaveAndLoadButton")) as GameObject).transform;

            //ボタン初期化
            saveAndLoadButton.GetComponent<SaveAndLoadButton>().Init(this, id);
            saveAndLoadButton.name = saveAndLoadButton.name.Replace("(Clone)", "");

            //Windowオブジェクト配下にprefab作成
            saveAndLoadButton.transform.SetParent(saveAndLoadWindow.transform,false);

            //ここでボタン表示用にセーブした内容を取得
            saveAndLoadButton = LoadButtonData(id , saveAndLoadButton);

        }
    }

    //セーブボタンから呼ばれ、セーブを行う
    public SavePlayerData Save(int buttonId)
    {
        //現在の状況をセーブデータにする
        SavePlayerData saveData = CreateSavePlayerData();

        //ボタンのIdによってファイル名を変える
        filename = buttonId + ".save";
        saveFilePath = Application.persistentDataPath + "/" + filename;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);

        try
        {
            // 指定したオブジェクトを上で作成したストリームにシリアル化する
            bf.Serialize(file, saveData);
        }
        finally
        {
            // ファイルの破棄
            if (file != null)
                file.Close();
        }
        Debug.Log("セーブ実行");
        return saveData;

    }

    //ロードを行う
    public void Load(int buttonId)
    {
        //押したボタンのIDからファイル名取得
        filename = buttonId + ".save";
        saveFilePath = Application.persistentDataPath + "/" + filename;

        if (File.Exists(saveFilePath))
        {
            // バイナリ形式でデシリアライズ
            BinaryFormatter bf = new BinaryFormatter();
            // 指定したパスのファイルストリームを開く
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            try
            {
                // 指定したファイルストリームをオブジェクトにデシリアライズ
                SavePlayerData saveData = (SavePlayerData)bf.Deserialize(file);
                
                //読み込んだデータを各プレイヤーデータに反映
                //ユニットの状態
                UnitController.unitList = saveData.unitList;
                UnitController.isInit = true;

                //お金
                CashManager.cash = saveData.cash;
                CashManager.isCashInit = true;

                //210303 空けた宝箱
                AcquiredTreasureManager.treasureList = saveData.treasureList;
                AcquiredTreasureManager.isInit = true;

                //進行度
                ChapterManager.chapter = saveData.chapter;
                ChapterManager.isChapterInit = true;

                //時間
                PlayTimeManager.hour = saveData.hour;
                PlayTimeManager.minute = saveData.minute;

                //ルート、難易度、モード
                ModeManager.route = saveData.route;
                ModeManager.mode = saveData.mode;
                ModeManager.difficulty = saveData.difficulty;
                ModeManager.isModeInit = true;

                //TODO 倉庫に入っている持ち物の復元を追加する

                //210207 ウィンドウを非表示にする
                saveAndLoadWindow.SetActive(false);

                Debug.Log("ロード成功");

                //ロードしたら自画面遷移
                fadeInOutManager.ChangeScene("Status");
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
            Debug.Log("ファイルが存在しません");
        }

    }

    //データ削除
    public void Delete(int buttonId)
    {
        Debug.Log("Delete");

        //押したボタンのIDからファイル名取得
        filename = buttonId + ".save";
        saveFilePath = Application.persistentDataPath + "/" + filename;

        //ファイルが存在すればファイル削除実行
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("削除成功");
        }
        else
        {
            Debug.Log("ファイルが存在しません");
        }
    }

    //200712 ボタン作成時に使用 ボタンのテキスト表示用データを読み込む
    private Transform LoadButtonData(int buttonId , Transform saveAndLoadButton)
    {
        filename = buttonId + ".save";
        saveFilePath = Application.persistentDataPath + "/" + filename;

        //指定パスにセーブデータが存在する場合
        if (File.Exists(saveFilePath))
        {
            // バイナリ形式でデシリアライズ
            BinaryFormatter bf = new BinaryFormatter();
            // 指定したパスのファイルストリームを開く
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            try
            {
                // 指定したファイルストリームをオブジェクトにデシリアライズ
                SavePlayerData saveData = (SavePlayerData)bf.Deserialize(file);
                //読み込んだデータを元にボタンの表示内容を更新して表示
                saveAndLoadButton.GetComponent<SaveAndLoadButton>().UpdateText(saveData);
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
            //セーブデータが存在しない場合は空ボタンの表示をする
            saveAndLoadButton.GetComponent<SaveAndLoadButton>().SaveDataEmptyView.SetActive(true);
        }

        return saveAndLoadButton;
    }

    //現在の状況をセーブデータにする。プレー時間、お金、キャラの状態、進行度
    private SavePlayerData CreateSavePlayerData()
    {
        SavePlayerData savePlayerData = new SavePlayerData();

        //仲間キャラの状態
        savePlayerData.unitList = UnitController.unitList;

        //お金
        savePlayerData.cash = CashManager.cash;

        //210303 明けた宝箱
        savePlayerData.treasureList = AcquiredTreasureManager.treasureList;

        //進行度
        savePlayerData.chapter = ChapterManager.chapter;

        //時間
        savePlayerData.hour = PlayTimeManager.hour;
        savePlayerData.minute = PlayTimeManager.minute;

        //ルート、難易度、モード
        savePlayerData.route = ModeManager.route;
        savePlayerData.difficulty = ModeManager.difficulty;
        savePlayerData.mode = ModeManager.mode;

        return savePlayerData;
    }
}

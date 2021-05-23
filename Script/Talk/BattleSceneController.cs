using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// パーサー(SceneReader)から呼ばれ、各種ノベルゲームの機能を実行するクラス(戦闘マップ用)
/// 初期かはBattleTalkManagerから行われる
/// </summary>
public class BattleSceneController : MonoBehaviour
{
    private BattleTalkManager battleTalkManager;

    private GUIManager guiManager;
    private SceneHolder sceneHolder;
    private BattleSceneReader battleSceneReader;    //戦闘マップ用
    GameObject talkView;

    BackGround background;

    private string tempText;
    private bool isEnd;

    public DestinationScene sceneChangeDestination;

    //画面がフェードアウト中か
    private bool isFade;

    private Sequence textSeq = DOTween.Sequence();
    private Sequence imageSeq = DOTween.Sequence();
    private bool isOptionsShowed;
    //文字を表示していくスピード SetTextメソッド参照
    private float messageSpeed = 0.04f;

    public Scene currentScene;
    public List<BattleCharacter> Characters = new List<BattleCharacter>();

    FadeInOutManager fadeInOutManager;


    //コンストラクタ(戦闘マップ用) Background(背景)が存在しない
    public BattleSceneController(BattleTalkManager battleTalkManager, GUIManager guiManager, FadeInOutManager fadeInOutManager, GameObject talkView)
    {
        this.battleTalkManager = battleTalkManager;

        //GUIManagerインスタンス作成 ウィンドウのテキスト表示などを行うクラス
        this.guiManager = guiManager;
        this.fadeInOutManager = fadeInOutManager;
        this.talkView = talkView;

        string scenarioPath = "ScenarioData/battleTalkText";
        sceneHolder = new SceneHolder(scenarioPath);
        battleSceneReader = new BattleSceneReader(this, battleTalkManager);

        //DoTween
        textSeq.Complete();
    }

    /// <summary>
    /// ボタン押した時の処理 GameControllerのUpdateメソッドから呼ばれて監視してる
    /// </summary>
    public void WaitClick()
    {

        //終了フラグが立っていたら画面遷移　スタートボタンを押した場合はスキップ
        if (isEnd || KeyConfigManager.GetKeyDown(KeyConfigType.START))
        {
            CharactorClear();
            battleTalkManager.TalkEnd();
        }

        //シーンが無ければ何もしない
        if (currentScene != null)
        {
            //ボタンが押されたら
            if (KeyConfigManager.GetKeyDown(KeyConfigType.SUBMIT))
            {
                //選択肢が出ておらず、画像表示中でなければ次の処理へ
                if (!isOptionsShowed && !imageSeq.IsPlaying())
                {
                    SetNextProcess();
                }
            }
        }
    }


    /// <summary>
    /// フラグを見てGUIを出したり消したりする処理
    /// </summary>
    public void SetComponents()
    {
        //フラグが立っていれば選択肢パネル表示

        if(guiManager.ButtonPanel != null)
        {
            guiManager.ButtonPanel.gameObject.SetActive(isOptionsShowed);
        }
        
        //このデルタはクリックを促すUIのこと
        guiManager.Delta.gameObject.SetActive
            (!textSeq.IsPlaying() && !isOptionsShowed && !imageSeq.IsPlaying());
    }


    /// <summary>
    /// クリックされた時に呼ばれる
    /// 1行ずつSceneのテキストを取り出して表示やコマンドを実行する
    /// 初期化の時とマウスクリック時に呼び出し
    /// </summary>
    public void SetNextProcess()
    {

        //DoTWEEN テキスト表示中なら引き続き文字を表示
        if (textSeq.IsPlaying())
        {
            //tempTextはどこから設定されているかというと、
            //SceneReaderからSetTextを呼ばれてセットされている
            //tempText
            SetText(tempText);
        }
        else
        {
            //既に文字が表示され終わっていたらパーサーで次の行を取得する
            battleSceneReader.ReadLines(currentScene);
        }
    }

    //シーンをセットする
    public void SetScene(string id)
    {
        //現在のシーンをシーン一覧から取得してセット
        currentScene = sceneHolder.Scenes.Find(s => s.ID == id);

        //もし存在しないシーンを指定されたらエラー
        if (currentScene == null)
        {
            Debug.LogError("scenario not found");
            return;
        }
        //インスタンスを作り直し
        currentScene = currentScene.Clone();
        
    }

    //引数のシーンが存在するか確認する
    public bool CheckSceneExist(string sceneName)
    {
        Scene scene = sceneHolder.Scenes.Find(s => s.ID == sceneName);
        bool sceneExist = (scene != null) ? true : false;
        return sceneExist;
    }

    //以下、SceneReaderからテキストのパース結果を元に呼ばれる
    //通常のテキストをtempTextに設定する
    public void SetText(string text)
    {
        //ここでやっと読み込んだSceneのテキストを表示用に設定
        tempText = text;

        //文字を表示中であれば全て表示完了させる
        if (textSeq.IsPlaying())
        {
            textSeq.Complete();
        }
        else
        {
            //文字を表示済みなら文字を初期化
            guiManager.Text.text = "";

            //少しずつ文字を表示
            //第一引数：表示テキスト、第二引数:テキストを表示する秒数
            //長さが変わっても文字ごとの表示速度は一定にする
            textSeq = DOTween.Sequence();
            textSeq.Append
                (guiManager.Text.DOText(text, text.Length * messageSpeed)
                .SetEase(Ease.Linear));
        }
    }

    //名前欄の設定と、喋っているキャラのハイライト
    //SceneReaderのパース中「#speaker」が有った時呼ばれる
    public void SetSpeaker(string name, string objname)
    {
        if (string.IsNullOrEmpty(name))
        {
            guiManager.namePanel.SetActive(false);
        }
        else
        {
            guiManager.namePanel.SetActive(true);
            guiManager.Speaker.text = name;
        }

        //喋っているキャラをハイライト
        foreach (BattleCharacter character in Characters)
        {
            if (character.Name == objname)
            {
                //ハイライト
                character.HighLight();
            }
            else
            {
                //グレーアウト
                character.GrayOut();
            }
        }
    }

    //キャラの退場 #leave=udon のように呼ばれる
    public void LeaveCaracter(string name = "")
    {
        foreach (var character in Characters)
        {
            if(character.Name == name)
            {
                //フェードアウト
                Characters.Remove(character);
                character.Leave();
                return;
            }
        }
    }

    //200616 シーン終了フラグを立てて、あと1度クリックしたら遷移するようにする
    public void SetIsEnd(bool flag)
    {
        isEnd = flag;
    }

    //全てのキャラクターを削除して1人だけ表示させる
    public void SetCharactor(string name)
    {
        foreach (BattleCharacter character in Characters)
        {
            Destroy(character.transform.parent);
            character.Destroy();
        }
        Characters = new List<BattleCharacter>();
        AddCharactorBattleMap(name);
    }

    //会話終了時にキャラクターをクリアする
    public void CharactorClear()
    {
        foreach (BattleCharacter character in Characters)
        {
            //アタッチされているゲームオブジェクト(立ち絵)削除
            Debug.Log($"削除{character.name}");
            Destroy(character.gameObject);
        }
        Characters = new List<BattleCharacter>();
    }

    //210519戦闘シーンでの立ち絵追加 カメラに依存しない為、全く別の処理
    public void AddCharactorBattleMap(string name)
    {
        //既に同名のキャラが居れば何もしない
        if (Characters.Exists(c => c.Name == name)) return;

        //Resourcesの中からCharactorというprefabを取得してcharacter型にする
        var prefab = Resources.Load("Prefabs/CharacterImage") as GameObject;
        var charactorObject = Object.Instantiate(prefab);

        //canvas配下に表示
        charactorObject.transform.SetParent(talkView.transform, false);

        //ウィンドウが手前に表示されるようにする
        charactorObject.transform.SetAsFirstSibling();
        BattleCharacter character = charactorObject.GetComponent<BattleCharacter>();

        //引数で名前を初期化
        character.Init(name);
        //キャラクターリストに追加
        Characters.Add(character);

        //シーケンス
        imageSeq = DOTween.Sequence();

        Debug.Log("Screenwidth:" + Screen.width);

        //キャラクター全員を画面に均等に表示させる
        for (int i = 0; i < Characters.Count; i++)
        {

            //上下、左右の微調整
            int yPos = 200;
            float xAdjust = 300;

            //立ち絵の位置 最初は画面中央
            var cpos = new Vector3(Screen.width / 2, yPos, 0);

            //リストの末尾＝新しく登場する人
            if (i == Characters.Count - 1)
            {
                //1人目の場合、iは0となるので何もせず画面の中央に表示される
                if (i == 0)
                {
                    Characters[i].transform.position = cpos;
                    Characters[i].Appear();
                }
                else
                {

                    //2人目以降はi>1となるので左端へ
                    cpos = new Vector3(Screen.width / 2 - xAdjust, yPos, 0);
                    Characters[i].transform.position = cpos;
                    Characters[i].Appear();
                }

            }

            else if (i == 0)
            {
                //一人目のキャラが、画面に2人目以上表示された場合のふるまい(最初の1人のみの時は入らない)
                //画面右端に移動させる その後は1人増える度少しずつ右へずれていく
                
                cpos = new Vector3(Screen.width / 2 + xAdjust + (0.5f * (Characters.Count - 2)), yPos, 0);
                Characters[i].transform.position = cpos;
                Characters[i].Appear();

            }
        }
    }

    //立ち絵の表情変更 #image_hiroko=aseriのように来た時呼ばれる
    public void SetImage(string name, string ID)
    {
        //キャラクターリストから引数の名前で検索
        var character = Characters.Find(c => c.Name == name);

        if(character == null)
        {
            Debug.LogError("立ち絵を変更するキャラクターが存在しません");
            return;
        }
        character.SetImage(ID);
    }

}
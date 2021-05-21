using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// まずGameControllerから呼ばれ、それぞれのクラスのインスタンスを作る
/// パーサー結果によってSceneReaderから各種画面処理のメソッドが呼ばれる
/// </summary>
public class SceneController : MonoBehaviour
{
    private GameController gc;
    private BattleTalkManager battleTalkManager;
    public Actions Actions;

    private GUIManager guiManager;
    private SceneHolder sceneHolder;
    private SceneReader sceneReader;
    private BattleSceneReader battleSceneReader;    //戦闘マップ用


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

    private Scene currentScene;
    public List<Character> Characters = new List<Character>();

    FadeInOutManager fadeInOutManager;

    //コンストラクタ
    public SceneController(GameController gc)
    {
        //GameControllerへのインスタンス作成
        this.gc = gc;

        //GUIManagerインスタンス作成
        guiManager = GameObject.Find("GUI").GetComponent<GUIManager>();

        //SceneHolderは初期化と共にtxtを
        //#scene=001のような記述ごとにScene型のリストに変換して保持しておく
        string scenarioPath = "ScenarioData/scenarios";
        sceneHolder = new SceneHolder(scenarioPath);

        sceneReader = new SceneReader(this);

        background = GameObject.Find("BackGround").GetComponent<BackGround>();
        background.Init();

        fadeInOutManager = GameObject.Find("FadeInOut").GetComponent<FadeInOutManager>();
        fadeInOutManager.FadeinStart();

        //DoTween
        textSeq.Complete();
    }

    /// <summary>
    /// ボタン押した時の処理 GameControllerのUpdateメソッドから呼ばれて監視してる
    /// </summary>
    public void WaitClick()
    {

        //210207 暗転中は操作しない
        if (fadeInOutManager.isFadeinFadeout())
        {
            return;
        }

        //終了フラグが立っていたら画面遷移　スタートボタンを押した場合はスキップ
        if (isEnd || KeyConfigManager.GetKeyDown(KeyConfigType.START))
        {
            //200827遷移先がマップ
            if(sceneChangeDestination == DestinationScene.MAP)
            {
                ChangeSceneToMap();
            }
            else
            {
                //そうでない時はステータス画面へ
                EndTalk();
            }
            
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

    //会話を終了してステータス画面に戻る
    public void EndTalk(){

        fadeInOutManager.ChangeScene("Status");
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
    /// 最初にGameControllerの初期化→SetSceneメソッドから呼ばれる
    /// その後はマウスクリック時に呼び出し
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
            sceneReader.ReadLines(currentScene);
        }
    }

    //シーンをセットする GameControllerに最初呼ばれる
    public void SetScene(string id)
    {
        //現在のシーンをシーン一覧から取得してセット
        currentScene = sceneHolder.Scenes.Find(s => s.ID == id);

        //インスタンスを作り直し
        currentScene = currentScene.Clone();
        //もし存在しないシーンを指定されたらエラー
        if (currentScene == null) Debug.LogError("scenario not found");
        //シーン最初のテキストをパースして画面表示
        SetNextProcess();
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

    //選択肢のパネルを表示する？
    public void SetOptionsPanel()
    {
        guiManager.ButtonPanel.gameObject.SetActive(isOptionsShowed);
    }

    //名前欄の設定と、喋っているキャラのハイライト
    //SceneReaderのパース中「#speaker」が有った時呼ばれる
    public void SetSpeaker(string name, string objname)
    {
        //引数の名前を画面の名前欄に設定
        guiManager.Speaker.text = name;

        //喋っているキャラをハイライト
        foreach (Character character in Characters)
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

    //200616 背景画像変更 back=jinja等
    public void SetBackGround(string spriteName = "")
    {
        background.setSprite(spriteName);
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
    public void setIsEnd(bool flag)
    {
        isEnd = flag;
    }

    //200827 会話後に戦闘シーンへ遷移する
    public void ChangeSceneToMap()
    {
        fadeInOutManager.ChangeScene("Map1Scene");
    }

    //全てのキャラクターを削除して1人だけ表示させる
    public void SetCharactor(string name)
    {
        Characters.ForEach(c => c.Destroy());
        Characters = new List<Character>();
        AddCharactor(name);
    }

    //SceneReaderから「#chara」が有った時呼ばれる
    //キャラを表示
    public void AddCharactor(string name)
    {
        //既に同名のキャラが居れば何もしない
        if (Characters.Exists(c => c.Name == name)) return;

        //Resourcesの中からCharactorというprefabを取得してcharacter型にする
        var prefab = Resources.Load("Charactor") as GameObject;
        var charactorObject = Object.Instantiate(prefab);
        Character character = charactorObject.GetComponent<Character>();

        //引数で名前を初期化
        character.Init(name);
        //キャラクターリストに追加
        Characters.Add(character);

        //シーケンス
        imageSeq = DOTween.Sequence();
        
        //キャラクター全員を画面に均等に表示させる
        for (int i = 0; i < Characters.Count; i++)
        {

            var pos = guiManager.MainCamera.ScreenToWorldPoint(Vector3.zero);
            var pos2 = guiManager.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            
            //画面の幅
            var posWidth = pos2.x - pos.x;

            //左からどの位の位置にするか pos.xは画面中央 i + 1は人数
            //1人の時は画面の幅/2となるので中央に配置される
            //var left = pos.x + (posWidth * (i + 1) / (Characters.Count + 1));

            //上下、左右の微調整
            float yAdjust = -3;
            float xAdjust = 4;

            //立ち絵の位置 最初は画面中央
            var cpos = new Vector3(pos.x + posWidth /2, guiManager.MainCamera.transform.position.y + yAdjust, 0);
            //var cpos = new Vector3(left, guiManager.MainCamera.transform.position.y + yAdjust, 0);

            //リストの末尾＝新しく登場する人
            if (i == Characters.Count - 1)
            {
                //1人目の場合、iは0となるので何もせず画面の中央に表示される
                if(i == 0)
                {
                    Characters[i].transform.position = cpos;
                    Characters[i].Appear();
                }
                else
                {

                    //2人目以降はi>1となるので左端へ
                    cpos = new Vector3(-xAdjust, guiManager.MainCamera.transform.position.y + yAdjust, 0);
                    imageSeq.Append(Characters[i].transform.DOMove(cpos, 0f))
                        .OnComplete(() => character.Appear());
                }
            }
            
            else if (i == 0)
            {
                //一人目のキャラが、画面に2人目以上表示された場合のふるまい(最初の1人のみの時は入らない)
                //画面右端に移動させる その後は1人増える度少しずつ右へずれていく
                cpos = new Vector3(xAdjust+(0.5f * (Characters.Count - 2)), guiManager.MainCamera.transform.position.y + yAdjust, 0);
                imageSeq.Append(Characters[i].transform.DOMove(cpos, 0.2f)).SetEase(Ease.OutCubic);

            }

            else
            {
                //1人目でなく、新しく登場でもない人 新キャラが登場したら右へ重ならないように移動する
                cpos = new Vector3(xAdjust-(1.5f*i), cpos.y, 0);
                imageSeq.Join(Characters[i].transform.DOMove(cpos, 0.2f)).SetEase(Ease.OutCubic);
            }
        }
    }

    /// <summary>
    /// 210515 キャラクターを移動させる
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pos"></param>
    public void MoveCharactor(string name, string moveDest)
    {
        Character character = Characters.FirstOrDefault(c => c.Name == name);
        if(character == null)
        {
            Debug.Log("キャラクターが存在しません");
            return;
        }
        if (moveDest == "center")
        {
            var pos = guiManager.MainCamera.ScreenToWorldPoint(Vector3.zero);
            var pos2 = guiManager.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            //画面の幅
            var posWidth = pos2.x - pos.x;

            //立ち絵の位置 最初は画面中央
            var cpos = new Vector3(pos.x + posWidth / 2, character.transform.position.y, 0);
            character.transform.DOMove(cpos, 0.2f);
        }
    }

    //立ち絵の表情変更 #image_hiroko=aseriのように来た時呼ばれる
    public void SetImage(string name, string ID)
    {
        //キャラクターリストから引数の名前で検索 存在しないとぬるぽ
        var character = Characters.Find(c => c.Name == name);
        character.SetImage(ID);
    }

    //選択肢を作成する
    public void SetOptions(List<(string text, string nextScene)> options)
    {
        isOptionsShowed = true;
        foreach (var o in options)
        {
            Button b = Object.Instantiate(guiManager.OptionButton);
            Text text = b.GetComponentInChildren<Text>();
            text.text = o.text;
            b.onClick.AddListener(() => onClickedOption(o.nextScene));
            b.transform.SetParent(guiManager.ButtonPanel, false);
        }
    }

    //選択肢がクリックされた時
    public void onClickedOption(string nextID = "")
    {
        SetScene(nextID);
        isOptionsShowed = false;
        foreach (Transform t in guiManager.ButtonPanel)
        {
            UnityEngine.Object.Destroy(t.gameObject);
        }
    }

}
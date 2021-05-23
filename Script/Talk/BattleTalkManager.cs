using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210519 戦闘シーンの会話シーン制御 GameControllerを参考に作成
/// </summary>
public class BattleTalkManager : MonoBehaviour
{
    public BattleSceneController battleSceneController;
    private BattleMapManager battleMapManager;
    private GameObject talkWindow;
    private GameObject talkView;
    private List<string> viewedTalkList = new List<string>();    //既に表示した会話の再表示を防ぐ用



    // 初期化
    public void Init(BattleMapManager battleMapManager, GameObject talkWindow,GameObject talkView, FadeInOutManager fadeInOutManager)
    {
        this.battleMapManager = battleMapManager;
        this.talkWindow = talkWindow;
        this.talkView = talkView;
        battleSceneController = new BattleSceneController(this, talkWindow.GetComponent<GUIManager>(), fadeInOutManager, talkView);
    }

    //BattleMapManagerのUpdateから呼ばれる
    public void TalkUpdate()
    {
        //戦闘開始前会話が無い章等、nullの場合は会話終了
        if (battleSceneController.currentScene == null)
        {
            Debug.Log("会話データが有りません");
            TalkEnd();
            return;
        }
        //非表示ならウィンドウ表示して最初のメッセージ表示
        if (!talkView.activeSelf)
        {
            talkView.SetActive(true);
            battleSceneController.SetNextProcess();
        }

        //クリックの処理
        battleSceneController.WaitClick();

        //フラグを見て選択肢、▽マークを出したり消したりする処理
        battleSceneController.SetComponents();
    }

    //戦闘前会話をセットする sceneの命名規則は「STAGE〇_BATTLESTART」
    //「戦闘開始」ボタンを押した時に呼ばれる
    public bool IsBattleStartTalkExist(Chapter chapter)
    {
        string sceneName = chapter.ToString() + "_BATTLESTART";

        if (battleSceneController.CheckSceneExist(sceneName)) { 
            battleSceneController.SetScene(sceneName);
            Debug.Log($"シーン読み込み : {sceneName}");
            return true;
        }
        Debug.Log($"シーンが存在しませんでした : {sceneName}");
        return false;
    }

    //指定ターン経過時の会話が有るか確認を行う 
    public bool IsTurnTalkExist(Chapter chapter, int turn)
    {
        //そのターンの会話が存在するかを確認する 命名規則は「STAGE〇_TURN_(ターン数)」
        string sceneName = chapter.ToString() + "_TURN_"+ turn ;

        //存在すれば会話モードへ
        if (battleSceneController.CheckSceneExist(sceneName))
        {
            //確認と同時にシーンにセットを行う
            battleSceneController.SetScene(sceneName);
            Debug.Log($"シーン読み込み : {sceneName}");
            return true;
        }
        Debug.Log($"シーンが存在しませんでした : {sceneName}");
        return false;
    }

    //210520 戦闘前会話が存在するかを確認する 表示済みかの判定も合わせて行う
    public bool IsBattleStartTalkExist(Chapter chapter, string unitName)
    {
        //命名規則は、汎用は「「STAGE〇_BOSS」、専用の組み合わせは「STAGE〇_BOSS_(味方)」
        //専用会話の方が優先度が高い
        string sceneName = chapter.ToString() + "_BOSS";

        //既に表示済みの会話は再表示しない
        if (viewedTalkList.Contains(sceneName))
        {
            Debug.Log($"既に表示済みの会話なのでスキップ : {sceneName}");
            return false;
        }

        //存在すれば会話モードへ
        if (battleSceneController.CheckSceneExist(sceneName))
        {
            //既に表示した会話リストに追加
            viewedTalkList.Add(sceneName);

            //確認と同時にシーンにセットを行う
            battleSceneController.SetScene(sceneName);
            Debug.Log($"シーン読み込み : {sceneName}");
            return true;
        }
        Debug.Log($"シーンが存在しませんでした : {sceneName}");
        return false;
    }

    //ボス撃破時の会話が有るか確認して、存在すればセットする
    public bool IsBossDestroyTalkExist(Chapter chapter)
    {
        //そのターンの会話が存在するかを確認する 命名規則は「STAGE〇__BOSS_DESTROY」
        string sceneName = chapter.ToString() + "_BOSS_DESTROY";

        //既に表示済みの会話は再表示しない
        if (viewedTalkList.Contains(sceneName))
        {
            Debug.Log($"既に表示済みの会話なのでスキップ : {sceneName}");
            return false;
        }

        //存在すれば会話モードへ
        if (battleSceneController.CheckSceneExist(sceneName))
        {
            //既に表示した会話リストに追加
            viewedTalkList.Add(sceneName);

            //確認と同時にシーンにセットを行う
            battleSceneController.SetScene(sceneName);
            Debug.Log($"シーン読み込み : {sceneName}");
            return true;
        }
        Debug.Log($"シーンが存在しませんでした : {sceneName}");
        return false;
    }

    //キャラ敗北時の会話 基本的には全員存在するが、一応確認
    public bool IsLoseTalkExist(string name)
    {

        string sceneName = name.ToLower() + "_LOSE";

        //既に表示済みの会話は再表示しない
        if (viewedTalkList.Contains(sceneName))
        {
            Debug.Log($"既に表示済みの会話なのでスキップ : {sceneName}");
            return false;
        }

        //存在すれば会話モードへ
        if (battleSceneController.CheckSceneExist(sceneName))
        {
            //既に表示した会話リストに追加
            viewedTalkList.Add(sceneName);

            //確認と同時にシーンにセットを行う
            battleSceneController.SetScene(sceneName);
            Debug.Log($"シーン読み込み : {sceneName}");
            return true;
        }
        Debug.Log($"シーンが存在しませんでした : {sceneName}");
        return false;
    }

    //210520　会話終了
    public void TalkEnd()
    {
        //立ち絵、ウィンドウなどのUIを消す
        talkView.SetActive(false);

        //戦闘前会話、ターン開始時会話の場合は自軍ターンへ
        if(battleMapManager.mapMode == MapMode.START_TALK)
        {
            battleMapManager.SetMapMode(MapMode.TURN_START);
        }
        else if (battleMapManager.mapMode == MapMode.TURN_START_TALK)
        {
            //ターン開始時会話は、開始エフェクト直後に挿入されているのでNORMALへ遷移
            battleMapManager.SetMapMode(MapMode.NORMAL);
        }
        else if (battleMapManager.mapMode == MapMode.BATTLE_BEFORE_TALK ||
            battleMapManager.mapMode == MapMode.BATTLE_AFTER_TALK)
        {
            //戦闘前会話、戦闘後会話、敗北時の会話はBATTLEモードへ
            battleMapManager.SetMapMode(MapMode.BATTLE);
        }
    }
}

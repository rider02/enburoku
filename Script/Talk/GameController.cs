
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ここから各クラスの初期化を行う
/// </summary>
public class GameController : MonoBehaviour
{
    public SceneController sceneController;


    void Start ()
    {
        //自分自身を引数にしてScneControllerのインスタンス作成
        //同時にActions、SceneHolder、SceneReaderの初期化、
        //txtからSceneリスト作成も行われる
        sceneController = new SceneController(this);

        //txtを読み込んでSceneリストを生成したら最初のシーンをセット
        SetFirstScene();
    }

    void Update()
    {
        //プレー時間更新
        PlayTimeManager.TimeUpdate();

        //クリックの処理
        sceneController.WaitClick();
        //これはフラグを見てGUIを出したり消したりする処理
        sceneController.SetComponents();
    }

    void SetFirstScene()
    {
        //200617 ここでTalkManagerの値を参照
        sceneController.SetScene(TalkManager.sceneName);
    }

}

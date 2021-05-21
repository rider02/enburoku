using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 支援会話レベルのボタン
/// </summary>
public class FriendLevelButton : MonoBehaviour
{
    [SerializeField]
    Text buttonText;

    TalkManager talkManager;

    //Initと言ってもオブジェクトを渡すだけ
    public void Init(TalkManager talkManager)
    {
        this.talkManager = talkManager;
    }

    //とりあえず昨日はテキストを変えるだけ＾＾；
    public void UpdateText(string text)
    {
        buttonText.text = text;
    }

    //ボタンがクリックされた時
    public void OnButtonClick()
    {
        //読み込む支援会話をセットする
        talkManager.setFriendTalk(this.gameObject.name);
        talkManager.ChangeSceneToTalk();

    }
}

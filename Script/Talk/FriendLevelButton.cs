using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 支援会話レベルのボタン
/// TODO 210522 まだ作りかけ 新着は文字色を水色にする機能を追加したい
/// </summary>
public class FriendLevelButton : MonoBehaviour
{
    [SerializeField]
    Text buttonText;

    TalkManager talkManager;

    //初期化
    public void Init(TalkManager talkManager)
    {
        this.talkManager = talkManager;
    }

    //テキスト変更 A、B、Cの表示のみ
    public void UpdateText(string text)
    {
        buttonText.text = text;
    }

    //ボタンがクリックされた時
    public void OnButtonClick()
    {
        //読み込む支援会話をセットして会話シーンへ
        talkManager.setFriendTalk(this.gameObject.name);
        talkManager.ChangeSceneToTalk();

    }
}

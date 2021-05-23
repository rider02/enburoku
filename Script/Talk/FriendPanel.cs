using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 支援会話リストの名前を表示したりボタンを表示する
/// TODO 210522 まだ作りかけ 新着は文字色を水色にする機能を追加したい
/// </summary>
public class FriendPanel : MonoBehaviour
{
    [SerializeField]
    Text nameText;
    [SerializeField]
    GameObject friendLevelList;

    //名前を更新する
    public void UpdateText(string name)
    {
        nameText.text = name;
    }

    //支援会話ボタンをリストに追加する
    public void AddFriendLevelList(Transform friendLevelButton)
    {
        friendLevelButton.transform.SetParent(friendLevelList.transform);
    }
}

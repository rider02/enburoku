using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 支援会話リストの名前を表示したりボタンを表示する
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
    public void addFriendLevelList(Transform friendLevelButton)
    {
        friendLevelButton.transform.SetParent(friendLevelList.transform);
    }
}

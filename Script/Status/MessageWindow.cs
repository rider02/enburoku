using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 200723
/// 警告などを表示するだけのウィンドウ
/// </summary>
public class MessageWindow : MonoBehaviour
{
    [SerializeField]
    Text messageText;

    public void UpdateText(string text)
    {

        messageText.text = text;
    }
}

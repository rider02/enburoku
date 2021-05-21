using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 戦闘のメッセージを表示する用のウィンドウ
/// </summary>
public class BattleMessageWindow : MonoBehaviour
{
    [SerializeField]
    Text messageText;

    // これは引数の内容を反映するだけ
    public void UpdateText(string message)
    {
        this.messageText.text = message;
    }

}

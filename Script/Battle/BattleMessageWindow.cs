using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 戦闘のメッセージを表示する用のウィンドウ 本番では非表示
/// </summary>
public class BattleMessageWindow : MonoBehaviour
{
    [SerializeField]
    Text messageText;

    //引数のテキストを表示するだけ
    public void UpdateText(string message)
    {
        this.messageText.text = message;
    }

}

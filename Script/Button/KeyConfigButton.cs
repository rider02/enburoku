using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210513 キーコンフィグ(決定、キャンセル等)を設定する用のボタン
/// </summary>
public class KeyConfigButton : MonoBehaviour
{
    [SerializeField]
    private Text KeyConfigTypeText; //決定、キャンセル等の機能

    [SerializeField]
    private Text assignKeyCodeText; //アサインされているKeyCode

    private KeyConfigType keyconfigType;    //どの機能のボタンかを保持

    private KeyConfigManager keyConfigManager;


    public void Init(KeyConfigManager keyConfigManager, KeyConfigType keyconfigType, KeyCode keycode)
    {
        //機能テキスト、アサインされているKeyCodeテキスト設定
        KeyConfigTypeText.text = keyconfigType.GetStringValue();
        assignKeyCodeText.text = keycode.ToString();

        this.keyconfigType = keyconfigType;

        this.keyConfigManager = keyConfigManager;
    }

    //テキスト更新
    public void UpdateText(KeyConfigType keyconfigType, KeyCode keycode)
    {
        //機能テキスト、アサインされているKeyCodeテキスト設定
        KeyConfigTypeText.text = keyconfigType.GetStringValue();
        assignKeyCodeText.text = keycode.ToString();
    }

    //クリック時
    public void OnClick()
    {
        keyConfigManager.KeyAssignReceipt(keyconfigType);
    }

    //選択時
    public void OnSelect()
    {

    }
}

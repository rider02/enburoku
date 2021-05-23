using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステータス画面のモード(ステータス、装備等)を左上に表示するだけのクラス
/// </summary>
public class ModeWindow : MonoBehaviour
{
    [SerializeField]
    Text modeText;

    //テキスト更新
    public void UpdateText(string text)
    {

        modeText.text = text;
    }
}

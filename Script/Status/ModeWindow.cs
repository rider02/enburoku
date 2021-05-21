using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeWindow : MonoBehaviour
{
    [SerializeField]
    Text modeText;

    /// <summary>
    /// 200726 現在のモードを表示するだけのウィンドウ
    /// </summary>
    /// <param name="text"></param>
    public void UpdateText(string text)
    {

        modeText.text = text;
    }
}

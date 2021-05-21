using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GUIの経過時間を更新するメソッド
/// </summary>
public class TimeWindow : MonoBehaviour
{
    [SerializeField]
    Text timeText;

    int beforeMinute;

    public void init()
    {
        UpdateText();
    }

        //テキスト更新
    public void UpdateTime()
    {
        //時間の更新が有った時のみテキスト更新
        if(PlayTimeManager.minute != beforeMinute)
        {
            //変更前の時間を更新
            beforeMinute = PlayTimeManager.minute;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        timeText.text = PlayTimeManager.hour.ToString() + " : " + PlayTimeManager.minute.ToString("00");
    }

}

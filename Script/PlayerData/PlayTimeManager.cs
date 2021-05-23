using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレー時間を加算してセーブデータ等に表示する
/// これからはUpdateTextを呼び出さず、ウィンドウから呼び出す
/// </summary>
public static class PlayTimeManager
{
    public static int hour;         //時
    public static int minute;       //分
    private static float seconds;   //秒

    public static void TimeUpdate()
    {
        //時間を更新
        seconds += Time.deltaTime;
        
        //秒が60になれば分を更新
        if (seconds >= 60f)
        {
            minute++;
            seconds = seconds - 60;

        }

        //分が60になれば時を更新
        if (minute >= 60f) {
            hour++;
            minute = minute - 60;

        }
    }


}

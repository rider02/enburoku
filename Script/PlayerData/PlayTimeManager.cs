using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレー時間を加算してセーブデータ等に表示する
/// これからはUpdateTextを呼び出さず、ウィンドウから呼び出す
/// </summary>
public class PlayTimeManager : MonoBehaviour
{

    //時
    public static int hour;

    //分
    public static int minute;

    //秒
    private static float seconds;

    public static bool isTimeInit;

    //初期化
    public static void init()
    {
        hour = 0;
        minute = 0;
        seconds = 0f;

        isTimeInit = true;
    }

    public static void TimeUpdate()
    {
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

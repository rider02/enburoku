using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの主人公、難易度、モードを保存する
/// </summary>
public class ModeManager : MonoBehaviour
{
    public static Route route;              //霊夢、レミリアルートのどちらか
    public static Difficulty difficulty;    //難易度
    public static Mode mode;                //敗北ユニットの処理
    public static bool isModeInit;          //初期化フラグ

    //最初の設定画面で設定しない場合、テスト用にこの値にする
    public void Init()
    {
        route = Route.REIMU;
        difficulty = Difficulty.NORMAL;
        mode = Mode.CLASSIC;

        isModeInit = true;
    }

    //200829_霊夢、レミリアルート設定
    public void setRoute(Route route)
    {
        ModeManager.route = route;
    }

    //210220 難易度設定
    public void SetDifficulty(Difficulty difficulty)
    {
        ModeManager.difficulty = difficulty;
    }

    //敗北したユニットの処理設定
    public void SetMode(Mode mode)
    {
        ModeManager.mode = mode;
    }
}

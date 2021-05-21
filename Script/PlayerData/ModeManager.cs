using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの主人公、難易度、モードを保存する
/// </summary>
public class ModeManager : MonoBehaviour
{
    public static Route route;

    public static Difficulty difficulty;

    public static Mode mode;

    public static bool isModeInit;

    //これ、最初の設定画面で設定するけどとりあえず仮
    public void Init()
    {
        route = Route.REIMU;
        difficulty = Difficulty.NORMAL;
        mode = Mode.CLASSIC;

        isModeInit = true;
    }

    //200829_レミリアモード実装 ボタンから呼ばれる
    public void setRoute(Route route)
    {
        ModeManager.route = route;
    }

    //210220 やっとモードと難易度を設定
    public void SetDifficulty(Difficulty difficulty)
    {
        ModeManager.difficulty = difficulty;
    }

    public void SetMode(Mode mode)
    {
        ModeManager.mode = mode;
    }
}

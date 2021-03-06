using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210303 入手済みの宝箱は再表示しない事を管理する
/// </summary>
public class AcquiredTreasureManager : MonoBehaviour
{
    //入手済みの宝箱リスト
    public static List<AcquiredTreasure> treasureList { get; set; }
    public static bool isInit;

    public void Init()
    {
        treasureList = new List<AcquiredTreasure>();
        isInit = true;
    }
}

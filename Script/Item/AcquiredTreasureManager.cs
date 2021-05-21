using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210303 “üèÏ‚İ‚Ì•ó” ‚ÍÄ•\¦‚µ‚È‚¢–‚ğŠÇ—‚·‚é
/// </summary>
public class AcquiredTreasureManager : MonoBehaviour
{
    public static List<AcquiredTreasure> treasureList { get; set; }
    public static bool isInit;

    public void Init()
    {
        treasureList = new List<AcquiredTreasure>();
        isInit = true;
    }
}

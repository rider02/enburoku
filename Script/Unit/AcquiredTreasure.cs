using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210303 入手済みの宝箱の情報
/// </summary>
[System.Serializable]
public class AcquiredTreasure
{
    //座標
    public int x;
    public int y;
    //登場する章
    public Chapter chapter;

    //コンストラクタ
    public AcquiredTreasure(int x, int y, Chapter chapter)
    {
        this.x = x;
        this.y = y;
        this.chapter = chapter;
    }
}

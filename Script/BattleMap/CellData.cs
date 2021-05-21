using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellData
{
    //座標
    public int x;
    public int y;
    public CellType type;

    public CellData(Main_Cell mainCell)
    {

        this.x = mainCell.X;
        this.y = mainCell.Y;
        this.type = mainCell.Type;
    }
}

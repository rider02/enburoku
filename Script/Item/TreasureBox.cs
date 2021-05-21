using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//210301 遂に作った 宝箱クラス
[System.Serializable]
public class TreasureBox
{
    //初期配置されている場所
    public Coordinate coordinate;

    public int x;
    public int y;

    public bool isEmpty = false;

    BattleMapManager battleMapManager;

    public Item item;

    //コンストラクタ
    public TreasureBox(Item item, Coordinate coordinate)
    {
        this.item = item;
        this.coordinate = coordinate;
    }

    public void Init(BattleMapManager battleMapManager)
    {
        this.battleMapManager = battleMapManager;
    }

    /// <summary>
    /// 宝箱を開ける
    /// </summary>
    /// <returns></returns>
    public Item Open()
    {
        //空いたフラグを立てる
        isEmpty = true;

        return item;
    }

    //初期化時にDatabaseから同じインスタンスを取得してしまうので作成
    public TreasureBox Clone()
    {
        // Object型で返ってくるのでキャストが必要
        return (TreasureBox)MemberwiseClone();
    }

}

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// マップの地形データ
/// アセットファイル化してステージ毎に読み込んで使用する
/// </summary>
public class MapData : ScriptableObject
{

    public List<CellData> cells = new List<CellData>();
}

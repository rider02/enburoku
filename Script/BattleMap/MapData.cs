using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// マップ一覧を作成する
/// UnitDatabaseとほぼ同じ
/// </summary>
public class MapData : ScriptableObject
{

    public List<CellData> cells = new List<CellData>();
}

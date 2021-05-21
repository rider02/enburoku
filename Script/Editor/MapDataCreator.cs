using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// マップ一覧を作成する
/// UnitDatabaseとほぼ同じ
/// </summary>

public static class MapDataCreator
{

    public static void Create(List<Main_Cell> cells, Stage stage)
    {
        MapData mapData = ScriptableObject.CreateInstance<MapData>();

        foreach(Main_Cell mainCell in cells)
        {
            mapData.cells.Add(new CellData(mainCell));
        }

        string assetPath = $"Assets/Resources/{stage.chapter.ToString()}mapData.asset";

        //ファイル書き出し Resources配下に作る
        AssetDatabase.CreateAsset(mapData, assetPath);
        Debug.Log($"{assetPath}にマップデータを保存しました。");
    }

}

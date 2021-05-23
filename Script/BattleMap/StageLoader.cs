using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210302 ステージの読み込みを行うクラス
/// </summary>
public class StageLoader : MonoBehaviour
{

    public GameObject LoadStageTerrain(Stage stage)
    {

        return Instantiate(Resources.Load($"Prefabs/Terrain/{stage.chapter.ToString()}Terrain") as GameObject);
    }

    public Terrain LoadStageTerrainData(Stage stage)
    {
        //ステージ名に対応したTerrainを取得する
        return Resources.Load<Terrain>($"Prefabs/Terrain/{stage.chapter.ToString()}Terrain");
    }
}

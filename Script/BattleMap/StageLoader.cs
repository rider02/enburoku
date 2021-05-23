using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210302 �X�e�[�W�̓ǂݍ��݂��s���N���X
/// </summary>
public class StageLoader : MonoBehaviour
{

    public GameObject LoadStageTerrain(Stage stage)
    {

        return Instantiate(Resources.Load($"Prefabs/Terrain/{stage.chapter.ToString()}Terrain") as GameObject);
    }

    public Terrain LoadStageTerrainData(Stage stage)
    {
        //�X�e�[�W���ɑΉ�����Terrain���擾����
        return Resources.Load<Terrain>($"Prefabs/Terrain/{stage.chapter.ToString()}Terrain");
    }
}

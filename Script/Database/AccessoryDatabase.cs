using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// �A�N�Z�T���ꗗ��ScriptableObject�����p�N���X
/// </summary>
public class AccessoryDatabase : ScriptableObject
{

    //List�X�e�[�^�X��List
    public List<Accessory> accessoryList = new List<Accessory>();

    public Accessory FindByName(string accessoryName)
    {
        //���O�̈�v���������Ԃ� �������null
        return accessoryList.FirstOrDefault(accessory => accessory.name == accessoryName);

    }

}

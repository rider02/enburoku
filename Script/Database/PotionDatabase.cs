using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// �A�C�e���ꗗScriptableObject�����p�N���X
/// </summary>
public class PotionDatabase : ScriptableObject
{

    public List<Potion> potionList = new List<Potion>();

    public Potion FindByName(string potionName)
    {
        //���O�̈�v�����A�C�e����Ԃ� �������null
        return potionList.FirstOrDefault(potion => potion.name == potionName);

    }

}
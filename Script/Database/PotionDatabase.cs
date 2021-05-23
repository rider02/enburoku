using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// アイテム一覧ScriptableObject精製用クラス
/// </summary>
public class PotionDatabase : ScriptableObject
{

    public List<Potion> potionList = new List<Potion>();

    public Potion FindByName(string potionName)
    {
        //名前の一致したアイテムを返す 無ければnull
        return potionList.FirstOrDefault(potion => potion.name == potionName);

    }

}
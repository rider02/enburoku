using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// アクセサリ一覧のScriptableObject精製用クラス
/// </summary>
public class AccessoryDatabase : ScriptableObject
{

    //ListステータスのList
    public List<Accessory> accessoryList = new List<Accessory>();

    public Accessory FindByName(string accessoryName)
    {
        //名前の一致した武器を返す 無ければnull
        return accessoryList.FirstOrDefault(accessory => accessory.name == accessoryName);

    }

}

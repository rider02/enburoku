using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ユニット一覧ScriptableObject精製用クラス
/// </summary>
public class UnitDatabase : ScriptableObject
{

    //ListステータスのList
    public List<Unit> unitList = new List<Unit>();

    /// <summary>
    /// ユニットの名前からユニットを返却する
    /// </summary>
    /// <param name="unitName"></param>
    /// <returns></returns>
    public Unit FindByName(string unitName)
    {
        //名前の一致したユニットを返す 無ければnull
        return unitList.FirstOrDefault(unit => unit.name == unitName);

    }

}
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 武器一覧のクラス
/// </summary>
public class WeaponDatabase : ScriptableObject
{

    //ListステータスのList
    public List<Weapon> weaponList = new List<Weapon>();

    /// <summary>
    /// 武器の名前から武器を返却する。武器の名前はユニーク前提
    /// </summary>
    /// <param name="weaponName"></param>
    /// <returns></returns>
    public Weapon FindByName(string weaponName)
    {
        //名前の一致した武器を返す 無ければnull
        return weaponList.FirstOrDefault(weapon => weapon.name == weaponName);

    }

}
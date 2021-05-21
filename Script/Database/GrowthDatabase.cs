using UnityEngine;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// ユニット成長率のクラス
/// </summary>
public class GrowthDatabase : ScriptableObject
{

    //ListステータスのList
    public List<GrowthRate> growthList = new List<GrowthRate>();

    /// <summary>
    /// ユニットの名前からユニットを返却する
    /// </summary>
    /// <param name="unitName"></param>
    /// <returns></returns>
    public GrowthRate FindByName(string unitName)
    {
        //名前の一致したユニットを返す 無ければnull
        return growthList.FirstOrDefault(growthRate => growthRate.name == unitName);

    }

}
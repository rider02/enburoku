using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// レベルアップ時のコメント一覧ScriptableObject精製用クラス
/// </summary>
public class LvUpImpreDatabase : ScriptableObject
{

    //ListステータスのList
    public List<LvUpImpre> impreList = new List<LvUpImpre>();

    /// <summary>
    /// ユニットの名前からユニットを返却する
    /// </summary>
    /// <param name="unitName"></param>
    /// <returns></returns>
    public LvUpImpre FindByName(string unitName)
    {
        //名前の一致したユニットのコメントを返す 無ければnull
        return impreList.FirstOrDefault(LvUpImpre => LvUpImpre.name == unitName);

    }

}
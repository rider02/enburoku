using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 敵一覧を作ってくれるクラス
/// UnitDatabaseとほぼ同じ
/// </summary>
public class EnemyDatabase : ScriptableObject
{

    //ListステータスのList
    public List<Enemy> enemyList = new List<Enemy>();

    /// <summary>
    /// ユニットの名前からユニットを返却する
    /// </summary>
    /// <param name="unitName"></param>
    /// <returns></returns>
    public Enemy FindByName(string enemyName)
    {
        //名前の一致したユニットを返す 無ければnull
        return enemyList.FirstOrDefault(enemy => enemy.name == enemyName);
    }

}

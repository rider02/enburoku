using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各ユニットがレベルアップした時のコメント
/// </summary>
[System.Serializable]
public class LvUpImpre
{
    public List<string> lvupImpre;

    public string name;

    //コンストラクタ
    public LvUpImpre(string name, List<string> lvUpImpre)
    {

        //コメントを言うキャラの名前
        this.name = name;

        this.lvupImpre = lvUpImpre;
    }
}

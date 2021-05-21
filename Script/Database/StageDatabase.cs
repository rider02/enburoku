using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 列挙型を渡すとステージを返してくれる
/// </summary>
public class StageDatabase : ScriptableObject
{
    public List<Stage> stageList = new List<Stage>();

    //stringで検索しても良いが、列挙型で検索してみる
    public Stage FindByChapter(Chapter chapter)
    {
        return stageList.FirstOrDefault(stage => stage.chapter == chapter);
    }
}

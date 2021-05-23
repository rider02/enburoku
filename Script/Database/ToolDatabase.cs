using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 道具(鍵等)ScriptableObject精製用クラス
/// </summary>
public class ToolDatabase : ScriptableObject
{

    public List<Tool> toolList = new List<Tool>();

    public Tool FindByName(string toolName)
    {
        //名前の一致したアイテムを返す 無ければnull
        return toolList.FirstOrDefault(tool => tool.name == toolName);

    }

}

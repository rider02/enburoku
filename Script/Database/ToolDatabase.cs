using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ToolDatabase : ScriptableObject
{

    public List<Tool> toolList = new List<Tool>();

    public Tool FindByName(string toolName)
    {
        //名前の一致したアイテムを返す 無ければnull
        return toolList.FirstOrDefault(tool => tool.name == toolName);

    }

}

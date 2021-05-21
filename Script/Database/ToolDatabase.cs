using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ToolDatabase : ScriptableObject
{

    public List<Tool> toolList = new List<Tool>();

    public Tool FindByName(string toolName)
    {
        //���O�̈�v�����A�C�e����Ԃ� �������null
        return toolList.FirstOrDefault(tool => tool.name == toolName);

    }

}

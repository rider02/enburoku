using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

/// <summary>
/// 210216
/// 金塊、鍵、クラスチェンジアイテムなどのアセットファイルを作ってくれるクラス
/// Unity上部の「Create」から使用する
/// </summary>
public static class ToolDatabaseCreator
{
    [MenuItem("Create/ToolDatabase")]
    private static void Create()
    {
        ToolDatabase toolDatabase = ScriptableObject.CreateInstance<ToolDatabase>();

        string name = "金塊";
        string annotation = "店で高く売れる";
        bool isClassChange = false;

        Tool tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "大きな金塊";
        annotation = "店で高く売れる";
        isClassChange = false;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "巨大な金塊";
        annotation = "店で高く売れる";
        isClassChange = false;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "扉の鍵";
        annotation = "扉を開けられる";
        isClassChange = false;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "宝の鍵";
        annotation = "宝箱を開けられる";
        isClassChange = false;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "勝利のビール";
        annotation = "中級職へのクラスチェンジに必要";
        isClassChange = true;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "栄光のビール";
        annotation = "上級職へのクラスチェンジに必要";
        isClassChange = true;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        //ファイル書き出し Resources配下に作る
        AssetDatabase.CreateAsset(toolDatabase, "Assets/Resources/toolDatabase.asset");
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

/// <summary>
/// 210216
/// ����A���A�N���X�`�F���W�A�C�e���Ȃǂ̃A�Z�b�g�t�@�C��������Ă����N���X
/// Unity�㕔�́uCreate�v����g�p����
/// </summary>
public static class ToolDatabaseCreator
{
    [MenuItem("Create/ToolDatabase")]
    private static void Create()
    {
        ToolDatabase toolDatabase = ScriptableObject.CreateInstance<ToolDatabase>();

        string name = "����";
        string annotation = "�X�ō��������";
        bool isClassChange = false;

        Tool tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "�傫�ȋ���";
        annotation = "�X�ō��������";
        isClassChange = false;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "����ȋ���";
        annotation = "�X�ō��������";
        isClassChange = false;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "���̌�";
        annotation = "�����J������";
        isClassChange = false;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "��̌�";
        annotation = "�󔠂��J������";
        isClassChange = false;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "�����̃r�[��";
        annotation = "�����E�ւ̃N���X�`�F���W�ɕK�v";
        isClassChange = true;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        name = "�h���̃r�[��";
        annotation = "�㋉�E�ւ̃N���X�`�F���W�ɕK�v";
        isClassChange = true;

        tool = new Tool(name, annotation, isClassChange);
        toolDatabase.toolList.Add(tool);

        //�t�@�C�������o�� Resources�z���ɍ��
        AssetDatabase.CreateAsset(toolDatabase, "Assets/Resources/toolDatabase.asset");
    }
    

}

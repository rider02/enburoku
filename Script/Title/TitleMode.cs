using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210513 �^�C�g���̏��
/// </summary>
public enum TitleMode
{
    [StringValue("�^�C�g��")]
    TITLE,

    [StringValue("���j���[")]
    MENU,

    [StringValue("���[�h")]
    LOAD,

    [StringValue("�f�[�^����")]
    DELETE,

    [StringValue("�L�[�R���t�B�O")]
    KEY_CONFIG,

    [StringValue("�L�[�R���t�B�O��t")]
    KEY_ASSIGN_RECEIPT,
}
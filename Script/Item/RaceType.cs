using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210219 ���j�b�g�ɐݒ肵���ꍇ�͎푰
/// ����ɐݒ肵���ꍇ�͓����Ώ�
/// </summary>
public enum RaceType
{
    [StringValue("����")]
    NONE,

    [StringValue("�l��")]
    HUMAN,

    [StringValue("�d��")]
    FAIRY,

    [StringValue("�H��")]
    GHOST,

    [StringValue("�d��")]
    YOUKAI,

    [StringValue("�g����")]
    MAGIC,

    [StringValue("�t�r�_")]
    TSUKUMOGAMI,

    [StringValue("�ʓe")]
    MOON_RABBIT,
}

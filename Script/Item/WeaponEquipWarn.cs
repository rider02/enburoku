using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 210217 ����𑕔��o���Ȃ����A�G���[���{�^���ɐݒ肷��
/// </summary>
public enum WeaponEquipWarn {

    //�G���[����
    NONE,

    //�K������
    [StringValue("���j�b�g���g�p�o���Ȃ���ނ̕���ł��B")]
    SKILL_NONE,

    //�Z�\���x���s��
    [StringValue("�n���x���s�����Ă��܂��B")]
    SKILL_LEVEL_NEED,

    //���L�����̐�p����
    [StringValue("���L�����̐�p����ł��B")]
    PRIVATE_WEAPON,

    //���Ă���
    [StringValue("��ꂽ����͏C������܂Ŏg���܂���B")]
    BROKEN
}

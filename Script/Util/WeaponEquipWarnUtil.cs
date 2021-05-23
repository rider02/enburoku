using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210522 ����𑕔��o���邩�m�F���āA�o���Ȃ���Όx���ƃG���[������Ԃ��N���X
/// �X�e�[�^�X��ʁA�퓬��ʂŌĂ΂�AStatusItemButton�AMapItemButton�Ŏg�p
/// </summary>
public static class WeaponEquipWarnUtil
{
    public static WeaponEquipWarn GetWeaponEquipWarn(Weapon weapon, Unit unit)
    {

        //210217 ����̏n���x������{�p�Ƀ��j�b�g�������Ă���K���ꗗ���擾

        //1.��p����̔���
        if (weapon.isPrivate)
        {
            //��p����̏ꍇ�A������Ǝg�p�҂���v���Ȃ���΃G���[
            if (weapon.ownerName != unit.name)
            {
                return WeaponEquipWarn.PRIVATE_WEAPON;
            }
        }

        //2.���Ă��邩����
        if (weapon.endurance <= 0)
        {
            return WeaponEquipWarn.BROKEN;
        }

        //3.�K���̔��� ���R��̃X�L�����x����NONE���Ƒ����s��
        //4.�X�L�����x��������Ă��Ȃ��Ƒ����s��
        if (weapon.type == WeaponType.SHOT)
        {
            if (unit.shotLevel == SkillLevel.NONE)
            {
                return WeaponEquipWarn.SKILL_NONE;
            }
            else if (weapon.skillLevel.GetPriorityValue() > unit.shotLevel.GetPriorityValue())
            {
                //����̗v�����x�����X�L�����x���ȏ�Ȃ�G���[
                return WeaponEquipWarn.SKILL_LEVEL_NEED;
            }


        }
        else if (weapon.type == WeaponType.LASER)
        {
            if (unit.laserLevel == SkillLevel.NONE)
            {
                return WeaponEquipWarn.SKILL_NONE;
            }
            else if (weapon.skillLevel.GetPriorityValue() > unit.laserLevel.GetPriorityValue())
            {
                return WeaponEquipWarn.SKILL_LEVEL_NEED;
            }

        }
        else if (weapon.type == WeaponType.STRIKE)
        {
            if (unit.strikeLevel == SkillLevel.NONE)
            {
                return WeaponEquipWarn.SKILL_NONE;
            }
            else if (weapon.skillLevel.GetPriorityValue() > unit.strikeLevel.GetPriorityValue())
            {
                return WeaponEquipWarn.SKILL_LEVEL_NEED;
            }

        }
        else if (weapon.type == WeaponType.HEAL)
        {
            if (unit.healLevel == SkillLevel.NONE)
            {
                return WeaponEquipWarn.SKILL_NONE;
            }
            else if (weapon.skillLevel.GetPriorityValue() > unit.healLevel.GetPriorityValue())
            {
                return WeaponEquipWarn.SKILL_LEVEL_NEED;
            }
        }

        //�G���[�Ȃ�
        return WeaponEquipWarn.NONE;
    }
}

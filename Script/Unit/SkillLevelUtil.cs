using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210218 �X�L�����x���A�b�v�̗ǂ����@�������č�炴��𓾂Ȃ�����
/// </summary>
public class SkillLevelUtil : MonoBehaviour
{
    /// <summary>
    /// 210218 �X�L�����x���̌v�Z���s��
    /// </summary>
    /// <param name="unit"></param>
    public static string CalculateSkillLevel(Unit unit)
    {
        //����𑕔����Ă��Ȃ��ꍇ�͌o���l�𓾂��Ȃ�
        if (unit.equipWeapon == null)
        {
            Debug.Log($"����𑕔����Ă��Ȃ��̂ŋZ�\�o���l�𓾂��܂���");
            return null;
        }

        if (unit.equipWeapon.type == WeaponType.SHOT)
        {
            //�X�L�����x����S�̏ꍇ�͌o���l�������Ȃ� NONE�͈ꉞ
            if (unit.shotLevel == SkillLevel.NONE || unit.shotLevel == SkillLevel.S)
            {
                return null;
            }

            //�o���l����
            if (unit.job.skills.Contains(Skill.�V�˔�))
            {
                //210226 �V�˔�������
                unit.shotExp += 2;
                Debug.Log($"{unit.name}��{Skill.�V�˔�}���ʂŃV���b�g�Z�\�o���l+2");
            }
            else
            {
                unit.shotExp += 1;
                Debug.Log($"{unit.name}�̃V���b�g�Z�\�o���l+1");
            }
            

            //SkillLevel�N���X��IntValue�͗v���o���l�Ȃ̂ŁA����ȏ�ɂȂ�΃X�L�����x���A�b�v
            if (unit.shotExp >= unit.shotLevel.GetIntValue())
            {
                //�o���l�����Z�b�g
                unit.shotExp = 0;

                //���x���A�b�v 
                unit.shotLevel = SkillLevelUtil.SkillLvUp(unit.shotLevel);
                Debug.Log($"{unit.name}�̃V���b�g�̏n���x��{unit.shotLevel}�ɏオ��܂����B");
                return $"�V���b�g�̏n���x��{unit.shotLevel}�ɏオ��܂����B";
            }
            else
            {
                //���Ƀ��x�����オ��Ȃ��������͉����Ԃ��Ȃ�
                return null;
            }
        }
        else if (unit.equipWeapon.type == WeaponType.LASER)
        {
            if (unit.job.skills.Contains(Skill.�V�˔�))
            {
                unit.laserExp += 2;
                Debug.Log($"{unit.name}��{Skill.�V�˔�}���ʂŃ��[�U�[�Z�\�o���l+2");
            }
            else
            {
                unit.laserExp += 1;
                Debug.Log($"{unit.name}�̃��[�U�[�Z�\�o���l+1");
            }
            

            if (unit.laserExp >= unit.laserLevel.GetIntValue())
            {
                unit.laserExp = 0;
                unit.laserLevel = SkillLevelUtil.SkillLvUp(unit.laserLevel);
                Debug.Log($"{unit.name}�̃��[�U�[�̏n���x��{unit.laserLevel}�ɏオ��܂����B");
                return $"���[�U�[�̏n���x��{unit.laserLevel}�ɏオ��܂����B";
            }
            else
            {
                //���Ƀ��x�����オ��Ȃ��������͉����Ԃ��Ȃ�
                return null;
            }
        }
        else if (unit.equipWeapon.type == WeaponType.STRIKE)
        {
            if (unit.job.skills.Contains(Skill.�V�˔�))
            {
                unit.strikeExp += 2;
                Debug.Log($"{unit.name}��{Skill.�V�˔�}���ʂŕ����Z�\�o���l+2");
            }
            else
            {
                unit.strikeExp += 1;
                Debug.Log($"{unit.name}�̕����Z�\�o���l+1");
            }
            

            if (unit.strikeExp >= unit.strikeLevel.GetIntValue())
            {
                unit.strikeExp = 0;
                unit.strikeLevel = SkillLevelUtil.SkillLvUp(unit.strikeLevel);

                Debug.Log($"{unit.name}�̕����̏n���x��{unit.strikeLevel}�ɏオ��܂����B");
                return $"�����̏n���x��{unit.strikeLevel}�ɏオ��܂����B";
            }
            else
            {
                //���Ƀ��x�����オ��Ȃ��������͉����Ԃ��Ȃ�
                return null;
            }
        }
        else if (unit.equipWeapon.type == WeaponType.HEAL)
        {
            if (unit.job.skills.Contains(Skill.�V�˔�))
            {
                unit.healExp += 2;
                Debug.Log($"{unit.name}��{Skill.�V�˔�}���ʂŖ����Z�\�o���l+2");
            }
            else
            {
                unit.healExp += 1;
                Debug.Log($"{unit.name}�̖����Z�\�o���l+1");
            }


            if (unit.healExp >= unit.laserLevel.GetIntValue())
            {
                unit.healExp = 0;
                unit.healLevel = SkillLevelUtil.SkillLvUp(unit.healLevel);
                Debug.Log($"{unit.name}�̖����̏n���x��{unit.healLevel}�ɏオ��܂����B");
                return $"�����̏n���x��{unit.healLevel}�ɏオ��܂����B";
            }
            else
            {
                //���Ƀ��x�����オ��Ȃ��������͉����Ԃ��Ȃ�
                return null;
            }
        }
        else
        {
            //�ʏ�L�蓾�Ȃ� �����~�X
            return null;
        }
    }

    //�񕜂̏ꍇ��equipWeapon���Q�Ƃ��Ȃ��̂Ōʂ�
    public static string CalculateHealSkillLevel(Unit unit)
    {
 
        //����S�Ȃ�オ��Ȃ�
        if (unit.healLevel == SkillLevel.NONE || unit.healLevel == SkillLevel.S)
        {
            return null;
        }

        //�o���l����
        if (unit.job.skills.Contains(Skill.�V�˔�))
        {
            unit.strikeExp += 2;
            Debug.Log($"{unit.name}��{Skill.�V�˔�}���ʂŉ񕜋Z�\�o���l+2");
        }
        else
        {
            unit.healExp += 1;
            Debug.Log($"{unit.name}�̉񕜋Z�\�o���l+1");
        }
        

        //SkillLevel�N���X��IntValue�͗v���o���l�Ȃ̂ŁA����ȏ�ɂȂ�΃X�L�����x���A�b�v
        if (unit.healExp >= unit.healLevel.GetIntValue())
        {
            //�o���l�����Z�b�g
            unit.healExp = 0;

            //���x���A�b�v 
            unit.healLevel = SkillLevelUtil.SkillLvUp(unit.healLevel);
            Debug.Log($"{unit.name}�̉񕜂̏n���x��{unit.healLevel}�ɏオ��܂����B");
            return $"�񕜂̏n���x��{unit.shotLevel}�ɏオ��܂����B";
        }
        else
        {
            //���Ƀ��x�����オ��Ȃ��������͉����Ԃ��Ȃ�
            return null;
        }
    }

    public static SkillLevel SkillLvUp(SkillLevel skillLevel)
    {
        switch (skillLevel)
        {
            case SkillLevel.E:
                return SkillLevel.D;
            case SkillLevel.D:
                return SkillLevel.C;
            case SkillLevel.C:
                return SkillLevel.B;
            case SkillLevel.B:
                return SkillLevel.A;
            case SkillLevel.A:
                return SkillLevel.S;
            default:
                //�ʏ�L�蓾�Ȃ���
                return SkillLevel.NONE;
        }
    }
}

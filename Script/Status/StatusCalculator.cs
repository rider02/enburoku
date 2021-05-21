using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210218 �o�t�A�f�o�t�A�����␳���̌v�Z���s���N���X
/// BattleCalculator�AStatusWindow����Ă΂��
/// </summary>
public class StatusCalculator
{
    public StatusDto GetBuffedStatus(StatusDto statusDto,string name, Weapon weapon, Accessory accessory, List<Skill> skills, bool isBerserk)
    {

        //����̕␳ StausType��NONE�ȊO�Ȃ牽������o�t���L�镐��
        if(weapon != null)
        {
            StatusType statusType = weapon.statusType;
            //������
            int amount = weapon.amount;

            //�N�\�����ŃX�e�[�^�X���グ��@�ǂ����@�������ȁE�E�E
            if(statusType == StatusType.HP)
            {
                statusDto.hp += amount;
            }
            else if (statusType == StatusType.LATK)
            {
                statusDto.latk += amount;
            }
            else if (statusType == StatusType.CATK)
            {
                statusDto.catk += amount;
            }
            else if (statusType == StatusType.AGI)
            {
                statusDto.agi += amount;
            }
            else if (statusType == StatusType.DEX)
            {
                statusDto.dex += amount;
            }
            else if (statusType == StatusType.LDEF)
            {
                statusDto.ldef += amount;
            }
            else if (statusType == StatusType.CDEF)
            {
                statusDto.cdef += amount;
            }
            else if (statusType == StatusType.LUK)
            {
                statusDto.luk += amount;
            }
            Debug.Log($"����̃X�e�[�^�X�␳ ���햼:{weapon.name} {statusType}+{amount}");
        }

        //�A�N�Z�T���̃o�t���f
        if (accessory != null)
        {
            //�㏸��
            int amount = accessory.amount;

            if (accessory.effect == AccessoryEffectType.LDEFUP)
            {
                
                statusDto.ldef += amount;
            }
            else if (accessory.effect == AccessoryEffectType.LATKUP)
            {
                statusDto.latk += amount;
            }
            else if (accessory.effect == AccessoryEffectType.AGIUP)
            {
                statusDto.agi += amount;
            }
            Debug.Log($"�����i�̃X�e�[�^�X�␳ �A�N�Z�T����:{accessory.name} {accessory.effect}+{amount}");
        }

        //TODO �X�L���␳
        //210226 �X�L���v�Z �����Ă�����ʃN���X�ɂ��Ă��ǂ��Ǝv��
        if (skills.Contains(Skill.�K�^))
        {
            statusDto.luk += 5;
            Debug.Log($"{name}�X�L���K�^ �K�^+5");
        }

        if (skills.Contains(Skill.HP5))
        {
            statusDto.hp += 5;
            Debug.Log($"{name}�X�L��{Skill.HP5} {Skill.HP5.GetStringValue()}");
        }

        //210226 �B�l�n�X�L�� ����͂ƂĂ��ȒP
        if (skills.Contains(Skill.�e���̒B�l) && weapon.type == WeaponType.SHOT) 
        {
            statusDto.latk += 5;
            statusDto.catk += 5;
            Debug.Log($"{name}�X�L��{Skill.�e���̒B�l} �������ߋ���+5");
        }
        else if (skills.Contains(Skill.���[�U�[�̒B�l) && weapon.type == WeaponType.LASER)
        {
            statusDto.latk += 5;
            statusDto.catk += 5;
            Debug.Log($"{name}�X�L��{Skill.���[�U�[�̒B�l} �������ߋ���+5");
        }
        else if (skills.Contains(Skill.����̒B�l) && weapon.type == WeaponType.STRIKE)
        {
            statusDto.latk += 5;
            statusDto.catk += 5;
            Debug.Log($"{name}�X�L��{Skill.����̒B�l} �������ߋ���+5");
        }

        if (skills.Contains(Skill.�Е����X))
        {
            Debug.Log($"{name}�X�L��{Skill.�Е����X} �K�^+4�AHP+4");
            statusDto.hp += 5;
            statusDto.luk += 5;
        }

        if (isBerserk)
        {
            Debug.Log($"{name}�X�L��{Skill.����}: {Skill.����.GetStringValue()}");
            statusDto.latk += 4;
            statusDto.catk += 4;
        }


        //TODO �o�t�A�f�o�t�␳

        //������������˔j���Ȃ��悤�ɐݒ�
        if (statusDto.hp >= StatusConst.HP_MAX)
        {
            statusDto.hp = StatusConst.HP_MAX;
        }
        if (statusDto.latk >= StatusConst.LATK_MAX)
        {
            statusDto.latk = StatusConst.LATK_MAX;
        }
        if (statusDto.catk >= StatusConst.CATK_MAX)
        {
            statusDto.catk = StatusConst.CATK_MAX;
        }
        if (statusDto.agi >= StatusConst.AGI_MAX)
        {
            statusDto.agi = StatusConst.AGI_MAX;
        }
        if (statusDto.dex >= StatusConst.DEX_MAX)
        {
            statusDto.dex = StatusConst.DEX_MAX;
        }
        if (statusDto.ldef >= StatusConst.LDEF_MAX)
        {
            statusDto.ldef = StatusConst.LDEF_MAX;
        }
        if (statusDto.cdef >= StatusConst.CDEF_MAX)
        {
            statusDto.cdef = StatusConst.CDEF_MAX;
        }
        if (statusDto.luk >= StatusConst.LUK_MAX)
        {
            statusDto.luk = StatusConst.LUK_MAX;
        }
        return statusDto;
    }

    //HP�o�t�̔��f�A�m�F�p
    public int CalcHpBuff(int hp, List<Skill> skills)
    {
        if (skills.Contains(Skill.�Е����X))
        {
            hp += 5;
        }

        if (skills.Contains(Skill.HP5))
        {
            hp += 5;
        }

        return hp;
    }

    //�ړ��̓A�b�v(���R)
    public int calcMove(Unit unit)
    {
        //movePlus�͕s�v�c�Ȍ��Ԃ��g�p����Ƒ�������
        int move = unit.job.move + unit.movePlus;
        //210226 �ړ��̓o�t


        return calcMoveCommon(move, unit.job.skills);
    }



    //�ړ��̓A�b�v(�G)
    public int calcMove(Enemy enemy)
    {
        //movePlus�͕s�v�c�Ȍ��Ԃ��g�p����Ƒ�������
        int move = enemy.job.move;
        //210226 �ړ��̓o�t


        return calcMoveCommon(move, enemy.job.skills);
    }

    public int calcMoveCommon(int move, List<Skill> skills)
    {
        if (skills.Contains(Skill.����))
        {
            move += 1;
            Debug.Log($" �X�L��{Skill.����} �ړ���+1");
        }
        return move;
    }
}

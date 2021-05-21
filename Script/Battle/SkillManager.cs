using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//210226�l��n����Ċe��X�L���̒��ł��������G�ȕ��̔�����s�����\�b�h
public class SkillManager
{

    //�u�N�W�Ɓv ����3�ȏ㏊�����Ă��鎞�A�K�E+10
    public bool isCollector(Unit unit)
    {
        if (unit.job.skills.Contains(Skill.�N�W��))
        {
            return isCollectorCommon(unit.carryItem);
        }
        return false;
    }

    public bool isCollector(Enemy enemy)
    {
        if (enemy.job.skills.Contains(Skill.�N�W��))
        {
            return isCollectorCommon(enemy.carryItem);
        }
        return false;
    }

    private bool isCollectorCommon(List<Item> carryItem)
    {
        List<Item> item = carryItem.Where(item => item.ItemType == ItemType.WEAPON).ToList();
        if (item.Count >= 3)
        {
            Debug.Log($"�X�L��{Skill.�N�W��} ���� �����Ă��镄�̐�:{item.Count}");
            return true;
        }
        else
        {
            Debug.Log($"�X�L��{Skill.�N�W��} �s�� �����Ă��镄�̐�:{item.Count}");
            return false;
        }
    }

    //�����e�i���X �K�^�~2�̊m���Ŕ���
    public bool isMaintenance(int luk, float ran)
    {
        if (ran <= luk * 2)
        {

            Debug.Log($"�X�L��{Skill.�����e�i���X} �ϋv�͏����");
            return true;
        }
        Debug.Log($"�X�L��{Skill.�����e�i���X} ���s");
        return false;
    }

    //�F�� �K�^�~1.5�̊m���Ŕ���
    public bool isPray(int luk , float ran)
    {
        if(ran <= luk * 1.5)
        {
            Debug.Log($"�X�L��{Skill.�F��} ����");
            return true;
        }
        Debug.Log($"�X�L��{Skill.�F��} ���s");
        return false;
    }

    //���͏��
    public bool isGuard(int dex , float ran)
    {
        if (ran <= dex * 2)
        {
            Debug.Log($"�X�L��{Skill.���͏��} ����");
            return true;
        }
        Debug.Log($"�X�L��{Skill.���͏��} ���s");
        return false;
    }

    //�^���\�m
    public bool isDestinyGuard(int luk, float ran)
    {
        if (ran <= luk)
        {
            Debug.Log($"�X�L��{Skill.�^���\�m} ����");
            return true;
        }
        Debug.Log($"�X�L��{Skill.�^���\�m} ���s");
        return false;
    }
}

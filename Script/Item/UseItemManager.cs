using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 210218 ���j�b�g�ƃA�C�e����n���ƃ��j�b�g�ɃA�C�e���̌��ʂ�^����N���X
/// </summary>
public class UseItemManager : MonoBehaviour
{
    private BattleMapManager battleMapManager;
    private EffectManager effectManager;
    private StatusManager statusManager;

    //�퓬�V�[���ƃX�e�[�^�X��ʂ̏o���������s��
    bool isBattleMap;

    //������
    public void Init(BattleMapManager battleMapManager, EffectManager effectManager)
    {
        this.battleMapManager = battleMapManager;
        this.effectManager = effectManager;
        isBattleMap = true;
    }

    //�X�e�[�^�X��ʂ���̎g�p
    public void Init(StatusManager statusManager)
    {
        this.statusManager = statusManager;
        isBattleMap = false;
    }

    //���j�b�g�ƃA�C�e����n���ƃ��j�b�g�ɃA�C�e���̌��ʂ�^����
    public void UseItem(PlayerModel playerModel, Potion potion)
    {
        Unit unit = playerModel.unit;
        //�񕜌n
        if(potion.potionType == PotionType.HEAL)
        {
            //������\��
            GameObject healText = Instantiate(effectManager.healText, playerModel.transform.position, Quaternion.identity) as GameObject;
            healText.GetComponent<DamageText>().Init(potion.amount.ToString());
            //�G�t�F�N�g�\��
            GameObject effect = Instantiate(effectManager.healEffectList[0], new Vector3(playerModel.transform.position.x, playerModel.transform.position.y + 1,
            playerModel.transform.position.z), Quaternion.identity) as GameObject;

            int maxHp = unit.maxhp + unit.job.statusDto.jobHp;
            StatusCalculator statusCalculator = new StatusCalculator();
            maxHp = statusCalculator.CalcHpBuff(maxHp, unit.job.skills);

            //210227 �X�L���u�悭������v���f
            if (unit.job.skills.Contains(Skill.�悭������))
            {
                unit.hp += potion.amount*2;
                Debug.Log($"{unit.name}: �X�L��{Skill.�悭������} ����{Skill.�悭������.GetStringValue()}");
            }
            else
            {
                unit.hp += potion.amount;
            }
            

            if(unit.hp >= maxHp)
            {
                unit.hp = maxHp;
            }

            Debug.Log($"{unit.name}��{potion.name}�ő̗͂�{potion.amount}�񕜂���");
            Debug.Log($"�񕜌��HP:{unit.hp}");
            
        }
        else if (potion.potionType == PotionType.STATUSUP)
        {
            //�X�e�[�^�X�A�b�v�n�̃A�C�e���������ꍇ
            string text = StatusUp(potion, unit);
            //���b�Z�[�W�E�B���h�E�\��
            battleMapManager.OpenMessageWindow(text);
            battleMapManager.SetMapMode(MapMode.MESSAGE);
        }

        //�g�p�񐔂����炷
        potion.useCount -= 1;
        //��ɂȂ����|�[�V�������폜���鏈��
        EmptyPotionDelete(playerModel.unit.carryItem);

        playerModel.isMoved = true;
        playerModel.SetBeforeAction(false);

        //���[�h��߂�
        battleMapManager.mapMode = MapMode.NORMAL;
        Debug.Log("MapMode : " + battleMapManager.mapMode);
    }

    //�X�e�[�^�X��ʂ���̓���g�p
    public string StatusUsePotion(Potion potion, Unit unit)
    {
        string text = StatusUp(potion, unit);
        potion.useCount -= 1;
        EmptyPotionDelete(unit.carryItem);

        return text;
    }

    /// <summary>
    /// 210219 ��ɂȂ����|�[�V�����폜
    /// ��A�̃|�[�V�����g�p�Ŕ���o����Ηǂ��������Apotion�����Item��������Ȃ������̂ō쐬
    /// </summary>
    public void EmptyPotionDelete(List<Item> carryItem)
    {
        //foreach���Ƀ��X�g��ύX�o���Ȃ��̂ŁAforeach�̊O�ō폜
        Item removeItem = null;

        //�A�C�e���̒��Ń|�[�V�������g�p�񐔂�0�ȉ��Ȃ�A�C�e������
        foreach(var item in carryItem)
        {
            if(item.ItemType == ItemType.POTION)
            {
                if(item.potion.useCount <= 0)
                {
                    removeItem = item;
                }
            }
        }
        if (removeItem != null)
        {
            carryItem.Remove(removeItem);
            Debug.Log($"{removeItem.ItemName}���폜���܂���");
        }


    }

    /// <summary>
    /// 210303 ���j�b�g�̃X�e�[�^�X�㏸ �퓬��ʂ����UseItem�A�X�e�[�^�X��ʂ���͂���𒼐ڌĂяo��
    /// </summary>
    /// <param name="potion"></param>
    /// <param name="unit"></param>
    public string StatusUp(Potion potion , Unit unit)
    {
        //TODO �����̖��^�[���㏸�l�����A�ǂ��������邩�l���邱��
        string text = "";
        //HP�A�b�v
        if (potion.name == "�d���̉ʎ�")
        {
            unit.hp += potion.amount;
            unit.maxhp += potion.amount;
            text = $"{unit.name}��HP���オ����";
        }
        else if (potion.name == "�S�̑���")
        {
            unit.catk += potion.amount;
            text = $"{unit.name}�̋ߍU���オ����";
            
        }
        else if (potion.name == "�����̖�����")
        {
            unit.latk += potion.amount;
            text = $"{unit.name}�̉��U���オ����";
            
        }
        else if (potion.name == "�͓��̋Z�p��")
        {
            unit.dex += potion.amount;
            text = $"{unit.name}�̋Z���オ����";
            
        }
        else if (potion.name == "�V���̉H�c��")
        {
            unit.agi += potion.amount;
            text = $"{unit.name}�̑������オ����";
            
        }
        else if (potion.name == "�ؒ���̕���")
        {
            unit.luk += potion.amount;
            text = $"{unit.name}�̉^���オ����";
            
        }
        else if (potion.name == "�V�l�̓�")
        {
            unit.cdef += potion.amount;
            text = $"{unit.name}�̋ߖh���オ����";
            
        }
        else if (potion.name == "���E�̌���")
        {
            unit.ldef += potion.amount;
            text = $"{unit.name}�̉��h���オ����";
        }
        else if (potion.name == "�s�v�c�Ȍ���")
        {
            unit.movePlus += potion.amount;
            text = $"{unit.name}�̈ړ����オ����";
        }
        Debug.Log(text);
        return (text);
    }
}

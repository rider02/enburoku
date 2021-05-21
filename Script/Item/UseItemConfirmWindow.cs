using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210218 ����̎g�p�m�F�E�B���h�E
/// �@�\�͖Œ��ꒃ���Ȃ����A�g�p����A�C�e����ێ������Ă����@�\����������
/// </summary>
public class UseItemConfirmWindow : MonoBehaviour
{
    //�g�p����A�C�e��
    private Potion potion;

    private BattleMapManager battleMapManager;
    public void Init(BattleMapManager battleMapManager, Potion potion)
    {
        this.battleMapManager = battleMapManager;

        //�g�p����A�C�e����ݒ�
        this.potion = potion;
    }

    //�N���b�N���ꂽ��
    public void Onclick()
    {
        battleMapManager.UsePotion(potion);
    }
}

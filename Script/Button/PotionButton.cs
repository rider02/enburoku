using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//210217 �}�b�v��X�e�[�^�X��ʂ̃A�C�e���{�^��
//���X�ɂ͕ʓr�l�i��\������ShopItemButton��݂���
public class PotionButton : MonoBehaviour
{
    [SerializeField]
    private Text potionNameText;
    [SerializeField]
    private Text enduranceText;
    [SerializeField]
    private Button button;

    private Potion potion;

    private BattleMapManager battleMapManager;

    private StatusManager statusManager;

    private Unit unit;

    //210222 �X�e�[�^�X��ʂŎg�p����ꍇ�̓Z���N�g���ɏڍׂ�؂�ւ���K�v�͂Ȃ���
    private bool isStatusScene;

    //�A�C�R���͌Œ肾���A������ȊO�̃A�C�R�����L��Ύ�������

    //�퓬�}�b�v�ł̕\���p
    public void Init(Potion potion, BattleMapManager battleMapManager, Unit unit)
    {
        this.potionNameText.text = potion.name;
        this.unit = unit;
        this.potion = potion;
        this.battleMapManager = battleMapManager;
        enduranceText.text = string.Format("{0}/{1}", potion.useCount, potion.maxUseCount);
    }

    //�X�e�[�^�X��ʂł̕\���p
    public void Init(Potion potion, StatusManager statusManager, Unit unit)
    {
        isStatusScene = true;
        this.potionNameText.text = potion.name;
        this.unit = unit;
        this.potion = potion;
        this.statusManager = statusManager;
        enduranceText.text = string.Format("{0}/{1}", potion.useCount, potion.maxUseCount);
    }

    public void Onclick()
    {
        //�m�F�E�B���h�E�\������
        battleMapManager.OpenUseItemConfirmWindow(potion);

        //�߂�{�^�������������̃t�H�[�J�X��ݒ�
        battleMapManager.selectedItem = this.gameObject;
    }

    public void OnSelect()
    {
        if (!isStatusScene)
        {
            //����̏ڍ׃E�B���h�E���X�V����
            battleMapManager.changePotionDetailWindow(potion);
        }
        
    }

    //�{�^����s�����ɂ��ĕ������O���[�A�E�g������
    public void setButtonDisinteractable()
    {
        button.interactable = false;
        potionNameText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
        enduranceText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
    }
}

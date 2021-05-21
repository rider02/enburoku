using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210217 �퓬�}�b�v�A�X�e�[�^�X��ʂŃA�C�e���̏ڍׂ�\������E�B���h�E
/// </summary>
public class ItemDetailWindow : MonoBehaviour
{
    [SerializeField]
    Text itemName;  //�A�C�e����

    [SerializeField]
    Text detailText;//����

    [SerializeField]
    Image icon;

    [SerializeField]
    Sprite[] iconList;

    /// <summary>
    /// �A�C�e����I�������ƃ{�^������Ă΂��
    /// </summary>
    public void UpdateText(Potion potion)
    {
        //����Ɣ�ׂ�Ɣ��ɒP��
        this.itemName.text = potion.name;
        this.detailText.text = potion.annotationText;
        icon.sprite = iconList[0];
    }

    public void UpdateText(Accessory accessory)
    {
        this.itemName.text = accessory.name;
        this.detailText.text = accessory.annotationText;
        icon.sprite = iconList[4];

    }

    public void UpdateText(Tool tool)
    {
        this.itemName.text = tool.name;
        this.detailText.text = tool.annotationText;

        //���A���A�N���X�`�F���W�A�C�e���ɂ���ăA�C�R����ύX
        if (tool.isClassChangeItem)
        {
            icon.sprite = iconList[3];
        }
        else if("���̌�" == tool.name || "��̌�" == tool.name) 
        {
            icon.sprite = iconList[2];
        }
        else
        {
            icon.sprite = iconList[1];
        }
    }

}

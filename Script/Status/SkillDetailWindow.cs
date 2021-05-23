using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210217 �퓬�}�b�v�A�X�e�[�^�X��ʂŃX�L���̏ڍׂ�\������E�B���h�E
/// </summary>
public class SkillDetailWindow : MonoBehaviour
{
    [SerializeField]
    Text skillName;  //�A�C�e����

    [SerializeField]
    Text detailText;//����

 
    //210225 
    public void UpdateText(Skill skill)
    {
        this.skillName.text = skill.ToString();
        this.detailText.text = skill.GetStringValue();
    }
}

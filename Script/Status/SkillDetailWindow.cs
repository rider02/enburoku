using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210217 戦闘マップ、ステータス画面でスキルの詳細を表示するウィンドウ
/// </summary>
public class SkillDetailWindow : MonoBehaviour
{
    [SerializeField]
    Text skillName;  //アイテム名

    [SerializeField]
    Text detailText;//説明

 
    //210225 
    public void UpdateText(Skill skill)
    {
        this.skillName.text = skill.ToString();
        this.detailText.text = skill.GetStringValue();
    }
}

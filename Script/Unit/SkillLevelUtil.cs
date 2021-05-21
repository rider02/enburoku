using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210218 スキルレベルアップの良い方法が無くて作らざるを得なかった
/// </summary>
public class SkillLevelUtil : MonoBehaviour
{
    /// <summary>
    /// 210218 スキルレベルの計算を行う
    /// </summary>
    /// <param name="unit"></param>
    public static string CalculateSkillLevel(Unit unit)
    {
        //武器を装備していない場合は経験値を得られない
        if (unit.equipWeapon == null)
        {
            Debug.Log($"武器を装備していないので技能経験値を得られません");
            return null;
        }

        if (unit.equipWeapon.type == WeaponType.SHOT)
        {
            //スキルレベルがSの場合は経験値も増えない NONEは一応
            if (unit.shotLevel == SkillLevel.NONE || unit.shotLevel == SkillLevel.S)
            {
                return null;
            }

            //経験値増加
            if (unit.job.skills.Contains(Skill.天才肌))
            {
                //210226 天才肌を実装
                unit.shotExp += 2;
                Debug.Log($"{unit.name}の{Skill.天才肌}効果でショット技能経験値+2");
            }
            else
            {
                unit.shotExp += 1;
                Debug.Log($"{unit.name}のショット技能経験値+1");
            }
            

            //SkillLevelクラスのIntValueは要求経験値なので、それ以上になればスキルレベルアップ
            if (unit.shotExp >= unit.shotLevel.GetIntValue())
            {
                //経験値をリセット
                unit.shotExp = 0;

                //レベルアップ 
                unit.shotLevel = SkillLevelUtil.SkillLvUp(unit.shotLevel);
                Debug.Log($"{unit.name}のショットの熟練度が{unit.shotLevel}に上がりました。");
                return $"ショットの熟練度が{unit.shotLevel}に上がりました。";
            }
            else
            {
                //特にレベルが上がらなかった時は何も返さない
                return null;
            }
        }
        else if (unit.equipWeapon.type == WeaponType.LASER)
        {
            if (unit.job.skills.Contains(Skill.天才肌))
            {
                unit.laserExp += 2;
                Debug.Log($"{unit.name}の{Skill.天才肌}効果でレーザー技能経験値+2");
            }
            else
            {
                unit.laserExp += 1;
                Debug.Log($"{unit.name}のレーザー技能経験値+1");
            }
            

            if (unit.laserExp >= unit.laserLevel.GetIntValue())
            {
                unit.laserExp = 0;
                unit.laserLevel = SkillLevelUtil.SkillLvUp(unit.laserLevel);
                Debug.Log($"{unit.name}のレーザーの熟練度が{unit.laserLevel}に上がりました。");
                return $"レーザーの熟練度が{unit.laserLevel}に上がりました。";
            }
            else
            {
                //特にレベルが上がらなかった時は何も返さない
                return null;
            }
        }
        else if (unit.equipWeapon.type == WeaponType.STRIKE)
        {
            if (unit.job.skills.Contains(Skill.天才肌))
            {
                unit.strikeExp += 2;
                Debug.Log($"{unit.name}の{Skill.天才肌}効果で物理技能経験値+2");
            }
            else
            {
                unit.strikeExp += 1;
                Debug.Log($"{unit.name}の物理技能経験値+1");
            }
            

            if (unit.strikeExp >= unit.strikeLevel.GetIntValue())
            {
                unit.strikeExp = 0;
                unit.strikeLevel = SkillLevelUtil.SkillLvUp(unit.strikeLevel);

                Debug.Log($"{unit.name}の物理の熟練度が{unit.strikeLevel}に上がりました。");
                return $"物理の熟練度が{unit.strikeLevel}に上がりました。";
            }
            else
            {
                //特にレベルが上がらなかった時は何も返さない
                return null;
            }
        }
        else if (unit.equipWeapon.type == WeaponType.HEAL)
        {
            if (unit.job.skills.Contains(Skill.天才肌))
            {
                unit.healExp += 2;
                Debug.Log($"{unit.name}の{Skill.天才肌}効果で癒符技能経験値+2");
            }
            else
            {
                unit.healExp += 1;
                Debug.Log($"{unit.name}の癒符技能経験値+1");
            }


            if (unit.healExp >= unit.laserLevel.GetIntValue())
            {
                unit.healExp = 0;
                unit.healLevel = SkillLevelUtil.SkillLvUp(unit.healLevel);
                Debug.Log($"{unit.name}の癒符の熟練度が{unit.healLevel}に上がりました。");
                return $"癒符の熟練度が{unit.healLevel}に上がりました。";
            }
            else
            {
                //特にレベルが上がらなかった時は何も返さない
                return null;
            }
        }
        else
        {
            //通常有り得ない 実装ミス
            return null;
        }
    }

    //回復の場合はequipWeaponを参照しないので個別で
    public static string CalculateHealSkillLevel(Unit unit)
    {
 
        //既にSなら上がらない
        if (unit.healLevel == SkillLevel.NONE || unit.healLevel == SkillLevel.S)
        {
            return null;
        }

        //経験値増加
        if (unit.job.skills.Contains(Skill.天才肌))
        {
            unit.strikeExp += 2;
            Debug.Log($"{unit.name}の{Skill.天才肌}効果で回復技能経験値+2");
        }
        else
        {
            unit.healExp += 1;
            Debug.Log($"{unit.name}の回復技能経験値+1");
        }
        

        //SkillLevelクラスのIntValueは要求経験値なので、それ以上になればスキルレベルアップ
        if (unit.healExp >= unit.healLevel.GetIntValue())
        {
            //経験値をリセット
            unit.healExp = 0;

            //レベルアップ 
            unit.healLevel = SkillLevelUtil.SkillLvUp(unit.healLevel);
            Debug.Log($"{unit.name}の回復の熟練度が{unit.healLevel}に上がりました。");
            return $"回復の熟練度が{unit.shotLevel}に上がりました。";
        }
        else
        {
            //特にレベルが上がらなかった時は何も返さない
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
                //通常有り得ないが
                return SkillLevel.NONE;
        }
    }
}

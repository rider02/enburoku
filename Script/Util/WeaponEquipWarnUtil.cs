using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210522 武器を装備出来るか確認して、出来なければ警告とエラー文言を返すクラス
/// ステータス画面、戦闘画面で呼ばれ、StatusItemButton、MapItemButtonで使用
/// </summary>
public static class WeaponEquipWarnUtil
{
    public static WeaponEquipWarn GetWeaponEquipWarn(Weapon weapon, Unit unit)
    {

        //210217 武器の熟練度判定実施用にユニットが持っている適正一覧を取得

        //1.専用武器の判定
        if (weapon.isPrivate)
        {
            //専用武器の場合、持ち主と使用者が一致しなければエラー
            if (weapon.ownerName != unit.name)
            {
                return WeaponEquipWarn.PRIVATE_WEAPON;
            }
        }

        //2.壊れているか判定
        if (weapon.endurance <= 0)
        {
            return WeaponEquipWarn.BROKEN;
        }

        //3.適正の判定 武騎手のスキルレベルがNONEだと装備不可
        //4.スキルレベルが足りていないと装備不可
        if (weapon.type == WeaponType.SHOT)
        {
            if (unit.shotLevel == SkillLevel.NONE)
            {
                return WeaponEquipWarn.SKILL_NONE;
            }
            else if (weapon.skillLevel.GetPriorityValue() > unit.shotLevel.GetPriorityValue())
            {
                //武器の要求レベルがスキルレベル以上ならエラー
                return WeaponEquipWarn.SKILL_LEVEL_NEED;
            }


        }
        else if (weapon.type == WeaponType.LASER)
        {
            if (unit.laserLevel == SkillLevel.NONE)
            {
                return WeaponEquipWarn.SKILL_NONE;
            }
            else if (weapon.skillLevel.GetPriorityValue() > unit.laserLevel.GetPriorityValue())
            {
                return WeaponEquipWarn.SKILL_LEVEL_NEED;
            }

        }
        else if (weapon.type == WeaponType.STRIKE)
        {
            if (unit.strikeLevel == SkillLevel.NONE)
            {
                return WeaponEquipWarn.SKILL_NONE;
            }
            else if (weapon.skillLevel.GetPriorityValue() > unit.strikeLevel.GetPriorityValue())
            {
                return WeaponEquipWarn.SKILL_LEVEL_NEED;
            }

        }
        else if (weapon.type == WeaponType.HEAL)
        {
            if (unit.healLevel == SkillLevel.NONE)
            {
                return WeaponEquipWarn.SKILL_NONE;
            }
            else if (weapon.skillLevel.GetPriorityValue() > unit.healLevel.GetPriorityValue())
            {
                return WeaponEquipWarn.SKILL_LEVEL_NEED;
            }
        }

        //エラーなし
        return WeaponEquipWarn.NONE;
    }
}

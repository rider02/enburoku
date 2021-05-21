using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210218 バフ、デバフ、装備補正等の計算を行うクラス
/// BattleCalculator、StatusWindowから呼ばれる
/// </summary>
public class StatusCalculator
{
    public StatusDto GetBuffedStatus(StatusDto statusDto,string name, Weapon weapon, Accessory accessory, List<Skill> skills, bool isBerserk)
    {

        //武器の補正 StausTypeがNONE以外なら何かしらバフが有る武器
        if(weapon != null)
        {
            StatusType statusType = weapon.statusType;
            //増加量
            int amount = weapon.amount;

            //クソ実装でステータスを上げる　良い方法無いかな・・・
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
            Debug.Log($"武器のステータス補正 武器名:{weapon.name} {statusType}+{amount}");
        }

        //アクセサリのバフ反映
        if (accessory != null)
        {
            //上昇量
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
            Debug.Log($"装飾品のステータス補正 アクセサリ名:{accessory.name} {accessory.effect}+{amount}");
        }

        //TODO スキル補正
        //210226 スキル計算 増えてきたら別クラスにしても良いと思う
        if (skills.Contains(Skill.幸運))
        {
            statusDto.luk += 5;
            Debug.Log($"{name}スキル幸運 幸運+5");
        }

        if (skills.Contains(Skill.HP5))
        {
            statusDto.hp += 5;
            Debug.Log($"{name}スキル{Skill.HP5} {Skill.HP5.GetStringValue()}");
        }

        //210226 達人系スキル これはとても簡単
        if (skills.Contains(Skill.弾幕の達人) && weapon.type == WeaponType.SHOT) 
        {
            statusDto.latk += 5;
            statusDto.catk += 5;
            Debug.Log($"{name}スキル{Skill.弾幕の達人} 遠距離近距離+5");
        }
        else if (skills.Contains(Skill.レーザーの達人) && weapon.type == WeaponType.LASER)
        {
            statusDto.latk += 5;
            statusDto.catk += 5;
            Debug.Log($"{name}スキル{Skill.レーザーの達人} 遠距離近距離+5");
        }
        else if (skills.Contains(Skill.武器の達人) && weapon.type == WeaponType.STRIKE)
        {
            statusDto.latk += 5;
            statusDto.catk += 5;
            Debug.Log($"{name}スキル{Skill.武器の達人} 遠距離近距離+5");
        }

        if (skills.Contains(Skill.威風堂々))
        {
            Debug.Log($"{name}スキル{Skill.威風堂々} 幸運+4、HP+4");
            statusDto.hp += 5;
            statusDto.luk += 5;
        }

        if (isBerserk)
        {
            Debug.Log($"{name}スキル{Skill.狂化}: {Skill.狂化.GetStringValue()}");
            statusDto.latk += 4;
            statusDto.catk += 4;
        }


        //TODO バフ、デバフ補正

        //ここから上限を突破しないように設定
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

    //HPバフの反映、確認用
    public int CalcHpBuff(int hp, List<Skill> skills)
    {
        if (skills.Contains(Skill.威風堂々))
        {
            hp += 5;
        }

        if (skills.Contains(Skill.HP5))
        {
            hp += 5;
        }

        return hp;
    }

    //移動力アップ(自軍)
    public int calcMove(Unit unit)
    {
        //movePlusは不思議な隙間を使用すると増加する
        int move = unit.job.move + unit.movePlus;
        //210226 移動力バフ


        return calcMoveCommon(move, unit.job.skills);
    }



    //移動力アップ(敵)
    public int calcMove(Enemy enemy)
    {
        //movePlusは不思議な隙間を使用すると増加する
        int move = enemy.job.move;
        //210226 移動力バフ


        return calcMoveCommon(move, enemy.job.skills);
    }

    public int calcMoveCommon(int move, List<Skill> skills)
    {
        if (skills.Contains(Skill.亜空穴))
        {
            move += 1;
            Debug.Log($" スキル{Skill.亜空穴} 移動力+1");
        }
        return move;
    }
}

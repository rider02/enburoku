using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//210226値を渡されて各種スキルの中でも少し複雑な物の判定を行うメソッド
public class SkillManager
{

    //「蒐集家」 符を3つ以上所持している時、必殺+10
    public bool isCollector(Unit unit)
    {
        if (unit.job.skills.Contains(Skill.蒐集家))
        {
            return isCollectorCommon(unit.carryItem);
        }
        return false;
    }

    public bool isCollector(Enemy enemy)
    {
        if (enemy.job.skills.Contains(Skill.蒐集家))
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
            Debug.Log($"スキル{Skill.蒐集家} 発動 持っている符の数:{item.Count}");
            return true;
        }
        else
        {
            Debug.Log($"スキル{Skill.蒐集家} 不発 持っている符の数:{item.Count}");
            return false;
        }
    }

    //メンテナンス 幸運×2の確率で発動
    public bool isMaintenance(int luk, float ran)
    {
        if (ran <= luk * 2)
        {

            Debug.Log($"スキル{Skill.メンテナンス} 耐久力消費無し");
            return true;
        }
        Debug.Log($"スキル{Skill.メンテナンス} 失敗");
        return false;
    }

    //祈り 幸運×1.5の確率で発動
    public bool isPray(int luk , float ran)
    {
        if(ran <= luk * 1.5)
        {
            Debug.Log($"スキル{Skill.祈り} 成功");
            return true;
        }
        Debug.Log($"スキル{Skill.祈り} 失敗");
        return false;
    }

    //魔力障壁
    public bool isGuard(int dex , float ran)
    {
        if (ran <= dex * 2)
        {
            Debug.Log($"スキル{Skill.魔力障壁} 成功");
            return true;
        }
        Debug.Log($"スキル{Skill.魔力障壁} 失敗");
        return false;
    }

    //運命予知
    public bool isDestinyGuard(int luk, float ran)
    {
        if (ran <= luk)
        {
            Debug.Log($"スキル{Skill.運命予知} 成功");
            return true;
        }
        Debug.Log($"スキル{Skill.運命予知} 失敗");
        return false;
    }
}

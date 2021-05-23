using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 210218 ユニットとアイテムを渡すとユニットにアイテムの効果を与えるクラス
/// </summary>
public class UseItemManager : MonoBehaviour
{
    private BattleMapManager battleMapManager;
    private EffectManager effectManager;
    private StatusManager statusManager;

    //戦闘シーンとステータス画面の出し分けを行う
    bool isBattleMap;

    //初期化
    public void Init(BattleMapManager battleMapManager, EffectManager effectManager)
    {
        this.battleMapManager = battleMapManager;
        this.effectManager = effectManager;
        isBattleMap = true;
    }

    //ステータス画面からの使用
    public void Init(StatusManager statusManager)
    {
        this.statusManager = statusManager;
        isBattleMap = false;
    }

    //ユニットとアイテムを渡すとユニットにアイテムの効果を与える
    public void UseItem(PlayerModel playerModel, Potion potion)
    {
        Unit unit = playerModel.unit;
        //回復系
        if(potion.potionType == PotionType.HEAL)
        {
            //数字を表示
            GameObject healText = Instantiate(effectManager.healText, playerModel.transform.position, Quaternion.identity) as GameObject;
            healText.GetComponent<DamageText>().Init(potion.amount.ToString());
            //エフェクト表示
            GameObject effect = Instantiate(effectManager.healEffectList[0], new Vector3(playerModel.transform.position.x, playerModel.transform.position.y + 1,
            playerModel.transform.position.z), Quaternion.identity) as GameObject;

            int maxHp = unit.maxhp + unit.job.statusDto.jobHp;
            StatusCalculator statusCalculator = new StatusCalculator();
            maxHp = statusCalculator.CalcHpBuff(maxHp, unit.job.skills);

            //210227 スキル「よくきく薬」反映
            if (unit.job.skills.Contains(Skill.よくきく薬))
            {
                unit.hp += potion.amount*2;
                Debug.Log($"{unit.name}: スキル{Skill.よくきく薬} 効果{Skill.よくきく薬.GetStringValue()}");
            }
            else
            {
                unit.hp += potion.amount;
            }
            

            if(unit.hp >= maxHp)
            {
                unit.hp = maxHp;
            }

            Debug.Log($"{unit.name}は{potion.name}で体力を{potion.amount}回復した");
            Debug.Log($"回復後のHP:{unit.hp}");
            
        }
        else if (potion.potionType == PotionType.STATUSUP)
        {
            //ステータスアップ系のアイテムだった場合
            string text = StatusUp(potion, unit);
            //メッセージウィンドウ表示
            battleMapManager.OpenMessageWindow(text);
            battleMapManager.SetMapMode(MapMode.MESSAGE);
        }

        //使用回数を減らす
        potion.useCount -= 1;
        //空になったポーションを削除する処理
        EmptyPotionDelete(playerModel.unit.carryItem);

        playerModel.isMoved = true;
        playerModel.SetBeforeAction(false);

        //モードを戻す
        battleMapManager.mapMode = MapMode.NORMAL;
        Debug.Log("MapMode : " + battleMapManager.mapMode);
    }

    //ステータス画面からの道具使用
    public string StatusUsePotion(Potion potion, Unit unit)
    {
        string text = StatusUp(potion, unit);
        potion.useCount -= 1;
        EmptyPotionDelete(unit.carryItem);

        return text;
    }

    /// <summary>
    /// 210219 空になったポーション削除
    /// 一連のポーション使用で判定出来れば良かったが、potionからはItemが分からなかったので作成
    /// </summary>
    public void EmptyPotionDelete(List<Item> carryItem)
    {
        //foreach中にリストを変更出来ないので、foreachの外で削除
        Item removeItem = null;

        //アイテムの中でポーションかつ使用回数が0以下ならアイテム消滅
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
            Debug.Log($"{removeItem.ItemName}を削除しました");
        }


    }

    /// <summary>
    /// 210303 ユニットのステータス上昇 戦闘画面からはUseItem、ステータス画面からはこれを直接呼び出す
    /// </summary>
    /// <param name="potion"></param>
    /// <param name="unit"></param>
    public string StatusUp(Potion potion , Unit unit)
    {
        //TODO 聖水の毎ターン上昇値減少、どう処理するか考えること
        string text = "";
        //HPアップ
        if (potion.name == "妖精の果実")
        {
            unit.hp += potion.amount;
            unit.maxhp += potion.amount;
            text = $"{unit.name}のHPが上がった";
        }
        else if (potion.name == "鬼の大吟醸")
        {
            unit.catk += potion.amount;
            text = $"{unit.name}の近攻が上がった";
            
        }
        else if (potion.name == "魔女の魔導書")
        {
            unit.latk += potion.amount;
            text = $"{unit.name}の遠攻が上がった";
            
        }
        else if (potion.name == "河童の技術書")
        {
            unit.dex += potion.amount;
            text = $"{unit.name}の技が上がった";
            
        }
        else if (potion.name == "天魔の羽団扇")
        {
            unit.agi += potion.amount;
            text = $"{unit.name}の速さが上がった";
            
        }
        else if (potion.name == "木彫りの仏像")
        {
            unit.luk += potion.amount;
            text = $"{unit.name}の運が上がった";
            
        }
        else if (potion.name == "天人の桃")
        {
            unit.cdef += potion.amount;
            text = $"{unit.name}の近防が上がった";
            
        }
        else if (potion.name == "結界の欠片")
        {
            unit.ldef += potion.amount;
            text = $"{unit.name}の遠防が上がった";
        }
        else if (potion.name == "不思議な隙間")
        {
            unit.movePlus += potion.amount;
            text = $"{unit.name}の移動が上がった";
        }
        Debug.Log(text);
        return (text);
    }
}

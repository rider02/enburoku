﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//210303 戦闘マップでアイテムを表示する用のボタン
public class MapItemButton : MonoBehaviour
{
    //武器名
    [SerializeField]
    private Text itemNameText;
    [SerializeField]
    private Text enduranceText;
    [SerializeField]
    private Image icon;//200825 武器のアイコンを設定
    [SerializeField]
    private Sprite[] iconList;
    [SerializeField]
    private GameObject equipMark;

    private BattleMapManager battleMapManager;

    private Unit unit;

    //交換の為、ボタンにアイテムのオブジェクトを持たせるしかない
    private Item item;

    private int index;

    private bool isEquipable;

    //武器を装備出来ない場合の警告を作成
    private WeaponEquipWarn warn;


    //装備しているか否か

    //装備武器ボタン用 いらないかも
    public void Init(Weapon weapon, BattleMapManager battleMapManager, Unit unit)
    {
        //装備している武器なので装備マークは表示しない
        equipMark.SetActive(false);

        this.battleMapManager = battleMapManager;
        itemNameText.text = weapon.name;
        this.unit = unit;
        enduranceText.text = string.Format("{0}/{1}", weapon.endurance.ToString(), weapon.maxEndurance.ToString());

        Item item = new Item(weapon);
        this.item = item;


        //200825 武器の種類によってアイコンを読み込む
        if (weapon.type == WeaponType.SHOT)
        {
            icon.sprite = iconList[0];
        }
        else if (weapon.type == WeaponType.LASER)
        {
            icon.sprite = iconList[1];
        }
        else if (weapon.type == WeaponType.STRIKE)
        {
            icon.sprite = iconList[2];
        }
        else if (weapon.type == WeaponType.HEAL)
        {
            icon.sprite = iconList[3];
        }
    }

    //装備アクセサリ用 いらないかも
    public void Init(Accessory accessory, BattleMapManager battleMapManager, Unit unit)
    {
        //装備している武器なので装備マークは表示しない
        equipMark.SetActive(false);

        itemNameText.text = accessory.name;
        this.unit = unit;
        this.battleMapManager = battleMapManager;

        Item item = new Item(accessory);
        this.item = item;

        //耐久は無いので非表示に
        enduranceText.enabled = false;
        icon.sprite = iconList[8];
    }

    //初期化メソッド unitはユニット手持ちのアイテム一覧などを表示する時に使用
    public void Init(Item item, BattleMapManager battleMapManager, Unit unit, int index)
    {
        this.battleMapManager = battleMapManager;
        this.unit = unit;
        this.item = item;
        this.index = index;
        icon.gameObject.SetActive(true);
        if (item.ItemType == ItemType.WEAPON)
        {
            Weapon weapon = item.weapon;
            //配下の名前、回数、値段を設定
            itemNameText.text = weapon.name;
            enduranceText.text = string.Format("{0}/{1}", weapon.endurance.ToString(), weapon.maxEndurance.ToString());

            //200825 武器の種類によってアイコンを読み込む
            if (weapon.type == WeaponType.SHOT)
            {
                icon.sprite = iconList[0];
            }
            else if (weapon.type == WeaponType.LASER)
            {
                icon.sprite = iconList[1];
            }
            else if (weapon.type == WeaponType.STRIKE)
            {
                icon.sprite = iconList[2];
            }
            else if (weapon.type == WeaponType.HEAL)
            {
                icon.sprite = iconList[3];
                //回復の杖は使ったり装備出来ないので文字を灰色に
                itemNameText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
                enduranceText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
            }

            //装備出来るかどうか確認して出来ない場合はグレーアウト
            if (WeaponEquipWarn.NONE != getWeaponEquipWarn(weapon, unit))
            {
                isEquipable = false;
                warn = getWeaponEquipWarn(weapon, unit);
                itemNameText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
                enduranceText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
            }
            else
            {
                isEquipable = true;
            }

        }
        else if (item.ItemType == ItemType.POTION)
        {
            Potion potion = item.potion;
            itemNameText.text = potion.name;
            enduranceText.text = string.Format("{0}/{1}", potion.useCount.ToString(), potion.maxUseCount.ToString());

            icon.sprite = iconList[4];
        }
        else if (item.ItemType == ItemType.ACCESSORY)
        {
            Accessory accessory = item.accessory;
            itemNameText.text = accessory.name;

            //耐久は無いので非表示に
            enduranceText.enabled = false;
            icon.sprite = iconList[8];
        }
        else if (item.ItemType == ItemType.TOOL)
        {
            Tool tool = item.tool;
            itemNameText.text = tool.name;

            //金塊
            if (tool.name == "金塊" || tool.name == "大きな金塊" || tool.name == "巨大な金塊")
            {
                enduranceText.text = "";
                icon.sprite = iconList[6];
                //文字を灰色に
                itemNameText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
                enduranceText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
            }
            else if (tool.isClassChangeItem)
            {
                enduranceText.text = "1/1";
                icon.sprite = iconList[7];

                //マップでは使用できないので文字を灰色に
                itemNameText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
                enduranceText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
            }
            else
            {
                //これは鍵など
                enduranceText.text = "1/1";
                icon.sprite = iconList[5];

                //鍵などはステータス画面では使えないので文字を灰色に
                itemNameText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
                enduranceText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
            }
        }

        //装備しているアイテムならマークを表示
        if (item.isEquip)
        {
            equipMark.SetActive(true);
        }
        else
        {
            equipMark.SetActive(false);
        }


    }



    //空のボタン初期化メソッド 戦闘マップでは通常は不要だが、ステータス一覧では使うかも・・・
    public void InitEmptyButton(BattleMapManager battleMapManager, Unit unit,int index)
    {
        //配下の名前、回数、値段を設定
        itemNameText.text = "";
        enduranceText.text = "";
        icon.gameObject.SetActive(false);
        
        this.item = null;
        this.battleMapManager = battleMapManager;
        this.unit = unit;
        this.index = index;

    }

    public void Onclick()
    {
        //ステータス用とは全然違う
        battleMapManager.OpenItemUseEquipWindow(unit, item, this.transform, index, warn);

    }

    public void OnSelect()
    {

        battleMapManager.changeItemDetailWindow(item);
        /*if(statusManager.menuMode == MenuMode.STATUS_BROWSE)
        {
            //ステータス表示時、装備している武器、アクセサリはItemを持っていない
            //ステータスウィンドウ用のDetailWindowが別途存在する
            statusManager.changeStatusItemDetailWindow(item, transform);
            

        }
        else
        {
            statusManager.changeItemDetailWindow(item);
        }*/


    }

    public void EquipItem()
    {
        //マーク表示
        equipMark.SetActive(true);

        //武器かアクセサリなら装備可能
        if(item.ItemType == ItemType.WEAPON || item.ItemType == ItemType.ACCESSORY)
        {
            item.isEquip = true;
        }
    }

    public void RemoveItem()
    {
        //マーク表示
        equipMark.SetActive(false);

        //武器かアクセサリなら装備可能
        if (item.ItemType == ItemType.WEAPON || item.ItemType == ItemType.ACCESSORY)
        {
            item.isEquip = false;
        }
    }

    /// <summary>
    /// 210224 BattleManagerから取ってきたが、staticメソッド等でまとめた方が良い
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    private WeaponEquipWarn getWeaponEquipWarn(Weapon weapon, Unit unit)
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
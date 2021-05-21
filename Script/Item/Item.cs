using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 手持ちのアイテムのラッパークラス
/// 武器、装飾品、薬、道具等を格納する
/// 210219 耐久力管理の為、武器を保持させる
/// </summary>
[System.Serializable]
public class Item
{

    //アイテムの種類 武器、装飾品、薬、道具、使用不可
    public ItemType ItemType;

    public string ItemName;

    //武器
    public Weapon weapon;

    public Accessory accessory;

    public Potion potion;

    public Tool tool;

    public bool isEquip;

    //public Tool tool { get; set; }

    //売却出来るか このフラグ、武器等にそのまま持たせた方が良いか
    public bool isNotForSale;

    //武器タイプで初期化
    public Item(Weapon weapon)
    {
        ItemType = ItemType.WEAPON;
        this.ItemName = weapon.name;
        this.weapon = weapon;
        this.isNotForSale = weapon.isNfs;
        isEquip = false;
    }

    public Item(Accessory accessory)
    {
        ItemType = ItemType.ACCESSORY;
        this.ItemName = accessory.name;
        this.accessory = accessory;
        this.isNotForSale = accessory.isNfs;
        isEquip = false;
    }

    //道具として使用出来る物
    public Item(Potion potion)
    {
        ItemType = ItemType.POTION;
        this.ItemName = potion.name;
        this.potion = potion;
        isEquip = false;
    }

    //金塊、鍵、クラスチェンジアイテムなど
    public Item(Tool tool)
    {
        ItemType = ItemType.TOOL;
        this.ItemName = tool.name;
        this.tool = tool;
        isEquip = false;
    }

    //装備している否かを設定
    public void Equip(bool isEquip)
    {
        this.isEquip = isEquip;
    }

}



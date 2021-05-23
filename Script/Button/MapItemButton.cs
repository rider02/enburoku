using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//210303 戦闘マップでアイテムを表示する用のボタン
public class MapItemButton : MonoBehaviour
{
    
    [SerializeField] private Text itemNameText;     //武器名
    [SerializeField] private Text enduranceText;    //使用回数
    [SerializeField] private Image icon;            //200825 武器のアイコン
    [SerializeField] private Sprite[] iconList;     //各アイコン ボタンに保持させておく
    [SerializeField] private GameObject equipMark;  //装備マーク

    private BattleMapManager battleMapManager;

    private Unit unit;  //持ち主 ユニット手持ちのアイテム一覧などを表示する時に使用
    private Item item;  //交換の為、ボタンにアイテムのオブジェクトを持たせる
    private int index;
    private bool isEquipable;   //装備出来るか

    //武器を装備出来ない場合の警告を作成
    private WeaponEquipWarn warn;


    //装備武器ボタン用
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

    //装備アクセサリ用
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

    //初期化メソッド 
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
            if (WeaponEquipWarn.NONE != WeaponEquipWarnUtil.GetWeaponEquipWarn(weapon, unit))
            {
                isEquipable = false;
                warn = WeaponEquipWarnUtil.GetWeaponEquipWarn(weapon, unit);
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

        //装備しているアイテムなら装備マークを表示
        if (item.isEquip)
        {
            equipMark.SetActive(true);
        }
        else
        {
            equipMark.SetActive(false);
        }
    }



    //空のボタン初期化メソッド
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

    //クリック時 「使う」、「装備」、装備不可の警告等を出す
    public void Onclick()
    {
        
        battleMapManager.OpenItemUseEquipWindow(unit, item, this.transform, index, warn);

    }

    public void OnSelect()
    {

        battleMapManager.changeItemDetailWindow(item);
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
}
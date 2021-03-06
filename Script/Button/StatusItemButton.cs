using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 200726 ステータス画面にアイテムを表示する用のボタン
/// ステータス画面、戦闘画面の２シーン共通で使用
/// </summary>
public class StatusItemButton : MonoBehaviour
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

    private StatusManager statusManager;

    private BattleMapManager battleMapManager;

    private Unit unit;

    //交換の為、ボタンにアイテムのオブジェクトを持たせるしかない
    private Item item;

    private int index;

    private bool isEquipable;


    //制御するクラスへの参照 ステータス画面、戦闘マップ画面の基本的にどちらか片方しか存在しない
    public void Init(StatusManager statusManager, BattleMapManager battleMapManager)
    {
        this.statusManager = statusManager;
        this.battleMapManager = battleMapManager;
    }

    //ステータス画面の装備武器ボタン用 押しても何も起こらない
    public void InitWeaponButton(Weapon weapon)
    {
        //装備している武器なので装備マークは表示しない
        equipMark.SetActive(false);

        itemNameText.text = weapon.name;
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

    //ステータス画面の装備アクセサリ用
    public void InitAccessoryButton(Accessory accessory)
    {
        //装備している武器なので装備マークは表示しない
        equipMark.SetActive(false);

        itemNameText.text = accessory.name;

        Item item = new Item(accessory);
        this.item = item;

        //耐久は無いので非表示に
        enduranceText.enabled = false;
        icon.sprite = iconList[8];
    }

    //初期化メソッド unitはユニット手持ちのアイテム一覧などを表示する時に使用
    public void Init(Item item, StatusManager statusManager, Unit unit, int index)
    {
        this.statusManager = statusManager;
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
            }

            //隙間に入れた場合、Unitはnullで初期化されているのでグレーアウトは発生しない
            if(unit != null)
            {
                //装備出来るかどうか確認して出来ない場合はグレーアウト
                if (WeaponEquipWarn.NONE != WeaponEquipWarnUtil.GetWeaponEquipWarn(weapon, unit))
                {
                    isEquipable = false;
                    itemNameText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
                    enduranceText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
                }
                else
                {
                    isEquipable = true;
                }
            }
            

        }
        else if (item.ItemType == ItemType.POTION)
        {
            Potion potion = item.potion;
            itemNameText.text = potion.name;
            enduranceText.text = string.Format("{0}/{1}", potion.useCount.ToString(), potion.maxUseCount.ToString());

            //ステータスアップアイテムは使用可能だが、それ以外は文字をグレーアウトさせる
            if(potion.potionType != PotionType.STATUSUP)
            {
                //文字を灰色に
                itemNameText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
                enduranceText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
            }

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

                //これはステータス画面で使用可能
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

    //空のボタン初期化メソッド 手持ちアイテムが6個に満たない場合は表示
    public void InitEmptyButton(StatusManager statusManager, Unit unit,int index)
    {
        //配下の名前、回数、値段を設定
        itemNameText.text = "";
        enduranceText.text = "";
        icon.gameObject.SetActive(false);
        
        this.item = null;
        this.statusManager = statusManager;
        this.unit = unit;
        this.index = index;

    }

    //クリック時 モードによりアイテム交換、交換アイテム選択、アイテムを預ける、取り出す等
    public void Onclick()
    {

        //210222 アイテム交換機能を実装
        //「装備」「捨てる」とかはまだ先
        if(statusManager.menuMode == MenuMode.ITEM_EXCHANGE)
        {
            //自身を非活性に
            this.GetComponent<Button>().interactable = false;
            //アイテム交換のフォーカスを変更
            statusManager.PrepareItemExchange(item, index, unit);
        }
        else if (statusManager.menuMode == MenuMode.EXCHANGE_ITEM_SELECT)
        {
            
            //既にアイテム1つ目を選択しており、2個めのアイテムを選択したので交換実行
            statusManager.ItemExchange(item, index, unit);
        }
        else if (statusManager.menuMode == MenuMode.ITEM)
        {
            //ここからは交換ではない場合 アイテムを預けたり装備する所
            statusManager.OpenItemDepositWindow(unit, item, this.transform, index, isEquipable);
        }
        else if (statusManager.menuMode == MenuMode.RECEIVE_ITEM)
        {
            //隙間からアイテムを取り出す時
            statusManager.ReceiveItem(item);
        }
        
    }

    public void OnSelect()
    {
        //210522 TODO 戦闘シーンで選択した場合の処理を追加
        if(statusManager == null)
        {
            return;
        }

        if(statusManager.menuMode == MenuMode.STATUS_BROWSE)
        {
            //ステータス表示時、装備している武器、アクセサリはItemを持っていない
            //ステータスウィンドウ用のDetailWindowが別途存在する
            statusManager.changeStatusItemDetailWindow(item, transform);
            

        }
        else
        {
            statusManager.changeItemDetailWindow(item);
        }
        
        
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
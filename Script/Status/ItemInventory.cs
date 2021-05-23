using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 倉庫(スキマ)のアイテムリスト
/// </summary>
public class ItemInventory : MonoBehaviour
{

    //武器一覧のアセットファイル
    [SerializeField]
    WeaponDatabase weaponDatabase;

    //倉庫(スキマ)
    public static List<Item> itemList { get; private set; }

    public static bool isInventoryInit { get; set; }

    //初期化
    public void Init()
    {
        itemList = new List<Item>();
        isInventoryInit = true;
    }

    //アイテムリストに追加
    public void AddItem(Item item)
    {
        Debug.Log("Inventory add" + item.ItemName);
        itemList.Add(item);
    }

    //アイテムリストから武器一覧のみ取得して返すメソッド 売却に使用する
    public List<Weapon> CreateWeaponList(){

        var inventoryWeaponList = new List<Weapon>();

        foreach (Item item in itemList){
            //タイプが武器のアイテムなら
            if(item.ItemType == ItemType.WEAPON)
            {
                //名前から武器を取得
                Weapon weapon = weaponDatabase.FindByName(item.ItemName);
                if(weapon != null)
                {
                    inventoryWeaponList.Add(weapon);
                }
            }
        }

        return inventoryWeaponList;
    }
}

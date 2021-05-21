using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//購入確認ウィンドウ
public class ConfirmWindow : MonoBehaviour
{
    [SerializeField]
    Text weaponName;

    [SerializeField]
    Text endurance;

    [SerializeField]
    Text price;

    ShopManager shopManager;

    public void init(ShopManager shopManager)
    {
        this.shopManager = shopManager;
    }

    /// <summary>
    /// 武器のボタンを押された時にShopManagerから呼ぶ
    /// </summary>
    /// <param name="weapon"></param>
    public void UpdateText(Weapon weapon)
    {

        this.weaponName.text = weapon.name;
        this.endurance.text = string.Format("{0}/{1}", weapon.endurance.ToString(), weapon.endurance.ToString());
        this.price.text = weapon.price.ToString();
    }

    public void buy()
    {
        //ウィンドウに表示している金額で購入処理
        shopManager.Buy(weaponName.text,int.Parse(price.text));
        //購入し終わったらウィンドウを閉じる
        shopManager.CloseConfirmWindow();
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 買う、売る、修理の機能を持つボタン
/// </summary>
public class WeaponButton : MonoBehaviour
{
    //武器名
    [SerializeField]
    private Text weaponNameText;
    [SerializeField]
    private Text priceText;
    [SerializeField]
    private Text enduranceText;

    private ShopManager shopManager;

    private DetailWindow detailWindow;

    public Weapon Weapon { get; private set; }

    

    public int NodeNumber { get { return transform.GetSiblingIndex(); } }

    //初期化メソッド
    public void Init(Weapon weapon, ShopManager shopManager,DetailWindow detailWindow)
    {
        //配下の名前、回数、値段を設定
        weaponNameText.text = weapon.name;
        enduranceText.text = string.Format("{0}/{1}", weapon.endurance.ToString(), weapon.endurance.ToString());

        priceText.text = string.Format("{0}円", weapon.price.ToString());

        //選択時の詳細表示用に持たせておく IDからアセットの値を取得した方が良い？
        this.Weapon = weapon;

        //Prefubは自分配下以外は[SerializeField]出来ないので、
        //初期化時にインスタンスを渡して貰う
        this.shopManager = shopManager;
        this.detailWindow = detailWindow;

    }

    //クリックされた時
    public void Onclick()
    {
        //menuModeによってボタンの機能を変える
        //買い物
        if (shopManager.shopMode == ShopMode.BUY)
        {
            //確認ウィンドウを開く
            shopManager.OpenConfirmWindow(Weapon);
        }
        else if (shopManager.shopMode == ShopMode.SALE)
        {
            //売る
        }
        else if (shopManager.shopMode == ShopMode.REPAIR)
        {
            //修理
        }


    }

    //選択された時に実行
    public void OnSelect()
    {
        detailWindow.UpdateText(Weapon);
    }

    
}

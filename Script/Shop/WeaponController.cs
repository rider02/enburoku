using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210522 お店の武器一覧制御用クラス
/// TODO ShopManagerに統合しても良いのでは？
/// </summary>
public class WeaponController : MonoBehaviour
{

    private GameObject buyWindow;
    private GameObject content;

    [SerializeField] private GameObject saleContent;


    /// <summary>
    /// 武器のボタン一覧を作る初期化メソッド
    /// </summary>
    /// <param name="shopManager"></param>
    public void initWeaponList(ShopManager shopManager, WeaponDatabase weaponDatabase,
        DetailWindow detailWindow )
    {
        foreach (var weapon in weaponDatabase.weaponList)
        {
            //非売品は表示しない
            if (weapon.isNfs)
            {
                continue;
            }
            //Resources配下からボタンをロード
            var itemButton = (Instantiate(Resources.Load("Prefabs/WeaponButton")) as GameObject).transform;
            //ボタン初期化 今はテキストのみ
            itemButton.GetComponent<WeaponButton>().Init(weapon, shopManager,detailWindow);
            itemButton.name = itemButton.name.Replace("(Clone)", "");

            //partyWindowオブジェクトをを探して取得
            buyWindow = GameObject.Find("BuyWindow");
            content = GameObject.Find("Viewport/Content");
            



            //partyWindowオブジェクト配下にprefab作成
            itemButton.transform.SetParent(content.transform);


        }
        //非表示にする
        buyWindow.SetActive(false);

    }

    //文字からアイテムを取得して返すメソッドを作る


    /// <summary>
    /// 売却用のウィンドウを手持ち武器から初期化する
    /// </summary>
    /// <param name="shopManager"></param>
    /// <param name="weaponDatabase"></param>
    /// <param name="detailWindow"></param>
    public void initSaleWeaponList(ShopManager shopManager, List<Weapon> weaponList,
        DetailWindow detailWindow)
    {
        foreach (var weapon in weaponList)
        {

            //Resources配下からボタンをロード
            var itemButton = (Instantiate(Resources.Load("Prefabs/WeaponButton")) as GameObject).transform;
            //ボタン初期化 今はテキストのみ
            itemButton.GetComponent<WeaponButton>().Init(weapon, shopManager, detailWindow);
            itemButton.name = itemButton.name.Replace("(Clone)", "");

            //partyWindowオブジェクト配下にprefab作成
            itemButton.transform.SetParent(saleContent.transform);


        }
    }
}

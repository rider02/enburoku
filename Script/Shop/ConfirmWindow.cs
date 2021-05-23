using UnityEngine;
using UnityEngine.UI;

//お店の購入確認ウィンドウUI制御クラス
public class ConfirmWindow : MonoBehaviour
{
    [SerializeField] Text weaponName;   //武器名
    [SerializeField] Text endurance;    //使用回数
    [SerializeField] Text price;        //値段

    ShopManager shopManager;

    //初期化メソッド
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

    //購入
    public void buy()
    {
        //ウィンドウに表示している金額で購入処理
        shopManager.Buy(weaponName.text,int.Parse(price.text));
        //購入し終わったらウィンドウを閉じる
        shopManager.CloseConfirmWindow();
    }

}


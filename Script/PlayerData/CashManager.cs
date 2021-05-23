using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210522 お金を管理するクラス
/// </summary>
public class CashManager : MonoBehaviour
{

    //金額を表示するUI
    [SerializeField] private Text cashTextNum;

    //お金
    public static int cash { get; set; }

    //初期化フラグ
    public static bool isCashInit { get; set; }

    //初期化
    public void Init()
    {
        cash = 10000;
        isCashInit = true;
    }


    //UI更新
    public void UpdateText()
    {
        cashTextNum.text = string.Format("{0}円", cash.ToString());
    }

    /// <summary>
    /// 購入処理
    /// </summary>
    /// <param name="price"></param>
    public void Buy(int price)
    {
        //通常処理は入らないが、もし代金が足りない場合は何もしない
        if((cash - price) < 0)
        {
            Debug.Log($"お金が不足しています 所持金:{cash}, 代金:{price}");
            return;
        }

        //所持金を支払う
        cash -= price;
        Debug.Log("購入" + price.ToString());

        UpdateText();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class CashManager : MonoBehaviour
{

    //金額
    [SerializeField]
    private Text cashTextNum;
    public static int cash { get; set; }

    public static bool isCashInit { get; set; }

    //初期化
    public void Init()
    {
        cash = 10000;
        isCashInit = true;
    }



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
        //マイナスになる場合は何もしない
        if((cash - price) < 0)
        {
            return;
        }

        Debug.Log("buy" + price.ToString());
        cash -= price;

        UpdateText();
    }

}

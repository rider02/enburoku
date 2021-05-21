using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210217 戦闘マップ、ステータス画面でアイテムの詳細を表示するウィンドウ
/// </summary>
public class ItemDetailWindow : MonoBehaviour
{
    [SerializeField]
    Text itemName;  //アイテム名

    [SerializeField]
    Text detailText;//説明

    [SerializeField]
    Image icon;

    [SerializeField]
    Sprite[] iconList;

    /// <summary>
    /// アイテムを選択されるとボタンから呼ばれる
    /// </summary>
    public void UpdateText(Potion potion)
    {
        //武器と比べると非常に単純
        this.itemName.text = potion.name;
        this.detailText.text = potion.annotationText;
        icon.sprite = iconList[0];
    }

    public void UpdateText(Accessory accessory)
    {
        this.itemName.text = accessory.name;
        this.detailText.text = accessory.annotationText;
        icon.sprite = iconList[4];

    }

    public void UpdateText(Tool tool)
    {
        this.itemName.text = tool.name;
        this.detailText.text = tool.annotationText;

        //鍵、金、クラスチェンジアイテムによってアイコンを変更
        if (tool.isClassChangeItem)
        {
            icon.sprite = iconList[3];
        }
        else if("扉の鍵" == tool.name || "宝の鍵" == tool.name) 
        {
            icon.sprite = iconList[2];
        }
        else
        {
            icon.sprite = iconList[1];
        }
    }

}

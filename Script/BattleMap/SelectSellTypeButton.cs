using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 入力するセルを選択するボタン
/// </summary>
public class SelectSellTypeButton : MonoBehaviour
{
    CellType cellType;

    MapEditManager mapEditManager;

    [SerializeField] Text buttonText;

    public void Init(CellType cellType, MapEditManager mapEditManager)
    {

        this.cellType = cellType;
        this.mapEditManager = mapEditManager;

        //EnumのStringValueからボタンの文字を設定
        buttonText.text = cellType.GetStringValue();
    }

    public void Onclick()
    {
        Debug.Log("入力するセルの種類を変更 : " + cellType.ToString());
        //入力するセルの種類を変更
        mapEditManager.SetInputCellType(cellType);
    }

}

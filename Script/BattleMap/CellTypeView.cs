using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// セルの種類を表示するクラス
/// </summary>

public class CellTypeView : MonoBehaviour
{
    [SerializeField] Text avoidRate;//回避率
    [SerializeField] Text defense;//防御力
    [SerializeField] Text cellName;//セルの名前
    [SerializeField] Text unmovable;//移動不可

    public void UpdateText(Main_Cell cell)
    {
        //名前
        cellName.text = string.Format("{0}", cell.TypeName);

        //210302 移動不可のセルを追加
        if (!cell.isBlock)
        {
            unmovable.enabled = false;

            avoidRate.enabled = true;
            defense.enabled = true;
            //回避率
            avoidRate.text = string.Format("回避率+{0}%", cell.AvoidRate);
            //防御力
            defense.text = string.Format("防御+{0}", cell.Defence);
        }
        else
        {
            //移動不可の表記をする
            avoidRate.enabled = false;
            defense.enabled = false;
            unmovable.enabled = true;
        }
        
        
    }

    //セルに宝箱がある場合は専用の表示とする
    public void TreasureUpdateText()
    {
        //名前
        cellName.text = "宝箱";

        avoidRate.enabled = false;
        defense.enabled = false;
        unmovable.enabled = true;
    }

}

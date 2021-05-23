using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 210521 レベルアップ時のキャラの感想を表示するUI制御用メソッド
/// </summary>
public class LvupImpreWindow : MonoBehaviour
{
    [SerializeField]
    Text lvupImpreText;     //感想テキスト

    [SerializeField]
    Image unitImage;        //ユニットの顔画像

    //テキスト更新
    public void UpdateText(string lvupImpreText)
    {
        this.lvupImpreText.text = lvupImpreText;
    }

    //画像更新
    public void UpdateImage(Sprite sprite)
    {
        this.unitImage.sprite = sprite;
    }
}

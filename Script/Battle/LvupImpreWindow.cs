using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// レベルアップ時のキャラの感想を表示するだけのメソッド
/// </summary>
public class LvupImpreWindow : MonoBehaviour
{
    [SerializeField]
    Text lvupImpreText;

    [SerializeField]
    Image unitImage;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 座標を表示するウィンドウ デバッグ用？
/// </summary>
public class CoordinateWindow : MonoBehaviour
{

    [SerializeField] Text x;
    [SerializeField] Text y;

    public void UpdateText(int x, int y)
    {
        this.x.text = x.ToString();
        this.y.text = y.ToString();

    }

}

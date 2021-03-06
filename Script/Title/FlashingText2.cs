using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashingText2 : MonoBehaviour
{
    private float num = Mathf.PI;
    // Update is called once per frame
    void Update()
    {
        /*
         * TextMeshProの機能を使うためにTextMeshProUGUIをオブジェクトから取ってくる
         */
        TextMeshProUGUI tmPro = gameObject.GetComponent<TextMeshProUGUI>();
        Material material = tmPro.fontMaterial;
        /*
        *----------------------------------------------------------- 
        * OutlineのThicknessの数値を0～0.4に変化するように設定
        * 数値の変化は三角関数のSinを利用
        * 数値が負の値になるとおかしくなるので、絶対値を設定
        * 分母を大きくすると太さの最大値が減る
        *-----------------------------------------------------------
        */
        material.SetFloat("_OutlineWidth", Mathf.Abs(Mathf.Sin(num)) * 2 / 7);
        num += Time.deltaTime;
    }
}
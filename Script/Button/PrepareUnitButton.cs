using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 200808 出撃準備用のユニットボタン
/// </summary>
public class PrepareUnitButton : MonoBehaviour
{

    //ボタン配下のテキスト
    [SerializeField]
    private Text buttonText;
    [SerializeField]
    private Text entryText;

    public string unitName;

    private BattleMapManager battleMapManager;

    private bool isDisable;

    //初期化メソッド
    public void Init(string unitName, BattleMapManager battleMapManager)
    {
        //配下のテキストを変更
        buttonText.text = unitName;
        this.unitName = unitName;
        //Prefubは自分配下以外は[SerializeField]出来ないので、
        //初期化時にインスタンスを渡して貰う
        this.battleMapManager = battleMapManager;
    }

    //200809 ClassChangeButtonと違い、無効化をInitと別メソッドへ
    public void SetDisable()
    {
        buttonText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
        isDisable = true;
    }

    //ボタンを有効か
    public void SetEnable()
    {
        buttonText.color = new Color(0f, 0, 0);
        isDisable = false;
    }

    public void setEntry()
    {
        entryText.gameObject.SetActive(true);
    }

    public void removeEntry()
    {
        entryText.gameObject.SetActive(false);
    }

    //クリックされた時
    public void Onclick()
    {

        if(isDisable)
        {
            //ブッブーみたいな効果音鳴らす
            return;
        }
        else
        {

            battleMapManager.EntryUnitAndRemoveUnit(buttonText.text);
        }


    }

    //選択された時に実行
    public void OnSelect()
    {
        battleMapManager.changeUnitOutlineWindow(unitName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210522 戦闘テストで戦うユニットを選択する為のボタン
/// テスト画面用で本編では使わない
/// </summary>
public class BattleUnitButton : MonoBehaviour
{

    //ボタン配下のテキスト
    [SerializeField]
    private Text buttonText;

    private BattleManager battleManager;

    //初期化メソッド
    public void Init(string unitName, BattleManager battleManager)
    {
        //ボタンにユニットの名前を設定
        buttonText.text = unitName;
        this.battleManager = battleManager;
    }

    //クリックされた時
    public void Onclick()
    {
        //選択したユニット名を渡す
        battleManager.selectedUnitName = buttonText.text;
    }

    //選択された時に実行
    public void OnSelect()
    {
        battleManager.changeUnitOutlineWindow(buttonText.text);
    }

}

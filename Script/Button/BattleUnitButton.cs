using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UnitButtonと似ているが、流石にステータス画面と共通化は難しいので
/// 別クラスで実装してしまう
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
        //配下のテキストを変更
        buttonText.text = unitName;
        //Prefubは自分配下以外は[SerializeField]出来ないので、
        //初期化時にインスタンスを渡して貰う
        this.battleManager = battleManager;
    }

    //クリックされた時
    public void Onclick()
    {
        //選択したユニット名を渡す
        battleManager.selectedUnitName = buttonText.text;
        //ウィンドウを開く
        //210215 戦闘テスト出来なくなったので改修が必要
        //battleManager.openWeaponWindow(buttonText.text);
    }

    //選択された時に実行
    public void OnSelect()
    {
        battleManager.changeUnitOutlineWindow(buttonText.text);
    }

}

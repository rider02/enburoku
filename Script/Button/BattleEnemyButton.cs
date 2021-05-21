using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Battleシーンで敵を選択する為のボタン 本番では使わなさそう
/// </summary>
public class BattleEnemyButton : MonoBehaviour
{
    //ボタン配下のテキスト
    [SerializeField]
    private Text buttonText;

    private BattleManager battleManager;

    public void Init(string enemyName, BattleManager battleManager)
    {
        //配下のテキストを変更
        buttonText.text = enemyName;
        //Prefubは自分配下以外は[SerializeField]出来ないので、
        //初期化時にインスタンスを渡して貰う
        this.battleManager = battleManager;
    }

    //クリックすると戦闘ウィンドウ開く
    public void Onclick()
    {
        battleManager.selectedEnemyName = buttonText.text;
        battleManager.OpenBattleView();
    }

    //選択された時に実行
    public void OnSelect()
    {
        battleManager.changeEnemyOutlineWindow(buttonText.text);
    }

}

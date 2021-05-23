using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210522 戦闘テストで敵を選択する為のボタン
/// テスト画面用で本編では使わない
/// </summary>
public class BattleEnemyButton : MonoBehaviour
{
    //ボタン配下のテキスト
    [SerializeField]
    private Text buttonText;

    private BattleManager battleManager;

    public void Init(string enemyName, BattleManager battleManager)
    {
        //ボタンに敵の名前を設定
        buttonText.text = enemyName;

        this.battleManager = battleManager;
    }

    //クリックすると戦闘ウィンドウ開く
    public void Onclick()
    {
        battleManager.selectedEnemyName = buttonText.text;
        //battleManager.OpenBattleView();
    }

    //選択された時に実行
    public void OnSelect()
    {
        battleManager.changeEnemyOutlineWindow(buttonText.text);
    }

}

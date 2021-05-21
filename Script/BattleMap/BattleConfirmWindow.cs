using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 200216 機能は少ないが、テキストの更新の為作成
/// </summary>
public class BattleConfirmWindow : MonoBehaviour
{
    [SerializeField] Text confirmText;
    BattleManager battleManager;

    bool isAttack;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;
    }

    public void UpdateConfirmtext(string text, bool isAttack)
    {
        confirmText.text = text;
        this.isAttack = isAttack;
    }

    //開始ボタンを押した時、戦闘開始、もしくは回復開始を行う為処理を出し分ける
    public void OnStartButtonClick()
    {
        if (isAttack)
        {
            battleManager.BattleStart();
        }
        else
        {
            battleManager.HealStart();
        }
    }
}

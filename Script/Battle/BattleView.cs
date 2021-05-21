using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 少しBattleManagerに集約させ過ぎなのでこっちへ
/// 敵と
/// </summary>
public class BattleView : MonoBehaviour
{
    [SerializeField]
    ButtleStatusWindow unitStatusWindow;

    [SerializeField]
    ButtleStatusWindow enemyStatusWindow;

    /// <summary>
    /// battleParameterDTOを受け取ったら
    /// </summary>
    /// <param name="battleParameterDTO"></param>
    public void UpdateText(BattleParameterDTO battleParameterDTO)
    {
        SetAttackMode();
        unitStatusWindow.UpdatePleyerText(battleParameterDTO);
        enemyStatusWindow.UpdateEnemyText(battleParameterDTO);
    }

    //回復版のウィンドウ更新 事前にSetHealModeを呼ぶこと
    public void HealUpdateText(HealParameterDTO healParameterDTO) {
        SetHealMode();
        unitStatusWindow.UpdateHealText(healParameterDTO);
        enemyStatusWindow.UpdateHealTargetText(healParameterDTO);
    }

    //210216 回復の場合はUIが結構変わる
    public void SetAttackMode()
    {
        enemyStatusWindow.SetAttackMode();
    }

    public void SetHealMode()
    {
        enemyStatusWindow.SetHealMode();
    }

    public void UpdateEnemyHp(int hp)
    {
        enemyStatusWindow.UpdateHp(hp);
    }

    public void UpdatePlayerHp(int hp)
    {
        unitStatusWindow.UpdateHp(hp);
    }


}

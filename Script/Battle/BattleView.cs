using UnityEngine;

/// <summary>
/// 戦闘の味方と敵のUIを制御するクラス
/// </summary>
public class BattleView : MonoBehaviour
{
    [SerializeField]
    ButtleStatusWindow unitStatusWindow;

    [SerializeField]
    ButtleStatusWindow enemyStatusWindow;

    //戦闘の情報を表示する
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

    //210216 通常の戦闘のUI
    public void SetAttackMode()
    {
        enemyStatusWindow.SetAttackMode();
    }

    //210216 回復の場合、相手が反撃してくる事は絶対有り得ないのでUI変更
    public void SetHealMode()
    {
        enemyStatusWindow.SetHealMode();
    }

    //HPの更新とゲージの変動開始
    public void UpdateEnemyHp(int hp)
    {
        enemyStatusWindow.UpdateHp(hp);
    }

    public void UpdatePlayerHp(int hp)
    {
        unitStatusWindow.UpdateHp(hp);
    }


}

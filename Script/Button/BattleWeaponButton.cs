using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210522 戦闘テストで武器を選択する為のボタン
/// テスト画面用で本編では使わない
/// </summary>
public class BattleWeaponButton : MonoBehaviour
{
    //武器名
    [SerializeField]
    private Text weaponNameText;
    [SerializeField]
    private Text enduranceText;

    private BattleManager battleManager;

    //初期化メソッド
    public void Init(Weapon weapon, BattleManager battleManager)
    {
        //配下の名前、回数、値段を設定
        weaponNameText.text = weapon.name;
        enduranceText.text = string.Format("{0}/{1}", weapon.endurance.ToString(), weapon.maxEndurance.ToString());

        this.battleManager = battleManager;
    }

    public void Onclick()
    {
        battleManager.setEquipWeapon(weaponNameText.text);

        //battleManager.OpenEnemyWindow();
    }

    public void OnSelect()
    {
        battleManager.changeWeaponDetailWindow(weaponNameText.text);
    }

}

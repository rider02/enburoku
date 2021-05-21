using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210218 道具の使用確認ウィンドウ
/// 機能は滅茶苦茶少ないが、使用するアイテムを保持させておく機能を持たせる
/// </summary>
public class UseItemConfirmWindow : MonoBehaviour
{
    //使用するアイテム
    private Potion potion;

    private BattleMapManager battleMapManager;
    public void Init(BattleMapManager battleMapManager, Potion potion)
    {
        this.battleMapManager = battleMapManager;

        //使用するアイテムを設定
        this.potion = potion;
    }

    //クリックされた時
    public void Onclick()
    {
        battleMapManager.UsePotion(potion);
    }
}

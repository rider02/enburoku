using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敗北条件をStageクラスに列挙型で持たせる
/// </summary>
public enum LoseCondition
{
    [StringValue("霊夢の撤退")]
    REIMU_LOSE,

    [StringValue("レミリアの撤退")]
    REMILIA_LOSE,

    [StringValue("10ターン経過")]
    TURN10,

    [StringValue("20ターン経過")]
    TURN20,

    //意外と少ねえな
}
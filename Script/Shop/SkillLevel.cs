using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器の熟練度
/// </summary>
public enum SkillLevel
{
    //210217 計算用に数字のAttribute設定
    //210218 技能レベルアップ用に必要経験値をIntValueで追加
    [StringValue("-")]
    [Priority(0)]
    [IntValue(1000)]
    NONE,

    [StringValue("E")]
    [Priority(1)]
    [IntValue(10)]
    E,

    [StringValue("D")]
    [Priority(2)]
    [IntValue(15)]
    D,

    [StringValue("C")]
    [Priority(3)]
    [IntValue(20)]
    C,

    [StringValue("B")]
    [Priority(4)]
    [IntValue(30)]
    B,

    [StringValue("A")]
    [Priority(5)]
    [IntValue(40)]
    A,

    [StringValue("S")]
    [Priority(6)]
    [IntValue(1000)]
    S
}

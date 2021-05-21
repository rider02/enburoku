using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 210217 武器を装備出来ない時、エラーをボタンに設定する
/// </summary>
public enum WeaponEquipWarn {

    //エラー無し
    NONE,

    //適正無し
    [StringValue("ユニットが使用出来ない種類の武器です。")]
    SKILL_NONE,

    //技能レベル不足
    [StringValue("熟練度が不足しています。")]
    SKILL_LEVEL_NEED,

    //他キャラの専用武器
    [StringValue("他キャラの専用武器です。")]
    PRIVATE_WEAPON,

    //壊れている
    [StringValue("壊れた武器は修理するまで使えません。")]
    BROKEN
}

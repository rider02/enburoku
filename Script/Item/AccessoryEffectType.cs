using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//210218 装飾品の効果 対応する項目にamountの値が付与される
public enum AccessoryEffectType
{
    //遠防 StatusCalculatorでステータスを開く時などに補正
    LDEFUP,

    //遠攻 同上
    LATKUP,

    //速さ 同上
    AGIUP,

    //必殺 
    CRITICALUP,

    //命中
    HITUP,

    //回避
    EVASIONUP,

    //回復量
    HEALUP,

    //必殺、特効無効
    CRITICAL_AND_SLAYER_INVALID,

    //人間特効無効
    HUMAN_SLAYER_INVALID,

    //妖怪特効無効
    YOUKAI_SLAYER_INVALID,

    //妖精特効無効
    FAIRY_SLAYER_INVALID,

    //経験値アップ
    EXPUP
}

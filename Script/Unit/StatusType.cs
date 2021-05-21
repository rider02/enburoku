using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パラメータの種類 レベルアップ時の判定に使う
/// 210218 これ、武器の補正するステータスの種類にも流用出来る事が判明
/// NONE以外ならバフ
/// </summary>
public enum StatusType
{
    HP,
    LATK,
    CATK,
    AGI,
    DEX,
    LUK,
    LDEF,
    CDEF,
    
    //Lvのピンに使用する
    LV,

    //武器に設定した場合、NONE以外ならステータスに補正する
    NONE
}

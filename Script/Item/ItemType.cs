using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 手持ちアイテムのタイプ
/// 武器、装飾品、薬、鍵やたいまつ、使用不可の5種類
/// </summary>
public enum ItemType
{
    WEAPON,
    POTION,
    TOOL,
    ACCESSORY,
    EVENT
}
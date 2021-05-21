using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210513 タイトルの状態
/// </summary>
public enum TitleMode
{
    [StringValue("タイトル")]
    TITLE,

    [StringValue("メニュー")]
    MENU,

    [StringValue("ロード")]
    LOAD,

    [StringValue("データ消去")]
    DELETE,

    [StringValue("キーコンフィグ")]
    KEY_CONFIG,

    [StringValue("キーコンフィグ受付")]
    KEY_ASSIGN_RECEIPT,
}
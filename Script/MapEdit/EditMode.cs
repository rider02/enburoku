using UnityEngine;

/// <summary>
/// 200802 エディットモード時の状態
/// </summary>
public enum EditMode
{
    [StringValue("無効")]
    NONE,

    [StringValue("セル入力")]
    CELL_EDIT,

    [StringValue("マップサイズ設定")]
    MAP_EDIT,

    [StringValue("メニュー")]
    MENU,

    [StringValue("セルの種類選択")]
    SELECT_CELL,
}
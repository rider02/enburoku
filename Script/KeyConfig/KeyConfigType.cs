/// <summary>
/// 210513 キーコンフィグを行うボタンの種類
/// StringValueはキーコンフィグの表示用に使用する
/// </summary>
public enum KeyConfigType
{
    [StringValue("決定")]
    SUBMIT,

    [StringValue("キャンセル")]
    CANCEL,

    [StringValue("スタート")]
    START,

    [StringValue("メニュー")]
    MENU,

    [StringValue("カーソル速度変更")]
    SPEED,

    [StringValue("カメラのズーム")]
    ZOOM
}
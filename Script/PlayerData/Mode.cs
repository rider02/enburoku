/// <summary>
/// 敗北したユニットの扱い (モード)
/// カジュアル：敗北したユニットは次の章復活
/// クラシック：敗北したユニットは消滅
/// ミディアム：敗北したユニットは１章出撃不可
/// </summary>
public enum Mode
{
    [StringValue("カジュアル")]
    CASUAL,

    [StringValue("クラシック")]
    CLASSIC,

    [StringValue("ミディアム")]
    MEDIUM   //210220 追加
}
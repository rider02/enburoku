/// <summary>
/// 210219 ユニットに設定した場合は種族
/// 武器に設定した場合は特効対象で、敵ユニットと一致したら特効
/// </summary>
public enum RaceType
{
    [StringValue("無し")]
    NONE,

    [StringValue("人間")]
    HUMAN,

    [StringValue("妖精")]
    FAIRY,

    [StringValue("幽霊")]
    GHOST,

    [StringValue("妖怪")]
    YOUKAI,

    [StringValue("使い魔")]
    MAGIC,

    [StringValue("付喪神")]
    TSUKUMOGAMI,

    [StringValue("玉兎")]
    MOON_RABBIT,
}

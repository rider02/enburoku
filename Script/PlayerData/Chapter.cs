using System;

/// <summary>
/// ゲームの進行状況
/// </summary>
public enum Chapter
{
    //メタ情報で表示名を設定すること
    [ChapterValue("異変の幕開け")]
    [StringValue("博麗神社")]
    [ReimuRoute(true)]
    STAGE1 = 1,

    [ChapterValue("夢幻夜行絵巻 ～ Mystic Flier")]
    [StringValue("夜の境内裏")]
    [ReimuRoute(true)]
    STAGE2 = 2,

    [ChapterValue("湖上の魔精 ～ Water Magus(前編)")]
    [StringValue("霧の湖(前編)")]
    [ReimuRoute(true)]
    STAGE3 = 3,

    [ChapterValue("湖上の魔精 ～ Water Magus(後編)")]
    [StringValue("霧の湖(後編)")]
    [ReimuRoute(true)]
    STAGE4 = 4,

    [ChapterValue("いたずらに命をかけて")]
    [StringValue("霧の湖(外伝)")]
    [ReimuRoute(true)]
    STAGE5 = 5,

    [ChapterValue("紅色の境 ～ Scarlet Land")]
    [StringValue("紅魔館の庭園")]
    [ReimuRoute(true)]
    STAGE6 = 6,

    [ChapterValue("暗闇の館 ～ Save the mind(前編)")]
    [StringValue("大図書館(前編)")]
    [ReimuRoute(true)]
    STAGE7 = 7,

    [ChapterValue("暗闇の館 ～ Save the mind(後編)")]
    [StringValue("大図書館(後編)")]
    [ReimuRoute(true)]
    STAGE8 = 8,

    [ChapterValue("紅い月に瀟洒な従者を")]
    [StringValue("紅魔館ホール(前編)")]
    [ReimuRoute(true)]
    STAGE9 = 9,

    [ChapterValue("メイド長 十六夜咲夜")]
    [StringValue("紅魔館ホール(後編)")]
    [ReimuRoute(true)]
    STAGE10 = 10,

    [ChapterValue("エリュシオンに血の雨")]
    [StringValue("紅魔館最上階(前編)")]
    [ReimuRoute(true)]
    STAGE11 = 11,

    [ChapterValue("こんなに月も紅いから")]
    [StringValue("紅魔館最上階(後編)")]
    [ReimuRoute(true)]
    STAGE12 = 12,

    [ChapterValue("紅魔の同胞")]
    [StringValue("紅魔館の庭園")]
    [ReimuRoute(false)]
    R_STAGE1 = 13,

    [ChapterValue("知識人の受難")]
    [StringValue("大図書館(前編)")]
    [ReimuRoute(false)]
    R_STAGE2 = 14,

    [ChapterValue("密室を出た少女")]
    [StringValue("大図書館(後編)")]
    [ReimuRoute(false)]
    R_STAGE3 = 15,

    [ChapterValue("最強の氷精")]
    [StringValue("霧の湖")]
    [ReimuRoute(false)]
    R_STAGE4 = 16,

    [ChapterValue("天狗の取材")]
    [StringValue("紅魔館の庭園")]
    [ReimuRoute(false)]
    R_STAGE5 = 17,

    [ChapterValue("泥棒さんこんにちわ")]
    [StringValue("大図書館")]
    [ReimuRoute(false)]
    R_STAGE6 = 18,

    [ChapterValue("地上の玉兎")]
    [StringValue("大図書館(前編)")]
    [ReimuRoute(false)]
    R_STAGE7 = 19,

    [ChapterValue("悪魔の妹")]
    [StringValue("紅魔館地下室")]
    [ReimuRoute(false)]
    R_STAGE8 = 20,

    [ChapterValue("こんなに月も紅いのに")]
    [StringValue("紅魔館最上階(前編)")]
    [ReimuRoute(false)]
    R_STAGE9 = 21,

    [ChapterValue("その斯くも美しき紅に")]
    [StringValue("紅魔館最上階(後編)")]
    [ReimuRoute(false)]
    R_STAGE10 = 22,
}
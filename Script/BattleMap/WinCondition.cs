/// <summary>
/// 勝利条件をStageクラスに列挙型で持たせる
/// </summary>
public enum WinCondition
{
    [StringValue("敵の全滅")]
    EXTERMINATION,

    [StringValue("敵将の撃破")]
    BOSS,

}

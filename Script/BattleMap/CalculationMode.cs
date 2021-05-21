using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210212 プレイヤーの移動、攻撃、敵の移動、攻撃でハイライトするべきセルが変わってくるのでEnumで処理を分岐させる
/// </summary>
public enum CalculationMode
{
    [StringValue("プレイヤーの移動")]
    PLAYER_MOVE,

    [StringValue("プレイヤーの攻撃")]
    PLAYER_ATTACK,

    [StringValue("敵の移動")]
    ENEMY_MOVE,

    [StringValue("敵の攻撃")]
    ENEMY_ATTACK,

    [StringValue("敵の探索")]
    ENEMY_SEARCH,
}

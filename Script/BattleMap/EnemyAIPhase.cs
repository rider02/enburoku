using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210212 敵AIの状態を列挙型で管理
/// </summary>
public enum EnemyAIPhase
{
    [StringValue("敵ユニット取得")]
    GET_ENEMY,

    [StringValue("カーソル移動中")]
    MOVE_CURSOR,

    [StringValue("移動可能距離表示")]
    MOVE,

    [StringValue("移動中")]
    MOVING,

    [StringValue("攻撃範囲表示")]
    ATTACK,

    [StringValue("会話表示中")]
    TALK,

    [StringValue("敵ターン終了")]
    END,

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210215 敵の索敵パターン
/// </summary>
public enum EnemyAIPattern
{
    //突撃AI
    [StringValue("探索")]
    SEARCH,

    [StringValue("指定ターン経過後に探索")]
    WAIT,

    [StringValue("攻撃可能範囲にユニットが居れば攻撃")]
    REACT,

    [StringValue("移動しない")]
    BOSS,
}

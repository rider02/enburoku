using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    INIT,

    OPENING,

    WAIT,

    //プレイヤー攻撃
    PLAYERATTACK,

    //敵攻撃
    ENEMYATTACK,

    //プレイヤー追撃
    PLAYERCHASE,

    //敵追撃
    ENEMYCHASE,

    //戦闘結果の経験値表示
    RESULT,

    //経験値をゲージに反映して、最後にレベルアップしたか判定
    EXPGAUGE,

    //210216 回復はかなり処理が違うので新規作成
    HEAL_INIT,

    //回復実行
    HEAL,

    //回復時の戦闘結果経験値表示
    HEAL_RESULT,

    //回復時の後処理
    HEAL_END,

    HEAL_LVUP,

    //レベルアップ中
    LVUP,

    //武器故障、アイテム取得、スキルレベル上昇等
    MESSAGE,

    //会話中
    TALK,

    //終了
    END,

    //敗北
    LOSE,
}
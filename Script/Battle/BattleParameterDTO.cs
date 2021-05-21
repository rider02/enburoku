using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleParameterDTO
{
    //名前
    public string unitName { get; set; }

    public string enemyName { get; set; }

    //武器 名前でなくWeaponで統一しても良いかも
    public string unitWeaponName { get; set; }

    public Weapon unitWeapon { get; set; }

    public string enemyWeaponName { get; set; }

    public Weapon enemyWeapon { get; set; }

    //210220 特効フラグを追加してUIに反映する
    public bool isUnitAttackSlayer { get; set; }

    public bool isEnemyAttackSlayer { get; set; }


    //相性 攻撃側有利のみで相手も決まるだろう
    public BattleWeaponAffinity affinity {get;set;}

    //HP
    public int unitHp { get; set; }

    public int unitMaxHp { get; set; }

    public int enemyHp { get; set; }

    public int enemyMaxHp { get; set; }

    //攻撃力
    public int unitAttack { get; set; }

    public int enemyAttack { get; set; }

    //命中率
    public int unitHitRate { get; set; }

    public int enemyHitRate { get; set; }

    //必殺率
    public int unitCriticalRate { get; set; }

    public int enemyCiritcalRate { get; set; }

    //追撃フラグ
    public bool unitChaseFlag { get; set; }

    public bool enemyChaseFlag { get; set; }

    //210220 攻撃可否フラグ
    public bool isUnitAttackable { get; set; }

    public bool isEnemyAttackable { get; set; }

    //210220 勇者武器フラグ
    public bool isUnitYuusha { get; set; }

    public bool isEnemyYuusha { get; set; }


}

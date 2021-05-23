/// <summary>
/// 210521 敵味方の戦闘に必要な各パラメータのDTO
/// </summary>
public class BattleParameterDTO
{
    //名前
    public string unitName { get; set; }
    public string enemyName { get; set; }

    //武器名
    public string unitWeaponName { get; set; }
    //武器
    public Weapon unitWeapon { get; set; }

    public string enemyWeaponName { get; set; }
    public Weapon enemyWeapon { get; set; }

    //210220 特効フラグ UIの文字の色が変わる
    public bool isUnitAttackSlayer { get; set; }
    public bool isEnemyAttackSlayer { get; set; }


    //ユニットの敵との相性 有利なら相手は不利
    public BattleWeaponAffinity affinity {get;set;}

    //HPと最大HP
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

    //210220 勇者武器フラグ(trueなら攻撃回数2倍でUIも変更)
    public bool isUnitYuusha { get; set; }
    public bool isEnemyYuusha { get; set; }
}

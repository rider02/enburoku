using System.Collections.Generic;

/// <summary>
/// 各ユニットのクラス
/// </summary>
/// //System.Serializableを設定しないと、データを保持できない(シリアライズできない)ので注意
[System.Serializable]
public class Unit
{
    //表示用ユニット名 最大6文字？
    public string name;

    //フルネーム
    public string fullName;

    //クラス
    public Job job;

    //210219 種族を追加
    public RaceType race;

    //Image/Charactors/+pathname+/icon.pngみたいに使う
    public string pathName;

    //レベル
    public int lv;

    //経験値
    public int exp;

    //HP
    public int hp;

    //最大HP
    public int maxhp;

    //遠距離攻撃 long-range attackの略
    public int latk;

    //近距離攻撃 close-range attakの略
    public int catk;
    public int agi;
    public int dex;
    public int luk;
    public int ldef;
    public int cdef;

    //210217 ユニットの移動力はクラス依存だが、ステータスアップアイテム用に作った
    public int movePlus = 0;

    //200827 DictionaryをScriptableObject化出来ないので個別に保持
    public SkillLevel shotLevel;
    public SkillLevel laserLevel;
    public SkillLevel strikeLevel;
    public SkillLevel healLevel;

    //210218 技能レベル実装
    public int shotExp { get; set; }
    public int laserExp { get; set; }

    public int strikeExp { get; set; }

    public int healExp { get; set; }

    //手持ちのアイテムリスト
    public List<Item> carryItem;
    
    //保持スキル
    public List<Skill> skills;

    //スキルレベル
    public Dictionary<WeaponType, SkillLevel> skillLevelMap;

    //装備武器
    public Weapon equipWeapon;

    //210216 回復の杖 武器と別に存在する
    public Weapon equipHealRod;

    //210218 アクセサリ
    public Accessory equipAccessory;

    //霊夢ルートで登場するキャラか falseならレミリアルートで表示
    public bool isReimuRoute;

    //コンストラクタ
    public Unit(string name ,string fullName, Job job, RaceType race, int[] status, List<Item> carryItem, string pathName
        , Dictionary<WeaponType, SkillLevel> skillLevelMap, bool isReimuRoute)
    {
        this.name = name;

        this.fullName = fullName;

        this.job = job;

        this.race = race;

        this.pathName = pathName;

        exp = 0;

        this.lv = status[0];

        this.hp = status[1];

        this.maxhp = status[1];

        this.latk = status[2];

        this.catk = status[3];

        this.dex = status[4];

        this.agi = status[5];

        this.luk = status[6];

        this.ldef = status[7];

        this.cdef = status[8];

        this.carryItem = carryItem;

        //装備フラグの立った武器やアクセサリを装備 もし複数有れば最後の物を取得する
        foreach (Item item in carryItem)
        {
            if (item.ItemType == ItemType.WEAPON && item.isEquip == true)
            {
                this.equipWeapon = item.weapon;
            }
            else if (item.ItemType == ItemType.ACCESSORY && item.isEquip == true)
            {
                this.equipAccessory = item.accessory;
            }
        }

        this.skillLevelMap = skillLevelMap;

        this.isReimuRoute = isReimuRoute;

        foreach (KeyValuePair<WeaponType, SkillLevel> kvp in skillLevelMap)
        {

            if (kvp.Key == WeaponType.SHOT)
            {

                shotLevel = kvp.Value;
            }
            else if (kvp.Key == WeaponType.LASER)
            {

                laserLevel = kvp.Value;
            }
            else if (kvp.Key == WeaponType.STRIKE)
            {

                strikeLevel = kvp.Value;
            }
            else if (kvp.Key == WeaponType.HEAL)
            {
                healLevel = kvp.Value;
            }
        }

    }

    // ダメージを受ける
    public void receiveDamage(int damage)
    {
        this.hp -= damage;
        if (hp <= 0) hp = 0;
    }

    //210216 回復を受ける
    public void receiveHeal(int healAmount)
    {
        this.hp += healAmount;

        //ここで回復最大HPよりも回復しない機能を追加
        int maxHp = maxhp + job.statusDto.jobHp;
        StatusCalculator statusCalc = new StatusCalculator();

        //スキルによるバフ反映しないと全回復しない
        maxHp = statusCalc.CalcHpBuff(maxHp, job.skills);
        if (hp >= maxHp) hp = maxHp;
    }

    //死んだかどうか返す
    public bool isDead()
    {
        return (hp <= 0);
    }

    //200719 スキルを持っているかを判定する
    public bool haveSkill(Skill skill)
    {
        foreach(var unitSkill in skills)
        {
            if(unitSkill == skill)
            {
                //持っている
                return true;
            }
        }

        //持っていない
        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵クラス Unitと大体同じ変数を持つ
/// </summary>
[System.Serializable]
public class Enemy
{

    //ユニット名
    public string name;

    //クラス
    public Job job;

    //210219 種族を追加
    public RaceType race;

    //Resource配下の画像表示用パス
    public string pathName;

    public int lv;      //レベル
    public int hp;      //HP
    public int maxhp;   //最大HP
    public int latk;    //遠距離攻撃 long-range attackの略
    public int catk;    //近距離攻撃 close-range attakの略
    public int agi;     //速さ
    public int dex;     //技
    public int luk;     //幸運
    public int ldef;    //遠距離防御
    public int cdef;    //近距離防御

    //ボス 顔グラフィック表示、戦う前後に会話等の判定に使用
    public bool isBoss;

    //AIのパターン 突撃してくるか、攻撃範囲に入ると攻撃してくる等
    public EnemyAIPattern enemyAIPattern;

    //手持ちのアイテムリスト
    public List<Item> carryItem;

    //装備している武器
    public Weapon equipWeapon;

    //210218 アクセサリ
    public Accessory equipAccessory;

    //保持スキル　210220 何故今まで無かったのか
    public List<Skill> skills;

    //ステージで初期配置されている場所
    public Coordinate coordinate;

    //ドロップアイテムを持っているか
    public bool hasDromItem = false;

    public Item dropItem;

    //行動開始する時間
    public int actionTurn;

    //210220 技能レベル Dictionaryはシリアライズ出来ないので個別に保持
    public SkillLevel shotLevel;
    public SkillLevel laserLevel;
    public SkillLevel strikeLevel;
    public SkillLevel healLevel;

    //コンストラクタ 同名キャラもステージによってステータス等が違う為、最低限必要しか設定しない
    //ステータス、レベルは基礎値
    public Enemy(string name, Job job, int[] status, bool isBoss, RaceType race, string pathName, Dictionary<WeaponType, SkillLevel> skillLevelMap)
    {
        this.job = job;
        this.name = name;
        this.race = race;
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
        this.isBoss = isBoss;
        this.pathName = pathName;

        //技能レベル
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

    /// <summary>
    /// StageDatabaseで呼んで、ステージごとのユニット設定を行う
    /// </summary>
    /// <param name="Lv"></param>
    /// <param name="coordinate">ステージで初期配置されている座標</param>
    /// <param name="weapon">装備武器</param>
    /// <param name="carryItem">持ち物</param>
    /// <param name="enemyAIPattern">行動パターン</param>
    public void StageInit(int lv , Coordinate coordinate , List<Item> carryItem,
        EnemyAIPattern enemyAIPattern)
    {
        this.lv = lv;
        this.coordinate = coordinate;
        this.carryItem = carryItem;
        this.enemyAIPattern = enemyAIPattern;

        //装備フラグがtrueの武器、アクセサリを装備
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

        //敵キャラはレベルを渡すとレベルアップ分のステータスを反映してくれる
        GrowthDatabase growthDatabase = Resources.Load<GrowthDatabase>("growthDatabase");

        //成長率取得
        GrowthRate growthRate = growthDatabase.FindByName(name);
        int lvUp = lv - 1;

        //小数点以下切り捨て 成長率は整数で入っているので10で割ってやる
        this.maxhp += Mathf.FloorToInt(lvUp * growthRate.hpRate/100);
        this.hp = maxhp;

        this.latk += Mathf.FloorToInt(lvUp * growthRate.latkRate / 100);

        this.catk += Mathf.FloorToInt(lvUp * growthRate.catkRate / 100);

        this.dex += Mathf.FloorToInt(lvUp * growthRate.dexRate / 100);

        this.agi += Mathf.FloorToInt(lvUp * growthRate.agiRate / 100);

        this.luk += Mathf.FloorToInt(lvUp * growthRate.lukRate / 100);

        this.ldef += Mathf.FloorToInt(lvUp * growthRate.ldefRate / 100);

        this.cdef += Mathf.FloorToInt(lvUp * growthRate.cdefRate / 100);


    }

    // ダメージを受ける
    public void ReceiveDamage(int damage)
    {
        this.hp -= damage;
        if (hp <= 0) hp = 0;
    }

    //TODO 敵にも回復を実装する

    //死んだかどうか返す
    public bool IsDead()
    {
        return (hp <= 0 );
    }

    // 210228 ドロップアイテム設定
    public void SetDropItem(Item item)
    {
        hasDromItem = true;
        dropItem = item;
    }


    // 210304 一定ターン経過から行動開始するよう設定
    public void SetActionTurn(int turn)
    {
        enemyAIPattern = EnemyAIPattern.WAIT;
        this.actionTurn = turn;
    }

    //200719 スキルを持っているかを判定する
    public bool HasSkill(Skill skill)
    {
        foreach (var unitSkill in skills)
        {
            if (unitSkill == skill)
            {
                //持っている
                return true;
            }
        }

        //持っていない
        return false;
    }

    //初期化時にDatabaseから同じインスタンスを取得してしまうので作成
    public Enemy Clone()
    {
        // Object型で返ってくるのでキャストが必要
        return (Enemy)MemberwiseClone();
    }
}

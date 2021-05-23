/// <summary>
/// 武器
/// </summary>
//System.Serializableを設定しないと、データを保持できない(シリアライズできない)ので注意
[System.Serializable]
public class Weapon
{
    public string name;         //名前
    public WeaponType type;     //種類
    public bool isYuusha;       //2回攻撃フラグ
    public bool isNfs;          //非売品(Not for sale)
    public int attack;          //攻撃力
    public int hitRate;         //命中率

    //必殺
    public int criticalRate;

    //遅延
    public int delay;

    //耐久力
    public int endurance;

    public int maxEndurance;
    //値段
    public int price;

    //熟練度
    public SkillLevel skillLevel;

    //説明文
    public string annotationText;

    //特記事項
    public string featureText;

    //射程距離
    public int range;

    //近距離攻撃か
    public bool isCloseAttack;

    //210217 専用武器フラグを追加
    public bool isPrivate;

    //専用武器の場合、判定に使用する持ち主
    public string ownerName;

    //210218 武器にバフを追加 StatusTypeがNONE以外ならanountの量を加える
    public StatusType statusType;

    //バフを盛る量
    public int amount;

    //210219 特効を追加
    public RaceType slayer;

    //210220 追撃不可フラグを追加
    public bool isChaseInvalid;

    //反撃不可フラグは「天狗のカメラ」の武器名だけで判定する

    //コンストラクタ
    public Weapon(string name, string annotation, string feature, WeaponType type,
        SkillLevel skillLevel, int[] parameter,int range, bool nfs, bool yuusha, bool isCloseAttack,
        bool isPrivate, string ownerName, StatusType statusType, int amount, RaceType slayer, bool isChaseInvalid)
    {
        this.name = name;
        this.annotationText = annotation;
        this.featureText = feature;
        this.type = type;
        this.skillLevel = skillLevel;

        this.attack = parameter[0];
        this.hitRate = parameter[1];
        this.criticalRate = parameter[2];
        this.delay = parameter[3];
        this.endurance = parameter[4];
        this.maxEndurance = parameter[4];
        this.price = parameter[5];
        this.range = range;

        this.isNfs = nfs;
        this.isYuusha = yuusha;
        this.isCloseAttack = isCloseAttack;

        this.isPrivate = isPrivate;
        this.ownerName = ownerName;

        this.statusType = statusType;
        this.amount = amount;

        this.slayer = slayer;

        this.isChaseInvalid = isChaseInvalid;
    }
}

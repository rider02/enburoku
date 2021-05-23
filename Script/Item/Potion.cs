/// <summary>
/// 使用出来るアイテム
/// </summary>
//System.Serializableを設定しないと、データを保持できない(シリアライズできない)ので注意
[System.Serializable]
public class Potion
{
    //名前
    public string name;

    //説明文
    public string annotationText;

    //ステータスアップアイテム
    //trueならステータス画面で使える
    public PotionType potionType;

    //薬師のみ使えるか
    public bool isRequirePharmacy;

    //回復量、上昇量
    public int amount = 0;

    //使用回数
    public int useCount;

    //最大使用回数
    public int maxUseCount;

    //値段
    public int price ;

    
    //コンストラクタ
    public Potion(string name, string annotation, PotionType potionType, bool isRequirePharmacy,
        int amount, int useCount, int price)
    {
        this.name = name;
        this.annotationText = annotation;
        this.potionType = potionType;
        this.isRequirePharmacy = isRequirePharmacy;
        this.amount = amount;
        this.useCount = useCount;
        this.maxUseCount = useCount;
        this.price = price;
    }

}

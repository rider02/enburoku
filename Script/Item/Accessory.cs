/// <summary>
/// アクセサリのクラス
/// </summary>
[System.Serializable]
public class Accessory
{
    //名前
    public string name;

    //説明文
    public string annotationText;

    //重さ (お守りが重いのはおかしいので一旦使わない)
    public int delay;

    //効果の分類
    public AccessoryEffectType effect;

    //効果量
    public int amount;

    //値段
    public int price;

    //非売品フラグ
    public bool isNfs;

    //コンストラクタ
    public Accessory(string name, string annotation,int delay , AccessoryEffectType effect, int amount,
        int price, bool isNfs)
    {
        this.name = name;
        this.annotationText = annotation;
        this.delay = delay;
        this.effect = effect;
        this.amount = amount;
        this.price = price;
        this.isNfs = isNfs;
    }
}
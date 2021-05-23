/// <summary>
/// 金塊、鍵、クラスチェンジアイテムなど
/// </summary>
//System.Serializableを設定しないと、データを保持できない(シリアライズできない)ので注意
[System.Serializable]
public class Tool
{
    //名前
    public string name;

    //説明文
    public string annotationText;

    public bool isClassChangeItem;

    public Tool(string name, string annotation, bool isClassChangeItem)
    {
        this.name = name;
        this.annotationText = annotation;
        this.isClassChangeItem = isClassChangeItem;
    }
}

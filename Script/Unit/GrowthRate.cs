/// <summary>
/// 210522 ユニットの成長率
/// GrowthRateDatabaseでアセット化して使用する
/// </summary>
//System.Serializableを設定しないと、データを保持できない(シリアライズできない)ので注意
[System.Serializable]
public class GrowthRate
{

    //ユニット名
    public string name;

    //各ステータス成長率
    public int hpRate;
    public int latkRate;
    public int catkRate;
    public int agiRate;
    public int dexRate;
    public int lukRate;
    public int ldefRate;
    public int cdefRate;

    public GrowthRate(string name, int[] growthRate)
    {

        this.name = name;

        this.hpRate = growthRate[0];
        this.latkRate = growthRate[1];
        this.catkRate = growthRate[2];
        this.dexRate = growthRate[3];
        this.agiRate = growthRate[4];
        this.lukRate = growthRate[5];
        this.ldefRate = growthRate[6];
        this.cdefRate = growthRate[7];

    }
}

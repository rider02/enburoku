using System.Collections.Generic;

//セーブデータ このクラスの値をセーブデータ化する
[System.Serializable]
public class SavePlayerData
{
    //仲間キャラの状態
    public List<Unit> unitList;

    //お金
    public int cash;

    //210303 取得済み宝箱
    public List<AcquiredTreasure> treasureList;

    //ゲーム進行度
    public Chapter chapter;

    //プレー時間
    public int hour;
    public int minute;

    //ルート、難易度モード
    public Route route;
    public Difficulty difficulty;
    public Mode mode;
}

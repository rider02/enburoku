using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ユニット初期値のアセットファイルを作ってくれるクラス
/// Unity上部の「Create」から使用する
/// </summary>
public static class UnitDatabaseCreator
{

    //MenuItemを付ける事で上部メニューに項目を追加
    [MenuItem("Create/UnitDatabase")]
    private static void Create()
    {

       UnitDatabase unitDatabase = ScriptableObject.CreateInstance<UnitDatabase>();

        //武器の初期化用にデータベース取得
        WeaponDatabase weaponDatabase = Resources.Load<WeaponDatabase>("weaponDatabase");

        //200719 職業をデータベースから取得
        JobDatabase jobDatabase = Resources.Load<JobDatabase>("jobDatabase");

        PotionDatabase potionDatabase = Resources.Load<PotionDatabase>("potionDatabase");

        //210218 装飾品を追加
        AccessoryDatabase accessoryDatabase = Resources.Load<AccessoryDatabase>("accessoryDatabase");

        ToolDatabase toolDatabase = Resources.Load<ToolDatabase>("toolDatabase");

        //霊夢
        int[] reimuStatus = new int[] { 1, 21, 7, 5, 8, 7, 8, 6, 4 };

        //手持ちアイテム
        var reimuItem = new List<Item>();

        Item equipItem = new Item(weaponDatabase.FindByName("ホーミングアミュレット"));
        equipItem.isEquip = true;
        reimuItem.Add(equipItem);
        Item item = new Item(weaponDatabase.FindByName("お祓い棒"));
        reimuItem.Add(item);
        item = new Item(weaponDatabase.FindByName("博麗の癒符"));
        reimuItem.Add(item);
        item = new Item(potionDatabase.FindByName("霧雨の傷薬"));
        reimuItem.Add(item);
        Item accessoryItem = new Item(accessoryDatabase.FindByName("博麗の形代"));
        accessoryItem.isEquip = true;
        reimuItem.Add(accessoryItem);

        //210301 宝箱テスト
        /*item = new Item(toolDatabase.FindByName("宝の鍵"));
        reimuItem.Add(item);*/

        /*item = new Item(weaponDatabase.FindByName("先代巫女のお札"));
        reimuItem.Add(item);*/

        // 210217 スキルレベルが足りないテスト
        /*item = new Item(weaponDatabase.FindByName("奇跡の癒符"));
        reimuItem.Add(item);*/

        //武器のバフがされるかテスト
        //equipWeapon = weaponDatabase.FindByName("先代巫女のお札");

        string pathName = "Reimu";

        //200827 スキルレベル実装
        var skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.D },
            {WeaponType.STRIKE,SkillLevel.D },
            {WeaponType.HEAL,SkillLevel.D },
        };

        //200719 職業を追加
        Job job = jobDatabase.FindByJob(JobName.巫女);

        //200830 霊夢ルート、レミリアルート実装
        bool isReimuRoute = true;

        //210219 種族追加
        RaceType race = RaceType.HUMAN;

        Unit reimu = new Unit("霊夢", "博麗 霊夢", job, race, reimuStatus, reimuItem, pathName, skillLevelMap, isReimuRoute);

        

        unitDatabase.unitList.Add(reimu);

        //魔理沙
        int[] marisaStatus = new int[] { 3, 21, 11, 5, 8, 10, 5, 5, 3 };
        
        var marisaItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("香霖堂の霊符"));
        marisaItem.Add(item);
        item = new Item(weaponDatabase.FindByName("香霖堂の術符"));
        marisaItem.Add(item);
        item = new Item(weaponDatabase.FindByName("ミニ八卦炉"));
        item.isEquip = true;
        marisaItem.Add(item);
        item = new Item(potionDatabase.FindByName("霧雨の傷薬"));
        marisaItem.Add(item);

        //210301 宝の鍵消費しないかテスト
        item = new Item(toolDatabase.FindByName("宝の鍵"));
        marisaItem.Add(item);

        //210217 魔理沙に装備出来ない武器を持たせるテスト 最終的には消す
        //技能レベルは足りているが専用ではない
        /*item = new Item(weaponDatabase.FindByName("ホーミングアミュレット"));
        marisaItem.Add(item);
        //適性が無い
        item = new Item(weaponDatabase.FindByName("新聞紙の撃符"));
        marisaItem.Add(item);
        //スキルレベルが足りない
        item = new Item(weaponDatabase.FindByName("死神の霊符"));
        marisaItem.Add(item);*/

        pathName = "Marisa";

        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.E },
            {WeaponType.LASER,SkillLevel.D },
        };

        job = jobDatabase.FindByJob(JobName.魔法使い);

        race = RaceType.HUMAN;

        Unit marisa = new Unit("魔理沙", "霧雨 魔理沙", job, race, marisaStatus, marisaItem, pathName, skillLevelMap, isReimuRoute);
        //レベルアップテストの為
        marisa.exp = 99;
        unitDatabase.unitList.Add(marisa);

        //文
        int[] ayaStatus = new int[] { 16, 30, 14, 11, 16, 20, 15, 8, 11 };

        var ayaItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("天狗のカメラ"));
        item.isEquip = true;
        ayaItem.Add(item);
        item = new Item(weaponDatabase.FindByName("新聞紙の術符"));
        ayaItem.Add(item);
        item = new Item(weaponDatabase.FindByName("天狗の羽団扇"));
        ayaItem.Add(item);
        item = new Item(potionDatabase.FindByName("永遠亭の傷薬"));
        ayaItem.Add(item);
        //210220 勇者武器テスト
        item = new Item(weaponDatabase.FindByName("賢者の術符"));
        ayaItem.Add(item);

        pathName = "Aya";

        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.B },
            {WeaponType.STRIKE,SkillLevel.C },
        };

        job = jobDatabase.FindByJob(JobName.伝統の幻想ブン屋);

        race = RaceType.YOUKAI;

        Unit aya = new Unit("文", "射命丸 文", job, race, ayaStatus, ayaItem, pathName, skillLevelMap, isReimuRoute);
        unitDatabase.unitList.Add(aya);

        //ルーミア
        int[] rumiaStatus = new int[] { 5, 24, 9, 8, 5, 6, 4, 9, 8 };

        var rumiaItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("香霖堂の霊符"));
        item.isEquip = true;
        rumiaItem.Add(item);
        item = new Item(weaponDatabase.FindByName("香霖堂の術符"));
        rumiaItem.Add(item);

        pathName = "Rumia";

        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.E },
            {WeaponType.LASER,SkillLevel.D },
        };

        job = jobDatabase.FindByJob(JobName.暗闇の妖怪);

        race = RaceType.YOUKAI;

        Unit rumia = new Unit("ルーミア", "ルーミア", job, race, rumiaStatus, rumiaItem, pathName, skillLevelMap , isReimuRoute);
        unitDatabase.unitList.Add(rumia);

        //大妖精
        int[] daiyouseiStatus = new int[] { 6, 21, 7, 3, 9, 8, 7, 8, 5 };

        var daiyouseiItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("香霖堂の術符"));
        item.isEquip = true;
        daiyouseiItem.Add(item);
        item = new Item(potionDatabase.FindByName("霧雨の傷薬"));
        daiyouseiItem.Add(item);
        item = new Item(weaponDatabase.FindByName("妖精の癒符"));
        daiyouseiItem.Add(item);

        pathName = "Daiyousei";

        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.D },
            {WeaponType.HEAL,SkillLevel.C },
        };

        job = jobDatabase.FindByJob(JobName.大妖精);

        race = RaceType.FAIRY;

        Unit daiyousei = new Unit("大妖精","大妖精", job, race, daiyouseiStatus, daiyouseiItem, pathName, skillLevelMap, isReimuRoute);
        unitDatabase.unitList.Add(daiyousei);

        //ツィルノ
        int[] chirnoStatus = new int[] { 7, 23, 7, 11, 7, 10, 9, 6, 7 };

        var chirnoItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("氷つぶて"));
        item.isEquip = true;
        chirnoItem.Add(item);

        //試験用にアイテムを持たせる
        item = new Item(potionDatabase.FindByName("妖精の果実"));
        chirnoItem.Add(item);

        item = new Item(toolDatabase.FindByName("大きな金塊"));
        chirnoItem.Add(item);

        item = new Item(toolDatabase.FindByName("扉の鍵"));
        chirnoItem.Add(item);

        item = new Item(toolDatabase.FindByName("勝利のビール"));
        chirnoItem.Add(item);

        pathName = "Cirno";

        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.C },
            {WeaponType.STRIKE,SkillLevel.D },
        };

        job = jobDatabase.FindByJob(JobName.湖上の氷精);

        race = RaceType.FAIRY;

        Unit chirno = new Unit("チルノ", "チルノ", job, race, chirnoStatus, chirnoItem, pathName, skillLevelMap, isReimuRoute);
        unitDatabase.unitList.Add(chirno);

        

        //うどんげ氏
        int[] udonStatus = new int[] { 10, 26, 13, 6, 15, 11 , 3, 9, 7};

        var udonItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("玉兎の弾丸"));
        item.isEquip = true;
        udonItem.Add(item);
        item = new Item(weaponDatabase.FindByName("月人の術符"));
        udonItem.Add(item);
        item = new Item(potionDatabase.FindByName("永遠亭の傷薬"));
        udonItem.Add(item);
        item = new Item(potionDatabase.FindByName("薬箱"));
        udonItem.Add(item);

        pathName = "Udon";

        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.B },
            {WeaponType.STRIKE,SkillLevel.C },
        };

        job = jobDatabase.FindByJob(JobName.狂気の月の兎);

        race = RaceType.MOON_RABBIT;

        Unit udonge = new Unit("鈴仙", "鈴仙・Ｕ・イナバ", job, race, udonStatus, udonItem, pathName, skillLevelMap, isReimuRoute);
        unitDatabase.unitList.Add(udonge);

        //ここからレミリアルート
        isReimuRoute = false;

        //美鈴
        int[] meirinStatus = new int[] { 6, 28, 5, 13, 8, 10, 5, 5, 8 };

        var meirinItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("拳"));
        item.isEquip = true;
        meirinItem.Add(item);
        item = new Item(weaponDatabase.FindByName("香霖堂の撃符"));
        meirinItem.Add(item);
        item = new Item(weaponDatabase.FindByName("妖精の癒符"));
        meirinItem.Add(item);
        item = new Item(potionDatabase.FindByName("霧雨の傷薬"));
        meirinItem.Add(item);

        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.D },
            {WeaponType.HEAL,SkillLevel.E },
        };

        pathName = "Meirin";

        job = jobDatabase.FindByJob(JobName.門番);

        race = RaceType.YOUKAI;

        Unit meirin = new Unit("美鈴","紅 美鈴", job, race, meirinStatus, meirinItem, pathName, skillLevelMap, isReimuRoute);
        unitDatabase.unitList.Add(meirin);

        //小悪魔
        int[] koakumaStatus = new int[] { 8, 23, 11, 7, 9, 11, 5, 6, 8 };

        //アイテム
        var koakumaItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("香霖堂の術符"));
        item.isEquip = true;
        koakumaItem.Add(item);
        item = new Item(weaponDatabase.FindByName("妖精の癒符"));
        koakumaItem.Add(item);
        item = new Item(potionDatabase.FindByName("霧雨の傷薬"));
        koakumaItem.Add(item);

        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.D },
            {WeaponType.HEAL,SkillLevel.D },
        };

        pathName = "Koakuma";

        race = RaceType.YOUKAI;

        job = jobDatabase.FindByJob(JobName.小悪魔);
        Unit koakuma = new Unit("小悪魔", "小悪魔", job, race, koakumaStatus, koakumaItem, pathName, skillLevelMap, isReimuRoute);
        unitDatabase.unitList.Add(koakuma);

        //パチュリー
        int[] patuStatus = new int[] { 10, 21, 15, 1, 16, 6, 8, 11, 3 };

        pathName = "Patu";

        //アイテム
        var patuItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("河童の術符"));
        patuItem.Add(item);
        item = new Item(weaponDatabase.FindByName("仙人の霊符"));
        item.isEquip = true;
        patuItem.Add(item);
        item = new Item(potionDatabase.FindByName("永遠亭の傷薬"));
        patuItem.Add(item);

        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.C },
            {WeaponType.LASER,SkillLevel.B },
        };

        job = jobDatabase.FindByJob(JobName.魔女);

        race = RaceType.YOUKAI;

        Unit patu = new Unit("パチュリー", "パチュリー・ノーレッジ", job, race, patuStatus, patuItem, pathName, skillLevelMap, isReimuRoute);
        unitDatabase.unitList.Add(patu);

        //咲夜
        int[] sakuyaStatus = new int[] { 8, 23, 9, 10, 12, 14, 12, 8, 10 };

        //アイテム
        var sakuyaItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("銀のナイフ"));
        item.isEquip = true;
        sakuyaItem.Add(item);
        item = new Item(weaponDatabase.FindByName("香霖堂の術符"));
        sakuyaItem.Add(item);
        item = new Item(potionDatabase.FindByName("永遠亭の傷薬"));
        sakuyaItem.Add(item);


        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.D },
            {WeaponType.STRIKE,SkillLevel.C },
        };

        pathName = "Sakuya";

        job = jobDatabase.FindByJob(JobName.紅魔館のメイド);

        race = RaceType.HUMAN;

        Unit sakuya = new Unit("咲夜", "十六夜 咲夜", job, race, sakuyaStatus, sakuyaItem, pathName, skillLevelMap, isReimuRoute);
        unitDatabase.unitList.Add(sakuya);

        //レミリア
        int[] remilliaStatus = new int[] { 18, 36, 15, 18, 16, 22, 20, 10, 15 };

        pathName = "Remilia";

        //アイテム
        var remilliaItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("グングニル"));
        item.isEquip = true;
        remilliaItem.Add(item);
        item = new Item(weaponDatabase.FindByName("河童の撃符"));
        remilliaItem.Add(item);

        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.B },
        };

        job = jobDatabase.FindByJob(JobName.紅い悪魔);

        race = RaceType.YOUKAI;

        Unit remillia = new Unit("レミリア", "レミリア・スカーレット", job, race, remilliaStatus, remilliaItem, pathName, skillLevelMap, isReimuRoute);
        unitDatabase.unitList.Add(remillia);

        //フラン
        int[] frandreStatus = new int[] { 25, 42, 24, 27, 16, 24, 12, 11, 18 };

        pathName = "Fran";

        //アイテム
        var franItem = new List<Item>();
        item = new Item(weaponDatabase.FindByName("レーヴァテイン"));
        item.isEquip = true;
        franItem.Add(item);
        item = new Item(weaponDatabase.FindByName("死神の撃符"));
        franItem.Add(item);

        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.A },
        };

        job = jobDatabase.FindByJob(JobName.紅のカタストロフ);

        race = RaceType.YOUKAI;

        Unit frandre = new Unit("フランドール", "フランドール・スカーレット", job, race, frandreStatus, franItem, pathName, skillLevelMap, isReimuRoute);
        unitDatabase.unitList.Add(frandre);

        //ファイル書き出し Resources配下に作る
        AssetDatabase.CreateAsset(unitDatabase, "Assets/Resources/unitDatabase.asset");
    }

}
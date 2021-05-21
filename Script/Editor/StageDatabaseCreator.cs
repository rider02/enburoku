using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using JetBrains.Annotations;

/// <summary>
/// 200808
/// ステージの出撃可能人数、出撃画面フラグ、ユニットの座標を管理するSTAGEクラスを
/// ScriptableObject化
/// </summary>
/// 
public static class StageDatabaseCreator
{

    [MenuItem("Create/StageDatabase")]
    private static void Create()
    {

        StageDatabase stageDatabase = ScriptableObject.CreateInstance<StageDatabase>();

        EnemyDatabase enemyDatabase = Resources.Load<EnemyDatabase>("enemyDatabase");

        //武器の初期化用にデータベース取得
        WeaponDatabase weaponDatabase = Resources.Load<WeaponDatabase>("weaponDatabase");

        AccessoryDatabase accessoryDatabase = Resources.Load<AccessoryDatabase>("accessoryDatabase");

        ToolDatabase toolDatabase = Resources.Load<ToolDatabase>("toolDatabase");

        PotionDatabase potionDatabase = Resources.Load<PotionDatabase>("potionDatabase");

        //ステージ1 博麗神社
        Chapter chapter = Chapter.STAGE1;

        //falseで出撃は霊夢１人だけにする
        bool isUnitSelectRequired = false;

        int entryUnitCount = 1;

        bool isReimuRoute = true;

        //ユニットを配置する座標
        //霊夢1人のみ
        List<Coordinate> entryUnitCoordinates = new List<Coordinate>();
        Coordinate coordinate1 = new Coordinate(10, 3);
        entryUnitCoordinates.Add(coordinate1);

        //勝利条件と敗北条件
        WinCondition winCondition = WinCondition.BOSS;
        LoseCondition loseCondition = LoseCondition.REIMU_LOSE;

        //210513 あらすじを追加
        string storyText = "ある夏の日、音も無く、\n不穏な妖霧が幻想郷を包み始めた。\n\n" +
            "博麗神社の巫女、博麗霊夢は\n原因を突き止める為に出発する。\n\n" +
            "そこへ現れたのは、\n見知った悪友の姿であった。";


        //敵は魔理沙Lv3×1、使い魔Lv2×2
        List<Enemy> enemyList = new List<Enemy>();

        //1.使い魔
        Enemy enemy = enemyDatabase.FindByName("使い魔");
        int lv = 2;
        Weapon weapon = weaponDatabase.FindByName("新聞紙の撃符");
        Item item = new Item(weapon);
        item.isEquip = true;

        List<Item> carryItem = new List<Item>();
        carryItem.Add(item);

        //座標は適当
        Coordinate coordinate = new Coordinate(10,9);
        EnemyAIPattern pattern = EnemyAIPattern.REACT;

        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //2.使い魔
        enemy = enemyDatabase.FindByName("使い魔");
        enemy = enemy.Clone();
        lv = 2;
        weapon = weaponDatabase.FindByName("新聞紙の撃符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //座標は適当
        coordinate = new Coordinate(7, 10);
        pattern = EnemyAIPattern.REACT;

        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //3.使い魔
        enemy = enemyDatabase.FindByName("使い魔");
        enemy = enemy.Clone();
        lv = 2;
        weapon = weaponDatabase.FindByName("新聞紙の撃符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //座標は適当
        coordinate = new Coordinate(13, 10);
        pattern = EnemyAIPattern.REACT;

        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //4.使い魔
        enemy = enemyDatabase.FindByName("使い魔");
        enemy = enemy.Clone();
        lv = 2;
        weapon = weaponDatabase.FindByName("新聞紙の撃符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //座標は適当
        coordinate = new Coordinate(10, 7);
        pattern = EnemyAIPattern.SEARCH;

        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //5.魔理沙
        enemy = enemyDatabase.FindByName("魔理沙");
        enemy = enemy.Clone();
        lv = 3;
        weapon = weaponDatabase.FindByName("新聞紙の霊符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップアイテムをセット
        Potion potion = potionDatabase.FindByName("結界の欠片");
        item = new Item(potion);
        enemy.SetDropItem(item);

        carryItem.Add(item);

        //座標は適当
        coordinate = new Coordinate(10, 13);
        pattern = EnemyAIPattern.REACT;

        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //210301 宝箱を設置する ステージ1は宝箱が無いので、これはテスト用
        List<TreasureBox> treasureList = new List<TreasureBox>();

        Stage stage = new Stage(chapter ,isUnitSelectRequired ,entryUnitCount , entryUnitCoordinates,
            enemyList, winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ2 夜の森
        chapter = Chapter.STAGE2;

        isUnitSelectRequired = false;

        entryUnitCount = 3;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(3, 1);
        entryUnitCoordinates.Add(coordinate1);

        //魔理沙が参戦
        coordinate = new Coordinate(2, 2);
        entryUnitCoordinates.Add(coordinate);

        //文ちゃんが参戦
        coordinate = new Coordinate(1, 1);
        entryUnitCoordinates.Add(coordinate);

        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REIMU_LOSE;

        storyText = "異変の原因を突き止める為、直感を頼りに湖の方向へ出発する霊夢と魔理沙。\n\n" +
            "夜道はいたずら好きな妖精や妖怪が良く現れる。\n\n" +
            "まずは肩慣らしも兼ねて、二人は特に理由も無く退治を始めるのであった。";


        //敵は10体、ルナで13体 Lv5、Lv6
        enemyList = new List<Enemy>();
        treasureList = new List<TreasureBox>();
        //1.ルーミア
        enemy = enemyDatabase.FindByName("ルーミア");
        enemy = enemy.Clone();
        lv = 8;
        weapon = weaponDatabase.FindByName("河童の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップアイテム 金塊
        Tool tool = toolDatabase.FindByName("金塊");
        item = new Item(tool);
        enemy.SetDropItem(item);
        carryItem.Add(item);

        //座標
        coordinate = new Coordinate(18, 18);
        pattern = EnemyAIPattern.BOSS;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //2.ひまわり妖精
        enemy = enemyDatabase.FindByName("ひまわり妖精");
        enemy = enemy.Clone();
        lv = 6;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップアイテム 天人の桃
        potion = potionDatabase.FindByName("天人の桃");
        item = new Item(potion);
        enemy.SetDropItem(item);
        carryItem.Add(item);

        //座標
        coordinate = new Coordinate(15, 16);
        pattern = EnemyAIPattern.REACT;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //3.妖精
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = 6;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(2, 11);
        pattern = EnemyAIPattern.REACT;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //4.妖精
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = 5;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(15, 12);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //5.妖精
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = 5;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(12, 9);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //6.妖精
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = 5;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(11, 16);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //7.毛玉
        enemy = enemyDatabase.FindByName("毛玉");
        enemy = enemy.Clone();
        lv = 6;
        weapon = weaponDatabase.FindByName("体当たり");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(13, 15);
        pattern = EnemyAIPattern.REACT;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //8.毛玉
        enemy = enemyDatabase.FindByName("毛玉");
        enemy = enemy.Clone();
        lv = 6;
        weapon = weaponDatabase.FindByName("体当たり");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(14, 14);
        pattern = EnemyAIPattern.REACT;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //9.毛玉
        enemy = enemyDatabase.FindByName("毛玉");
        enemy = enemy.Clone();
        lv = 5;
        weapon = weaponDatabase.FindByName("体当たり");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(8, 4);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //10.毛玉
        enemy = enemyDatabase.FindByName("毛玉");
        enemy = enemy.Clone();
        lv = 5;
        weapon = weaponDatabase.FindByName("体当たり");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(6, 6);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        ///宝箱
        //1.河童の術符
        coordinate = new Coordinate(2, 13);
        weapon = weaponDatabase.FindByName("河童の術符");
        item = new Item(weapon);

        TreasureBox treasure = new TreasureBox(item, coordinate);
        treasureList.Add(treasure);

        //2.博麗の癒符
        coordinate = new Coordinate(9, 11);
        weapon = weaponDatabase.FindByName("博麗の癒符");
        item = new Item(weapon);

        treasure = new TreasureBox(item, coordinate);
        treasureList.Add(treasure);

        //3.河童の技術書
        coordinate = new Coordinate(18, 4);
        potion = potionDatabase.FindByName("河童の技術書");
        item = new Item(potion);

        treasure = new TreasureBox(item, coordinate);
        treasureList.Add(treasure);


        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);


        //ステージ3 霧の湖(前編)
        chapter = Chapter.STAGE3;

        isUnitSelectRequired = true;

        entryUnitCount = 4;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(9, 1);
        entryUnitCoordinates.Add(coordinate1);

        //魔理沙が参戦
        coordinate = new Coordinate(10, 2);
        entryUnitCoordinates.Add(coordinate);

        //文ちゃんが参戦
        coordinate = new Coordinate(12, 2);
        entryUnitCoordinates.Add(coordinate);

        //ルーミアが参戦
        coordinate = new Coordinate(13, 1);
        entryUnitCoordinates.Add(coordinate);

        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REIMU_LOSE;

        storyText = "異変の原因を求めて向かった湖は濃い霧に包まれていた。\n\n" +
            "人里離れた湖に、夏は水場を求めて多くの妖怪や妖精が集まる。\n\n" +
            "退屈を持て余して襲い掛かる妖精達を霊夢達は蹴散らしていく。";

        //敵は12体、ルナで15体 Lv8～9
        enemyList = new List<Enemy>();
        treasureList = new List<TreasureBox>();
        //1.大妖精
        enemy = enemyDatabase.FindByName("大妖精");
        enemy = enemy.Clone();
        lv = 11;
        weapon = weaponDatabase.FindByName("河童の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップアイテム 妖精の癒符
        weapon = weaponDatabase.FindByName("妖精の癒符");
        item = new Item(weapon);
        enemy.SetDropItem(item);
        carryItem.Add(item);

        //座標
        coordinate = new Coordinate(18, 12);
        pattern = EnemyAIPattern.BOSS;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //2.ひまわり妖精
        enemy = enemyDatabase.FindByName("ひまわり妖精");
        enemy = enemy.Clone();
        lv = 9;
        weapon = weaponDatabase.FindByName("河童の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップアイテム 金塊
        tool = toolDatabase.FindByName("勝利のビール");
        item = new Item(tool);
        enemy.SetDropItem(item);
        carryItem.Add(item);

        //座標
        coordinate = new Coordinate(10, 15);
        pattern = EnemyAIPattern.WAIT;
        enemy.SetActionTurn(5);

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //3.ひまわり妖精
        enemy = enemyDatabase.FindByName("ひまわり妖精");
        enemy = enemy.Clone();
        lv = 9;
        weapon = weaponDatabase.FindByName("河童の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(17, 11);
        pattern = EnemyAIPattern.WAIT;
        enemy.SetActionTurn(5);

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //4.妖精
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = 8;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(9, 14);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //5.妖精
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = 8;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(10, 13);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //6.妖精
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = 8;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(11, 14);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //7.幽霊
        enemy = enemyDatabase.FindByName("幽霊");
        enemy = enemy.Clone();
        lv = 8;
        weapon = weaponDatabase.FindByName("香霖堂の霊符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(9, 9);


        //行動パターン 5ターン目から行動
        pattern = EnemyAIPattern.WAIT;
        enemy.SetActionTurn(5);

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //8.幽霊
        enemy = enemyDatabase.FindByName("幽霊");
        enemy = enemy.Clone();
        lv = 8;
        weapon = weaponDatabase.FindByName("香霖堂の霊符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(11, 9);
        pattern = EnemyAIPattern.WAIT;
        enemy.SetActionTurn(5);

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //9.幽霊
        enemy = enemyDatabase.FindByName("幽霊");
        enemy = enemy.Clone();
        lv = 8;
        weapon = weaponDatabase.FindByName("香霖堂の霊符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(10, 10);
        pattern = EnemyAIPattern.WAIT;
        enemy.SetActionTurn(5);

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //10.妖精(追加)
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = 8;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(15, 10);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //11.毛玉
        enemy = enemyDatabase.FindByName("毛玉");
        enemy = enemy.Clone();
        lv = 8;
        weapon = weaponDatabase.FindByName("体当たり");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(19, 2);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //12.毛玉
        enemy = enemyDatabase.FindByName("毛玉");
        enemy = enemy.Clone();
        lv = 8;
        weapon = weaponDatabase.FindByName("体当たり");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(2, 3);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //13.毛玉
        enemy = enemyDatabase.FindByName("毛玉");
        enemy = enemy.Clone();
        lv = 8;
        weapon = weaponDatabase.FindByName("体当たり");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(1, 2);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        ///宝箱
        //1.河童の撃符
        coordinate = new Coordinate(3, 2);
        weapon = weaponDatabase.FindByName("河童の撃符");
        item = new Item(weapon);

        treasure = new TreasureBox(item, coordinate);
        treasureList.Add(treasure);

        //2.妖精退治の術符
        coordinate = new Coordinate(19, 11);
        weapon = weaponDatabase.FindByName("妖精退治の術符");
        item = new Item(weapon);

        treasure = new TreasureBox(item, coordinate);
        treasureList.Add(treasure);

        //3.回避のお守り
        coordinate = new Coordinate(12, 14);
        Accessory accessory = accessoryDatabase.FindByName("回避のお守り");
        item = new Item(accessory);

        treasure = new TreasureBox(item, coordinate);
        treasureList.Add(treasure);


        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList, winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ4 霧の湖(後編)
        chapter = Chapter.STAGE4;

        isUnitSelectRequired = false;

        entryUnitCount = 5;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(17, 2);
        entryUnitCoordinates.Add(coordinate1);

        //魔理沙が参戦
        coordinate = new Coordinate(16, 3);
        entryUnitCoordinates.Add(coordinate);

        //文ちゃんが参戦
        coordinate = new Coordinate(18, 3);
        entryUnitCoordinates.Add(coordinate);

        //ルーミアが参戦
        coordinate = new Coordinate(16, 1);
        entryUnitCoordinates.Add(coordinate);

        //ルーミアが参戦
        coordinate = new Coordinate(18, 1);
        entryUnitCoordinates.Add(coordinate);

        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REIMU_LOSE;

        storyText = "濃い霧の為、湖で目的地の洋館を見つけられずに迷う霊夢達。\n\n" +
        "季節は夏であるにも関わらず、強い冷気を感じた霊夢達の前に現れたのは、\n" + 
        "湖に住む妖精の中でも格別に強い力を持つ妖精であった。";


        //敵は15体、ルナで18体？ Lv11～12
        enemyList = new List<Enemy>();
        treasureList = new List<TreasureBox>();
        //1.チルノ
        enemy = enemyDatabase.FindByName("チルノ");
        enemy = enemy.Clone();
        lv = 14;
        weapon = weaponDatabase.FindByName("氷つぶて");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップアイテム 妖精の癒符
        potion = potionDatabase.FindByName("妖精の果実");
        item = new Item(potion);
        enemy.SetDropItem(item);
        carryItem.Add(item);

        //座標
        coordinate = new Coordinate(3, 17);
        pattern = EnemyAIPattern.BOSS;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //2.ひまわり妖精
        enemy = enemyDatabase.FindByName("ひまわり妖精");
        enemy = enemy.Clone();
        lv = 12;
        weapon = weaponDatabase.FindByName("河童の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップアイテム 仙人の癒符
        weapon = weaponDatabase.FindByName("仙人の癒符");
        item = new Item(weapon);
        enemy.SetDropItem(item);
        carryItem.Add(item);

        //座標
        coordinate = new Coordinate(6, 16);
        pattern = EnemyAIPattern.WAIT;
        enemy.SetActionTurn(5);

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //3.ひまわり妖精
        enemy = enemyDatabase.FindByName("ひまわり妖精");
        enemy = enemy.Clone();
        lv = 12;
        weapon = weaponDatabase.FindByName("河童の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップアイテム 仙人の癒符
        tool = toolDatabase.FindByName("勝利のビール");
        item = new Item(tool);
        enemy.SetDropItem(item);
        carryItem.Add(item);

        
        

        //ドロップなし

        //座標
        coordinate = new Coordinate(4, 14);
        pattern = EnemyAIPattern.WAIT;
        enemy.SetActionTurn(5);

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //4.ひまわり妖精
        enemy = enemyDatabase.FindByName("ひまわり妖精");
        enemy = enemy.Clone();
        lv = 11;
        weapon = weaponDatabase.FindByName("河童の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(5, 15);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //5.妖精
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = lv = 11;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(17, 13);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);



        //6.妖精
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = 11;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(18, 12);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //7.妖精
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = 11;
        weapon = weaponDatabase.FindByName("香霖堂の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(19, 13);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //8.幽霊
        enemy = enemyDatabase.FindByName("幽霊");
        enemy = enemy.Clone();
        lv = 12;
        weapon = weaponDatabase.FindByName("河童の霊符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(7, 13);
        pattern = EnemyAIPattern.WAIT;
        enemy.SetActionTurn(3);

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //9.幽霊
        enemy = enemyDatabase.FindByName("幽霊");
        enemy = enemy.Clone();
        lv = 11;
        weapon = weaponDatabase.FindByName("香霖堂の霊符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(8, 14);
        pattern = EnemyAIPattern.WAIT;
        enemy.SetActionTurn(3);

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //10.幽霊
        enemy = enemyDatabase.FindByName("幽霊");
        enemy = enemy.Clone();
        lv = 11;
        weapon = weaponDatabase.FindByName("香霖堂の霊符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(6, 12);
        pattern = EnemyAIPattern.WAIT;
        enemy.SetActionTurn(3);

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //11.毛玉
        enemy = enemyDatabase.FindByName("毛玉");
        enemy = enemy.Clone();
        lv = 12;
        weapon = weaponDatabase.FindByName("体当たり");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(3, 7);
        pattern = EnemyAIPattern.REACT;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);


        //11.毛玉
        enemy = enemyDatabase.FindByName("毛玉");
        enemy = enemy.Clone();
        lv = 11;
        weapon = weaponDatabase.FindByName("体当たり");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(5, 7);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //12.毛玉
        enemy = enemyDatabase.FindByName("毛玉");
        enemy = enemy.Clone();
        lv = 11;
        weapon = weaponDatabase.FindByName("体当たり");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(5, 7);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //14.毛玉
        enemy = enemyDatabase.FindByName("毛玉");
        enemy = enemy.Clone();
        lv = 11;
        weapon = weaponDatabase.FindByName("体当たり");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(3, 5);
        pattern = EnemyAIPattern.SEARCH;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        //15.妖精
        enemy = enemyDatabase.FindByName("妖精");
        enemy = enemy.Clone();
        lv = 12;
        weapon = weaponDatabase.FindByName("河童の術符");
        item = new Item(weapon);
        item.isEquip = true;

        carryItem = new List<Item>();
        carryItem.Add(item);

        //ドロップなし

        //座標
        coordinate = new Coordinate(18, 12);
        pattern = EnemyAIPattern.REACT;

        //初期化
        enemy.StageInit(lv, coordinate, carryItem, pattern);
        enemyList.Add(enemy);

        ///宝箱
        //1.大きな金塊
        coordinate = new Coordinate(0, 7);
        tool = toolDatabase.FindByName("大きな金塊");
        item = new Item(tool);

        treasure = new TreasureBox(item, coordinate);
        treasureList.Add(treasure);

        //2.幽霊退治の霊符
        coordinate = new Coordinate(0, 15);
        weapon = weaponDatabase.FindByName("幽霊退治の霊符");
        item = new Item(weapon);

        treasure = new TreasureBox(item, coordinate);
        treasureList.Add(treasure);

        //3.仙人の撃符
        coordinate = new Coordinate(19, 12);
        weapon = weaponDatabase.FindByName("仙人の撃符");
        item = new Item(weapon);

        treasure = new TreasureBox(item, coordinate);
        treasureList.Add(treasure);

        //3.仙人の術符
        coordinate = new Coordinate(16, 16);
        weapon = weaponDatabase.FindByName("仙人の術符");
        item = new Item(weapon);

        treasure = new TreasureBox(item, coordinate);
        treasureList.Add(treasure);

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ5 霧の湖(外伝)
        chapter = Chapter.STAGE5;

        isUnitSelectRequired = false;

        entryUnitCount = 6;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REIMU_LOSE;

        storyText = "妖精達のいたずらは、時として同じ妖精もターゲットになる事が有る。\n\n" +
            "霧の湖で出会ったチルノと大妖精に、\n霊夢達は異変解決の為、取引を持ち掛ける。";

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList, winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ6 紅魔館の庭園
        chapter = Chapter.STAGE6;

        isUnitSelectRequired = false;

        entryUnitCount = 6;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REIMU_LOSE;

        storyText = "霧の湖を抜けた霊夢達の前に現れたのは異様な妖気を放つ深紅の洋館であった。\n\n" +
            "異変の原因を突き止める為、押し入ろうとする霊夢達に妖精メイドと門番の妖怪が立ち塞がる。";

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //大図書館(前編)
        chapter = Chapter.STAGE7;

        isUnitSelectRequired = true;

        entryUnitCount = 6;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REIMU_LOSE;

        storyText = "妖精メイド達の反撃が激しく、窓から紅魔館の内部に侵入して大図書館を抜ける霊夢達。\n\n" +
            "そこは侵入者を撃退する為の魔導書や妖精メイドが待ち受ける難所であった。";

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //大図書館(後編)
        chapter = Chapter.STAGE8;

        isUnitSelectRequired = true;

        entryUnitCount = 6;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REIMU_LOSE;

        storyText = "小悪魔を撃退した霊夢達の前に大図書館の主が現れる。\n\n" +
            "苛烈さを増す侵入者への攻撃を霊夢達は迎え撃つ。";

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);
        
        //紅魔館ホール
        chapter = Chapter.STAGE9;

        isUnitSelectRequired = true;

        entryUnitCount = 7;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        Coordinate coordinate7 = new Coordinate(9, 4);
        entryUnitCoordinates.Add(coordinate7);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REIMU_LOSE;

        storyText = "大図書館を抜けて最上階を目前にした霊夢達。\n\n" +
            "そこに立ち塞がるのは洋館の妖精メイド達を従える人間のメイド長であった。\n\n" +
            "容赦なく弾幕を放つ\n霊夢達であったが・・・";

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //紅魔館ホール2
        chapter = Chapter.STAGE10;

        isUnitSelectRequired = true;

        entryUnitCount = 7;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate7 = new Coordinate(9, 4);
        entryUnitCoordinates.Add(coordinate7);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REIMU_LOSE;

        storyText = "無敵と思われた咲夜の能力に気付き、霊夢達は攻略の糸口を掴む。\n\n" +
            "異変の原因まで後わずか、\n力を振り絞る霊夢達と紅魔館の総力を賭けた戦いが始まる。";

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList, winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //紅魔館最上階(前編)
        chapter = Chapter.STAGE11;

        isUnitSelectRequired = true;

        entryUnitCount = 7;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate7 = new Coordinate(9, 4);
        entryUnitCoordinates.Add(coordinate7);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REIMU_LOSE;

        storyText = "メイド長、咲夜を破り異様な妖気を発する原因へ急ぐ霊夢達。\n\n" +
            "霊夢達の前に現れたのは、既に満身創痍となっても主人の為、\n紅魔館の残存兵力を従えて立ち塞がる咲夜の姿であった。";

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //紅魔館最上階(前編)
        chapter = Chapter.STAGE12;

        isUnitSelectRequired = true;

        entryUnitCount = 7;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate7 = new Coordinate(9, 4);
        entryUnitCoordinates.Add(coordinate7);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REIMU_LOSE;

        storyText = "昼も夜も無い館に、「彼女」は、いた。\n永い夜が今始まる。";


        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ここから紅魔ルート

        //ステージ1 博麗神社
        chapter = Chapter.R_STAGE1;

        isUnitSelectRequired = false;

        entryUnitCount = 2;

        isReimuRoute = false;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REMILIA_LOSE;

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ2 夜の森
        chapter = Chapter.R_STAGE2;

        isUnitSelectRequired = false;

        entryUnitCount = 2;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REMILIA_LOSE;

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ3 霧の湖(前編)
        chapter = Chapter.R_STAGE3;

        isUnitSelectRequired = false;

        entryUnitCount = 4;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REMILIA_LOSE;

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ4 霧の湖(後編)
        chapter = Chapter.R_STAGE4;

        isUnitSelectRequired = false;

        entryUnitCount = 5;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REMILIA_LOSE;

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ5 紅魔館の庭園
        chapter = Chapter.R_STAGE5;

        isUnitSelectRequired = false;

        entryUnitCount = 6;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REMILIA_LOSE;

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ6 大図書館(前編)
        chapter = Chapter.R_STAGE6;

        isUnitSelectRequired = true;

        entryUnitCount = 6;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REMILIA_LOSE;

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ7 大図書館(後編)
        chapter = Chapter.R_STAGE7;

        isUnitSelectRequired = true;

        entryUnitCount = 6;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REMILIA_LOSE;

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ8 紅魔館ホール
        chapter = Chapter.R_STAGE8;

        isUnitSelectRequired = true;

        entryUnitCount = 7;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate7 = new Coordinate(9, 4);
        entryUnitCoordinates.Add(coordinate7);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REMILIA_LOSE;

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ステージ9 紅魔館最上階(前編)
        chapter = Chapter.R_STAGE9;

        isUnitSelectRequired = true;

        entryUnitCount = 7;

        //座標
        entryUnitCoordinates = new List<Coordinate>();
        coordinate1 = new Coordinate(4, 4);
        entryUnitCoordinates.Add(coordinate1);
        coordinate = new Coordinate(4, 5);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(5, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(6, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(7, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate = new Coordinate(8, 4);
        entryUnitCoordinates.Add(coordinate);
        coordinate7 = new Coordinate(9, 4);
        entryUnitCoordinates.Add(coordinate7);
        winCondition = WinCondition.BOSS;
        loseCondition = LoseCondition.REMILIA_LOSE;

        stage = new Stage(chapter, isUnitSelectRequired, entryUnitCount, entryUnitCoordinates,
            enemyList ,winCondition, loseCondition, storyText, isReimuRoute, treasureList);
        stageDatabase.stageList.Add(stage);

        //ファイル書き出し Resources配下に作る
        AssetDatabase.CreateAsset(stageDatabase, "Assets/Resources/stageDatabase.asset");
    }

}

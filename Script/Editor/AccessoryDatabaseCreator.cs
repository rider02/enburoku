using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

/// <summary>
/// 210218 アクセサリ一覧ScriptabeleObject製作用クラス
/// Unity上部の「Create」から使用する
/// </summary>
public static class AccessoryDatabaseCreator
{
    //MenuItemを付ける事で上部メニューに項目を追加
    [MenuItem("Create/AccessoryDatabase")]
    private static void Create()
    {
        //ScriptableObjectのインスタンスを作成
        AccessoryDatabase accessoryDatabase = ScriptableObject.CreateInstance<AccessoryDatabase>();

        //データを設定
        string name = "経験の熊手";
        string text = "獲得経験値1.5倍";
        AccessoryEffectType effect = AccessoryEffectType.EXPUP; //アクセサリの効果

        int delay = 0;      //重さ
        int amount = 0;     //補正値 経験の熊手はfloatになると面倒なので別個計算
        int price = 5000;   //値段
        bool isNfs = true;  //NotForSale 非売品

        Accessory accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);


        name = "流し雛";
        text = "特効と必殺無効";
        effect = AccessoryEffectType.CRITICAL_AND_SLAYER_INVALID;

        delay = 0;
        amount = 0;
        price = 5000;
        isNfs = true;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "速さのお守り";
        text = "速さ+2";
        effect = AccessoryEffectType.AGIUP;
        isNfs = false;

        delay = 0;
        amount = 2;
        price = 3000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "回避のお守り";
        text = "回避+10";
        effect = AccessoryEffectType.EVASIONUP;
        isNfs = false;

        delay = 0;
        amount = 10;
        price = 3000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "必殺のお守り";
        text = "必殺+10";
        effect = AccessoryEffectType.CRITICALUP;
        isNfs = false;

        delay = 0;
        amount = 10;
        price = 3000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "命中のお守り";
        text = "命中+10";
        effect = AccessoryEffectType.HITUP;
        isNfs = false;

        delay = 0;
        amount = 10;
        price = 3000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "妖精安全の札";
        text = "妖精特効無効 遠防+2";
        effect = AccessoryEffectType.FAIRY_SLAYER_INVALID;
        isNfs = true;

        delay = 0;
        amount = 2;
        price = 5000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "人間安全の札";
        text = "人間特効無効 遠防+2";
        effect = AccessoryEffectType.HUMAN_SLAYER_INVALID;
        isNfs = true;

        delay = 0;
        amount = 2;
        price = 5000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "妖怪安全の札";
        text = "妖怪特効無効 遠防+2";
        effect = AccessoryEffectType.YOUKAI_SLAYER_INVALID;
        isNfs = true;

        delay = 0;
        amount = 2;
        price = 5000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "癒しのお祓い棒";
        text = "回復量+10";
        effect = AccessoryEffectType.HEALUP;

        delay = 0;
        amount = 10;
        price = 3000;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "風祝の髪飾り";
        text = "遠攻+3";
        effect = AccessoryEffectType.LATKUP;

        delay = 0;
        amount = 3;
        price = 3000;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "新聞紙の形代";
        text = "遠防+1";
        effect = AccessoryEffectType.LDEFUP;

        delay = 0;
        amount = 1;
        price = 500;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "博麗の形代";
        text = "遠防+2";
        effect = AccessoryEffectType.LDEFUP;

        delay = 0;
        amount = 1;
        price = 1000;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "仙人の形代";
        text = "遠防+3";
        effect = AccessoryEffectType.LDEFUP;

        delay = 0;
        amount = 1;
        price = 1500;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "賢者の形代";
        text = "遠防+4";
        effect = AccessoryEffectType.LDEFUP;

        delay = 0;
        amount = 1;
        price = 2000;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        //ファイル書き出し Resources配下に作る
        AssetDatabase.CreateAsset(accessoryDatabase, "Assets/Resources/accessoryDatabase.asset");
    }

}

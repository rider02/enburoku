using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

/// <summary>
/// 210216
/// 回復、ステータスアップアイテムのScriptabeleObject製作用クラス
/// Unity上部の「Create」から使用する
/// </summary>
public static class PotionDatabaseCreator
{
    [MenuItem("Create/PotionDatabase")]
    private static void Create()
    {
        PotionDatabase potionDatabase = ScriptableObject.CreateInstance<PotionDatabase>();

        //名前
        string potionName = "霧雨の傷薬";
        int amount = 10;
        int useCount = 3;
        int price = 300;
        string annotationtext = "使うとHPが10回復する";
        PotionType type = PotionType.HEAL;
        bool isRequirePharmacy = false;

        Potion potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "永遠亭の傷薬";
        amount = 20;
        useCount = 3;
        price = 600;
        annotationtext = "使うとHPが20回復する";
        type = PotionType.HEAL;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "胡蝶夢丸";
        amount = 99;
        useCount = 3;
        price = 1200;
        annotationtext = "使うとHPが全回復する";
        type = PotionType.HEAL;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "薬箱";
        amount = 10;
        useCount = 3;
        price = 400;
        annotationtext = "薬売りのみ使用可能 隣接する仲間のHPを10回復";
        type = PotionType.HEAL;
        isRequirePharmacy = true;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "永遠亭の薬箱";
        amount = 20;
        useCount = 3;
        price = 900;
        annotationtext = "薬売りのみ使用可能 隣接する仲間のHPを20回復";
        type = PotionType.HEAL;
        isRequirePharmacy = true;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "仙人の丹";
        amount = 0;
        useCount = 3;
        price = 600;
        annotationtext = "遠防+7 毎ターン2ずつ減少";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "妖精の果実";
        amount = 5;
        useCount = 1;
        price = 2500;
        annotationtext = "HP+5";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "鬼の大吟醸";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "近攻+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "魔女の魔導書";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "遠攻+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "河童の技術書";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "技+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "天魔の羽団扇";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "速さ+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "木彫りの仏像";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "幸運+4";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "天人の桃";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "近防+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "結界の欠片";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "遠防+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "不思議な隙間";
        amount = 1;
        useCount = 1;
        price = 2500;
        annotationtext = "移動+1";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        //ファイル書き出し Resources配下に作る
        AssetDatabase.CreateAsset(potionDatabase, "Assets/Resources/potionDatabase.asset");
    }
}

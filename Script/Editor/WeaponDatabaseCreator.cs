using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 武器のアセットファイルを作ってくれるクラス
/// Unity上部の「Create」から使用する
/// </summary>
public static class WeaponDatabaseCreator
{

    //MenuItemを付ける事で上部メニューに項目を追加
    [MenuItem("Create/WeaponDatabase")]
    private static void Create()
    {
        //ScriptableObjectのインスタンスを作成
        WeaponDatabase weaponDatabase = ScriptableObject.CreateInstance<WeaponDatabase>();

        //データを設定
        //■ショット
        //新聞紙の術符
        int[] parameter = new int[] { 3, 100, 0, 4, 50, 260 };
        string annotationtext = "天狗が置いて行った新聞紙";
        string featureText = null;
        string weaponName = "新聞紙の術符";
        //非売品フラグ
        bool nfs = false;
        int range = 2;
        bool isPrivate = false;
        string ownerName = null;

        StatusType statusType = StatusType.NONE;
        int amount = 0;

        //210219 特効を追加
        RaceType slayer = RaceType.NONE;

        //210220 追撃不可フラグを追加
        bool isChaseInvalid = false;

        Weapon weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.E, parameter ,range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //香霖堂の術符
        parameter = new int[] { 5, 90, 0, 5, 40, 520 };
        annotationtext = "香霖堂に売られている標準的な性能の札";
        featureText = null;
        weaponName = "香霖堂の術符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.E, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //河童の術符
        parameter = new int[] { 8, 85, 0, 10, 50, 910 };
        annotationtext = "河童に作られた発射が遅いが威力が有る札";
        featureText = null;
        weaponName = "河童の術符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;
        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.D, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //仙人の術符
        parameter = new int[] { 12, 90, 0, 8, 30, 1410 };
        annotationtext = "仙人に作られた良質な札";
        featureText = null;
        weaponName = "仙人の術符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;
        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.B, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //賢者の術符
        parameter = new int[] { 9, 85, 0, 12, 30, 2100 };
        annotationtext = "妖怪の賢者に作られた札";
        featureText = "自分から攻撃した時2回攻撃";
        weaponName = "賢者の術符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.B, parameter, range, nfs, true, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //死神の術符
        parameter = new int[] { 8, 85, 25, 8, 20, 1500 };
        annotationtext = "死神の妖気が込められた札";
        featureText = "必殺率が高い";
        weaponName = "死神の術符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.C, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //妖精退治の術符
        parameter = new int[] { 8, 90, 0, 11, 20, 1500 };
        annotationtext = "妖精に高い威力を発揮する札";
        featureText = "妖精特効";
        weaponName = "妖精退治の術符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.FAIRY;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.D, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //月人の術符
        parameter = new int[] { 13, 90, 0, 9, 25, 4000 };
        annotationtext = "幻想郷に存在しない技術で作られた高性能な札";
        featureText = null;
        weaponName = "月人の術符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.A, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //ホーミングアミュレット
        parameter = new int[] { 7, 100, 5, 5, 40, 2000 };
        annotationtext = "霊夢が妖怪退治に使う弾幕";
        featureText = "霊夢専用 命中率が高い";
        weaponName = "ホーミングアミュレット";
        nfs = true;
        range = 2;
        isPrivate = true;
        ownerName = "霊夢";

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.E, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //氷つぶて
        parameter = new int[] { 6, 90, 5, 3, 40, 2000 };
        annotationtext = "チルノが弾幕に使用する氷つぶて";
        featureText = "チルノ専用 遠近両用";
        weaponName = "氷つぶて";
        nfs = true;
        range = 2;
        isPrivate = true;
        ownerName = "チルノ";

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.E, parameter, range, nfs, true, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //玉兎の弾丸
        parameter = new int[] { 5, 95, 30, 5, 40, 2000 };
        annotationtext = "玉兎が一般的に使用する弾丸";
        featureText = "鈴仙専用 必殺率が高い 射程が長い";
        weaponName = "玉兎の弾丸";
        nfs = true;
        range = 3;
        isPrivate = true;
        ownerName = "鈴仙";

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.E, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //先代巫女のお札
        parameter = new int[] { 15, 90, 10, 7, 30, 10000 };
        annotationtext = "先代博麗の巫女が使っていたとされるお札";
        featureText = "霊夢専用 遠距離防御+5";
        weaponName = "先代巫女のお札";
        nfs = true;
        range = 2;
        isPrivate = true;
        ownerName = "霊夢";

        statusType = StatusType.LDEF;
        amount = 5;

        slayer = RaceType.NONE;
        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.E, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //天狗のカメラ
        parameter = new int[] { 10, 80, 0, 7, 30, 10000 };
        annotationtext = "文が取材用に愛用するカメラ";
        featureText = "文専用 追撃不可 遠近両用 相手からの反撃を受けない";
        weaponName = "天狗のカメラ";
        nfs = true;
        range = 2;
        isPrivate = true;
        ownerName = "文";

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;
        isChaseInvalid = true;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.SHOT, SkillLevel.E, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);



        //■レーザー
        //新聞紙の霊符
        parameter = new int[] { 4, 90, 0, 5, 50, 280 };
        annotationtext = "天狗が置いて行った新聞紙";
        featureText = null;
        weaponName = "新聞紙の霊符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.LASER, SkillLevel.E, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //香霖堂の霊符
        parameter = new int[] { 6, 80, 0, 6, 40, 560 };
        annotationtext = "香霖堂に売られている標準的な性能の札";
        featureText = null;
        weaponName = "香霖堂の霊符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.LASER, SkillLevel.E, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //河童の霊符
        parameter = new int[] { 9, 75, 0, 11, 50, 910 };
        annotationtext = "河童に作られた発射が遅いが威力が有る札";
        featureText = null;
        weaponName = "河童の霊符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.LASER, SkillLevel.D, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //仙人の霊符
        parameter = new int[] { 13, 80, 0, 9, 30, 1560 };
        annotationtext = "仙人に作られた良質な札";
        featureText = null;
        weaponName = "仙人の霊符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.LASER, SkillLevel.B, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //賢者の霊符
        parameter = new int[] { 10, 75, 0, 13, 30, 2200 };
        annotationtext = "妖怪の賢者に作られた札";
        featureText = "自分から攻撃した時2回攻撃";
        weaponName = "賢者の霊符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.LASER, SkillLevel.B, parameter, range, nfs, true, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //死神の霊符
        parameter = new int[] { 9, 80, 25, 11, 20, 1700 };
        annotationtext = "死神の妖気が込められた札";
        featureText = "必殺率が高い";
        weaponName = "死神の霊符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.LASER, SkillLevel.C, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //幽霊退治の霊符
        parameter = new int[] { 8, 90, 0, 9, 25, 1700 };
        annotationtext = "幽霊に高い威力を発揮する札";
        featureText = "幽霊特効";
        weaponName = "幽霊退治の霊符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.GHOST;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.LASER, SkillLevel.D, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //月人の霊符
        parameter = new int[] { 15, 80, 0, 10, 25, 4000 };
        annotationtext = "幻想郷に存在しない技術で作られた高性能な札";
        featureText = null;
        weaponName = "月人の霊符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.LASER, SkillLevel.A, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //八卦炉
        parameter = new int[] { 8, 85, 10, 5, 40, 2000 };
        annotationtext = "魔理沙が愛用するマジックアイテム";
        featureText = "魔理沙専用 妖精特効";
        weaponName = "ミニ八卦炉";
        nfs = true;
        range = 2;
        isPrivate = true;
        ownerName = "魔理沙";

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.FAIRY;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.LASER, SkillLevel.E, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //七曜の魔導書
        parameter = new int[] { 12, 70, 15, 18, 10, 10000 };
        annotationtext = "パチュリーの魔導書";
        featureText = "パチュリー専用 遠距離攻撃 追撃不可 技+5";
        weaponName = "七曜の魔導書";
        nfs = true;
        range = 8;
        isPrivate = true;
        ownerName = "パチュリー";

        statusType = StatusType.DEX;
        amount = 5;

        slayer = RaceType.NONE;

        isChaseInvalid = true;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.LASER, SkillLevel.E, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //八卦炉・改
        parameter = new int[] { 17, 80, 10, 8, 30, 10000 };
        annotationtext = "魔理沙が愛用するマジックアイテム";
        featureText = "遠攻+5 妖精特効";
        weaponName = "ミニ八卦炉・改";
        nfs = true;
        range = 2;
        isPrivate = true;
        ownerName = "魔理沙";

        statusType = StatusType.LATK;
        amount = 5;

        slayer = RaceType.FAIRY;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.LASER, SkillLevel.E, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //■物理
        //新聞紙の撃符
        parameter = new int[] { 6, 85, 0, 6, 50, 300 };
        annotationtext = "天狗が置いて行った新聞紙";
        featureText = null;
        weaponName = "新聞紙の撃符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //香霖堂の撃符
        parameter = new int[] { 8, 75, 0, 7, 40, 600 };
        annotationtext = "香霖堂に売られている標準的な性能の札";
        featureText = null;
        weaponName = "香霖堂の撃符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //河童の撃符
        parameter = new int[] { 11, 70, 0, 12, 50, 1000 };
        annotationtext = "河童に作られた発射が遅いが威力が有る札";
        featureText = null;
        weaponName = "河童の撃符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.D, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //仙人の撃符
        parameter = new int[] { 16, 75, 0, 10, 30, 1740 };
        annotationtext = "仙人に作られた良質な札";
        featureText = null;
        weaponName = "仙人の撃符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.B, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //賢者の撃符
        parameter = new int[] { 12, 70, 0, 14, 30, 2400 };
        annotationtext = "妖怪の賢者に作られた札";
        featureText = "自分から攻撃した時2回攻撃";
        weaponName = "賢者の撃符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.B, parameter, range, nfs, true, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //死神の撃符
        parameter = new int[] { 11, 75, 25, 12, 20, 1900 };
        annotationtext = "死神の妖気が込められた札";
        featureText = "必殺率が高い";
        weaponName = "死神の撃符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.C, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //付喪神退治の撃符
        parameter = new int[] { 10, 75, 0, 12, 25, 1800 };
        annotationtext = "付喪神に対して高い威力を発揮する札";
        featureText = "付喪神特効";
        weaponName = "付喪神退治の撃符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.TSUKUMOGAMI;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.D, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //月人の撃符
        parameter = new int[] { 17, 75, 0, 10, 25, 4000 };
        annotationtext = "幻想郷に存在しない技術で作られた高性能な札";
        featureText = null;
        weaponName = "月人の撃符";
        nfs = false;
        range = 2;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.A, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //お祓い棒
        parameter = new int[] { 7, 90, 10, 6, 40, 2000 };
        annotationtext = "霊夢の愛用するお祓い棒";
        featureText = "霊夢専用 妖怪特効 近距離攻撃";
        weaponName = "お祓い棒";
        nfs = true;
        range = 1;
        isPrivate = true;
        ownerName = "霊夢";

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.YOUKAI;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);


        //天狗の羽団扇
        parameter = new int[] { 7, 90, 5, 4, 40, 2000 };
        annotationtext = "文の羽団扇 強風を巻き起こす";
        featureText = "文専用 近距離攻撃";
        weaponName = "天狗の羽団扇";
        nfs = true;
        range = 1;
        isPrivate = true;
        ownerName = "文";

        statusType = StatusType.NONE;

        slayer = RaceType.NONE;

        isChaseInvalid = false;
        amount = 0;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //銀のナイフ
        parameter = new int[] { 8, 90, 10, 6, 40, 2000 };
        annotationtext = "咲夜が弾幕に使用するナイフ";
        featureText = "咲夜専用 人間特効 遠近両用";
        weaponName = "銀のナイフ";
        nfs = true;
        range = 2;
        isPrivate = true;
        ownerName = "咲夜";

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.HUMAN;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //拳
        parameter = new int[] { 2, 85, 10, 1, 70, 2000 };
        annotationtext = "美鈴の拳法";
        featureText = "美鈴専用 近距離攻撃 自分から攻撃時2回攻撃";
        weaponName = "拳";
        nfs = true;
        range = 1;
        isPrivate = true;
        ownerName = "美鈴";

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, true, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //グングニル
        parameter = new int[] { 19, 70, 10, 10, 30, 10000 };
        annotationtext = "レミリアが弾幕に使用する槍";
        featureText = "レミリア専用 遠近両用　運+10";
        weaponName = "グングニル";
        nfs = true;
        range = 2;
        isPrivate = true;
        ownerName = "レミリア";

        statusType = StatusType.LUK;
        amount = 10;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //レーヴァテイン
        parameter = new int[] { 23, 55, 40, 10, 30, 10000 };
        annotationtext = "フランドールが弾幕に使用する剣";
        featureText = "フランドール専用 近距離攻撃 近攻+5";
        weaponName = "レーヴァテイン";
        nfs = true;
        range = 1;
        isPrivate = true;
        ownerName = "フランドール";

        statusType = StatusType.CATK;
        amount = 5;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //先代巫女のお祓い棒
        parameter = new int[] { 16, 80, 10, 8, 30, 10000 };
        annotationtext = "先代博麗の巫女が使っていたとされるお祓い棒";
        featureText = "霊夢専用 近距離攻撃 妖怪特効 近攻+5";
        weaponName = "先代巫女のお祓い棒";
        nfs = true;
        range = 1;
        isPrivate = true;
        ownerName = "霊夢";

        statusType = StatusType.CATK;
        amount = 5;

        slayer = RaceType.YOUKAI;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //魔界メイドのナイフ
        parameter = new int[] { 6, 80, 10, 8, 30, 10000 };
        annotationtext = "魔界のメイドが使用していたとされるナイフ";
        featureText = "咲夜専用 自分から攻撃時2回攻撃 速+5";
        weaponName = "魔界メイドのナイフ";
        nfs = true;
        range = 2;
        isPrivate = true;
        ownerName = "咲夜";

        statusType = StatusType.AGI;
        amount = 5;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, true, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);


        //体当たり(敵専用)
        parameter = new int[] { 5, 75, 0, 5, 50, 10 };
        annotationtext = "敵専用";
        featureText = null;
        weaponName = "体当たり";
        nfs = true;
        range = 1;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //妖獣の牙(敵専用)
        parameter = new int[] { 11, 70, 0, 7, 50, 10 };
        annotationtext = "敵専用";
        featureText = null;
        weaponName = "妖獣の牙";
        nfs = true;
        range = 1;
        isPrivate = false;
        ownerName = null;

        statusType = StatusType.NONE;
        amount = 0;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.STRIKE, SkillLevel.E, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //癒符
        //妖精の癒符
        //200829 配列は威力、命中率、必殺率、重さ、耐久、値段
        parameter = new int[] { 8, 100, 0, 0, 30, 500 };
        annotationtext = "体力を少し回復する";
        featureText = null;
        weaponName = "博麗の癒符";
        nfs = false;
        range = 1;
        isPrivate = false;
        ownerName = null;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.HEAL, SkillLevel.E, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //仙人の癒符
        parameter = new int[] { 30, 100, 0, 0, 20, 1000 };
        annotationtext = "体力を大きく回復する";
        featureText = null;
        weaponName = "仙人の癒符";
        nfs = false;
        range = 1;
        isPrivate = false;
        ownerName = null;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.HEAL, SkillLevel.D, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //妖精の癒符
        parameter = new int[] { 8, 100, 0, 0, 15, 1500 };
        annotationtext = "離れた相手の体力を回復する";
        featureText = "射程 = 遠攻の1/2";
        weaponName = "妖精の癒符";
        nfs = false;
        range = 1;//遠攻 / 2
        isPrivate = false;
        ownerName = null;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.HEAL, SkillLevel.C, parameter, range, nfs, false, false,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //奇跡の癒符
        parameter = new int[] { 15, 100, 0, 0, 2, 4000 };
        annotationtext = "範囲内の味方全員のHPを回復";
        featureText = "範囲 = 遠攻の1/4";
        weaponName = "奇跡の癒符";
        nfs = true;
        range = 1; //遠攻 / 4
        isPrivate = false;
        ownerName = null;

        slayer = RaceType.NONE;

        isChaseInvalid = false;

        weapon = new Weapon(weaponName, annotationtext, featureText,
            WeaponType.HEAL, SkillLevel.A, parameter, range, nfs, false, true,
            isPrivate, ownerName, statusType, amount, slayer, isChaseInvalid);

        weaponDatabase.weaponList.Add(weapon);

        //ファイル書き出し Resources配下に作る
        AssetDatabase.CreateAsset(weaponDatabase, "Assets/Resources/weaponDatabase.asset");
    }

}
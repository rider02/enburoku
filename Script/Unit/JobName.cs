﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 職業一覧
/// </summary>
public enum JobName
{
    [StringValue("博麗神社の巫女。弾幕と回復でパーティを支える。")]
    巫女,

    [StringValue("自由奔放な巫女。敵の防御系スキルを無効化する能力を持つ。")]
    空飛ぶ不思議な巫女,

    [StringValue("多くの異変を解決した巫女。強力な弾幕と高い機動力を持つ。")]
    永遠の巫女,

    [StringValue("妖怪退治のエキスパート。弾幕とお払い棒であらゆる妖怪を退治する。")]
    結界の巫女,

    [StringValue("普通の魔法使い。宝箱や扉を開ける事が出来る。")]
    魔法使い,

    [StringValue("普通の魔法使い。火力と素早さに優れる。")]
    普通の黒魔法少女,

    [StringValue("多くの異変を解決した魔法使い。弾幕はパワー。")]
    恋の魔砲少女,

    [StringValue("妖怪退治のエキスパート。攻撃の回避と薬に長けており、継戦能力が高い。")]
    森の魔砲探偵,

    [StringValue("闇を操る妖怪。暗闇で攻撃を防ぎ、防御力に優れる。")]
    暗闇の妖怪,

    [StringValue("闇を操る妖怪。倒した相手の生命力を吸収出来る。")]
    宵闇の妖怪,

    [StringValue("封じられた大妖怪。高い攻撃力を持つが、自分を傷付けてしまう。")]
    封印の大妖怪,

    [StringValue("深淵に潜む妖怪。闇で敵の追撃を防ぎ、高い防御力を持つ。")]
    深淵の少女,

    [StringValue("高い知能を持つ妖精。仲間を回復する事が出来る。")]
    大妖精,

    [StringValue("高い知能を持つ妖精。応援で仲間の幸運を上げる事が出来る。")]
    ルーネイトエルフ,

    [StringValue("高位の妖精。応援で仲間の遠距離攻撃をサポート出来る。")]
    ティターニア,

    [StringValue("高位の妖精。あらゆる傷を癒す回復のエキスパート。")]
    四大精霊,

    [StringValue("強い力を持つ氷精。")]
    湖上の氷精,

    [StringValue("強い力を持つ氷精。凍える冷気で近くの敵をの回避率を下げる事が出来る。")]
    おてんば恋娘,

    [StringValue("妖精の範疇を超える力を持つ氷精。応援で仲間の近接攻撃をサポート出来る。")]
    最強の氷精,

    [StringValue("氷と冬の力が備わり最強に見える氷精。戦闘後、敵の移動を封じる。")]
    冬将軍,

    [StringValue("人里に最も近い天狗。素早さに優れ、宝箱や扉を開ける事が出来る。")]
    伝統の幻想ブン屋,

    [StringValue("幻想郷最速と呼ばれる妖怪。非常に高い素早さで敵を翻弄する。")]
    風神少女,

    [StringValue("天狗の組織内で暗躍する高位の妖怪。技と速さが高く、時折敵を一撃で倒す。")]
    天魔の密偵,

    [StringValue("地上の月の兎。高い命中率の弾幕と高性能な薬を使えるが、運が悪い。")]
    狂気の月の兎,

    [StringValue("月の賢者の弟子。薬で自分を強化する事が出来る。")]
    賢者の弟子,

    [StringValue("穢れ無き月人の従。傷を負うと性格が豹変する。")]
    綿月の戦士,

    門番,
    虹色の門番,
    悪魔の守護者,
    龍の末裔,

    小悪魔,
    インプ,
    サキュバス,
    グレモリィ,

    魔女,
    知識と日陰の少女,
    動かない大図書館,
    七曜の魔女,

    紅魔館のメイド,
    電光石火のメイド,
    完全で瀟洒な従者,
    銀の手品師,

    紅い悪魔,
    永遠に紅い幼き月,
    紅色のノクターナルデビル,

    紅のカタストロフ,

    //ここから敵専用クラス
    妖精,
    メイド妖精,
    毛玉,
    妖獣,
    魔導書,
    グリモワール,
    使い魔,
    ひまわり妖精,
    ハイフェアリー,
    ホブゴブリン,
    幽霊,
    怨霊,
    吸血コウモリ,
    ツパイ,
    妖怪,
    付喪神,
}

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

/// <summary>
/// レベルアップした時の感想ScriptabeleObject製作用クラス
/// Unity上部の「Create」から使用する
/// 各キャラ3種類必ず存在する
/// 0～2ピン : あまり良くない反応
/// 3～5ピン : 普通
/// 6～8ピン : 良い反応
/// TODO あと、成長が限界になった時の台詞が有っても良いかも
/// </summary>
public static class LvUpImpreCreator
{
    [MenuItem("Create/LvUpImpreDatabase")]
    private static void Create()
    {

        LvUpImpreDatabase lvUpImpreDatabase = ScriptableObject.CreateInstance<LvUpImpreDatabase>();

        //霊夢
        List<string> reimuImpre = new List<string>() { "体が重いな、修行を怠け過ぎたか・・・",
            "やっと体が温まってきた。\nこの調子で行くわよ！",
            "よし、もう手加減していられない！\nやられたい妖怪から掛かってこい！" };

        LvUpImpre reimu = new LvUpImpre("霊夢", reimuImpre);

        lvUpImpreDatabase.impreList.Add(reimu);

        //魔理沙
        List<string> marisaImpre = new List<string>() { "こんな筈じゃ・・・\n自分に腹が立つぜ。",
            "まあ、努力してるからこの位はな。",
            "私にこんな力が有ったとは・・・\n驚いたぜ。" };

        LvUpImpre marisa = new LvUpImpre("魔理沙", marisaImpre);

        lvUpImpreDatabase.impreList.Add(marisa);

        //ルーミア
        List<string> rumiaImpre = new List<string>() { "お腹すいちゃったよ・・・",
            "レベルアップ？そーなのかー",
            "もう我慢出来ないよ・・・\n食べていい人類どこかな～。" };

        LvUpImpre rumia = new LvUpImpre("ルーミア", rumiaImpre);
        lvUpImpreDatabase.impreList.Add(rumia);

        //大妖精
        List<string> daiyouseiImpre = new List<string>() { "・・・",
            "・・・！",
            "・・・！・・・！" };

        LvUpImpre daiyousei = new LvUpImpre("大妖精", daiyouseiImpre);
        lvUpImpreDatabase.impreList.Add(daiyousei);

        //ツィルノ
        List<string> chirnoImpre = new List<string>() { "飽きてきたな・・・\n蛙を凍らせて遊ぼうっと。",
            "あたいったら最強ね！",
            "今なら何でも凍らせられそうだ！" };

        LvUpImpre chirno = new LvUpImpre("チルノ", chirnoImpre);
        lvUpImpreDatabase.impreList.Add(chirno);

        //文
        List<string> ayaImpre = new List<string>() { "あやややや・・・\nこれでは記事になりませんね。",
            "筆が乗ってきました。\nわくわく。",
            "大スクープの予感がします！\n良い記事が書けそう！" };

        LvUpImpre aya = new LvUpImpre("文", ayaImpre);
        lvUpImpreDatabase.impreList.Add(aya);

        //うどんげ氏
        List<string> udongeImpre = new List<string>() { "ああ、師匠に怒られるぅ・・・",
            "当然の結果ね。\n月では優秀だったので。",
            "師匠、依姫様、\n見ていてください！" };

        LvUpImpre udonge = new LvUpImpre("鈴仙", udongeImpre);
        lvUpImpreDatabase.impreList.Add(udonge);


        //ファイル書き出し Resources配下に作る
        AssetDatabase.CreateAsset(lvUpImpreDatabase, "Assets/Resources/lvUpImpreDatabase.asset");
    }

    
}

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ユニット初期値のアセットファイルを作ってくれるクラス
/// Unity上部の「Create」から使用する
/// </summary>
public static class GrowthDatabaseCreator
{

    //MenuItemを付ける事で上部メニューに項目を追加
    [MenuItem("Create/GrowthDatabase")]
    private static void Create()
    {

        GrowthDatabase growthDatabase = ScriptableObject.CreateInstance<GrowthDatabase>();

        //霊夢
        int[] growthRate = new int[] { 45, 45, 35, 45, 45, 50, 35, 30 };
        GrowthRate reimuGrowth = new GrowthRate("霊夢", growthRate);
        growthDatabase.growthList.Add(reimuGrowth);

        //魔理沙
        growthRate = new int[] { 40, 50, 35, 40, 50, 40, 30, 20 };
        GrowthRate marisaGrowth = new GrowthRate("魔理沙", growthRate);
        growthDatabase.growthList.Add(marisaGrowth);

        //ルーミア
        growthRate = new int[] { 50, 45, 35, 30, 25, 20, 50, 40 };
        GrowthRate rumiaGrowth = new GrowthRate("ルーミア", growthRate);
        growthDatabase.growthList.Add(rumiaGrowth);

        //大妖精
        growthRate = new int[] { 30, 40, 30, 40, 40, 30, 30, 20 };
        GrowthRate daiyouseiGrowth = new GrowthRate("大妖精", growthRate);
        growthDatabase.growthList.Add(daiyouseiGrowth);

        //ツィルノ
        growthRate = new int[] { 50, 40, 45, 30, 45, 40, 30, 30 };
        GrowthRate chirnoGrowth = new GrowthRate("チルノ", growthRate);
        growthDatabase.growthList.Add(chirnoGrowth);

        //文
        growthRate = new int[] { 40, 35, 35, 40, 65, 45, 30, 30 };
        GrowthRate ayaGrowth = new GrowthRate("文", growthRate);
        growthDatabase.growthList.Add(ayaGrowth);

        //うどんげ氏
        growthRate = new int[] { 40, 50, 30, 55, 45, 15, 35, 20 };
        GrowthRate udongeGrowth = new GrowthRate("鈴仙", growthRate);
        growthDatabase.growthList.Add(udongeGrowth);

        //美鈴
        growthRate = new int[] { 65, 20, 50, 40, 45, 20, 25, 40 };
        GrowthRate meirinGrowth = new GrowthRate("美鈴", growthRate);
        growthDatabase.growthList.Add(meirinGrowth);

        //小悪魔
        growthRate = new int[] { 40, 45, 30, 40, 45, 20, 20, 30 };
        GrowthRate koakumaGrowth = new GrowthRate("小悪魔", growthRate);
        growthDatabase.growthList.Add(koakumaGrowth);

        //パチュリー
        growthRate = new int[] { 25, 65, 15, 65, 35, 30, 40, 10 };
        GrowthRate patuGrowth = new GrowthRate("パチュリー", growthRate);
        growthDatabase.growthList.Add(patuGrowth);

        //咲夜
        growthRate = new int[] { 40, 45, 30, 50, 50, 40, 30, 35 };
        GrowthRate sakuyaGrowth = new GrowthRate("咲夜", growthRate);
        growthDatabase.growthList.Add(sakuyaGrowth);

        //レミリア
        growthRate = new int[] { 55, 40, 50, 40, 45, 65, 25, 30 };
        GrowthRate remilliaGrowth = new GrowthRate("レミリア", growthRate);
        growthDatabase.growthList.Add(remilliaGrowth);

        //フラン
        growthRate = new int[] { 50, 50, 65, 25, 50, 25, 20, 25 };
        GrowthRate frandreGrowth = new GrowthRate("フランドール", growthRate);
        growthDatabase.growthList.Add(frandreGrowth);

        //以下、敵の成長率 咲夜とかが敵で出た場合も、成長率は味方と同じものを参照
        //妖精
        growthRate = new int[] { 50, 40, 40, 30, 35, 25, 25, 20 };
        GrowthRate growth = new GrowthRate("妖精", growthRate);
        growthDatabase.growthList.Add(growth);

        //メイド妖精
        growthRate = new int[] { 60, 45, 45, 40, 45, 30, 30, 25 };
        growth = new GrowthRate("メイド妖精", growthRate);
        growthDatabase.growthList.Add(growth);

        //毛玉  
        growthRate = new int[] { 40 , 20 , 35 , 25 , 45 , 20 , 10 , 30 };
        growth = new GrowthRate("毛玉", growthRate);
        growthDatabase.growthList.Add(growth);

        //妖獣  
        growthRate = new int[] { 50 , 30 , 40 , 30 , 50 , 20 , 20 , 35 };
        growth = new GrowthRate("妖獣", growthRate);
        growthDatabase.growthList.Add(growth);

        //魔導書 
        growthRate = new int[] {40 , 55 , 10 , 50 , 30 , 10 , 35 , 20 };
        growth = new GrowthRate("魔導書", growthRate);
        growthDatabase.growthList.Add(growth);

        //グリモワール  
        growthRate = new int[] {45 , 60 , 20 , 60 , 35 , 20 , 40 , 25 };
        growth = new GrowthRate("グリモワール", growthRate);
        growthDatabase.growthList.Add(growth);

        //使い魔 
        growthRate = new int[] {45 , 50 , 40 , 40 , 40 , 30 , 25 , 20 };
        growth = new GrowthRate("使い魔", growthRate);
        growthDatabase.growthList.Add(growth);

        //ひまわり妖精  
        growthRate = new int[] {60 , 45 , 45 , 25 , 20 , 15 , 40 , 20 };
        growth = new GrowthRate("ひまわり妖精", growthRate);
        growthDatabase.growthList.Add(growth);

        //ハイフェアリー 
        growthRate = new int[] {70 , 50 , 50 , 30 , 25 , 20 , 45 , 25 };
        growth = new GrowthRate("ハイフェアリー", growthRate);
        growthDatabase.growthList.Add(growth);

        //ホブゴブリン  
        growthRate = new int[] {50 , 30 , 50 , 40 , 50 , 20 , 20 , 35 };
        growth = new GrowthRate("ホブゴブリン", growthRate);
        growthDatabase.growthList.Add(growth);

        //幽霊  
        growthRate = new int[] {50 , 50 , 20 , 30 , 45 , 10 , 30 , 30 };
        growth = new GrowthRate("幽霊", growthRate);
        growthDatabase.growthList.Add(growth);

        //怨霊  
        growthRate = new int[] {55 , 55 , 25 , 40 , 50 , 15 , 35 , 35 };
        growth = new GrowthRate("怨霊", growthRate);
        growthDatabase.growthList.Add(growth);

        //吸血コウモリ  
        growthRate = new int[] {40 , 40 , 30 , 35 , 55 , 35 , 20 , 20 };
        growth = new GrowthRate("吸血コウモリ", growthRate);
        growthDatabase.growthList.Add(growth);

        //ツパイ 
        growthRate = new int[] {45 , 50 , 40 , 40 , 60 , 40 , 25 , 25 };
        growth = new GrowthRate("ツパイ", growthRate);
        growthDatabase.growthList.Add(growth);


        //ファイル書き出し Resources配下に作る
        AssetDatabase.CreateAsset(growthDatabase, "Assets/Resources/growthDatabase.asset");
    }

}

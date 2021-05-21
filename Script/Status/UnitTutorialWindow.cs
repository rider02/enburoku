using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210221 ユニットのチュートリアルテキストを表示するクラス
/// </summary>
public class UnitTutorialWindow : MonoBehaviour
{
    //名前
    [SerializeField] private Text name;

    //種族
    [SerializeField] private Text race;

    //テキスト
    [SerializeField] private Text detailText;

    [SerializeField] private Image image;

    //HP
    [SerializeField] private Text hp;
    [SerializeField] private Slider hpGauge;

    //遠距離攻撃
    [SerializeField] private Text latk;
    [SerializeField] private Slider latkGauge;

    //近距離攻撃
    [SerializeField] private Text catk;
    [SerializeField] private Slider catkGauge;

    //速さ
    [SerializeField] private Text agi;
    [SerializeField] private Slider agiGauge;

    //技
    [SerializeField] private Text dex;
    [SerializeField] private Slider dexGauge;

    //幸運
    [SerializeField] private Text luk;
    [SerializeField] private Slider lukGauge;

    //遠距離防御
    [SerializeField] private Text ldef;
    [SerializeField] private Slider ldefGauge;

    //近距離防御
    [SerializeField] private Text cdef;
    [SerializeField] private Slider cdefGauge;

    public void UpdateText(Unit unit)
    {
        this.name.text = unit.name;
        this.race.text = unit.race.GetStringValue();

        this.image.sprite = Resources.Load<Sprite>("Image/Charactors/" + unit.pathName + "/status");

        //アセットから成長率を取得
        GrowthDatabase growthDatabase = Resources.Load<GrowthDatabase>("growthDatabase");
        GrowthRate growthRate = growthDatabase.FindByName(unit.name);

        //HP
        hp.text = growthRate.hpRate.ToString();
        hpGauge.maxValue = StatusConst.GROWTH_MAX;
        hpGauge.value = growthRate.hpRate;

        //遠距離攻撃
        latk.text = growthRate.latkRate.ToString();
        latkGauge.maxValue = StatusConst.GROWTH_MAX;
        latkGauge.value = growthRate.latkRate;

        //近距離攻撃
        catk.text = growthRate.catkRate.ToString();
        catkGauge.maxValue = StatusConst.GROWTH_MAX;
        catkGauge.value = growthRate.catkRate;

        //速さ
        agi.text = growthRate.agiRate.ToString();
        agiGauge.maxValue = StatusConst.GROWTH_MAX;
        agiGauge.value = growthRate.agiRate;

        //技
        dex.text = growthRate.dexRate.ToString();
        dexGauge.maxValue = StatusConst.GROWTH_MAX;
        dexGauge.value = growthRate.dexRate;

        //幸運
        luk.text = growthRate.lukRate.ToString();
        lukGauge.maxValue = StatusConst.GROWTH_MAX;
        lukGauge.value = growthRate.lukRate;

        //遠距離防御
        ldef.text = growthRate.ldefRate.ToString();
        ldefGauge.maxValue = StatusConst.GROWTH_MAX;
        ldefGauge.value = growthRate.ldefRate;
        
        //近距離防御
        cdef.text = growthRate.cdefRate.ToString();
        cdefGauge.maxValue = StatusConst.GROWTH_MAX;
        cdefGauge.value = growthRate.cdefRate;


        if (unit.name == "霊夢")
        {
            this.detailText.text = "博麗神社の巫女さん。\n\n" +
                "2種類の武器と遠距離近距離攻撃、仲間の回復が出来、\n" +
                "多くの状況で有利に戦う事が出来ます。\n\n" +

                "また、武器や癒符の熟練度が上がりやすくなる\n" +
                "スキルを持っているので、\n" +
                "序盤から強力な符を装備する事が出来ます。\n\n" +

                "中級職で敵の防御スキルを無効化するスキルを習得します。\n\n" +

                "上級職では遠距離攻撃特化型と、近距離攻撃が得意で移動力が高い\n" +
                "バランス型を選ぶ事が出来ます。";
        }
        else if (unit.name == "魔理沙")
        {
            this.detailText.text = "弾幕はパワー。\n\n" +

                "遠距離攻撃と素早さが上がりやすく、\n" +
                "経験値を多く獲得するスキルで成長も早いので、\n" +
                "攻撃の要となります。\n" +
                "また、鍵無しで宝箱を開ける事が出来ます。\n" +
                "防御力は高くないので、孤立させ過ぎないように注意しましょう。\n\n" + 

                "中級職では武器の使用回数消費を抑えるスキルを習得します。\n\n" +

                "上級職では攻撃特化型と、回避率が高くアイテムの使用後に\n" +
                "行動が出来る防御型を選ぶ事が出来ます。";
        }
        else if (unit.name == "ルーミア")
        {
            this.detailText.text = "遠距離、近距離防御力共に高く、ダメージを受けにくいです。\n" +
                "反面、素早さは低いので、攻撃力の高い敵から\n" +
                "追撃を受けないように注意が必要です。\n" +

                "上級職にクラスチェンジすると・・・";
        }
        else if (unit.name == "大妖精")
        {
            this.detailText.text = "仲間の回復とステータスを上げる事が出来ます。\n" +
                "戦闘能力は低いので、攻撃を受けないように注意しましょう。\n\n" +

                "中級職になると、ターン開始時に\n" +
                "周囲の仲間の体力を少し回復する事が出来ます。\n\n" +

                "上級職になると仲間を再行動させる事が出来るようになり、\n" +
                "更に仲間のステータスを上げる事が出来るようになります。\n";

                
        }

        else if (unit.name == "チルノ")
        {
            this.detailText.text = "戦闘能力は突出した所が無いですが、\n" +
                "貴重な遠距離と近距離の両方に\n" +
                "攻撃出来る武器を使用する事が出来ます。\n\n" +

                "中級職で隣接する敵の回避率を下げるスキルを習得します。\n\n" +

                "上級職では仲間のステータスを上げるスキルを習得する職か、\n" +
                "敵を移動出来なくするスキルを習得する職を選ぶ事が出来るので、\n" +
                "上手に使う事で戦局を有利に進める事が出来ます。";
        }

        else if (unit.name == "文")
        {
            this.detailText.text = "序盤から加入して性能が高いですが、レベルが高い為、\n" +
                "文ばかり戦わせると他のキャラが成長しません。\n" +
                "序盤は戦闘させ過ぎないように注意が必要です。\n\n" +

                "鍵無しで宝箱を開ける事が出来るので宝箱の回収係や、\n" +
                "武器を外して壁として活躍させるのがお勧めです。\n\n" +

                "素早さが非常に高いですが、取材が目的で異変の解決には\n" +
                "関与する気が無い為、攻撃力は低いです。\n\n" +

                "所謂お助けキャラですが、成長率は高いので、\n" +
                "最後まで活躍する事が出来ます。\n" +

                "上級職では攻撃特化型と、回避率の高い\n" +
                "防御型を選ぶ事が出来ます。";
        }
        
        else if (unit.name == "美鈴")
        {
            this.detailText.text = "近距離攻撃と防御に特化した性能で、\n" +
                "HPが高く壁として活躍する事が出来ます。\n\n" +

                "近距離攻撃の手段を持たない敵に対しては\n" +
                "一方的に高いダメージを与える事が出来ます。\n\n" +

                "反面、遠距離攻撃への防御力は低く、\n" +
                "運も低い為必殺を受けやすいので、\n" +
                "過信していると想像以上にダメージを受ける事が有ります。\n\n" +

                "中級職から仲間の体力を回復させる事が出来ます。\n\n" +

                "上級職では高い必殺率を持つ職業と、\n" +
                "仲間の防御力を上げる事が出来る職業を選ぶ事が出来ます。\n";
        }
        else if (unit.name == "小悪魔")
        {
            this.detailText.text = "仲間の回復とステータスを上げる事が出来ます。\n" +
                "攻撃力もそこそこの性能で、敵にダメージを与えやすいです。\n" +
                "また、仲間を回復すると自分のHPも回復する事が出来ます。\n\n" +

                "運が低く、防御力も高くない為、\n" +
                "攻撃を受けないように注意しましょう。\n\n" +

                "中級職になると、周囲の仲間の攻撃力を上げる事が出来ます。\n" +

                "上級職になると仲間を再行動させる事が出来るようになり、\n" +
                "更に仲間のステータスを上げる事が出来るようになります。";
        }
        else if (unit.name == "鈴仙")
        {
            this.detailText.text = "攻撃の命中率が高く、射程距離の長い武器で\n" +
                "敵を狙撃する事が出来ます。\n\n" +

                "運が非常に低く必殺を受けやすいので、\n" +
                "攻撃を受けないように注意が必要です。\n\n" +

                "上級職では薬で仲間の体力を回復させる事が出来るバランス型と、\n" +
                "攻撃特化型の傾向が大きく異なる2種類の職業を選ぶ事が出来ます。\n\n" +

                "趣味で登場させたけど、運用に難が有って可愛い。";
        }
        else if (unit.name == "パチュリー")
        {
            this.detailText.text = "遠距離攻撃防御力、技が非常に高く、\n" +
                "最大8マス先の敵に遠距離攻撃出来る武器を使用する事が出来る為、\n" +
                "遠距離攻撃では圧倒的な性能を持っています。\n\n" +

                "反面、HP、近距離防御力は非常に低く、素早さも低めなので、\n" +
                "近距離攻撃を受けると一撃で倒されてしまう事も有ります。\n\n" +

                "専用武器は修理出来ますが非常に修理費が高額な為、\n" +
                "ここぞという時に使用しましょう。\n\n" +

                "中級職ではより攻撃力が上がり、\n" +
                "上級職では攻守共に強力なスキルを習得します。\n\n" +

                "装備出来る符の種類が多く、ショット、レーザーに加えて\n" +
                "上級職では物理、癒符のどちらかを装備する事が出来ます。\n";

        }
        else if (unit.name == "咲夜")
        {
            this.detailText.text = "技、速さ、幸運が上がりやすく、\n" +
                "また、遠近両方に対して攻撃出来る専用武器を持っている為、\n" +
                "隙の無い活躍が出来ます。\n\n" +

                "バランス型の成長をするキャラのお約束で\n" +
                "運が悪いと器用貧乏になる事も有りますが、\n" +
                "その場合も仲間のサポートと鍵開けによる宝箱の回収で\n" +
                "いぶし銀の活躍が出来る、まさにパーフェクトメイド。\n\n" +

                "中級職では行動せずに待機すると\n" +
                "回避率が上がるスキルを習得します。\n\n" +

                "上級職では攻撃特化型の職業と、\n" +
                "仲間のサポートが出来る職業を選ぶ事が出来ます。\n";

        }

        else if (unit.name == "レミリア")
        {
            this.detailText.text = "序盤から加入して性能が高いですが、レベルが高い為、\n" +
                "レミリアばかり戦わせると他のキャラが成長しません。\n" +
                "序盤は戦闘させ過ぎないように注意が必要です。\n\n" +

                "専用武器のグングニルは遠近両用で非常に高性能ですが、\n" +
                "修理費が高額な為、多用すると紅魔館の財政を圧迫してしまいます。\n\n" +

                "運命を操る程度の能力の為、非常に幸運の成長率が高く、\n" +
                "全体的なステータスの成長率も高い為、\n" +
                "終盤も活躍する事が出来ます。\n\n" +

                "上級職では戦闘後に敵の能力を下げるスキルを習得する職業と、\n" +
                "敵から受けるダメージを確率で半減させるスキルを習得する\n" +
                "職業を選ぶ事が出来ます。\n";

        }
        else if (unit.name == "フランドール")
        {
            this.detailText.text = "非常に攻撃寄りのステータスにクリティカル率が高い専用武器、\n" +
                "必殺率を上げる専用スキルを持っており、\n" +
                "攻撃が命中すれば多くの敵を一撃で倒す事が出来ます。\n\n" +

                "反面、技が低い為攻撃の命中が安定しないので、\n" +
                "運用に運の要素が強いです。\n\n" +
                "また防御力は低いので、シリーズ恒例の\n" +
                "「やっつけ負け」に注意する必要が有ります。\n\n" +

                "ゲーム終盤に上級職で加入する為、\n" +
                "クラスチェンジ可能な職業は有りません。";

        }

    }
}

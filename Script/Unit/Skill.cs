﻿/// <summary>
/// スキル一覧
/// </summary>
public enum Skill
{
    [StringValue("取得技能経験値2倍")]
    天才肌,

    [StringValue("幸運+5")]
    幸運,

    //没スキル
    [StringValue("敵の防御系個人スキルを無効化する")]
    自由奔放,

    [StringValue("移動+1")]
    亜空穴,

    [StringValue("ショットによる攻撃時、攻撃力+5")]
    弾幕の達人,

    [StringValue("攻撃時、技×1.5％で発動。力の攻撃は敵の力、魔法攻撃なら敵の魔力の半分をダメージ追加")]
    無想転生,

    [StringValue("自分から攻撃後に移動力が残っていれば再行動可能")]
    再行動,

    [StringValue("物理による攻撃時、攻撃力+5")]
    武器の達人,

    [StringValue("自分と周囲2マスに隣接する味方の受けるダメージ-3")]
    博麗の小結界,

    [StringValue("取得経験値1.2倍")]
    努力家,

    [StringValue("カギの掛かった宝箱を開ける")]
    鍵開け,

    [StringValue("幸運×2％の確率で武器耐久数を減らさずに攻撃する")]
    メンテナンス,

    [StringValue("符を3つ以上所持している時、必殺+10")]
    蒐集家,

    [StringValue("レーザーによる攻撃時、攻撃力+5")]
    レーザーの達人,

    [StringValue("自分から攻撃時、回避+30")]
    一撃離脱,

    [StringValue("攻撃を受けた時、回避+20%、守備+2")]
    目隠し,

    [StringValue("HP+5")]
    HP5,

    [StringValue("自分から攻撃して敵を倒した時、HPが最大HPの50%回復")]
    生命吸収,

    [StringValue("自分から攻撃時、ダメージ+7、必殺+20 戦闘後HP-5")]
    暗黒の一撃,

    [StringValue("HP75%以下で敵から攻撃された時、全能力+4、必殺+30%")]
    EX化,

    [StringValue("戦闘中、自分も敵も追撃が発生しない")]
    宵闇の守り,

    [StringValue("味方が2マス以内にいる時、味方が受けるダメージ-2")]
    小さな声援,

    [StringValue("幸運×1.5%で発動。自分のHPが2以上の時、HPが0になる攻撃を受けてもHPが1残る")]
    祈り,

    [StringValue("「応援」コマンド使用時、周囲の仲間の幸運+10")]
    幸運の叫び,

    [StringValue("「応援」コマンド使用時、周囲の仲間の遠攻+4")]
    遠距離の叫び,

    [StringValue("戦闘後、敵の速さ-6(1ターン)")]
    速さ封じ,

    [StringValue("ターン開始時、自分と周囲2マス以内の味方を最大HPの20%HP回復")]
    自然の恵み,

    [StringValue("HP回復量+10")]
    癒し手,

    [StringValue("HPが100％の時命中・回避+15")]
    自信満々,

    [StringValue("幸運％で発動。ターン開始時、HPが最大HPの30％回復")]
    しぶとい心,

    [StringValue("隣接する敵が戦闘時、敵の回避-20")]
    凍える冷気,

    [StringValue("「応援」コマンド使用時、周囲の仲間の近攻+4")]
    近接の叫び,

    [StringValue("攻撃した相手の移動力を0にする")]
    移動封じ,

    [StringValue("自分から攻撃時、速さ+5")]
    飛燕の一撃,

    [StringValue("敵をすり抜けて移動可能")]
    すり抜け,

    //没
    [StringValue("自分から攻撃を仕掛けて敵を撃破した時、遠攻・近攻・技・速さ+4(1ターン)")]
    探求心,

    [StringValue("自分から攻撃して敵を倒すと、1ターンに1度だけ再行動できる")]
    疾風迅雷,

    [StringValue("技×0.5%で発動 相手を即死させる ボスはHPの半分のダメージを与える")]
    瞬殺,

    [StringValue("技×0.75%で発動 ダメージ半分で5回連続攻撃")]
    流星,

    [StringValue("自分から攻撃時命中+40%")]
    慧眼の一撃,

    [StringValue("攻撃を受けた時、回避+20")]
    狂気の瞳,

    [StringValue("HP回復薬の効果2倍")]
    よくきく薬,

    [StringValue("薬を使用後、行動終了せずに行動出来る")]
    薬売り,

    [StringValue("薬でHPを回復すると遠攻・近攻・技・速さ+4(1ターン)")]
    国士無双の薬,

    [StringValue("HPが満タンでない時攻撃+4、必殺+20%、命中率-10%")]
    狂化,

    [StringValue("行動せずに待機すると、遠防、近防+4（1ターン）")]
    紅魔の盾,

    [StringValue("自分のＨＰを５０％回復")]
    瞑想,

    [StringValue("自分から攻撃した時、近防+10")]
    金剛の一撃,

    [StringValue("「応援」コマンド使用時、周囲の仲間の近防+4")]
    近防の応援,

    [StringValue("HPが50%以上で敵から攻撃された時、必ず追撃する")]
    切り返し,

    [StringValue("必殺が15上がる")]
    必殺,

    [StringValue("味方が2マス以内にいる時、味方が与えるダメージ+2")]
    鼓舞,

    [StringValue("自分が味方を回復させる時、自分も同じ量だけ回復できる")]
    ご奉仕の喜び,

    [StringValue("味方が隣接している時、味方が与えるダメージ+1、受けるダメージ-3")]
    紅の花,

    [StringValue("「応援」コマンド使用時、周囲の仲間の遠防+4")]
    遠防の叫び,

    [StringValue("戦闘後、敵の遠攻-6(1ターン)")]
    遠攻封じ,

    [StringValue("「応援」コマンド使用時、周囲の仲間の技+4")]
    技の叫び,

    [StringValue("戦闘後、敵の遠防-6(1ターン)")]
    遠防封じ,

    [StringValue("自分が地形効果0以外の場所で戦闘時、受けるダメージ-3")]
    密室の守り,

    [StringValue("味方が隣接している時、味方が与えるダメージ+3、受けるダメージ-1")]
    紫の花,

    [StringValue("自分から攻撃した場合、遠攻+6")]
    魔女の一撃,

    [StringValue("技×2%で発動 ダメージを半減させる")]
    魔力障壁,

    [StringValue("技%で発動 与えたダメージの半分回復")]
    太陽,

    [StringValue("自分から攻撃して敵を撃破した時、HPの30%回復する")]
    魔力吸収,

    [StringValue("技%で発動 相手の防御力を半減して攻撃")]
    月光,

    [StringValue("HPが満タンの時、命中、回避+15")]
    完璧主義,

    [StringValue("行動せずに「待機」すると、回避+30(1ターン)")]
    ミスディレクション,

    [StringValue("仲間と隣接している時、互いの全能力+2")]
    パーフェクトメイド,

    [StringValue("「応援」コマンド使用時、周囲の仲間の速さ+4")]
    速さの叫び,

    [StringValue("仲間と位置を入れ替える")]
    チェンジリングマジック,

    [StringValue("自分から攻撃した時、近攻+6")]
    鬼神の一撃,

    [StringValue("幸運+4、HP+4")]
    威風堂々,

    [StringValue("周囲3マスの味方の回避、命中+10%")]
    カリスマ,

    [StringValue("戦闘後、敵の全能力-4(1ターン)")]
    運命操作,

    [StringValue("命中・回避・必殺+10")]
    最強の体術,

    [StringValue("幸運%で発動。ダメージを半減させる")]
    運命予知,

    [StringValue("自分から攻撃した時、自分の必殺+30、回避、必殺回避-10")]
    直死の一撃,

    [StringValue("敵に与えるダメージ+10、敵から受けるダメージ+10")]
    死線,

    [StringValue("HP50%以下で攻撃を受けた時、必殺+50")]
    怒り,

    //空のボタン用
    NONE

}

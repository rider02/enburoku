using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 210210 ターン開始アニメーション等を実装する
/// </summary>
public class TurnEffectManager : MonoBehaviour
{
    private BattleMapManager battleMapManager;
    [SerializeField] Image playerTurnText;
    [SerializeField] Image enemyTurnText;
    [SerializeField] Image playerTurnEmblem;
    [SerializeField] Image enemyTurnEmblem;
    [SerializeField] AudioSource se;

    public bool isEffectInit;
    public bool isEffectFinished;
    public float effectSpeed = 0.3f;

    //完全に拡大縮小する前に消滅させたい
    public float fadeSpeed = 0.2f;

    Sequence textSequence;
    Sequence emblemSequence;

    //初期化
    public void Init(BattleMapManager battleMapManager)
    {
        this.battleMapManager = battleMapManager;
        isEffectFinished = false;

        //回転　setLoopsに-1で無限ループ
        playerTurnEmblem.transform.DOLocalRotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        //回転　setLoopsに-1で無限ループ
        enemyTurnEmblem.transform.DOLocalRotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        //シーケンス初期化
    }

    //実行前に呼び出してフラグを倒す
    public void EffectInit()
    {
        isEffectInit = false;
        isEffectFinished = false;

        //自軍ターン 拡大縮小をデフォルトに戻す
        playerTurnText.transform.localScale = new Vector3(1.2f , 1.2f, 1.2f);
        playerTurnEmblem.transform.localScale = new Vector3(1, 1, 1);

        //敵軍ターン
        enemyTurnText.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        enemyTurnEmblem.transform.localScale = new Vector3(1, 1, 1);
    }

    //自軍ターン開始エフェクト
    public void TurnStartEffectUpdate()
    {

        if (!isEffectInit)
        {
            isEffectInit = true;
            //シーケンスをセット
            textSequence = setTextSequence(playerTurnText);
            emblemSequence = setEmblemSequence(playerTurnEmblem, true);


            textSequence.Play();
            emblemSequence.Play();

            //効果音
            se.Play();
        }
        //第一引数：アルファ値 1が不透明 第二引数は秒単位
        //テキストと紋章を1秒でフェードインさせる 紋章は半透明なので0.8f
    }

    //210212 敵軍ターン開始演出
    public void EnemyTurnStartEffectUpdate()
    {
        if (!isEffectInit)
        {
            isEffectInit = true;

            //シーケンスをセット
            textSequence = setTextSequence(enemyTurnText);
            emblemSequence = setEmblemSequence(enemyTurnEmblem, false);

            textSequence.Play();
            emblemSequence.Play();

            //効果音
            se.Play();
        }
    }

    //味方ターン、敵ターンのシーケンスを返す
    private Sequence setTextSequence(Image turnText)
    {
        //文字は0.7秒でフェードイン、0.7秒待機、0.7秒でフェードアウトというシーケンスを行う
        //OutSine : 徐々に遅くなる
        //DoFade : フェードインフェードアウト
        //DoScale : 拡大
        Sequence textSequence = DOTween.Sequence();
        textSequence.Append(turnText.DOFade(endValue: 1f, duration: fadeSpeed).SetEase(Ease.OutSine))
        .Join(turnText.transform.DOScale(endValue: new Vector3(1, 1, 1), effectSpeed).SetEase(Ease.OutSine))

        .AppendInterval(effectSpeed - 0.2f)

        .Append(turnText.DOFade(endValue: 0f, duration: fadeSpeed))
        .Join(turnText.transform.DOScale(endValue: new Vector3(5, 5, 5), effectSpeed).SetEase(Ease.OutSine));

        return textSequence;
    }

    private Sequence setEmblemSequence(Image emblem , bool isPlayer)
    {
        //エンブレムのsequence 透明から半透明にフェードインするのでendValueは0.8fくらい
        Sequence emblemSequence = DOTween.Sequence();

        emblemSequence.Append(emblem.DOFade(endValue: 0.7f, duration: fadeSpeed).SetEase(Ease.OutSine))
            .Join(emblem.transform.DOScale(endValue: new Vector3(0.9f, 0.9f, 0.9f), effectSpeed + 0.2f).SetEase(Ease.OutSine))


            .Append(emblem.DOFade(endValue: 0f, duration: fadeSpeed).SetEase(Ease.OutSine))
            .Join(emblem.transform.DOScale(endValue: new Vector3(5, 5, 5), effectSpeed).SetEase(Ease.OutSine))
            .OnComplete(() => {
                //完了したらモード変更 敵軍ターンと味方ターンで呼ぶメソッド変える
                if (isPlayer)
                {
                    isEffectInit = false;
                    battleMapManager.PrepareMapmodeNormal();
                }
                else
                {
                    isEffectInit = false;
                    battleMapManager.ChangeMapmodeEnemyTurn();
                }
            });

        return emblemSequence;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210521 経験値増加ゲージを表示するUI
/// </summary>
public class EXPWindow : MonoBehaviour
{

    [SerializeField] Text expNumText;
    [SerializeField] Slider expSlider;
    [SerializeField] AudioSource audioSource;

    //現在の経験値に獲得経験値を足した値
    int targetExp;

    //ゲージの増加を行う間隔 少ない程なめらか
    float gaugeInterval = 0.01f;

    //経験値増加完了フラグ
    bool expIncrementFinish = false;

    //　経過時間
    private float elapsedTime = 0f;

    //210301 Unity開始初期に作ったゲージだけど普通に使える
    public bool ExpGaugeUpdate()
    {
        
        //経過時間を取得
        elapsedTime += Time.deltaTime;

        if (elapsedTime <= gaugeInterval)
        {
            return false;
        }

        //経験値上昇が終わっていたら終了 trueを返して処理が進む
        if (expIncrementFinish)
        {
            return true;
        }

        elapsedTime = 0;
        
        //スライダーと経験値の数値を更新
        expSlider.value++;
        expNumText.text = string.Format("{0}", expSlider.value);

        //効果音再生
        audioSource.Play();

        //100を超えたらレベルアップ 折り返して余りを加算
        if (expSlider.value >= 100)
        {
            expSlider.value = expSlider.value - 100;
            targetExp = targetExp - 100;
            expNumText.text = string.Format("{0}", expSlider.value);

        }

        if (expSlider.value >= targetExp)
        {
            //ゲージの経験値が指定した値に達したら増加中フラグをfalse
            expIncrementFinish = true;

        }
        return false;
    }

    /// <summary>
    /// //ゲージ初期化 プレイヤーの経験値(レベルアップしたなら100以上になる)を渡して初期化
    /// </summary>
    /// <param name="playerExp">現在の経験値</param>
    /// <param name="getExp">現在の経験値 + 獲得した経験値</param>
    public void InitExpGauge(int playerExp , int getExp)
    {
        //経験値を数値とゲージに反映
        expNumText.text = string.Format("{0}", playerExp);
        expSlider.value = playerExp;
        Debug.Log($"現在の経験値:{playerExp}, 獲得経験値:{getExp}");

        //この値までゲージが増加する
        this.targetExp = playerExp + getExp;

        expIncrementFinish = false;
        
    }

    public bool ExpIncrementFinish
    {
        get { return this.expIncrementFinish; }  //取得用
        set { this.expIncrementFinish = value; } //値入力用
    }


}

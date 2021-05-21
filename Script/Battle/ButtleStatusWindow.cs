using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtleStatusWindow : MonoBehaviour
{
    [SerializeField] GameObject nameWindow;
    [SerializeField] Text nameText;//名前
    [SerializeField] Text hpText;
    [SerializeField] Text attackText;
    [SerializeField] Text weaponText;
    [SerializeField] Text hitRateText;
    [SerializeField] Text criticalRateText;
    [SerializeField] Slider hpSlider;
    [SerializeField] GameObject chase;
    [SerializeField] GameObject yuushaChase;
    [SerializeField] GameObject advantage;
    [SerializeField] GameObject disadvantage;

    
    [SerializeField] GameObject healTargetNameWindow;
    [SerializeField] Text healTargetNameText;

    //ゲージが減る時間 少ない程早い
    float HpDecrementTime = 0.1f;
    float gaugeSplit;//ゲージの減少を何回にかけて行うか
    float gaugeInterval = 0.01f;//ゲージの減少を行う間隔 少ない程なめらか

    //ゲージ表示用HP。徐々に増えたり減ったりする
    float gaugeHp;
    int targetHp;//このHPまで減らしたい
    bool isHpChange = false;//フラグが立ってる間減り続ける
    float splitDiff;//指定した秒数でHPを変化させる場合の分割した値

    //　経過時間
    private float elapsedTime = 0f;

    // Update is called once per frame
    void Update()
    {
        //HP減少中なら
        if (isHpChange)
        {
            //経過時間を取得する
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= gaugeInterval)
            {

                elapsedTime = 0;
                hpSlider.value -= splitDiff;

                //ダメージの時
                if (splitDiff >= 0)
                {
                    //実際の値までHPが下がったら
                    if (hpSlider.value <= targetHp)
                    {
                        //減少中フラグをfalse
                        isHpChange = false;
                    }
                }
                else
                {
                    //回復の時
                    //実際の値までHPが上がったら
                    if (hpSlider.value >= targetHp)
                    {
                        //減少中フラグをfalse
                        isHpChange = false;
                    }
                }
                
            }
        }
    }

    //ユニットのステータスを反映するメソッドのDTO版
    //敵ウィンドウに割り当てているインスタンスから呼ぶと動かないので注意
    public void UpdatePleyerText(BattleParameterDTO battleParameterDTO)
    {
        nameText.text = battleParameterDTO.unitName;

        hpText.text = string.Format("{0}", battleParameterDTO.unitHp);
        hpSlider.maxValue = battleParameterDTO.unitMaxHp;
        hpSlider.value = battleParameterDTO.unitHp;
        weaponText.text = battleParameterDTO.unitWeaponName;

        //相性
        if (battleParameterDTO.affinity == BattleWeaponAffinity.ADVANTAGE)
        {
            //有利な時
            advantage.SetActive(true);
            disadvantage.SetActive(false);
        }
        else if (battleParameterDTO.affinity == BattleWeaponAffinity.DISADVANTAGE)
        {
            //不利な時
            advantage.SetActive(false);
            disadvantage.SetActive(true);
        }
        else
        {
            advantage.SetActive(false);
            disadvantage.SetActive(false);
        }

        //攻撃出来る時
        if (battleParameterDTO.isUnitAttackable)
        {
            attackText.text = string.Format("{0}", battleParameterDTO.unitAttack);
            hitRateText.text = string.Format("{0}", battleParameterDTO.unitHitRate);
            criticalRateText.text = string.Format("{0}", battleParameterDTO.unitCriticalRate);

            //相性や追撃を表示
            //210220 特効フラグが立っていれば攻撃力の数字をハイライトする処理を追加
            if (battleParameterDTO.isUnitAttackSlayer)
            {
                Color highLightColor = new Color(192.0f / 255.0f, 0f / 255.0f, 27.0f / 255.0f, 255.0f / 255.0f);
                attackText.color = highLightColor;
            }
            else
            {
                attackText.color = Color.black;
            }

            //追撃
            if (battleParameterDTO.unitChaseFlag)
            {
                //勇者武器の場合
                if (battleParameterDTO.isUnitYuusha)
                {
                    //4回攻撃
                    yuushaChase.SetActive(true);
                    chase.SetActive(false);
                }
                else
                {
                    //通常の追撃
                    yuushaChase.SetActive(false);
                    chase.SetActive(true);
                }
                
            }
            else
            {
                //追撃出来ないが勇者武器の場合
                if (battleParameterDTO.isUnitYuusha)
                {
                    //2回攻撃
                    yuushaChase.SetActive(false);
                    chase.SetActive(true);
                }
                else
                {
                    //追撃出来ない通常攻撃
                    yuushaChase.SetActive(false);
                    chase.SetActive(false);
                }
                    
            }

            
        }
        else
        {
            //攻撃出来ない時
            attackText.text = "-";
            hitRateText.text = "-";
            criticalRateText.text = "-";

            //相性や追撃は非表示にする
            chase.SetActive(false);
        }
    }

    //敵のステータスを反映するメソッドのDTO版
    public void UpdateEnemyText(BattleParameterDTO battleParameterDTO)
    {
        nameText.text = battleParameterDTO.enemyName;
        hpText.text = string.Format("{0}", battleParameterDTO.enemyHp);
        hpSlider.maxValue = battleParameterDTO.enemyMaxHp;
        hpSlider.value = battleParameterDTO.enemyHp;
        weaponText.text = battleParameterDTO.enemyWeaponName;

        //相性
        if (battleParameterDTO.affinity == BattleWeaponAffinity.ADVANTAGE)
        {
            //プレイヤー有利な時
            advantage.SetActive(false);
            disadvantage.SetActive(true);
        }
        else if (battleParameterDTO.affinity == BattleWeaponAffinity.DISADVANTAGE)
        {
            //プレイヤー不利な時
            advantage.SetActive(true);
            disadvantage.SetActive(false);
        }
        else
        {
            advantage.SetActive(false);
            disadvantage.SetActive(false);
        }

        if (battleParameterDTO.isEnemyAttackable)
        {
            
            attackText.text = string.Format("{0}", battleParameterDTO.enemyAttack);
            hitRateText.text = string.Format("{0}", battleParameterDTO.enemyHitRate);
            criticalRateText.text = string.Format("{0}", battleParameterDTO.enemyCiritcalRate);



            //210220 特効フラグが立っていれば攻撃力の数字をハイライトする処理を追加
            if (battleParameterDTO.isEnemyAttackSlayer)
            {
                Color highLightColor = new Color(192.0f / 255.0f, 0f / 255.0f, 27.0f / 255.0f, 255.0f / 255.0f);
                attackText.color = highLightColor;
            }
            else
            {
                attackText.color = Color.black;
            }

            //追撃
            if (battleParameterDTO.enemyChaseFlag)
            {
                //勇者武器の場合
                if (battleParameterDTO.isEnemyYuusha)
                {
                    //4回攻撃
                    yuushaChase.SetActive(true);
                    chase.SetActive(false);
                }
                else
                {
                    //通常の追撃
                    yuushaChase.SetActive(false);
                    chase.SetActive(true);
                }

            }
            else
            {
                //追撃出来ないが勇者武器の場合
                if (battleParameterDTO.isEnemyYuusha)
                {
                    //2回攻撃
                    yuushaChase.SetActive(false);
                    chase.SetActive(true);
                }
                else
                {
                    //追撃出来ない通常攻撃
                    yuushaChase.SetActive(false);
                    chase.SetActive(false);
                }

            }

            
        }
        else
        {
            //攻撃出来ない時
            attackText.text = "-";
            hitRateText.text = "-";
            criticalRateText.text = "-";

            //相性は非表示にする
            chase.SetActive(false);
        }

        
    }

    /// <summary>
    /// 210216 回復をする側
    /// </summary>
    public void UpdateHealText(HealParameterDTO healParameterDTO)
    {
        //enemyと書いてるけどプレイヤー 名前と杖の名前を表示
        nameText.text = healParameterDTO.unitName;
        weaponText.text = healParameterDTO.unitHealRodName;

        //回復するキャラのHP
        hpText.text = string.Format("{0}", healParameterDTO.unitHp);

        //回復量
        attackText.text = healParameterDTO.healAmount.ToString();
        hitRateText.text = "-";
        criticalRateText.text = "-";

        hpSlider.maxValue = healParameterDTO.unitMaxHp;
        hpSlider.value = healParameterDTO.unitHp;

        //相性や追撃は非表示にする
        chase.SetActive(false);
        advantage.SetActive(false);
        disadvantage.SetActive(false);
    }

    /// <summary>
    /// 210216 回復をされる側
    /// </summary>
    public void UpdateHealTargetText(HealParameterDTO healParameterDTO)
    {
        //HPのみ表示して後は使わないので「-」を表示
        healTargetNameText.text = healParameterDTO.targetName;
        weaponText.text = "-";

        hpText.text = string.Format("{0}", healParameterDTO.targetHp);
        attackText.text = "-";
        hitRateText.text = "-";
        criticalRateText.text = "-";

        hpSlider.maxValue = healParameterDTO.targetMaxHp;
        hpSlider.value = healParameterDTO.targetHp;

        //相性や追撃は非表示にする
        chase.SetActive(false);
        advantage.SetActive(false);
        disadvantage.SetActive(false);
    }

    //210216 回復と攻撃で結構UIが変わるので変化させる
    public void SetAttackMode()
    {
        //nullチェックしているのは、対象のみUIが変化する為
        if (nameWindow != null)
        {
            nameWindow.SetActive(true);
        }
        if (healTargetNameWindow != null)
        {
            healTargetNameWindow.SetActive(false);
        }
    }

    public void SetHealMode()
    {
        if (nameWindow != null)
        {
            nameWindow.SetActive(false);
            
        }
        if (healTargetNameWindow != null)
        {
            healTargetNameWindow.SetActive(true);
        }
        
    }

    //HPを更新する 敵、味方共に同じクラス
    public void UpdateHp(int hp)
    {
        //現在のHPの数値から戦闘後のHPをを引いて受けたダメージを計算
        //210216 これ、回復にも適用されるようになったのでdamageではなくdiff
        int diff = int.Parse(hpText.text) - hp;
        
        //数値を更新
        hpText.text = hp.ToString();

        //このHPまで変化させたい
        targetHp = hp;


        //何秒に1回ゲージを更新するかは固定
        //ゲージを分割する数を求める
        gaugeSplit = HpDecrementTime / gaugeInterval;
        splitDiff = diff / gaugeSplit;

        //ゲージのHPを減少中に
        isHpChange = true;
    }

}

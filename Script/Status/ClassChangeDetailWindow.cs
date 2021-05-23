using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210522 クラスチェンジ先の詳細を表示するウィンドウ
/// </summary>
public class ClassChangeDetailWindow : MonoBehaviour
{
    //職業の名前
    [SerializeField]
    private Text jobName;

    //職業の詳細
    [SerializeField]
    private Text jobDetail;

    //クラスチェンジ要求レベル
    [SerializeField] private Text lvRequired;

    //表示するステータス補正値 クラスチェンジするとこの値上がる
    //HP
    [SerializeField] private Text hp;

    //遠距離攻撃
    [SerializeField] private Text latk;

    //近距離攻撃
    [SerializeField] private Text catk;

    //速さ
    [SerializeField] private Text agi;

    //技
    [SerializeField] private Text dex;

    //幸運
    [SerializeField] private Text luk;

    //遠距離防御
    [SerializeField] private Text ldef;

    //近距離防御
    [SerializeField] private Text cdef;

    public void UpdateText(Job job)
    {
        JobStatusDto statusDto = job.statusDto;

        this.jobName.text = job.jobName.ToString();
        this.jobDetail.text = job.jobName.GetStringValue();

        //クラスチェンジ要求レベル
        if(job.jobLevel == JobLevel.ADEPT)
        {
            this.lvRequired.text = "10";
        }
        else if (job.jobLevel == JobLevel.MASTER)
        {

            this.lvRequired.text = "20";
        }
        else
        {
            //中級職以外は渡されないので、ここに入ると実装ミス
            this.lvRequired.text = "0";
        }

        this.hp.text = string.Format("{0}{1}", getOperand(statusDto.jobHp), statusDto.jobHp.ToString());
        this.latk.text = string.Format("{0}{1}", getOperand(statusDto.jobLatk), statusDto.jobLatk.ToString());
        this.catk.text = string.Format("{0}{1}", getOperand(statusDto.jobCatk), statusDto.jobCatk.ToString());
        this.agi.text = string.Format("{0}{1}", getOperand(statusDto.jobAgi), statusDto.jobAgi.ToString());
        this.dex.text = string.Format("{0}{1}", getOperand(statusDto.jobDex), statusDto.jobDex.ToString());
        this.luk.text = string.Format("{0}{1}", getOperand(statusDto.jobLuk), statusDto.jobLuk.ToString());
        this.ldef.text = string.Format("{0}{1}", getOperand(statusDto.jobLdef), statusDto.jobLdef.ToString());
        this.cdef.text = string.Format("{0}{1}", getOperand(statusDto.jobCdef), statusDto.jobCdef.ToString());

    }

    //1以上なら"+"を返す
    private string getOperand(int status)
    {
        return (status > 0) ? "+" : "-";
    }
}

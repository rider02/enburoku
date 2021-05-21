using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトル画面とかでユニットにフォーカスが当たった時きり変わるウィンドウ
/// </summary>
public class UnitOutlineWindow : MonoBehaviour
{
    [SerializeField]
    Text nameText;

    [SerializeField]
    Text lvNum;

    [SerializeField]
    Text maxHp;

    [SerializeField]
    Text hp;

    [SerializeField]
    Text job;

    [SerializeField]
    Text exp;

    [SerializeField]
    Image image;

    public void UpdateText(Unit unit)
    {
        StatusCalculator statusCalc = new StatusCalculator();

        JobStatusDto statusDto = unit.job.statusDto;

        this.nameText.text = unit.name;

        this.lvNum.text = unit.lv.ToString();

        this.hp.text = unit.hp.ToString();

        this.job.text = unit.job.jobName.ToString();

        this.exp.text = unit.exp.ToString();

        //威風堂々等、HPバフも有り得るので
        int maxHp = statusCalc.CalcHpBuff(unit.maxhp + statusDto.jobHp, unit.job.skills); ;

        this.maxHp.text = string.Format("/  {0}", (maxHp).ToString());

        this.image.sprite = Resources.Load<Sprite>("Image/Charactors/" + unit.pathName + "/status");

    }

    public void UpdateText(Enemy enemy)
    {

        JobStatusDto statusDto = enemy.job.statusDto;

        this.nameText.text = enemy.name;

        this.lvNum.text = enemy.lv.ToString();

        this.hp.text = (enemy.hp + statusDto.jobHp).ToString();

        this.job.text = enemy.job.jobName.ToString();

        this.exp.text = "0";

        this.maxHp.text = string.Format("/  {0}", (enemy.maxhp + statusDto.jobHp).ToString());

        //210518 現状、ボス以外に顔アイコン表示は無いのでボスのみアイコン表示
        if (enemy.isBoss)
        {
            image.enabled = true;
            image.sprite = Resources.Load<Sprite>("Image/Charactors/" + enemy.pathName + "/status");
        }
        else
        {
            image.enabled = false;
        }
        

    }

}

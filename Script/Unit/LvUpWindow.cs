using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// レベルアップ項目を表示するウィンドウ
/// 戦闘画面の他、ステータス画面でもテスト用に使用する
/// </summary>
public class LvUpWindow : MonoBehaviour
{
    [SerializeField]    private Text name;
    [SerializeField]    private Text lv;

    [SerializeField]    private Text job;

    //各ステータス
    [SerializeField]    private Text hp;
    [SerializeField]    private Text hpUp;  //上昇すると「+1」と表示

    [SerializeField]    private Text latk;
    [SerializeField]    private Text latkUp;

    [SerializeField]    private Text catk;
    [SerializeField]    private Text catkUp;

    [SerializeField]    private Text agi;
    [SerializeField]    private Text agiUp;

    [SerializeField]    private Text dex;
    [SerializeField]    private Text dexUp;

    [SerializeField]    private Text luk;
    [SerializeField]    private Text lukUp;

    [SerializeField]    private Text ldef;
    [SerializeField]    private Text ldefUp;

    [SerializeField]    private Text cdef;
    [SerializeField]    private Text cdefUp;

    [SerializeField]    GameObject effect;

    //表示した時に呼んで初期化
    public void Init(Unit unit)
    {
        JobStatusDto statusDto = unit.job.statusDto;

        this.name.text = unit.name;
        this.job.text = unit.job.jobName.ToString();
        this.lv.text = unit.lv.ToString();
        this.hp.text = (unit.maxhp + statusDto.jobHp).ToString();
        this.latk.text = (unit.latk + statusDto.jobLatk).ToString();
        this.catk.text = (unit.catk + statusDto.jobCatk).ToString();
        this.agi.text = (unit.agi + statusDto.jobAgi).ToString();
        this.dex.text = (unit.dex + statusDto.jobDex).ToString();
        this.luk.text = (unit.luk + statusDto.jobLuk).ToString();
        this.ldef.text = (unit.ldef + statusDto.jobLdef).ToString();
        this.cdef.text = (unit.cdef + statusDto.jobCdef).ToString();

        //一旦全て非表示にする
        hpUp.enabled = false;
        latkUp.enabled = false;
        catkUp.enabled = false;
        agiUp.enabled = false;
        dexUp.enabled = false;
        lukUp.enabled = false;
        ldefUp.enabled = false;
        cdefUp.enabled = false;

    }

    //受け取ったテキスト更新
    public void UpdateText(StatusType upStatus)
    {
        //レベルアップ
        if (upStatus == StatusType.LV)
        {
            this.lv.text = (int.Parse(lv.text)+1).ToString();
            GameObject LvupEffect = Instantiate(effect, new Vector3(-4.54f, 3.15f, 0), Quaternion.identity) as GameObject;

        }
        if (upStatus == StatusType.HP)
        {
            Debug.Log("hp up");
            hpUp.enabled = true;
            GameObject LvupEffect = Instantiate(effect, new Vector3(-5.4f, 2.56f, 0), Quaternion.identity) as GameObject;

        }
        else if(upStatus == StatusType.LATK)
        {
            Debug.Log("latk up");
            latkUp.enabled = true;
            GameObject LvupEffect = Instantiate(effect, new Vector3(-5.4f, 2f, 0), Quaternion.identity) as GameObject;
        }
        else if(upStatus == StatusType.CATK)
        {
            Debug.Log("catk up");
            catkUp.enabled = true;
            GameObject LvupEffect = Instantiate(effect, new Vector3(-5.4f, 1.45f, 0), Quaternion.identity) as GameObject;
        }
        else if (upStatus == StatusType.AGI)
        {
            Debug.Log("agi up");
            agiUp.enabled = true;
            GameObject LvupEffect = Instantiate(effect, new Vector3(-5.4f, 0.9f, 0), Quaternion.identity) as GameObject;
        }
        else if (upStatus == StatusType.DEX)
        {
            Debug.Log("dex up");
            dexUp.enabled = true;
            GameObject LvupEffect = Instantiate(effect, new Vector3(-2.77f, 2.56f, 0), Quaternion.identity) as GameObject;
        }
        else if (upStatus == StatusType.LUK)
        {
            Debug.Log("luk up");
            lukUp.enabled = true;
            GameObject LvupEffect = Instantiate(effect, new Vector3(-2.77f, 2f, 0), Quaternion.identity) as GameObject;
        }
        else if (upStatus == StatusType.LDEF)
        {
            Debug.Log("ldef up");
            ldefUp.enabled = true;
            GameObject LvupEffect = Instantiate(effect, new Vector3(-2.77f, 1.45f, 0), Quaternion.identity) as GameObject;
        }
        else if (upStatus == StatusType.CDEF)
        {
            Debug.Log("cdef up");
            cdefUp.enabled = true;
            GameObject LvupEffect = Instantiate(effect, new Vector3(-2.77f, 0.9f, 0), Quaternion.identity) as GameObject;
        }

    }
}

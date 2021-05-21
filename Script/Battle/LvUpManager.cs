using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// レベルアップ判定を行うクラス
/// </summary>
public class LvUpManager : MonoBehaviour
{

    [SerializeField]
    GameObject lvUpWindow;

    [SerializeField]
    GameObject lvupImpreWindow;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip pin;

    //成長率データベース
    private GrowthDatabase growthDatabase;

    private LvUpImpreDatabase lvUpImpreDatabase;

    private UnitDatabase unitDatabase;

    //レベルアップ処理用のインデックス
    int index;

    //経過時間
    float elapsedTime;

    //ピンピンする時間の間隔
    float exTime = 0.2f;

    //感想を表示したかどうか 処理が入るのを防ぐ
    bool isImpreShow;

    //コンストラクタ
    public void init()
    {
        //audioSource = new AudioSource();
        //アセットファイルを取得
        growthDatabase = Resources.Load<GrowthDatabase>("growthDatabase");
        lvUpImpreDatabase = Resources.Load<LvUpImpreDatabase>("lvUpImpreDatabase");
        unitDatabase = Resources.Load<UnitDatabase>("unitDatabase");
    }

    //レベルアップするメソッド 直接Static型のUnitController.unitListを変更
    public List<StatusType> lvup(string lvUpUnitName,int resultExp)
    {
        //上がった要素をstring型リストに詰めて返す
        var lvUpList = new List<StatusType>();

        List<Unit> unitList = UnitController.unitList;
        List<Unit> newUnitList = new List<Unit>();

        foreach(var unit in unitList)
        {
            //リストの中でレベルアップしたユニットが居れば
            if(unit.name == lvUpUnitName)
            {
                //名前から成長率を取得　200719　Unitクラスに持たせた方が良いのでは・・・
                GrowthRate growthRate = growthDatabase.FindByName(lvUpUnitName);

                //職業成長率
                GrowthRateDto growthRateDto = unit.job.growthRateDto;

                //ユニット成長率と職業成長率を合算
                int hpRate = growthRate.hpRate + growthRateDto.jobHpRate;
                int latkRate = growthRate.latkRate + growthRateDto.jobLatkRate;
                int catkRate = growthRate.catkRate + growthRateDto.jobCatkRate;
                int agiRate = growthRate.agiRate + growthRateDto.jobAgiRate;
                int dexRate = growthRate.dexRate + growthRateDto.jobDexRate;
                int lukRate = growthRate.lukRate + growthRateDto.jobLukRate;
                int ldefRate = growthRate.ldefRate + growthRateDto.jobLdefRate;
                int cdefRate = growthRate.cdefRate + growthRateDto.jobCdefRate;

                //200719 同じステが2回上がるのを防止
                bool hpUpped = false;
                bool latkUpped = false;
                bool catkUpped = false;
                bool agiUpped = false;
                bool dexUpped = false;
                bool lukUpped = false;
                bool ldefUpped = false;
                bool cdefUpped = false;

                //Lv
                unit.lv++;
                unit.exp = resultExp;

                //210225 LVのピンピン処理に追加
                lvUpList.Add(StatusType.LV);

                
                

                //2ピン補正したか否か
                bool isRetry = false;
                //2ピン補正 上がったステが2個以下ならHPからループしなおし
                while(true)
                {
                    //各成長判定 成長率以下なら上がるものとする
                    //HP

                    //1～100の値を作成
                    float ran = Random.Range(1.0f, 100.0f);

                    if (ran <= hpRate && !hpUpped)
                    {

                        unit.maxhp += 1;
                        lvUpList.Add(StatusType.HP);

                        hpUpped = true;
                        if(lvUpList.Count > 2 && isRetry)
                        {
                            break;
                        }
                    }

                    ran = Random.Range(1.0f, 100.0f);

                    //遠距離攻撃
                    if (ran <= latkRate && !latkUpped)
                    {

                        unit.latk += 1;
                        lvUpList.Add(StatusType.LATK);
                        latkUpped = true;
                        if (lvUpList.Count > 2 && isRetry)
                        {
                            break;
                        }
                    }

                    ran = Random.Range(1.0f, 100.0f);

                    //近距離攻撃
                    if (ran <= catkRate && !catkUpped)
                    {

                        unit.catk += 1;
                        lvUpList.Add(StatusType.CATK);
                        catkUpped = true;
                        if (lvUpList.Count > 2 && isRetry)
                        {
                            break;
                        }
                    }

                    ran = Random.Range(1.0f, 100.0f);

                    //速さ
                    if (ran <= agiRate && !agiUpped)
                    {

                        unit.agi += 1;
                        lvUpList.Add(StatusType.AGI);
                        agiUpped = true;
                        if (lvUpList.Count > 2 && isRetry)
                        {
                            break;
                        }
                    }

                    ran = Random.Range(1.0f, 100.0f);

                    //技
                    if (ran <= dexRate && !dexUpped)
                    {

                        unit.dex += 1;
                        lvUpList.Add(StatusType.DEX);
                        dexUpped = true;
                        if (lvUpList.Count > 2 && isRetry)
                        {
                            break;
                        }
                    }

                    ran = Random.Range(1.0f, 100.0f);

                    //運
                    if (ran <= lukRate && !lukUpped)
                    {

                        unit.luk += 1;
                        lvUpList.Add(StatusType.LUK);
                        lukUpped = true;
                        if (lvUpList.Count > 2 && isRetry)
                        {
                            break;
                        }
                    }

                    ran = Random.Range(1.0f, 100.0f);

                    //遠距離防御
                    if (ran <= ldefRate && !ldefUpped)
                    {

                        unit.ldef += 1;
                        lvUpList.Add(StatusType.LDEF);
                        ldefUpped = true;
                        if (lvUpList.Count > 2 && isRetry)
                        {
                            break;
                        }
                    }

                    ran = Random.Range(1.0f, 100.0f);

                    //近距離防御
                    if (ran <= cdefRate && !cdefUpped)
                    {

                        unit.cdef += 1;
                        lvUpList.Add(StatusType.CDEF);
                        cdefUpped = true;
                        if (lvUpList.Count > 2 && isRetry)
                        {
                            break;
                        }
                    }

                    if (lvUpList.Count > 2)
                    {
                        break;
                    }

                    //下まで来たら2ピン補正に入る
                    isRetry = true;
                }
                newUnitList.Add(unit);
            }
            else
            {
                //レベルアップしたユニット以外はそのまま
                newUnitList.Add(unit);
            }
        }

        return lvUpList;
    }


    //初期化するだけ、BattleManagerから直接読んで良くないか？
    public void InitLvupWindow(Unit unit)
    {
        
        //210225 レベルアップのピンピンをカウントする用の変数を初期化
        index = 0;
        isImpreShow = false;

        //初期化して表示する
        lvUpWindow.GetComponent<LvUpWindow>().Init(unit);
        lvUpWindow.SetActive(true);
    }

    //レベルアップウィンドウとインプレウィンドウを閉じる
    public void closeLvupWindow()
    {
        lvUpWindow.SetActive(false);
        lvupImpreWindow.SetActive(false);

    }

    /// <summary>
    /// レベルアップしたパラメータリストを渡すだけで
    /// 一定時間おきにUIに表示してくれる
    /// </summary>
    /// <param name="lvUpList"></param>
    /// <returns></returns>
    public bool UpdateLvupWindow(List<StatusType> lvUpList, string unitName)
    {
        elapsedTime += Time.deltaTime;

        //出力した項目が最後の項目ならコメントを表示
        if (index >= lvUpList.Count)
        {
            if (!isImpreShow)
            {
                //最後にレベルアップ結果への感想を喋らせる
                selectLvUpImplre(lvUpList, unitName);
                isImpreShow = true;
            }
            

            return true;
        }

        //exTimeはピンピンする時間の間隔
        if (elapsedTime <= exTime)
        {
            return false;
        }
        elapsedTime = 0;

        lvUpWindow.GetComponent<LvUpWindow>().UpdateText(lvUpList[index]);

        //効果音
        audioSource.PlayOneShot(pin);

        //要素の最後でなければカウントを増やす
        index++;

        return false;
    }

    //レベルアップの成長のコメントを表示するメソッド
    public void selectLvUpImplre(List<StatusType> lvUpList, string unitName)
    {
        LvUpImpre lvUpImpre = lvUpImpreDatabase.FindByName(unitName);
        Unit unit = unitDatabase.FindByName(unitName);

        Sprite image = Resources.Load<Sprite>("Image/Charactors/" + unit.pathName + "/status");
        lvupImpreWindow.GetComponent<LvupImpreWindow>().UpdateImage(image);

        lvupImpreWindow.SetActive(true);

        //もしちゃんとコメントが3種類設定していなかったら最初のコメントを表示
        if (lvUpImpre.lvupImpre.Count !=3)
        {
            lvupImpreWindow.GetComponent<LvupImpreWindow>().UpdateText(lvUpImpre.lvupImpre[0]);
            return;
        }
        

        if (lvUpList.Count >= 0 && lvUpList.Count <= 3)
        {
            //0～2ピンした時
            lvupImpreWindow.GetComponent<LvupImpreWindow>().UpdateText(lvUpImpre.lvupImpre[0]);

        }
        else if (lvUpList.Count >= 3 && lvUpList.Count <= 6)
        {
            //3～5ピンした時
            lvupImpreWindow.GetComponent<LvupImpreWindow>().UpdateText(lvUpImpre.lvupImpre[1]);
        }
        else
        {
            //6～8ピンした時
            lvupImpreWindow.GetComponent<LvupImpreWindow>().UpdateText(lvUpImpre.lvupImpre[2]);

        }

        Debug.Log("上がった能力の数 = " + lvUpList.Count.ToString());
        //lvupImpreWindow.transform.DOMove(new Vector3(lvupImpreWindow.transform.position.x, lvupImpreWindow.transform.position.y + 50, lvupImpreWindow.transform.position.z), 1);
    }
        
}

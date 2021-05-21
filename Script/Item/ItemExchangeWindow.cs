using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 210222 アイテムの交換を行うウィンドウ
/// </summary>
public class ItemExchangeWindow : MonoBehaviour
{
    //アイテムを交換する二人の名前
    [SerializeField]
    private Text unitNameText;
    [SerializeField]
    private Text targetNameText;

    //レベル
    [SerializeField]
    private Text unitLv;
    [SerializeField]
    private Text targetLv;

    //経験値
    [SerializeField]
    private Text unitExp;
    [SerializeField]
    private Text targetExp;

    //HP
    [SerializeField]
    private Text unitHp;
    [SerializeField]
    private Text targetHp;

    [SerializeField]
    private Text unitMaxHp;
    [SerializeField]
    private Text targetMaxHp;

    //職業
    [SerializeField]
    private Text unitJob;
    [SerializeField]
    private Text targetJob;

    //画像
    [SerializeField]
    Image image;
    [SerializeField]
    Image targetImage;

    //ウィンドウ
    [SerializeField]
    GameObject unitItemWindow;
    [SerializeField]
    GameObject targetItemWindow;


    /// <summary>
    /// 210222 2人分のUnitOutline表示 面倒過ぎる
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="targetUnit"></param>
    public void Init(Unit unit, Unit targetUnit, StatusManager statusManager)
    {

        //アイテム以外のUnitOutline
        JobStatusDto statusDto = unit.job.statusDto;
        JobStatusDto targetStatusDto = targetUnit.job.statusDto;

        this.unitNameText.text = unit.name;
        this.targetNameText.text = targetUnit.name;

        this.unitLv.text = unit.lv.ToString();
        this.targetLv.text = targetUnit.lv.ToString();

        this.unitHp.text = (unit.hp + statusDto.jobHp).ToString();
        this.targetHp.text = (targetUnit.hp + targetStatusDto.jobHp).ToString();

        this.unitJob.text = unit.job.jobName.ToString();
        this.targetJob.text = targetUnit.job.jobName.ToString();

        this.unitExp.text = unit.exp.ToString();
        this.targetExp.text = targetUnit.exp.ToString();

        this.unitMaxHp.text = string.Format("/  {0}", (unit.maxhp + statusDto.jobHp).ToString());
        this.targetMaxHp.text = string.Format("/  {0}", (targetUnit.maxhp + targetStatusDto.jobHp).ToString());

        this.image.sprite = Resources.Load<Sprite>("Image/Charactors/" + unit.pathName + "/status");
        this.targetImage.sprite = Resources.Load<Sprite>("Image/Charactors/" + targetUnit.pathName + "/status");

        //2人のアイテム一覧を作成する
        CreateItemButton(unit, unitItemWindow, statusManager);
        CreateItemButton(targetUnit, targetItemWindow, statusManager);
    }

    //第一引数のユニットのアイテムを第二引数のウィンドウに表示していく
    private void CreateItemButton(Unit unit, GameObject itemWindow, StatusManager statusManager)
    {
        List<Item> itemList = unit.carryItem;

        //BattleMapManagerのOpenItemWindowを参考に作成
        //共通化は不活性、フォーカス等が有り、少し難しいと思う

        int index = 0;
        //アイテム一覧を作成
        foreach (Item item in itemList)
        {
                //Resources配下からボタンをロード
                var weaponButton = (Instantiate(Resources.Load("Prefabs/StatusItemButton")) as GameObject).transform;
                weaponButton.GetComponent<StatusItemButton>().Init(item, statusManager, unit, index);
                weaponButton.name = weaponButton.name.Replace("(Clone)", "");
                weaponButton.name += index;
                //partyWindowオブジェクト配下にprefab作成
                weaponButton.transform.SetParent(itemWindow.transform);
            index ++;
        }

        if (itemList.Count < 6)
        {
            int emptyButtonNum = 6 - itemList.Count;

            for (int i = 0; i < emptyButtonNum; i++)
            {
                //Resources配下からボタンをロード
                var weaponButton = (Instantiate(Resources.Load("Prefabs/StatusItemButton")) as GameObject).transform;
                //ボタン初期化 今はテキストのみ
                weaponButton.GetComponent<StatusItemButton>().InitEmptyButton(statusManager, unit, index);
                weaponButton.name = weaponButton.name.Replace("(Clone)", "");
                weaponButton.name += index;

                weaponButton.transform.SetParent(itemWindow.transform);
                index++;
            }

        }

    }

    /// <summary>
    /// アイテムを交換し終わった後にアイテムを再表示する
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="targetUnit"></param>
    public void ReloadUnitItem(Unit unit,int index, StatusManager statusManager)
    {
        //ボタンの更新を行う 交換したボタンを取得
        StatusItemButton statusItemButton = unitItemWindow.transform.Find("StatusItemButton" + index).GetComponent<StatusItemButton>();



        CreateItemButton(unit, unitItemWindow, statusManager);
    }

    public void ReloadTargetItem(Unit targetUnit, Item item, int index, StatusManager statusManager)
    {
        //ボタンの更新を行う 交換したボタンを取得
        StatusItemButton statusItemButton = targetItemWindow.transform.Find("StatusItemButton" + index).GetComponent<StatusItemButton>();

        CreateItemButton(targetUnit, targetItemWindow, statusManager);
    }

    //閉じる時にボタンの削除を行う
    public void DeleteUnitItem()
    {
        foreach (Transform obj in unitItemWindow.transform)
        {
            Destroy(obj.gameObject);
        }

    }

    public void DeleteTargetItem()
    {
        foreach (Transform obj in targetItemWindow.transform)
        {
            Destroy(obj.gameObject);
        }

    }

    public void FocusItemButton(int index, bool isLefttoRight)
    {
        if (isLefttoRight)
        {
            //フォーカスを変更 普通に左のユニットの一番上で良いか
            EventSystem.current.SetSelectedGameObject(this.transform.Find("UnitItemWindow/StatusItemButton" + index).gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(this.transform.Find("TargetItemWindow/StatusItemButton" + index).gameObject);
        }
        
    }

    /// <summary>
    /// ボタンを活性化
    /// </summary>
    public void InteractableButton()
    {
        foreach (Transform obj in unitItemWindow.transform)
        {
            obj.GetComponent<Button>().interactable = true;
        }

        foreach (Transform obj in targetItemWindow.transform)
        {
            obj.GetComponent<Button>().interactable = true;
        }
    }
}

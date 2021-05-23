using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 210222 �A�C�e���̌������s���E�B���h�E
/// </summary>
public class ItemExchangeWindow : MonoBehaviour
{
    //�A�C�e�������������l�̖��O
    [SerializeField]
    private Text unitNameText;
    [SerializeField]
    private Text targetNameText;

    //���x��
    [SerializeField]
    private Text unitLv;
    [SerializeField]
    private Text targetLv;

    //�o���l
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

    //�E��
    [SerializeField]
    private Text unitJob;
    [SerializeField]
    private Text targetJob;

    //�摜
    [SerializeField]
    Image image;
    [SerializeField]
    Image targetImage;

    //�E�B���h�E
    [SerializeField]
    GameObject unitItemWindow;
    [SerializeField]
    GameObject targetItemWindow;


    /// <summary>
    /// 210222 2�l����UnitOutline�\��
    /// </summary>
    /// <param name="unit">���������j�b�g</param>
    /// <param name="targetUnit">�����惆�j�b�g</param>
    public void Init(Unit unit, Unit targetUnit, StatusManager statusManager)
    {

        //�A�C�e���ȊO��UnitOutline
        JobStatusDto statusDto = unit.job.statusDto;
        JobStatusDto targetStatusDto = targetUnit.job.statusDto;

        //���O
        this.unitNameText.text = unit.name;
        this.targetNameText.text = targetUnit.name;

        //���x��
        this.unitLv.text = unit.lv.ToString();
        this.targetLv.text = targetUnit.lv.ToString();

        //HP
        this.unitHp.text = (unit.hp + statusDto.jobHp).ToString();
        this.targetHp.text = (targetUnit.hp + targetStatusDto.jobHp).ToString();

        //�E��
        this.unitJob.text = unit.job.jobName.ToString();
        this.targetJob.text = targetUnit.job.jobName.ToString();

        //�o���l
        this.unitExp.text = unit.exp.ToString();
        this.targetExp.text = targetUnit.exp.ToString();

        //HP
        this.unitMaxHp.text = string.Format("/  {0}", (unit.maxhp + statusDto.jobHp).ToString());
        this.targetMaxHp.text = string.Format("/  {0}", (targetUnit.maxhp + targetStatusDto.jobHp).ToString());

        //�摜
        this.image.sprite = Resources.Load<Sprite>("Image/Charactors/" + unit.pathName + "/status");
        this.targetImage.sprite = Resources.Load<Sprite>("Image/Charactors/" + targetUnit.pathName + "/status");

        //2�l�̃A�C�e���ꗗ���쐬����
        CreateItemButton(unit, unitItemWindow, statusManager);
        CreateItemButton(targetUnit, targetItemWindow, statusManager);
    }

    //�������̃��j�b�g�̃A�C�e����������̃E�B���h�E�ɕ\�����Ă���
    private void CreateItemButton(Unit unit, GameObject itemWindow, StatusManager statusManager)
    {
        List<Item> itemList = unit.carryItem;

        int index = 0;
        //�A�C�e���ꗗ���쐬
        foreach (Item item in itemList)
        {
                //Resources�z������{�^�������[�h
                var weaponButton = (Instantiate(Resources.Load("Prefabs/StatusItemButton")) as GameObject).transform;
                weaponButton.GetComponent<StatusItemButton>().Init(item, statusManager, unit, index);
                weaponButton.name = weaponButton.name.Replace("(Clone)", "");
                weaponButton.name += index;
                //partyWindow�I�u�W�F�N�g�z����prefab�쐬
                weaponButton.transform.SetParent(itemWindow.transform);
            index ++;
        }

        if (itemList.Count < 6)
        {
            int emptyButtonNum = 6 - itemList.Count;

            for (int i = 0; i < emptyButtonNum; i++)
            {
                //Resources�z������{�^�������[�h
                var weaponButton = (Instantiate(Resources.Load("Prefabs/StatusItemButton")) as GameObject).transform;
                //�{�^�������� ���̓e�L�X�g�̂�
                weaponButton.GetComponent<StatusItemButton>().InitEmptyButton(statusManager, unit, index);
                weaponButton.name = weaponButton.name.Replace("(Clone)", "");
                weaponButton.name += index;

                weaponButton.transform.SetParent(itemWindow.transform);
                index++;
            }

        }

    }

    /// <summary>
    /// �A�C�e�����������I�������ɃA�C�e�����ĕ\������
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="targetUnit"></param>
    public void ReloadUnitItem(Unit unit,int index, StatusManager statusManager)
    {
        //�{�^���̍X�V���s�� ���������{�^�����擾
        StatusItemButton statusItemButton = unitItemWindow.transform.Find("StatusItemButton" + index).GetComponent<StatusItemButton>();
        CreateItemButton(unit, unitItemWindow, statusManager);
    }

    public void ReloadTargetItem(Unit targetUnit, Item item, int index, StatusManager statusManager)
    {
        //�{�^���̍X�V���s�� ���������{�^�����擾
        StatusItemButton statusItemButton = targetItemWindow.transform.Find("StatusItemButton" + index).GetComponent<StatusItemButton>();

        CreateItemButton(targetUnit, targetItemWindow, statusManager);
    }

    //���鎞�Ƀ{�^���̍폜���s��
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

    //�t�H�[�J�X�ύX
    public void FocusItemButton(int index, bool isLefttoRight)
    {
        if (isLefttoRight)
        {
            //�t�H�[�J�X��ύX ���ʂɍ��̃��j�b�g�̈�ԏ�ŗǂ���
            EventSystem.current.SetSelectedGameObject(this.transform.Find("UnitItemWindow/StatusItemButton" + index).gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(this.transform.Find("TargetItemWindow/StatusItemButton" + index).gameObject);
        }
        
    }

    /// <summary>
    /// �{�^����������
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

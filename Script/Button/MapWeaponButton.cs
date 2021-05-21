using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapWeaponButton : MonoBehaviour
{
    //武器名
    [SerializeField]
    private Text weaponNameText;
    [SerializeField]
    private Text enduranceText;
    [SerializeField]
    private Image icon;//210215 武器のアイコンを設定
    [SerializeField]
    private Sprite[] iconList;
    [SerializeField]
    private Button button;

    private BattleManager battleManager;

    private Weapon weapon;

    private BattleMapManager battleMapManager;

    private Unit unit;

    //武器を装備出来ない場合の警告を作成
    private WeaponEquipWarn warn;

    //初期化メソッド
    public void Init(Weapon weapon, BattleManager battleManager, BattleMapManager battleMapManager,Unit unit)
    {
        this.unit = unit;

        //配下の名前、回数、値段を設定
        weaponNameText.text = weapon.name;
        this.weapon = weapon;
        enduranceText.text = string.Format("{0}/{1}", weapon.endurance.ToString(), weapon.maxEndurance.ToString());

        //Prefubは自分配下以外は[SerializeField]出来ないので、
        //初期化時にインスタンスを渡して貰う
        this.battleManager = battleManager;
        this.battleMapManager = battleMapManager;

        SetIcon(weapon);
    }


    private void SetIcon(Weapon weapon)
    {
        //200825 武器の種類によってアイコンを読み込む
        if (weapon.type == WeaponType.SHOT)
        {
            icon.sprite = iconList[0];
        }
        else if (weapon.type == WeaponType.LASER)
        {
            icon.sprite = iconList[1];
        }
        else if (weapon.type == WeaponType.STRIKE)
        {
            icon.sprite = iconList[2];
        }
        else if (weapon.type == WeaponType.HEAL)
        {
            icon.sprite = iconList[3];
        }

    }

    public void Onclick()
    {
        //if()
        //戻るボタンを押した時にフォーカスを戻すオブジェクト選択
        battleMapManager.selectedItem = this.gameObject;

        //警告が有ればメッセージウィンドウを開く処理を追加
        if (warn != WeaponEquipWarn.NONE)
        {
            battleMapManager.OpenMessageWindow(warn);
            return;
        }

        //回復の符でなければユニットに選択した武器を装備させる
        if(weapon.type != WeaponType.HEAL)
        {
            battleMapManager.Equip(unit, weapon);
            
        }
        else
        {
            //回復の杖は装備品にしたくないので別途変数に設定
            unit.equipHealRod = weapon;
            Debug.Log($"{unit.name}は{unit.equipHealRod.name}を装備した");
        }
        

        battleMapManager.Attack(weapon);
    }

    public void OnSelect()
    {
        //武器の詳細ウィンドウを更新する
        battleManager.changeWeaponDetailWindow(weaponNameText.text);
        
    }

    //ボタンを不活性にして文字もグレーアウトさせる
    public void setButtonDisinteractable()
    {
        button.interactable = false;
        weaponNameText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
        enduranceText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
    }

    //警告を追加すると同時にボタンを選択、クリック出来るがエラーとなる状態にする
    public void setWarn(WeaponEquipWarn warn)
    {
        this.warn = warn;

        //文字を灰色に
        weaponNameText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
        enduranceText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
    }

    
}

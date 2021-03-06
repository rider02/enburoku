using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 武器の詳細表示ウィンドウUI制御クラス
/// </summary>
public class DetailWindow : MonoBehaviour
{
    [SerializeField] Text weaponName;   //武器名
    [SerializeField] Text endurance;    //使用回数
    [SerializeField] Text detailText;   //特効、霊夢専用など
    [SerializeField] Text featureText;  //細かい説明文
    [SerializeField] Text attack;       //攻撃
    [SerializeField] Text hit;          //命中率
    [SerializeField] Text critical;     //必殺率
    [SerializeField] Text delay;        //重さ
    [SerializeField] Text skill;        //要求技能レベル
    [SerializeField] Text range;        //射程距離

    [SerializeField] private Image icon;//210215 武器のアイコンを設定
    [SerializeField] private Sprite[] iconList;

    /// <summary>
    /// 武器を選択されるとボタンから呼ばれるメソッド
    /// </summary>
    /// <param name="weapon"></param>
    public void UpdateText(Weapon weapon)
    {
        this.weaponName.text = weapon.name;
        this.endurance.text = string.Format("{0}/{1}", weapon.endurance.ToString(), weapon.endurance.ToString());
        this.attack.text = string.Format("{0}", weapon.attack.ToString());
        this.detailText.text = weapon.annotationText;
        this.hit.text = weapon.hitRate.ToString();
        this.critical.text = weapon.criticalRate.ToString();
        this.delay.text = weapon.delay.ToString();
        this.skill.text = weapon.skillLevel.ToString();


        //武器の射程距離表示 
        if (weapon.range == 1 && weapon.isCloseAttack)
        {
            //お払い棒とか1距離の武器
            this.range.text = "1";
        }
        else if (weapon.range != 1 && weapon.isCloseAttack)
        {
            //遠近両用の武器
            this.range.text = $"1-{weapon.range}";
        }
        else if (weapon.range != 1 && !weapon.isCloseAttack)
        {
            this.range.text = weapon.range.ToString();
        }
        

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

    //武器屋用ではない簡略化版ウィンドウ 使用回数、説明文を省略
    public void UpdateBattleWeaponText(Weapon weapon)
    {
        this.weaponName.text = weapon.name;
        this.attack.text = string.Format("{0}", weapon.attack.ToString());
        this.hit.text = weapon.hitRate.ToString();
        this.critical.text = weapon.criticalRate.ToString();
        this.delay.text = weapon.delay.ToString();
        this.skill.text = weapon.skillLevel.ToString();

        //武器の射程距離表示 
        if (weapon.range == 1 && weapon.isCloseAttack)
        {
            //お払い棒とか1距離の武器
            this.range.text = "1";
        }
        else if (weapon.range != 1 && weapon.isCloseAttack)
        {
            //遠近両用の武器
            this.range.text = $"1-{weapon.range}";
        }
        else if (weapon.range != 1 && !weapon.isCloseAttack)
        {
            this.range.text = weapon.range.ToString();
        }

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

        //210215 武器の説明
        if(weapon.featureText != null)
        {
            this.featureText.text = weapon.featureText;
        }
        else
        {
            this.featureText.text = "";
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoView : MonoBehaviour
{
    [SerializeField] Text unitName;//名前
    [SerializeField] Text unitLv;//Lv
    [SerializeField] Text unitHp;//HP

    public void UpdateText(Unit unit)
    {
        //名前
        unitName.text = string.Format("{0}", unit.name);
        //Lv
        unitLv.text = string.Format("Lv{0}", unit.lv);
        //HP
        unitHp.text = string.Format("HP{0}/{1}", unit.hp,unit.maxhp);
    }
}

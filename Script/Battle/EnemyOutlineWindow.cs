using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyOutlineWindow : MonoBehaviour
{
    [SerializeField]
    Text nameText;

    [SerializeField]
    Text lvNum;

    [SerializeField]
    Text maxHp;

    [SerializeField]
    Text hp;

    public void UpdateText(Enemy enemy)
    {

        this.nameText.text = enemy.name;

        this.lvNum.text = enemy.lv.ToString();

        this.hp.text = enemy.hp.ToString();

        this.maxHp.text = string.Format("/  {0}", enemy.maxhp.ToString());

    }
}

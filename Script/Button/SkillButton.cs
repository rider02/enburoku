using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//200726 ステータス画面にスキルを表示する用のボタン
public class SkillButton : MonoBehaviour
{
    //スキル名
    [SerializeField]
    private Text skillNameText;
    private Skill skill;

    private StatusManager statusManager;
    private BattleMapManager battleMapManager;

    public void Init(StatusManager statusManager, BattleMapManager battleMapManager)
    {
        this.statusManager = statusManager;
        this.battleMapManager = battleMapManager;
    }

    //初期化メソッド(ステータス画面で使用する場合)
    public void Init(Skill skill, StatusManager statusManager)
    {
        //配下の名前、回数、値段を設定
        skillNameText.text = skill.ToString();
        this.skill = skill;

    }

    //空のボタン初期化メソッド
    public void InitEmptyButton(StatusManager statusManager, BattleMapManager battleMapManager)
    {
        //配下の名前、回数、値段を設定
        this.skill = Skill.NONE;
        skillNameText.text = "";
        this.statusManager = statusManager;
        this.battleMapManager = battleMapManager;

    }



    public void Onclick()
    {

        //とりあえず無し
    }

    public void OnSelect()
    {
        //StatusManagerとBattleMapManagerで初期化されている場合の2種類が有る
        if(statusManager != null)
        {
            statusManager.chageStatusSkillDetailWindow(skill, this.transform);
        }
        else if (battleMapManager != null)
        {
            //210226 TODO まだマップ用ではフォーカスで詳細が出る機能無し 忘れないように
        }
    }
}

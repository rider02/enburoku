using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 200726 ステータス画面にスキルを表示する用のボタン
/// ステータス画面、戦闘画面の2シーンのステータス画面から共通で使用される
/// </summary>
public class SkillButton : MonoBehaviour
{
    [SerializeField] private Text skillNameText;    //スキル名
    private Skill skill;                            //スキル

    //参照
    private StatusManager statusManager;
    private BattleMapManager battleMapManager;

    //初期化メソッド 戦闘画面
    public void Init(StatusManager statusManager, BattleMapManager battleMapManager)
    {
        this.statusManager = statusManager;
        this.battleMapManager = battleMapManager;
    }

    //初期化メソッド ステータス画面
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

        //TODO 210522もしスキル付け外しを実装するならここに処理を追加
    }

    //選択するとスキルの解説ウィンドウを表示する
    public void OnSelect()
    {
        //StatusManagerとBattleMapManagerで初期化されている場合の2種類が有る
        if(statusManager != null)
        {
            statusManager.chageStatusSkillDetailWindow(skill, this.transform);
        }
        else if (battleMapManager != null)
        {
            //210226 TODO まだマップ用ではフォーカスで詳細が出る機能無し
        }
    }
}

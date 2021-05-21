using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//210217 マップやステータス画面のアイテムボタン
//お店には別途値段を表示したShopItemButtonを設ける
public class PotionButton : MonoBehaviour
{
    [SerializeField]
    private Text potionNameText;
    [SerializeField]
    private Text enduranceText;
    [SerializeField]
    private Button button;

    private Potion potion;

    private BattleMapManager battleMapManager;

    private StatusManager statusManager;

    private Unit unit;

    //210222 ステータス画面で使用する場合はセレクト時に詳細を切り替える必要はない筈
    private bool isStatusScene;

    //アイコンは固定だが、もし薬以外のアイコンが有れば実装する

    //戦闘マップでの表示用
    public void Init(Potion potion, BattleMapManager battleMapManager, Unit unit)
    {
        this.potionNameText.text = potion.name;
        this.unit = unit;
        this.potion = potion;
        this.battleMapManager = battleMapManager;
        enduranceText.text = string.Format("{0}/{1}", potion.useCount, potion.maxUseCount);
    }

    //ステータス画面での表示用
    public void Init(Potion potion, StatusManager statusManager, Unit unit)
    {
        isStatusScene = true;
        this.potionNameText.text = potion.name;
        this.unit = unit;
        this.potion = potion;
        this.statusManager = statusManager;
        enduranceText.text = string.Format("{0}/{1}", potion.useCount, potion.maxUseCount);
    }

    public void Onclick()
    {
        //確認ウィンドウ表示処理
        battleMapManager.OpenUseItemConfirmWindow(potion);

        //戻るボタンを押した時のフォーカスを設定
        battleMapManager.selectedItem = this.gameObject;
    }

    public void OnSelect()
    {
        if (!isStatusScene)
        {
            //武器の詳細ウィンドウを更新する
            battleMapManager.changePotionDetailWindow(potion);
        }
        
    }

    //ボタンを不活性にして文字もグレーアウトさせる
    public void setButtonDisinteractable()
    {
        button.interactable = false;
        potionNameText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
        enduranceText.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
    }
}

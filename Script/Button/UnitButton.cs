﻿using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// ステータス画面でのユニット一覧のボタン
/// </summary>
public class UnitButton : MonoBehaviour
{
    //ボタン配下のテキスト
    [SerializeField]
    public Text buttonText;

    [SerializeField]
    private Image unitImage;

    private StatusManager statusManager;

    //210221 押してユニットのチュートリアルを開く時、画像と種族を取り出す為に持たせておく
    private string pathName;

    //初期化メソッド
    public void Init(string unitName, StatusManager statusManager ,string pathName)
    {
        //配下のテキストを変更
        buttonText.text = unitName;
        //Prefubは自分配下以外は[SerializeField]出来ないので、
        //初期化時にインスタンスを渡して貰う
        this.statusManager = statusManager;

        this.pathName = pathName;

        //210221 旧ユニットボタンがダサすぎたので更新
        this.unitImage.sprite = Resources.Load<Sprite>("Image/ButtonImage/" + pathName);

    }

    //クリックされた時
    public void Onclick()
    {
        //210221 ユニット性能一覧表示時のこのボタンは振る舞いが違うので、フォーカスを取得してはいけない
        if(statusManager.menuMode != MenuMode.TUTORIAL_SELECT)
        {
            //キャンセルボタンを押した時のフォーカス設定
            statusManager.nestedReturnButton = this.gameObject;
        }
        else
        {
            statusManager.tutorialnestedReturnButton = this.gameObject;
        }
        

        //menuModeによってボタンの機能を変える
        //ステータス画面を表示する時
        if (statusManager.menuMode == MenuMode.STATUS)
        {
            //ボタン名からステータスウィンドウの文字列をユニット名に変更
            statusManager.setStatusWindowText(buttonText.text);
            statusManager.OpenWindow("StatusWindow");
            statusManager.menuMode = MenuMode.STATUS_BROWSE;
        }
        else if (statusManager.menuMode == MenuMode.LVUP)
        {
            //レベルアップ処理呼び出し
            Debug.Log($"レベルアップ:{buttonText.text}");
            statusManager.LvUp(buttonText.text);
            

        }
        else if (statusManager.menuMode == MenuMode.TALK)
        {
            //支援会話モードの場合
            statusManager.OpenTalkListWindow(buttonText.text);

        }
        else if (statusManager.menuMode == MenuMode.CLASSCHANGE)
        {

            //200719 クラスチェンジの場合
            //ボタンのユニット名を渡してクラスチェンジ先を開く
            statusManager.OpenClassChangeDestinationWindow(buttonText.text);

        }
        else if (statusManager.menuMode == MenuMode.TUTORIAL_SELECT)
        {
            //210221 チュートリアル選択の時
            statusManager.OpenUnitTutorialWindow(buttonText.text);

        }
        else if (statusManager.menuMode == MenuMode.ITEM_UNIT_SELECT)
        {
            //身支度でユニット一覧が表示されている時
            //アイテム交換対象を記憶しておく
            statusManager.exchangeTargetUnitButton = this.gameObject;
            statusManager.OpenItemControlWindow(buttonText.text, this.transform);
        }
        else if (statusManager.menuMode == MenuMode.ITEM_EXCHANGE_TARGET_SELECT)
        {
            //アイテムを交換する対象キャラを選択した時

            
            statusManager.OpenItemExchangeWindow(buttonText.text);
        }
    }

    //選択された時
    public void OnSelect()
    {

        statusManager.changeUnitOutlineWindow(buttonText.text);

        //210221 ユニットの手持ちアイテムを表示する機能を追加
        statusManager.ChangeUnitItemWindow(buttonText.text);
    }

}

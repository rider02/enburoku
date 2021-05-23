using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// ステータス画面を制御するクラス
/// </summary>
public class StatusManager : MonoBehaviour
{

    //　ステータスウインドウの全部の画面
    [SerializeField]
    private GameObject[] windowLists;

    [SerializeField] GameObject cullentWindow;              //現在表示しているウィンドウ
    [SerializeField] UnitController unitController;         //ユニット一覧
    [SerializeField] CashManager cashManager;               //お金の制御
    [SerializeField] FadeInOutManager fadeInOutManager;     //フェードイン、フェードアウト制御
    [SerializeField] TalkManager talkManager;               //支援会話制御
    [SerializeField] TimeWindow timeWindow;                 //プレー時間制御
    [SerializeField] SaveAndLoadManager saveAndLoadManager; //セーブ、ロード
    [SerializeField] ClassChangeManager classChangeManager; //クラスチェンジ
    [SerializeField] ModeManager modeManager;               //ルート、難易度、敗北ユニットの処理
    [SerializeField] UnitOutlineWindow unitOutlineWindow;   //ユニットを選択した時の概要ウィンドウ
    [SerializeField] ModeWindow modeWindow;                 //左上に「ステータス」、「装備」等モードを表示するウィンドウ
    [SerializeField] Image characterImage;                  //霊夢かレミリアの立ち絵
    [SerializeField] Image backGroundImage;                 //背景画像
    [SerializeField] GameObject menuWindow;                 //メニューウィンドウ

    [SerializeField] LvUpManager lvUpManager;               //レベルアップ制御
    [SerializeField] ItemInventory itemInventory;           //アイテム倉庫
    [SerializeField] GameObject itemDepositWindow;          //アイテムを預けるウィンドウ
    [SerializeField] GameObject inventoryWindow;            //倉庫ウィンドウ

    [SerializeField] GameObject detailView;
    [SerializeField] GameObject statusDetailView;           //ステータス詳細ウィンドウ
    [SerializeField] GameObject weaponDetailWindow;         //武器詳細ウィンドウ
    [SerializeField] GameObject itemDetailWindow;           //道具詳細ウィンドウ
    [SerializeField] GameObject statusWeaponDetailWindow;   //ステータス画面時の武器詳細ウィンドウ
    [SerializeField] GameObject statusItemDetailWindow;     //ステータス画面時の道具詳細ウィンドウ
    [SerializeField] GameObject statusSkillDetailWindow;    //ステータス画面時のスキル詳細ウィンドウ
    [SerializeField] UseItemManager useItemManager;         //道具使用制御

    [SerializeField] GameObject classChangeDetailWindow;    //転職先の詳細を表示するウィンドウ
    [SerializeField] GameObject messageWindow;

    //　自身の親のCanvasGroup
    private CanvasGroup canvasGroup;
    //　200726 戻るボタンを押した時フォーカスするオブジェクト保持
    public GameObject returnButton;

    //210207 戻るボタンを押した時フォーカスするオブジェクト保持(ネストしたUI用)
    public GameObject nestedReturnButton;

    //210221 チュートリアルのネストは3段階なので更に作成
    public GameObject tutorialnestedReturnButton;

    //アイテム交換関連
    //2人目のアイテム交換対象
    public GameObject exchangeTargetUnitButton;

    //210222 アイテムの交換を行うユニットの名前
    Unit itemControllUnit;
    Unit itemExchangeUnit;

    //交換するアイテムを保持
    Item exchangeItem;
    int exchangeItemIndex;
    
    //預ける関連
    //預けるアイテムを保持
    Item depositItem;

    //「預ける」ウィンドウ表示時にキャンセルを押した時フォーカスを戻す先
    GameObject depositItemButton;
    int depositItemButtonIndex;
    
    //各アセットファイル
    UnitDatabase unitDatabase;
    JobDatabase jobDatabase;
    WeaponDatabase weaponDatabase;
    PotionDatabase potionDatabase;
    AccessoryDatabase accessoryDatabase;
    ToolDatabase toolDatabase;

    //BGMプレイヤー
    BGMPlayer bgmPlayer;

    //210208 シーンをまたがる効果音再生用
    AudioSource audioSource;

    //メッセージウィンドウを表示しているか
    private bool ismassageActive;

    //210207 暗転中はメニューを表示しない
    private bool isInitFinish = false;

    //210226 詳細ウィンドウを表示しているか
    private bool isDetailVisible = false;

    //アイテム操作ウィンドウの離隔
    private const int ITEM_CONTROL_WINDOW_DISTANCE = 260;

    //210214 メニュー画面でレベルアップの表示テストを行う為の変数
    List<StatusType> lvUpList;
    Unit lvupUnit;

    //ステータス画面の状態
    public MenuMode menuMode {get; set; }

    void Start()
    {
        //立ち絵と背景の設定
        if (ModeManager.route == Route.REIMU)
        {
            //霊夢ルート
            this.characterImage.sprite = Resources.Load<Sprite>("Image/Charactors/Reimu/normal");
            this.backGroundImage.sprite = Resources.Load<Sprite>("Image/BackGrounds/jinja");
        }
        else
        {
            //紅魔ルート
            this.characterImage.sprite = Resources.Load<Sprite>("Image/Charactors/Remilia/normal");
            this.backGroundImage.sprite = Resources.Load<Sprite>("Image/BackGrounds/kouma2");
        }

        //200711 SaveAndLoadManager初期化
        saveAndLoadManager.Init(fadeInOutManager);

        canvasGroup = GetComponentInParent<CanvasGroup>();

        //支援会話リスト初期化
        talkManager.init(this);

        //unitDatabase初期化
        unitDatabase = Resources.Load<UnitDatabase>("unitDatabase");

        //200724 各データベース初期化
        jobDatabase = Resources.Load<JobDatabase>("jobDatabase");
        weaponDatabase = Resources.Load<WeaponDatabase>("weaponDatabase");
        potionDatabase = Resources.Load<PotionDatabase>("potionDatabase");
        toolDatabase = Resources.Load<ToolDatabase>("toolDatabase");
        accessoryDatabase = Resources.Load<AccessoryDatabase>("AccessoryDatabase");

        //仲間ユニット一覧初期化
        if (!UnitController.isInit) {

            unitController.InitUnitList(unitDatabase);
        }

        //ユニットボタン作成
        unitController.InitPartyList(this);

        //201719 クラスチェンジ機能初期化
        classChangeManager.Init(this, jobDatabase, unitController);

        useItemManager.Init(this);

        if (!CashManager.isCashInit) {
        //お金を初期化
            cashManager.Init();
        }
        cashManager.UpdateText();

        //時間ウィンドウを初期化
        timeWindow.init();

        //進行度初期化
        if (!ChapterManager.isChapterInit)
        {
            ChapterManager.Init();
        }

        //初期化されていなければ手持ちアイテムリスト初期化
        if (!ItemInventory.isInventoryInit)
        {
            itemInventory.Init();
        }

        //20200712 モードを初期化は、設定しないと列挙型なのでカジュアル、霊夢ルート、ノーマルとなるので不要

        //210514 キーコンフィグ初期化
        if (KeyConfigManager.configMap == null)
        {
            string configFilePath = Application.persistentDataPath + "/keyConfig";
            KeyConfigManager.InitKeyConfig(configFilePath);
        }

        //セーブ、ロードボタン作成
        saveAndLoadManager.createSaveAndLoadButton();


        menuMode = MenuMode.STATUS;

        lvUpManager.init();

        //210205 BGMPlayerをタイトルから引き継いでなければ作成？
        GameObject bgmManager = GameObject.Find("BGMManager");
        if(bgmManager == null)
        {
            bgmManager = (Instantiate(Resources.Load("Prefabs/BGMManager")) as GameObject);
            bgmManager.name = bgmManager.name.Replace("(Clone)", "");
            
        }
        bgmPlayer = bgmManager.GetComponent<BGMPlayer>();

        //210206 BGM変更 既にステータス画面の曲が流れてる場合は再生しない
        if(BGMType.STATUS != bgmPlayer.playingBGM) { 
            bgmPlayer.ChangeBGM(BGMType.STATUS);
            bgmPlayer.PlayBGM();
        }

        //効果音再生用
        audioSource = GameObject.Find("BGMManager").GetComponent<AudioSource>();

        //210221 ステータス画面に戻ってくると仲間全員のHP回復 
        Rest();

        fadeInOutManager.FadeinStart();
    }

    void Update()
    {
        //時間を更新
        PlayTimeManager.TimeUpdate();
        timeWindow.UpdateTime();

        //210207 暗転中は操作しない
        if (fadeInOutManager.isFadeinFadeout())
        {
            return;
        }


        //初期化完了時のみメニューウィンドウ表示
        if (!isInitFinish)
        {
            menuWindow.SetActive(true);
            //最初のボタンを選択
            EventSystem.current.SetSelectedGameObject(menuWindow.transform.Find("DepartureButton").gameObject);
            isInitFinish = true;
        }

        //決定ボタン
        if (KeyConfigManager.GetKeyDown(KeyConfigType.SUBMIT))
        {
            SubmitButton();
        }

        //メニューボタン
        if (KeyConfigManager.GetKeyDown(KeyConfigType.MENU) || Input.GetButtonDown("Menu"))
        {
            MenuButton();
        }

        //キャンセルボタン
        if (KeyConfigManager.GetKeyDown(KeyConfigType.CANCEL) || Input.GetButtonDown("Cancel"))
        {

            CancelButton();
        }

        //210223 アイテム交換でボタン更新中にフォーカスが消滅してしまった場合
        if(menuMode == MenuMode.ITEM_EXCHANGE)
        {
            if(EventSystem.current.currentSelectedGameObject == null)
            {
                Debug.Log("ItemButtonを取得出来ませんでした");
                GameObject unitItemWindow = getWindow("ItemExchangeWindow");
                EventSystem.current.SetSelectedGameObject(unitItemWindow.transform.Find("UnitItemWindow/StatusItemButton0").gameObject);
            }
        }
        else if (menuMode == MenuMode.ITEM)
        {
            if(EventSystem.current.currentSelectedGameObject == null)
            {
                Debug.Log("ItemButtonを取得出来ませんでした");
                GameObject weaponWindow = getWindow("WeaponWindow");
                EventSystem.current.SetSelectedGameObject(weaponWindow.transform.Find("StatusItemButton" + depositItemButtonIndex).gameObject);
            }
            
        }
        else if (menuMode == MenuMode.LVUP_EFFECT)
        {
            bool isLvupEnd;

            //210225 レベルアップの処理中
            isLvupEnd = lvUpManager.UpdateLvupWindow(lvUpList, lvupUnit.name);

            //レベルアップ処理が終わるまで何も出来ない
            if (!isLvupEnd){
                return;
            }
            //レベルアップ処理が終わっていたら
            menuMode = MenuMode.LVUP_END;
            Debug.Log($"menuMode:{menuMode}");

        }

    }

    //モードのセットとログ出力
    public void SetMenuMode(MenuMode menuMode)
    {
        this.menuMode = menuMode;
        Debug.Log($"menuMode:{menuMode}");
    }

    //決定ボタン
    public void SubmitButton()
    {
        //いつでもUGUIのボタンクリックは優先する
        KeyConfigManager.ButtonClick();

        if (menuMode == MenuMode.LVUP_END)
        {
            //レベルアップ完了時
            //レベルアップウィンドウと感想のウィンドウを閉じる
            lvUpManager.closeLvupWindow();

            //元の状態へ
            SetMenuMode(MenuMode.LVUP);
            return;
        }
        //メッセージウィンドウが表示され、ボタンを選択されていない時のみウィンドウを閉じる
        if (ismassageActive)
        {
            //CloseMessageWindow();
            return;
        }
        else
        {
            return;
        }
    }

    //メニューボタン
    public void MenuButton()
    {
        //ステータスウィンドウを開いている時のみ動作する
        if(cullentWindow.name == "StatusWindow")
        {

            if (!isDetailVisible)
            {
                isDetailVisible = true;
                statusDetailView.SetActive(true);
                EventSystem.current.SetSelectedGameObject(cullentWindow.transform.Find("EquipList/StatusItemButton").gameObject);
            }
            else
            {
                //既に表示済みの場合は何も選択せず、詳細ウィンドウも閉じた状態へ
                isDetailVisible = false;
                statusDetailView.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

    /// <summary>
    /// 200724 キャンセルボタン
    /// モードによって色々UI制御を行う
    /// </summary>
    public void CancelButton()
    {
        //200725 決定ボタンでもキャンセルボタンでもメッセージウィンドウを消す
        if (cullentWindow == messageWindow)
        {
            CloseMessageWindow();
            return;
        }

        //ステータスウィンドウ表示中なら
        if (cullentWindow.name == "StatusWindow" || cullentWindow.name == "TalkListWindow"
            || cullentWindow.name == "ClassChangeDestinationWindow")
        {
            Debug.Log("close window" + cullentWindow.name);

            //200726 スキル一覧、装備品一覧を削除する
            if (cullentWindow.name == "StatusWindow")
            {
                var window = getWindow("StatusWindow");
                window.GetComponent<StatusWindow>().DeleteSkillButtonAndItemButton();

                //詳細情報表示をオフに
                statusDetailView.SetActive(false);
                isDetailVisible = false;

                SetMenuMode(MenuMode.STATUS);
            }

            //キャンセルボタンを押したタイミングで支援リストを削除する
            else if (cullentWindow.name == "TalkListWindow")
            {
                DeleteFriendPanel();
            }

            //200719 転職先を表示するウィンドウならボタンを削除する
            else if (cullentWindow.name == "ClassChangeDestinationWindow")
            {

                classChangeManager.DeleteClassChangeButton();

                //詳細ウィンドウも閉じる
                classChangeDetailWindow.SetActive(false);
            }

            foreach (var item in windowLists)
            {
                if (item.name == "PartyWindow")
                {
                    item.SetActive(true);
                    cullentWindow = item;
                    EventSystem.current.SetSelectedGameObject(null);
                }
                else
                {
                    item.SetActive(false);
                }
            }

            //200726 unitOutlineWindowを表示
            unitOutlineWindow.gameObject.SetActive(true);

            //　押したボタンをアクティブにする
            EventSystem.current.SetSelectedGameObject(nestedReturnButton);

        }
        else if (cullentWindow.name == "PartyWindow" || cullentWindow.name == "SaveAndLoadWindow" 
            || cullentWindow.name == "TutorialWindow")
        {
            //パーティ一覧、セーブ、ロードウィンドウを表示中の場合

            if(menuMode != MenuMode.ITEM_EXCHANGE_TARGET_SELECT)
            {
                //windowListからMenuWindowを探してアクティブにする
                foreach (var item in windowLists)
                {
                    if (item.name == "MenuWindow")
                    {
                        //cullentWindowをMenuWindowにする
                        item.SetActive(true);
                        cullentWindow = item;
                        EventSystem.current.SetSelectedGameObject(null);
                    }
                    else
                    {
                        //それ以外のウィンドウは非表示にする
                        item.SetActive(false);
                    }
                }

                //unitOutlineWindow閉じる
                unitOutlineWindow.gameObject.SetActive(false);

                SetMenuMode(MenuMode.ROOT);
                //テキストを更新する
                modeWindow.UpdateText(menuMode.GetStringValue());

                //210224 隙間ウィンドウ閉じる
                inventoryWindow.SetActive(false);

                //　遷移前に押したボタンをアクティブにする
                EventSystem.current.SetSelectedGameObject(returnButton);
            }
            else if(menuMode == MenuMode.ITEM_EXCHANGE_TARGET_SELECT)
            {
                //210222 アイテムの交換を行うユニットを選択する時
                //モード変更
                SetMenuMode(MenuMode.ITEM_MENU);

                //左上の表示を変更
                modeWindow.UpdateText(menuMode.GetStringValue());

                //パーティ一覧取得
                GameObject partyWindow = getWindow("PartyWindow");
                
                foreach (Transform unitButton in partyWindow.transform)
                {
                    //ボタンを不活性にしてしまっているので、全員活性化する
                    if (unitButton.GetComponent<UnitButton>().buttonText.text == itemControllUnit.name)
                    {
                        unitButton.GetComponent<Button>().interactable = true;
                    }

                }

                //「持ち物」「交換」「全預け」と表示されたウィンドウを開いて戻る
                GameObject itemControllWindow = getWindow("ItemControlWindow");
                cullentWindow = itemControllWindow;
                itemControllWindow.SetActive(true);

                // フォーカス変更
                EventSystem.current.SetSelectedGameObject(itemControllWindow.transform.Find("ExchangeItemButton").gameObject);

            }

        }
        else if (cullentWindow.name == "UnitTutorialListWindow")
        {
            //210221 チュートリアルのカテゴリを開き、ユニット一覧、戦闘マップ詳細等を開いている状態
            //そろそろcullentWindowでの制御限界になってきた
            menuMode = MenuMode.TUTORIAL_CATEGORY_SELECT;

            //UnitButtonを削除する
            foreach (Transform obj in cullentWindow.transform)
            {
                Destroy(obj.gameObject);
            }

            cullentWindow.SetActive(false);

            //前のウィンドウを開く
            OpenWindow("TutorialWindow");

            //　遷移前に押したボタンをアクティブにする
            EventSystem.current.SetSelectedGameObject(nestedReturnButton);
        }
        else if (cullentWindow.name == "UnitTutorialWindow")
        {
            //ユニットの詳細を表示している時
            menuMode = MenuMode.TUTORIAL_SELECT;

            cullentWindow.SetActive(false);
            OpenWindow("UnitTutorialListWindow");
            EventSystem.current.SetSelectedGameObject(tutorialnestedReturnButton);
        }
        else if (cullentWindow.name == "ItemControlWindow")
        {
            //「持ち物」「交換」「全預け」ボタンを消す
            SetMenuMode(MenuMode.ITEM_UNIT_SELECT);
            cullentWindow.SetActive(false);


            GameObject partyWindow = getWindow("PartyWindow");
            cullentWindow = partyWindow;

            EventSystem.current.SetSelectedGameObject(nestedReturnButton);
        }
        else if (cullentWindow.name == "WeaponWindow")
        {
            //ユニットの手持ち道具ウィンドウを開いている時
            SetMenuMode(MenuMode.ITEM_UNIT_SELECT);
            GameObject partyWindow = getWindow("PartyWindow");
            partyWindow.SetActive(true);
            cullentWindow = partyWindow;

            //詳細ウィンドウ非表示
            detailView.SetActive(false);

            //隙間ウィンドウ閉じる
            inventoryWindow.SetActive(false);

            EventSystem.current.SetSelectedGameObject(nestedReturnButton);
        }
        else if (menuMode == MenuMode.ITEM_EXCHANGE)
        {
            //アイテム交換中の時
            //アイテム一覧へ戻す
            SetMenuMode(MenuMode.ITEM_UNIT_SELECT);

            //アイテム削除
            GameObject itemExchangeWindow = getWindow("ItemExchangeWindow");
            itemExchangeWindow.GetComponent<ItemExchangeWindow>().DeleteUnitItem();
            itemExchangeWindow.GetComponent<ItemExchangeWindow>().DeleteTargetItem();
            itemExchangeWindow.SetActive(false);

            //左上の表示を変更
            modeWindow.UpdateText(menuMode.GetStringValue());
            OpenWindow("PartyWindow");
            unitOutlineWindow.gameObject.SetActive(true);


            GameObject partyWindow = getWindow("PartyWindow");
            foreach (Transform unitButton in partyWindow.transform)
            {
                //ボタンを不活性にしてしまっているので、全員活性化する
                if (unitButton.GetComponent<UnitButton>().buttonText.text == itemControllUnit.name)
                {
                    unitButton.GetComponent<Button>().interactable = true;
                }

            }

            GameObject weaponWindow = getWindow("WeaponWindow");
            weaponWindow.SetActive(true);

            // フォーカス変更 記憶していた対象へ戻る
            EventSystem.current.SetSelectedGameObject(exchangeTargetUnitButton);

        }
        else if (menuMode == MenuMode.EXCHANGE_ITEM_SELECT)
        {
            //1つめのアイテムを選択している時
            SetMenuMode(MenuMode.ITEM_EXCHANGE);

            GameObject itemExchangeWindow = getWindow("ItemExchangeWindow");
            itemExchangeWindow.GetComponent<ItemExchangeWindow>().InteractableButton();
        }
        else if (menuMode == MenuMode.ITEM_DEPOSIT_MENU)
        {
            SetMenuMode(MenuMode.ITEM);

            //アイテムを「預ける」、「装備」ウィンドウを表示している時
            //ウィンドウを閉じる
            itemDepositWindow.SetActive(false);
            

            GameObject weaponWindow = getWindow("WeaponWindow");

            cullentWindow = weaponWindow;
            foreach (Transform weaponButton in weaponWindow.transform)
            {
                weaponButton.GetComponent<Button>().interactable = true;
            }

            //フォーカスを元のアイテムボタンへ戻す
            EventSystem.current.SetSelectedGameObject(depositItemButton);
            
        }
        else if (menuMode == MenuMode.RECEIVE_ITEM)
        {
            //隙間からアイテムを受け取る時
            //アイテム一覧まで戻る
            SetMenuMode(MenuMode.ITEM);

            GameObject weaponWindow = getWindow("WeaponWindow");
            cullentWindow = weaponWindow;
            foreach (Transform weaponButton in weaponWindow.transform)
            {
                weaponButton.GetComponent<Button>().interactable = true;
            }

            //フォーカスを元のアイテムボタンへ戻す
            EventSystem.current.SetSelectedGameObject(depositItemButton);
        }
    }//CancelButton

    //　ステータス画面のウインドウのオン・オフメソッド
    public void ChangeWindow(GameObject window)
    {
        foreach (var item in windowLists)
        {
            if (item == window)
            {
                Debug.Log("window active"+ window.name);
                item.SetActive(true);

                //現在のウィンドウをここで設定
                cullentWindow = item;

                //一度選択オブジェクトのフォーカスを外す
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                item.SetActive(false);
            }

            if (window.name == "PartyWindow")
            {
                //ボタンのフォーカスを変える
                //　それぞれのウインドウのMenuAreaの最初の子要素をアクティブな状態にする
                EventSystem.current.SetSelectedGameObject(window.transform.Find("UnitButton").gameObject);

                //200726 UnitDetailWindow開く
                unitOutlineWindow.gameObject.SetActive(true);
            }

            //200712 セーブウィンドウを開いた時、フォーカスを変更する
            else if(window.name == "SaveAndLoadWindow")
            {

                //　それぞれのウインドウのMenuAreaの最初の子要素をアクティブな状態にする
                EventSystem.current.SetSelectedGameObject(window.transform.Find("SaveAndLoadButton").gameObject);
            }

            //200719 クラスチェンジウィンドウ
            else if(window.name == "ClassChangeDestinationWindow")
            {

                //　それぞれのウインドウのMenuAreaの最初の子要素をアクティブな状態にする
                EventSystem.current.SetSelectedGameObject(window.transform.Find("ClassChangeButton").gameObject);
            }

            //200726 ステータスウインドウを開いた時はUnitOutlineWindow消す
            else if(window.name == "StatusWindow")
            {
                unitOutlineWindow.gameObject.SetActive(false);
            }

            else if (window.name == "TutorialWindow")
            {
                //2100221 フォーカス変更はChangeWindow内に書くルールだったのでここに記載
                EventSystem.current.SetSelectedGameObject(window.transform.Find("UnitTutorialButton").gameObject);
            }



            //200726 modeWindow表示とテキスト反映
            modeWindow.UpdateText(menuMode.GetStringValue());
            modeWindow.gameObject.SetActive(true);
        }
    }

    //ウィンドウ名から対象ウィンドウを開く
    public void OpenWindow(string windowName)
    {
        var window = getWindow(windowName);
        WindowOnOff(window);
    }

    //　ステータスウインドウを非アクティブにする
    public void DisableWindow()
    {
        if (canvasGroup == null || canvasGroup.interactable)
        {
            //　ウインドウを非アクティブにする
            transform.root.gameObject.SetActive(false);
        }
    }

    //　他の画面を表示する
    private void WindowOnOff(GameObject window)
    {
        if (canvasGroup == null || canvasGroup.interactable)
        {
            ChangeWindow(window);
        }
    }
    //　前の画面に戻るボタンを選択状態にする
    public void SelectReturnButton()
    {
        EventSystem.current.SetSelectedGameObject(returnButton);
    }

    //ユニットのステータス表示
    public void SetStatusWindowText(string unitName)
    {
        //名前から対象のユニットを取得
        var unit = unitController.findByName(unitName);

        //ボタン作成処理はStatusWindowクラスに任せる
        var window = getWindow("StatusWindow");
        if(window != null)
        {
            window.GetComponent<StatusWindow>().updateText(unit);

            //200825 装備している武器を設定する
            window.GetComponent<StatusWindow>().addEquipWeapon(unit.equipWeapon, this, null);

            //装備しているアクセサリを表示
            window.GetComponent<StatusWindow>().addEquipAccessory(unit.equipAccessory, this, null);

            //200726 手持ちアイテムから武器リストを作成する
            //ユニット名からユニットの持っている武器を取得
            List<Item> itemList = unit.carryItem;
            window.GetComponent<StatusWindow>().addInventoryList(itemList, this, null);

            //スキル一覧作成
            List<Skill> skillList = unit.job.skills;
            window.GetComponent<StatusWindow>().addSkillList(skillList, this, null);
        }
    }

    //ウィンドウ名から対象のgameObjectを返す
    private GameObject getWindow(string windowName)
    {
        foreach (var window in windowLists)
        {
            if(window.name == windowName)
            return window;
        }
        return null;
    }

    /// <summary>
    /// レベルアップテストモード
    /// </summary>
    public void OpenLvUpWindow()
    {
        returnButton = GameObject.Find("LvUpButton");

        //モードを変更
        SetMenuMode(MenuMode.LVUP);

        OpenWindow("PartyWindow");

    }

    /// <summary>
    /// 200725 メッセージウィンドウを表示
    /// </summary>
    /// <param name="text"></param>
    public void OpenMessageWindow(string text)
    {
        //メッセージ設定
        messageWindow.GetComponent<MessageWindow>().UpdateText(text);
        cullentWindow = messageWindow;

        //ボタンから選択外す
        EventSystem.current.SetSelectedGameObject(null);

        
        ismassageActive = true;
        //メッセージウィンドウ表示フラグ
        Debug.Log("ismassageActive" + ismassageActive);

        //ウィンドウ表示
        messageWindow.SetActive(true);

        //確認ボタンにフォーカス設定
        EventSystem.current.SetSelectedGameObject(messageWindow.transform.Find("ConfirmButton").gameObject);
    }

    //メッセージウィンドウ非表示
    public void CloseMessageWindow()
    {

        ismassageActive = false;
        messageWindow.SetActive(false);

        //ボタン活性化
        GameObject weaponWindow = getWindow("WeaponWindow");
        cullentWindow = weaponWindow;
        foreach (Transform weaponButton in weaponWindow.transform)
        {
            //ボタンを活性化する
            weaponButton.GetComponent<Button>().interactable = true;
        }

        //元のフォーカスへ
        EventSystem.current.SetSelectedGameObject(weaponWindow.transform.Find("StatusItemButton0").gameObject);
    }

    /// <summary>
    /// 200726 UnitButtonを選択したタイミングで文字を変更する
    /// </summary>
    /// <param name="unitName"></param>
    public void changeUnitOutlineWindow(string unitName)
    {
        Unit forcusUnit = unitController.findByName(unitName);
        unitOutlineWindow.UpdateText(forcusUnit);

    }

    /// <summary>
    /// クラスチェンジ関連
    /// </summary>

    // 200719 クラスチェンジしたいキャラを選択
    public void OpenClassChangeWindow()
    {
        returnButton = GameObject.Find("JubButton");

        //モードを変更
        SetMenuMode(MenuMode.CLASSCHANGE);

        //開くウィンドウはPartyWindowで、処理はUnitButtonに委ねる
        OpenWindow("PartyWindow");
    }

    /// <summary>
    /// 200719 クラスチェンジ先を選択する
    /// </summary>
    public void OpenClassChangeDestinationWindow(string unitName)
    {
        //ボタン名からユニットを取得して、転職先ボタンを作成
        var unit = unitController.findByName(unitName);
        classChangeManager.CreateClassChangeButton(unit, classChangeDetailWindow.GetComponent<ClassChangeDetailWindow>());
        OpenWindow("ClassChangeDestinationWindow");

        //転職先が存在すれば詳細ウィンドウも表示する 最上級職の場合は表示されない
        if ((unit.job.jobLevel != JobLevel.MASTER))
        {
            classChangeDetailWindow.SetActive(true);
        }
    }

    public void OpenStatusWindow()
    {
        returnButton = GameObject.Find("StatusButton");
        menuMode = MenuMode.STATUS;
        OpenWindow("PartyWindow");

    }

    /// <summary>
    /// 210222 アイテム交換関連 かなり大掛かりな内容
    /// TODO 別クラスに分けて整理する ウィンドウ制御が複雑なので、マップ画面と共通処理出来なさそう
    /// </summary>

    //210222 アイテム一覧を表示する
    public void OpenItemWindow()
    {
        returnButton = GameObject.Find("ItemButton");
        menuMode = MenuMode.ITEM_UNIT_SELECT;
        Debug.Log($"menuMode:{menuMode}");

        //アイテム交換はPartyWindowとUnitButtonで実施する
        //currentWindow設定とユニット一覧表示
        OpenWindow("PartyWindow");

        //ユニットの手持ちアイテム一覧ウィンドウを表示する
        GameObject weaponWindow = getWindow("WeaponWindow");
        weaponWindow.SetActive(true);

    }

    //210222 ユニットのボタンを選択時に呼ばれ、選択ユニットの手持ちアイテム一覧表示する
    public void ChangeUnitItemWindow(string unitName)
    {
        //無限に増えて行ってしまうので削除を行う
        GameObject weaponWindow = getWindow("WeaponWindow");
        foreach (Transform obj in weaponWindow.transform)
        {
            Destroy(obj.gameObject);
        }
        Unit forcusUnit = unitController.findByName(unitName);
        List<Item> itemList = forcusUnit.carryItem;

        int index = 0;

        //アイテム一覧を作成
        foreach (Item item in itemList)
        {

            //Resources配下からボタンをロード
            var weaponButton = (Instantiate(Resources.Load("Prefabs/StatusItemButton")) as GameObject).transform;
            weaponButton.GetComponent<StatusItemButton>().Init(item, this, forcusUnit, index);
            weaponButton.name = weaponButton.name.Replace("(Clone)", "");
            weaponButton.name += index;

            //partyWindowオブジェクト配下にprefab作成
            weaponButton.transform.SetParent(weaponWindow.transform,false);

            //ボタンを不活性にしてフォーカスを移動しないようにする
            weaponButton.GetComponent<Button>().interactable = false;
            index++;
        }
        if (itemList.Count < 6)
        {
            //6個未満の場合は空のアイテムボタン作成
            int emptyButtonNum = 6 - itemList.Count;

            for (int i = 0; i < emptyButtonNum; i++)
            {
                //Resources配下からボタンをロード
                var weaponButton = (Instantiate(Resources.Load("Prefabs/StatusItemButton")) as GameObject).transform;
                //ボタン初期化 今はテキストのみ
                weaponButton.GetComponent<StatusItemButton>().InitEmptyButton(this, forcusUnit, index);
                weaponButton.name = weaponButton.name.Replace("(Clone)", "");
                weaponButton.name += index;

                //partyWindowオブジェクト配下にprefab作成
                weaponButton.transform.SetParent(weaponWindow.transform,false);

                //partyWindowオブジェクト配下にprefab作成
                weaponButton.GetComponent<Button>().interactable = false;
                index++;
            }
        }
    }

    //210222 「持ち物」「交換」「全預け」でアイテムの操作を行うウィンドウ表示
    public void OpenItemControlWindow(string unitName, Transform unitButtonTransform)
    {
        //モード変更
        menuMode = MenuMode.ITEM_MENU;
        Debug.Log($"menuMode:{menuMode}");

        //アイテムの操作を行うユニットを控えておく
        Unit unit = unitController.findByName(unitName);
        itemControllUnit = unit;

        GameObject itemControllWindow = getWindow("ItemControlWindow");

        //ウィンドウは開くが、PartyWindowは消さないまま
        itemControllWindow.SetActive(true);
        cullentWindow = itemControllWindow;
        itemControllWindow.transform.position = new Vector3(unitButtonTransform.position.x - ITEM_CONTROL_WINDOW_DISTANCE, unitButtonTransform.position.y - 30, unitButtonTransform.position.z);

        //フォーカス変更
        EventSystem.current.SetSelectedGameObject(itemControllWindow.transform.Find("CarryItemButton").gameObject);
    }

    //アイテムの交換を行うユニットを選択する
    public void SelectItemExchangeUnit()
    {
        //モード変更
        menuMode = MenuMode.ITEM_EXCHANGE_TARGET_SELECT;
        Debug.Log($"menuMode:{menuMode}");

        //左上の表示を変更
        modeWindow.UpdateText(menuMode.GetStringValue());

        //パーティ一覧取得
        GameObject partyWindow = getWindow("PartyWindow");
        cullentWindow = partyWindow;

        GameObject focusUnitButton = null;
        bool isFocus = false;

        foreach (Transform unitButton in partyWindow.transform)
        {
            //ボタンを不活性にする
            if(unitButton.GetComponent<UnitButton>().buttonText.text == itemControllUnit.name)
            {
                unitButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                //選択したユニット以外の最初の要素をフォーカス変更用にGameObject取得
                if (!isFocus)
                {
                    isFocus = true;
                    focusUnitButton = unitButton.gameObject;
                }
                
            }
        }

        //「持ち物」「交換」「全預け」等のウィンドウを消す
        GameObject itemControllWindow = getWindow("ItemControlWindow");
        itemControllWindow.SetActive(false);

        //フォーカス変更
        EventSystem.current.SetSelectedGameObject(focusUnitButton);
    }

    /// <summary>
    /// 210222 アイテム交換対象を選択したので、専用の画面を開く 
    /// </summary>
    /// <param name="targetUnitName"></param>
    public void OpenItemExchangeWindow(string targetUnitName)
    {
        //ここでやっとモードをアイテム交換へ＾＾；
        menuMode = MenuMode.ITEM_EXCHANGE;
        Debug.Log($"menuMode:{menuMode}");

        //ウィンドウ取得
        GameObject itemExchangeWindow = getWindow("ItemExchangeWindow");

        //それ以外のウィンドウは閉じてしまうのでOpenWindowで
        //currentWindowも同時に設定
        OpenWindow("ItemExchangeWindow");

        //unitOutlineWindowは消えないので消す 戻すのを忘れないように
        unitOutlineWindow.gameObject.SetActive(false);

        Unit targetUnit = unitController.findByName(targetUnitName);

        itemExchangeWindow.GetComponent<ItemExchangeWindow>().Init(itemControllUnit, targetUnit, this);

        //フォーカスの変更 交換元ユニットの道具ボタンの最初の要素をセット
        EventSystem.current.SetSelectedGameObject(itemExchangeWindow.transform.Find("UnitItemWindow/StatusItemButton0").gameObject);
    }

    /// <summary>
    /// 210222 アイテムの交換の1個目のアイテムを選択する
    /// </summary>
    /// <param name="item"></param>
    public void PrepareItemExchange(Item item,int index, Unit unit)
    {
        //交換するアイテムをtempしておく
        itemExchangeUnit = unit;
        exchangeItem = item;
        exchangeItemIndex = index;
        if (exchangeItem != null)
        {
            Debug.Log($"{itemExchangeUnit.name}の{exchangeItem.ItemName}を選択しました");
        }
        else
        {
            Debug.Log($"{itemExchangeUnit.name}の空欄を選択しました");
        }
        

        //モード変更
        menuMode = MenuMode.EXCHANGE_ITEM_SELECT;
        Debug.Log($"menuMode:{menuMode}");

        //ウィンドウ取得
        GameObject itemExchangeWindow = getWindow("ItemExchangeWindow");

        //クリックされたボタンがアイテム交換元のユニット名の場合 相手の要素を選択
        if (unit == itemControllUnit)
        {
            EventSystem.current.SetSelectedGameObject(itemExchangeWindow.transform.Find("TargetItemWindow/StatusItemButton0").gameObject);
        }
        else
        {
            //交換先ユニットのボタンをクリックした場合、交換元の要素を選択
            EventSystem.current.SetSelectedGameObject(itemExchangeWindow.transform.Find("UnitItemWindow/StatusItemButton0").gameObject);
        }
    }

    /// <summary>
    /// 210222 やっとアイテムの交換を実行する
    /// </summary>
    /// <param name="item"></param>
    public void ItemExchange(Item targetItem, int index, Unit targetUnit)
    {
        ItemExchangeWindow itemExchangeWindow = getWindow("ItemExchangeWindow").GetComponent<ItemExchangeWindow>();

        //2人のアイテム交換を行っている場合
        if (targetUnit != itemExchangeUnit)
        {
            //最初に選択したユニットのアイテムを１番目に選択している場合、□→□
            if (itemControllUnit == itemExchangeUnit)
            {
                //アイテム交換実施
                //最初に選択したユニットのアイテムが有る場合
                if (exchangeItem != null)
                {
                    //←のユニットの装備解除処理
                    if (exchangeItem.isEquip == true)
                    {
                        //装備済みフラグをfalseにする
                        exchangeItem.isEquip = false;
                        if (exchangeItem.ItemType == ItemType.WEAPON)
                        {
                            itemControllUnit.equipWeapon = null;
                        }
                        else if (exchangeItem.ItemType == ItemType.ACCESSORY)
                        {
                            itemControllUnit.equipAccessory = null;
                        }
                    }

                    //互いのアイテムが存在
                    if (targetItem != null)
                    {
                        //→のユニットの装備解除処理
                        if (targetItem.isEquip == true)
                        {
                            targetItem.isEquip = false;
                            if (targetItem.ItemType == ItemType.WEAPON)
                            {
                                targetUnit.equipWeapon = null;
                            }
                            else if (targetItem.ItemType == ItemType.ACCESSORY)
                            {
                                targetUnit.equipAccessory = null;
                            }
                        }

                        itemExchangeUnit.carryItem[exchangeItemIndex] = targetItem;
                        targetUnit.carryItem[index] = exchangeItem;
                        Debug.Log($"{itemExchangeUnit.name}の{exchangeItem.ItemName}を{targetUnit.name}に渡しました");
                        Debug.Log($"{targetUnit.name}の{targetItem.ItemName}を{itemExchangeUnit.name}に渡しました");
                    }
                    else
                    {
                        //相手のアイテムが存在しない場合は自分のアイテム削除
                        itemExchangeUnit.carryItem.RemoveAt(exchangeItemIndex);

                        //相手へのアイテムはそのまま渡す
                        targetUnit.carryItem.Add(exchangeItem);

                        Debug.Log($"{itemExchangeUnit.name}の{exchangeItem.ItemName}を{targetUnit.name}に渡しました");
                    }
                    
                }

                else
                {
                    //最初に選択したユニットのアイテムが空の場合
                    if (targetItem != null)
                    {
                        //→のユニットの装備解除処理
                        if (targetItem.isEquip == true)
                        {
                            targetItem.isEquip = false;
                            if (targetItem.ItemType == ItemType.WEAPON)
                            {
                                targetUnit.equipWeapon = null;
                            }
                            else if (targetItem.ItemType == ItemType.ACCESSORY)
                            {
                                targetUnit.equipAccessory = null;
                            }
                        }

                        //対象のアイテムが存在
                        //対象のアイテムは普通にもらう
                        itemExchangeUnit.carryItem.Add(targetItem);

                        //対象のアイテムは削除される
                        targetUnit.carryItem.RemoveAt(index);
                        Debug.Log($"{targetUnit.name}の{targetItem.ItemName}を{itemExchangeUnit.name}に渡しました");
                    }
                    else
                    {
                        //互いのアイテムが存在しない場合は何も発生しない
                        Debug.Log($"互いのアイテムが存在しません");

                    }
                }

                //UI更新する
                //左のユニットのアイテム 
                
                itemExchangeWindow.DeleteUnitItem();
                itemExchangeWindow.DeleteTargetItem();
                itemExchangeWindow.ReloadUnitItem(itemExchangeUnit, exchangeItemIndex, this);
                itemExchangeWindow.ReloadTargetItem(targetUnit, exchangeItem, index, this);
                //フォーカスを変更 普通に左のユニットの一番上で良いか
                itemExchangeWindow.FocusItemButton(exchangeItemIndex, true);
            }
            else
            {
                //□←□のパターン
                //アイテム交換実施
                if (exchangeItem != null)
                {
                    //←のユニットの装備解除処理
                    if (exchangeItem.isEquip == true)
                    {
                        //装備済みフラグをfalseにする
                        exchangeItem.isEquip = false;
                        if (exchangeItem.ItemType == ItemType.WEAPON)
                        {
                            itemExchangeUnit.equipWeapon = null;
                        }
                        else if (exchangeItem.ItemType == ItemType.ACCESSORY)
                        {
                            itemExchangeUnit.equipAccessory = null;
                        }
                    }

                    //互いのアイテムが存在
                    if (targetItem != null)
                    {
                        //→のユニットの装備解除処理
                        if (targetItem.isEquip == true)
                        {
                            targetItem.isEquip = false;
                            if (targetItem.ItemType == ItemType.WEAPON)
                            {
                                targetUnit.equipWeapon = null;
                            }
                            else if (targetItem.ItemType == ItemType.ACCESSORY)
                            {
                                targetUnit.equipAccessory = null;
                            }
                        }

                        itemExchangeUnit.carryItem[exchangeItemIndex] = targetItem;
                        targetUnit.carryItem[index] = exchangeItem;
                        Debug.Log($"{itemExchangeUnit.name}の{exchangeItem.ItemName}を{targetUnit.name}に渡しました");
                        Debug.Log($"{targetUnit.name}の{targetItem.ItemName}を{itemExchangeUnit.name}に渡しました");
                    }
                    else
                    {
                        //相手のアイテムが存在しない場合は自分のアイテム削除
                        itemExchangeUnit.carryItem.RemoveAt(exchangeItemIndex);

                        //相手へのアイテムはそのまま渡す
                        targetUnit.carryItem.Add(exchangeItem);

                        Debug.Log($"{itemExchangeUnit.name}の{exchangeItem.ItemName}を{targetUnit.name}に渡しました");
                    }

                }

                else
                {
                    //最初に選択したユニットのアイテムが空の場合、かつ交換するアイテムがnullではない場合
                    if (targetItem != null)
                    {
                        //→のユニットの装備解除処理
                        if (targetItem.isEquip == true)
                        {
                            targetItem.isEquip = false;
                            if (targetItem.ItemType == ItemType.WEAPON)
                            {
                                targetUnit.equipWeapon = null;
                            }
                            else if (targetItem.ItemType == ItemType.ACCESSORY)
                            {
                                targetUnit.equipAccessory = null;
                            }
                        }

                        //対象のアイテムが存在
                        //→のユニットはアイテムを普通にもらう
                        itemExchangeUnit.carryItem.Add(targetItem);

                        //←のアイテムは削除される
                        targetUnit.carryItem.RemoveAt(index);
                        Debug.Log($"{targetUnit.name}の{targetItem.ItemName}を{itemExchangeUnit.name}に渡しました");
                    }
                    else
                    {
                        //互いのアイテムが存在しない場合は何も発生しない

                    }
                }
                itemExchangeWindow.DeleteUnitItem();
                itemExchangeWindow.DeleteTargetItem();

                //引数を左右逆にする
                //左のユニットのアイテム 2番目に左のユニットを選択したので第一引数は自分自身を指定するのでtarget
                //第二引数は最初に選択したexchangeItemが右のユニットの物なのでexchangeItem
                itemExchangeWindow.ReloadUnitItem(targetUnit, index, this);

                //右のユニットのアイテム 1番目に右のユニットを選択したので第一引数は自分自身を指定するのでitemExchangeUnit
                itemExchangeWindow.ReloadTargetItem(itemExchangeUnit, targetItem, exchangeItemIndex, this);
                //フォーカスを変更 普通に左のユニットの一番上で良いか
                itemExchangeWindow.FocusItemButton(exchangeItemIndex, false);
            }

        }
        else
        {
            //ウィンドウ左側のユニット同士、右のユニット内のアイテムを並べ替えている場合
            
            //アイテム交換実施
                
            if(exchangeItem != null)
            {
                //1回目、2回目のアイテムが存在する場合
                if (targetItem != null)
                {
                    itemExchangeUnit.carryItem[exchangeItemIndex] = targetItem;
                    itemExchangeUnit.carryItem[index] = exchangeItem;
                    Debug.Log($"{itemExchangeUnit.name}の{exchangeItem.ItemName}を{targetItem.ItemName}と並べ替えました");
                }
                else
                {
                    //1回目選択のアイテムが存在し、2回目は存在しない場合
                    itemExchangeUnit.carryItem.Remove(exchangeItem);
                    itemExchangeUnit.carryItem.Add(exchangeItem);
                    Debug.Log($"{itemExchangeUnit.name}の{exchangeItem.ItemName}を末尾に並べ替えました");
                }
            }
            else
            {
                //最初に選択したアイテムが空の場合
                if (targetItem != null)
                {
                    //2回目に選択したアイテムが存在した場合は、2回目に選択したアイテムを一旦外して並べ替える
                    itemExchangeUnit.carryItem.Remove(targetItem);
                    itemExchangeUnit.carryItem.Add(targetItem);
                    Debug.Log($"{itemExchangeUnit.name}の{targetItem.ItemName}を末尾に並べ替えました");
                }
                else
                {
                    //空白同士を選択したので何も起こらないパターン
                    Debug.Log($"互いのアイテムが存在しません");
                }

                itemExchangeWindow.FocusItemButton(exchangeItemIndex, true);
            }


            if (itemControllUnit == itemExchangeUnit)
            {
                itemExchangeWindow.DeleteUnitItem();

                //ボタン更新 indexと対応する中身を入れ替え
                itemExchangeWindow.ReloadUnitItem(targetUnit, index, this);
            }
            else
            {
                itemExchangeWindow.DeleteTargetItem();
                itemExchangeWindow.ReloadTargetItem(targetUnit, targetItem, exchangeItemIndex, this);
            }

                itemExchangeWindow.FocusItemButton(exchangeItemIndex, false);

        }

        

        //モード変更 再度アイテム交換へ戻す
        menuMode = MenuMode.ITEM_EXCHANGE;
        Debug.Log($"menuMode:{menuMode}");

    }

    //アイテム交換 ここまで



    /// <summary>
    /// 倉庫関連
    /// </summary>
    //210222「持ち物」ボタンを押された時 倉庫(スキマ)を表示して倉庫に入れたり、装備する処理を開始
    public void ControlCarryItem()
    {
        menuMode = MenuMode.ITEM;
        Debug.Log($"menuMode:{menuMode}");

        GameObject weaponWindow = getWindow("WeaponWindow");
        cullentWindow = weaponWindow;

        //詳細ウィンドウ表示
        detailView.SetActive(true);

        foreach (Transform weaponButton in weaponWindow.transform)
        {
            //ボタンを活性化する
            weaponButton.GetComponent<Button>().interactable = true;
        }
        //フォーカス変更
        EventSystem.current.SetSelectedGameObject(weaponWindow.transform.Find("StatusItemButton0").gameObject);


        //「持ち物」「交換」「全預け」等のウィンドウとユニット一覧を消す
        GameObject itemControllWindow = getWindow("ItemControlWindow");
        itemControllWindow.SetActive(false);
        GameObject partyWindow = getWindow("PartyWindow");
        partyWindow.SetActive(false);

        //インベントリ内のアイテム更新
        ReloadInventory();

        inventoryWindow.SetActive(true);

    }

    /// <summary>
    /// アイテムを預けたり、装備するウィンドウを開く
    /// </summary>
    public void OpenItemDepositWindow(Unit unit,Item item, Transform itemButtonTransform, int index, bool isEquipable)
    {
        //預けるアイテムをこのクラスに一旦保管
        itemControllUnit = unit;

        //一旦、全てのボタンを非表示にして使うボタンのみ出していく
        //「使う」ボタンは非表示
        GameObject itemUseButton = itemDepositWindow.transform.Find("ItemUseButton").gameObject;
        itemUseButton.SetActive(false);

        GameObject itemEquipButton = itemDepositWindow.transform.Find("ItemEquipButton").gameObject;
        itemEquipButton.SetActive(false);

        //「受け取る」ボタン
        GameObject itemReceiveButton = itemDepositWindow.transform.Find("ItemReceiveButton").gameObject;
        itemReceiveButton.SetActive(false);

        //預けるボタン
        GameObject itemDepositButton = itemDepositWindow.transform.Find("ItemDepositButton").gameObject;
        itemDepositButton.SetActive(false);

        //「預ける」等のコマンドウィンドウを表示する
        itemDepositWindow.transform.position = new Vector3(itemButtonTransform.position.x + ITEM_CONTROL_WINDOW_DISTANCE - 10, itemButtonTransform.position.y - 45, itemButtonTransform.position.z);
        itemDepositWindow.SetActive(true);

        //キャンセル時にフォーカスを戻すボタンを記憶
        depositItemButton = itemButtonTransform.gameObject;
        depositItemButtonIndex = index;

        if (item != null)
        {
            //空でないボタンを押した場合
            this.depositItem = item;
            itemDepositButton.SetActive(true);

            //210224 ステータス上昇アイテムのみ「使うコマンドを表示」
            if (item.ItemType == ItemType.POTION)
            {
                if (item.potion.potionType == PotionType.STATUSUP)
                {
                    itemUseButton.SetActive(true);
                }
            }

            else if (item.ItemType == ItemType.WEAPON && isEquipable)
            {
                //武器の場合、装備不可能な武器を選択した場合は装備コマンド自体表示しない
                itemEquipButton.SetActive(true);
            }
            else if (item.ItemType == ItemType.ACCESSORY)
            {
                //アクセサリは今の所、誰でも装備出来る
                itemEquipButton.SetActive(true);
            }
                //預けるボタンにフォーカス
                EventSystem.current.SetSelectedGameObject(itemDepositWindow.transform.Find("ItemDepositButton").gameObject);
        }
        else
        {
            //空のボタンを押した場合
            itemReceiveButton.SetActive(true);

            //受け取るボタンにフォーカス
            EventSystem.current.SetSelectedGameObject(itemDepositWindow.transform.Find("ItemReceiveButton").gameObject);
            
        }

        //手持ちアイテムを不活性化
        GameObject weaponWindow = getWindow("WeaponWindow");
        foreach (Transform weaponButton in weaponWindow.transform)
        {
            weaponButton.GetComponent<Button>().interactable = false;
        }

        menuMode = MenuMode.ITEM_DEPOSIT_MENU;
        Debug.Log($"menuMode:{menuMode}");

        
        cullentWindow = itemDepositWindow;
    }

    /// <summary>
    /// アイテムを預ける
    /// </summary>
    /// <param name="item"></param>
    public void DepositItem()
    {
        //装備品を預けた場合はユニットの装備する武器やアクセサリを消す
        if(depositItem.isEquip == true)
        {
            if(depositItem.ItemType == ItemType.WEAPON)
            {
                itemControllUnit.equipWeapon = null;
            }
            else if (depositItem.ItemType == ItemType.ACCESSORY)
            {
                itemControllUnit.equipAccessory = null;
            }
        }
        //ユニットの手持ちから消してインベントリへ入れる
        itemControllUnit.carryItem.Remove(depositItem);
        itemInventory.AddItem(depositItem);
        Debug.Log($"{depositItem.ItemName}を預けました");

        //更新
        ChangeUnitItemWindow(itemControllUnit.name);

        //「預ける」ウィンドウを消す
        itemDepositWindow.SetActive(false);

        //モードをアイテム一覧操作に変更
        menuMode = MenuMode.ITEM;
        Debug.Log($"menuMode:{menuMode}");

        cullentWindow = getWindow("WeaponWindow");

        //フォーカス変更
        GameObject weaponWindow = getWindow("WeaponWindow");
        foreach (Transform weaponButton in weaponWindow.transform)
        {
            //ボタンを活性化する
            weaponButton.GetComponent<Button>().interactable = true;
        }

        //インベントリ内のアイテム更新
        ReloadInventory();

        EventSystem.current.SetSelectedGameObject(weaponWindow.transform.Find("StatusItemButton0").gameObject);
    }

    /// <summary>
    /// 210224 全預け実行
    /// </summary>
    public void DepositAllItem()
    {
        //装備品を消して、アイテムの装備フラグも消す処理
        itemControllUnit.equipAccessory = null;
        itemControllUnit.equipWeapon = null;

        foreach(Item item in itemControllUnit.carryItem)
        {
            item.isEquip = false;
        }

        //全てのアイテムを入れる
        ItemInventory.itemList.AddRange(itemControllUnit.carryItem);
        //ユニットの全てのアイテムを消す
        itemControllUnit.carryItem.Clear();

        //アイテム更新
        ChangeUnitItemWindow(itemControllUnit.name);

        //「全預け」等が表示されたウィンドウを閉じる
        getWindow("ItemControlWindow").SetActive(false);

        //モードをアイテムを操作するユニット一覧に変更
        menuMode = MenuMode.ITEM_UNIT_SELECT;
        Debug.Log($"menuMode:{menuMode}");

        cullentWindow = getWindow("PartyWindow");

        EventSystem.current.SetSelectedGameObject(nestedReturnButton);

    }

    /// <summary>
    /// 210224 アイテムを「受け取る」ボタンを押した時
    /// </summary>
    public void PrepareReceiveItem()
    {
        GameObject weaponWindow = getWindow("WeaponWindow");

        //もし隙間が空の場合は元のアイテム選択へ戻る
        if (ItemInventory.itemList.Count == 0)
        {
            menuMode = MenuMode.ITEM;
            //「受け取る」ウィンドウを消す
            itemDepositWindow.SetActive(false);
            cullentWindow = weaponWindow;

            //ボタン活性化
            foreach (Transform weaponButton in weaponWindow.transform)
            {
                weaponButton.GetComponent<Button>().interactable = true;
            }

            //元のボタンへフォーカス
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(depositItemButton);
            return;
        }

        menuMode = MenuMode.RECEIVE_ITEM;
        Debug.Log($"menuMode:{menuMode}");

        //「受け取る」ウィンドウを消す
        itemDepositWindow.SetActive(false);

        

        //ユニットのアイテムを不活性化する
        foreach (Transform weaponButton in weaponWindow.transform)
        {
            weaponButton.GetComponent<Button>().interactable = false;
        }

        //フォーカス変更
        foreach (Transform weaponButton in inventoryWindow.transform)
        {
            //ボタンを活性化する
            weaponButton.GetComponent<Button>().interactable = true;
        }

        //隙間の最初の要素を選択
        EventSystem.current.SetSelectedGameObject(inventoryWindow.transform.Find("StatusItemButton").gameObject);
    }

    /// <summary>
    /// 隙間からアイテム取り出し実行
    /// </summary>
    /// <param name="item"></param>
    public void ReceiveItem(Item item)
    {
        //隙間からアイテム削除
        ItemInventory.itemList.Remove(item);
        itemControllUnit.carryItem.Add(item);

        menuMode = MenuMode.ITEM;
        Debug.Log($"menuMode:{menuMode}");

        //インベントリ内のアイテム更新
        ReloadInventory();

        //持ち物一覧更新
        ChangeUnitItemWindow(itemControllUnit.name);

        //ボタン活性化
        GameObject weaponWindow = getWindow("WeaponWindow");
        cullentWindow = weaponWindow;
        foreach (Transform weaponButton in weaponWindow.transform)
        {
            //ボタンを活性化する
            weaponButton.GetComponent<Button>().interactable = true;
        }

        //フォーカスを選択したボタンへ戻す
        EventSystem.current.SetSelectedGameObject(weaponWindow.transform.Find("StatusItemButton" + depositItemButtonIndex).gameObject);

    }

    //倉庫内のアイテム更新
    private void ReloadInventory()
    {
        //インベントリのアイテム一覧削除
        foreach (Transform weaponButton in inventoryWindow.transform)
        {
            //ボタンを削除する
            Destroy(weaponButton.gameObject);
        }

        //預けているアイテム一覧を表示する
        foreach (Item item in ItemInventory.itemList)
        {

            //Resources配下からボタンをロード
            var weaponButton = (Instantiate(Resources.Load("Prefabs/StatusItemButton")) as GameObject).transform;

            //交換などではないのでunit、indexの引数は無くて良い？
            weaponButton.GetComponent<StatusItemButton>().Init(item, this, null, 0);
            weaponButton.name = weaponButton.name.Replace("(Clone)", "");

            //inventoryWindowオブジェクト配下にprefab作成
            weaponButton.transform.SetParent(inventoryWindow.transform,false);

            //ボタンを不活性にしてフォーカスを移動しないようにする
            weaponButton.GetComponent<Button>().interactable = false;
        }
    }

    //倉庫 ここまで



    /// <summary>
    /// 武器、アクセサリの装備を行う 「装備」ボタン押下時に呼ばれる
    /// </summary>
    public void EquipItem()
    {
        if(depositItem.ItemType == ItemType.WEAPON)
        {
            itemControllUnit.equipWeapon = depositItem.weapon;

            //一旦、全ての武器の装備フラグを切ってしまう
            foreach(Item item in itemControllUnit.carryItem)
            {
                if(item.ItemType == ItemType.WEAPON)
                {
                    item.isEquip = false;
                }
            }
        }
        else if (depositItem.ItemType == ItemType.ACCESSORY)
        {
            itemControllUnit.equipAccessory = depositItem.accessory;
            foreach (Item item in itemControllUnit.carryItem)
            {
                if (item.ItemType == ItemType.ACCESSORY)
                {
                    item.isEquip = false;
                }
            }
        }

        //装備フラグを立てる
        depositItem.isEquip = true;

        //装備フラグが変わるので持ち物一覧更新
        ChangeUnitItemWindow(itemControllUnit.name);

        //「装備」ウィンドウを消す
        itemDepositWindow.SetActive(false);

        //アイテムを操作するモードへ戻る
        menuMode = MenuMode.ITEM;
        Debug.Log($"menuMode:{menuMode}");

        //ボタン活性化
        GameObject weaponWindow = getWindow("WeaponWindow");
        cullentWindow = weaponWindow;
        foreach (Transform weaponButton in weaponWindow.transform)
        {
            //ボタンを活性化する
            weaponButton.GetComponent<Button>().interactable = true;
        }

        //フォーカスを選択したボタンへ戻す
        EventSystem.current.SetSelectedGameObject(weaponWindow.transform.Find("StatusItemButton" + depositItemButtonIndex).gameObject);
    }

    /// <summary>
    /// 210304 「使う」ボタンを押した時
    /// </summary>
    public void UseItem()
    {
        //210304 これ、分岐作ればクラスチェンジアイテムの使用にも使える？
        //アイテム使用
        string text = useItemManager.StatusUsePotion(depositItem.potion, itemControllUnit);
        useItemManager.EmptyPotionDelete(itemControllUnit.carryItem);

        OpenMessageWindow(text);

        //使用したアイテムは消えるので持ち物一覧更新
        ChangeUnitItemWindow(itemControllUnit.name);

        //HPが変わったりするのでユニットの情報更新
        changeUnitOutlineWindow(itemControllUnit.name);

        //モードを戻す
        menuMode = MenuMode.ITEM;
        Debug.Log($"menuMode:{menuMode}");

        //「装備」「使う」ウィンドウを消す
        itemDepositWindow.SetActive(false);

        //フォーカスを選択したボタンへ戻す
    }

     //アイテム詳細ウィンドウの更新を実施
    public void changeItemDetailWindow(Item item)
    {
        //空のアイテムボタン選択時
        if(item == null)
        {
            weaponDetailWindow.SetActive(false);
            itemDetailWindow.SetActive(false);
            return;
        }

        //武器とそれ以外では情報量が違うのでウィンドウ切り替え
        if(item.ItemType == ItemType.WEAPON){
            weaponDetailWindow.SetActive(true);
            itemDetailWindow.SetActive(false);
            Weapon weapon = weaponDatabase.FindByName(item.ItemName);
            weaponDetailWindow.GetComponent<DetailWindow>().UpdateBattleWeaponText(weapon);
        }
        else if(item.ItemType == ItemType.POTION)
        {
            weaponDetailWindow.SetActive(false);
            itemDetailWindow.SetActive(true);

            Potion potion = potionDatabase.FindByName(item.ItemName);

            itemDetailWindow.GetComponent<ItemDetailWindow>().UpdateText(potion);
        }
        else if (item.ItemType == ItemType.ACCESSORY)
        {
            weaponDetailWindow.SetActive(false);
            itemDetailWindow.SetActive(true);

            Accessory accessory = accessoryDatabase.FindByName(item.ItemName);

            itemDetailWindow.GetComponent<ItemDetailWindow>().UpdateText(accessory);
        }
        else if (item.ItemType == ItemType.TOOL)
        {
            weaponDetailWindow.SetActive(false);
            itemDetailWindow.SetActive(true);
            Tool tool = toolDatabase.FindByName(item.ItemName);

            itemDetailWindow.GetComponent<ItemDetailWindow>().UpdateText(tool);
        }
    }

    //ステータス画面の詳細ウィンドウ更新
    public void changeStatusItemDetailWindow(Item item, Transform transform)
    {
        //フォーカスした要素の近くに移動させる
        statusDetailView.transform.position = transform.position;

        statusSkillDetailWindow.SetActive(false);

        //空のアイテムボタン選択時
        if (item == null)
        {
            statusWeaponDetailWindow.SetActive(false);
            statusItemDetailWindow.SetActive(false);
            return;
        }

        //武器とそれ以外では情報量が違うのでウィンドウ切り替え
        if (item.ItemType == ItemType.WEAPON)
        {
            statusWeaponDetailWindow.SetActive(true);
            statusItemDetailWindow.SetActive(false);
            Weapon weapon = weaponDatabase.FindByName(item.ItemName);
            statusWeaponDetailWindow.GetComponent<DetailWindow>().UpdateBattleWeaponText(weapon);
        }
        else if (item.ItemType == ItemType.POTION)
        {
            statusWeaponDetailWindow.SetActive(false);
            statusItemDetailWindow.SetActive(true);

            Potion potion = potionDatabase.FindByName(item.ItemName);

            statusItemDetailWindow.GetComponent<ItemDetailWindow>().UpdateText(potion);
        }
        else if (item.ItemType == ItemType.ACCESSORY)
        {
            statusWeaponDetailWindow.SetActive(false);
            statusItemDetailWindow.SetActive(true);

            Accessory accessory = accessoryDatabase.FindByName(item.ItemName);

            statusItemDetailWindow.GetComponent<ItemDetailWindow>().UpdateText(accessory);
        }
        else if (item.ItemType == ItemType.TOOL)
        {
            statusWeaponDetailWindow.SetActive(false);
            statusItemDetailWindow.SetActive(true);
            Tool tool = toolDatabase.FindByName(item.ItemName);

            statusItemDetailWindow.GetComponent<ItemDetailWindow>().UpdateText(tool);
        }
    }

    //ステータス画面でスキルボタンを選択された場合の処理
    public void chageStatusSkillDetailWindow(Skill skill, Transform transform)
    {
        statusWeaponDetailWindow.SetActive(false);
        statusItemDetailWindow.SetActive(false);

        if (skill == Skill.NONE)
        {
            statusSkillDetailWindow.SetActive(false);
            return;
        }

        statusDetailView.transform.position = new Vector3(transform.position.x - 35, transform.position.y, transform.position.z);

        statusSkillDetailWindow.SetActive(true);

        statusSkillDetailWindow.GetComponent<SkillDetailWindow>().UpdateText(skill);
    }

    /// <summary>
    /// 支援会話関連
    /// </summary>

    //支援会話の為の仲間一覧ウィンドウを表示
    public void OpenTalkPartyWindow()
    {
        //210207 元のボタンに戻る
        returnButton = GameObject.Find("TalkButton");
        menuMode = MenuMode.TALK;
        OpenWindow("PartyWindow");
    }

    //支援会話リストを表示する
    public void OpenTalkListWindow(string unitName)
    {
        //talkManagerのメソッドで支援会話リストを初期化
        talkManager.initTalkListWindow(unitName);
        //表示
        OpenWindow("TalkListWindow");
        var window = getWindow("TalkListWindow");

        //ここクソ実装過ぎるしなんとかしたい
        if("霊夢" == unitName)
        {
            EventSystem.current.SetSelectedGameObject(window.transform.Find("marisaPanel/FriendLevelList/reimu_marisa_C").gameObject);
        }
        else if("魔理沙" == unitName)
        {
            EventSystem.current.SetSelectedGameObject(window.transform.Find("reimuPanel/FriendLevelList/marisa_reimu_C").gameObject);
        }
        else if ("ルーミア" == unitName)
        {
            EventSystem.current.SetSelectedGameObject(window.transform.Find("daiPanel/FriendLevelList/rumia_dai_C").gameObject);
        }
        else if ("大妖精" == unitName)
        {
            EventSystem.current.SetSelectedGameObject(window.transform.Find("rumiaPanel/FriendLevelList/dai_rumia_C").gameObject);
        }
        else if ("チルノ" == unitName)
        {
            EventSystem.current.SetSelectedGameObject(window.transform.Find("daiPanel/FriendLevelList/cirno_dai_C").gameObject);
        }
        else if ("文" == unitName)
        {
            EventSystem.current.SetSelectedGameObject(window.transform.Find("reimuPanel/FriendLevelList/aya_reimu_C").gameObject);
        }
        else if ("鈴仙" == unitName)
        {
            EventSystem.current.SetSelectedGameObject(window.transform.Find("reimuPanel/FriendLevelList/udon_reimu_C").gameObject);
        }

    }

    //200711 セーブウィンドウを開く
    public void OpenSaveWindow()
    {

        returnButton = GameObject.Find("SaveButton");
        menuMode = MenuMode.SAVE;
        saveAndLoadManager.mode = FileControlMode.SAVE;
        OpenWindow("SaveAndLoadWindow");
    }

    //200711 ロードウィンドウを開く
    public void OpenLoadWindow()
    {
        returnButton = GameObject.Find("LoadButton");
        menuMode = MenuMode.LOAD;
        saveAndLoadManager.mode = FileControlMode.LOAD;
        OpenWindow("SaveAndLoadWindow");
    }

    /// <summary>
    /// 210221 チュートリアルウィンドウを開く
    /// </summary>
    public void OpenTutorialWindow()
    {
        //モード切替
        menuMode = MenuMode.TUTORIAL_CATEGORY_SELECT;

        //キャンセルボタンを押した時の戻り先を記憶
        returnButton = GameObject.Find("TutorialButton");

        //ウィンドウを開く処理、他を閉じる処理、フォーカスを変更する処理全部やってくれる
        OpenWindow("TutorialWindow");
    }

    //210221 チュートリアル用のユニット一覧を表示する
    public void OpenUnitTutorialListWindow()
    {
        //モード切替
        menuMode = MenuMode.TUTORIAL_SELECT;
        Debug.Log($"menuMode:{menuMode}");

        //ネストした戻り先を「ユニット性能」ボタンに設定する
        nestedReturnButton = GameObject.Find("UnitTutorialButton");

        //cullentWindow変更 変な実装だけど仕方が無い
        OpenWindow("UnitTutorialListWindow");

        //UnitTutorialListWindowを探して表示
        GameObject unitTutorialListWindow = getWindow("UnitTutorialListWindow");
        

        foreach (var unit in UnitController.unitList)
        {
            //Resources配下からボタンをロード
            var itemButton = (Instantiate(Resources.Load("Prefabs/UnitButton")) as GameObject).transform;
            //ボタン初期化 210221 遂にユニットの画像をボタンに表示するよう改修
            itemButton.GetComponent<UnitButton>().Init(unit.name, this, unit.pathName);
            itemButton.name = itemButton.name.Replace("(Clone)", "");

            //partyWindowオブジェクト配下にprefab作成
            itemButton.transform.SetParent(unitTutorialListWindow.transform);
        }

        //フォーカス変更 最初のUnitButtonを取得
        EventSystem.current.SetSelectedGameObject(unitTutorialListWindow.transform.Find("UnitButton").gameObject);
    }

    //210221 ユニットボタンを押すと呼ばれ、ウィンドウ表示と表示前処理を行う
    public void OpenUnitTutorialWindow(string unitName)
    {
        Unit unit = unitController.findByName(unitName);

        //ウィンドウ設定
        UnitTutorialWindow unitTutorialWindow = getWindow("UnitTutorialWindow").GetComponent<UnitTutorialWindow>();
        unitTutorialWindow.UpdateText(unit);

        //menumode更新
        menuMode = MenuMode.TUTORIAL;

        //ウィンドウを開き、cullentWindow更新
        OpenWindow("UnitTutorialWindow");
    }

    //UnitButtonから呼ばれる レベルアップ処理
    public void LvUp(string lvUpUnitName)
    {
        //本クラスにレベルアップしたキャラを保持
        lvupUnit = unitController.findByName(lvUpUnitName);

        //ウィンドウにユニットの情報を表示
        lvUpManager.InitLvupWindow(lvupUnit);

        //第二引数はレベルアップ後の経験値
        lvUpList = lvUpManager.lvup(lvUpUnitName, 0);

        //テスト用にメニュー画面でレベルアップの演出を行う
        menuMode = MenuMode.LVUP_EFFECT;
    }

    //200712 ユニットの体力を回復させる
    public void Rest()
    {
        unitController.rest();
        Debug.Log("ユニット全員の体力を回復しました");
    }

    /// <summary>
    /// 引数のシーンに遷移する　
    /// 210207 これ、支援会話シーンの遷移なので、消した方が良いかも
    /// </summary>
    /// <param name="scene"></param>
    public void ChangeScene(string scene)
    {
        //210207 暗転中はウィンドウを非表示にする
        foreach (var item in windowLists)
        {
            item.SetActive(false);
        }

        //シーン変更
        fadeInOutManager.ChangeScene(scene);
    }

    //キャンセルボタンを押した時などに支援会話パネルを削除する
    private void DeleteFriendPanel()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("FriendPanel");
        foreach (var obj in g)
        {
            Destroy(obj);
        }
    }

    //210207 シーン変更の効果音を鳴らす
    public void PlayChangeSE()
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
        
    }

}

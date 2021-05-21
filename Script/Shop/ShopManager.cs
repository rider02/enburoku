using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{

    //現在表示しているウィンドウ
    GameObject currentWindow;
    [SerializeField]
    CashManager cashManager;
    [SerializeField]
    WeaponController weaponController;

    //武器一覧のアセットファイル
    [SerializeField]
    WeaponDatabase weaponDatabase;
    [SerializeField]
    GameObject buyWindow;
    [SerializeField]
    GameObject detailWindow;
    [SerializeField]
    GameObject confirmWindow;
    [SerializeField]
    ItemInventory itemInventory;
    [SerializeField]
    FadeInOutManager fadeInOutManager;


    //　お店のウィンドウ全部をSerializeFieldに入れていく
    //cashWindowは除外＾＾；
    [SerializeField]
    private GameObject[] windowLists;

    //　自身の親のCanvasGroup
    private CanvasGroup canvasGroup;

    public ShopMode shopMode { get; set; }

    void Start()
    {
        currentWindow = GameObject.Find("MenuWindow");

        canvasGroup = GetComponentInParent<CanvasGroup>();

        //お金を反映
        if (!CashManager.isCashInit)
        {
            //お金を初期化
            cashManager.Init();
        }
        cashManager.UpdateText();

        //初期化されていなければ手持ちアイテムリスト初期化
        if (!ItemInventory.isInventoryInit)
        {
            itemInventory.Init();
        }

        //リスト作成
        weaponController.initWeaponList(this, weaponDatabase, detailWindow.GetComponent<DetailWindow>());

        //消しておく Unityのチェックボックスはenabled？
        detailWindow.SetActive(false);

        //初期化 クラスのインスタンスを渡すのみ
        confirmWindow.GetComponent<ConfirmWindow>().init(this);
    }

    // Update is called once per frame
    void Update()
    {
        //プレー時間更新
        PlayTimeManager.TimeUpdate();

        //キャンセルボタンが押されたらステータス画面へ戻る
        if (Input.GetButtonDown("Cancel"))
        {
            if (currentWindow.name == "BuyWindow")
            {
                foreach (var item in windowLists)
                {
                    if (item.name == "MenuWindow")
                    {
                        //詳細ウィンドウ消す
                        detailWindow.SetActive(false);

                        Debug.Log("window active" + item.name);
                        item.SetActive(true);
                        currentWindow = item;
                        EventSystem.current.SetSelectedGameObject(null);
                        //　ウインドウのMenuAreaの最初の子要素をアクティブな状態にする
                        EventSystem.current.SetSelectedGameObject(currentWindow.transform.Find("BuyButton").gameObject);
                    }
                    else
                    {
                        item.SetActive(false);
                    }
                }
                
                
            }
            else if (currentWindow.name == "ConfirmWindow")
            {

                //購入確認ウィンドウの時
                CloseConfirmWindow();
            }
            else
            {
                //WeaponWindow表示中以外に戻るボタンを押したらStatus画面へ
                ReturnToStatus();
            }
        }
    }

    /// <summary>
    /// 引数のシーンに遷移する
    /// </summary>
    /// <param name="scene"></param>
    public void ChangeScene(string scene)
    {
        //シーン変更
        SceneManager.LoadScene(scene);
    }

    //アイテムを買う処理へ遷移 「買う」ボタンを押した時
    public void OpenBuyWindow()
    {
        //モード変更
        shopMode = ShopMode.BUY;

        //ウィンドウを開く
        detailWindow.SetActive(true);
        OpenWindow("BuyWindow");
        


    }

    //アイテムを売る処理へ遷移 「売るボタンを押した時」
    public void OpenSaleWindow()
    {
        //モード変更
        shopMode = ShopMode.SALE;

        initSaleWindow();

        //売却時はボタンを押されたタイミングでPrefubをロードする
        detailWindow.SetActive(true);
        //ウィンドウを開く
        OpenWindow("SaleWindow");

    }

    /// <summary>
    /// 手持ちのアイテムから売却ウィンドウを初期化する
    /// </summary>
    public void initSaleWindow()
    {
        var weaponList = itemInventory.CreateWeaponList();
        weaponController.initSaleWeaponList(this, weaponList, detailWindow.GetComponent<DetailWindow>());
    }

    //ウィンドウ名から対象ウィンドウを開く
    public void OpenWindow(string windowName)
    {
        var window = getWindow(windowName);
        WindowOnOff(window);
    }

    //ウィンドウ名から対象のgameObjectを返す
    private GameObject getWindow(string windowName)
    {
        foreach (var window in windowLists)
        {
            if (window.name == windowName)
                return window;
        }
        return null;
    }

    //　他の画面を表示する
    public void WindowOnOff(GameObject window)
    {
        if (canvasGroup == null || canvasGroup.interactable)
        {
            ChangeWindow(window);
        }
    }

    //　ステータス画面のウインドウのオン・オフメソッド
    public void ChangeWindow(GameObject window)
    {
        foreach (var item in windowLists)
        {
            if (item == window)
            {
                Debug.Log("window active" + window.name);
                item.SetActive(true);
                currentWindow = item;
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                item.SetActive(false);
            }

            if (window.name == "BuyWindow")
            {

                //　それぞれのウインドウのMenuAreaの最初の子要素をアクティブな状態にする
                EventSystem.current.SetSelectedGameObject(window.transform.Find("Scroll View/Viewport/Content/WeaponButton").gameObject);
            }
        }
    }

    /// <summary>
    /// 確認ウィンドウを開く
    /// </summary>
    /// <param name="weapon"></param>
    public void OpenConfirmWindow(Weapon weapon)
    {
        //テキスト更新
        confirmWindow.GetComponent<ConfirmWindow>().UpdateText(weapon);
        //ウィンドウ表示
        confirmWindow.SetActive(true);
        currentWindow = confirmWindow;
        //ボタン選択
        EventSystem.current.SetSelectedGameObject(confirmWindow.transform.Find("ButtonLayout/BuyButton").gameObject);
    }

    /// <summary>
    /// 確認ウィンドウを閉じる
    /// </summary>
    public void CloseConfirmWindow()
    {

        //ウィンドウ表示
        confirmWindow.SetActive(false);
        currentWindow = buyWindow;
        EventSystem.current.SetSelectedGameObject(null);
    //最初の子要素をアクティブな状態にする
    EventSystem.current.SetSelectedGameObject(buyWindow.transform.Find("Scroll View/Viewport/Content/WeaponButton").gameObject);

    }

    /// <summary>
    /// まだ武器を購入した時のみ
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="price"></param>
    public void Buy(string itemName,int price)
    {
        //お金の変更
        cashManager.Buy(price);

        //WeaponControllerからアイテムをテキストで検索して取得
        Weapon weapon = weaponDatabase.FindByName(itemName);

        if (weapon != null)
        {
            //武器インスタンスからアイテム化
            Item purchasedWeapon = new Item(weapon);
            
            //インベントリにアイテム追加
            itemInventory.addItem(purchasedWeapon);

        }

    }

    public void ReturnToStatus()
    {
        fadeInOutManager.ChangeScene("Status");
    }

}

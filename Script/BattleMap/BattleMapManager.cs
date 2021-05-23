using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// 戦闘マップ制御用クラス
/// マップ処理はMain_Map、戦闘はBattleManager、敵ターンはEnemyAIManagerとやり取りして各種処理を行う
/// </summary>
public class BattleMapManager : MonoBehaviour
{

    [SerializeField] BattleMapCursor battleMapCursor;       //カーソル
    [SerializeField] GameObject mainCamera;                 //カメラ
    [SerializeField] Main_Map mainMap;                      //マップ
    [SerializeField] CellTypeView cellTypeView;             //セルの種類表示UI
    [SerializeField] UnitController unitController;         //ユニット一覧
    [SerializeField] Transform unitContainer;               //ユニットを入れるゲームオブジェクト
    [SerializeField] CoordinateWindow coordinateWindow;     //座標表示UI(エディタ用)
    [SerializeField] Transform enemyContainer;              //敵を入れるゲームオブジェクト
    [SerializeField] Transform treasureContainer;           //宝箱を入れるゲームオブジェクト
    [SerializeField] EditorModeView editorModeView;         //エディタUI
    [SerializeField] GameObject editMenuWindow;             //エディタメニュー
    [SerializeField] GameObject stageDetailWindow;          //戦闘前のステージ詳細UI
    [SerializeField] GameObject selectCellTypeWindow;       //エディタのセル表示UI
    [SerializeField] GameObject unitSelectView;             //戦闘前のユニット選択UI
    [SerializeField] Text inputCellText;                    //エディタの入力セル表示UI
    [SerializeField] UnitOutlineWindow unitOutlineWindow;   //ユニットにカーソルを合わせた時の表示UI
    [SerializeField] FadeInOutManager fadeInOutManager;     //フェードイン、フェードアウト処理クラス
    [SerializeField] GameObject preparationMenuWindow;      //戦闘準備メニューUI
    [SerializeField] GameObject preparationWindow;          //戦闘準備メニューUI
    [SerializeField] EntryCountWindow entryCountWindow;     //戦闘準備のユニット出撃数UI
    [SerializeField] GameObject mapView;
    [SerializeField] UnitOutlineWindow mapUnitOutlineWindow;//ユニットにカーソルを合わせた時の表示UI
    [SerializeField] UnitOutlineWindow mapEnemyOutlineWindow;   //敵にカーソルを合わせた時の表示UI
    [SerializeField] GameObject movedMenuWindow;            //移動後のメニューUI
    [SerializeField] BattleManager battleManager;           //戦闘処理クラス
    [SerializeField] GameObject weaponWindow;               //攻撃する武器表示UI
    [SerializeField] GameObject detailWindow;               //武器の詳細UI
    [SerializeField] GameObject itemDetailWindow;           //道具の詳細UI
    [SerializeField] GameObject weaponDetailWindow;         //武器の詳細UI
    [SerializeField] GameObject ItemUseEquipWindow;         //道具を選択した時、装備、使うコマンドを表示するUI

    [SerializeField] GameObject mapMenuView;                //マップメニュー(ターン終了等)UI
    [SerializeField] BattleConditionWindow battleConditionWindow;
    [SerializeField] EnemyAIManager enemyAIManager;         //敵AI制御クラス
    [SerializeField] GameObject battleConfirmWindow;        //戦闘開始確認UI
    [SerializeField] GameObject useItemConfirmWindow;       //道具使用確認UI
    [SerializeField] UseItemManager useItemManager;         //アイテム使用制御クラス
    [SerializeField] GameObject messageWindow;              //メッセージ(技能レベルアップ等)表示ウィンドウ
    [SerializeField] GameObject unitSelectButton;
    [SerializeField] ItemInventory itemInventory;
    [SerializeField] EXPWindow expWindow;
    [SerializeField] MapEditManager mapEditManager;
    [SerializeField] Light light;
    [SerializeField] AcquiredTreasureManager acquiredTreasureManager;

    //210519 会話用クラス
    [SerializeField] GameObject talkView;
    [SerializeField] GameObject talkWindow;
    BattleTalkManager battleTalkManager;

    //210219 ステータスウィンドウを移植
    [SerializeField] GameObject statusWindow;

    //ターン開始
    [SerializeField] TurnEffectManager turnEffectManager;

    //エフェクト
    [SerializeField] EffectManager effectManager;

    //効果音
    [SerializeField] AudioSource decideSE;
    [SerializeField] AudioSource battleSE;
    [SerializeField] AudioSource treasureSE;

    //210523 戦闘中デバッグ用メッセージを表示するか
    [SerializeField] bool battleTextVisible;

    //各種アイテムアセットファイル
    WeaponDatabase weaponDatabase;
    AccessoryDatabase accessoryDatabase;
    PotionDatabase potionDatabase;
    ToolDatabase toolDatabase;
    UnitDatabase unitDatabase;

    //210216 キャンセルボタンを押した時にフォーカスを戻す武器
    public GameObject selectedItem;

    //210302 フォーカスを戻すUI 武器を選択するとselectedItemが書き変わって戻れなくなるので、こちらに保存する
    public GameObject selectedMovedMenuItem;

    //セル
    Main_Cell cell;

    //200725 移動前に戻る為に座標を保存
    Main_Cell beforeCell;

    //宝箱が有る座標
    Coordinate treasureCoordinate;

    //地形の高さに合わせてカーソルを移動させる用
    Terrain terrain;

    //カメラの位置を調整　正の数ならプレイヤーの前に、負の数ならプレイヤーの後ろに配置する
    float yAdjust = 4;
    float zAdjust = -4;
    int yAdjustTarget;
    int zAdjustTarget;
    bool isCameraAdjusting = false;

    //200725 カメラの前後の位置を調整する速さ
    float adjustSpeed = 0.1f;

    //カーソルの動く速さと、フレームによる速さ固定
    float cursorMoveSpeed = 0.03f;
    private const float CURSOR_MOVE_WAIT = 0.005f;

    //カーソルの高さがTerrainと同一だと埋まってしまう
    float cursorAdjust = 0.5f;
    float terrainHight;
    //現在地と移動先の高さの差
    float terrainHightDif;
    bool isCloseCamera = true;

    //アイテム操作ウィンドウの離隔
    private const int ITEM_CONTROL_WINDOW_DISTANCE = 260;


    public bool isMoving;

    //200816 現在のターン数
    public int turn = 0;

    //200809 出撃するユニットの名前
    public List<string> entryUnitNameList { get; set; }

    //選択したステージ
    public Stage stage;

    //出撃準備からスタート
    public MapMode mapMode = MapMode.PREPARATION;

    //210522 ユニットのステータス画面にはマップ確認、通常から遷移出来るので、戻り先を保存しておく
    private MapMode returnMapMode;

    //整数にしたカーソルの座標
    Vector3 cursorPos;

    //200808 ボタンを1回押してからのウエイト
    public float deltaTime;

    //ボタンを押す最短間隔
    private const float BUTTON_WAIT = 0.1f;

    //200814 カーソルの移動の四捨五入調整の為使用
    private bool isXMovePositive;
    private bool isYMovePositive;

    //キャラクターの向き反転用
    private Vector3 latestPos;

    //210207 暗転中はメニューを表示しない
    private bool isInitFinish = false;

    //210210 十字キー入力中にボタンを押されるとバグる件対応
    private bool isMoveKeyDown;

    private float ControllDeltaTime;

    //210216 戦闘開始確認か
    private bool isAttackConfirm;

    //210218 メッセージの表示でモードが変わると戻り先確認が面倒なので、フラグで管理
    private bool isMessaseExist;

    //210220 カーソル自動移動の為作成
    private bool isCursorMove;

    //210303 持ち物一覧で選択した時に装備、使用するアイテムをtmpさせておく
    Item controlItem;
    int controlItemButtonIndex;

    //210211 BGN
    BGMPlayer bgmPlayer;

    //アイテムを取得したスキマに送った等の戦闘後メッセージ
    List<string> messageList;


    void Start()
    {
        //各種アセットファイル読み込み
        weaponDatabase = Resources.Load<WeaponDatabase>("weaponDatabase");
        potionDatabase = Resources.Load<PotionDatabase>("potionDatabase");
        toolDatabase = Resources.Load<ToolDatabase>("toolDatabase");
        accessoryDatabase = Resources.Load<AccessoryDatabase>("accessoryDatabase");
        EnemyDatabase enemyDatabase = Resources.Load<EnemyDatabase>("enemyDatabase");
        unitDatabase = Resources.Load<UnitDatabase>("unitDatabase");

        //選択したステージの情報をアセットから取得する 列挙型は選択しないとステージ1
        StageDatabase stageDatabase = Resources.Load<StageDatabase>("stageDatabase");
        stage = stageDatabase.FindByChapter(StageSelectManager.selectedChapter);
        //210302 ステージのテストの為
        //stage = stageDatabase.FindByChapter(Chapter.STAGE3);

        entryUnitNameList = new List<string>();
        messageList = new List<string>();

        //200808 ユニット一覧が初期化されていない場合 基本、ゲーム開始時初期化される為テスト用
        if (!UnitController.isInit)
        {
            //unitDatabase初期化
            Debug.Log("ユニット一覧初期化");
            
            unitController.InitUnitList(unitDatabase);
        }

        //210303 宝箱の取得情報
        if (!AcquiredTreasureManager.isInit)
        {
            acquiredTreasureManager.Init();
            Debug.Log("取得した宝箱一覧を初期化");
        }

        //210514 キーコンフィグを初期化
        if (KeyConfigManager.configMap == null)
        {
            string configFilePath = Application.persistentDataPath + "/keyConfig";
            KeyConfigManager.InitKeyConfig(configFilePath);
        }

        if (StageSelectManager.selectedChapter == 0)
        {
            StageSelectManager.selectedChapter = Chapter.STAGE1;
        }

        //進行度初期化
        if (!ChapterManager.isChapterInit)
        {
            ChapterManager.Init();
        }


        if (stage == null)
        {
            //通常有り得ない
            stage = stageDatabase.FindByChapter(Chapter.STAGE1);
            Debug.Log($"ERROR : 指定したステージが有りませんでした。");
        }
        Debug.Log($"ステージ:{stage.chapter}読み込み");

        //ステージが出撃選択出来ない場合は出撃選択ボタン無効化
        if (stage.isUnitSelectRequired)
        {
            unitSelectButton.SetActive(true);
        }
        else
        {
            unitSelectButton.SetActive(false);
        }

        //210519 会話クラスの初期化
        battleTalkManager = new BattleTalkManager();
        battleTalkManager.Init(this, talkWindow, talkView, fadeInOutManager);

        //210302 エディットに必要な要素一式を渡す
        mapEditManager.Init(this, mainMap, editorModeView, editMenuWindow, selectCellTypeWindow, inputCellText);

        //210211 戦闘処理の準備
        battleManager.Init(this, effectManager, enemyAIManager, battleTalkManager, expWindow, battleTextVisible);

        //210212 敵AIを初期化
        enemyAIManager.Init(this, battleManager, battleTalkManager, mainMap, unitContainer, enemyContainer, battleMapCursor);


        //ウィンドウにステージの名前、参加人数などを表示
        stageDetailWindow.GetComponent<StageDetailWindow>().Init(stage);

        //戦闘のメニュー画面の表示を設定
        battleConditionWindow.Init(stage);

        //210210 ターン開始エフェクトの初期化
        turnEffectManager.Init(this);

        //210523 出撃前のユニット追加処理
        unitController.AddUnitBeforeBattle(stage.chapter, unitDatabase);

        //出撃ユニット一覧を作成する
        unitController.InitPreparePartyList(this, unitSelectView);

        //現在の出撃ユニット数と最大出撃ユニット数表示
        entryCountWindow.UpdateText(entryUnitNameList.Count , stage.entryUnitCount);

        if (!ItemInventory.isInventoryInit)
        {
            itemInventory.Init();
        }

        StageLoader stageLoader = new StageLoader();

        //200902 Terrainの情報を取得
        terrain = stageLoader.LoadStageTerrainData(stage);


        //210302 Terrainを読み込む
        GameObject stageTerrain = stageLoader.LoadStageTerrain(stage);
        terrain.transform.position = stageTerrain.transform.position;
        Debug.Log($"Terrainの位置{terrain.transform.position}");

        //マップ精製 初回のマップ作成時は使用する
        //mainMap.Generate(20, 20);

        //マップ読み込み
        mainMap.LoadMap(stage);

        //210303 ステージによってライトの色を変更
        if(stage.chapter == Chapter.STAGE2)
        {
            //ステージ2は夜
            light.color = new Color(106.0f / 255.0f, 109f / 255.0f, 135.0f / 255.0f, 255.0f / 255.0f);
        }
        else
        {
            light.color = new Color(255.0f / 255.0f, 244f / 255.0f, 214.0f / 255.0f, 255.0f / 255.0f);
        }

        //霧の設定
        if (stage.chapter == Chapter.STAGE3 || stage.chapter == Chapter.STAGE4)
        {
            RenderSettings.fog = true;
        }
        else
        {
            RenderSettings.fog = false;
        }


        //210216 戦闘開始確認ウィンドウの初期化
        battleConfirmWindow.GetComponent<BattleConfirmWindow>().Init(battleManager);

        useItemManager.Init(this, effectManager);

        //出撃キャラの名前リストからPrefab精製
        int index = 0;
        foreach(String entryUnitName in entryUnitNameList)
        {
            
            var playerModel2 = (Instantiate(Resources.Load("Prefabs/Unit/UdongeModel")) as GameObject).transform;
            Unit unit = unitController.findByName(entryUnitName);
            if (unit != null)
            {

                playerModel2.GetComponentInChildren<PlayerModel>().Init(unit, this, mainMap);
                playerModel2.name = playerModel2.name.Replace("(Clone)", "");
                playerModel2.rotation = Quaternion.Euler(0, 180, 0);

                //ステージがユニットの座標を持っているので取得して配置する
                Coordinate coordinate = stage.entryUnitCoordinates[index];
                mainMap.PutUnit(coordinate.x, coordinate.y, playerModel2.GetComponent<PlayerModel>());
            }

            //出撃可能人数未満ならindex増やす
            if (index < entryUnitNameList.Count)
            {
                index++;
            }
        }
        //200813 敵リストから敵を配置 敵情報はステージが保持している
        List<Enemy> enemyList = stage.enemyList;

        int id = 0;
        foreach(Enemy enemy in enemyList)
        {
            Transform enemyModel = (Instantiate(Resources.Load("Prefabs/Unit/UdongeModel")) as GameObject).transform;
            enemyModel.GetComponent<EnemyModel>().Init(enemy, this, enemyAIManager, mainMap, id);
            enemyModel.name = enemyModel.name.Replace("(Clone)", "");
            Coordinate coordinate = enemy.coordinate;
            enemyModel.rotation = Quaternion.Euler(0, 180, 0);

            mainMap.PutEnemy(coordinate.x, coordinate.y, enemyModel.GetComponent<EnemyModel>());
            Debug.Log($"敵を配置 名前{enemy.name} X:{coordinate.x} , Y:{coordinate.y}");
            id++;
        }

        //210301 宝箱を設定する
        List<TreasureBox> treasureList = stage.treasureList;
        foreach (TreasureBox treasure in treasureList)
        {
            //章、座標が一致する取得済み宝箱情報が有るかを確認
            AcquiredTreasure acquiredTreasure = AcquiredTreasureManager.treasureList.FirstOrDefault(c => c.x == treasure.coordinate.x && c.y == treasure.coordinate.y && c.chapter == stage.chapter);
            if(acquiredTreasure != null)
            {
                Debug.Log($"既に取得済みの宝箱を配置しませんでした。 中身{treasure.item.ItemName} X:{treasure.x} , Y:{treasure.y}");
                continue;
            }

            //本クラスへの参照持たせる
            treasure.Init(this);
            Transform treasureModel = (Instantiate(Resources.Load("Prefabs/TreasureModel")) as GameObject).transform;

            //初期化
            treasureModel.GetComponent<TreasureModel>().Init(treasure,this , mainMap);
            treasureModel.name = treasureModel.name.Replace("(Clone)", "");
            Coordinate coordinate = treasure.coordinate;
            treasureModel.rotation = Quaternion.Euler(0, 180, 0);

            mainMap.PutTreasure(coordinate.x, coordinate.y, treasureModel.GetComponent<TreasureModel>());
            Debug.Log($"宝箱を配置 中身{treasure.item.ItemName} X:{coordinate.x} , Y:{coordinate.y}");
            id++;
        }

        //210227 主人公にカーソル移動
        Coordinate FirstCoordinate = stage.entryUnitCoordinates[0];
        Main_Cell firstCell = mainMap.Cells.FirstOrDefault(c => c.X == FirstCoordinate.x && c.Y == FirstCoordinate.y);


        battleMapCursor.transform.position = firstCell.transform.position;

        //カーソルが埋もれないように高さを調整
        terrainHight = getTerrainHight(battleMapCursor.transform.position.x, battleMapCursor.transform.position.z + cursorMoveSpeed);
        terrainHightDif = terrainHight - battleMapCursor.transform.position.y + cursorAdjust;
        //x,z座標の移動
        battleMapCursor.transform.Translate(0, terrainHightDif, cursorMoveSpeed);

        //200816 現在のターン数
        turn = 1;

        //210205 BGMPlayerをタイトルから引き継いでなければ作成
        GameObject bgmManager = GameObject.Find("BGMManager");
        if (bgmManager == null)
        {
            bgmManager = (Instantiate(Resources.Load("Prefabs/BGMManager")) as GameObject);
            bgmManager.name = bgmManager.name.Replace("(Clone)", "");

        }
        bgmPlayer = bgmManager.GetComponent<BGMPlayer>();

        //210206 BGM変更 既にステータス画面の曲が流れてる場合は再生しない
        bgmPlayer.ChangeBGM(BGMType.FIELD1);
        bgmPlayer.PlayBGM();

        //210209 フェードイン
        fadeInOutManager.FadeinStart();

    }//Startここまで

    void Update()
    {
        //プレー時間更新
        PlayTimeManager.TimeUpdate();
        deltaTime += Time.deltaTime;
        ControllDeltaTime += Time.deltaTime;

        //カメラの座標更新
        cameraUpdate();

        //カーソルの表示非表示制御
        CursorControll();

        //210207 暗転中は操作しない
        if (fadeInOutManager.isFadeinFadeout())
        {
            return;
        }

        //210209 初期化完了時のみ戦闘準備メニューウィンドウ表示
        if (!isInitFinish)
        {
            preparationWindow.SetActive(true);
            //最初のボタンを選択
            EventSystem.current.SetSelectedGameObject(preparationMenuWindow.transform.Find("BattleStartButton").gameObject);
            isInitFinish = true;
        }

        //210210 ターン開始時の処理を追加
        if (mapMode == MapMode.TURN_START)
        {
            turnEffectManager.TurnStartEffectUpdate();
            return;
        }
        else if (mapMode == MapMode.ENEMY_TURN_START)
        {
            //敵軍ターン開始処理
            turnEffectManager.EnemyTurnStartEffectUpdate();
            return;
        }
        else if (mapMode == MapMode.ENEMY_TURN)
        {
            //敵軍ターンの処理 enemyAIManager内で処理する
            enemyAIManager.EnemyAIUpdate();
            return;
        }
        else if (mapMode == MapMode.BATTLE)
        {
            //戦闘の処理はbattleManagerで行う
            battleManager.battleUpdate();
            return;
        } 
        else if(mapMode == MapMode.TALK || mapMode == MapMode.START_TALK || 
            mapMode == MapMode.BATTLE_BEFORE_TALK || mapMode == MapMode.BATTLE_AFTER_TALK || mapMode == MapMode.TURN_START_TALK)
        {
            //会話の処理はBattleTalkManagerで行う
            battleTalkManager.TalkUpdate();
            return;
        }
        else if (mapMode == MapMode.NORMAL)
        {
            if (isMessaseExist)
            {
                SetMapMode(MapMode.MESSAGE);
            }

            //オートターンエンド処理
            //オプションで変更出来るようにする
            AutoTurnEnd();
            //自軍ターンで敵が居ないか判定して0ならクリアとみなす
            if (enemyContainer.childCount == 0)
            {
                StageClear();
            }
        }
        else if (mapMode == MapMode.GAME_OVER)
        {
            //ゲームオーバー
            ReturnTitle();
        }



        //決定ボタンに関する処理
        SubmitButton();

        //メニューボタン
        MenuButton();

        //200725 キャンセルボタン
        CancelButton();


        //出撃準備の時は行わない処理
        if(mapMode != MapMode.PREPARATION && mapMode != MapMode.UNIT_SELECT)
        {
            //現在カーソルが有るセルの情報取得 nullが返ってきたらエラー
            //200725 現在の座標をまずカーソルの実際の位置から四捨五入する
            cursorPos = RoundPos(battleMapCursor.transform.position.x,
                battleMapCursor.transform.position.y,
                battleMapCursor.transform.position.z);

            //十字キー入力の反映
            GetControll();

            //210210 移動キーが押されているか確認
            checkMoveKeyDown();

            //カーソルの速さ変えるボタン
            ChangeCursorSpeed();

            //カメラの距離を変えるボタン
            AdjustCamela();

            //移動中はメニューを表示しない
            if (mapMode == MapMode.MOVEDMENU)
            {
                //移動中でなければメニュー表示
                if (isMoving)
                {
                    movedMenuWindow.SetActive(false);
                }
            }

            //210302 セルの味方、敵、地形効果を取得して反映
            GetCellProperty();
            //座標に敵や味方が居ればUIに反映
            UpdateUnitOutlineWindow();
            UpdateEnemyOutlineWindow();
        }

        //210303 フォーカスが外れた場合
        if(mapMode == MapMode.ITEM_SELECT)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                Debug.Log("ItemButtonを取得出来ませんでした");
                EventSystem.current.SetSelectedGameObject(weaponWindow.transform.Find("MapItemButton" + controlItemButtonIndex).gameObject);
            }
        }

        //エディット機能
        if (Input.GetKeyDown(KeyCode.E))
        {
            mapEditManager.ToggleEditMode();
        }
    }

    /// <summary>
    /// 210301 Updateから呼ばれ、現在のセルの敵味方ユニット、セル情報を表示する
    /// </summary>
    private void GetCellProperty()
    {
        
        cell = mainMap.Cells.FirstOrDefault(c => c.X == cursorPos.x && c.Y == cursorPos.z);

        //宝箱が有れば宝箱とUIに表示
        if (mainMap.IsTreasureExist(cell))
        {
            cellTypeView.TreasureUpdateText();
        }
        else
        {
            //宝箱が無い時
            //200725 現在の座標のセルを取得して画面情報に表示する
            if (cell != null)
            {
                cellTypeView.UpdateText(cell);

            }
        }
        //座標更新(エディット時のみのUI
        coordinateWindow.UpdateText((int)cursorPos.x, (int)cursorPos.z);

        
    }

    //現在のカーソル位置にユニットが居ればUIに表示する
    private void UpdateUnitOutlineWindow()
    {
        //カーソル位置にユニットが居たら情報を返す
        //200725 まずunitContainerにUnitが存在するか確認
        if (unitContainer.GetComponentInChildren<PlayerModel>() != null)
        {
            //特定のモードの場合は表示しない
            if (mapMode == MapMode.BATTLE_CONFIRM || mapMode == MapMode.ENEMY_TURN_START || mapMode == MapMode.ENEMY_TURN
                || mapMode == MapMode.ENEMY_MOVE)
            {
                mapUnitOutlineWindow.gameObject.SetActive(false);
                return;
            }

            //200725 座標にユニットが居ない事も当然有るのでNullを許容するFirstOrDefaultを使用
            PlayerModel selectedPlayer = unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(
                c => c.x == cursorPos.x && c.z == cursorPos.z);

            //nullチェックして存在すればウィンドウ表示
            if (selectedPlayer != null)
            {
                //プレイヤー情報を設定して表示
                mapUnitOutlineWindow.UpdateText(selectedPlayer.unit);
                mapUnitOutlineWindow.gameObject.SetActive(true);
            }
            else
            {
                //存在しなければウィンドウ自体を消す
                mapUnitOutlineWindow.gameObject.SetActive(false);
            }
        }
    }

    //現在のカーソル位置に敵が居ればUIに表示する
    private void UpdateEnemyOutlineWindow()
    {
        //敵の情報を表示
        if (enemyContainer.GetComponentInChildren<EnemyModel>() != null)
        {
            if (mapMode == MapMode.BATTLE_CONFIRM || mapMode == MapMode.ENEMY_TURN_START || mapMode == MapMode.ENEMY_TURN
                || mapMode == MapMode.ENEMY_MOVE || mapMode == MapMode.BATTLE)
            {
                mapEnemyOutlineWindow.gameObject.SetActive(false);
                return;
            }
            //200725 座標にユニットが居ない事も当然有るので
            //Nullを許容するFirstOrDefaultを使用
            EnemyModel selectedEnemy = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
                c => c.x == cursorPos.x && c.y == cursorPos.z);

            //nullチェックして存在すればウィンドウ表示
            if (selectedEnemy != null)
            {
                mapEnemyOutlineWindow.UpdateText(selectedEnemy.enemy);
                mapEnemyOutlineWindow.gameObject.SetActive(true);
            }
            else
            {
                //存在しなければウィンドウ自体を消す
                mapEnemyOutlineWindow.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 200808 ユニット選択ウィンドウを開く
    /// </summary>
    public void OpenUnitSelectWindow()
    {
        Debug.Log("Open UnitSelectWindow");

        //210207 ステージ詳細、メニューを非表示にする
        stageDetailWindow.SetActive(false);
        preparationMenuWindow.SetActive(false);

        unitSelectView.SetActive(true);
        unitOutlineWindow.gameObject.SetActive(true);

        mapMode = MapMode.UNIT_SELECT;
        //フォーカスを作成したPartyWindowの最初のボタンに合わせる
        Transform unitSelectWindow = unitSelectView.transform.Find("UnitSelectWindow");
        EventSystem.current.SetSelectedGameObject(unitSelectWindow.Find("PrepareUnitButton").gameObject);
    }

    /// <summary>
    /// 200813 「戦闘開始」ボタンを押した時 戦闘開始前会話→ターン開始エフェクトと遷移する
    /// </summary>
    public void BattleStart()
    {
        //準備メニュー非表示
        preparationWindow.SetActive(false);

        //210519 戦闘前会話をセット 基本的に存在する
        if (battleTalkManager.IsBattleStartTalkExist(StageSelectManager.selectedChapter))
        {
            //画面暗転して戦闘前会話へ
            fadeInOutManager.FadeoutAndFadeinStart(MapMode.START_TALK);
        }
        else
        {
            //戦闘前会話が存在しない場合
            fadeInOutManager.FadeoutAndFadeinStart(MapMode.TURN_START);
        }

        
    }

    //210211敵軍ターン開始 敵軍ターン開始エフェクトが終わった時に呼ばれる
    public void ChangeMapmodeEnemyTurn()
    {

        //敵軍ターン開始エフェクト表示へ
        mapMode = MapMode.ENEMY_TURN;
        enemyAIManager.enemyAIPhase = EnemyAIPhase.GET_ENEMY;
        Debug.Log("MapMode : " + mapMode);
    }

    /// <summary>
    /// 自軍のターン開始処理 指定ターン開始時の会話確認も行う
    /// </summary>
    public void PrepareMapmodeNormal()
    {
        //210520 ターン開始前会話が存在すればターン開始前会話へ
        if (battleTalkManager.IsTurnTalkExist(StageSelectManager.selectedChapter, turn))
        {
            SetMapMode(MapMode.TURN_START_TALK);
        }
        else
        {
            SetMapMode(MapMode.NORMAL);
        }

        mapView.gameObject.SetActive(true);
    }

    //mapModeをセットとログ出力
    public void SetMapMode(MapMode mapMode)
    {
        this.mapMode = mapMode;
        Debug.Log("mapMode = " + this.mapMode);
    }

    /// <summary>
    /// 210210 自軍ターン開始前の演出準備してから、戦闘前会話かターン開始演出へ遷移する
    /// </summary>
    public void ChangeMapmodeTurnStart(MapMode mapMode)
    {
        //エフェクトを表示完了にしてターン開始へ
        turnEffectManager.EffectInit();
        SetMapMode(mapMode);
    }

    /// <summary>
    /// 移動後のメニューを開く
    /// </summary>
    public void PrepareMapmodeMovedMenu()
    {
        Debug.Log("PrepareMapmodeMovedMenu");

        //210215 TODO 会話とか宝箱コマンドの表示もここに記載する
        PlayerModel playerModel = mainMap.ActiveUnit;
        Unit unit = mainMap.ActiveUnit.unit;

        //210301 宝箱判定を追加
        Main_Cell cell = mainMap.Cells.FirstOrDefault(c => c.X == playerModel.x && c.Y == playerModel.z);
        if(cell == null)
        {
            Debug.Log($"ERROR : セルが有りません x:{mainMap.ActiveUnit.x} y:{mainMap.ActiveUnit.z}");
        }
        //宝箱を取得する座標更新 空の場合は無いとみなす
        treasureCoordinate = mainMap.SearchTreasureBox(cell);

        //まず各ボタンを非表示とする
        var attackButton = movedMenuWindow.transform.Find("BattleButton").gameObject;
        var treasureButton = movedMenuWindow.transform.Find("TreasureButton").gameObject;
        var healButton = movedMenuWindow.transform.Find("HealButton").gameObject;

        attackButton.SetActive(false);
        treasureButton.SetActive(false);
        healButton.SetActive(false);

        //宝箱を発見した場合
        if (treasureCoordinate != null)
        {
            //鍵が有るか鍵開けスキルが有れば開けられるってこと
            bool hasSkill = unit.job.skills.Contains(Skill.鍵開け);
            bool hasKey = false;
            foreach(Item item in unit.carryItem)
            {
                if (item.ItemName == "宝の鍵")
                {
                    hasKey = true;
                    break;
                }
            }
            if (hasSkill || hasKey)
            {
                //宝箱ボタンを表示
                treasureButton.SetActive(true);
            }
        }

        //回復の杖、武器を持っているか確認
        bool hasHeal = false;
        bool hasWeapon = false;

        
        var itemList = unit.carryItem;
        foreach (var item in itemList)
        {
            if (item.ItemType == ItemType.WEAPON)
            {
                Weapon weapon = item.weapon;
                if (weapon.type == WeaponType.HEAL)
                {
                    //回復の杖を持っている
                    hasHeal = true;
                }
                else
                {
                    //回復以外の武器を持っているという事は攻撃可能ってこと
                    hasWeapon = true;
                }
            }
        }

        
        //武器を持っていれば攻撃対象が存在するかを確認
        if (hasWeapon)
        {
            //210302 敵が存在するか確認 リストで取得して、後々攻撃対象切り替えに使用する
            List<EnemyModel> attackableEnemyList = mainMap.GetAttackableEnemy(cell, unit);

            if (attackableEnemyList.Count != 0)
            {
                attackButton.SetActive(true);
            }
        }

        if (hasHeal)
        {
            List<PlayerModel> healableUnitList = mainMap.GetHealableUnit(cell, unit);

            if (healableUnitList.Count != 0)
            {
                healButton.SetActive(true);
            }
        }

        movedMenuWindow.SetActive(true);

        //連続で表示すると設定されない場合が有るのでリセット
        EventSystem.current.SetSelectedGameObject(null);

        //基本的には攻撃>回復>宝箱の優先度でボタンをフォーカス
        if (attackButton.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(attackButton);
        }
        else if (healButton.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(healButton);
        }
        else if (treasureButton.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(treasureButton);
        }
        else
        {
            //持ち物ボタンは絶対に存在する
            EventSystem.current.SetSelectedGameObject(movedMenuWindow.transform.Find("ItemButton").gameObject);
        }

        
        
        mapMode = MapMode.MOVEDMENU;
    }

    /// <summary>
    /// 210301 宝箱を開ける処理 「宝箱」ボタンを押すと呼ばれる
    /// </summary>
    /// <param name="coordinate"></param>
    public void OpenTreasureBox()
    {
        movedMenuWindow.SetActive(false);
        deltaTime = 0;

        Unit unit = mainMap.ActiveUnit.unit;
        //保存していなければ、無い状況で「宝箱」ボタンが表示されてるってこと　通常有り得ない
        if (treasureCoordinate == null)
        {
            Debug.Log("Error : 宝箱の座標を保持していません");
            return;
        }


        //既に探索済みなのでない事は無いはず
        TreasureModel treasureModel = treasureContainer.GetComponentsInChildren<TreasureModel>().FirstOrDefault(
                            c => c.x == treasureCoordinate.x && c.y == treasureCoordinate.y);

        //鍵開けスキルが無ければ宝の鍵を消費
        if (!unit.job.skills.Contains(Skill.鍵開け))
        {
            foreach(Item item in unit.carryItem)
            {
                if(item.ItemName == "宝の鍵")
                {
                    //宝の鍵削除 
                    unit.carryItem.Remove(item);
                    Debug.Log("宝の鍵を使用");
                    
                    //1個だけ削除するのでbreak
                    break;
                }
            }
        }

        //宝箱からアイテムを取り出すと同時に宝箱が空となる
        Item treasure = treasureModel.treasureBox.Open();
        treasureSE.Play();

        //取得済みに追加
        AcquiredTreasure acquiredTreasure = new AcquiredTreasure(treasureModel.x, treasureModel.y, stage.chapter);
        AcquiredTreasureManager.treasureList.Add(acquiredTreasure);

        Destroy(treasureModel.gameObject);

        messageList.Add($"{treasure.ItemName}を手に入れた！");
        Debug.Log($"{unit.name}はドロップアイテム{treasure.ItemName} を手に入れた！");

        //持ち物が一杯なら
        if (unit.carryItem.Count >= 6)
        {
            itemInventory.AddItem(treasure);
            messageList.Add($"{treasure.ItemName}は\n手持ちが一杯なのでスキマに送りました。");
            Debug.Log($"{treasure.ItemName} は手持ちが一杯なのでスキマに入れた。");
        }
        else
        {
            mainMap.ActiveUnit.unit.carryItem.Add(treasure);
            
        }

        //メッセージ表示
        OpenMessageWindow(messageList[0]);
        messageList.RemoveAt(0);//表示と同時に1件確認したので削除

        //待機処理 
        mainMap.ActiveUnit.isMoved = true;
        mainMap.ActiveUnit.SetBeforeAction(false);
        

        SetMapMode(MapMode.MESSAGE);
    }

    /// <summary>
    /// 200806 「待機」を押した時
    /// </summary>
    public void Wait()
    {
        //210521 敗北時は既にユニットはDestroyしているので行動済み処理行わない
        if(mainMap.ActiveUnit != null)
        {
            mainMap.ActiveUnit.isMoved = true;
            mainMap.ActiveUnit.SetBeforeAction(false);
        }
        
        deltaTime = 0;
        movedMenuWindow.SetActive(false);
        mapMode = MapMode.NORMAL;
        Debug.Log("MapMode : " + mapMode);

    }

    //210519 「マップ確認」ボタンを押した時 マップの確認
    public void MapView()
    {
        mapMode = MapMode.MAP_VIEW;
        Debug.Log("MapMode : " + mapMode);

        //戦闘準備ウィンドウ非表示
        preparationWindow.SetActive(false);

        //セル情報等を表示するようにする
        mapView.gameObject.SetActive(true);
    }

    //210518 「戻る」ボタンを押した時 ステージ選択画面に戻る
    public void Retire()
    {
        preparationWindow.SetActive(false);
        fadeInOutManager.ChangeScene("StageSelect");
    }

    /// <summary>
    /// 自動ターンエンド処理
    /// </summary>
    public void AutoTurnEnd()
    {
        //敵が1人も居ない時は勝利条件を満たしている可能性が有るので自動ターンエンド処理実施しない
        if(enemyContainer.childCount == 0)
        {
            return;
        }

        PlayerModel actionableUnit = unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(
                c => c.isMoved == false);

        if(actionableUnit == null)
        {
            TurnEnd();
        }
    }

    /// <summary>
    /// 200816 自軍のターン終了 処理はEnemyAIManagerに委ねる
    /// </summary>
    public void TurnEnd()
    {
        //行動済みキャラを未行動に
        foreach (PlayerModel player in unitContainer.GetComponentsInChildren<PlayerModel>())
        {

            player.isMoved = false;
            player.SetBeforeAction(true);
        }

        //210221 全ての敵の警戒範囲を削除する
        mainMap.ResetWarningCells();

        //ユニット概要を非表示に
        mapUnitOutlineWindow.gameObject.SetActive(false);
        mapEnemyOutlineWindow.gameObject.SetActive(false);


        //ターン追加と表示更新
        turn += 1;
        battleConditionWindow.UpdateTurn(turn);

        //ここで指定ターン数待機の敵をチェックして動作させる
        enemyAIManager.WaitEnemyControl();

        //「ターン終了」等のウィンドウが表示されているので非表示とする
        mapMenuView.SetActive(false);

        //エフェクトを表示未完了にして敵のターン開始へ
        turnEffectManager.EffectInit();
        mapMode = MapMode.ENEMY_TURN_START;
        Debug.Log("MapMode : " + mapMode);
    }

    /// <summary>
    /// 武器ウィンドウを表示する前処理
    /// </summary>
    public void initOpenWeaponWindow()
    {
        deltaTime = 0;

        selectedMovedMenuItem = movedMenuWindow.transform.Find("BattleButton").gameObject;

        mapMode = MapMode.WEAPON_SELECT;
        movedMenuWindow.SetActive(false);

        if (mainMap.ActiveUnit != null)
        {
            //武器ウィンドウを開く
            battleManager.openWeaponWindow(mainMap.ActiveUnit.unit.name, mapMode);
        }
    }

    /// <summary>
    /// 回復ウィンドウを表示する前処理
    /// </summary>
    public void InitOpenHealWindow()
    {
        deltaTime = 0;

        selectedMovedMenuItem = movedMenuWindow.transform.Find("HealButton").gameObject;

        mapMode = MapMode.HEAL_SELECT;
        movedMenuWindow.SetActive(false);

        if (mainMap.ActiveUnit != null)
        {
            //武器ウィンドウを開く
            battleManager.openWeaponWindow(mainMap.ActiveUnit.unit.name, mapMode);
        }
    }

    /// <summary>
    /// 210217 アイテム一覧表示
    /// BattleManagerのopenWeaponWindowと同じような処理
    /// </summary>
    public void OpenItemWindow()
    {
        deltaTime = 0;

        //モード変更
        mapMode = MapMode.ITEM_SELECT;

        //キャンセルボタンを押した時の戻り先を設定
        selectedMovedMenuItem = movedMenuWindow.transform.Find("ItemButton").gameObject;

        //道具リストのUIを表示する
        weaponWindow.SetActive(true);
        itemDetailWindow.SetActive(true);

        //移動後メニューを非表示に
        movedMenuWindow.SetActive(false);

        if (mainMap.ActiveUnit != null)
        {
            Debug.Log("ERROR: 選択ユニットが存在しません");
        }

        var itemList = mainMap.ActiveUnit.unit.carryItem;

        //ボタン精製後にフォーカス処理用
        bool isfocused = false;

        int index = 0;

        //アイテム一覧を作成
        foreach (Item item in itemList)
        {

            //Resources配下からボタンをロード
            var weaponButton = (Instantiate(Resources.Load("Prefabs/MapItemButton")) as GameObject).transform;
            weaponButton.GetComponent<MapItemButton>().Init(item, this, mainMap.ActiveUnit.unit, index);
            weaponButton.name = weaponButton.name.Replace("(Clone)", "");
            weaponButton.name += index;

            if(isfocused == false)
            {
                EventSystem.current.SetSelectedGameObject(weaponButton.gameObject);
                isfocused = true;
            }

            //partyWindowオブジェクト配下にprefab作成
            weaponButton.transform.SetParent(weaponWindow.transform, false);

            index++;
        }
    }

    //210217 道具の使用確認 PotionButtonクリック時に呼ばれる
    public void OpenUseItemConfirmWindow(Potion potion)
    {
        //モードを道具使用確認へ
        mapMode = MapMode.ITEM_USE_CONFIRM;

        //使用アイテム持たせる
        useItemConfirmWindow.GetComponent<UseItemConfirmWindow>().Init(this,potion);

        //表示していたウィンドウを閉じる
        itemDetailWindow.SetActive(false);
        weaponWindow.SetActive(false);

        //ウィンドウ開く
        useItemConfirmWindow.SetActive(true);

        //使用ボタンにフォーカス設定
        EventSystem.current.SetSelectedGameObject(useItemConfirmWindow.transform.Find("ButtonLayout/UseButton").gameObject);
    }

    //薬ボタンを選択した時に呼ばれる 薬の効果を表示する
    public void changePotionDetailWindow(Potion potion)
    {
        itemDetailWindow.GetComponent<ItemDetailWindow>().UpdateText(potion);
    }

    //210303 アイテム使用の設計変更により追加
    public void ClickUsePotion()
    {
        UsePotion(controlItem.potion);
    }

    //210217 UseItemConfirmWindowの決定ボタンから呼ばれる
    public void UsePotion(Potion potion)
    {
        //WeaponWindow初期化 これ、開く前に行えば良い気もする
        DeleteWeaponWindowItem();

        //雑に各ウィンドウを閉じる
        weaponWindow.SetActive(false);
        itemDetailWindow.SetActive(false);
        ItemUseEquipWindow.SetActive(false);

        //ウィンドウを閉じる
        useItemConfirmWindow.SetActive(false);

        //アイテム使用
        useItemManager.UseItem(mainMap.ActiveUnit, potion);


        
    }

    //武器が無限にWeaponWindowに増えてしまうのを削除
    private void DeleteWeaponWindowItem()
    {
        foreach (Transform obj in weaponWindow.transform)
        {
            Destroy(obj.gameObject);
        }
    }

    //アイテム詳細ウィンドウの更新を実施 MapItemButtonの選択時に実施
    public void changeItemDetailWindow(Item item)
    {
        //空のアイテムボタン選択時
        if (item == null)
        {
            weaponDetailWindow.SetActive(false);
            itemDetailWindow.SetActive(false);
            return;
        }

        //武器とそれ以外では情報量が違うのでウィンドウ切り替え
        if (item.ItemType == ItemType.WEAPON)
        {
            weaponDetailWindow.SetActive(true);
            itemDetailWindow.SetActive(false);
            Weapon weapon = weaponDatabase.FindByName(item.ItemName);
            weaponDetailWindow.GetComponent<DetailWindow>().UpdateBattleWeaponText(weapon);
        }
        else if (item.ItemType == ItemType.POTION)
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

    /// <summary>
    /// アイテムを預けたり、装備するウィンドウを開く
    /// </summary>
    public void OpenItemUseEquipWindow(Unit unit, Item item, Transform itemButtonTransform, int index, WeaponEquipWarn warn)
    {
        //一旦、「使う」、「装備」のボタンを非表示にして使うボタンのみ出していく
        GameObject itemUseButton = ItemUseEquipWindow.transform.Find("ItemUseButton").gameObject;
        itemUseButton.SetActive(false);

        GameObject itemEquipButton = ItemUseEquipWindow.transform.Find("ItemEquipButton").gameObject;
        itemEquipButton.SetActive(false);

        //キャンセル時にフォーカスを戻すボタンを記憶
        selectedItem = itemButtonTransform.gameObject;
        //depositItemButtonIndex = index;

        if (item == null)
        {
            return;
        }

        //鍵、金塊、クラスチェンジ等で使用可能なアイテムは存在しない
        if(item.ItemType == ItemType.TOOL)
        {
            return;
        }

        //回復の杖も何も起こらない
        if (item.ItemType == ItemType.WEAPON)
        {
            if(item.weapon.type == WeaponType.HEAL)
            {
                return;
            }

        }

        //操作するアイテムを保存
        controlItem = item;
        controlItemButtonIndex = index;

        //「預ける」等のコマンドウィンドウを表示する
        ItemUseEquipWindow.transform.position = new Vector3(itemButtonTransform.position.x + ITEM_CONTROL_WINDOW_DISTANCE - 10, itemButtonTransform.position.y - 20, itemButtonTransform.position.z);
        ItemUseEquipWindow.SetActive(true);

        //210224 ステータス上昇アイテム、回復アイテム共に「使う」コマンド表示 薬は全て使える
        if (item.ItemType == ItemType.POTION)
        {
            itemUseButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(ItemUseEquipWindow.transform.Find("ItemUseButton").gameObject);
        }

        else if (item.ItemType == ItemType.WEAPON)
        {
            //装備出来ない等の武器は警告を出す
            if(warn != WeaponEquipWarn.NONE)
            {
                OpenMessageWindow(warn);
                return;
            }

            //装備不可能でも一応、「装備」ボタンを出す
            itemEquipButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(ItemUseEquipWindow.transform.Find("ItemEquipButton").gameObject);
        }
        else if (item.ItemType == ItemType.ACCESSORY)
        {
            //アクセサリは今の所、誰でも装備出来る
            itemEquipButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(ItemUseEquipWindow.transform.Find("ItemEquipButton").gameObject);
        }

        //手持ちアイテムを不活性化
        foreach (Transform weaponButton in weaponWindow.transform)
        {
            weaponButton.GetComponent<Button>().interactable = false;
        }

        //モード変更
        mapMode = MapMode.ITEM_EQUIP_USE_MENU;
        Debug.Log($"mapMode:{mapMode}");

    }

    //「装備」ボタンを押した時の処理 StatusManagerを参考に作成
    public void EquipItem()
    {
        Unit unit = mainMap.ActiveUnit.unit;
        if (controlItem.ItemType == ItemType.WEAPON)
        {
            unit.equipWeapon = controlItem.weapon;

            //一旦、全ての武器の装備フラグを切ってしまう
            foreach (Item item in unit.carryItem)
            {
                if (item.ItemType == ItemType.WEAPON)
                {
                    item.isEquip = false;
                }
            }
        }
        else if (controlItem.ItemType == ItemType.ACCESSORY)
        {
            unit.equipAccessory = controlItem.accessory;
            foreach (Item item in unit.carryItem)
            {
                if (item.ItemType == ItemType.ACCESSORY)
                {
                    item.isEquip = false;
                }
            }
        }

        //装備フラグを立てる
        controlItem.isEquip = true;

        //装備フラグが変わるので持ち物一覧更新
        ChangeUnitItemWindow(unit.name);

        //「装備」「使う」ウィンドウを消す
        ItemUseEquipWindow.SetActive(false);

        //アイテムを操作するモードへ戻る
        mapMode = MapMode.ITEM_SELECT;
        Debug.Log($"mapMode:{mapMode}");

        //ボタン活性化
        foreach (Transform weaponButton in weaponWindow.transform)
        {
            //ボタンを活性化する
            weaponButton.GetComponent<Button>().interactable = true;
        }

        //フォーカスを選択したボタンへ戻す
        EventSystem.current.SetSelectedGameObject(weaponWindow.transform.Find("MapItemButton" + controlItemButtonIndex).gameObject);
    }

    //装備を変更した際のアイテム一覧更新
    public void ChangeUnitItemWindow(string unitName)
    {
        //無限に増えて行ってしまうので削除を行う
        foreach (Transform obj in weaponWindow.transform)
        {
            Destroy(obj.gameObject);
        }


        Unit unit = mainMap.ActiveUnit.unit;


        List<Item> itemList = unit.carryItem;

        //BattleMapManagerのOpenItemWindowを参考に作成
        //共通化は不活性、フォーカス等が有り、少し難しいと思う

        int index = 0;

        //アイテム一覧を作成
        foreach (Item item in itemList)
        {

            //Resources配下からボタンをロード
            var weaponButton = (Instantiate(Resources.Load("Prefabs/MapItemButton")) as GameObject).transform;
            weaponButton.GetComponent<MapItemButton>().Init(item, this, unit, index);
            weaponButton.name = weaponButton.name.Replace("(Clone)", "");
            weaponButton.name += index;

            //partyWindowオブジェクト配下にprefab作成
            weaponButton.transform.SetParent(weaponWindow.transform, false);

            index++;
        }

    }

    //210217 武器、回復を装備出来ないボタンを押した時に警告を表示
    public void OpenMessageWindow(WeaponEquipWarn warn)
    {
        //メッセージフラグ表示
        isMessaseExist = true;

        //メッセージ表示
        messageWindow.GetComponent<MessageWindow>().UpdateText(warn.GetStringValue());

        //ウィンドウ表示
        messageWindow.SetActive(true);

        //確認ボタンにフォーカス設定
        EventSystem.current.SetSelectedGameObject(messageWindow.transform.Find("ConfirmButton").gameObject);

    }

    /// <summary>
    /// 210225 汎用的なメッセージ表示
    /// </summary>
    /// <param name="text"></param>
    public void OpenMessageWindow(string text)
    {
        //メッセージフラグ表示
        isMessaseExist = true;

        //メッセージ表示
        messageWindow.GetComponent<MessageWindow>().UpdateText(text);

        //ウィンドウ表示
        messageWindow.SetActive(true);

        //確認ボタンにフォーカス設定
        EventSystem.current.SetSelectedGameObject(messageWindow.transform.Find("ConfirmButton").gameObject);
    }

    //メッセージウィンドウの確認ボタンをクリックした時に呼ぶ用 ウィンドウを閉じて元のフォーカスへ
    public void CloseMessageWindow()
    {
        if(messageList.Count != 0)
        {
            //まだメッセージがある場合は次のメッセージ表示
            OpenMessageWindow(messageList[0]);
            messageList.RemoveAt(0);
        }
        else
        {
            //最後のメッセージの場合
            isMessaseExist = false;
            
            messageWindow.SetActive(false);

            //210523 基本、宝箱のメッセージのみだが、そうでない場合は戻り先の制御が必要となる
            SetMapMode(MapMode.NORMAL);

            //元のフォーカスへ
            EventSystem.current.SetSelectedGameObject(selectedItem);
            
        }
        
    }

    //戦闘後のメッセージは、終了後のフォーカスが無いのでこっち。
    public void BatleCloseMessageWindow()
    {
        isMessaseExist = false;
        messageWindow.SetActive(false);

    }



    /// <summary>
    /// 200813 ユニットの武器ボタンを押した時呼ばれる
    /// 攻撃する対象の敵を選ぶ
    /// </summary>
    public void Attack(Weapon weapon)
    {
        decideSE.Play();

        deltaTime = 0;
        if(mainMap.ActiveUnit != null)
        {
            //武器ウィンドウを非表示に
            movedMenuWindow.SetActive(false);
            battleManager.CloseWeaponWindow();


            //攻撃対象選択
            if(weapon.type != WeaponType.HEAL)
            {
                //攻撃の場合
                mapMode = MapMode.ATTACK_SELECT;
            }
            else
            {
                //回復の場合
                mapMode = MapMode.HEAL_TARGET_SELECT;

            }

            Debug.Log("MapMode : " + mapMode);

            //武器を装備
            mainMap.ActiveUnit.Attack(weapon);
        }
        
    }

    public void Equip(Unit unit, Weapon weapon)
    {
        //武器を装備
        unit.equipWeapon = weapon;
        Debug.Log($"{unit.name}は{weapon.name}を装備した");

        //ボタンのUI更新 他の武器は装備していない状態にする
        foreach(Item item in unit.carryItem)
        {
            if(item.ItemType == ItemType.WEAPON){
                //ボタンに設定されたインスタンスのみ装備フラグを立てる
                if(item.weapon == weapon)
                {
                    item.isEquip = true;
                }
                else
                {
                    item.isEquip = false;
                }
                
            }
        }
    }

    /// <summary>
    /// 210220 風花雪月を意識して、攻撃範囲の敵が居るセルにカーソルを移動させる
    /// </summary>
    public void MoveCursorToAttackAbleCell(Weapon weapon)
    {
        //攻撃可能範囲のセルを取得
        if (weapon.type != WeaponType.HEAL)
        {
            foreach (var cell in mainMap.Cells.Where(c => c.IsAttackable))
            {

                EnemyModel destinationEnemy = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
                c => c.x == cell.X && c.y == cell.Y);

                if (destinationEnemy != null)
                {
                    //自動カーソルスナップをオフ
                    mapMode = MapMode.CURSOR_MOVE;

                    Debug.Log($"攻撃範囲に敵が存在しました x={cell.X},y={cell.Y}");
                    battleMapCursor.transform.DOMove(new Vector3(cell.transform.position.x, cell.transform.position.y + cursorAdjust,
                        cell.transform.position.z), 0.2f).OnComplete(() =>
                        {
                                mapMode = MapMode.ATTACK_SELECT;
                        });
                    break;
                }
            }
        }
        else
        {
            //回復の場合
            foreach (var cell in mainMap.Cells.Where(c => c.IsHealable))
            {
                PlayerModel destinationUnit = unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(
                c => c.x == cell.X && c.z == cell.Y);

                if (destinationUnit != null)
                {
                    //自動カーソルスナップをオフ
                    mapMode = MapMode.CURSOR_MOVE;

                    Debug.Log($"回復範囲にユニットが存在しました x={cell.X},y={cell.Y}");
                    battleMapCursor.transform.DOMove(new Vector3(cell.transform.position.x, cell.transform.position.y + cursorAdjust,
                        cell.transform.position.z), 0.2f).OnComplete(() =>
                        {
                            mapMode = MapMode.HEAL_TARGET_SELECT;
                        });
                    break;
                }
            }
        }
    }

    //210210 カーソルの表示非表示を制御する
    private void CursorControll()
    {
        //戦闘準備、戦闘の時はカーソルを非表示
        if (mapMode == MapMode.PREPARATION || mapMode == MapMode.UNIT_SELECT || mapMode == MapMode.BATTLE || mapMode == MapMode.STAGE_CLEAR)
        {

            battleMapCursor.gameObject.SetActive(false);
        }
        else
        {
            battleMapCursor.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 210225 ステージクリア判定メソッド
    /// </summary>
    public void StageClear()
    {
        //ユニット追加処理
        unitController.AddUnitAfterBattle(stage.chapter, unitDatabase);

        //210304 進行度を更新 インクリメントすると次の章へ
        //210522 体験版はステージ2まで 
        if ((int)stage.chapter < 2){
            ChapterManager.chapter = stage.chapter += 1;
        }
        
        Debug.Log("ステージクリア！次のステージ : " + ChapterManager.chapter.GetStringValue());
        //仮にメッセージを表示
        OpenMessageWindow("ステージクリアしました。");

        mapMode = MapMode.STAGE_CLEAR;
        Debug.Log("MapMode : " + mapMode);
    }

    public void GameOver()
    {
        mapMode = MapMode.GAME_OVER;
        Debug.Log("MapMode : " + mapMode);

        //210304 進行度を更新 インクリメントすると次の章へ
        ChapterManager.chapter = stage.chapter += 1;
        Debug.Log("ゲームオーバー");
        //仮にメッセージを表示
        OpenMessageWindow("満身創痍！ゲームオーバー・・・");
    }

    //ゲームオーバー後にタイトル画面へ戻る
    private void ReturnTitle()
    {
        //メッセージを消したら処理が始まる
        if (isMessaseExist)
        {
            return;
        }
        if (bgmPlayer != null)
        {
            bgmPlayer.StopBGM();
        }
        fadeInOutManager.ChangeScene("Title");
    }


    /// <summary>
    /// 200808 UnitButtonを選択したタイミングで文字を変更する
    /// </summary>
    /// <param name="unitName"></param>
    public void changeUnitOutlineWindow(string unitName)
    {
        Unit forcusUnit = unitController.findByName(unitName);
        unitOutlineWindow.UpdateText(forcusUnit);

    }
    /// <summary>
    /// 選択したユニットをList<string>の出撃ユニット一覧へ追加
    /// 既に追加されていたら削除する
    /// </summary>
    /// <param name="unitName"></param>
    public void EntryUnitAndRemoveUnit(String selectedUnitName)
    {
        List<GameObject> prepareUnitButtons = new List<GameObject>(GameObject.FindGameObjectsWithTag("PrepareUnitButton"));
        GameObject selectedPrepareUnitButton = prepareUnitButtons.FirstOrDefault(
                unitButton => unitButton.GetComponent<PrepareUnitButton>().unitName.Equals(selectedUnitName));

        //まず、既に追加されているか確認
        string unitName = entryUnitNameList.FirstOrDefault(name => name.Equals(selectedUnitName));

        //既に存在していたらリストから除く
        if(unitName != null)
        {
            entryUnitNameList.Remove(selectedUnitName);
            
            //ボタンに出撃済みマークを追加
            if(selectedPrepareUnitButton != null)
            {
                selectedPrepareUnitButton.GetComponent<PrepareUnitButton>().removeEntry();
            }
        }
        else
        {
            //まだ存在しなければ追加
            entryUnitNameList.Add(selectedUnitName);

            //ボタンの出撃済みマークを消す
            if (selectedPrepareUnitButton != null)
            {
                selectedPrepareUnitButton.GetComponent<PrepareUnitButton>().setEntry();
            }
        }

        

        //出撃人数未満なら全てのユニットボタンに対して有効化する
        if (entryUnitNameList.Count < stage.entryUnitCount)
        {
            
            foreach (GameObject prepareUnitButton in prepareUnitButtons)
            {

                prepareUnitButton.GetComponent<PrepareUnitButton>().SetEnable();
            }
        }
        else
        {
            //出撃ユニット人数に達した時は、既に出撃対象のユニット以外のボタンを無効化する
            foreach (GameObject prepareUnitButton in prepareUnitButtons)
            {
                if (!entryUnitNameList.Contains(prepareUnitButton.GetComponent<PrepareUnitButton>().unitName))
                {
                    prepareUnitButton.GetComponent<PrepareUnitButton>().SetDisable();
                }
            }
        }
        

        //人数更新
        entryCountWindow.UpdateText(entryUnitNameList.Count, stage.entryUnitCount);

    }



    /// <summary>
    /// カーソルの早さを変える
    /// </summary>
    private void ChangeCursorSpeed()
    {
        if (Input.GetButton("Speed") || KeyConfigManager.GetKey(KeyConfigType.SPEED))
        {
            cursorMoveSpeed = 0.1f;
        }
        if (Input.GetButtonUp("Speed") || KeyConfigManager.GetKeyUp(KeyConfigType.SPEED))
        {
            cursorMoveSpeed = 0.05f;
        }
    }

    //座標を整数に丸めてくれる 200814 戻ってしまう事が有るので移動方向により切り上げ、切り捨てを変更
    Vector3 RoundPos(float x,float y, float z)
    {
        int roundX;
        int roundZ;

        if (isXMovePositive)
        {
            roundX = Mathf.CeilToInt(x);
        }
        else
        {
            roundX = Mathf.FloorToInt(x);
        }

        if (isYMovePositive)
        {
            roundZ = Mathf.CeilToInt(z);
        }
        else
        {
            roundZ = Mathf.FloorToInt(z);
        }

        return new Vector3(roundX, y, roundZ);
    }

    //座標からTerrainの高さを取得する
    float getTerrainHight(float x, float z) 
    {
        //terrainの座標を取得するおまじない
        //Debug.Log($"x : {(x - terrain.transform.position.x) / terrain.terrainData.size.x} z : {(z - terrain.transform.position.z) / terrain.terrainData.size.z}");
        return terrain.terrainData.GetInterpolatedHeight((x - terrain.transform.position.x) / terrain.terrainData.size.x, (z - terrain.transform.position.z) / terrain.terrainData.size.z);
    }

    

    //決定ボタン
    private void SubmitButton()
    {
        //〇ボタン
        if (KeyConfigManager.GetKeyDown(KeyConfigType.SUBMIT) || Input.GetButtonDown("Submit"))
        {
            //210302 エディットモード中は専用メソッドに処理を委ねる
            if (mapEditManager.isEditorMode)
            {
                return;
            }

            //ボタンの操作間隔未満で連打された場合は何もしない
            if (deltaTime <= BUTTON_WAIT)
            {
                return;
            }

            deltaTime = 0;

            //十字キー入力中はバグるので操作させない
            if (isMoveKeyDown){
                return;
            }

            if (mapMode.isMenuVisible())
            {
                //210514 uGUIが表示されている時はボタンをクリック
                KeyConfigManager.ButtonClick();
                return;
            }

            //編集用モードではない時
            //200725 ユニットが選択された状態で〇が押されたら移動距離を表示
            if (mapMode == MapMode.NORMAL)
            {
                //ユニットの移動
                if (unitContainer.GetComponentInChildren<PlayerModel>() != null)
                {
                    //決定ボタンを押すとカーソル位置のユニット取得
                    PlayerModel selectedPlayer = unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(
                    c => c.x == cursorPos.x && c.z == cursorPos.z);

                    if (selectedPlayer != null)
                    {
                        Debug.Log("選択したユニット:" + selectedPlayer.unit.name);

                        //行動済みなら何も起こらない
                        if (selectedPlayer.isMoved == true)
                        {
                            return;
                        }

                        //効果音再生
                        decideSE.Play();

                        //選択中のユニット名保存
                        mainMap.activeUnitName = selectedPlayer.unit.name;

                        //移動前の座標を保存しておく
                        if (cell != null)
                        {
                            beforeCell = cell;
                        }

                        //移動可能な距離を表示する
                        selectedPlayer.HighlightMovableCells();
                        mapMode = MapMode.MOVE;
                        Debug.Log("mapMode =" + mapMode);
                        return;
                    }
                 }

                //210221 敵の攻撃可能範囲ハイライトを追加
                if (enemyContainer.GetComponentInChildren<EnemyModel>() == null)
                {
                    return;
                }

                //決定ボタンを押すとカーソル位置のユニット取得
                EnemyModel selectedEnemy = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
                c => c.x == cursorPos.x && c.y == cursorPos.z);

                if (selectedEnemy == null)
                {
                    Debug.Log($"選択したセルに敵が存在しません x={cursorPos.x}, y={cursorPos.z}");
                    return;
                }

                Debug.Log($"選択した敵:{selectedEnemy.enemy.name}x={cursorPos.x}, y={cursorPos.z}");

                //選択した敵を更新
                mainMap.activeEnemyName = selectedEnemy.enemy.name;
                mainMap.activeEnemyid = selectedEnemy.enemyId;

                //効果音再生
                decideSE.Play();

                //特に選択した敵をActiveEnemyに設定する等の処理は無い予定。
                if (!selectedEnemy.isHighLight)
                {
                    //まだ攻撃範囲を表示していなければ、攻撃可能な範囲を表示する
                    selectedEnemy.isHighLight = true;
                    selectedEnemy.HighLightWarningCells();
                }
                else
                {
                    //既にハイライト済みの時
                    selectedEnemy.isHighLight = false;
                    selectedEnemy.attackableCellList.Clear();
                    selectedEnemy.isCancelReload = false;
                    mainMap.RemoveWarningCells(selectedEnemy.enemyId);
                }

                return;
            }


            else if (mapMode == MapMode.MOVE)
            {

                //ユニットの移動可能範囲を表示している時
                //cellが無かったり、移動不可だったら何もしない
                if (cell == null)
                {
                    Debug.Log("WARN : セルが有りません");
                    return;
                }
                if (!cell.IsMovable)
                {
                    Debug.Log("WARN : 移動不可能なセルです");
                    return;
                }

                //移動可能な先にユニットが居る場合、自分自身ならその場待機、自分以外なら重なってしまうので移動不可能
                
                PlayerModel destinationPlayer = unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(
                    c => c.x == cell.X && c.z == cell.Y);

                if (destinationPlayer != null)
                {
                    if(destinationPlayer.unit.name != mainMap.activeUnitName)
                    {
                        Debug.Log("WARN : 既にユニットが居ます");
                        return;
                    }
                }

                //敵が居るセルは通行出来ないのでそもそもセルが移動可能にならないが、すり抜け対策に書いておく
                EnemyModel destinationEnemy = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
                c => c.x == cell.X && c.y == cell.Y);

                if (destinationEnemy != null)
                {
                    Debug.Log("WARN : 敵ユニットが居るセルには移動出来ません");
                    return;
                }

                //移動可能なセルなら移動
                if (cell.IsMovable)
                {
                    //移動可能セルの削除
                    mainMap.ResetMovableCells();
                    mainMap.ResetAttackableCells();
                    //210219 移動先 = 現在いる場所の場合は移動を行なわない
                    if (cell.X == mainMap.ActiveUnit.x && cell.Y == mainMap.ActiveUnit.z)
                    {
                        
                        isMoving = false;
                        PrepareMapmodeMovedMenu();
                        return;
                    }
                    //移動中モードへ
                    isMoving = true;

                    //カーソルを移動させただけでセルを取得してくれているので、Mapのメソッドに渡して移動
                    mainMap.ActiveUnit.MoveTo(cell);
                    return;
                }
            }
            else if(mapMode == MapMode.ATTACK_SELECT)
            {
                //攻撃する敵を選択する時

                if(cell != null)
                {
                    //攻撃可能のセルの場合
                    if (cell.IsAttackable)
                    {
                        //現在のセルに居る敵を取得して、存在すれば
                        EnemyModel selectedEnemy = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
                            c => c.x == cell.X && c.y == cell.Y);
                        if (selectedEnemy != null)
                        {

                            //activeEnemyに追加
                            mainMap.activeEnemyName = selectedEnemy.enemy.name;
                            mainMap.activeEnemyid = selectedEnemy.enemyId;

                            //選択した敵の方向を向く
                            Vector3 vector = new Vector3(selectedEnemy.transform.position.x, mainMap.ActiveUnit.transform.position.y,
                                selectedEnemy.transform.position.z);
                            mainMap.ActiveUnit.transform.DOLookAt(vector, 0.3f);

                            //攻撃可能範囲を消す
                            mainMap.ResetAttackableCells();

                            //戦闘確認からの戻り先モード判定の為
                            isAttackConfirm = true;

                            mapMode = MapMode.BATTLE_CONFIRM;
                            //200814 戦闘画面を開く
                            battleManager.MapOpenBattleView(mainMap.ActiveUnit, mainMap.ActiveEnemy, true);
                        }
                    }
                }
            }
            else if(mapMode == MapMode.HEAL_TARGET_SELECT)
            {
                if (cell == null)
                {
                    Debug.Log("ERROR: セルが有りません");
                    return;
                }

                //攻撃可能のセル(ハイライトされていればOK)の場合
                if (cell.IsHealable)
                {

                    //現在のセルに居る敵を取得して、存在すれば
                    PlayerModel selectedUnit = unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(
                        c => c.x == cell.X && c.z == cell.Y);
                    if (selectedUnit == null)
                    {
                        Debug.Log("WARN: プレイヤーが存在しません");
                        return;
                    }
                    Debug.Log($"対象のユニット:{selectedUnit.unit.name}");
                    StatusCalculator statusCalculator = new StatusCalculator();
                    int maxhp = selectedUnit.unit.maxhp + selectedUnit.unit.job.statusDto.jobHp;
                    maxhp = statusCalculator.CalcHpBuff(maxhp, selectedUnit.unit.job.skills);


                    if (selectedUnit.unit.hp >= maxhp)
                    {
                        Debug.Log("WARN: 体力が減っているユニットのみ回復出来ます");
                        return;
                    }

                    //アクティブなプレイヤーは回復する側なので、回復対象としないこと

                    //選択したユニットの方向を向く
                    Vector3 vector = new Vector3(selectedUnit.transform.position.x, mainMap.ActiveUnit.transform.position.y,
                        selectedUnit.transform.position.z);
                    mainMap.ActiveUnit.transform.DOLookAt(vector, 0.3f);

                    //攻撃可能範囲を消す
                    mainMap.ResetAttackableCells();
                    
                    //戦闘確認からの戻り先モード判定の為
                    isAttackConfirm = false;

                    mapMode = MapMode.BATTLE_CONFIRM;
                    //200814 戦闘画面を開く
                    battleManager.MapOpenHealView(mainMap.ActiveUnit.unit, selectedUnit);
                }
            }
            else if(mapMode == MapMode.STAGE_CLEAR)
            {

                //クリアした時に決定ボタンを押した場合、戦闘後会話へ　「_AFTER」を付けると戦闘後会話
                TalkManager.sceneName = StageSelectManager.selectedChapter.ToString() + "_AFTER";
                Debug.Log($"会話読み込み{TalkManager.sceneName}");
                fadeInOutManager.ChangeScene("Talk");
            }
            
        }

        if (mapEditManager.isEditorMode)
        {
            //編集モードのみ押しっぱなし対応でセル連続入力
            if (KeyConfigManager.GetKey(KeyConfigType.SUBMIT) || Input.GetButton("Submit"))
            {
                mapEditManager.EditFireButton();
            }
        }
        
    }

    /// <summary>
    /// 210219 ステータス画面を開く等、その他の処理 エディット中は使用しない
    /// </summary>
    public void MenuButton()
    {
        //210221 主に△ボタンはメニューボタンにする
        if (KeyConfigManager.GetKeyDown(KeyConfigType.MENU) || Input.GetButton("Menu"))
        {
            //ボタン連打防止
            if (deltaTime <= BUTTON_WAIT)
            {
                return;
            }

            //メッセージ表示中は何も起こらない
            if (isMessaseExist)
            {
                return;
            }

            deltaTime = 0;

            Debug.Log("メニューボタンを押した");

            //通常時、マップ確認時のみ表示 敵のターン、UIが出ている時等に表示されたらダメ
            if (mapMode == MapMode.NORMAL || mapMode == MapMode.MAP_VIEW)
            {
                //有り得ないが、もしユニットが存在しなければ何もしない
                if (unitContainer.GetComponentInChildren<PlayerModel>() == null)
                {
                    Debug.Log("ERROR:ユニットがunitContainerに存在しません");
                    return;
                }

                //カーソル位置のユニット取得
                PlayerModel selectedPlayer = unitContainer.GetComponentsInChildren<PlayerModel>().FirstOrDefault(
                c => c.x == cursorPos.x && c.z == cursorPos.z);

                EnemyModel selectedEnemy = enemyContainer.GetComponentsInChildren<EnemyModel>().FirstOrDefault(
                c => c.x == cursorPos.x && c.y == cursorPos.z);

                //プレイヤーが存在すればプレイヤーのステータス表示
                if (selectedPlayer != null)
                {
                    OpenUnitStatusWindow(selectedPlayer);

                }
                else if (selectedEnemy != null)
                {
                    OpenEnemyStatusWindow(selectedEnemy);
                }
                else
                {
                    //敵、味方共に居ない場合は何も起こらない
                    Debug.Log("WARN:ユニットの居ないセルです");
                    return;
                }

                //効果音再生
                decideSE.Play();

                //通常、マップ確認等からステータス画面に遷移出来るので、戻り先を保存しておく
                returnMapMode = mapMode;

                mapMode = MapMode.STATUS;
                Debug.Log($"mapMode:{mapMode}");

                //ここでやっとステータス表示
                statusWindow.SetActive(true);

                return;
            }
        }
        
    }

    private void OpenUnitStatusWindow(PlayerModel selectedPlayer)
    {
        Debug.Log("選択したユニット:" + selectedPlayer.unit.name);

        Unit unit = selectedPlayer.unit;



        //選択中のユニット名保存
        mainMap.activeUnitName = selectedPlayer.unit.name;

        //ユニットのDBに依存しないデータ表示
        statusWindow.GetComponent<StatusWindow>().updateText(unit);

        //装備している武器を表示
        statusWindow.GetComponent<StatusWindow>().addEquipWeapon(unit.equipWeapon, null, this);

        //装備しているアクセサリを表示
        statusWindow.GetComponent<StatusWindow>().addEquipAccessory(unit.equipAccessory, null, this);


        //200726 手持ちアイテムから道具リストを作成する
        List<Item> itemList = unit.carryItem;
        statusWindow.GetComponent<StatusWindow>().addInventoryList(itemList, null, this);
        

        //スキル一覧作成
        List<Skill> skillList = unit.job.skills;
        statusWindow.GetComponent<StatusWindow>().addSkillList(skillList, null, this);
        
    }

    //210518 敵のステータスを表示する
    public void OpenEnemyStatusWindow(EnemyModel selectedEnemy)
    {
        Debug.Log("選択した敵:" + selectedEnemy.enemy.name);

        Enemy enemy = selectedEnemy.enemy;



        //選択中のユニット名保存
        mainMap.activeEnemyName = selectedEnemy.enemy.name;

        //ユニットのDBに依存しないデータ表示
        statusWindow.GetComponent<StatusWindow>().updateText(enemy);

        //装備している武器を表示
        statusWindow.GetComponent<StatusWindow>().addEquipWeapon(enemy.equipWeapon, null, this);

        //装備しているアクセサリを表示
        statusWindow.GetComponent<StatusWindow>().addEquipAccessory(enemy.equipAccessory, null, this);


        //200726 手持ちアイテムから道具リストを作成する
        List<Item> itemList = enemy.carryItem;
        statusWindow.GetComponent<StatusWindow>().addInventoryList(itemList, null, this);


        //スキル一覧作成
        List<Skill> skillList = enemy.job.skills;
        statusWindow.GetComponent<StatusWindow>().addSkillList(skillList, null, this);
    }


    /// <summary>
    /// キャンセルボタン
    /// </summary>
    public void CancelButton()
    {
        //200725 キャンセルボタン
        if (KeyConfigManager.GetKeyDown(KeyConfigType.CANCEL) || Input.GetButton("Cancel"))
        {
            if (deltaTime <= BUTTON_WAIT)
            {
                return;
            }

            //ボタン連打防止
            deltaTime = 0;

            //エディットモード時は専用メソッドに処理を委ねる
            if (mapEditManager.isEditorMode)
            {
                mapEditManager.EditCancelButton();
                return;
            }

            //210217 メッセージウィンドウは優先度高く設定
            if (isMessaseExist)
            {
                CloseMessageWindow();
                return;
            }

            //200816 キャンセルボタンでマップメニュー開
            if (mapMode == MapMode.NORMAL)
            {
                Debug.Log("mapmenu");
                mapMode = MapMode.MAP_MENU;
                mapMenuView.SetActive(true);
                battleConditionWindow.UpdateUnitCount(unitContainer.childCount, enemyContainer.childCount);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(mapMenuView.transform.Find("MapMenuWindow/TurnEndButton").gameObject);
            }

            else if (mapMode == MapMode.MAP_VIEW)
            {
                //マップ確認中の時
                mapMode = MapMode.PREPARATION;
                Debug.Log($"mapMode:{mapMode}");

                //セルの情報などが表示されるUIを非表示、戦闘準備ウィンドウ表示
                mapView.SetActive(false);
                preparationWindow.SetActive(true);

                //「マップ確認」ボタンにフォーカス
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(preparationMenuWindow.transform.Find("SearchButton").gameObject);
            }

            //200816 既にマップメニュー表示中の時は閉じる
            else if (mapMode == MapMode.MAP_MENU)
            {

                mapMode = MapMode.NORMAL;
                mapMenuView.SetActive(false);
            }

            //出撃準備時に押されたらステージ選択に戻る
            else if (mapMode == MapMode.PREPARATION)
            {
                Retire();
            }

            else if (mapMode == MapMode.UNIT_SELECT)
            {
                //ユニット選択時
                preparationMenuWindow.SetActive(true);
                stageDetailWindow.SetActive(true);
                unitSelectView.SetActive(false);
                unitOutlineWindow.gameObject.SetActive(false);
                mapMode = MapMode.PREPARATION;
                Debug.Log($"mapMode:{mapMode}");
                EventSystem.current.SetSelectedGameObject(preparationMenuWindow.transform.Find("BattleStartButton").gameObject);

            }
            else if (mapMode == MapMode.WEAPON_SELECT || mapMode == MapMode.HEAL_SELECT || mapMode == MapMode.ITEM_SELECT)
            {
                //武器ボタンを削除する
                battleManager.DeleteButtleWeaponButton();

                //アイテムボタンをすべて削除する
                DeleteWeaponWindowItem();

                mapMode = MapMode.MOVEDMENU;
                battleManager.CloseWeaponWindow();
                itemDetailWindow.SetActive(false);
                movedMenuWindow.SetActive(true);
                EventSystem.current.SetSelectedGameObject(selectedMovedMenuItem);
            }
            else if (mapMode == MapMode.ATTACK_SELECT || mapMode == MapMode.HEAL_TARGET_SELECT)
            {
                //攻撃対象選択時

                //セルのハイライトを消す
                mainMap.ResetAttackableCells();
                //モードを武器選択へ
                if (mapMode == MapMode.ATTACK_SELECT)
                {
                    mapMode = MapMode.WEAPON_SELECT;
                }
                else
                {
                    //回復の杖の場合
                    mapMode = MapMode.HEAL_SELECT;
                }

                weaponWindow.SetActive(true);
                detailWindow.SetActive(true);

                //一度nullにしないでWindowを表示非表示すると選択済みでなくなるっぽい
                //選択したボタンへフォーカスを戻す
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(selectedItem);
            }
            else if (mapMode == MapMode.BATTLE_CONFIRM)
            {
                //戦闘開始の確認画面の時
                //ウィンドウを閉じてモードをATTACK_SELECTへ
                battleManager.MapCloseBattleView();

                //再度攻撃可能範囲を表示
                if (isAttackConfirm)
                {

                    if (mainMap.ActiveUnit.unit.equipWeapon != null)
                    {
                        Attack(mainMap.ActiveUnit.unit.equipWeapon);
                    }
                }
                else
                {
                    //杖の場合
                    if (mainMap.ActiveUnit.unit.equipHealRod == null)
                    {
                        Debug.Log("ERROR : 回復の杖を持っていません");
                    }
                    Attack(mainMap.ActiveUnit.unit.equipHealRod);
                }
            }
            else if (mapMode == MapMode.ITEM_USE_CONFIRM)
            {
                //210217 道具の使用確認の時
                //モードを道具選択に
                mapMode = MapMode.ITEM_SELECT;

                //非表示に
                useItemConfirmWindow.SetActive(false);

                //道具選択ウィンドウ表示
                weaponWindow.SetActive(true);
                itemDetailWindow.SetActive(true);

                //選択していたオブジェクトにフォーカス
                EventSystem.current.SetSelectedGameObject(selectedItem);
            }

            //移動範囲表示中の場合
            else if (mapMode == MapMode.MOVE)
            {

                //とりあえず、移動可能なマスを消す
                mainMap.ResetMovableCells();
                mainMap.ResetAttackableCells();
                mapMode = MapMode.NORMAL;
            }

            //移動後のメニュー表示中の場合
            else if (mapMode == MapMode.MOVEDMENU)
            {

                //アニメーション無しで瞬間移動させてNORMALへ
                mainMap.ActiveUnit.ReturnTo(beforeCell);

                //210221 敵の攻撃可能範囲を再計算
                mainMap.CancelReloadHighLightCells();

                movedMenuWindow.SetActive(false);
                mapMode = MapMode.NORMAL;
                EventSystem.current.SetSelectedGameObject(null);
            }
            else if (mapMode == MapMode.STATUS)
            {
                //ステータス画面表示中
                //無限に増えないように作ったボタンを削除
                statusWindow.GetComponent<StatusWindow>().DeleteSkillButtonAndItemButton();

                //モードを自軍ターンへ移動して何も選択しない
                statusWindow.SetActive(false);
                mapMode = returnMapMode;
                EventSystem.current.SetSelectedGameObject(null);
            }

            else if (mapMode == MapMode.ITEM_EQUIP_USE_MENU)
            {
                //アイテムの装備、使うメニューを表示している時
                //モードをアイテム選択へ
                mapMode = MapMode.ITEM_SELECT;
                Debug.Log($"mapMode:{mapMode}");

                //アイテムを「預ける」、「装備」ウィンドウを表示している時
                //ウィンドウを閉じる
                ItemUseEquipWindow.SetActive(false);

                //ボタンを活性化
                foreach (Transform weaponButton in weaponWindow.transform)
                {
                    weaponButton.GetComponent<Button>().interactable = true;
                }

                //フォーカスを元のアイテムボタンへ戻す
                EventSystem.current.SetSelectedGameObject(selectedItem);
            }
        }
    }

    /// <summary>
    /// カーソルなどを動かす
    /// </summary>
    private void GetControll()
    {
        //操作系は画面の暗転中は行わない
        if (fadeInOutManager.isFadeinFadeout())
        {
            return;
        }

        //メッセージ表示中はカーソル移動不可
        if (isMessaseExist)
        {
            return;
        }

        //準備、ユニット選択、移動後のメニューでは操作しない
        //210210 MapModeにてカーソル操作可否を制御
        if (!mapMode.isControllAble())
        {
            return;
        }

        //エディットモードのメニュー表示中はカーソルを移動出来ない
        if (mapEditManager.isEditorMode)
        {
            if(mapEditManager.editMode != EditMode.CELL_EDIT)
            {
                return;
            }
        }

        //ユニット移動中は入力を受け付けない
        if (isMoving)
        {
            return;
        }

        if (ControllDeltaTime <= CURSOR_MOVE_WAIT)
        {
            return;
        }
        ControllDeltaTime = 0;

        //カーソル移動
        if (Input.GetAxis("Vertical") > 0)
        {
            //上
            //カーソルのセルへのスナップをキャンセル
            battleMapCursor.transform.DOKill();

            //210211 セルが無い所までカーソルが移動するのを防ぐ処理を追加
            //カーソルの現在位置を切り捨てた値 + 1右のセルが有るか確認
            cell = mainMap.Cells.FirstOrDefault(c => c.X == cursorPos.x && c.Y == Mathf.FloorToInt(battleMapCursor.transform.position.z) + 1);
            if (cell == null)
            {
                battleMapCursor.transform.position = new Vector3(battleMapCursor.transform.position.x, battleMapCursor.transform.position.y,
                    Mathf.FloorToInt(battleMapCursor.transform.position.z));
                return;
            }

            //高さの変更
            //移動先の高さを取得
            terrainHight = getTerrainHight(battleMapCursor.transform.position.x, battleMapCursor.transform.position.z + cursorMoveSpeed);
            terrainHightDif = terrainHight - battleMapCursor.transform.position.y + cursorAdjust;
            //x,z座標の移動
            battleMapCursor.transform.Translate(0, terrainHightDif, cursorMoveSpeed);

            //カーソルのスナップ調整
            isYMovePositive = true;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            //下
            battleMapCursor.transform.DOKill();

            cell = mainMap.Cells.FirstOrDefault(c => c.X == cursorPos.x && c.Y == Mathf.CeilToInt(battleMapCursor.transform.position.z) - 1);
            if (cell == null)
            {
                battleMapCursor.transform.position = new Vector3(battleMapCursor.transform.position.x, battleMapCursor.transform.position.y,
                    Mathf.CeilToInt(battleMapCursor.transform.position.z));
                return;
            }

            terrainHight = getTerrainHight(battleMapCursor.transform.position.x, battleMapCursor.transform.position.z - cursorMoveSpeed);
            terrainHightDif = terrainHight - battleMapCursor.transform.position.y + cursorAdjust;

            battleMapCursor.transform.Translate(0, terrainHightDif, -cursorMoveSpeed);

            isYMovePositive = false;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            //右
            battleMapCursor.transform.DOKill();

            //210211 カーソル停止処理
            cell = mainMap.Cells.FirstOrDefault(c => c.X == Mathf.FloorToInt(battleMapCursor.transform.position.x) + 1 && c.Y == cursorPos.z);
            if (cell == null)
            {
                battleMapCursor.transform.position = new Vector3(Mathf.FloorToInt(battleMapCursor.transform.position.x), battleMapCursor.transform.position.y,
                    battleMapCursor.transform.position.z);
                return;
            }

            terrainHight = getTerrainHight(battleMapCursor.transform.position.x + cursorMoveSpeed, battleMapCursor.transform.position.z);
            terrainHightDif = terrainHight - battleMapCursor.transform.position.y + cursorAdjust;

            battleMapCursor.transform.Translate(cursorMoveSpeed, terrainHightDif, 0);

            isXMovePositive = true;
        }
        
        if (Input.GetAxis("Horizontal") < 0)
        {
            //左
            battleMapCursor.transform.DOKill();

            //210211 カーソル停止処理
            cell = mainMap.Cells.FirstOrDefault(c => c.X == Mathf.CeilToInt(battleMapCursor.transform.position.x) - 1 && c.Y == cursorPos.z);
            if (cell == null)
            {
                battleMapCursor.transform.position = new Vector3(Mathf.CeilToInt(battleMapCursor.transform.position.x), battleMapCursor.transform.position.y,
                    battleMapCursor.transform.position.z);
                return;
            }

            terrainHight = getTerrainHight(battleMapCursor.transform.position.x - cursorMoveSpeed, battleMapCursor.transform.position.z);
            terrainHightDif = terrainHight - battleMapCursor.transform.position.y + cursorAdjust;

            battleMapCursor.transform.Translate(-cursorMoveSpeed, terrainHightDif, 0);

            isXMovePositive = false;
        }

        //ここから何も移動キーを操作していない時

        //210220 攻撃対象へのカーソル自動移動
        if(isCursorMove)
        {
            return;
        }

        SnapCursor();
    }

    //カーソルの位置を四捨五入する
    public void SnapCursor()
    {
        //移動キーを離したら、座標が小数点以下なので整数にスナップさせる
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            battleMapCursor.transform.DOMove(RoundPos(battleMapCursor.transform.position.x,
                battleMapCursor.transform.position.y,
                battleMapCursor.transform.position.z), 0.3f);
        }
    }

    //移動キーボタン押下中にボタンを押すとバグる件対応　キー押下中はボタン操作させない
    private void checkMoveKeyDown()
    {
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0){
            isMoveKeyDown = true;
        }
        else
        {
            isMoveKeyDown = false;
        }
    }

    //カメラの距離を切り替える
    private void AdjustCamela()
    {
        //カメラの距離切り替えボタン
        if (Input.GetButtonDown("Zoom") || KeyConfigManager.GetKeyDown(KeyConfigType.ZOOM))
        {
            //true、false切り替え
            isCloseCamera = isCloseCamera ? false : true;
            //最終的なカメラの値を決める
            yAdjustTarget = isCloseCamera ? 4 : 8;
            zAdjustTarget = isCloseCamera ? -4 : -8;
            isCameraAdjusting = true;
        }
    }

    /// <summary>
    /// カメラの位置を更新する
    /// </summary>
    void cameraUpdate()
    {
        //カメラはカーソルと同じ位置にする Yはそのまま
        mainCamera.transform.position =
            new Vector3(battleMapCursor.transform.position.x, battleMapCursor.transform.position.y + yAdjust, 
            (battleMapCursor.transform.position.z + zAdjust));

        if (isCameraAdjusting)
        {
            if (yAdjust > yAdjustTarget)
            {
                yAdjust -= adjustSpeed;
            }
            else if (yAdjust < yAdjustTarget)
            {
                yAdjust += adjustSpeed;
            }

            if (zAdjust > zAdjustTarget)
            {
                zAdjust -= adjustSpeed;
            }
            else if (zAdjust < zAdjustTarget)
            {
                zAdjust += adjustSpeed;
            }

            //最終的な値と現在の値が同じになれば調整中をfalse
            //微妙にTerrainの値で変わってしまうので四捨五入
            if (Mathf.RoundToInt(yAdjust) == yAdjustTarget && Mathf.RoundToInt(zAdjust) == zAdjustTarget)
            {
                isCameraAdjusting = false;
            }

        }
    }

    /// <summary>
    /// 210302エディタモードでなければカメラを見下ろした時真ん中に来るように調整
    /// </summary>
    /// <param name="editorMode"></param>
    public void setCamelaEditMode(bool editorMode)
    {
        Transform cursor = battleMapCursor.transform.Find("Cursor");
        if (editorMode)
        {
            //カメラを見下ろしへ
            mainCamera.transform.DORotate(new Vector3(90, 0, 0), 1.0f);

            cursor.position = new Vector3(0, 1, 0);
        }
        else
        {
            mainCamera.transform.DORotate(new Vector3(45, 0, 0), 1.0f);
            cursor.position = new Vector3(0, 1, -3.5f);
        }
        //カメラの位置調整
        zAdjust = (editorMode) ? 0 : -4;
    }

    public Vector3 GetCursorPos()
    {
        return cursorPos;
    }
}

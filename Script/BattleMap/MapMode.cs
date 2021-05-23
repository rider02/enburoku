/// <summary>
/// 200725 戦闘シーンのモード
/// </summary>
public enum MapMode
{

    [StringValue("出撃準備")]
    [ControllAble(false)]   //十字キーの操作でカーソルが移動出来るか
    [MenuVisible(true)]     //メニュー(uGUI)が表示されているか uGUIの制御用
    PREPARATION,

    [StringValue("出撃ユニット選択")]
    [ControllAble(false)]
    [MenuVisible(true)]
    UNIT_SELECT,

    [StringValue("マップ確認")]
    [ControllAble(true)]
    [MenuVisible(false)]
    MAP_VIEW,

    [StringValue("自軍ターン開始")]
    [ControllAble(false)]
    [MenuVisible(false)]
    TURN_START,

    [StringValue("自軍ターン")]
    [ControllAble(true)]
    [MenuVisible(false)]
    NORMAL,

    [StringValue("マップメニュー")]
    [ControllAble(false)]
    [MenuVisible(true)]
    MAP_MENU,

    [StringValue("ステータス")]
    [ControllAble(false)]
    [MenuVisible(false)]
    STATUS,

    [StringValue("持ち物")]
    [ControllAble(false)]
    [MenuVisible(true)]
    PROPERTY,

    [StringValue("物交換")]
    [ControllAble(false)]
    [MenuVisible(true)]
    EXCHANGE,

    [StringValue("移動可能範囲表示")]
    [ControllAble(true)]
    [MenuVisible(false)]
    MOVE,

    [StringValue("武器選択")]
    [ControllAble(false)]
    [MenuVisible(true)]
    WEAPON_SELECT,

    [StringValue("道具選択")]
    [ControllAble(false)]
    [MenuVisible(true)]
    ITEM_SELECT,

    [StringValue("道具の装備か使用を選択")]
    [ControllAble(false)]
    [MenuVisible(true)]
    ITEM_EQUIP_USE_MENU,

    [StringValue("道具使用確認")]
    [ControllAble(false)]
    [MenuVisible(true)]
    ITEM_USE_CONFIRM,

    [StringValue("敵選択")]
    [ControllAble(true)]
    [MenuVisible(false)]
    ATTACK_SELECT,

    [StringValue("回復の符選択")]
    [ControllAble(false)]
    [MenuVisible(true)]
    HEAL_SELECT,

    [StringValue("回復対象選択")]
    [ControllAble(true)]
    [MenuVisible(false)]
    HEAL_TARGET_SELECT,

    [StringValue("戦闘開始確認")]
    [ControllAble(false)]
    [MenuVisible(true)]
    BATTLE_CONFIRM,

    [StringValue("戦闘中")]
    [ControllAble(false)]
    [MenuVisible(false)]
    BATTLE,

    [StringValue("敵軍ターン開始")]
    [ControllAble(false)]
    [MenuVisible(false)]
    ENEMY_TURN_START,

    [StringValue("敵軍ターン")]
    [ControllAble(false)]
    [MenuVisible(false)]
    ENEMY_TURN,

    [StringValue("敵の移動中")]
    [ControllAble(false)]
    [MenuVisible(false)]
    ENEMY_MOVE,

    [StringValue("移動後メニュー表示")]
    [ControllAble(false)]
    [MenuVisible(true)]
    MOVEDMENU,

    [StringValue("レベルアップ")]
    [ControllAble(false)]
    [MenuVisible(false)]
    LVUP,

    [StringValue("開始前会話")]
    [ControllAble(false)]
    [MenuVisible(false)]
    START_TALK,

    [StringValue("会話")]
    [ControllAble(false)]
    [MenuVisible(false)]
    TALK,

    //宝箱を開けた場合などに遷移する
    [StringValue("メッセージ")]
    [ControllAble(false)]
    [MenuVisible(true)]
    MESSAGE,

    [StringValue("ターン開始会話")]
    [ControllAble(false)]
    [MenuVisible(false)]
    TURN_START_TALK,

    [StringValue("戦闘前会話")]
    [ControllAble(false)]
    [MenuVisible(false)]
    BATTLE_BEFORE_TALK,

    [StringValue("戦闘前会話")]
    [ControllAble(false)]
    [MenuVisible(false)]
    BATTLE_AFTER_TALK,

    [StringValue("退却確認")]
    [ControllAble(false)]
    [MenuVisible(true)]
    RETREAT_CONFIRM,

    [StringValue("ステージクリア")]
    [ControllAble(false)]
    [MenuVisible(false)]
    STAGE_CLEAR,

    [StringValue("カーソル移動中")]
    [ControllAble(false)]
    [MenuVisible(false)]
    CURSOR_MOVE,

    [StringValue("セル編集モード")]
    [ControllAble(true)]
    [MenuVisible(false)]
    EDIT,

    [StringValue("ゲームオーバー")]
    [ControllAble(false)]
    [MenuVisible(true)]
    GAME_OVER,

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステータス画面の状態列挙型
/// </summary>
public enum MenuMode
{
    [StringValue("出撃準備")]
    ROOT,

    [StringValue("ステータス")]
    STATUS,

    [StringValue("ステータス")]
    STATUS_BROWSE,

    [StringValue("レベルアップ")]
    LVUP,
    [StringValue("レベルアップ表示テスト")]
    LVUP_EFFECT,

    [StringValue("レベルアップ終了")]
    LVUP_END,

    [StringValue("装備")]
    EQUIP,
    [StringValue("支援会話")]
    TALK,
    [StringValue("クラスチェンジ")]
    CLASSCHANGE,
    [StringValue("セーブ")]
    SAVE,
    [StringValue("ロード")]
    LOAD,

    //ユニット性能、ゲームのルール等のカテゴリ
    [StringValue("チュートリアル")]
    TUTORIAL_CATEGORY_SELECT,

    //チュートリアル内容の選択
    [StringValue("チュートリアル")]
    TUTORIAL_SELECT,

    //チュートリアルの内容を表示
    [StringValue("チュートリアル")]
    TUTORIAL,

    //210221 やっとアイテム交換系の機能を追加
    //アイテムの「持ち物」、「交換」、「装備」を開きたいユニットを選択
    [StringValue("持ち物")]
    ITEM_UNIT_SELECT,

    //アイテムの「持ち物」「交換」「全預け」を開いている状態
    [StringValue("持ち物")]
    ITEM_MENU,

    //「持ち物」を押してユニットのアイテムにフォーカス
    [StringValue("持ち物")]
    ITEM,

    //隙間のアイテムにフォーカスしている状態
    [StringValue("持ち物")]
    RECEIVE_ITEM,

    //アイテムの「預ける」「使う」「装備」を開いている状態
    [StringValue("持ち物")]
    ITEM_DEPOSIT_MENU,

    [StringValue("持ち物交換")]
    ITEM_EXCHANGE_TARGET_SELECT,
    
    //アイテムの交換で、アイテム1めを選択する状態
    [StringValue("持ち物交換")]
    ITEM_EXCHANGE,

    //既に交換するアイテムを1つ選択して、2つめのアイテムを選択する場合
    [StringValue("持ち物交換")]
    EXCHANGE_ITEM_SELECT
}

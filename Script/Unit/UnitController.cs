﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitController : MonoBehaviour
{

    //仲間のユニットリスト
    public static List<Unit> unitList { get; set; }

    private GameObject partyWindow;

    public static bool isInit;

    private BattleMapManager battleMapManager;

    public void initUnitList(UnitDatabase unitDatabase)
    {
        //実際は仲間になったタイミングで入れていく
        //200830 ルートを設定
        if (ModeManager.route == Route.REIMU)
        {
            unitList = unitDatabase.unitList.FindAll(unit => unit.isReimuRoute == true);
        }
        else
        {
            unitList = unitDatabase.unitList.FindAll(unit => unit.isReimuRoute == false);
        }

        isInit = true;
    }

    /// <summary>
    /// PartyWindowのボタン一覧を作る初期化メソッド
    /// </summary>
    public void initPartyList(StatusManager statusManager)
    {

        foreach (var unit in unitList)
        {

            //Resources配下からボタンをロード
            var itemButton = (Instantiate(Resources.Load("Prefabs/UnitButton")) as GameObject).transform;
            //ボタン初期化 210221 遂にユニットの画像をボタンに表示するよう改修
            itemButton.GetComponent<UnitButton>().Init(unit.name, statusManager, unit.pathName);
            itemButton.name = itemButton.name.Replace("(Clone)", "");

            //partyWindowオブジェクトをを探して取得
            partyWindow = GameObject.Find("PartyWindow");

            //partyWindowオブジェクト配下にprefab作成
            itemButton.transform.SetParent(partyWindow.transform,false);


        }
        //非表示にする
        partyWindow.SetActive(false);
    }

    /// <summary>
    /// BattleシーンでPartyWindowのボタン一覧を作る初期化メソッド
    /// 引数の違いを処理するのが難しいので2つ作ってしまう
    /// </summary>
    public void initBattlePartyList(BattleManager battleManager)
    {

        foreach (var unit in unitList)
        {

            //Resources配下からボタンをロード
            var itemButton = (Instantiate(Resources.Load("Prefabs/BattleUnitButton")) as GameObject).transform;
            //ボタン初期化 今はテキストのみ
            itemButton.GetComponent<BattleUnitButton>().Init(unit.name, battleManager);
            itemButton.name = itemButton.name.Replace("(Clone)", "");

            //partyWindowオブジェクトをを探して取得
            partyWindow = GameObject.Find("PartyWindow");

            //partyWindowオブジェクト配下にprefab作成
            itemButton.transform.SetParent(partyWindow.transform);


        }
    }

    //200809 先頭に参加するユニット一覧を作成する
    public void InitPreparePartyList(BattleMapManager battleMapManager, GameObject unitSelectView)
    {
        int count = 0;
        foreach (var unit in unitList)
        {
            //Resources配下からボタンをロード
            var unitButton = (Instantiate(Resources.Load("Prefabs/PrepareUnitButton")) as GameObject).transform;
            unitButton.name = unitButton.name.Replace("(Clone)", "");
            //partyWindowオブジェクト配下にprefab作成
            unitButton.transform.SetParent(unitSelectView.transform.Find("UnitSelectWindow"));
            //ボタン初期化
            unitButton.GetComponent<PrepareUnitButton>().Init(unit.name, battleMapManager);

            //出撃人数未満なら出撃リストに追加
            if (count < battleMapManager.stage.entryUnitCount) { 
                
                //出撃ユニット一覧に名前を登録
                battleMapManager.entryUnitNameList.Add(unit.name);
                unitButton.GetComponent<PrepareUnitButton>().setEntry();
            }
            else
            {
                //出撃人数以上の場合はボタンを無効化
                unitButton.GetComponent<PrepareUnitButton>().SetDisable();
                unitButton.GetComponent<PrepareUnitButton>().removeEntry();

            }
            count++;
        }
    }

    /// <summary>
    /// 名前で検索してユニットを返す
    /// </summary>
    /// <param name="unitName"></param>
    /// <returns></returns>
    public Unit findByName(string unitName)
    {
        foreach(var unit in unitList)
        {
            if (unit.name == unitName)
            return unit;
        }
        return null;
    }

    /// <summary>
    /// ユニットにアイテムを持たせるメソッド
    /// </summary>
    /// <param name="unitName"></param>
    /// <param name="item"></param>
    public void setItem(string unitName,Item item)
    {
        foreach (var unit in unitList)
        {
            if (unit.name == unitName)
                unit.carryItem.Add(item);
        }

    }

    public void updateUnit(Unit updatedUnit)
    {
        var tmpList = new List<Unit>();

        foreach (var unit in unitList)
        {
            
            //更新ユニットと名前が一致したら更新済みユニットを代わりに入れる
            if (unit.name == updatedUnit.name)
            {
                tmpList.Add(updatedUnit);
            }
            else
            {
                //関係のないユニットはそのまま
                tmpList.Add(unit);
            }
        }

        unitList = tmpList;
    }

    /// <summary>
    /// 200712 ユニットの体力回復
    /// </summary>
    public void rest()
    {
        var tmpList = new List<Unit>();
        StatusCalculator statusCalc = new StatusCalculator();

        foreach (var unit in unitList)
        {
            //体力全回復 200719 職業補正を反映
            int maxHp = unit.maxhp + unit.job.statusDto.jobHp;
            maxHp = statusCalc.CalcHpBuff(maxHp, unit.job.skills);
            unit.hp = maxHp;
            tmpList.Add(unit);
        }

        unitList = tmpList;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Battleシーンで作成 戦闘テスト用で本番では使用しないクラス
/// </summary>
public class EnemyController : MonoBehaviour
{
    private GameObject enemyWindow;

    public void initEnemyList(BattleManager battleManager,EnemyDatabase enemyDatabase)
    {

        foreach (var enemy in enemyDatabase.enemyList)
        {

            //Resources配下からボタンをロード
            var itemButton = (Instantiate(Resources.Load("Prefabs/BattleEnemyButton")) as GameObject).transform;
            //ボタン初期化 今はテキストのみ
            itemButton.GetComponent<BattleEnemyButton>().Init(enemy.name, battleManager);
            itemButton.name = itemButton.name.Replace("(Clone)", "");

            //partyWindowオブジェクトをを探して取得
            enemyWindow = GameObject.Find("EnemyWindow");

            //partyWindowオブジェクト配下にprefab作成
            itemButton.transform.SetParent(enemyWindow.transform);


        }
        //非表示にする
        enemyWindow.SetActive(false);
    }

}

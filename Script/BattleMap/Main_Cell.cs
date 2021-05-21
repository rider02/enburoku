using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 座標 移動可能だと青色になる
/// </summary>

//[RequireComponent(typeof(Button))]
public class Main_Cell : MonoBehaviour
{
    [SerializeField] Main_Map map;
    [SerializeField] BattleMapManager battleMapManager;
    [SerializeField] GameObject highlight;
    [SerializeField] GameObject attackAbleHighlight;
    [SerializeField] GameObject healAbleHighlight;
    [SerializeField] public TextMeshPro typeText;

    //210221 警戒範囲を追加
    [SerializeField] GameObject warningHighlight;

    //移動コスト
    [SerializeField]
    int cost;
    
    Terrain terrain;
    //カーソルの高さがTerrainと同一だと埋まってしまう
    float cellHightAdjust = 0.03f;

    //座標
    int x;
    int y;

    //セルの名前 森、平地とか
    CellType type;

    //セルの名前    
    string typeName;

    //セルの回避率
    int avoidRate;

    //セルの防御力
    int defense;

    //210302 壁など、移動出来ないセルの場合
    public bool isBlock;

    //この攻撃範囲を表示した敵 重複を防ぐ為HashSet
    public HashSet<int> highLightEnemyidSet = new HashSet<int>();

    /// <summary>
    /// 移動可能なマスかどうか
    /// </summary>
    /// <value><c>true</c> if this instance is movable; otherwise, <c>false</c>.</value>
    public bool IsMovable
    {
        // 移動可能ならハイライトを表示させる
        set { highlight.gameObject.SetActive(value); }
        //自分がアクティブかどうか返す
        get { return highlight.gameObject.activeSelf; }
    }

    //200813 攻撃出来るかどうか
    public bool IsAttackable
    {
        // 移動可能ならハイライトを表示させる
        set { attackAbleHighlight.gameObject.SetActive(value); }
        //自分がアクティブかどうか返す
        get { return attackAbleHighlight.gameObject.activeSelf; }
    }

    public bool IsHealable
    {
        // 移動可能ならハイライトを表示させる
        set { healAbleHighlight.gameObject.SetActive(value); }
        //自分がアクティブかどうか返す
        get { return healAbleHighlight.gameObject.activeSelf; }
    }

    //210221 敵の攻撃範囲を取得
    public bool IsWarning
    {
        // 移動可能ならハイライトを表示させる
        set { warningHighlight.gameObject.SetActive(value); }
        //自分がアクティブかどうか返す
        get { return warningHighlight.gameObject.activeSelf; }
    }

    public int Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    public int X
    {
        get { return x; }
    }

    public int Y
    {
        get { return y; }
    }

    public CellType Type
    {
        get { return type; }
        set { type = value; }
    }

    public string TypeName
    {
        get { return typeName; }
        set { typeName = value; }
    }

    public int AvoidRate
    {
        get { return avoidRate; }
        set { avoidRate = value; }
    }

    public int Defence
    {
        get { return defense; }
        set { defense = value; }
    }

    /// <summary>
    /// 210221 警戒範囲の設定 警戒
    /// </summary>
    /// <param name="enemyId"></param>
    public void SetIsWarning(int enemyId)
    {
        //問題は消す時なのでハイライトする分には何も考えずハイライトする
        IsWarning = true;
        highLightEnemyidSet.Add(enemyId);
    }

    /// <summary>
    /// 警戒範囲表示セットから指定ユニットのIDを消す
    /// </summary>
    /// <param name="enemyId"></param>
    public void RemoveIsWarning(int enemyId)
    {
        highLightEnemyidSet.Remove(enemyId);

        //全ての敵ユニットのIDがセルから除去された時点でハイライトを消す
        if(highLightEnemyidSet.Count == 0)
        {
            IsWarning = false;
        }
    }

    /// <summary>
    /// 座標をセットします
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public void SetCoordinate(int x, int y, CellType cellType)
    {
        //座標をセットして敷き詰める
        this.x = x;
        this.y = y;

        this.transform.position =
            new Vector3(x, 0, y);

        //Terrainの高さを取得してその上に表示する
        terrain = Terrain.activeTerrain;

        float terrainHight;
        if (cellType == CellType.WATER)
        {
            //とりあえず仮で2
            terrainHight = 2;
        }
        else
        {
            terrainHight = getTerrainHight(
            this.transform.position.x, this.transform.position.z);
        }


        //2DはGrid Layout Groupを使ってたけど、座標を指定しないといけない
        this.transform.position=
            new Vector3(x, terrainHight + cellHightAdjust, y);
    }

    //200802 enumから地形効果を設定する
    public void SetType(CellType cellType)
    {
        this.type = cellType;

        //セルの種類に基づく各種値を設定
        if (cellType == CellType.Block)
        {
            this.cost = 99;
            this.typeName = "壁";
            this.avoidRate = 0;
            this.Defence = 0;
            this.isBlock = true;
        }
        else if (cellType == CellType.Tree)
        {
            this.cost = 2;
            this.typeName = "茂み";
            this.avoidRate = 15;
            this.Defence = 1;
            this.isBlock = false;
        }
        else if (cellType == CellType.Field)
        {
            this.cost = 1;
            this.typeName = "平地";
            this.avoidRate = 0;
            this.Defence = 0;
            this.isBlock = false;
        }
        else if (cellType == CellType.Forest)
        {
            this.cost = 2;
            this.typeName = "森";
            this.avoidRate = 20;
            this.Defence = 1;
            this.isBlock = false;
        }
        else if (cellType == CellType.JINJA)
        {
            this.cost = 99;
            this.typeName = "社殿";
            this.avoidRate = 0;
            this.Defence = 0;
            this.isBlock = true;
        }
        else if (cellType == CellType.LANTERN)
        {
            this.cost = 99;
            this.typeName = "石灯篭";
            this.avoidRate = 0;
            this.Defence = 0;
            this.isBlock = true;
        }
        else if (cellType == CellType.WATER)
        {
            this.cost = 1;
            this.typeName = "水面";
            this.avoidRate = 0;
            this.Defence = 0;
            this.isBlock = false;
        }
        else if (cellType == CellType.DEEP_FOREST)
        {
            this.cost = 99;
            this.typeName = "深い森";
            this.avoidRate = 0;
            this.Defence = 0;
            this.isBlock = true;
        }

        //テキスト更新
        UpdateTypeText(typeName , cellType);
    }

    //200802_デバッグ用の表示を更新する
    private void UpdateTypeText(string typeName, CellType type)
    {
        typeText.text = typeName;

        //基本は白
        Color textColor = Color.white;

        if(type == CellType.Forest)
        {
            textColor = Color.green;
        }
        else if (type == CellType.Tree)
        {
            textColor = Color.yellow;
        }
        else if (type == CellType.Block)
        {
            textColor = Color.gray;
        }

        typeText.color = textColor;
    }

    float getTerrainHight(float x, float z)
    {
        //terrainの座標を取得するおまじない
        return terrain.terrainData.GetInterpolatedHeight((x - terrain.transform.position.x) / terrain.terrainData.size.x, (z - terrain.transform.position.z) / terrain.terrainData.size.z);
    }
}

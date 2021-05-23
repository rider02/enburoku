using System.Collections.Generic;

/// <summary>
/// テキストを読み込んでシーンを1行ずつstring型のリストとして保持するクラス
/// SceneHolderクラスのList<Scene> Scenesに格納されて使用する
/// </summary>
public class Scene
{
    //#scene=001なら001を取得する
    public string ID { get; private set; }

    //そのシーンのテキストをリストとして保持する
    public List<string> Lines { get; private set; } = new List<string>();
    public int Index { get; private set; } = 0;

    //コンストラクタ
    public Scene(string ID = "")
    {
        this.ID = ID;
    }

    //sceneControllerのSetSceneから呼ばれる
    public Scene Clone()
    {
        //Indexを初期化してインスタンス作り直し
        return new Scene(ID)
        {
            Index = 0,
            Lines = new List<string>(Lines)
        };
    }

    //最後まで読んだか 行数がLinesリストのサイズ以上なら終わり
    public bool IsFinished()
    {
        return Index >= Lines.Count;
    }

    //現在の行数のテキストを取得
    public string GetCurrentLine()
    {
        return Lines[Index];
    }

    //SceneReaderから呼ばれる 次の行へ
    public void GoNextLine()
    {
        Index++;
    }
}
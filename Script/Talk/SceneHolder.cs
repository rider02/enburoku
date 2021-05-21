using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// 初期化時にtxtを読み込んで、#Sceneを元にScene型の配列を作るクラス
/// 全メソッド初期化後はもうScenes以外呼ばれる事は無い＾＾
/// </summary>
public class SceneHolder
{
    //テキストから読み込んだSceneのリスト
    //ScenController.SetSceneから呼ばれる
    public List<Scene> Scenes = new List<Scene>();

    //コンストラクタ
    public SceneHolder(string scenarioPath)
    {
        Load(scenarioPath);
    }

    //txt読み込み コンストラクタから呼ばれるのみ
    public void Load(string scenarioPath)
    {   //  Resouces/CSV下のtxt読み込み TextAsset形式でtxtを全て取得
        //LoadはObject型を返すので型変換が必要
        var itemFile = Resources.Load(scenarioPath) as TextAsset;
        //1行ずつ取得してList<string>形式で受け取る
        List<string> csvData = LoadCSV(itemFile);

        //パースしてString型のリストをscene型のリストに変換
        Scenes = Parse(csvData);
    }


    //txt読み込み コンストラクタから呼ばれるのみ
    public List<string> LoadCSV(TextAsset file)
    {

        StringReader reader = new StringReader(file.text);
        var list = new List<string>();

        //1行ずつtxt読み込んでList<string>に入れる。
        //Peekで末端まで繰り返す Peekはファイル末尾
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            list.Add(line);         }
        return list;
    }

    //String型のリストを受け取るとscene型のリストを返す
    public List<Scene> Parse(List<string> list)
    {
        var scenes = new List<Scene>();
        var scene = new Scene();
        foreach (string line in list)
        {
            //#sceneが含まれていたら
            if (line.Contains("#scene"))
            {
                //#sceneを消して数字だけにする
                var ID = line.Replace("#scene=", "");
                //scene型のインスタンスにしてリストに追加
                //Linesには追加しないので、Readerでは#sceneをパースしてない
                scene = new Scene(ID);
                scenes.Add(scene);
            }
            else
            {
                //#scene以外は現在のシーンに追加
                //LinesはString型のリスト
                scene.Lines.Add(line);
            }
        }
        //テキストを全てsceneに変換し終わったら返す
        return scenes;
    }

}

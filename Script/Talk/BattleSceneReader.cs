using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// テキストを1行1行読み込んで処理を行うパーサークラス
/// 戦闘シーンは処理が結構違うのでSceneReaderと分離・・・
/// </summary>
public class BattleSceneReader
{
    private BattleSceneController sceneController;
    private BattleTalkManager battleTalkManager;
    private Actions actions;

    //コンストラクタ
    public BattleSceneReader(BattleSceneController sceneController, BattleTalkManager battleTalkManager)
    {
        //SceneController、actionsを参照させる
        this.sceneController = sceneController;
        this.battleTalkManager = battleTalkManager;

        actions = sceneController.Actions;
    }


    /// <summary>
    /// 画面をクリックされた時、文字を表示中でなければ呼ばれる
    /// Sceneから行を読み込んで処理する
    /// SceneControllerのSetNextProcessから呼ばれる
    /// #speaker : 名前変更
    /// #next : 次のシーンへ
    /// #chara : 立ち絵追加
    /// #image_hiroko=smile : Resourses配下の立ち絵切り替え
    /// #options : 選択肢を作成
    /// </summary>
    /// <param name="s"></param>
    public void ReadLines(Scene scene)
    {
        //インデックスがSceneの行数以上なら何もしない
        if (scene.Index >= scene.Lines.Count) return;

        //現在の行の文字をString型で取得する
        string line = scene.GetCurrentLine();
        string text = "";

        //200613 コメント行を実装
        if (line.Contains("//"))
        {
            //シーンのインデックスを++
            scene.GoNextLine();
            //現在の行のテキストを取得する
            line = scene.GetCurrentLine();
        }

        //#で始まる行が有ればコマンド、SceneControllerの各メソッド呼び出し
        //sceneは読み込みの時点でSceneリストに入れていない為、存在しない
        if (line.Contains("#"))
        {
            //複数#が連発しても良いようにループさせる
            while (true)
            {
                //行に#が無くなればループ終了
                if (!line.Contains("#")) break;

                //まず#を消す
                line = line.Replace("#", "");

                //#speaker=主人公のように来た時 喋っている人の名前をセット
                //200616 _で区切ると立ち絵のハイライトが出来るように修正
                if (line.Contains("name"))
                {
                    //#speaker=を消す
                    line = line.Replace("name=", "");
                    var splitted = line.Split('_');
                    if(splitted.Length == 2)
                    {
                        sceneController.SetSpeaker(splitted[0], splitted[1]);
                    }
                    else
                    {
                        sceneController.SetSpeaker(splitted[0], null);
                    }
                    
                }
                //#chara=hiroko のように来た時 立ち絵を追加
                else if (line.Contains("chara"))
                {
                    line = line.Replace("chara=", "");
                    sceneController.AddCharactorBattleMap(line);    //これは戦闘シーンでは独自の処理
                }
                else if (line.Contains("leave"))
                {
                    //200614 キャラの退場を追加
                    line = line.Replace("leave=", "");
                    sceneController.LeaveCaracter(line);
                }
                else if (line.Contains("move"))
                {
                    //210515 キャラの移動
                    line = line.Replace("move=", "");
                    var splitted = line.Split('_');
                    sceneController.MoveCharactor(splitted[0], splitted[1]);
                }
                //#image_hiroko=aseri のように来た時 画像変更
                else if (line.Contains("image"))
                {
                    line = line.Replace("image_", "");
                    //=で分割して、第一引数が名前、第二引数が画像名
                    var splitted = line.Split('=');
                    sceneController.SetImage(splitted[0], splitted[1]);
                }
                //methodだった時
                else if (line.Contains("method"))
                {
                    line = line.Replace("method=", "");
                    var type = actions.GetType();
                    MethodInfo mi = type.GetMethod(line);
                    mi.Invoke(actions, new object[] { });
                }
                else if (line.Contains("end"))
                {
                    //キャラクターをクリアする
                    sceneController.CharactorClear();

                    //会話終了
                    battleTalkManager.TalkEnd();
                }

                //次の行を取得する為インデックス+1
                scene.GoNextLine();
                //終了していたらループ終了 Index >= Lines.Count;
                if (scene.IsFinished()) break;
                //次の行を取得
                line = scene.GetCurrentLine();
            }

        }//ここまで#で始まる特殊な処理

        //これは普通のテキストを表示する処理 {}で囲まれてる文字を表示
        if (line.Contains('{'))
        {
            //まず{を消す
            line = line.Replace("{", "");
            
            //何行も取得出来るよう「}」が有るまでループ
            while (true)
            {
                if (line.Contains('}'))
                {
                    line = line.Replace("}", "");
                    //表示用のstringに足していく
                    text += line;
                    //次の行へ
                    scene.GoNextLine();
                    break;
                }
                else
                {
                    /*"}"が無い場合は複数行に渡る台詞なので、
                     表示用テキストに追加して次の行へ*/
                    text += line;
                }
                //SceneクラスのIndexを++
                scene.GoNextLine();

                //シーンの最後の行まで読み込んでいたら終了
                if (scene.IsFinished()) break;
                //次の行のテキストを取得する
                line = scene.GetCurrentLine();
            }

            //StringReader.ReadLineでエスケープされた改行コードを復元
            text = text.Replace("\\n", "\n");

            //表示用のStringに渡した文字がnull空でなければ画面に文字をセット
            if (!string.IsNullOrEmpty(text)) sceneController.SetText(text);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

/// <summary>
/// 210522 立ち絵のクラス(戦闘マップ用)
/// </summary>
public class BattleCharacter : MonoBehaviour {

    private GameObject charactorObject;

    //キャラの立ち絵
    private Image charactorImage;

    //キャラの立ち絵一覧
    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    public string Name { get; private set; }
    
    public void Init(string name)
    {
        this.Name = name;
        charactorObject = gameObject;
        charactorImage = charactorObject.GetComponent<Image>();
        gameObject.SetActive(false);
        LoadImage();
    }

    
    //初期化した時に呼ばれる
    public void LoadImage()
    {
        //Resources配下のキャラクターの名前フォルダ以下の画像をリスト化する
        //キャラの立ち絵は名前のフォルダ配下にまとめておく
        var temp = Resources.LoadAll<Sprite>("Image/Charactors/"+Name).ToList();
        foreach (Sprite sprite in temp)
        {
            sprites.Add(sprite.name, sprite);
        }
    }

    ////SceneReaderで「#image_hiroko=aseri」等が有った時に
    //SceneControllerから呼ばれ、画像を設定する
    public void SetImage(string imageID)
    {
        //imageIdは「aseri」、「smile」とかそんな感じ
        charactorImage.sprite = sprites[imageID];
        //Rectの大きさを元画像と同じにする
        charactorImage.SetNativeSize();

        //200614 表情の変化はフェードさせたくない
        //FadeIn();

    }

    //フェードイン
    public void Appear()
    {
        charactorObject.SetActive(true);
        FadeIn();
    }

    //立ち絵をフェードアウトし、インスタンスも削除する
    public void Leave()
    {
        FadeOut();
    }

    //フェードアウトして自分自身を削除
    public void FadeOut()
    {
        //第一引数：透明度 第二引数：秒
        charactorImage.DOFade(0.0f, 0.2f);
        Destroy();
    }

    //立ち絵がフェードで表示される
    public void FadeIn()
    {
        //透明にしてからフェードインさせる
        charactorImage.color = new Color(1f, 1f, 1f, 0);
        charactorImage.DOFade(1.0f, 0.2f);
    }

    //会話しているキャラはグレーアウト解除して、最前面へ
    public void HighLight()
    {
        charactorImage.color = new Color(1f, 1f, 1f, 1);
        //charactorImage.sortingOrder = 2;
    }

    //会話していないキャラはグレーアウト
    public void GrayOut()
    {
        charactorImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
        //charactorImage.sortingOrder = 1;
    }

    public void Destroy()
    {
        Destroy(this);
    }


}

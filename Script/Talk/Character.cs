using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

//立ち絵のクラス　(会話シーン用)
public class Character : MonoBehaviour {

    private GameObject charactorObject;
    private SpriteRenderer charactorImage;
    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    private CanvasGroup canvasGroup;

    public string Name { get; private set; }

    public void Init(string name)
    {
        this.Name = name;
        charactorObject = gameObject;
        charactorImage = charactorObject.GetComponent<SpriteRenderer>();
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
            //DictionaryはMapのような物 Sprite型とか分からん
            sprites.Add(sprite.name, sprite);
        }
    }

    ////SceneReaderで「#image_hiroko=aseri」が有った時に
    //SceneControllerの199行目、LoadImageから呼ばれる
    public void SetImage(string imageID)
    {
        //imageIdは「aseri」、「smile」とかそんな感じ
        charactorImage.sprite = sprites[imageID];

        //200614 表情の変化はフェードさせたくない
        //FadeIn();

    }

    public void Appear()
    {
        charactorObject.SetActive(true);
        FadeIn();
    }

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
        charactorImage.sortingOrder = 2;
    }

    //会話していないキャラはグレーアウト
    public void GrayOut()
    {
        charactorImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
        charactorImage.sortingOrder = 1;
    }

    public void Destroy()
    {
        Destroy(this);
    }


}

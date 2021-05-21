using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FadeInOutManager : MonoBehaviour
{
    [SerializeField] BattleMapManager battleMapManager;

    //フェードアウト中
    public bool isFadeOut = false;

    //フェードイン中
    public bool isFadeIn = false;

    //フェードアウト後シーン変更する
    private bool isFadeoutToChangeScene = false;

    //フェードアウト後ゲーム終了する
    private bool isFadeoutToQuit = false;

    //210204 マップ用 画面を暗くして再表示する動作中か
    private bool isFadeoutAndFadein = false;

    private bool isFadeoutFinish;

    //遷移先シーン名
    string destination;

    MapMode destinationMapMode;

    ////透明度が変わるスピードを管理
    float fadeSpeed = 0.03f;

    Image fadeImage;

    float alfa;

    private float deltaTime;

    void Update()
    {
        deltaTime += Time.deltaTime;

        //フェードアウト中か
        if (isFadeOut)
        {
            FadeOut();
        }

        //210204 マップ用 画面を暗くして再表示する動作
        if (isFadeoutAndFadein)
        {
            FadeoutAndFadein();
        }

        //フェードイン中か
        if (isFadeIn)
        {
            FadeIn();
        }
    }

    //210204 タイトル画面用 フェードイン開始
    public void FadeinStart()
    {
        fadeImage = GetComponent<Image>();

       
        //画面を暗転させる
        alfa = 1;
        SetAlpha();
        
        fadeImage.enabled = true;   //真っ黒なイメージ表示
        isFadeIn = true;    //フェードイン開始
    }

    /// <summary>
    /// シーン変更を行う
    /// </summary>
    /// <param name="destination"></param>
    public void ChangeScene(string destination)
    {
        Debug.Log("Fade Out Start");
        //スクリーンを非表示にしていたのを表示
        fadeImage = GetComponent<Image>();

        isFadeoutToChangeScene = true;

        //遷移先設定
        this.destination = destination;

        //フェードアウト開始
        //Imageのアルファ値を取得 最初透明なので0となる 
        alfa = fadeImage.color.a;
        fadeImage.enabled = true;
        isFadeOut = true;

    }

    public void QuitFadeOut()
    {
        Debug.Log("Fade Out Start");

        fadeImage = GetComponent<Image>();

        isFadeoutToQuit = true;

        alfa = fadeImage.color.a;
        fadeImage.enabled = true;
        isFadeOut = true;
    }

    //200814 とりあえずマップ用 画面を暗転させて、完全に暗くなったらモード変更
    public void FadeoutAndFadeinStart(MapMode mapMode)
    {
        Debug.Log("FadeOutAndFadeIn Start");

        //遷移先モード設定
        this.destinationMapMode = mapMode;
        //スクリーンを非表示にしていたのを表示
        fadeImage = GetComponent<Image>();
        fadeImage.enabled = true;
        isFadeoutFinish = false;
        isFadeoutAndFadein = true;
    }

    //戦闘マップ用 画面を一度暗くしてモードを変更する
    private void FadeoutAndFadein()
    {
        //0.01秒おきに透明度変更
        if (deltaTime >= 0.01)
        {
            //フェードアウトが終了していなければ暗くしていく
            if (!isFadeoutFinish)
            {
                alfa += fadeSpeed;
                SetAlpha();

                // 完全に画面が暗くなったらシーン変更
                if (alfa >= 1)
                {
                    //戦闘前会話へ、無い場合はターン開始処理へ遷移
                    battleMapManager.ChangeMapmodeTurnStart(destinationMapMode);
                    //フェードアウトフラグをオフに
                    isFadeoutFinish = true;
                    Debug.Log("フェードアウト完了");
                }
            }
            else
            {
                //フェードアウトが完了したので今度はフェードインして画面を表示する
                alfa -= fadeSpeed;
                SetAlpha();

                //完全に画面が表示されたら
                if (alfa <= 0)
                {
                    //フェードインフェードアウト中フラグをオフに
                    isFadeoutAndFadein = false;
                    Debug.Log("フェードイン完了");
                    return;
                }
            }
            
            
            deltaTime = 0;
        }
    }

    private void FadeOut()
    {
        if (deltaTime >= 0.01) { 

            alfa += fadeSpeed;
            SetAlpha();
            
            //完全に暗転したら
            if (alfa >= 1)
            {
                //フェードアウトフラグをオフに
                isFadeOut = false;

                //210204 マップ用 画面を暗転させて再表示する動作なら再度フェードイン
                if (isFadeoutAndFadein)
                {
                    isFadeIn = true;
                }

                //ゲーム終了
                if (isFadeoutToQuit)
                {
                    Quit();
                    return;
                }

                //フェードアウトしてシーンを変更する時
                if (isFadeoutToChangeScene)
                {
                    // d)完全に不透明になったらシーン変更
                    ChangeScene();
                    return;
                }
            }
            deltaTime = 0;
        }
    }

    private void FadeIn()
    {
        if (deltaTime >= 0.01)
        {

            alfa -= fadeSpeed;
            SetAlpha();

            //完全に表示されたら
            if (alfa <= 0)
            {
                //フェードアウトフラグをオフに
                isFadeIn = false;
                isFadeoutAndFadein = false;

            }
            deltaTime = 0;
        }
    }

    public bool isFadeinFadeout()
    {
        //isFadeoutAndFadeinは戦闘マップの開始時、画面を一度暗転させている処理のこと
        if (isFadeIn || isFadeOut || isFadeoutAndFadein)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetAlpha()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, 
            fadeImage.color.b, alfa);
    }

    //シーン変更
    private void ChangeScene()
    {
        Debug.Log("Change Scnene " + destination);
        //シーン変更
        SceneManager.LoadScene(destination);
    }

    //ゲーム終了
    private void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
                  UnityEngine.Application.Quit();
        #endif
    }
}

using UnityEngine;

/// <summary>
/// 210521 BGM再生用クラス
/// </summary>
public class BGMPlayer : SingletonMonoBehaviour<BGMPlayer>
{
    //タイトル
    [SerializeField] AudioClip bgmIntroAudioClip;
    [SerializeField] AudioClip bgmLoopAudioClip;

    //ステータス
    [SerializeField] AudioClip bgmStatusIntroAudioClip;
    [SerializeField] AudioClip bgmStatusLoopAudioClip;

    //戦闘1
    [SerializeField] AudioClip field1IntroAudioClip;
    [SerializeField] AudioClip field1LoopAudioClip;

    AudioSource introAudioSource;
    AudioSource loopAudioSource;

    //フェードアウトする時間
    public double fadeOutSeconds = 0.1;

    //フェードアウト開始からの時間
    double FadeDeltaTime = 0;

    //フェードアウト中か
    bool isFadeOut;

    //現在再生しているBGMを保存しておく
    public BGMType playingBGM = BGMType.TITLE;


    public void Awake()
    {
        //もし、このインスタンスが既に有るインスタンスでなければ自身を削除する
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        //シーンを変更しても消えない
        DontDestroyOnLoad(gameObject);

        introAudioSource = gameObject.AddComponent<AudioSource>();
        

        //イントロ用BGMデータ
        introAudioSource.clip = bgmIntroAudioClip;

        if(bgmLoopAudioClip == null)
        {
            introAudioSource.loop = true;
        }
        else
        {
            loopAudioSource = gameObject.AddComponent<AudioSource>();
            introAudioSource.loop = false;  //ループしない
        }
        
        introAudioSource.playOnAwake = false;

        if (bgmLoopAudioClip != null)
        {
            //ループ用BGMデータ
            loopAudioSource.clip = bgmLoopAudioClip;
            loopAudioSource.loop = true;    //ループする
            loopAudioSource.playOnAwake = false;
        }
        
    }

    private void Update()
    {
        if (isFadeOut)
        {
            FadeDeltaTime += Time.deltaTime;

            //指定した時間以上経過したら
            if (FadeDeltaTime >= fadeOutSeconds)
            {
                FadeDeltaTime = fadeOutSeconds;
                isFadeOut = false;
                Destroy(gameObject);
            }

            introAudioSource.volume = (float)(1.0 - FadeDeltaTime / fadeOutSeconds);
            loopAudioSource.volume = (float)(1.0 - FadeDeltaTime / fadeOutSeconds);
        }
    }

    //BGM再生
    public void PlayBGM()
    {
        if (introAudioSource == null)
        {
            Debug.Log("play_failed");
            return;
        }

        Debug.Log("intro_play");
        //イントロ再生
        introAudioSource.Play();

        if (loopAudioSource != null)
        {
            //イントロBGMの長さ分再生されたタイミングでループを再生
            loopAudioSource.PlayScheduled(AudioSettings.dspTime + introAudioSource.clip.length);
        }
        
    }

    //BGM停止
    public void StopBGM()
    {
        if (introAudioSource == null)
        {
            return;
        }

        if (introAudioSource.isPlaying)
        {
            introAudioSource.Stop();
        }
        if (bgmLoopAudioClip != null)
        {
            if (loopAudioSource.isPlaying)
            {
                loopAudioSource.Stop();
            }
        }
        
    }

    //210206 BGM変更
    public void ChangeBGM(BGMType bgmType)
    {
        playingBGM = bgmType;

        if (BGMType.TITLE == bgmType)
        {
            introAudioSource.clip = bgmIntroAudioClip;

            //タイトル = ループなのでintroをループさせる
            introAudioSource.loop = true;
        }

        else if (BGMType.STATUS == bgmType)
        {
            introAudioSource.clip = bgmStatusIntroAudioClip;

            if (loopAudioSource == null)
            {
                loopAudioSource = gameObject.AddComponent<AudioSource>();
            }
            loopAudioSource.clip = bgmStatusLoopAudioClip;

            loopAudioSource.playOnAwake = false;

            introAudioSource.loop = false;
            loopAudioSource.loop = true;


        }
        else if (BGMType.FIELD1 == bgmType)
        {
            introAudioSource.clip = field1IntroAudioClip;

            if (loopAudioSource == null)
            {
                loopAudioSource = gameObject.AddComponent<AudioSource>();
            }
            loopAudioSource.clip = field1LoopAudioClip;

            loopAudioSource.playOnAwake = false;
            introAudioSource.loop = false;
            loopAudioSource.loop = true;


        }
    }

    public void FadeOutBGM()
    {
        isFadeOut = true;
    }

    //BGMプレイヤー削除
    public void DeleteBGMPlayer()
    {
        Destroy(gameObject);
    }
}
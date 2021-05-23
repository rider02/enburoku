using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 会話シーンの名前パネル、テキスト等を制御するWindowクラス
/// </summary>
public class GUIManager : MonoBehaviour
{
    public Camera MainCamera;
    public Transform ButtonPanel;
    public GameObject namePanel;
    public Button OptionButton;
    public Text Text;
    public Text Speaker;
    public GameObject Delta;

    private void Start()
    {
        //下矢印を点滅させる
        Delta.transform.DOMoveY(-0.2f, 1.0f).SetRelative().SetEase(Ease.InCubic)
            .SetLoops(-1, LoopType.Restart);
    }
}
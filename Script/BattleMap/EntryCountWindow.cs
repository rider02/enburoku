using UnityEngine;
using UnityEngine.UI;

//出撃準備の時、出撃するユニットの人数を表示するウィンドウ制御クラス
public class EntryCountWindow : MonoBehaviour
{
    [SerializeField] Text entryCountText;

    public void UpdateText(int entryCount, int maxEntryCount)
    {
        entryCountText.text = string.Format("出撃人数    {0}人 / {1}人", entryCount, maxEntryCount);

    }
}

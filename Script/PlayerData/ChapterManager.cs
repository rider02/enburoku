using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの進行度を保存する
/// </summary>
public class ChapterManager : MonoBehaviour
{

    public static Chapter chapter { get; set; }

    public static bool isChapterInit { get; set; }

    //初期化
    public void Init()
    {
        chapter = Chapter.STAGE1;
        isChapterInit = true;
    }
}

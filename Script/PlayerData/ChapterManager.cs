using UnityEngine;

/// <summary>
/// ゲームの進行度を保存する
/// </summary>
public static class ChapterManager
{
    //ゲームの進行状況
    public static Chapter chapter { get; set; }

    //初期化済みフラグ
    public static bool isChapterInit { get; set; }

    //初期化
    public static void Init()
    {
        chapter = Chapter.STAGE1;
        isChapterInit = true;
    }
}

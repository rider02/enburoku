﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 200713
/// Jobクラスに持たせる ステータス一覧
/// </summary>
[System.Serializable]
public class JobStatusDto
{
    /// <summary>
    /// 職業のステータス補正
    /// </summary>
    //HP
    public int jobHp;

    public int jobLatk;

    public int jobCatk;

    public int jobAgi;

    public int jobDex;

    public int jobLuk;

    public int jobLdef;

    public int jobCdef;
}

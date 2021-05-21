using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210303 ����ς݂̕󔠂̏��
/// </summary>
[System.Serializable]
public class AcquiredTreasure
{
    //���W
    public int x;
    public int y;
    //�o�ꂷ���
    public Chapter chapter;

    //�R���X�g���N�^
    public AcquiredTreasure(int x, int y, Chapter chapter)
    {
        this.x = x;
        this.y = y;
        this.chapter = chapter;
    }
}

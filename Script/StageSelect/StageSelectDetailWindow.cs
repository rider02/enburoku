using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

/// <summary>
/// 210513 �X�e�[�W�I����ʂł̃X�e�[�W�T�v�\���p�N���X
/// </summary>
public class StageSelectDetailWindow : MonoBehaviour
{
    //�X�e�[�W ��10�� �g���ٍŏ�K(���)
    [SerializeField] Text chapter;

    //�̖͂��O
    [SerializeField] Text chapterName;

    //�o���l��
    [SerializeField] Text entryCount;

    //��������
    [SerializeField] Text winCondition;

    //�s�k����
    [SerializeField] Text loseCondition;

    //�G�̕���Lv
    [SerializeField] Text enemyLevelAverage;

    //���炷��
    [SerializeField] Text storyText;

    public void UpdateText(Stage stage)
    {
        this.chapter.text = string.Format("��{0}��",
            (int)stage.chapter, stage.chapter.GetStringValue());

        this.chapterName.text = stage.chapter.GetChapterValue();

        //�o���l���A�����s�k����
        this.entryCount.text = string.Format("{0}�l", stage.entryUnitCount);
        this.winCondition.text = stage.winCondition.GetStringValue();
        this.loseCondition.text = stage.loseCondition.GetStringValue();

        //�G�̕���Lv
        List<int> enemyLevelList = new List<int>();

        //�X�e�[�W�̑S�G�̃��x�����擾
        foreach (Enemy enemy in stage.enemyList)
        {
            enemyLevelList.Add(enemy.lv);
        }

        //�l�̌ܓ������G�̕���Lv��\��
        int enemyLevelAverageNum = Mathf.RoundToInt((float)enemyLevelList.Average());
        enemyLevelAverage.text = enemyLevelAverageNum.ToString();

        //���炷��
        this.storyText.text = stage.storyText;
    }
}

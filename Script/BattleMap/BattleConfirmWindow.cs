using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 200216 �@�\�͏��Ȃ����A�e�L�X�g�̍X�V�̈׍쐬
/// </summary>
public class BattleConfirmWindow : MonoBehaviour
{
    [SerializeField] Text confirmText;
    BattleManager battleManager;

    bool isAttack;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;
    }

    public void UpdateConfirmtext(string text, bool isAttack)
    {
        confirmText.text = text;
        this.isAttack = isAttack;
    }

    //�J�n�{�^�������������A�퓬�J�n�A�������͉񕜊J�n���s���׏������o��������
    public void OnStartButtonClick()
    {
        if (isAttack)
        {
            battleManager.BattleStart();
        }
        else
        {
            battleManager.HealStart();
        }
    }
}

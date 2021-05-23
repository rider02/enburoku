using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

/// <summary>
/// 210216
/// �񕜁A�X�e�[�^�X�A�b�v�A�C�e����ScriptabeleObject����p�N���X
/// Unity�㕔�́uCreate�v����g�p����
/// </summary>
public static class PotionDatabaseCreator
{
    [MenuItem("Create/PotionDatabase")]
    private static void Create()
    {
        PotionDatabase potionDatabase = ScriptableObject.CreateInstance<PotionDatabase>();

        //���O
        string potionName = "���J�̏���";
        int amount = 10;
        int useCount = 3;
        int price = 300;
        string annotationtext = "�g����HP��10�񕜂���";
        PotionType type = PotionType.HEAL;
        bool isRequirePharmacy = false;

        Potion potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "�i�����̏���";
        amount = 20;
        useCount = 3;
        price = 600;
        annotationtext = "�g����HP��20�񕜂���";
        type = PotionType.HEAL;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "�Ӓ�����";
        amount = 99;
        useCount = 3;
        price = 1200;
        annotationtext = "�g����HP���S�񕜂���";
        type = PotionType.HEAL;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "��";
        amount = 10;
        useCount = 3;
        price = 400;
        annotationtext = "�򔄂�̂ݎg�p�\ �אڂ��钇�Ԃ�HP��10��";
        type = PotionType.HEAL;
        isRequirePharmacy = true;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "�i�����̖�";
        amount = 20;
        useCount = 3;
        price = 900;
        annotationtext = "�򔄂�̂ݎg�p�\ �אڂ��钇�Ԃ�HP��20��";
        type = PotionType.HEAL;
        isRequirePharmacy = true;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "��l�̒O";
        amount = 0;
        useCount = 3;
        price = 600;
        annotationtext = "���h+7 ���^�[��2������";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "�d���̉ʎ�";
        amount = 5;
        useCount = 1;
        price = 2500;
        annotationtext = "HP+5";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "�S�̑���";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "�ߍU+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "�����̖�����";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "���U+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "�͓��̋Z�p��";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "�Z+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "�V���̉H�c��";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "����+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "�ؒ���̕���";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "�K�^+4";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "�V�l�̓�";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "�ߖh+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "���E�̌���";
        amount = 2;
        useCount = 1;
        price = 2500;
        annotationtext = "���h+2";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        potionName = "�s�v�c�Ȍ���";
        amount = 1;
        useCount = 1;
        price = 2500;
        annotationtext = "�ړ�+1";
        type = PotionType.STATUSUP;
        isRequirePharmacy = false;

        potion = new Potion(potionName, annotationtext, type, isRequirePharmacy,
            amount, useCount, price);

        potionDatabase.potionList.Add(potion);

        //�t�@�C�������o�� Resources�z���ɍ��
        AssetDatabase.CreateAsset(potionDatabase, "Assets/Resources/potionDatabase.asset");
    }
}

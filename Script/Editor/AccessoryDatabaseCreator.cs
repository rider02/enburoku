using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

/// <summary>
/// 210218 �A�N�Z�T���ꗗScriptabeleObject����p�N���X
/// Unity�㕔�́uCreate�v����g�p����
/// </summary>
public static class AccessoryDatabaseCreator
{
    //MenuItem��t���鎖�ŏ㕔���j���[�ɍ��ڂ�ǉ�
    [MenuItem("Create/AccessoryDatabase")]
    private static void Create()
    {
        //ScriptableObject�̃C���X�^���X���쐬
        AccessoryDatabase accessoryDatabase = ScriptableObject.CreateInstance<AccessoryDatabase>();

        //�f�[�^��ݒ�
        string name = "�o���̌F��";
        string text = "�l���o���l1.5�{";
        AccessoryEffectType effect = AccessoryEffectType.EXPUP; //�A�N�Z�T���̌���

        int delay = 0;      //�d��
        int amount = 0;     //�␳�l �o���̌F���float�ɂȂ�Ɩʓ|�Ȃ̂ŕʌv�Z
        int price = 5000;   //�l�i
        bool isNfs = true;  //NotForSale �񔄕i

        Accessory accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);


        name = "������";
        text = "�����ƕK�E����";
        effect = AccessoryEffectType.CRITICAL_AND_SLAYER_INVALID;

        delay = 0;
        amount = 0;
        price = 5000;
        isNfs = true;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "�����̂����";
        text = "����+2";
        effect = AccessoryEffectType.AGIUP;
        isNfs = false;

        delay = 0;
        amount = 2;
        price = 3000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "����̂����";
        text = "���+10";
        effect = AccessoryEffectType.EVASIONUP;
        isNfs = false;

        delay = 0;
        amount = 10;
        price = 3000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "�K�E�̂����";
        text = "�K�E+10";
        effect = AccessoryEffectType.CRITICALUP;
        isNfs = false;

        delay = 0;
        amount = 10;
        price = 3000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "�����̂����";
        text = "����+10";
        effect = AccessoryEffectType.HITUP;
        isNfs = false;

        delay = 0;
        amount = 10;
        price = 3000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "�d�����S�̎D";
        text = "�d���������� ���h+2";
        effect = AccessoryEffectType.FAIRY_SLAYER_INVALID;
        isNfs = true;

        delay = 0;
        amount = 2;
        price = 5000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "�l�Ԉ��S�̎D";
        text = "�l�ԓ������� ���h+2";
        effect = AccessoryEffectType.HUMAN_SLAYER_INVALID;
        isNfs = true;

        delay = 0;
        amount = 2;
        price = 5000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "�d�����S�̎D";
        text = "�d���������� ���h+2";
        effect = AccessoryEffectType.YOUKAI_SLAYER_INVALID;
        isNfs = true;

        delay = 0;
        amount = 2;
        price = 5000;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "�����̂��P���_";
        text = "�񕜗�+10";
        effect = AccessoryEffectType.HEALUP;

        delay = 0;
        amount = 10;
        price = 3000;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "���j�̔�����";
        text = "���U+3";
        effect = AccessoryEffectType.LATKUP;

        delay = 0;
        amount = 3;
        price = 3000;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "�V�����̌`��";
        text = "���h+1";
        effect = AccessoryEffectType.LDEFUP;

        delay = 0;
        amount = 1;
        price = 500;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "����̌`��";
        text = "���h+2";
        effect = AccessoryEffectType.LDEFUP;

        delay = 0;
        amount = 1;
        price = 1000;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "��l�̌`��";
        text = "���h+3";
        effect = AccessoryEffectType.LDEFUP;

        delay = 0;
        amount = 1;
        price = 1500;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        name = "���҂̌`��";
        text = "���h+4";
        effect = AccessoryEffectType.LDEFUP;

        delay = 0;
        amount = 1;
        price = 2000;
        isNfs = false;

        accessory = new Accessory(name, text, delay, effect, amount, price, isNfs);
        accessoryDatabase.accessoryList.Add(accessory);

        //�t�@�C�������o�� Resources�z���ɍ��
        AssetDatabase.CreateAsset(accessoryDatabase, "Assets/Resources/accessoryDatabase.asset");
    }

}

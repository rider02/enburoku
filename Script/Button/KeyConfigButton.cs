using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210513 �L�[�R���t�B�O(����A�L�����Z����)��ݒ肷��p�̃{�^��
/// </summary>
public class KeyConfigButton : MonoBehaviour
{
    [SerializeField]
    private Text KeyConfigTypeText; //����A�L�����Z�����̋@�\

    [SerializeField]
    private Text assignKeyCodeText; //�A�T�C������Ă���KeyCode

    private KeyConfigType keyconfigType;    //�ǂ̋@�\�̃{�^������ێ�

    private KeyConfigManager keyConfigManager;


    public void Init(KeyConfigManager keyConfigManager, KeyConfigType keyconfigType, KeyCode keycode)
    {
        //�@�\�e�L�X�g�A�A�T�C������Ă���KeyCode�e�L�X�g�ݒ�
        KeyConfigTypeText.text = keyconfigType.GetStringValue();
        assignKeyCodeText.text = keycode.ToString();

        this.keyconfigType = keyconfigType;

        this.keyConfigManager = keyConfigManager;
    }

    //�e�L�X�g�X�V
    public void UpdateText(KeyConfigType keyconfigType, KeyCode keycode)
    {
        //�@�\�e�L�X�g�A�A�T�C������Ă���KeyCode�e�L�X�g�ݒ�
        KeyConfigTypeText.text = keyconfigType.GetStringValue();
        assignKeyCodeText.text = keycode.ToString();
    }

    //�N���b�N��
    public void OnClick()
    {
        keyConfigManager.KeyAssignReceipt(keyconfigType);
    }

    //�I����
    public void OnSelect()
    {

    }
}

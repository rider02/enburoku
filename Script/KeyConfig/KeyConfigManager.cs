using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

/// <summary>
/// 210513 �L�[�R���t�B�O�̃t�@�C�����o�́A���͎�t���s���N���X
/// </summary>
public class KeyConfigManager : MonoBehaviour
{
    private TitleManager titleManager;

    //Key : �@�\��(����A�L�����Z����) Value : �A�T�C�������Q�[���p�b�h�̃{�^��
    public static Dictionary<KeyConfigType, KeyCode> configMap;

    //���݊��蓖�Ă��s���Ă���@�\
    private KeyConfigType assignKeyConfigType;

    //���蓖�ď������I��������Ƀt�H�[�J�X��߂��{�^��
    private GameObject selectedButton;

    //������(�^�C�g����ʂ̂�)
    public KeyConfigManager(TitleManager titleManager, string configFilePath)
    {
        configMap = new Dictionary<KeyConfigType, KeyCode>();
        this.titleManager = titleManager;

        //�L�[�R���t�B�O�t�@�C����ǂݍ���
        LoadKeyConfig(configFilePath);
    }

    //�L�[�R���t�B�O�̃��[�h(�J���Ń^�C�g����ʈȊO����J�n�������A�{�Ԃ͌Ă΂�Ȃ�)
    public static void InitKeyConfig(string configFilePath)
    {
        configMap = new Dictionary<KeyConfigType, KeyCode>();

        //�L�[�R���t�B�O�t�@�C����ǂݍ���
        LoadKeyConfig(configFilePath);
    }

    /// <summary>
    /// �Z�[�u�A���[�h�֘A
    /// </summary>

    //�L�[�R���t�B�O�t�@�C���擾 �N�������Ɏ��s
    private static void LoadKeyConfig(string configFilePath)
    {
        //���݂���΃��[�h���s
        if (File.Exists(configFilePath))
        {
            // �w�肵���p�X�̃t�@�C���X�g���[�����J��
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(configFilePath, FileMode.Open);
            try
            {
                // �w�肵���t�@�C���X�g���[�����f�V���A���C�Y���Ď擾
                configMap = (Dictionary<KeyConfigType, KeyCode>)bf.Deserialize(file);
                Debug.Log("�L�[�R���t�B�O�����[�h���܂���");

            }
            finally
            {
                // �t�@�C���̔j��
                if (file != null)
                    file.Close();
            }
        }
        else
        {
            //����N�������A�L�[�R���t�B�O�t�@�C�������݂��Ȃ��ꍇ�̓f�t�H���g�쐬
            Debug.Log($"WARN : �L�[�R���t�B�O�t�@�C�������݂��܂��� path : {configFilePath}");
            InitKeyConfig(configFilePath);
        }
    }

    //�L�[�R���t�B�O�ۑ� �R���t�B�OUI����鎞�̂݌Ă΂��
    public void SaveKeyConfig(string configFilePath)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(configFilePath);

        try
        {
            // �w�肵���I�u�W�F�N�g����ō쐬�����X�g���[���ɃV���A��������
            bf.Serialize(file, configMap);
        }
        finally
        {
            // �t�@�C���̔j��
            if (file != null)
                file.Close();
        }
        Debug.Log("�L�[�R���t�B�O��ۑ����܂���");
    }

    /// <summary>
    /// �L�[���͎擾�֘A
    /// </summary>

    //�L�[�̓��͂��m�F����
    private static bool InputKeyCheck(KeyConfigType keyConfigType, Func<KeyCode, bool> predicate)
    {
        if (!configMap.ContainsKey(keyConfigType))
        {
            Debug.Log($"Error �L�[�R���t�B�ODictionary�ɃL�[�����݂��܂��� key : {keyConfigType.ToString()}");
            return false;
        }

        //key(�@�\)����Value��keyCode���擾
        KeyCode keyCode = configMap[keyConfigType];

        if (predicate(keyCode))
        {
            //���O����R�o��̂ŃR�����g�A�E�g
            //Debug.Log($"�L�[��������܂��� key : {keyConfigType.ToString()}");
            return true;
        }
                
        return false;
    }

    //�����̋@�\(����A�L�����Z����)�ɃA�T�C������Ă���L�[��������Ԃ���Ԃ�
    public static bool GetKey(KeyConfigType keyConfigType)
    {
        return InputKeyCheck(keyConfigType, Input.GetKey);
    }

    //�����̋@�\(����A�L�����Z����)�ɃA�T�C������Ă���L�[�����͂��ꂽ����Ԃ�
    public static bool GetKeyDown(KeyConfigType keyConfigType)
    {
        return InputKeyCheck(keyConfigType, Input.GetKeyDown);
    }

    //�����̋@�\(����A�L�����Z����)�ɃA�T�C������Ă���L�[����������Ă��Ȃ�����Ԃ�
    public static bool GetKeyUp(KeyConfigType keyConfigType)
    {
        return InputKeyCheck(keyConfigType, Input.GetKeyUp);
    }

    //�����ꂩ�̃{�^���ł��L�[�R���t�B�O�ɃA�T�C�����ꂽ�L�[��������Ă����true��Ԃ�
    public static bool GetKeyDownAny()
    {
        foreach (KeyConfigType key in Enum.GetValues(typeof(KeyConfigType)))
        {
            if (GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }

    //UGUI�̃{�^�����N���b�N���X�N���v�g������s���鏈��
    public static void ButtonClick()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        if(obj == null)
        {
            Debug.Log("WARN : �I�𒆂�GameObject���L��܂���");
            return;
        }

        Button button = obj.GetComponent<Button>();
        if(button == null)
        {
            Debug.Log("WARN : �I�𒆂�GameObject�̓{�^���ł͂���܂���");
            return;
        }

        //�{�^���N���b�N���s
        button.onClick.Invoke();
    }

    /// <summary>
    /// �L�[���蓖�Ċ֘A
    /// </summary>
    
    //�{�^�����������L�[�̊��蓖�ē��͎�t�J�n
    public void KeyAssignReceipt(KeyConfigType keyConfigType)
    {
        //�L�[�̊��蓖�Ă��s���Ă���{�^�����擾
        assignKeyConfigType = keyConfigType;

        //�t�H�[�J�X��߂��{�^�����T���Ă���
        selectedButton = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);

        //���[�h�ύX
        titleManager.KeyAssignReceipt();
    }

    //Update�Ŏ��s����A�L�[�����͂��ꂽ�炻�̃L�[�������Ɋ��蓖�Ă�
    public void KeyAssign()
    {
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel") ||
            Input.GetButtonDown("Menu") || Input.GetButtonDown("Zoom") || Input.GetButtonDown("Speed"))
        {
            Debug.Log("����InputManager�ɃA�T�C������Ă���{�^���͊��蓖�ďo���܂���");
            return;
        }

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            Debug.Log("InputManager�ɃA�T�C������Ă���㉺���E�{�^���͊��蓖�ďo���܂���");
            return;
        }

        //�����L�[�������ꂽ��
        if (Input.anyKeyDown)
        {
            //�S�ẴL�[�R�[�h�ƕt�����킹�āA���͂��ꂽ�L�[�R�[�h����肷��
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    //����������
                    Debug.Log($"���͂��ꂽ�L�[���@�\�ɃA�T�C�� key : {code.ToString()}");

                    //�A�T�C���㏑��
                    configMap[assignKeyConfigType] = code;
                    //�{�^���̃e�L�X�g�X�V
                    selectedButton.GetComponent<KeyConfigButton>().UpdateText(assignKeyConfigType, code);

                    break;
                }
            }

            //���[�h��߂��āA�L�[���蓖�ăE�B���h�E���\���ɂ���
            titleManager.KeyAssignFinish(selectedButton);
        }
    }

    /// <summary>
    /// �������֘A
    /// </summary>

    //����N���ȂǂŃL�[�R���t�B�O�t�@�C���������ꍇ�A�f�t�H���g�쐬
    private void InitKeyConfig()
    {
        Debug.Log("�L�[�R���t�B�O�t�@�C���������ׁA�f�t�H���g�l���쐬");

        //����
        configMap.Add(KeyConfigType.SUBMIT, KeyCode.JoystickButton1);

        //�L�����Z��
        configMap.Add(KeyConfigType.CANCEL, KeyCode.JoystickButton2);

        //�X�^�[�g
        configMap.Add(KeyConfigType.START, KeyCode.JoystickButton9);

        //���j���[(�X�e�[�^�X�\����)
        configMap.Add(KeyConfigType.MENU, KeyCode.JoystickButton0);

        //�J�[�\�����x�ύX
        configMap.Add(KeyConfigType.SPEED, KeyCode.JoystickButton3);

        //�J�����̃Y�[��
        configMap.Add(KeyConfigType.ZOOM, KeyCode.JoystickButton7);
    }

    //�^�C�g����ʋN�����ɃL�[�R���t�B�O�ݒ�UI�̃{�^���ꗗ���쐬����
    public void CreateConfigButtonList(GameObject keyConfigWindow)
    {
        int index = 0;

        //�ݒ荀�ڂ̐��{�^���𐶐�
        foreach (KeyValuePair<KeyConfigType, KeyCode> config in configMap)
        {
            //�{�^���쐬�Ə�����
            var configButton = (Instantiate(Resources.Load("Prefabs/ConfigButton")) as GameObject).transform;
            configButton.GetComponent<KeyConfigButton>().Init(this, config.Key, config.Value);
            configButton.name = configButton.name.Replace("(Clone)", "");
            configButton.name += index;

            //keyConfigWindow�z����prefab�쐬
            configButton.transform.SetParent(keyConfigWindow.transform, false);
            
            index++;
        }

    }
}

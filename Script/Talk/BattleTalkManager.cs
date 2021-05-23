using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210519 �퓬�V�[���̉�b�V�[������ GameController���Q�l�ɍ쐬
/// </summary>
public class BattleTalkManager : MonoBehaviour
{
    public BattleSceneController battleSceneController;
    private BattleMapManager battleMapManager;
    private GameObject talkWindow;
    private GameObject talkView;
    private List<string> viewedTalkList = new List<string>();    //���ɕ\��������b�̍ĕ\����h���p



    // ������
    public void Init(BattleMapManager battleMapManager, GameObject talkWindow,GameObject talkView, FadeInOutManager fadeInOutManager)
    {
        this.battleMapManager = battleMapManager;
        this.talkWindow = talkWindow;
        this.talkView = talkView;
        battleSceneController = new BattleSceneController(this, talkWindow.GetComponent<GUIManager>(), fadeInOutManager, talkView);
    }

    //BattleMapManager��Update����Ă΂��
    public void TalkUpdate()
    {
        //�퓬�J�n�O��b�������͓��Anull�̏ꍇ�͉�b�I��
        if (battleSceneController.currentScene == null)
        {
            Debug.Log("��b�f�[�^���L��܂���");
            TalkEnd();
            return;
        }
        //��\���Ȃ�E�B���h�E�\�����čŏ��̃��b�Z�[�W�\��
        if (!talkView.activeSelf)
        {
            talkView.SetActive(true);
            battleSceneController.SetNextProcess();
        }

        //�N���b�N�̏���
        battleSceneController.WaitClick();

        //�t���O�����đI�����A���}�[�N���o������������肷�鏈��
        battleSceneController.SetComponents();
    }

    //�퓬�O��b���Z�b�g���� scene�̖����K���́uSTAGE�Z_BATTLESTART�v
    //�u�퓬�J�n�v�{�^�������������ɌĂ΂��
    public bool IsBattleStartTalkExist(Chapter chapter)
    {
        string sceneName = chapter.ToString() + "_BATTLESTART";

        if (battleSceneController.CheckSceneExist(sceneName)) { 
            battleSceneController.SetScene(sceneName);
            Debug.Log($"�V�[���ǂݍ��� : {sceneName}");
            return true;
        }
        Debug.Log($"�V�[�������݂��܂���ł��� : {sceneName}");
        return false;
    }

    //�w��^�[���o�ߎ��̉�b���L�邩�m�F���s�� 
    public bool IsTurnTalkExist(Chapter chapter, int turn)
    {
        //���̃^�[���̉�b�����݂��邩���m�F���� �����K���́uSTAGE�Z_TURN_(�^�[����)�v
        string sceneName = chapter.ToString() + "_TURN_"+ turn ;

        //���݂���Ή�b���[�h��
        if (battleSceneController.CheckSceneExist(sceneName))
        {
            //�m�F�Ɠ����ɃV�[���ɃZ�b�g���s��
            battleSceneController.SetScene(sceneName);
            Debug.Log($"�V�[���ǂݍ��� : {sceneName}");
            return true;
        }
        Debug.Log($"�V�[�������݂��܂���ł��� : {sceneName}");
        return false;
    }

    //210520 �퓬�O��b�����݂��邩���m�F���� �\���ς݂��̔�������킹�čs��
    public bool IsBattleStartTalkExist(Chapter chapter, string unitName)
    {
        //�����K���́A�ėp�́u�uSTAGE�Z_BOSS�v�A��p�̑g�ݍ��킹�́uSTAGE�Z_BOSS_(����)�v
        //��p��b�̕����D��x������
        string sceneName = chapter.ToString() + "_BOSS";

        //���ɕ\���ς݂̉�b�͍ĕ\�����Ȃ�
        if (viewedTalkList.Contains(sceneName))
        {
            Debug.Log($"���ɕ\���ς݂̉�b�Ȃ̂ŃX�L�b�v : {sceneName}");
            return false;
        }

        //���݂���Ή�b���[�h��
        if (battleSceneController.CheckSceneExist(sceneName))
        {
            //���ɕ\��������b���X�g�ɒǉ�
            viewedTalkList.Add(sceneName);

            //�m�F�Ɠ����ɃV�[���ɃZ�b�g���s��
            battleSceneController.SetScene(sceneName);
            Debug.Log($"�V�[���ǂݍ��� : {sceneName}");
            return true;
        }
        Debug.Log($"�V�[�������݂��܂���ł��� : {sceneName}");
        return false;
    }

    //�{�X���j���̉�b���L�邩�m�F���āA���݂���΃Z�b�g����
    public bool IsBossDestroyTalkExist(Chapter chapter)
    {
        //���̃^�[���̉�b�����݂��邩���m�F���� �����K���́uSTAGE�Z__BOSS_DESTROY�v
        string sceneName = chapter.ToString() + "_BOSS_DESTROY";

        //���ɕ\���ς݂̉�b�͍ĕ\�����Ȃ�
        if (viewedTalkList.Contains(sceneName))
        {
            Debug.Log($"���ɕ\���ς݂̉�b�Ȃ̂ŃX�L�b�v : {sceneName}");
            return false;
        }

        //���݂���Ή�b���[�h��
        if (battleSceneController.CheckSceneExist(sceneName))
        {
            //���ɕ\��������b���X�g�ɒǉ�
            viewedTalkList.Add(sceneName);

            //�m�F�Ɠ����ɃV�[���ɃZ�b�g���s��
            battleSceneController.SetScene(sceneName);
            Debug.Log($"�V�[���ǂݍ��� : {sceneName}");
            return true;
        }
        Debug.Log($"�V�[�������݂��܂���ł��� : {sceneName}");
        return false;
    }

    //�L�����s�k���̉�b ��{�I�ɂ͑S�����݂��邪�A�ꉞ�m�F
    public bool IsLoseTalkExist(string name)
    {

        string sceneName = name.ToLower() + "_LOSE";

        //���ɕ\���ς݂̉�b�͍ĕ\�����Ȃ�
        if (viewedTalkList.Contains(sceneName))
        {
            Debug.Log($"���ɕ\���ς݂̉�b�Ȃ̂ŃX�L�b�v : {sceneName}");
            return false;
        }

        //���݂���Ή�b���[�h��
        if (battleSceneController.CheckSceneExist(sceneName))
        {
            //���ɕ\��������b���X�g�ɒǉ�
            viewedTalkList.Add(sceneName);

            //�m�F�Ɠ����ɃV�[���ɃZ�b�g���s��
            battleSceneController.SetScene(sceneName);
            Debug.Log($"�V�[���ǂݍ��� : {sceneName}");
            return true;
        }
        Debug.Log($"�V�[�������݂��܂���ł��� : {sceneName}");
        return false;
    }

    //210520�@��b�I��
    public void TalkEnd()
    {
        //�����G�A�E�B���h�E�Ȃǂ�UI������
        talkView.SetActive(false);

        //�퓬�O��b�A�^�[���J�n����b�̏ꍇ�͎��R�^�[����
        if(battleMapManager.mapMode == MapMode.START_TALK)
        {
            battleMapManager.SetMapMode(MapMode.TURN_START);
        }
        else if (battleMapManager.mapMode == MapMode.TURN_START_TALK)
        {
            //�^�[���J�n����b�́A�J�n�G�t�F�N�g����ɑ}������Ă���̂�NORMAL�֑J��
            battleMapManager.SetMapMode(MapMode.NORMAL);
        }
        else if (battleMapManager.mapMode == MapMode.BATTLE_BEFORE_TALK ||
            battleMapManager.mapMode == MapMode.BATTLE_AFTER_TALK)
        {
            //�퓬�O��b�A�퓬���b�A�s�k���̉�b��BATTLE���[�h��
            battleMapManager.SetMapMode(MapMode.BATTLE);
        }
    }
}

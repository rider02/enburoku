using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;
using System;

/// <summary>
/// 210302 BattleMapManager�̒��ɗL�����̂ŕҏW�@�\�͕�������
/// </summary>
public class MapEditManager : MonoBehaviour
{

    BattleMapManager battleMapManager;
    Main_Map mainMap;
    EditorModeView editorModeView;
    GameObject editMenuWindow;
    GameObject selectCellTypeWindow;
    Text inputCellText;

    public bool isEditorMode = false;

    public EditMode editMode = EditMode.NONE;

    //200803 ���͎��̃Z���̎��
    CellType inputCellType = CellType.Field;

    public void Init(BattleMapManager battleMapManager, Main_Map mainMap, EditorModeView editorModeView,
        GameObject editMenuWindow, GameObject selectCellTypeWindow, Text inputCellText)
    {
        this.battleMapManager = battleMapManager;
        this.mainMap = mainMap;
        this.editorModeView = editorModeView;
        this.editMenuWindow = editMenuWindow;               //�G�f�B�b�g�̃��[�h��I�ԃE�B���h�E
        this.selectCellTypeWindow = selectCellTypeWindow;   //���͂���Z����I�ԃE�B���h�E
        this.inputCellText = inputCellText;                 //���ݓ��͂��Ă���Z���̎�ނ�\�L

        //�G�f�B�b�g���[�h�̃Z���̎�ރ��X�g�쐬
        CreateSelectCellTypeButton();
    }

    //�G�f�B�b�g���[�h�̐؂�ւ����s��
    public void ToggleEditMode()
    {
        MapMode mapMode = battleMapManager.mapMode;
        if (mapMode != MapMode.NORMAL)
        {
            Debug.Log("���R�^�[�����ȊO�̓G�f�B�b�g���[�h�ɑΉ����Ă��܂���B");
            return;
        }

        //���[�h�ؑ�
        if (isEditorMode)
        {
            //���ɃG�f�B�^���[�h�Ȃ����
            editorModeView.gameObject.SetActive(false);
            battleMapManager.setCamelaEditMode(false);
            EventSystem.current.SetSelectedGameObject(null);

            //battleMapManager.mapMode = MapMode.NORMAL;
            Debug.Log($"mapMode : {battleMapManager.mapMode}");
        }
        else
        {
            //�G�f�B�^���[�h�łȂ���΃G�f�B�b�g�p���j���[��\��
            editorModeView.gameObject.SetActive(true);
            battleMapManager.setCamelaEditMode(true);
            //���̓Z���I���{�^����I��
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(editMenuWindow.transform.Find("InputCellButton").gameObject);

            //�G�Ƀ}�b�v�̃��[�h��EDIT�ɂ���
            //battleMapManager.mapMode = MapMode.EDIT;
            Debug.Log($"mapMode : {battleMapManager.mapMode}");

            //�ҏW���[�h�ݒ�
            editMode = EditMode.MENU;
        }

        //�G�f�B�b�g���[�h��ݒ�
        isEditorMode = isEditorMode ? false : true;
        Debug.Log($"�G�f�B�b�g���[�h : {isEditorMode}");

        //�S�ẴZ���Ɂu�ǁv�u����v�Ƃ���ނ�\��
        mainMap.ShowSellType(isEditorMode);
    }

    //�ҏW���̌���{�^��
    public void EditFireButton()
    {
        //�Z�����͂̂݃{�^���������ςȂ��Ή�
        //�Z�����̓��[�h
        if (editMode == EditMode.CELL_EDIT)
        {
            //200802 �Z���̎�ނ�ύX
            Vector3 cursorPos = battleMapManager.GetCursorPos();
            int index = mainMap.Cells.IndexOf(mainMap.Cells.FirstOrDefault(c => c.X == cursorPos.x && c.Y == cursorPos.z));

            //������Ȃ����-1���Ԃ��Ă���̂ŉ������Ȃ�
            if (index == -1)
            {
                return;
            }

            Main_Cell cell = mainMap.Cells[index];
            cell.SetType(inputCellType);

            //�X�V�O�O
            mainMap.Cells[index] = cell;

        }
    }

    //�ҏW���̃L�����Z���{�^��
    public void EditCancelButton()
    {
        //���j���[
        if (editMode == EditMode.MENU)
        {

            //���j���[�\�����̓��j���[�������āA�Z�����̓��[�h�ɕύX
            editMenuWindow.SetActive(false);
            editMode = EditMode.CELL_EDIT;
            Debug.Log($"�G�f�B�b�g���[�h : {isEditorMode}");
        }

        //�Z���I�����[�h
        else if (editMode == EditMode.SELECT_CELL)
        {
            selectCellTypeWindow.SetActive(false);
            editMenuWindow.SetActive(true);
            editMode = EditMode.MENU;
            Debug.Log($"�G�f�B�b�g���[�h : {isEditorMode}");

            //�{�^���I��
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(editMenuWindow.transform.Find("InputCellButton").gameObject);
        }

        //���̓��[�h�̎�
        else if (editMode == EditMode.CELL_EDIT)
        {
            editMenuWindow.SetActive(true);
            editMode = EditMode.MENU;
            Debug.Log($"�G�f�B�b�g���[�h : {isEditorMode}");

            //�{�^���I��
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(editMenuWindow.transform.Find("InputCellButton").gameObject);
        }
    }

    /// <summary>
    /// 200802 �Z���̎�ނ�I������E�B���h�E���J��
    /// </summary>
    public void OpenCellTypeWindow()
    {
        //�Z���I�����[�h��
        editMode = EditMode.SELECT_CELL;
        Debug.Log($"�G�f�B�b�g���[�h : {isEditorMode}");

        //�Z�����̓{�^����I��
        selectCellTypeWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectCellTypeWindow.transform.Find("SelectCellTypeButton").gameObject);
        
    }

    /// <summary>
    /// 200803 �G�f�B�b�g���[�h�œ��͂���Z���̎�ނ�ύX
    /// SelectSellTypeButton����Ă�
    /// </summary>
    /// <param name="cellType"></param>
    public void SetInputCellType(CellType cellType)
    {
        //���͂���Z���̎�ނ�ύX���ē��̓��[�h��
        this.inputCellType = cellType;
        inputCellText.text = cellType.GetStringValue();
        editMode = EditMode.CELL_EDIT;
        editMenuWindow.SetActive(false);
        selectCellTypeWindow.SetActive(false);
    }

    /// <summary>
    /// 200802 �������ɃG�f�B�b�g���[�h�œ��͂���Z���̎�ނ�I������{�^�����쐬
    /// </summary>
    private void CreateSelectCellTypeButton()
    {
        //EditMode�N���X��enum��S�ă{�^����
        foreach (CellType cellType in Enum.GetValues(typeof(CellType)))
        {
            //Resources�z������{�^�������[�h
            var cellButton = (Instantiate(Resources.Load("Prefabs/SelectCellTypeButton")) as GameObject).transform;

            //�{�^��������
            cellButton.GetComponent<SelectSellTypeButton>().Init(cellType, this);
            cellButton.name = cellButton.name.Replace("(Clone)", "");

            //selectCellTypeWindow�z����prefab�쐬
            cellButton.transform.SetParent(selectCellTypeWindow.transform);
        }
    }

    //UI�̃Z�[�u�{�^���A���[�h�{�^������Ă΂��
    public void SaveMap()
    {
        mainMap.SaveMap(battleMapManager.stage);
    }
    public void LoadMap()
    {
        mainMap.LoadMap(battleMapManager.stage);
    }
}

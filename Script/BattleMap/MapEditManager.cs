using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;
using System;

/// <summary>
/// 210302 BattleMapManagerの中に有ったので編集機能は分割した
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

    //200803 入力時のセルの種類
    CellType inputCellType = CellType.Field;

    public void Init(BattleMapManager battleMapManager, Main_Map mainMap, EditorModeView editorModeView,
        GameObject editMenuWindow, GameObject selectCellTypeWindow, Text inputCellText)
    {
        this.battleMapManager = battleMapManager;
        this.mainMap = mainMap;
        this.editorModeView = editorModeView;
        this.editMenuWindow = editMenuWindow;               //エディットのモードを選ぶウィンドウ
        this.selectCellTypeWindow = selectCellTypeWindow;   //入力するセルを選ぶウィンドウ
        this.inputCellText = inputCellText;                 //現在入力しているセルの種類を表記

        //エディットモードのセルの種類リスト作成
        CreateSelectCellTypeButton();
    }

    //エディットモードの切り替えを行う
    public void ToggleEditMode()
    {
        MapMode mapMode = battleMapManager.mapMode;
        if (mapMode != MapMode.NORMAL)
        {
            Debug.Log("自軍ターン時以外はエディットモードに対応していません。");
            return;
        }

        //モード切替
        if (isEditorMode)
        {
            //既にエディタモードなら解除
            editorModeView.gameObject.SetActive(false);
            battleMapManager.setCamelaEditMode(false);
            EventSystem.current.SetSelectedGameObject(null);

            //battleMapManager.mapMode = MapMode.NORMAL;
            Debug.Log($"mapMode : {battleMapManager.mapMode}");
        }
        else
        {
            //エディタモードでなければエディット用メニューを表示
            editorModeView.gameObject.SetActive(true);
            battleMapManager.setCamelaEditMode(true);
            //入力セル選択ボタンを選択
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(editMenuWindow.transform.Find("InputCellButton").gameObject);

            //雑にマップのモードをEDITにする
            //battleMapManager.mapMode = MapMode.EDIT;
            Debug.Log($"mapMode : {battleMapManager.mapMode}");

            //編集モード設定
            editMode = EditMode.MENU;
        }

        //エディットモードを設定
        isEditorMode = isEditorMode ? false : true;
        Debug.Log($"エディットモード : {isEditorMode}");

        //全てのセルに「壁」「平野」とか種類を表示
        mainMap.ShowSellType(isEditorMode);
    }

    //編集中の決定ボタン
    public void EditFireButton()
    {
        //セル入力のみボタン押しっぱなし対応
        //セル入力モード
        if (editMode == EditMode.CELL_EDIT)
        {
            //200802 セルの種類を変更
            Vector3 cursorPos = battleMapManager.GetCursorPos();
            int index = mainMap.Cells.IndexOf(mainMap.Cells.FirstOrDefault(c => c.X == cursorPos.x && c.Y == cursorPos.z));

            //見つからなければ-1が返ってくるので何もしない
            if (index == -1)
            {
                return;
            }

            Main_Cell cell = mainMap.Cells[index];
            cell.SetType(inputCellType);

            //更新＾＾
            mainMap.Cells[index] = cell;

        }
    }

    //編集中のキャンセルボタン
    public void EditCancelButton()
    {
        //メニュー
        if (editMode == EditMode.MENU)
        {

            //メニュー表示中はメニューを消して、セル入力モードに変更
            editMenuWindow.SetActive(false);
            editMode = EditMode.CELL_EDIT;
            Debug.Log($"エディットモード : {isEditorMode}");
        }

        //セル選択モード
        else if (editMode == EditMode.SELECT_CELL)
        {
            selectCellTypeWindow.SetActive(false);
            editMenuWindow.SetActive(true);
            editMode = EditMode.MENU;
            Debug.Log($"エディットモード : {isEditorMode}");

            //ボタン選択
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(editMenuWindow.transform.Find("InputCellButton").gameObject);
        }

        //入力モードの時
        else if (editMode == EditMode.CELL_EDIT)
        {
            editMenuWindow.SetActive(true);
            editMode = EditMode.MENU;
            Debug.Log($"エディットモード : {isEditorMode}");

            //ボタン選択
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(editMenuWindow.transform.Find("InputCellButton").gameObject);
        }
    }

    /// <summary>
    /// 200802 セルの種類を選択するウィンドウを開く
    /// </summary>
    public void OpenCellTypeWindow()
    {
        //セル選択モードへ
        editMode = EditMode.SELECT_CELL;
        Debug.Log($"エディットモード : {isEditorMode}");

        //セル入力ボタンを選択
        selectCellTypeWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectCellTypeWindow.transform.Find("SelectCellTypeButton").gameObject);
        
    }

    /// <summary>
    /// 200803 エディットモードで入力するセルの種類を変更
    /// SelectSellTypeButtonから呼ぶ
    /// </summary>
    /// <param name="cellType"></param>
    public void SetInputCellType(CellType cellType)
    {
        //入力するセルの種類を変更して入力モードへ
        this.inputCellType = cellType;
        inputCellText.text = cellType.GetStringValue();
        editMode = EditMode.CELL_EDIT;
        editMenuWindow.SetActive(false);
        selectCellTypeWindow.SetActive(false);
    }

    /// <summary>
    /// 200802 初期時にエディットモードで入力するセルの種類を選択するボタンを作成
    /// </summary>
    private void CreateSelectCellTypeButton()
    {
        //EditModeクラスのenumを全てボタン化
        foreach (CellType cellType in Enum.GetValues(typeof(CellType)))
        {
            //Resources配下からボタンをロード
            var cellButton = (Instantiate(Resources.Load("Prefabs/SelectCellTypeButton")) as GameObject).transform;

            //ボタン初期化
            cellButton.GetComponent<SelectSellTypeButton>().Init(cellType, this);
            cellButton.name = cellButton.name.Replace("(Clone)", "");

            //selectCellTypeWindow配下にprefab作成
            cellButton.transform.SetParent(selectCellTypeWindow.transform);
        }
    }

    //UIのセーブボタン、ロードボタンから呼ばれる
    public void SaveMap()
    {
        mainMap.SaveMap(battleMapManager.stage);
    }
    public void LoadMap()
    {
        mainMap.LoadMap(battleMapManager.stage);
    }
}

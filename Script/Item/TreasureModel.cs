using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �󔠃N���XTreasureBox���X�e�[�W�ɔz�u����ׂ̃��b�p�[ ���W��ێ�����
/// </summary>
public class TreasureModel : MonoBehaviour
{
    public TreasureBox treasureBox;
    public int x;
    public int y;

    BattleMapManager battleMapManager;
    Main_Map map;

    public void Init(TreasureBox treasureBox, BattleMapManager battleMapManager, Main_Map map)
    {
        this.battleMapManager = battleMapManager;
        this.map = map;
        this.treasureBox = treasureBox;
    }
}

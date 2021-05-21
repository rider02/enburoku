using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//210301 ���ɍ���� �󔠃N���X
[System.Serializable]
public class TreasureBox
{
    //�����z�u����Ă���ꏊ
    public Coordinate coordinate;

    public int x;
    public int y;

    public bool isEmpty = false;

    BattleMapManager battleMapManager;

    public Item item;

    //�R���X�g���N�^
    public TreasureBox(Item item, Coordinate coordinate)
    {
        this.item = item;
        this.coordinate = coordinate;
    }

    public void Init(BattleMapManager battleMapManager)
    {
        this.battleMapManager = battleMapManager;
    }

    /// <summary>
    /// �󔠂��J����
    /// </summary>
    /// <returns></returns>
    public Item Open()
    {
        //�󂢂��t���O�𗧂Ă�
        isEmpty = true;

        return item;
    }

    //����������Database���瓯���C���X�^���X���擾���Ă��܂��̂ō쐬
    public TreasureBox Clone()
    {
        // Object�^�ŕԂ��Ă���̂ŃL���X�g���K�v
        return (TreasureBox)MemberwiseClone();
    }

}

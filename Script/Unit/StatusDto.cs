using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210218
/// �o�t�A�␳����ʃN���X�Őݒ肷��ׂ�DTO
/// </summary>
[System.Serializable]
public class StatusDto
{

    //HP
    public int hp;

    //���U
    public int latk;

    //�ߍU
    public int catk;

    //����
    public int agi;

    //�Z
    public int dex;

    //�^
    public int luk;

    //���h
    public int ldef;

    //�ߖh
    public int cdef;

    //�K�v���L��Ζ������A��𗦁A�K�E�����n��

    public StatusDto(int hp, int latk, int catk, int agi, int dex, int ldef, int cdef, int luk)
    {
        this.hp = hp;
        this.latk = latk;
        this.catk = catk;
        this.agi = agi;
        this.dex = dex;
        this.ldef = ldef;
        this.cdef = cdef;
        this.luk = luk;
    }

}

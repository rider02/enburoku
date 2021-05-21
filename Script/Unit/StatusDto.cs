using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210218
/// ƒoƒtA•â³“™‚ğ•ÊƒNƒ‰ƒX‚Åİ’è‚·‚éˆ×‚ÌDTO
/// </summary>
[System.Serializable]
public class StatusDto
{

    //HP
    public int hp;

    //‰“U
    public int latk;

    //‹ßU
    public int catk;

    //‘¬‚³
    public int agi;

    //‹Z
    public int dex;

    //‰^
    public int luk;

    //‰“–h
    public int ldef;

    //‹ß–h
    public int cdef;

    //•K—v‚ª—L‚ê‚Î–½’†—¦A‰ñ”ğ—¦A•KE—¦‚à“n‚·

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

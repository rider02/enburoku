/// <summary>
/// 210218
/// otAβ³πΚNXΕέθ·ιΧΜDTO
/// </summary>
[System.Serializable]
public class StatusDto
{

    //HP
    public int hp;

    //U
    public int latk;

    //ίU
    public int catk;

    //¬³
    public int agi;

    //Z
    public int dex;

    //^
    public int luk;

    //h
    public int ldef;

    //ίh
    public int cdef;

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

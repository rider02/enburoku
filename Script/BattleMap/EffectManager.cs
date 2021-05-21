using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//210211 戦闘に必要なエフェクトを保持しておくクラス

public class EffectManager : MonoBehaviour
{
    private BattleManager battleManager;

    public GameObject[] slashEffectList;
    public GameObject[] fireEffectList;
    public GameObject[] waterEffectList;
    public GameObject[] iceEffectList;
    public GameObject[] windEffectList;
    public GameObject[] shineEffectList;
    public GameObject[] darkEffectList;
    public GameObject[] healEffectList;
    public GameObject[] portalEffectList;

    public GameObject damageText;
    public GameObject healText;

    //初期化
    void Init(BattleManager battleManager, EffectManager effectManager)
    {
        this.battleManager = battleManager;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

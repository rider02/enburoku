using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210216 BattleParameterDTO‚Ìˆêí UŒ‚‚æ‚è‚à•\¦‚·‚é“à—e‚ª­‚È‚¢
/// </summary>
public class HealParameterDTO : MonoBehaviour
{
    public string unitName { get; set; }

    public string targetName { get; set; }

    public string unitHealRodName { get; set; }

    public Weapon unitHealRod { get; set; }

    public int unitHp { get; set; }

    public int unitMaxHp { get; set; }

    public int targetHp { get; set; }

    public int targetMaxHp { get; set; }

    public int healAmount { get; set; }
}

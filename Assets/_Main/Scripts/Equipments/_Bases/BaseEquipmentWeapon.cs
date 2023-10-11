using UnityEngine;
using static StatisticsShared;

public class BaseEquipmentWeapon : BaseEquipment
{
    //Weapon Specific Statistics
    public float Quality;
    public float Range;
    public float Handling;
    public StatisticName Affinity;

    //General bonuses
    /// <summary>
    /// Increase critical hit probability, flat number. Added to base value
    /// </summary>
    public float BonusCriticalHitProbability;
    /// <summary>
    /// Increase critical hit probability, percent increased on base value
    /// </summary>
    public float BonusCriticalHitProbabilityPercent;

    /// <summary>
    /// Critical hit damage multiplier, flat number. Use damage output as calculation reference, last calculation
    /// </summary>
    public float BonusCriticalHitDamage;
    /// <summary>
    /// Critical hit damage multiplier, percent increase. Use damage output as reference, last calculation
    /// </summary>
    public float BonusCriticalHitDamagePercent;

    /// <summary>
    /// Bonus value added to the damage output, flat number. Added before crit damage
    /// </summary>
    public float BonusDamage;
    /// <summary>
    /// Bonus value added to the damage output, percent increase. Added before crit damage
    /// </summary>
    public float BonusDamagePercent;

}

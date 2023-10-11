using UnityEngine;

public abstract class BaseEquipment : MonoBehaviour
{
    /// <summary>
    /// Reduce damage, flat number.
    /// </summary>
    public float Armor;
    /// <summary>
    /// Reduce magic damage, flat number.
    /// </summary>
    public float MagicArmor;

    /// <summary>
    /// Increase knockback resistance, flat number. Added to base value
    /// </summary>
    public float BonusKnockbackResistance;
    /// <summary>
    /// Increase knockback resistance, percent increase. Added to base value
    /// </summary>
    public float BonusKnockbackResistancePercent;

    /// <summary>
    /// Bonus movement speed, flat number. Added to base value
    /// </summary>
    public float BonusSpeed;
    /// <summary>
    /// Bonus movement speed, percent increase. Added to base value
    /// </summary>
    public float BonusSpeedPercent;

    /// <summary>
    /// Bonus attack speed, flat number. Added to base value
    /// </summary>
    public float BonusAttackSpeed;
    /// <summary>
    /// Bonus attack speed, percent increase. Added to base value
    /// </summary>
    public float BonusAttackSpeedPercent;

    /// <summary>
    /// Bonus vitality, flat number. Added to base value
    /// </summary>
    public float BonusVitality;
    /// <summary>
    /// Bonus vitality, percent increase. Added to base value
    /// </summary>
    public float BonusVitalityPercent;

    /// <summary>
    /// Bonus strenght, flat number. Added to base value
    /// </summary>
    public float BonusStrenght;
    /// <summary>
    /// Bonus strenght, percent increase. Added to base value
    /// </summary>
    public float BonusStrenghtPercent;

    /// <summary>
    /// Bonus dexterity, flat number. Added to base value
    /// </summary>
    public float BonusDexterity;
    /// <summary>
    /// Bonus dexterity, percent increase. Added to base value
    /// </summary>
    public float BonusDexterityPercent;

    /// <summary>
    /// Bonus intelligence, flat number. Added to base value
    /// </summary>
    public float BonusIntelligence;
    /// <summary>
    /// Bonus intelligence, percent increase. Added to base value
    /// </summary>
    public float BonusIntelligencePercent;
}

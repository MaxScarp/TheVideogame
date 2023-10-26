using System;
using UnityEngine;

public static class StatisticsShared
{
    public enum StatisticName
    {
        VITALITY,
        STRENGHT,
        DEXTERITY,
        INTELLIGENCE,
    }

    public enum SubStatisticName
    {
        MAX_HP,
        HP_REGEN,
        KNOCKBACK_RESISTANCE,
        INVENTORY_SPACE,
        KNOCKBACK,
        MOVEMENT_SPEED,
        ATTACK_SPEED,
        DODGE_CHANCHE,
        BALANCE,
        AGGRO_RANGE,
        EXP_GAIN,
        VIEW_RANGE,
    }

    [Serializable]
    public struct SubStatistic
    {
        public SubStatisticName Name;
        public float BaseValue;
        public float Value;
    }

    public struct DamageInformation
    {
        public float ActualDamage;
        public bool IsCriticalHit;
    }
}

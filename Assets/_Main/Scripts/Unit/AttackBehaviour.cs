using System;
using Unity.Mathematics;
using UnityEngine;
using static StatisticsShared;

public class AttackBehaviour : BaseBehaviour
{
    public override void TakeAction(Vector3 targetPosition, Unit unit)
    {
        //ToDo Get all unit in attack range
        DamageCalculations(unit);
        //ToDo Send damage to all unit hit
    }

    #region private methods
    private float DamageCalculations(Unit unit)
    {
        //ToDo Get stats from Unit statistics and weapon class
        //return CalculateHitDamage(unit.GetEquipment()., weaponQuality);

        return 1;
    }

    private DamageInformation CalculateHitDamage(float baseDamage, float weaponQuality) 
    {
        DamageInformation dmgInfo = new DamageInformation();
        dmgInfo.ActualDamage = baseDamage * UnityEngine.Random.Range(weaponQuality, 1);
        dmgInfo.IsCriticalHit = CriticalHitCheck(ref dmgInfo.ActualDamage);
        return dmgInfo;
    }

    private bool CriticalHitCheck(ref float actualDamage)
    {
        //ToDo Decide formula to calculate hit % based on Dexterity
        //ToDo Get bonuses stats from equipment
        float hitPercent = 0.1f; //PLACEHOLDER, TO BE REPLACED WITH ACTUAL hitpercent
        float critResult = UnityEngine.Random.Range(0f, 1f);

        if(critResult < hitPercent)
        {
            //ToDo Get crit damage bonus from equipment
            //actualDamage += bonusCritDamage; 
            return true;
        }
        else
        {
            return false; 
        }
    }
    #endregion

}
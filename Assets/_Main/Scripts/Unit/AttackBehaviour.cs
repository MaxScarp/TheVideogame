using System;
using Unity.Mathematics;
using UnityEngine;
using static StatisticsShared;

public class AttackBehaviour : BaseBehaviour
{
    public override void TakeAction(Vector3 targetPosition)
    {
        Debug.Log("ATTACCA!");
    }

    #region private methods
    private float DamageCalculations()
    {
        //ToDo Get stats from Unit statistics and weapon class

        //return CalculateHitDamage(baseDamageFromUnit, weaponQuality);
        return 1;
    }

    private Hit CalculateHitDamage(float baseDamage, float weaponQuality) 
    {
        Hit hit = new Hit();
        hit.ActualDamage = baseDamage * UnityEngine.Random.Range(weaponQuality, 1);
        hit.IsCriticalHit = CriticalHitCheck(ref hit.ActualDamage);
        return hit;
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

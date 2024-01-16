using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public abstract class SkillBase
{
    public abstract void UseSkill(GameObject attacker);

    public BigInteger GetDamage(BigInteger currDamage, float damageMultiplier)
    {
        var temp = (currDamage * (int)damageMultiplier) / 100;
        return currDamage + temp;
    }

    public Attack CreateAttackToMonster(ResultPlayerStats attacker, MonsterStats defender, float multiple)
    {
        float criticalChance = attacker.GetCritical();   
        float randomFloat = Random.Range(0f, 100f);
        randomFloat = Mathf.Round(randomFloat * 10f) / 10f;
        bool isCritical = randomFloat <= criticalChance;

        var currentDamage = attacker.ResultMonsterSkillDamage(isCritical, 0, multiple);
        return new Attack(currentDamage, false);
    }
}

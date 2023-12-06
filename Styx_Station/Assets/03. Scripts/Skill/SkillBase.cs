using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase 
{
    public abstract void UseSkill(GameObject attacker, GameObject defender);

    public Attack CreateAttackToMonster(ResultPlayerStats attacker, MonsterStats defender)
    {
        float criticalChance = attacker.GetCritical();   //0~100까지 치확
        float randomFloat = Random.Range(0f, 100f);
        randomFloat = Mathf.Round(randomFloat * 10f) / 10f;
        bool isCritical = randomFloat <= criticalChance;

        var currentDamage = attacker.ResultMonsterNormalDamage(isCritical, 0);

        return new Attack(currentDamage, false);
    }

}

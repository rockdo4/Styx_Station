using System.Collections;
using System.Numerics;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public BigInteger GetDamage(BigInteger currDamage, float damageMultiplier)
    {
        var temp = (currDamage * (int)damageMultiplier) / 100;
        return currDamage + temp;
    }

    public Attack CreateAttackToMonster(ResultPlayerStats attacker, MonsterStats defender, float multiple)
    {
        float criticalChance = attacker.GetCritical();   //0~100까지 치확
        float randomFloat = Random.Range(0f, 100f);
        randomFloat = Mathf.Round(randomFloat * 10f) / 10f;
        bool isCritical = randomFloat <= criticalChance;

        var currentDamage = attacker.ResultMonsterNormalDamage(isCritical, 0);
        currentDamage = GetDamage(currentDamage, multiple);
        return new Attack(currentDamage, false);
    }
}

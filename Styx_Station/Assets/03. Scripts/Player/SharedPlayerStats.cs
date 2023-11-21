using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class SharedPlayerStats 
{
    private static int playerPower;
    private static int playerPowerBoost;
    private static int playerPowerBoostMax = 4440;
    private static int attackSpeed;
    private static int attackCritical;
    private static int attackCriticalPower;
    private static int monsterDamagePower;
    private static int hp;
    private static int increaseHp;

    public static void IncreasePlayerPower()
    {
        playerPower++;
    }
    public static int GetPlayerPower()
    {
        return playerPower;
    }

    public static void IncreasePlayerPowerBoost()
    {
        playerPowerBoost++;
    }
    public static int GetPlayerPowerBoost()
    {
        if (playerPowerBoost > playerPowerBoostMax)
            playerPowerBoost = playerPowerBoostMax;
        return playerPowerBoost;
    }

    public static void IncreaseAttackSpeed()
    {
        attackSpeed++;
    }
    public static int GetAttackSpeed()
    {
        return attackSpeed;
    }

    public static void IncreaseAttackCritical()
    {
        attackCritical++;
    }
    public static int GetAttackCritical()
    {
        return attackCritical;
    }

    public static void IncreaseAttackCriticalPower()
    {
        attackCriticalPower++;
    }
    public static int GetAttackCriticlaPower()
    {
        return attackCriticalPower;
    }

    public static void IncreaseMonsterDamagePower()
    {
        monsterDamagePower++;
    }
    public static int GetMonsterDamagePower()
    { 
    return monsterDamagePower;
    }

    public static void IncreaseHp()
    {
        hp++;
    }
    public static int GetHp()
    {
        return hp;
    }

    public static void IncreaseIncreaseHp()
    {
        increaseHp++;
    }
    public static int GetIncreaseHp()
    {
        return increaseHp;
    }
}

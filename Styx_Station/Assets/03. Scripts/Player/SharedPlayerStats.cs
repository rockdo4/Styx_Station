using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class SharedPlayerStats 
{
    private static int playerPower=1;

    private static int playerPowerBoost=1;
    private static int playerPowerBoostMax = 4440;
    private static bool isPlayerPowerBoostMax = false;
    public static bool IsPlayerPowerBoostMax
    {
        get { return isPlayerPowerBoostMax; }
    }

    private static int attackSpeed=1;
    private static int attackSpeedMax = 3000;
    private static bool isAttackSpeedMax = false;
    public static bool IsAttackSpeedMax
    {
        get { return isAttackSpeedMax; }
    }

    private static int attackCritical=1;
    private static int attackCriticalMax = 1000;
    private static bool isAttackCriticalMax = false;
    public static bool IsAttackCriticalMax
    {
        get { return isAttackCriticalMax; }
    }

    private static int attackCriticalPower = 1;


    private static int monsterDamagePower=1;
    private static int monsterDamagePowerMax = 4440;
    private static bool isMonsterDamagePowerMax = false;
    public static bool IsMonsterDamagePowerMax
    {
        get { return isMonsterDamagePowerMax; }
    }


    private static int hp = 1;
    private static int healing =1 ;

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
        if(!isPlayerPowerBoostMax)
            playerPowerBoost++;

        if (playerPowerBoost >= playerPowerBoostMax) 
        {
            isPlayerPowerBoostMax = true;
            playerPowerBoost = playerPowerBoostMax;
        }
    }
    public static int GetPlayerPowerBoost()
    { 
        return playerPowerBoost;
    }



    public static void IncreaseAttackSpeed()
    {
        if(!isAttackSpeedMax)
            attackSpeed++;

        if(attackSpeed >= attackSpeedMax)
        {
            isAttackSpeedMax = true;
            attackSpeed = attackSpeedMax;
        }
    }
    public static int GetAttackSpeed()
    {
        return attackSpeed;
    }



    public static void IncreaseAttackCritical()
    {
        if(!isAttackCriticalMax)
            attackCritical++;

        if(attackCritical >= attackCriticalMax)
        {
            isAttackCriticalMax = true;
            attackCritical = attackCriticalMax;
        }
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
        if(!isMonsterDamagePowerMax)
            monsterDamagePower++;

        if(monsterDamagePower >= monsterDamagePowerMax)
        {
            isMonsterDamagePowerMax = true;
            monsterDamagePower = monsterDamagePowerMax;
        }
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


    public static void IncreaseHealing()
    {
        healing++;
    }
    public static int GetHealing()
    {
        return healing;
    }
}

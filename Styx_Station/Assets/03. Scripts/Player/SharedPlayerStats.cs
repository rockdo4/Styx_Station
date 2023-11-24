using System.Numerics;



public static class SharedPlayerStats 
{
    private static int playerPower=1;
    public static int PlayerPower { set { playerPower = value; } }

    private static int playerPowerBoost=1;
    public static int PlayerPowerBoost { set { playerPowerBoost = value; } }

    private static int playerPowerBoostMax = 4440;
    private static bool isPlayerPowerBoostMax = false;
    public static bool IsPlayerPowerBoostMax
    {
        get { return isPlayerPowerBoostMax; }
    }

    private static int playerAttackSpeed=1;
    public static int PlayerAttackSpeed { set { playerAttackSpeed = value; } }
    private static int attackSpeedMax = 3000;
    private static bool isAttackSpeedMax = false;
    public static bool IsAttackSpeedMax
    {
        get { return isAttackSpeedMax; }
    }

    private static int critical=1;
    public static int Critical { set { critical = value; } }
    private static int attackCriticalMax = 1000;
    private static bool isAttackCriticalMax = false;
    public static bool IsAttackCriticalMax
    {
        get { return isAttackCriticalMax; }
    }

    private static int criticalPower = 1;
    public static int CriticalPower { set { criticalPower = value; } }

    private static int monsterDamage=1;
    public static int MonsterDamage { set { monsterDamage = value; } }
    private static int monsterDamagePowerMax = 4440;
    private static bool isMonsterDamagePowerMax = false;
    public static bool IsMonsterDamagePowerMax
    {
        get { return isMonsterDamagePowerMax; }
    }


    private static int maxHp = 1;
    public static int MaxHp { set { maxHp = value; } }

    private static int healing =1 ;
    public static int Healing { set { healing = value; } }

    public static void IncreasePlayerPower()
    {
        price = 5000000000000;
        price *= playerPower;

        if (money1 > price)
        {
            money1 -= price;
            playerPower++;
        }
    }
    public static int GetPlayerPower()
    {
        return playerPower;
    }
    


    public static void IncreasePlayerPowerBoost()
    {
        if (!isPlayerPowerBoostMax)
        {
            price = 5000000000000;
            price *= playerPower;

            if (money1 > price)
            {
                money1 -= price;
                playerPowerBoost++;
            }
        }


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



    public static void IncreasePlayerAttackSpeed()
    {
        if (!isAttackSpeedMax)
        {
            price = 5000000000000;
            price *= playerPower;

            if (money1 > price)
            {
                money1 -= price;
                playerAttackSpeed++;
            }
        }

        if(playerAttackSpeed >= attackSpeedMax)
        {
            isAttackSpeedMax = true;
            playerAttackSpeed = attackSpeedMax;
        }
    }
    public static int GetPlayerAttackSpeed()
    {
        return playerAttackSpeed;
    }



    public static void IncreaseAttackCritical()
    {
        if (!isAttackCriticalMax)
        {
            price = 5000000000000;
            price *= playerPower;

            if (money1 > price)
            {
                money1 -= price;

                critical++;
            }
        }

        if(critical >= attackCriticalMax)
        {
            isAttackCriticalMax = true;
            critical = attackCriticalMax;
        }
    }
    public static int GetAttackCritical()
    {
        return critical;
    }


    public static void IncreaseAttackCriticalPower()
    {
        price = 5000000000000;
        price *= playerPower;

        if (money1 > price)
        {
            money1 -= price;
            criticalPower++;
        }

    }
    public static int GetAttackCriticlaPower()
    {
        return criticalPower;
    }


    public static void IncreaseMonsterDamagePower()
    {
        if (!isMonsterDamagePowerMax)
        {
            price = 5000000000000;
            price *= playerPower;

            if (money1 > price)
            {
                money1 -= price;

                monsterDamage++;
            }
        }

        if(monsterDamage >= monsterDamagePowerMax)
        {
            isMonsterDamagePowerMax = true;
            monsterDamage = monsterDamagePowerMax;
        }
    }
    public static int GetMonsterDamagePower()
    { 
        return monsterDamage;
    }


    public static void IncreaseHp()
    {
        price = 5000000000000;
        price *= playerPower;

        if (money1 > price)
        {
            money1 -= price;

            maxHp++;
        }
    }
    public static int GetHp()
    {
        return maxHp;
    }


    public static void IncreaseHealing()
    {
        price = 5000000000000;
        price *= playerPower;

        if (money1 > price)
        {
            money1 -= price;

            healing++;
        }
    }
    public static int GetHealing()
    {
        return healing;
    }


    public static BigInteger money1 = new BigInteger();
    public static BigInteger money2 = new BigInteger();
    public static BigInteger money3 = new BigInteger();
    private static BigInteger price = new BigInteger();

    public static int playerMaxHp = 0;
}

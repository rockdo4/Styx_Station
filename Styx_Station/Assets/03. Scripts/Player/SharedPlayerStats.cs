using System.Diagnostics;
using System.Numerics;

public static class SharedPlayerStats
{
    public static ResultPlayerStats resultPlayerStats;

    private static int playerPower = 1;
    public static int PlayerPower { set { playerPower = value; } }

    private static int playerPowerBoost = 1;
    public static int PlayerPowerBoost { set { playerPowerBoost = value; } }
    public static bool IsPlayerPowerBoostAmplifiable { get; set; }

    private static int playerPowerBoostMax = 4440;
    private static bool isPlayerPowerBoostMax = false;
    public static bool IsPlayerPowerBoostMax
    {
        get { return isPlayerPowerBoostMax; }
    }

    private static int playerAttackSpeed = 1;
    public static int PlayerAttackSpeed { set { playerAttackSpeed = value; } }
    private static int attackSpeedMax = 200;
    private static bool isAttackSpeedMax = false;
    public static bool IsAttackSpeedMax
    {
        get { return isAttackSpeedMax; }
    }

    private static int critical = 1;
    public static int Critical { set { critical = value; } }
    private static int attackCriticalMax = 1000;
    private static bool isAttackCriticalMax = false;
    public static bool IsAttackCriticalMax
    {
        get { return isAttackCriticalMax; }
    }

    private static int criticalPower = 1;
    public static int CriticalPower { set { criticalPower = value; } }


    private static int monsterDamage = 1;
    public static int MonsterDamage { set { monsterDamage = value; } }
    private static int monsterDamagePowerMax = 4440;
    private static bool isMonsterDamagePowerMax = false;
    public static bool IsMonsterDamagePowerMax
    {
        get { return isMonsterDamagePowerMax; }
    }


    private static int maxHp = 1;
    public static int MaxHp { set { maxHp = value; } }

    private static int healing = 1;
    public static int Healing { set { healing = value; } }

    private static BigInteger prevPrice = new BigInteger(0);

    public static void IncreasePlayerPower()
    {
        //if (resultPlayerStats != null)
        //{
        //    CurrencyManager.playerPowerPrice = resultPlayerStats.GetPlayerPowerByNonInventory();
        //} //캐릭터 바뀔까봐 넣었던코드
        //else
        //{
        //    CurrencyManager.playerPowerPrice = GetPlayerPower() * 100;
        //}

        prevPrice = CurrencyManager.playerPowerPrice;
        if (playerPower < 50)
            CurrencyManager.playerPowerPrice += 1;
        else if (playerPower < 500)
            CurrencyManager.playerPowerPrice += (playerPower - 1);
        else
            CurrencyManager.playerPowerPrice += (playerPower - 1) + (playerPower / 10);
        if (CurrencyManager.money1 > CurrencyManager.playerPowerPrice)
        {
            CurrencyManager.money1 -= CurrencyManager.playerPowerPrice;
            playerPower++;
            if(playerPower>=1000 &&!IsPlayerPowerBoostAmplifiable)
            {
                IsPlayerPowerBoostAmplifiable = true;
            }
        }
        else
        {
            CurrencyManager.playerPowerPrice = prevPrice;
        }
    }
    public static int GetPlayerPower()
    {
        return playerPower;
    }



    public static void IncreasePlayerPowerBoost()
    {
        if (!isPlayerPowerBoostMax && playerPower > 1000)
        {
            CurrencyManager.price = 5000;
            //CurrencyManager.price *= playerPower;

            if (CurrencyManager.money2 > CurrencyManager.price)
            {
                CurrencyManager.money2 -= CurrencyManager.price;
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
            prevPrice = CurrencyManager.playerAttackSpeedPrice;
            if (playerPower < 50)
                CurrencyManager.playerAttackSpeedPrice += 1;
            else if (playerPower < 500)
                CurrencyManager.playerAttackSpeedPrice += (playerAttackSpeed - 1);
            else
                CurrencyManager.playerAttackSpeedPrice += (playerAttackSpeed - 1) + (playerAttackSpeed / 10);
            if (CurrencyManager.money1 > CurrencyManager.playerAttackSpeedPrice)
            {
                CurrencyManager.money1 -= CurrencyManager.playerAttackSpeedPrice;
                playerAttackSpeed++;
            }
            else
            {
                CurrencyManager.playerAttackSpeedPrice = prevPrice;
            }
        }

        if (playerAttackSpeed >= attackSpeedMax)
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
            prevPrice = CurrencyManager.criticalPrice;
            if (playerPower < 50)
                CurrencyManager.criticalPrice += 1;
            else if (playerPower < 500)
                CurrencyManager.criticalPrice += (critical - 1);
            else
                CurrencyManager.criticalPrice += (critical - 1) + (critical / 10);
            if (CurrencyManager.money1 > CurrencyManager.criticalPrice)
            {
                CurrencyManager.money1 -= CurrencyManager.criticalPrice;

                critical++;
            }
            else
            {
                CurrencyManager.criticalPrice = prevPrice;
            }
        }

        if (critical >= attackCriticalMax)
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
        CurrencyManager.price = 5000;
        //CurrencyManager.price *= playerPower;

        if (CurrencyManager.money2 > CurrencyManager.price)
        {
            CurrencyManager.money2 -= CurrencyManager.price;
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
            CurrencyManager.price = 5000;

            if (CurrencyManager.money2 > CurrencyManager.price)
            {
                CurrencyManager.money2 -= CurrencyManager.price;

                monsterDamage++;
            }
        }

        if (monsterDamage >= monsterDamagePowerMax)
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
        prevPrice = CurrencyManager.maxHpPrice;
        if (playerPower < 50)
            CurrencyManager.maxHpPrice += 1;
        else if (playerPower < 500)
            CurrencyManager.maxHpPrice += (maxHp - 1);
        else
            CurrencyManager.maxHpPrice += (maxHp - 1) + (maxHp / 10);

        if (CurrencyManager.money1 > CurrencyManager.maxHpPrice)
        {
            CurrencyManager.money1 -= CurrencyManager.maxHpPrice;

            maxHp++;
        }
        else
        {
            CurrencyManager.maxHpPrice = prevPrice;
        }
    }
    public static int GetHp()
    {
        return maxHp;
    }


    public static void IncreaseHealing()
    {
        prevPrice = CurrencyManager.healingPrie;
        if (playerPower < 50)
            CurrencyManager.healingPrie += 1;
        else if (playerPower < 500)
            CurrencyManager.healingPrie += (healing - 1);
        else
            CurrencyManager.healingPrie += (healing - 1) + (healing / 10);

        if (CurrencyManager.money1 > CurrencyManager.healingPrie)
        {
            CurrencyManager.money1 -= CurrencyManager.healingPrie;

            healing++;
        }
        else
        {
            CurrencyManager.healingPrie = prevPrice;
        }
    }
    public static int GetHealing()
    {
        return healing;
    }


}

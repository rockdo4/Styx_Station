using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public static class SharedPlayerStats
{
    public static ResultPlayerStats resultPlayerStats;

    private static int playerPower = 1; //21 ¾ï 
    public static int PlayerPower { set { playerPower = value; } }

    private static int playerPowerBoost = 1;
    public static int PlayerPowerBoost { set { playerPowerBoost = value; } }
    public static bool IsPlayerPowerBoostAmplifiable { get; set; }


    private static int playerPowerBoostMax = 4441;
    private static bool isPlayerPowerBoostMax = false;
    public static bool IsPlayerPowerBoostMax
    {
        get { return isPlayerPowerBoostMax; }
    }

    private static int playerAttackSpeed = 1;
    public static int PlayerAttackSpeed { set { playerAttackSpeed = value; } }
    private static int attackSpeedMax = 301;
    private static bool isAttackSpeedMax = false;
    public static bool IsAttackSpeedMax
    {
        get { return isAttackSpeedMax; }
    }

    private static int critical = 1;
    public static int Critical { set { critical = value; } }
    private static int attackCriticalMax = 1001;
    private static bool isAttackCriticalMax = false;
    public static bool IsAttackCriticalMax
    {
        get { return isAttackCriticalMax; }
    }

    private static int criticalPower = 1;
    public static int CriticalPower { set { criticalPower = value; } }


    private static int monsterDamage = 1;
    public static int MonsterDamage { set { monsterDamage = value; } }
    private static int monsterDamagePowerMax = 4441;
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

    public static void CheckLimitAll()
    {
        PlayerPowerBoostCondition();
        PlayerPowerBoosMaxCondition();
        PlayerAttackSpeedMaxCondition();
        PlayerAttackCriticalMaxCondition();
        MonsterDamageMaxCondition();
    }

    public static void IncreasePlayerPower()
    {

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
            StateSystem.Instance.TotalUpdate();
            UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().InfoTextUpdate();
            PlayerPowerBoostCondition();

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
            prevPrice = CurrencyManager.playerPowerBoostPrice;
            if (playerPowerBoost < 50)
                CurrencyManager.playerPowerBoostPrice += 1;
            else if (playerPowerBoost < 500)
            {
                var price = CurrencyManager.playerPowerBoostPrice += (playerPowerBoost - 1);
                price /= 10;
                CurrencyManager.playerPowerBoostPrice = price;
            }
            else
            {
                var price = CurrencyManager.playerPowerBoostPrice += (playerPowerBoost - 1) + (playerPowerBoost / 10);
                price /= 10;
                CurrencyManager.playerPowerBoostPrice = price;
            }

            if (CurrencyManager.money2 > CurrencyManager.playerPowerBoostPrice)
            {
                CurrencyManager.money2 -= CurrencyManager.playerPowerBoostPrice;
                playerPowerBoost++;
                StateSystem.Instance.TotalUpdate();
                UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().InfoTextUpdate();

            }
            else
            {
                CurrencyManager.playerPowerBoostPrice = prevPrice;
            }
        }
        PlayerPowerBoosMaxCondition();
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
                StateSystem.Instance.TotalUpdate();
                UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().InfoTextUpdate();
            }
            else
            {
                CurrencyManager.playerAttackSpeedPrice = prevPrice;
            }
        }

        PlayerAttackSpeedMaxCondition();
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
                StateSystem.Instance.TotalUpdate();
                UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().InfoTextUpdate();
            }
            else
            {
                CurrencyManager.criticalPrice = prevPrice;
            }
        }

        PlayerAttackCriticalMaxCondition();
    }
    public static int GetAttackCritical()
    {
        return critical;
    }

    public static void IncreaseAttackCriticalPower()
    {
        prevPrice = CurrencyManager.criticalPowerPrice;
        if (criticalPower < 50)
            CurrencyManager.criticalPowerPrice += 1;
        else if (criticalPower < 500)
        {
            var price = CurrencyManager.criticalPowerPrice += (criticalPower - 1);
            price /= 10;
            CurrencyManager.criticalPowerPrice = price;
        }
        else
        {
            var price = CurrencyManager.criticalPowerPrice += (criticalPower - 1) + (criticalPower / 10);
            price /= 10;
            CurrencyManager.criticalPowerPrice = price;
        }

        if (CurrencyManager.money2 > CurrencyManager.criticalPowerPrice)
        {
            CurrencyManager.money2 -= CurrencyManager.criticalPowerPrice;
            criticalPower++;
            StateSystem.Instance.TotalUpdate();
            UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().InfoTextUpdate();
        }
        else
        {
            CurrencyManager.criticalPowerPrice = prevPrice;
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

            prevPrice = CurrencyManager.monsterDamagerPrice;
            if (monsterDamage < 50)
                CurrencyManager.monsterDamagerPrice += 1;
            else if (monsterDamage < 500)
            {
                var price = CurrencyManager.monsterDamagerPrice += (monsterDamage - 1);
                price /= 10;
                CurrencyManager.monsterDamagerPrice = price;
            }
            else
            {
                var price = CurrencyManager.monsterDamagerPrice += (monsterDamage - 1) + (monsterDamage / 10);
                price /= 10;
                CurrencyManager.monsterDamagerPrice = price;
            }

            if (CurrencyManager.money2 > CurrencyManager.monsterDamagerPrice)
            {
                CurrencyManager.money2 -= CurrencyManager.monsterDamagerPrice;

                monsterDamage++;
                StateSystem.Instance.TotalUpdate();
                UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().InfoTextUpdate();
            }
            else
                CurrencyManager.monsterDamagerPrice = prevPrice;
        }

        MonsterDamageMaxCondition();
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
            StateSystem.Instance.TotalUpdate();
            UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().InfoTextUpdate();
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
        prevPrice = CurrencyManager.healingPrice;
        if (playerPower < 50)
            CurrencyManager.healingPrice += 1;
        else if (playerPower < 500)
            CurrencyManager.healingPrice += (healing - 1);
        else
            CurrencyManager.healingPrice += (healing - 1) + (healing / 10);

        if (CurrencyManager.money1 > CurrencyManager.healingPrice)
        {
            CurrencyManager.money1 -= CurrencyManager.healingPrice;

            healing++;
            StateSystem.Instance.TotalUpdate();
            UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().InfoTextUpdate();
        }
        else
        {
            CurrencyManager.healingPrice = prevPrice;
        }
    }

    private static void PlayerPowerBoostCondition()
    {
        if (playerPower > 1000 && !IsPlayerPowerBoostAmplifiable)
        {
            IsPlayerPowerBoostAmplifiable = true;
        }
    }

    private static void PlayerPowerBoosMaxCondition()
    {
        if (playerPowerBoost >= playerPowerBoostMax)
        {
            isPlayerPowerBoostMax = true;
            playerPowerBoost = playerPowerBoostMax;
        }
    }

    private static void PlayerAttackSpeedMaxCondition()
    {
        if (playerAttackSpeed >= attackSpeedMax)
        {
            isAttackSpeedMax = true;
            playerAttackSpeed = attackSpeedMax;
        }
    }

    private static void PlayerAttackCriticalMaxCondition()
    {
        if (critical >= attackCriticalMax)
        {
            isAttackCriticalMax = true;
            critical = attackCriticalMax;
        }
    }

    private static void MonsterDamageMaxCondition()
    {
        if (monsterDamage >= monsterDamagePowerMax)
        {
            isMonsterDamagePowerMax = true;
            monsterDamage = monsterDamagePowerMax;
        }
    }

    public static int GetHealing()
    {
        return healing;
    }


    public static void ResetAll()
    {
        playerPower = 1;
        playerPowerBoost = 1;
        playerAttackSpeed = 1;
        critical = 1;
        criticalPower = 1;
        monsterDamage = 1;
        maxHp = 1;
        healing = 1;
    }

}

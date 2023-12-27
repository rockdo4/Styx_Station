using System;
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
    private static BigInteger ten = new BigInteger(10);
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
        if (playerPower < 99)
        {
            var price = Math.Pow(1.07303, playerPower);
            var pr = Math.Round(price);
            CurrencyManager.playerPowerPrice = (int)pr;
        }
        else
        {
            var price = CurrencyManager.playerPowerPrice;
            var length = price.ToString().Length;
            length =length- 3;
            var t = BigInteger.Pow(ten, length);
            CurrencyManager.playerPowerPrice += t;
        }
        if (CurrencyManager.money1 > CurrencyManager.playerPowerPrice)
        {
            CurrencyManager.money1 -= CurrencyManager.playerPowerPrice;
            playerPower++;
            StateSystem.Instance.TotalUpdate();
            UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().InfoTextUpdate();
            PlayerPowerBoostCondition();

            if(UIManager.Instance.questSystemUi.currentQuestType ==QuestType.CheckPlayerStats)
                UIManager.Instance.questSystemUi.CheckPlayerStatsUpgradeClear();

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
            if (playerPowerBoost < 99)
            {
                var price2 = Math.Pow(1.07303, playerPowerBoost);
                price2 /= 2;
                var pr = Math.Round(price2);
                CurrencyManager.playerPowerBoostPrice = (int)pr;
            }
            else
            {
                var pr = prevPrice / 10;
                pr /= 2;
                CurrencyManager.playerPowerBoostPrice += pr;
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
            if (playerAttackSpeed < 99)
            {
                var price = Math.Pow(1.07303, playerAttackSpeed);
                var pr = Math.Round(price);
                CurrencyManager.playerAttackSpeedPrice = (int)pr;
            }
            else
            {
                var or = CurrencyManager.playerAttackSpeedPrice;
                var length = or.ToString().Length;
                length = length - 3;
                var t = BigInteger.Pow(ten, length);
                CurrencyManager.playerAttackSpeedPrice += t;
            }
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
            if (critical < 99)
            {
                var price = Math.Pow(1.07303, critical);
                var pr = Math.Round(price);
                CurrencyManager.criticalPrice = (int)pr;
            }
            else
            {
                var or = CurrencyManager.criticalPrice;
                var length = or.ToString().Length;
                length = length - 3;
                var t = BigInteger.Pow(ten, length);
                CurrencyManager.criticalPrice += t;
            }
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
        if (criticalPower < 99)
        {
            var price2 = Math.Pow(1.07303, criticalPower);
            price2 /= 2;
            var pr = Math.Round(price2);
            CurrencyManager.criticalPowerPrice = (int)pr;
        }
        else
        {
            var pr = prevPrice / 10;
            pr /= 2;
            CurrencyManager.criticalPowerPrice += pr;
        }

        if (CurrencyManager.money2 > CurrencyManager.criticalPowerPrice)
        {
            CurrencyManager.money2 -= CurrencyManager.criticalPowerPrice;
            criticalPower++;
            StateSystem.Instance.TotalUpdate();
            UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().InfoTextUpdate();

            if (UIManager.Instance.questSystemUi.currentQuestType == QuestType.CheckPlayerStats)
                UIManager.Instance.questSystemUi.CheckPlayerStatsUpgradeClear();
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
            if (monsterDamage < 99)
            {
                var price2 = Math.Pow(1.07303, monsterDamage);
                price2 /= 2;
                var pr = Math.Round(price2);
                CurrencyManager.monsterDamagerPrice = (int)pr;
            }
            else
            {
                var pr = prevPrice / 10;
                pr /= 2;
                CurrencyManager.monsterDamagerPrice += pr;
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
        if (maxHp < 99)
        {
            var price = Math.Pow(1.07303, maxHp);
            var pr = Math.Round(price);
            CurrencyManager.maxHpPrice = (int)pr;
        }
        else
        {
            var or = CurrencyManager.maxHpPrice;
            var length = or.ToString().Length;
            length = length - 3;
            var t = BigInteger.Pow(ten, length);
            CurrencyManager.maxHpPrice += t;
        }

        if (CurrencyManager.money1 > CurrencyManager.maxHpPrice)
        {
            CurrencyManager.money1 -= CurrencyManager.maxHpPrice;

            maxHp++;
            StateSystem.Instance.TotalUpdate();
            UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().InfoTextUpdate();
            if (UIManager.Instance.questSystemUi.currentQuestType == QuestType.CheckPlayerStats)
                UIManager.Instance.questSystemUi.CheckPlayerStatsUpgradeClear();
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
        if (healing < 99)
        {
            var price = Math.Pow(1.07303, healing);
            var pr = Math.Round(price);
            CurrencyManager.healingPrice = (int)pr;
        }
        else
        {
            var or = CurrencyManager.healingPrice;
            var length = or.ToString().Length;
            length = length - 3;
            var t = BigInteger.Pow(ten, length);
            CurrencyManager.healingPrice += t;
        }

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

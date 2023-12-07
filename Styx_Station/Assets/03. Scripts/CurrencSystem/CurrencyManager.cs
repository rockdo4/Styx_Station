using System.Numerics;
using UnityEngine;

public static class CurrencyManager 
{
    public static BigInteger money1 = new BigInteger();
    public static BigInteger money2 = new BigInteger();
    public static BigInteger money3 = new BigInteger();
    public static BigInteger price = new BigInteger();

    public static BigInteger playerPowerPrice = new BigInteger(1);
    public static BigInteger playerPowerBoostPrice = new BigInteger(1); // Ç÷·ù¼®
    public static BigInteger playerAttackSpeedPrice = new BigInteger(1);
    public static BigInteger criticalPrice = new BigInteger(1);
    public static BigInteger criticalPowerPrice = new BigInteger(1);// Ç÷·ù¼®
    public static BigInteger monsterDamagerPrice =new BigInteger(1);// Ç÷·ù¼®
    public static BigInteger maxHpPrice = new BigInteger(1);
    public static BigInteger healingPrice =new BigInteger(1);

    public static int itemAsh = 0;


    public static void GetSilver(BigInteger money,int a)
    {
        switch(a)
        {
            case 0:
                money1 += money;
                break;

            case 1:
                money2 += money;
                break;

            case 2:
                money3 += money;
                break;
        }
    }
    public static void IncreaseMoney1(BigInteger imoney)
    {
        imoney = 10;
        money1 += imoney * imoney;
    }
    public static void IncreaseMoney2(BigInteger imoney)
    {
        imoney = 10;
        money2 += imoney * imoney;
    }
    public static void IncreaseMoney3(BigInteger imoney)
    {
        imoney = 10;
        money3 += imoney * imoney;
    }


    public static void SetPlayerStatsAllRest()
    {
        var playerPower = SharedPlayerStats.GetPlayerPower();
        for(int i=1;i<playerPower;++i)
        {
            if (i < 50)
                playerPowerPrice++;
            else if( i <500)
            {
                playerPowerPrice += (playerPower - 1);
            }
            else
            {
                playerPowerPrice += (playerPower - 1) + (playerPower / 10);
            }
        }
        var playerPowerBoost = SharedPlayerStats.GetPlayerPowerBoost();
        for (int i = 1; i < playerPowerBoost; ++i)
        {
            if (i < 50)
                playerPowerBoostPrice++;
            else if (i < 500)
            {
                playerPowerBoostPrice += (playerPower - 1);
                playerPowerBoostPrice /= 10;
            }
            else
            {
                playerPowerBoostPrice += (playerPower - 1) + (playerPower / 10);
                playerPowerBoostPrice /= 10;
            }
        }
    }
}

public enum MoneyType    
{
    money1,
    money2,
    money3,
}

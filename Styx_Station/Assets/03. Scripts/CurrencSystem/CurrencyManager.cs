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
                money1 += money + ((money * GameData.labBuffData.re_Sliup) / GameData.labBuffDataPercent);
                money1 += money + ((money *PlayerBuff.Instance.buffData.silingBuff)*PlayerBuff.Instance.percent);
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
        SetSliverPrice(SharedPlayerStats.GetPlayerPower(), ref playerPowerPrice);
        SetPomegranatePrice(SharedPlayerStats.GetPlayerPowerBoost(), ref playerPowerBoostPrice);
        SetSliverPrice(SharedPlayerStats.GetPlayerAttackSpeed(), ref playerAttackSpeedPrice);
        SetSliverPrice(SharedPlayerStats.GetAttackCritical(), ref criticalPrice);
        SetPomegranatePrice(SharedPlayerStats.GetAttackCriticlaPower(), ref criticalPowerPrice);
        SetPomegranatePrice(SharedPlayerStats.GetMonsterDamagePower(), ref monsterDamagerPrice);
        SetSliverPrice(SharedPlayerStats.GetHp(), ref maxHpPrice);
        SetSliverPrice(SharedPlayerStats.GetHealing(), ref healingPrice);
    }
    private static void SetSliverPrice(int loopSize ,ref BigInteger price)
    {
        price = 0;
        if(loopSize <=1) 
        {
            price = 1;
            return;
        }
        for (int i=1;i<loopSize;++i)
        {
            if (i < 50)
                price++;
            else if (i < 500)
            {
                price += (i - 1);
            }
            else
            {
                price += (i - 1) + (i / 10);
            }
        }
    }
    private static void SetPomegranatePrice(int loopSize, ref BigInteger price)
    {
        price = 0;
        if (loopSize <= 1)
        {
            price = 1;
            return;
        }
        for (int i = 1; i < loopSize; ++i)
        {
            if (i < 50)
                price++;
            else if (i < 500)
            {
                price += (i - 1);
                price /= 2;
            }
            else
            {
                price += (i - 1) + (i / 10);
                price /= 2;
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

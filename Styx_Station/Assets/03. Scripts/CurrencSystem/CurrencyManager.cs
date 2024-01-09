using System;
using System.Numerics;

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

    public static BigInteger itemAsh = new BigInteger();

    private static BigInteger ten=new BigInteger(10);
    public static void GetSilver(BigInteger money,int a)
    {
        switch(a)
        {
            case 0:
                money1 += money + (money * (BigInteger)StateSystem.Instance.TotalState.CoinAcquire / 100);
                break;
            case 1:
                money2 += money;
                break;

            case 2:
                money3 += money;
                break;
            case 3:
                itemAsh += money;
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
    public static void IncreaseMoney4(BigInteger imoney)
    {
        imoney = 10;
        itemAsh += imoney * imoney;
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
            if (i < 99)
            {
                var price2 = Math.Pow(1.07303, i);
                var pr = Math.Round(price2);
                price = (int)pr;
            }
            else
            {
                //var pr = price / 10;
                //price += pr;

                var or = price.ToString().Length;
                or = or - 3;
                var t = BigInteger.Pow(ten, or);
                price += t;
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
            if (i < 99)
            {
                var price2 = Math.Pow(1.07303, i);
                price2 /= 2;
                var pr = Math.Round(price2);
                price = (int)pr;
            }
            else
            {
                var pr = price / 10;
                pr /= 2;
                price += pr;
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

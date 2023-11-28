using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public static class CurrencyManager 
{
    public static BigInteger money1 = new BigInteger();
    public static BigInteger money2 = new BigInteger();
    public static BigInteger money3 = new BigInteger();
    public static BigInteger price = new BigInteger();

    public static BigInteger playerPowerPrice = new BigInteger();   

    public static void IncreaseMoney1(BigInteger imoney)
    {
        imoney = 1000000000;
        money1 += imoney * imoney;
    }
    public static void IncreaseMoney2(BigInteger imoney)
    {
        imoney = 1000000000;
        money2 += imoney * imoney;
    }
    public static void IncreaseMoney3(BigInteger imoney)
    {
        imoney = 1000000000;
        money3 += imoney * imoney;
    }
}

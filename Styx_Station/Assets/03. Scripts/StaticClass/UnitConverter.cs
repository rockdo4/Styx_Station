using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public static class UnitConverter
{
    private static bool isInit = false;
    private static bool IsInit
    {
        get { return isInit; }
        set
        {
            if (!isInit)
                isInit = value;
        }
    }

    private static BigInteger a1 = new BigInteger(1000);
    private static BigInteger[] units = new BigInteger[52];
    private static string[] unitNames = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", 
        "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z","AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", 
        "AL", "AM", "AN","AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ"};
    public static void InitUnitConverter()
    {
        if (!IsInit)
        {
            units[0] = a1;
            
            for (int i = 1; i < units.Length; i++)
            {
                units[i] = units[i - 1] * a1;
            }
            IsInit = true;
        }
    }

    public static BigInteger A1
    {
        get
        {
            return a1;
        }
    }

    public static string OutString(BigInteger money)
    {
        if (money == null)
        {
            Debug.Log("Err");
            return "None";
        }
        if (money < a1)
        {
            return money.ToString();
        }
        for (int i = 0; i < units.Length; i++)
        {
            if (money >= units[i] && (i == units.Length - 1 || money < units[i + 1]))
            {
                int first = (int)(money / units[i]);
                int second = (int)((money % units[i]) / (units[i] / 100));
                return $"{first}.{second}{unitNames[i]}";
            }
        }

        return "None";
    }
}

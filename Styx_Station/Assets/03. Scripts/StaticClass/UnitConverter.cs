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
    private static BigInteger[] units = new BigInteger[702];

    private static string[] unitNames;
    public static void InitUnitConverter()
    {
        if (!IsInit)
        {
            InitUnitNames();
            units[0] = a1;
            for (int i = 1; i < units.Length; i++)
            {
                units[i] = units[i - 1] * a1;
            }
            IsInit = true;
        }
    }
    private static void InitUnitNames()
    {
        List<string> namesList = new List<string>();
        for (char c1 = 'A'; c1 <= 'Z'; c1++)
        {
            namesList.Add(c1.ToString());
            //for (char c2 = 'A'; c2 <= 'Z'; c2++)
            //{
            //    namesList.Add($"{c1}{c2}");
            //}
        }
        for (char c1 = 'A'; c1 <= 'Z'; c1++)
        {
            for (char c2 = 'A'; c2 <= 'Z'; c2++)
            {
                namesList.Add($"{c1}{c2}");
            }
        }
        unitNames = namesList.ToArray();
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
                if(second<10)
                    return $"{first}.0{second}{unitNames[i]}";
                else return $"{first}.{second}{unitNames[i]}";
            }
        }

        return null;
    }
}

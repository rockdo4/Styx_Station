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
            if(!isInit)
                isInit = value;
        }
    }

    private static BigInteger a1 = new BigInteger(1000);
    private static BigInteger b1 = new BigInteger(0);
    private static BigInteger c1 = new BigInteger(0);
    private static BigInteger d1 = new BigInteger(0);
    private static BigInteger e1 = new BigInteger(0);
    private static BigInteger f1 = new BigInteger(0);
    private static BigInteger g1 = new BigInteger(0);
    private static BigInteger h1 = new BigInteger(0);
    private static BigInteger i1 = new BigInteger(0);
    private static BigInteger j1 = new BigInteger(0);
    private static BigInteger k1 = new BigInteger(0);
    private static BigInteger l1 = new BigInteger(0);
    private static BigInteger m1 = new BigInteger(0);
    private static BigInteger n1 = new BigInteger(0);
    private static BigInteger o1 = new BigInteger(0);
    private static BigInteger p1 = new BigInteger(0);
    private static BigInteger q1 = new BigInteger(0);
    private static BigInteger r1 = new BigInteger(0);
    private static BigInteger s1 = new BigInteger(0);
    private static BigInteger t1 = new BigInteger(0);
    private static BigInteger u1 = new BigInteger(0);
    private static BigInteger v1 = new BigInteger(0);
    private static BigInteger w1 = new BigInteger(0);
    private static BigInteger x1 = new BigInteger(0);
    private static BigInteger y1 = new BigInteger(0);
    private static BigInteger z1 = new BigInteger(0);

    public static void InitUnitConverter()
    {
        if(!IsInit)
        {
            b1 = a1 * a1;
            c1 = b1 * a1;
            d1 = c1 * a1;
            e1 = d1 * a1;
            f1 = e1 * a1;
            g1 = f1 * a1;
            h1 = g1 * a1;
            i1 = h1 * a1;
            j1 = i1 * a1;
            k1 = j1 * a1;
            l1 = k1 * a1;
            m1 = l1 * a1;
            n1 = m1 * a1;
            o1 = n1 * a1;
            p1 = o1 * a1;
            q1 = p1 * a1;
            r1 = q1 * a1;
            s1 = r1 * a1;
            t1 = s1 * a1;
            u1 = t1 * a1;
            v1 = u1 * a1;
            w1 = v1 * a1;
            x1 = w1 * a1;
            y1 = x1 * a1;
            z1 = y1 * a1;

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
        if(money == null) 
        {
            Debug.Log("Err");
            return "None";
        }
        else if(money <a1)
        {
            return money.ToString();
        }
        else if(money >=a1 && money < b1)
        {
            int frist = (int)(money / a1);
            int second = (int)((money % a1) / 10);
            return $"{frist}.{second}A";
        }
        else if(money >=b1 && money <c1)
        {
            int frist = (int)(money / b1);
            int second = (int)((money % b1) / (10*a1));
            return $"{frist}.{second}B";
        }
        else if (money >= c1 && money < d1)
        {
            int frist = (int)(money / c1);
            int second = (int)((money % c1) / (10 * b1));
            return $"{frist}.{second}C";
        }
        else if (money >= d1 && money < e1)
        {
            int frist = (int)(money / d1);
            int second = (int)((money % d1) / (10 * c1));
            return $"{frist}.{second}D";
        }
        else if (money >= e1 && money < f1)
        {
            int frist = (int)(money / e1);
            int second = (int)((money % e1) / (10 * d1));
            return $"{frist}.{second}E";
        }
        else if (money >= f1 && money < g1)
        {
            int frist = (int)(money / f1);
            int second = (int)((money % f1) / (10 * e1));
            return $"{frist}.{second}F";
        }
        else if (money >= g1 && money < h1)
        {
            int frist = (int)(money / g1);
            int second = (int)((money % g1) / (10 * f1));
            return $"{frist}.{second}G";
        }
        else if (money >= h1 && money < i1)
        {
            int frist = (int)(money / h1);
            int second = (int)((money % h1) / (10 * g1));
            return $"{frist}.{second}H";
        }
        else if (money >= i1 && money < j1)
        {
            int frist = (int)(money / i1);
            int second = (int)((money % i1) / (10 * h1));
            return $"{frist}.{second}I";
        }
        else if (money >= j1 && money < k1)
        {
            int frist = (int)(money / j1);
            int second = (int)((money % j1) / (10 * i1));
            return $"{frist}.{second}J";
        }
        else if (money >= k1 && money < l1)
        {
            int frist = (int)(money / k1);
            int second = (int)((money % k1) / (10 * j1));
            return $"{frist}.{second}K";
        }
        else if (money >= l1 && money < m1)
        {
            int frist = (int)(money / l1);
            int second = (int)((money % l1) / (10 * k1));
            return $"{frist}.{second}L";
        }
        else if (money >= m1 && money < n1)
        {
            int frist = (int)(money / m1);
            int second = (int)((money % m1) / (10 * l1));
            return $"{frist}.{second}M";
        }
        else if (money >= n1 && money < o1)
        {
            int frist = (int)(money / n1);
            int second = (int)((money % n1) / (10 * m1));
            return $"{frist}.{second}N";
        }
        else if (money >= o1 && money < p1)
        {
            int frist = (int)(money / o1);
            int second = (int)((money % o1) / (10 * n1));
            return $"{frist}.{second}O";
        }
        else if (money >= p1 && money < q1)
        {
            int frist = (int)(money / p1);
            int second = (int)((money % p1) / (10 * o1));
            return $"{frist}.{second}P";
        }
        else if (money >= q1 && money < r1)
        {
            int frist = (int)(money / q1);
            int second = (int)((money % q1) / (10 * p1));
            return $"{frist}.{second}Q";
        }
        else if (money >= r1 && money < s1)
        {
            int frist = (int)(money / r1);
            int second = (int)((money % r1) / (10 * q1));
            return $"{frist}.{second}R";
        }
        else if (money >= s1 && money < t1)
        {
            int frist = (int)(money / s1);
            int second = (int)((money % s1) / (10 * r1));
            return $"{frist}.{second}S";
        }
        else if (money >= t1 && money < u1)
        {
            int frist = (int)(money / t1);
            int second = (int)((money % t1) / (10 * s1));
            return $"{frist}.{second}T";
        }
        else if (money >= u1 && money < v1)
        {
            int frist = (int)(money / u1);
            int second = (int)((money % u1) / (10 * t1));
            return $"{frist}.{second}U";
        }
        else if (money >= v1 && money < w1)
        {
            int frist = (int)(money / v1);
            int second = (int)((money % v1) / (10 * u1));
            return $"{frist}.{second}V";
        }
        else if (money >= w1 && money < x1)
        {
            int frist = (int)(money / w1);
            int second = (int)((money % w1) / (10 * v1));
            return $"{frist}.{second}W";
        }
        else if (money >= x1 && money < y1)
        {
            int frist = (int)(money / w1);
            int second = (int)((money % w1) / (10 * w1));
            return $"{frist}.{second}X";
        }
        else if (money >= y1 && money < z1)
        {
            int frist = (int)(money / y1);
            int second = (int)((money % y1) / (10 * x1));
            return $"{frist}.{second}Y";
        }
        else if (money >= z1 )
        {
            int frist = (int)(money / z1);
            int second = (int)((money % z1) / (10 * y1));
            return $"{frist}.{second}Z";
        }

        return "None";

    }
}

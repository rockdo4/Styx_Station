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
        b1 = a1 * a1;
        c1 = b1*a1;
        d1 = c1*a1;
        e1 = d1 * a1;
        f1 = e1 * a1;
        g1 = f1 *a1;
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


        Debug.Log(a1);
        Debug.Log(b1);
        Debug.Log(c1);
        Debug.Log(d1);
        Debug.Log(e1);
        Debug.Log(f1);
        Debug.Log(g1);
        Debug.Log(h1);
        Debug.Log(i1);
        Debug.Log(j1);
        Debug.Log(k1);
        Debug.Log(l1);
        Debug.Log(m1);
        Debug.Log(n1);
        Debug.Log(o1);
        Debug.Log(p1);
        Debug.Log(q1);
        Debug.Log(r1);
        Debug.Log(s1);
        Debug.Log(t1);
        Debug.Log(u1);
        Debug.Log(v1);
        Debug.Log(w1);
        Debug.Log(x1);
        Debug.Log(y1);
        Debug.Log(z1);



    }

    public static BigInteger A1
    {
        get
        {
            return a1;
        }
    }
   
}

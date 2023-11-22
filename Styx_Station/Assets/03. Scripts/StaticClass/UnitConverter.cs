using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitConverter  
{
    const int a1 = 1000;
    const int b1 = a1 * a1;
    const int c1 = b1 * a1;
    const long d1 = (long)c1 * a1;

    public static int A1
    {
        get
        {
            return a1;
        }
    }
    public static int B1
    {
        get
        {
            return b1;
        }
    }
    public static int C1
    {
        get
        {
            return c1;
        }
    }
    public static long D1
    {
        get
        {
            return d1;
        }
    }
}

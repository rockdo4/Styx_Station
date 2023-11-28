using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public struct Attack 
{
    public BigInteger Damage { get; private set; }
    public bool IsCritical { get; private set; }    
    public Attack(BigInteger damage, bool critical)
    {
        Damage = damage;
        IsCritical = critical;
    }
}

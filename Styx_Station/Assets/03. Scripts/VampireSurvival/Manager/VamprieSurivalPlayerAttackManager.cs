using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VamprieSurivalPlayerAttackManager : PoolAble
{
    protected Vector2 direction;
    public float range;
    public float speed;
    public float aliveTime;
    [HideInInspector]
    public float nowTime;
    public float coolTime;
    public VamprieSurivalAttackType attackType;
    public abstract void LineAttackRange(Vector2 position);
}

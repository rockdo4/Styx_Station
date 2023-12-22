using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VamprieSurivalPlayerAttackType : PoolAble
{
    public int damage;
    protected Vector2 direction;
    protected float timer;
    public float speed;
    public float aliveTime;
    [HideInInspector]
    public abstract void LineAttackRange(Vector2 position);

    public virtual void Setting(int damage , float speed,float aliveTime)
    {
        this.damage = damage;
        this.speed = speed;  
        this.aliveTime = aliveTime;
    }

    public override void ReleaseObject()
    {
        if (gameObject.activeSelf)
        {
            ObjPool.Release(gameObject);
        }
    }
}

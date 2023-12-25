using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VamprieSurivalPlayerAttackType : PoolAble
{
    public float damage;
    protected Vector2 direction;
    protected float timer;
    public float speed;
    public float aliveTime;
    protected float range;
    [HideInInspector]
    public abstract void LineAttackRange(Vector2 position);

    public virtual void Setting(float damage , float speed,float aliveTime,float range=0)
    {
        this.damage = damage;
        this.speed = speed;  
        this.aliveTime = aliveTime;
        this.range = range;
    }

    public override void ReleaseObject()
    {
        if (gameObject.activeSelf)
        {
            ObjPool.Release(gameObject);
        }
    }
}

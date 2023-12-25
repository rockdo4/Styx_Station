using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VamprieSurivalAttackArrow : VamprieSurivalPlayerAttackType
{
    private Rigidbody2D rb;
    private bool isStartObject;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var vel = rb.velocity;
        vel += direction * speed * Time.fixedDeltaTime;
        rb.velocity = vel;
    }
    private void Update()
    {
        if(isStartObject)
        {
            timer += Time.deltaTime;
            if(timer >= aliveTime)
                ReleaseObject();
        }
    }

    public override void LineAttackRange(Vector2 position)
    {
        timer = 0f;
        isStartObject =true;
        direction = position;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider.CompareTag("VampireEnemy"))
    //    {
    //        var mon = collision.collider.GetComponent<VampireSurivalMonster>();
    //        mon.GetDamage(damage);
    //        ReleaseObject();
    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("VampireEnemy"))
    //    {
    //        var mon = other.GetComponent<VampireSurivalMonster>();
    //        mon.GetDamage(damage);
    //        ReleaseObject();
    //    }
    //}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VamprieSurivalAttackArrow : VamprieSurivalPlayerAttackManager
{
    private Rigidbody2D rb;
    public float timer;
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
        if (isStartObject)
        {
            timer += Time.deltaTime;
        }
        if(timer >=aliveTime)
        {
            ReleaseObject();
        }
    }

    public override void LineAttackRange(Vector2 position)
    {
        timer = 0f;
        isStartObject =true;
        direction = position;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.collider.CompareTag("VampireEnemy"))
    //    {
    //        collision.collider.GetComponent<VampireSurivalMonster>().GetDamage(damage);
    //        ReleaseObject();
    //    }
    //}

}

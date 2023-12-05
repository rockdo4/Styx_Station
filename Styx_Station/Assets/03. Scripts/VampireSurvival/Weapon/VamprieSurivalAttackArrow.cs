using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VamprieSurivalAttackArrow : VamprieSurivalPlayerAttackManager
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, aliveTime);
    }

    private void FixedUpdate()
    {
        var vel = rb.velocity;
        vel += direction * speed * Time.fixedDeltaTime;
        rb.velocity = vel;
    }
    public override void LineAttackRange(Vector2 position)
    {
        direction = position;
    }
}

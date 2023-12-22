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
        isStartObject =true;
        direction = position;
    }
}

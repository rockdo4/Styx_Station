using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VampireSliverItem : PoolAble
{
    private GameObject player;
    public float speed = 1f;
    private Rigidbody2D rb;
    public float eatDistance=0.2f;

    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(player!=null)
        {
            var direction = (player.transform.position - transform.position).normalized;
            var pos = rb.position;
            pos += (Vector2)(direction * speed * Time.deltaTime);
            rb.MovePosition(pos);

            if (Vector2.Distance(rb.position, (Vector2)player.transform.position) < eatDistance)
            {
                VampireSurvivalGameManager.Instance.sliver++;
                ReleaseObject();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("VampireEatItem"))
            player = collision.gameObject;
        
    }
}

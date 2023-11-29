using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBow : PoolAble
{
    public event Action<GameObject, GameObject> OnCollided;
    private GameObject attacker;
    public float speed;
    public GameObject targetPos;

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        var vel = rb.velocity;
        Vector2 dir = (targetPos.transform.position-gameObject.transform.position ).normalized;
        var movePos = dir * speed * Time.deltaTime;
        vel += movePos;
        rb.velocity = vel;
    }
    private void Update()
    {
        
    }
    public void SetPetBow(GameObject obj, float speed, GameObject targetPos)
    {
        attacker = obj;
        this.speed = speed;
        this.targetPos = targetPos;
    }

    public bool CheckOnCollided() 
    {
        return OnCollided != null;
    }
}

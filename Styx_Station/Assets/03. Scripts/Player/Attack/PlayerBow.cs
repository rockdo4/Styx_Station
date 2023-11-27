using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerBow : PoolAble
{
    public event Action<GameObject, GameObject> OnCollided;

    private Rigidbody2D rb;
    private float speed; //속도
    private Vector3 targetPos; //타겟 위치 - 마지막 위치
    private Vector3 startPos; //시작 위치
    private GameObject caster; //공격자
    private float increaseAttackSpeed = 0.01f;
    private bool isRelease = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   

    public void Fire(GameObject c, float s, Vector3 t)
    {
        speed = s;
        speed += ((SharedPlayerStats.GetPlayerAttackSpeed() - 1) * increaseAttackSpeed);
        targetPos = t;
        caster = c;
        startPos = transform.position;
    }

    private void OnEnable()
    {
        isRelease = false;
    }

    private void FixedUpdate()
    { 
        Vector2 direction = (targetPos - startPos).normalized;
        var moveVector = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveVector);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        Collider2D attackedMon = colliders[0];
        if(colliders.Length > 1)
        {
            foreach(Collider2D collider in colliders)
            {
                if (collider.gameObject.GetComponentInChildren<SortingGroup>().sortingOrder > attackedMon.GetComponentInChildren<SortingGroup>().sortingOrder)
                {
                    attackedMon = collider;
                }
            }
        }
        if (OnCollided != null)
            OnCollided(caster, attackedMon.gameObject);
        //Debug.Log(other.name);
        if (!isRelease)
        {
            isRelease = true;
            ReleaseObject();
        }
        
    }
}

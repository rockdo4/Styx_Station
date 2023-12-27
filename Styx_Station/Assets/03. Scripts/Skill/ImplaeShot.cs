using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplaeShot : PoolAble
{
    public event Action<GameObject, GameObject> OnCollided;

    public bool isPositionSet = false;
    private bool hasBeenHit = false;
    private GameObject caster;
    private Vector2 initialPos = Vector2.zero;
    private LayerMask targetLayer;

    private WaitForSeconds wait = new WaitForSeconds(0.5f);
    IEnumerator WaitForRelease()
    {
        yield return wait;
        ReleaseObject();
    }

    private void OnEnable()
    {
        initialPos = transform.position;
    }

    private void Update()
    {
        if(isPositionSet)
        {
            StartCoroutine(WaitForRelease());

            if (!hasBeenHit)
            {
                CheckCollision();
            }
        }
    }
    public void SetImpaleShot(Vector2 pos, GameObject c)
    {
        gameObject.transform.position = pos;
        isPositionSet = true;
        caster = c;
        targetLayer = 1 << LayerMask.NameToLayer("Enemy");
    }
    public override void ReleaseObject()
    {
        isPositionSet = false;
        hasBeenHit = false;
        transform.position = initialPos;
        base.ReleaseObject();
    }

    public bool CheckOnCollided()
    {
        return OnCollided != null;
    }

    private void CheckCollision()
    {
        Vector2 center = transform.position; // Áß½ÉÁ¡
        Vector2 size = gameObject.transform.localScale;
        float eular = gameObject.transform.rotation.z;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, eular, targetLayer);

        foreach (Collider2D col in colliders)
        {
            Debug.Log("Collided with: " + col.gameObject.name);
            if (col.GetComponent<MonsterStats>() == null)
            {
                continue;
            }
            if (col.GetComponent<MonsterStats>().currHealth <= 0)
            {
                continue;
            }
            if (OnCollided != null)
            {
                if (col == null)
                {
                    return;
                }
                OnCollided(caster, col.gameObject);
            }
        }

        hasBeenHit = true;
    }

    public void SetIsPoisitionSet(bool isSet)
    {
        isPositionSet = isSet;
    }
}

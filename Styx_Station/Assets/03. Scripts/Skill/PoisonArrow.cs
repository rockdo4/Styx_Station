using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrow : PoolAble
{
    public event Action<GameObject, GameObject> OnCollided;

    private Camera mainCamera;

    private Rigidbody2D rg;
    private GameObject caster;
    private float speed;

    private bool isRelease = false;

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        isRelease = false;
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.right;
        var moveVector = direction * speed * Time.fixedDeltaTime;
        rg.MovePosition(rg.position + moveVector);
    }

    private void Update()
    {
        if (!IsInCameraView())
        {
            ReleaseArrow();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MonsterStats>() == null)
        {
            return;
        }
        if (other.GetComponent<MonsterStats>().currHealth <= 0)
        {
            return;
        }
        if (OnCollided != null)
        {
            if (other == null)
            {
                return;
            }
            OnCollided(caster, other.gameObject);
        }
    }

    public void Fire(GameObject c, float s)
    {
        speed = s;
        caster = c;
    }

    private void ReleaseArrow()
    {
        if (!isRelease)
        {
            isRelease = true;
            ReleaseObject();
        }
    }

    bool IsInCameraView()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        Vector3 screenPoint2D = mainCamera.ViewportToScreenPoint(screenPoint);
        return screenPoint2D.x > 0 && screenPoint2D.x < Screen.width && screenPoint2D.y > 0 && screenPoint2D.y < Screen.height && screenPoint.z > 0;
    }

    public bool CheckOnCollided() //true: not null, false: null
    {
        return OnCollided != null;
    }
}

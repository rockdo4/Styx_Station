using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOfLightArrow : PoolAble
{
    public event Action<GameObject, GameObject> OnCollided;

    private Camera mainCamera;

    private Rigidbody2D rg;
    private GameObject caster;
    private float speed;

    private List<GameObject> monsters = new List<GameObject>();

    private AudioSource aoudioSourece;
    public AudioClip arrowClip;

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        aoudioSourece = GetComponent<AudioSource>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
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
            ReleaseObject();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "EndCamera")
        {
            ReleaseObject();
        }
        if (other.gameObject.GetComponent<MonsterStats>() == null)
        {
            return;
        }
        if (other.gameObject.GetComponent<MonsterStats>().currHealth <= 0)
        {
            return;
        }
        if(monsters.Contains(other.gameObject))
        {
            return;
        }
        else
        {
            monsters.Add(other.gameObject);
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
        if (aoudioSourece == null)
            aoudioSourece = GetComponent<AudioSource>();
        aoudioSourece.PlayOneShot(arrowClip);
        speed = s;
        caster = c;
    }

    private bool IsInCameraView()
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

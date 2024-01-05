using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Meteorite : PoolAble
{
    public event Action<GameObject, GameObject> OnCollided;

    private Camera mainCamera;

    private Rigidbody2D rg;
    private GameObject caster;
    private float speed;

    //public bool isRelease = false;

    private bool hasDealtDamage = false;

    private Vector2 DirectionPos = Vector2.zero;
    float arrivalThreshold = 0.1f;
    private AudioSource audioSource;
    public AudioClip meteoClip;

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        if(audioSource == null )
            audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }
    public void AudioPlay()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(meteoClip);
    }
    private void OnEnable()
    {
        //isRelease = false;
        hasDealtDamage = false;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, DirectionPos) < arrivalThreshold)
        {
            //ReleaseArrow();
            ReleaseObject();
            return;
        }
        
        var moveVector = (DirectionPos - (Vector2)transform.position).normalized * speed * Time.fixedDeltaTime;
        rg.MovePosition(rg.position + moveVector);
    }

    private void Update()
    {
        if (!IsInCameraView())
        {
            ReleaseObject();
            //ReleaseArrow();
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
        if(hasDealtDamage)
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
            hasDealtDamage = true;
        }
    }


    public void Fire(GameObject c, float s)
    {
       
        speed = s;
        caster = c;

        hasDealtDamage = false;
        DirectionPos = new Vector2(transform.position.x + 1, caster.transform.position.y);
        //DirectionPos = new Vector2(transform.position.x + 1, transform.position.y - 2);
    }

    private void ReleaseArrow()
    {
        //if (!isRelease)
        //{
        //    isRelease = true;
        //    ReleaseObject();
        //}
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

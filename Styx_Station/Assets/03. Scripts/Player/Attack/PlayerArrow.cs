using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerArrow : PoolAble
{
    public event Action<GameObject, GameObject> OnCollided;

    public int arrowRangeLayer = 20;

    private Rigidbody2D rb;
    private float speed; //속도
    private Vector3 targetPos; //타겟 위치 - 마지막 위치
    private Vector3 startPos; //시작 위치
    private GameObject caster; //공격자
    private float increaseAttackSpeed = 0.01f;
    public bool isRelease = false;
    private ContactFilter2D filter2D = new ContactFilter2D();
    private Camera mainCamera;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
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

    private void Update()
    {
        if (!IsInCameraView())
        {
            if(!isRelease)
            {
                isRelease = true;
                ReleaseObject();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == arrowRangeLayer)
        {
            ReleaseArrow();
            return;
        }
        Collider2D[] colliders = new Collider2D[20];
        int count =  Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), filter2D, colliders);
        Collider2D attackedMon = colliders[0];
        if(colliders.Length > 1)
        {
            for(int i = 0; i < count; i++)
            {
                if (colliders[i].gameObject.GetComponentInChildren<SortingGroup>().sortingOrder >
                    attackedMon.GetComponentInChildren<SortingGroup>().sortingOrder &&
                    colliders[i].gameObject.GetComponent<MonsterStats>().currHealth > 0)
                {
                    attackedMon = colliders[i];
                }
            }
        }
        if (attackedMon == null)
        {
            //Debug.Log("ERR: attackedMon Null");
            return;
        }
        if (attackedMon.GetComponent<MonsterStats>().currHealth <= 0)
        {
            //Debug.Log("Monster Health is 0");
            return;
        }
        if (OnCollided != null)
        {
            if(attackedMon == null)
            {
                //Debug.Log("ERR: attackedMon Null");
                return;
            }
            OnCollided(caster, attackedMon.gameObject);
        }
        //Debug.Log(other.name);
        ReleaseArrow();
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

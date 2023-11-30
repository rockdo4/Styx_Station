using System;
using System.Net;
using UnityEngine;

public class ProjectileBow : PoolAble
{
    public event Action<GameObject, GameObject> OnCollided;

    private Rigidbody2D rb;
    private float speed; //속도
    private Vector3 targetPos; //타겟 위치 - 마지막 위치
    private Vector3 startPos; //시작 위치
    private Vector3 bezierPos; //베지어 곡선 계산 포인트
    private GameObject caster; //공격자
    private float distance;
    private float timeElapsed = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(GameObject c, float s, Vector3 t, Vector3 b)
    {
        speed = s;
        targetPos = t;
        caster = c;
        bezierPos = b;
        startPos = transform.position;
        distance = Vector3.Distance(startPos, targetPos);
    }

    private void OnEnable()
    {
        timeElapsed = 0f;
    }

    private void FixedUpdate()
    {
        // 속도에 따른 시간 계산
        timeElapsed += Time.deltaTime * speed;

        if (timeElapsed < 1.0f)
        {
            // 베지어 곡선 계산
            Vector2 positionOnCurve = CalculateBezierPoint(startPos, bezierPos, targetPos, timeElapsed);

            // 리지드바디 이동
            rb.MovePosition(positionOnCurve);
        }


    }
    // 베지어 곡선의 한 점 계산
    Vector2 CalculateBezierPoint(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector2 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (OnCollided != null)
            OnCollided(caster, other.gameObject);

        //Debug.Log(other.name);
        ReleaseObject();
        //Destroy(gameObject);
    }
}

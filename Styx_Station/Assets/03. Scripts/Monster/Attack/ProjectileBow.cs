using System;
using System.Net;
using UnityEngine;

public class ProjectileBow : PoolAble
{
    public event Action<GameObject, GameObject> OnCollided;

    private Rigidbody2D rb;
    private float speed; //�ӵ�
    private Vector3 targetPos; //Ÿ�� ��ġ - ������ ��ġ
    private Vector3 startPos; //���� ��ġ
    private Vector3 bezierPos; //������ � ��� ����Ʈ
    private GameObject caster; //������
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
        // �ӵ��� ���� �ð� ���
        timeElapsed += Time.deltaTime * speed;

        if (timeElapsed < 1.0f)
        {
            // ������ � ���
            Vector2 positionOnCurve = CalculateBezierPoint(startPos, bezierPos, targetPos, timeElapsed);

            // ������ٵ� �̵�
            rb.MovePosition(positionOnCurve);
        }


    }
    // ������ ��� �� �� ���
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

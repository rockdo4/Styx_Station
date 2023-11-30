using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float width;
    public Transform center;
    public Vector2 centerPos;
    private void Awake()
    {
        var boxCollider = GetComponent<BoxCollider2D>();
        width = boxCollider.size.x * 0.56f;
        centerPos = center.position;
    }

    void Update()
    {
        if(transform.position.x < centerPos.x - width) //ȭ�� ������ ������ ��
        {
            Reposition(); //���� sky ���ȭ�� ���� ������ �̵��ϰ� ��
            WaveManager.instance.ScrollBackground(false);
            WaveManager.instance.StartWave();
        }
    }

    private void Reposition()
    {
        var offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}

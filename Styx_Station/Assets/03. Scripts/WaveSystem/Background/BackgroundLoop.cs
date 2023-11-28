using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float width;
    private void Awake()
    {
        var boxCollider = GetComponent<BoxCollider2D>();
        width = boxCollider.size.x;
    }

    void Update()
    {
        if(transform.position.x < -width) //ȭ�� ������ ������ ��
        {
            Reposition(); //���� sky ���ȭ�� ���� ������ �̵��ϰ� ��
        }
    }

    private void Reposition()
    {
        var offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}

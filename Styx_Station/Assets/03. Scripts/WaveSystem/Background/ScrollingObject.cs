using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f; //�̵� �ӵ�
    void Start()
    {
        
    }

    void Update()
    {
        //if(!GameManager.instance.isGameOver) //���ӿ����� �ƴҶ��� ��� ��ũ��
        //{
        //    transform.Translate(Vector3.left * speed * Time.deltaTime);
        //}
    }
}

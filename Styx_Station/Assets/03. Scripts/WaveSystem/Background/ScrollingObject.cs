using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f; //이동 속도
    void Start()
    {
        
    }

    void Update()
    {
        //if(!GameManager.instance.isGameOver) //게임오버가 아닐때만 배경 스크롤
        //{
        //    transform.Translate(Vector3.left * speed * Time.deltaTime);
        //}
    }
}

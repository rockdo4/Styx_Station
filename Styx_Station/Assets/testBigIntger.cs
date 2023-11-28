using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class testBigIntger : MonoBehaviour
{
    public int Count;
    private ResultPlayerStats result;
    private PlayerAttributes a;
    private Time time;
    private string currentTime1;
    private string currentTime2;
    
    void Start()
    {
        result = GetComponent<ResultPlayerStats>();
        a = GetComponent<PlayerAttributes>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            for (int i = 0; i < Count; ++i)
            {
                if (i == 0 )
                {
                    currentTime1 = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초 fff밀리초");
                    Debug.Log($"{currentTime1} BigIntger");
                }
                if(i  == Count - 1)
                {
                    currentTime2 = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초 fff밀리초");
                    Debug.Log($"{currentTime2} BigIntger");
                }
                TestBigIntger();
                TestBigIntger();
                TestBigIntger();
                TestBigIntger();
                TestBigIntger();
                TestBigIntger();
                TestBigIntger();
                TestBigIntger();
                TestBigIntger();
                TestBigIntger();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            for (int i = 0; i < Count; ++i)
            {
                if (i == 0)
                {
                    currentTime1 = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초 fff밀리초");
                    Debug.Log($"{currentTime1} Int");
                }
                if (i == Count - 1)
                {
                    currentTime2 = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초 fff밀리초");
                    Debug.Log($"{currentTime2} Int");
                }
                TestInt();
                TestInt();
                TestInt();
                TestInt();
                TestInt();
                TestInt();
                TestInt();
                TestInt();
                TestInt();
                TestInt();
            }
        }
    }

    private void TestBigIntger ()
    {
        BigInteger a = new BigInteger(100);
        BigInteger b = new BigInteger(100);
        var t = a * b * a * b * a * b * a * a * b * a * b * a * b * a * b * a * a * a * a * a * a * a;
    }

    private void TestInt()
    {
        var a = 100;
        var b = 100;
        var t = a * b * a * b * a * b * a * a * b * a * b * a * b * a * b * a * a * a * a * a * a * a;
    }

}

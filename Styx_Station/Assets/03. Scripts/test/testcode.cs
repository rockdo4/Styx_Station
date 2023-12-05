using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class testcode : MonoBehaviour
{
    static string save = string.Empty;
    static string load = string.Empty;
    int[] prevTime = new int[5];
    static float result = 0;
    static float maxResult = 86400; // 24시간을 초로 나눈 상태 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameData.GetAccumulateOfflineEarnings();
            Debug.Log($"result :{GameData.result}");
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameData.ChnageTime();
        }

    }
}

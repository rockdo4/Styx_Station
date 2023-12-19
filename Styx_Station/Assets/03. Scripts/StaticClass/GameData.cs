using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class GameData
{
    public static StringBuilder exitTime = new StringBuilder() ; // ������ �ð� ���� -> �Ĵ�ĭ���� ��� ����
    public static StringBuilder keyPrevAccumlateTime = new StringBuilder();
    private static StringBuilder nowTime = new StringBuilder();

    public static float result = 0;
    public static float maxResult = 1440; // 24�ð��� ������ ���� ���� 

    [HideInInspector]
    public static string datetimeString = "yy�� MM�� dd�� HH�� mm�� ss��";

    public static StageData stageData;

    public static List<LabSaveData> Re_AtkSaveDataList = new List<LabSaveData>();
    public static List<LabSaveData> Re_HPSaveDataList = new List<LabSaveData>();
    public static List<LabSaveData> Re_CriSaveDataList =new List<LabSaveData>();
    public static List<LabSaveData>Re_SilupSaveDataList=new List<LabSaveData>();
    public static List<LabSaveData> Re_MidAtkSaveDataList = new List<LabSaveData>();
    public static List<LabSaveData> Re_MidHPSaveDataList = new List<LabSaveData>();
    public static CurrentLavSaveData currnetLabSaveData;

    public static int tic = 1000;

    public static LabBuffData labBuffData;

    public static void GetAccumulateOfflineEarnings()
    {
        var prevData = DateTime.ParseExact(keyPrevAccumlateTime.ToString(), datetimeString, null);
        nowTime.Clear();
        nowTime.Append(DateTime.Now.ToString(datetimeString));
        var date = DateTime.ParseExact(nowTime.ToString(), datetimeString, null);

        TimeSpan timeDifference = date.Subtract(prevData);
        if (timeDifference.TotalDays >= 1)
        {
            result = maxResult;
        }
        else
        {
            result = (float)(timeDifference.Hours * 60 + timeDifference.Minutes + timeDifference.Seconds * 0.01);
        }

        CurrencyManager.money1 += (int)result * 100 / 100;

        ChnageTime();
    }

    public static void ChnageTime()
    {
        keyPrevAccumlateTime.Clear();
        var rt = DateTime.Now.ToString(datetimeString);
        keyPrevAccumlateTime.Append(rt);
        if(result >0)
        {
            // ���� ŉ�� �����ϰ� �ϴ� �ڵ� �ֱ�
            result = 0f;
        }
    }

}

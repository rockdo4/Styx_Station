using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class GameData
{
    public static StringBuilder exitTime = new StringBuilder() ; // ������ �ð� ���� -> �Ĵ�ĭ���� ��� ����
    public static StringBuilder keyPrevAccumlateTime = new StringBuilder();
    public static StringBuilder nowTime = new StringBuilder();

    public static int result = 0;
    public static int maxResult = 1440; // 24�ð��� ������ ���� ���� 

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
    public static int labBuffDataPercent = 10;
    private static int compensationTime;

    private static bool isBanchiCompensationTime;

    public static void GetAccumulateOfflineEarnings()
    {
        if(keyPrevAccumlateTime.ToString() == string.Empty)
        {
            keyPrevAccumlateTime.Append(DateTime.Now.ToString(datetimeString));
        }
        var prevData = DateTime.ParseExact(keyPrevAccumlateTime.ToString(), datetimeString, null);
        nowTime.Clear();
        nowTime.Append(DateTime.Now.ToString(datetimeString));
        var date = DateTime.ParseExact(nowTime.ToString(), datetimeString, null);

        TimeSpan timeDifference = date.Subtract(prevData);
        if (timeDifference.TotalMinutes < 10)
        {
            isBanchiCompensationTime = false;
            return;
        }
        isBanchiCompensationTime = true;
        if (timeDifference.TotalDays >= 1)
        {
            result = maxResult;
        }
        else
        {
            result = (timeDifference.Hours * 60 + timeDifference.Minutes);
        }

        //CurrencyManager.money1 += result * 100 / 100;
        compensationTime = result;
        //ChnageTime();
    }

    public static void ChnageTime()
    {
        keyPrevAccumlateTime.Clear();
        var rt = DateTime.Now.ToString(datetimeString);
        keyPrevAccumlateTime.Append(rt);
        if(result >0)
        {
            // ���� ŉ�� �����ϰ� �ϴ� �ڵ� �ֱ�
            result = 0;
        }
    }
    public static int GetCompensationTime()
    {
        return compensationTime;
    }
    public static void ResetCompensationTime()
    {
        ChnageTime();
        compensationTime = 0;
    }
    public static bool GetBanchiCompensationTime()
    {
        return isBanchiCompensationTime;
    }
}

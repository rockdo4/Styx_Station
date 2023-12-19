using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class GameData
{
    public static StringBuilder exitTime = new StringBuilder() ; // 종료한 시간 시점 -> 식당칸에서 사용 예정
    public static StringBuilder keyPrevAccumlateTime = new StringBuilder();
    private static StringBuilder nowTime = new StringBuilder();

    public static float result = 0;
    public static float maxResult = 1440; // 24시간을 분으로 나눈 상태 

    [HideInInspector]
    public static string datetimeString = "yy년 MM월 dd일 HH시 mm분 ss초";

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
            // 보상 흭득 랜덤하게 하는 코드 넣기
            result = 0f;
        }
    }

}

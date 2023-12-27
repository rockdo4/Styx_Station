using System;
using UnityEngine;

public class DiningRoomSystem : Singleton<DiningRoomSystem>
{
    public float timer = 0;
    public float max = 3600f; //12.15 180f
    [HideInInspector] public int counting = 0;

    public int timerUpgradeLevel = 0;
    public int maxTimerUpgradeLevel =12; // 60
     public bool isMaxTimerUpgradeLevel;

    public int selectFoodCount = 1;
    [HideInInspector] public int maxSelectfoodCount = 6;
    [HideInInspector] public bool isMaxSelectUpgradeLevel;

    public FoodData[] foodDatas = new FoodData[6];
    [HideInInspector]  public bool isFullFood;

    public SaveFoodData[] saveFood = new SaveFoodData[6];

    [HideInInspector]public bool isAwkeTime;
    [HideInInspector] public bool isLoad;

    public float decreaseMaxTimer = 10f; //30f
    private void Awake()
    {
        
    }
    private void Start()
    {
        if (selectFoodCount >= maxSelectfoodCount)
        {
            isMaxSelectUpgradeLevel = true;
            selectFoodCount = maxSelectfoodCount;
        }
        if (timerUpgradeLevel >= maxTimerUpgradeLevel)
        {
            timerUpgradeLevel = maxTimerUpgradeLevel;
            isMaxTimerUpgradeLevel = true;
        }
        if (!isLoad)
        {
            timer = max;
            isLoad = true;
        }
    }
    private void Update()
    {
        if (!isFullFood)
        {
            timer -= Time.deltaTime;
            if (timer < 1f)
            {
                counting++;
                timer = max;
                if (counting >= selectFoodCount)
                {
                    FoodDatasNullCheck();
                }
            }
        }  
    }

    public void UpgradeTimerLevel()
    {

        timerUpgradeLevel++;
        if (timerUpgradeLevel >= maxTimerUpgradeLevel)
        {
            timerUpgradeLevel = maxTimerUpgradeLevel;
            isMaxTimerUpgradeLevel = true;
        }
        if(!isMaxTimerUpgradeLevel)
            max -= decreaseMaxTimer;

    }

    public void UpgradeSelectFoodLevel()
    {
        if(isMaxSelectUpgradeLevel)
        {
            return;
        }
        selectFoodCount++;
        isFullFood = false;
        if(selectFoodCount >=maxSelectfoodCount)
        {
            isMaxSelectUpgradeLevel = true; 
        }
        timer = max;
    }

    public void ReMoveFoodData(int index)
    {
        foodDatas[index] = null;
        isFullFood = false;
        timer = max;
        counting--;
        if (counting <= 0)
            counting = 0;
    }

    public void SetFood(FoodData foodData,int index =-1)
    {
        if(index >=0)
        {
            foodDatas[index] = foodData;
            return;
        }
        for(int i =0;i< selectFoodCount; i++)
        {
            if (foodDatas[i] == null)
            {
                foodDatas[i] = foodData;
                break;
            }
        }
        FoodDatasNullCheck();
    }
    public void FoodDatasNullCheck()
    {
        for (int i = 0; i < selectFoodCount; i++)
        {
            if (foodDatas[i] == null)
            {
                return;
            }
        }
        isFullFood = true;
        timer = 0f;
    }
    public FoodData[] GetAllFoodData()
    {
        return foodDatas;
    }
    public void LoadFoodData(SaveFoodData saveFoodData,int index)
    {
        saveFood[index] = saveFoodData;
    }
    public void LoadMaxTimer()
    {
        for (int i = 0; i <timerUpgradeLevel; ++i)
        {
            max -= decreaseMaxTimer;
        }
        if(timerUpgradeLevel >= maxTimerUpgradeLevel)
        {
            isMaxSelectUpgradeLevel = true;
            timerUpgradeLevel = maxTimerUpgradeLevel;
        }
        if (!isLoad)
        {
            timer = max;
            isLoad = true;
        }
    }
    public void CalculateTimer(int count)
    {
        var exitTime =DateTime.ParseExact(GameData.exitTime.ToString(), GameData.datetimeString, null);
        var nowTimeStr =DateTime.Now.ToString(GameData.datetimeString);
        var nowTimeSpan = DateTime.ParseExact(nowTimeStr, GameData.datetimeString, null);

        TimeSpan timeDifference = nowTimeSpan.Subtract(exitTime);
        float tmepTime = 0f;
        if(timeDifference.TotalSeconds >0f)
        {
             timer -= (float)timeDifference.TotalSeconds;
             tmepTime = timer;
        }

        int newCount = selectFoodCount - count;
        if (newCount <= 0)
            return;

        while (tmepTime <0f)
        {
            if (timer < 0f)
            {
                counting++;
                tmepTime += max;
                timer = max;
                if (counting >= newCount)
                {
                    break;  
                }
            }
            else
            {
                break;
            }
            //timer += tmepTime;

        }
    }

    public void ResetSaveFoodData()
    {
        for(int i=0;i<saveFood.Length;i++)
        {
            saveFood[i]= null;  
        }
    }
}

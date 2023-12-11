using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;

public class DiningRoomSystem : Singleton<DiningRoomSystem>
{
    public float timer = 0;
    public float max = 3600f;
    [HideInInspector] public int counting = 0;

    public int timerUpgradeLevel = 0;
    [HideInInspector] public int maxTimerUpgradeLevel = 60;
    [HideInInspector] public bool isMaxTimerUpgradeLevel;

    public int selectFoodCount = 1;
    [HideInInspector] public int maxSelectfoodCount = 6;
    [HideInInspector] public bool isMaxSelectUpgradeLevel;

    public FoodData[] foodDatas = new FoodData[6];
    [HideInInspector]  public bool isFullFood;

    public SaveFoodData[] saveFood = new SaveFoodData[6];

    [HideInInspector]public bool isAwkeTime;
    [HideInInspector] public bool isLoad;

    public float decreaseMaxTimer=30f;
    private void Awake()
    {
        
    }
    private void Start()
    {
    }
    private void Update()
    {
        if (!isFullFood)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
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
        max -= decreaseMaxTimer;
        if (timerUpgradeLevel >= maxTimerUpgradeLevel)
        {
            timerUpgradeLevel = maxTimerUpgradeLevel;
            isMaxTimerUpgradeLevel = true;
        }
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

    }

    public void ReMoveFoodData(int index)
    {
        foodDatas[index] = null;
        isFullFood = false;
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
        for (int i = 1; i <=timerUpgradeLevel; ++i)
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
    public void CalculateTimer()
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

        while(tmepTime <0f)
        {
            if (timer < 0f)
            {
                counting++;
                tmepTime += max;
                timer = max;
                if (counting >= selectFoodCount)
                {
                    FoodDatasNullCheck();
                }
            }
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

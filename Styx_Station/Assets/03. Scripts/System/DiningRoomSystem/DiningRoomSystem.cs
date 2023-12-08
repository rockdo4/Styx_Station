using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningRoomSystem : Singleton<DiningRoomSystem>
{
    public float timer = 0;
    public float max = 3600f;
    [HideInInspector] public int counting = 0;

    public int timerUpgradeLevel = 0;
    public int selectFoodCount = 1;
    [HideInInspector] public int maxSelectfoodCount = 6;
    [HideInInspector] public int selectFoodUpgrade = 0;

    public FoodData[] foodDatas = new FoodData[6];
    [HideInInspector]  public bool isFullFood;

    private void Awake()
    {
        //OnGameData에 넣어주기
        timer = max;
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
                if(counting >= selectFoodCount)
                {
                    FoodDatasNullCheck();
                }
            }
        }  
    }

    public void UpgradeTimerLevel()
    {
        timerUpgradeLevel++;
    }

    public void UpgradeSelectFoodLevel()
    {
        if(selectFoodCount >=maxSelectfoodCount)
        {
            return;
        }
        selectFoodCount++;
    }

    public void ReMoveFoodData(int index)
    {
        foodDatas[index] = null;
    }

    public void SetFood(FoodData foodData)
    {
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
        counting = 0;
    }
    public FoodData[] GetAllFoodData()
    {
        return foodDatas;
    }
}

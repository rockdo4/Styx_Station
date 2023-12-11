using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public SaveFoodData[] saveFood = new SaveFoodData[6];

    [HideInInspector]public bool isAwkeTime;

    public float decreaseMaxTimer=30f;
    private void Awake()
    {
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
        isFullFood = false;
        counting--;
        if (counting <= 0)
            counting = 0;
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
    }
    public FoodData[] GetAllFoodData()
    {
        return foodDatas;
    }
    public void LoadFoodData(SaveFoodData saveFoodData,int index)
    {
        saveFood[index] = saveFoodData;
    }

    public void ResetSaveFoodData()
    {
        for(int i=0;i<saveFood.Length;i++)
        {
            saveFood[i]= null;  
        }
    }
}

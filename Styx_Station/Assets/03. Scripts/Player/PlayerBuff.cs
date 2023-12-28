using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerBuff : Singleton<PlayerBuff>
{
    public readonly int percent = 100;
    public PlayerBuffData buffData;
    public StringBuilder foodId =new StringBuilder();
    private void Update()
    {
        if(buffData.isEatFood)
        {
            buffData.timer += Time.deltaTime;
            if (buffData.timer > buffData.foodBuffMaxTimer)
            {
                Reset();
                UIManager.Instance.ClosePlayerBuffInfo();
            }
        }
       
    }
    public void SetFoodId(string id)
    {
        foodId.Clear();
        foodId.Append(id);
    }
    public void GetBuffAll(int power, int critcal, int skill, int boss, int siling, float timer, FoodType type)
    {
        buffData.timer = 0f;
        buffData.playerPowerBuff = power;
        buffData.criticalPowerBuff = critcal;
        buffData.skillBuff = skill;
        buffData.bossAttackBuff = boss;
        buffData.silingBuff = siling;
        buffData.foodBuffMaxTimer = timer;
        buffData.foodType = type;
        buffData.isEatFood = true;
        UIManager.Instance.OpenPlayerBuffInfo();
        StateSystem.Instance.TotalUpdate();
    }
    public void Reset()
    {
        buffData.playerPowerBuff = 0;
        buffData.criticalPowerBuff = 0;
        buffData.skillBuff = 0;
        buffData.bossAttackBuff = 0;
        buffData.silingBuff = 0;
        buffData.foodBuffMaxTimer = 0f;
        buffData.foodType = FoodType.None;
        buffData.isEatFood = false;
    }
}

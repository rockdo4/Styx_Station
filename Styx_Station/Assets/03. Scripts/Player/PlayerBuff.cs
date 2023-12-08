using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuff  :Singleton<PlayerBuff>
{
    private readonly int percent = 100;
    public bool isEatFood = false;
    [SerializeField]private int playerPowerBuff;
    [SerializeField] private int criticalPowerBuff;
    [SerializeField] private int skillBuff;
    [SerializeField] private int bossAttackBuff;
    [SerializeField] private int silingBuff;
    [SerializeField] private float foodBuffMaxTimer;
    [SerializeField] private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer> foodBuffMaxTimer )
        {
            Debug.Log($"{gameObject.name} / BuffOff");
            Reset();
        }
    }

    public void GetBuffAll(int power , int critcal,int skill,int boss, int siling,float timer)
    {
        if(isEatFood)
        {
            return;
        }
        playerPowerBuff =power;
        criticalPowerBuff =critcal;
        skillBuff =skill;
        bossAttackBuff =boss;
        silingBuff =siling;
        foodBuffMaxTimer =timer;
        isEatFood = true;
    }
    public void Reset()
    {
        playerPowerBuff = 0;
        criticalPowerBuff = 0;
        skillBuff = 0;
        bossAttackBuff = 0;
        silingBuff = 0;
        foodBuffMaxTimer = 0f;
        isEatFood = false;
    }
}

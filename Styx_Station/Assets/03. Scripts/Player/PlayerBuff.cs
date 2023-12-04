using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerBuff 
{
    private static readonly int percent = 100;
    public static bool isEatFood = false;
    private static int playerPowerBuff;
    private static int criticalPowerBuff;
    private static int skillBuff;
    private static int bossAttackBuff;
    private static int silingBuff;
    private static float foodBuffMaxTimer;


    // 시간초 지나면 버프 초기화 코드는 고민후 작업 해야할듯...
    // 인게임매니저에서 추가적으로 시간을 계산후 초기화하면 될것 같음

    public static void GetBuffAll(int power , int critcal,int skill,int boss, int siling,float timer)
    {
        playerPowerBuff =power;
        criticalPowerBuff =critcal;
        skillBuff =skill;
        bossAttackBuff =boss;
        silingBuff =siling;
        foodBuffMaxTimer =timer;
        isEatFood = true;
    }
    public static void Reset()
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

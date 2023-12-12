using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerBuffData
{
    public bool isEatFood;
    public int playerPowerBuff;
    public int criticalPowerBuff;
    public int skillBuff;
    public int bossAttackBuff;
    public int silingBuff;
    public float foodBuffMaxTimer;
    public float timer;
    public FoodType foodType;
}

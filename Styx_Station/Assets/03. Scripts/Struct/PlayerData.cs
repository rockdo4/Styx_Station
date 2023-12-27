using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public struct PlayerData
{
    public int playerPower;
    public int playerPowerboost;
    public int playerAttackSpeed;
    public int critical;
    public int criticalPower;
    public int monsterDamage;
    public int maxHp;
    public int healing;

    public string money1; // 은화
    public string money2; // 유료재화
    public string money3; // 영혼 혈류석 ?
    public string money4;
    
    public int clearIndex; // clearNumber;
}
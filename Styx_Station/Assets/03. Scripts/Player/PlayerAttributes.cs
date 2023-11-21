using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // player 기본 스탯 내용 
    [SerializeField,Range(0,1000)]
    [Header("HP")]
    public int hp; // 체력
    [SerializeField, Range(0, 100)]
    [Header("공격력")]
    public int attackPower; // 공격력
    [Header("공격속도 ")]
    [SerializeField, Range(0f, 1f)]
    public float attackSpeed; // 공격속도
}
/*
    public float attack;
    public float attackBoost;
    public float attackSpeed;
    public float attackCritical;
    public float attackCriticlaDamage;
    public float monsterDamage;
    public float bossDamage;
    public float hp;
    public float Maxhp;
    public float increasHp;
    public float skillCooldown;
    public float evasionRate;
    public float getMoney;
    public float getDamage;
    public float bloodsucking;
 */
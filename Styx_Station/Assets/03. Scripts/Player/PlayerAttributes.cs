using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // player 기본 스탯 내용 
    [SerializeField,Range(0,1000)]
    [Header("HP")]
    public int hp; 
    [SerializeField, Range(0, 500)]
    [Header("공격력")]
    public int attackPower; 
    [Header("공격속도 ")]
    [SerializeField, Range(0f, 1f)]
    public float attackSpeed; 

    [Header("공격 범위 ")]
    [SerializeField, Range(0, 5)]
    public float playerAttackRange; 
}


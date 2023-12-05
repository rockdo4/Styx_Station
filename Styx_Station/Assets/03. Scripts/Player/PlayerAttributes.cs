using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // player 기본 스탯 내용 
    [HideInInspector]
    public int hp;

    [SerializeField, Range(0, 1000000)]
    [Header("HP")]
    public int MaxHp;
    [SerializeField, Range(0, 500)]
    [Header("공격력")]
    public int attackPower; 
    [Header("공격속도 ")]
    [SerializeField, Range(0f, 5f)]
    public float attackSpeed; 

    [Header("공격 범위 ")]
    [SerializeField, Range(0, 15)]
    public float playerAttackRange;

    private void Awake()
    {
        if(MaxHp != hp)
        {
            if(hp > MaxHp)
            {
                MaxHp = hp; 
            }
            else
            {
                hp = MaxHp; 
            }
        }
    }
    private void FixedUpdate()
    {
        
    }
    private void Update()
    {
        if(hp <= 0 )
        {
            GameManager.instance.ReStart();
        }
    }
}
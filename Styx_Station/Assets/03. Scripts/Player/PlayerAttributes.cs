using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // player �⺻ ���� ���� 
    [HideInInspector]
    public int hp;

    [SerializeField, Range(0, 1000000)]
    [Header("HP")]
    public int MaxHp;
    [SerializeField, Range(0, 500)]
    [Header("���ݷ�")]
    public int attackPower; 
    [Header("���ݼӵ� ")]
    [SerializeField, Range(0f, 5f)]
    public float attackSpeed; 

    [Header("���� ���� ")]
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // player �⺻ ���� ���� 
    [SerializeField,Range(0,1000)]
    [Header("HP")]
    public int hp; // ü��
    [SerializeField, Range(0, 100)]
    [Header("���ݷ�")]
    public int attackPower; // ���ݷ�
    [Header("���ݼӵ� ")]
    [SerializeField, Range(0f, 1f)]
    public float attackSpeed; // ���ݼӵ�
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
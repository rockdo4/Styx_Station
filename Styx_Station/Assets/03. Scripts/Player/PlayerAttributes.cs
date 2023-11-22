using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // player �⺻ ���� ���� 
    [SerializeField,Range(0,1000)]
    [Header("HP")]
    public int hp;
    [SerializeField, Range(0, 500)]
    [Header("���ݷ�")]
    public int attackPower; 
    [Header("���ݼӵ� ")]
    [SerializeField, Range(0f, 5f)]
    public float attackSpeed; 

    [Header("���� ���� ")]
    [SerializeField, Range(0, 5)]
    public float playerAttackRange; 
}
// �����̶��ֱ��ؼ� hp �κ� �����ؾ��ҵ� maxHp�� ������ �����ؾ��ϱ⶧��

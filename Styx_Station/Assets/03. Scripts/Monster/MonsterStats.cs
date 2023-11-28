using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [Header("���� �ִ� ü��")]
    public BigInteger maxHp;

    public BigInteger currHealth { get; set; }

    [Header("���� ������")]
    public BigInteger damage;

    [Header("���� ���� Ÿ��(���Ÿ� or �ٰŸ�)")]
    public AttackType attackType = AttackType.None;

    [Header("���� �̵��ӵ�")]
    public float speed;

    private void OnEnable()
    {
        currHealth = maxHp;
    }
    public void SetStats(BigInteger maxH, BigInteger d, AttackType attackT, float s)
    {
        maxHp = maxH;
        currHealth = maxHp;
        damage = d;
        attackType = attackT;
        speed = s;
    }
}

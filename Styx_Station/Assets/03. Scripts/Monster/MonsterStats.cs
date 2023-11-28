using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [Header("몬스터 최대 체력")]
    public BigInteger maxHp;

    public BigInteger currHealth;

    [Header("몬스터 데미지")]
    public BigInteger damage;

    [Header("몬스터 공격 타입(원거리 or 근거리)")]
    public AttackType attackType = AttackType.None;

    [Header("몬스터 이동속도")]
    public float speed;

    private void OnEnable()
    {
        currHealth = maxHp;
    }
    public void SetStats(string maxH, string d, AttackType attackT, float s)
    {
        maxHp = BigInteger.Parse(maxH);
        currHealth = maxHp;
        damage = BigInteger.Parse(d);
        attackType = attackT;
        speed = s;
    }
}

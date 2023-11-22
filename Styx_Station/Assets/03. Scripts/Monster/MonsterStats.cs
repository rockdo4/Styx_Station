using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [Header("몬스터 최대 체력")]
    public float maxHp;

    public float currHealth { get; set; }

    [Header("몬스터 데미지")]
    public float damage;

    [Header("몬스터 공격 타입(원거리 or 근거리)")]
    public AttackType attackType = AttackType.None;

    [Header("몬스터 이동속도")]
    public float speed;

    private void OnEnable()
    {
        currHealth = maxHp;
    }
    public void SetStats(float maxH, float d, AttackType attackT, float s)
    {
        maxHp = maxH;
        currHealth = maxHp;
        damage = d;
        attackType = attackT;
        speed = s;
    }
}

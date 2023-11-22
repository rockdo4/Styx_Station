using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [Header("���� �ִ� ü��")]
    public float maxHp;

    public float currHealth { get; set; }

    [Header("���� ������")]
    public float damage;

    [Header("���� ���� Ÿ��(���Ÿ� or �ٰŸ�)")]
    public AttackType attackType = AttackType.None;

    [Header("���� �̵��ӵ�")]
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackedTakeDamage : MonoBehaviour, IAttackable
{
    private MonsterStats stats;
    private void Awake()
    {
        stats = GetComponent<MonsterStats>();
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        stats.currHealth -= attack.Damage;
        Debug.Log($"OnAttack: {attack.Damage}");
        if (stats.currHealth <= 0)
        {
            stats.currHealth = 0;
            //gameObject.transform.GetComponent<Animator>().SetTrigger("Die");
        }
    }
}

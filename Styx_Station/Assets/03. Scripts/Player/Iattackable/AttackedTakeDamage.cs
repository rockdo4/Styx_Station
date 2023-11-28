using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedTakeDamage : MonoBehaviour, IAttackable
{
    private ResultPlayerStats stats;

    private void Awake()
    {
        stats = GetComponent<ResultPlayerStats>();
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        stats.playerCurrentHp -= attack.Damage;
        if (stats.playerCurrentHp <= 0)
        {
            stats.playerCurrentHp = 0;
            //gameObject.transform.GetComponent<Animator>().SetTrigger("Die");
            //var destructables = GetComponents<IDestructable>();
            //foreach (var destructable in destructables)
            //{
            //    destructable.OnDestruction(attacker);
            //}
        }
    }
}

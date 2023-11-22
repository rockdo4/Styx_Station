using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedTakeDamage : MonoBehaviour, IAttackable
{
    private PlayerAttributes stats;

    private void Awake()
    {
        stats = GetComponent<PlayerAttributes>();
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        stats.hp -= attack.Damage;
        if (stats.hp <= 0)
        {
            stats.hp = 0;
            gameObject.transform.GetComponent<Animator>().SetTrigger("Die");
            var destructables = GetComponents<IDestructable>();
            foreach (var destructable in destructables)
            {
                destructable.OnDestruction(attacker);
            }
        }
    }
}

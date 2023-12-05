using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedTakeDamage : MonoBehaviour, IAttackable
{
    private ResultPlayerStats stats;
    private PlayerController controller;

    private void Awake()
    {
        stats = GetComponent<ResultPlayerStats>();
        controller = GetComponent<PlayerController>();  
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        stats.TakeDamage(attack.Damage);
        Debug.Log(stats.playerCurrentHp);
        //stats.playerCurrentHp -= attack.Damage;
        if (stats.playerCurrentHp <= 0 )
        {
            stats.playerCurrentHp = 0;
            controller.GetAnimator().SetTrigger("Die");
            controller.SetState(States.Die);
        }
    }
}

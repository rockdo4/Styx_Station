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
        if(controller.currentStates == States.Idle || controller.currentStates == States.Attack)
        {
            stats.TakeDamage(attack.Damage);
            //controller.hpBar.fillAmount = (float)((long)stats.playerCurrentHp / (float)(long)stats.playerMaxHp);
            //stats.playerCurrentHp -= attack.Damage;
            if (stats.playerCurrentHp <= 0)
            {
                stats.playerCurrentHp = 0;
                controller.GetAnimator().SetTrigger("Die");
                controller.SetState(States.Die);
            }
        }
    }
}

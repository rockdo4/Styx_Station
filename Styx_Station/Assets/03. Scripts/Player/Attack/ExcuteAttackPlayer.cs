using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcuteAttackPlayer : MonoBehaviour
{
    public AttackDefinition weapon;
    public GameObject target;
    public GameObject attacker;

    public PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void Hit()
    {
        playerController.SetExcuteHit();
        if (weapon == null || target == null)
        {
            return;
        }
        weapon.ExecuteAttack(attacker, target);
    }
}

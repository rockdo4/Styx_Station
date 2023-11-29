using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcuteAttackPet : MonoBehaviour
{
    public AttackDefinition weapon;
    public GameObject target;
    public GameObject attacker;
    public Transform petFirePos;
    private PetController petController;
    private float attackSpeed;
    private void Awake()
    {
        petController = GetComponentInParent<PetController>();
    }

    public void Hit()
    {
        if (petController != null)
        {
            petController.SetExcuteHit();
            if (weapon == null || target == null)
            {
                return;
            }
            weapon.ExecuteAttack(attacker, target);
        }
    }
}

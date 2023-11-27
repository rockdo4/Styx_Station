using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcuteAttackPet : MonoBehaviour
{
    public AttackDefinition weapon;
    public GameObject target;
    public GameObject attacker;

    public PetController petController;

    private void Awake()
    {
        petController = GetComponentInParent<PetController>();
    }

    public void Hit()
    {
        petController.SetExcuteHit();
        if (/*weapon == null ||*/ target == null)
        {
            return;
        }
        weapon.ExecuteAttack(attacker, target);
        Debug.Log("°ø°ÝÇÔ");
    }
}

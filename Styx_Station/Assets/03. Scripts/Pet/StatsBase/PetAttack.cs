using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAttack : PetStateBase
{
    public PetAttack(PetController petController) : base(petController)
    {
    }

    public override void Enter()
    {
        petController.GetAnimator().SetTrigger("Attack");
        petController.GetAnimator().SetFloat("NormalState", 0f);
        petController.GetAnimator().speed = petController.attackSpeed;
    }

    public override void Exit()
    {

        petController.GetAnimator().speed = 1f;
    }

    public override void FixedUpate()
    {
        var master =petController.masterPlayer.GetComponent<PlayerController>();
        if(master != null)
        {
            if(petController.GetPetStateManager().GetCurrentState() != master.GetPlayerCurrentState())
            {
                petController.GetAnimator().SetTrigger("Idle");
            }
        }
    }

    public override void Update()
    {
        
    }

    
}

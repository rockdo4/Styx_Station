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
        petController.GetAnimator().SetBool("Attack", true);
        petController.GetAnimator().SetFloat("NormalState", 0.5f);
        petController.GetAnimator().speed = petController.attackSpeed;
    }

    public override void Exit()
    {
        petController.GetAnimator().SetBool("Attack", false);
        petController.GetAnimator().speed = 1f;
        petController.GetAnimator().SetFloat("NormalState", 0f);
        petController.SetState(States.Idle);
    }

    public override void FixedUpate()
    {
       
    }

    public override void Update()
    {
        
    }

    
}

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
        var test = Physics2D.OverlapCircle(petController.transform.position, petController.range, petController.layerMask);
        if (test == null)
        {
            petController.SetState(States.Idle);
            petController.GetAnimator().SetTrigger("Idle");
        }
        }

    public override void Update()
    {
        
    }

    
}

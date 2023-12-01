using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRun : PetStateBase
{
    public PetRun(PetController petController) : base(petController)
    {
    }

    public override void Enter()
    {
        petController.GetAnimator().SetTrigger("Idle");
        petController.GetAnimator().SetBool("Run", true);
        petController.GetAnimator().SetFloat("RunState", 0.5f);
    }

    public override void Exit()
    {
        petController.GetAnimator().SetBool("Run", false);
        petController.GetAnimator().SetFloat("RunState", 0f);
    }

    public override void FixedUpate()
    {
        var master = petController.masterPlayer.GetComponent<PlayerController>();
        if (master != null)
        {
            if (petController.currentStates != master.currentStates)
            {
                petController.SetState(master.currentStates);
            }
        }
    }

    public override void Update()
    {
        
    }
}

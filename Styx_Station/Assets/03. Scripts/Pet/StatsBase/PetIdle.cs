using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PetIdle : PetStateBase
{
    public PetIdle(PetController petController) : base(petController)
    {
    }

    public override void Enter()
    {
        petController.GetAnimator().SetBool("Run", false);
        petController.GetAnimator().SetFloat("RunState", 0f);
    }

    public override void Exit()
    {

    }

    public override void FixedUpate()
    {
        var test = Physics2D.OverlapCircle(petController.transform.position, petController.range, petController.layerMask);

        if (test != null)
            petController.SetState(States.Attack);
    }

    public override void Update()
    {
        
    }
}

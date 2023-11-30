using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerIdleState : PlayerStateBase
{
    public PlayerIdleState(PlayerController playertController) : base(playertController)
    {
     
    }

    public override void Enter()
    {
        //playertController.GetAnimator().SetTrigger("Idle");
        playertController.GetAnimator().SetFloat("RunState", 0f);
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpate()
    {
        
    }

    public override void Update()
    {

    }

   
}

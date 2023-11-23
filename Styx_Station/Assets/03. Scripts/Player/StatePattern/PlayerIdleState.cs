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
        playertController.GetAnimator().SetTrigger("Idle");
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpate()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {

    }

   
}

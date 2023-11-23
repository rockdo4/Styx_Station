using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerStateBase
{

    public float nowTime;
    public float stateDuration;
    
    public PlayerMoveState(PlayerController playerController) : base(playerController)
    {

    }
    public override void Enter()
    {
        nowTime = Time.time;
        stateDuration = playertController.backgroundLength /playertController.playerMoveSpeed;

        playertController.GetAnimator().SetTrigger("Run");
    }

    public override void Exit()
    {
        nowTime = 0f;
    }

    public override void FixedUpate()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        if(Time.time >  nowTime + stateDuration && playertController.IsStartTarget) 
        {
            playertController.SetState(States.Idle);
            playertController.GetAnimator().SetTrigger("Idle");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playertController.SetState(States.Attack);
            playertController.GetAnimator().SetTrigger("Attack");
        }
    }
}

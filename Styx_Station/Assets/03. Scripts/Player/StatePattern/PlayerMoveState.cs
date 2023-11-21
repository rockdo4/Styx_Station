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

        Debug.Log("Player Moving");
    }

    public override void Exit()
    {
        nowTime = 0f;
        Debug.Log("Player Not Moving");
    }

    public override void Update()
    {
        if(Time.time >  nowTime + stateDuration) 
        {
            playertController.SetState(States.Attack);
        }
    }
}

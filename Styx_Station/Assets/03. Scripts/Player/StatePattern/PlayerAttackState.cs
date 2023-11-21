using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerStateBase
{
    public PlayerAttackState(PlayerController playerController) : base(playerController)
    {

    }
    public override void Enter()
    {
        Debug.Log("Player Attacking");
    }

    public override void Exit()
    {
        Debug.Log("Player Not Attacking");
    }

    public override void Update()
    {
        
    }
}

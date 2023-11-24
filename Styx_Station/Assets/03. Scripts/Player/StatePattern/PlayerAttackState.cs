using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttackState : PlayerStateBase
{
    public PlayerAttackState(PlayerController playerController) : base(playerController)
    {

    }

    private float defaultSpeed = 1f;
    private float increaseAttackSpeed = 0.01f;
    private float attackSpeed = 1f;

    public override void Enter()
    {
        var spped = defaultSpeed + ((SharedPlayerStats.GetPlayerAttackSpeed()-1)* increaseAttackSpeed);
        playertController.GetAnimator().speed = spped;
        playertController.GetAnimator().SetTrigger("Attack");
        playertController.GetAnimator().SetFloat("NormalState", 0.5f);
    }

    public override void Exit()
    {
        playertController.GetAnimator().speed = defaultSpeed;
    }

    public override void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return)) 
        {
            playertController.SetState(States.Move);
            playertController.GetAnimator().SetTrigger("Run");
        }
        //Debug.Log(playertController.GetAnimator().speed);
    }

    public override void FixedUpate()
    {
        
    }
}

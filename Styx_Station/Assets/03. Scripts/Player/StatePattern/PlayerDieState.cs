using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerStateBase
{
    public float timer = 0f;
    public float duration = 1.5f;
    public PlayerDieState(PlayerController playertController) : base(playertController)
    {

    }

    public override void Enter()
    {
        timer = 0f;
    }

    public override void Exit()
    {

    }

    public override void FixedUpate()
    {
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if(timer > duration)
        {
            playertController.GetAnimator().SetBool("EditChk", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : PlayerStateBase
{
    public PlayerAttackState(PlayerController playerController) : base(playerController)
    {

    }
    public override void Enter()
    {
        var spped = 1f;
        playertController.GetAnimator().speed = spped; // attackSpeed;
    }

    public override void Exit()
    {
        playertController.GetAnimator().speed = 1f;
    }

    public override void Update()
    {
        //웨이브 클리어 자료 받아서 이동 패턴으로 변경 코드 추가 필요

        if(Input.GetKeyUp(KeyCode.Return)) 
        {
            playertController.SetState(States.Move);
            playertController.GetAnimator().SetTrigger("Run");
        }
    }
}

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
        //���̺� Ŭ���� �ڷ� �޾Ƽ� �̵� �������� ���� �ڵ� �߰� �ʿ�

        if(Input.GetKeyUp(KeyCode.Return)) 
        {
            playertController.SetState(States.Move);
            playertController.GetAnimator().SetTrigger("Run");
        }
    }
}

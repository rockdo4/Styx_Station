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
    public override void Enter()
    {
        var spped = defaultSpeed + ((SharedPlayerStats.GetAttackSpeed()-1)* increaseAttackSpeed);
        playertController.GetAnimator().speed = spped;
        playertController.GetAnimator().SetTrigger("Attack");
    }

    public override void Exit()
    {
        playertController.GetAnimator().speed = defaultSpeed;
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

    public override void FixedUpate()
    {
        throw new System.NotImplementedException();
    }
}

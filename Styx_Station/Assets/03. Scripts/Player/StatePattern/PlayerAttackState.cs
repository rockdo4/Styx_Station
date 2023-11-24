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
        //웨이브 클리어 자료 받아서 이동 패턴으로 변경 코드 추가 필요
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

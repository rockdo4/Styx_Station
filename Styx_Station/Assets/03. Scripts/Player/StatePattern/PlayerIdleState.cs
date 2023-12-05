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
        //playertController.GetAnimator().SetTrigger("Idle");
        playertController.GetAnimator().SetFloat("RunState", 0f);
        //playertController.GetAnimator().SetBool("EditChk", false);
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpate()
    {
        var range =  playertController.GetPlayerAttackRange();
        var findEnemey =
        Physics2D.OverlapCircleAll(playertController.transform.position, range,
        playertController.layerMask);

        if (findEnemey.Length < 1) 
        {
            return;
        }
        else
        {
            foreach (var enemy in findEnemey)
            {
                if (enemy.GetComponent<MonsterStats>().currHealth > 0)
                {
                    playertController.SetState(States.Attack);
                    return;
                }
            }
        }
    }

    public override void Update()
    {

    }
}

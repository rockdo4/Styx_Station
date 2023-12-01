using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PetIdle : PetStateBase
{
    public PetIdle(PetController petController) : base(petController)
    {
    }

    public override void Enter()
    {
        petController.GetAnimator().SetTrigger("Idle");
        petController.GetAnimator().SetBool("Run", false);
        petController.GetAnimator().SetFloat("RunState", 0f);
    }

    public override void Exit()
    {

    }

    public override void FixedUpate()
    {
        var findEnemy = Physics2D.OverlapCircleAll(petController.transform.position, petController.range, petController.layerMask);

        if (findEnemy == null)
        {
            return;
        }
        else
        {
            foreach (var enemy in findEnemy)
            {
                if(enemy.GetComponent<MonsterStats>().currHealth >0)
                {
                    petController.SetState(States.Attack);
                    return;
                }
            }
        }
    }

    public override void Update()
    {
        
    }
}

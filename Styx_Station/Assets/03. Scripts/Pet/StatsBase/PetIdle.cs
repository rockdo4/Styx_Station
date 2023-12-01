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
        petController.GetAnimator().SetBool("Attacking",false);
        petController.GetAnimator().SetBool("Run", false);
        petController.GetAnimator().SetFloat("RunState", 0f);
    }

    public override void Exit()
    {

    }

    public override void FixedUpate()
    {
        var master = petController.masterPlayer.GetComponent<PlayerController>();
        if (master != null)
        {
            if ((master.currentStates == States.Move))
            {
                petController.SetState(States.Move);
            }
        }
        var findEnemy = Physics2D.OverlapCircleAll(petController.transform.position, petController.range, petController.layerMask);

        if (findEnemy.Length<1)
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

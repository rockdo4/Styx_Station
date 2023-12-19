using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAttack : PetStateBase
{
    private float timer;
    private float checkMonsterTime = 0.1f;
    public PetAttack(PetController petController) : base(petController)
    {
    }

    public override void Enter()
    {

        petController.GetAnimator().SetTrigger("Attack");
        petController.GetAnimator().SetBool("Attacking",true);
        petController.GetAnimator().SetFloat("NormalState", 0f);
        petController.GetAnimator().speed = petController.attackSpeed;
    }

    public override void Exit()
    {
        petController.GetAnimator().SetTrigger("Idle");
        petController.GetAnimator().speed = 1f;
    }

    public override void FixedUpate()
    {
        //if (petController.masterPlayer.GetComponent<ResultPlayerStats>().playerCurrentHp <= 0)
        if(petController.masterPlayer.GetComponent<PlayerController>().currentStates == States.Die)
        {
            petController.SetState(States.Idle);
            return;
        }
        var findEnemy = Physics2D.OverlapCircleAll(petController.transform.position, petController.range, petController.layerMask);

        if (findEnemy.Length <1||findEnemy[0] == null)
        {
            petController.SetState(States.Idle);
        }

        //var master = petController.masterPlayer.GetComponent<PlayerController>();
        //if (master != null)
        //{
        //    if (petController.currentStates != master.currentStates)
        //    {
        //            petController.SetState(master.currentStates);
        //    }
        //}
    }

    public override void Update()
    {

    }


}

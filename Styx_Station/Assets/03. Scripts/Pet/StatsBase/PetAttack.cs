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
        petController.GetAnimator().SetFloat("NormalState", 0f);
        petController.GetAnimator().speed = petController.attackSpeed;
    }

    public override void Exit()
    {

        petController.GetAnimator().speed = 1f;
    }

    public override void FixedUpate()
    {
        //var findEnemy = Physics2D.OverlapCircleAll(petController.transform.position, petController.range, petController.layerMask);

        //if (findEnemy == null)
        //{
        //    petController.SetState(States.Idle);
        //    return;
        //}

        var master = petController.masterPlayer.GetComponent<PlayerController>();
        if (master != null)
        {
            if (petController.currentStates != master.currentStates)
            {
                    petController.SetState(master.currentStates);
            }
        }
    }

    public override void Update()
    {

    }


}

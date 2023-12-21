using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveState : MonsterStateBase
{
    public Vector2 moveDir = Vector2.left;
    private AttackType attackType;
    private bool isArrived;
    public MonsterMoveState(MonsterController manager, AttackType attackType) : base(manager)
    {
        this.attackType = attackType;
    }

    public override void Enter()
    {
        monsterCtrl.animator.SetFloat("RunState", 0.15f);
    }

    public override void Exit()
    {
        monsterCtrl.animator.SetFloat("RunState", 0f);
        isArrived = false;
    }

    public override void FixedUpate()
    {
        if(!isArrived)
        {
            Vector2 moveVelocity = moveDir.normalized * monsterCtrl.monsterStats.speed;
            monsterCtrl.rigid.MovePosition(monsterCtrl.rigid.position + moveVelocity * Time.deltaTime);
        }
    }

    public override void Update()
    {
        if(monsterStats.currHealth <= 0)
        {
            monsterCtrl.SetState(States.Die);
            return;
        }

        if(DistanceToPlayer <= monsterCtrl.range)
        {
            isArrived = true;
            if(attackType != AttackType.Tank)
            {
                monsterCtrl.SetState(States.Attack);
                return;
            }
            else
            {
                monsterCtrl.animator.SetFloat("RunState", 0f);
            }
        }
    }
}

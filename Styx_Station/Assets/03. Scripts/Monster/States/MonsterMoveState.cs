using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveState : MonsterStateBase
{
    public Vector2 moveDir = Vector2.left;
    public MonsterMoveState(MonsterController manager) : base(manager)
    {

    }

    public override void Enter()
    {
        monsterCtrl.animator.SetFloat("RunState", 0.15f);
    }

    public override void Exit()
    {
        monsterCtrl.animator.SetFloat("RunState", 0f);
    }

    public override void Update()
    {
        //Vector2 pos = monsterCtrl.transform.position;
        //pos.x -= 0.05f;
        //monsterCtrl.rigid.MovePosition(pos);

        if(DistanceToPlayer <= monsterCtrl.range)
        {
            monsterCtrl.SetState(States.Attack);
            return;
        }

        Vector2 moveVelocity = moveDir.normalized * monsterCtrl.monsterStats.speed;
        monsterCtrl.rigid.MovePosition(monsterCtrl.rigid.position + moveVelocity * Time.deltaTime);
    }
}

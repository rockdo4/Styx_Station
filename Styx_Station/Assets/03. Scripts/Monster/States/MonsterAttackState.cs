using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterStateBase
{
    private float AttackBet = 3f; //юс╫ц
    private float lastAttackTime = 0f;
    public MonsterAttackState(MonsterController manager) : base(manager)
    {

    }

    public override void Enter()
    {
        lastAttackTime = Time.time;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if(Time.time - lastAttackTime < AttackBet)
        {
            monsterCtrl.animator.SetTrigger("Attack");
        }
    }
}

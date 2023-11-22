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
        lastAttackTime = 0;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (monsterStats.currHealth <= 0)
        {
            monsterCtrl.SetState(States.Die);
        }
        if (Time.time - lastAttackTime > AttackBet)
        {
            monsterCtrl.animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
    }
}

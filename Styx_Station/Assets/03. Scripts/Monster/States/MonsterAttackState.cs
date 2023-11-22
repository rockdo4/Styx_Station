using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterAttackState : MonsterStateBase
{
    private float attackBet;
    private float lastAttackTime = 0f;
    private float attackOffset = 0; //공격 애니메이션 재생 관리. 0.5: 원거리 공격
    public MonsterAttackState(MonsterController manager) : base(manager)
    {

    }

    public override void Enter()
    {
        lastAttackTime = 0;
        attackBet = monsterCtrl.weapon.cooldown;
        if(monsterStats.attackType == AttackType.Melee)
        {
            attackOffset = 0;
        }
        else if(monsterStats.attackType == AttackType.Ranged)
        {
            attackOffset = 0.5f;
        }
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
        if (Time.time - lastAttackTime > attackBet)
        {
            monsterCtrl.animator.SetTrigger("Attack");
            //monsterCtrl.weapon.ExecuteAttack(monsterCtrl.gameObject, monsterCtrl.target);
            lastAttackTime = Time.time;
        }
    }
}

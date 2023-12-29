using UnityEngine;

public class MonsterAttackState : MonsterStateBase
{
    private float attackBet;
    private float lastAttackTime = 0f;
    private float attackOffset = 0; //공격 애니메이션 재생 관리. 0.5: 원거리 공격
    private float arriveDis = 0.1f;
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
        monsterCtrl.animator.SetFloat("NormalState", attackOffset);
    }

    public override void Exit()
    {

    }

    public override void FixedUpate()
    {

    }

    public override void Update()
    {
        if (monsterStats.currHealth <= 0)
        {
            monsterCtrl.SetState(States.Die);
        }
        if(player.GetComponent<PlayerController>().currentStates == States.Die)
        {
            monsterCtrl.SetState(States.Idle);
        }
        if (DistanceToPlayer > monsterCtrl.range + arriveDis)
        {
            monsterCtrl.SetState(States.Move);
        }
        if (Time.time - lastAttackTime > attackBet)
        {
            monsterCtrl.animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
    }
}

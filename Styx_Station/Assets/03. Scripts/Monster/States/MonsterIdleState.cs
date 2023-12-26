using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterIdleState : MonsterStateBase
{
    protected float timer = 0f;
    private float stunTimer = 0f;
    private float stunTime = 1f;
    private float startStunTime = 0f;

    public MonsterIdleState(MonsterController manager): base(manager) 
    {
    
    }

    public override void Enter()
    {
        timer = 0f;

        if(monsterCtrl.isStunned)
        {
            monsterCtrl.animator.SetFloat("RunState", 1f);
        }
    }

    public override void Exit()
    {
        timer = 0f;
        monsterCtrl.animator.SetFloat("RunState", 0f);
    }

    public override void FixedUpate()
    {

    }

    public override void Update()
    {
        if (monsterCtrl.isTargetDie)
        {
            return;
        }

        if (monsterCtrl.isStunned)
        {
            return;
        }

        if(player.GetComponent<PlayerController>() != null)
        {
            if(player.GetComponent<PlayerController>().currentStates == States.Die)
            {
                return;
            }
        }
        if (monsterStats.currHealth <= 0)
        {
            monsterCtrl.SetState(States.Die);
        }
        timer += Time.deltaTime;
        if(timer > monsterCtrl.startDelay)
        {
            monsterCtrl.SetState(States.Move);
            return;
        }
    }
}

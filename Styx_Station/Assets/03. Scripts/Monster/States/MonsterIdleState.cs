using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterIdleState : MonsterStateBase
{
    protected float timer = 0f;
    public MonsterIdleState(MonsterController manager): base(manager) 
    {
    
    }

    public override void Enter()
    {
        timer = 0f;
    }

    public override void Exit()
    {

    }

    public override void FixedUpate()
    {

    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if(timer > monsterCtrl.startDelay)
        {
            monsterCtrl.SetState(States.Move);
            return;
        }
    }
}

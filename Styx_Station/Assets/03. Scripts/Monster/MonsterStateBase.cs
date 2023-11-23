using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterStateBase : StateBase
{
    protected MonsterController monsterCtrl;
    protected GameObject player;
    protected MonsterStats monsterStats;
    //protected Animator animator;
    //protected Rigidbody2D rigidbody;

    public float DistanceToPlayer
    {
        get
        {
            if(player == null)
            {
                return 0f;
            }
            return monsterCtrl.transform.position.x - player.transform.position.x;  
            //return Vector2.Distance(player.transform.position, monsterCtrl.transform.position);
        }
    }

    public MonsterStateBase(MonsterController monsterCtrl)
    {
        this.monsterCtrl = monsterCtrl;
        player = GameObject.FindGameObjectWithTag("Player");
        monsterStats = monsterCtrl.GetComponent<MonsterStats>();
        //animator = monsterCtrl.GetComponentInChildren<Animator>();
        //rigidbody = monsterCtrl.GetComponent<Rigidbody2D>();
    }
}

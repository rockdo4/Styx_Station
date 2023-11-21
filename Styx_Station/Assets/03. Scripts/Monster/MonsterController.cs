using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private StateManager stateManager = new StateManager();
    private List<StateBase> states = new List<StateBase>();

    public float startDelay = 1f; //움직이기 시작할 시간, 스포너에서 생성 시에 세팅해줄 값.
    public Animator animator { get; private set; }
    public Rigidbody2D rigid { get; private set; }
    public MonsterStats monsterStats { get; private set; }

    public float range; //임시사용 - 공격 사거리

    public void SetState(States newState)
    {
        stateManager.ChangeState(states[(int)newState]);
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        monsterStats = GetComponentInChildren<MonsterStats>();
    }
    private void Start()
    {
        states.Add(new MonsterIdleState(this));
        states.Add(new MonsterMoveState(this));
        states.Add(new MonsterAttackState(this));

        SetState(States.Idle);
    }

    private void Update()
    {
        stateManager.Update();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private StateManager stateManager = new StateManager();
    private List<StateBase> states = new List<StateBase>();

    public float startDelay = 1f; //�����̱� ������ �ð�, �����ʿ��� ���� �ÿ� �������� ��.
    public Animator animator { get; private set; }
    public Rigidbody2D rigid { get; private set; }
    public MonsterStats monsterStats { get; private set; }

    public float range; //�ӽû�� - ���� ��Ÿ�

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

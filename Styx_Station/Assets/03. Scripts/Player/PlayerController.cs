using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private int characterLevel;
    //private bool isLock;

    private StateManager playerStateManager = new StateManager();
    private List<StateBase> playerStateBases = new List<StateBase>();
    private Animator animator;


    [Header("�÷��̾� 1�ʴ� �̵��ϴ� �ӵ�")]
    public float playerMoveSpeed = 3f;
    [Header("��׶��� ����-> ��׶��� / �÷��̾� �ӵ��� ���� ����")]
    public float backgroundLength = 6f;


    public Transform destinationPoint; // ���� ���۽� ���� ����
    public float playerStartTime = 0.2f; // ���ӽ������ڸ� �÷��̾� �̵��� ��ġ ���
    private float playerStartTimer; // �ð���������

    public bool IsAtTarget { get; set; }

    public void Awake()
    {
        playerStateBases.Add(new PlayerIdleState(this));
        playerStateBases.Add(new PlayerMoveState(this));
        playerStateBases.Add(new PlayerAttackState(this));


        animator = GetComponentInChildren<Animator>();


        SetState(States.Move);

    }
    private void Start()
    {

    }
    public void Update()
    {
        playerStateManager.Update();
        playerStartTimer += Time.deltaTime;
        if (!IsAtTarget && playerStartTimer > playerStartTime)
        {

            transform.position = Vector3.Lerp(transform.position, destinationPoint.position, playerMoveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destinationPoint.position) < 0.1f)
            {
                IsAtTarget = true;
                SetState(States.Idle);
            }
        }
    }

    public void SetState(States newState)
    {
        playerStateManager.ChangeState(playerStateBases[(int)newState]);
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}

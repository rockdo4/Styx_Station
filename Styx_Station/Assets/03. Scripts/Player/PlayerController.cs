using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private int characterLevel;
    //private bool isLock;

    private StateManager playerStateManager = new StateManager();
    private List<StateBase> playerStateBases = new List<StateBase>();
    private Animator animator;


    [Header("플레이어 1초당 이동하는 속도")]
    public float playerMoveSpeed = 3f;
    [Header("백그라운드 길이-> 백그라운드 / 플레이어 속도로 상태 변경")]
    public float backgroundLength = 6f;


    public Transform destinationPoint; // 게임 시작시 도착 지점
    public float playerStartTime = 0.2f; // 게임시작하자마 플레이어 이동시 위치 계산
    private float playerStartTimer; // 시간가질예정

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

using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private int characterLevel; // 캐릭터 강화 
    //private bool isLock; // 캐릭터 해금 

    private StateManager playerStateManager = new StateManager();
    private List<StateBase> playerStateBases = new List<StateBase>();
    private Animator animator;

    // ?? 
    [Header("플레이어 1초당 이동하는 속도")]
    public float playerMoveSpeed = 3f;
    [Header("백그라운드 길이-> 백그라운드 / 플레이어 속도로 상태 변경")]
    public float backgroundLength = 6f;
    // ??

    public Transform destinationPoint; // 게임 시작시 도착 지점 // 백그라운드 및 설정되면 다해야함
    public float playerStartTime = 0.2f; // 게임시작하자마 플레이어 이동시 위치 계산 // 최종적으로 변경 되어야함 
    private float playerStartTimer; // 시간가질예정

    private float range; // player공격 range 
    public LayerMask layerMask;
    

    public bool IsStartTarget { get; set; } 

    public void Awake()
    {
        playerStateBases.Add(new PlayerIdleState(this));
        playerStateBases.Add(new PlayerMoveState(this));
        playerStateBases.Add(new PlayerAttackState(this));


        animator = GetComponentInChildren<Animator>();

        range = GetComponent<PlayerAttributes>().playerAttackRange;

        SetState(States.Move);

    }
    private void Start()
    {
        

    }
    private void FixedUpdate()
    {
        /// <summary>
        /// //monster가 스폰이 되었는지에 대한 정보가 필요하면 좋을 것같음
        /// <summary>
        ShootLayMaskCircle();
    }
    public void Update()
    {
        playerStateManager.Update();
        if (!IsStartTarget)
        {
            StartMove();
        }
    }


    /// <summary>
    /// private
    /// <summary>
    
    private void StartMove()
    {
        playerStartTimer += Time.deltaTime;
        if (playerStartTimer > playerStartTime)
        {

            transform.position = Vector3.Lerp(transform.position, destinationPoint.position, playerMoveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destinationPoint.position) < 0.1f)
            {
                IsStartTarget = true;
                SetState(States.Idle);
            }
        }
    } // 시작되면 한번 플레이어가 밖에서 앞으로 오는 것으로 변경해둠

    private void ShootLayMaskCircle()
    {
        var test = Physics2D.OverlapCircle(transform.position, range, layerMask);

        if (test != null)
            SetState(States.Attack);
    }


    /// <summary>
    /// public 
    /// <summary>

    public void SetState(States newState)
    {
        playerStateManager.ChangeState(playerStateBases[(int)newState]);
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public float GetPlayerAttackRange()
    {
        return range;
    }



    /// <summary>
    /// 테스트 위한 함수들 밑의 함수들은 무조건 지워야함
    /// <summary>

    private void OnDrawGizmos()
    {
        var prevColor = Gizmos.color;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GetPlayerAttackRange());

        Gizmos.color = prevColor;
    }

    
}

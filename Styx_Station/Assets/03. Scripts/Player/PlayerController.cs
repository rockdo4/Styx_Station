using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class PlayerController : MonoBehaviour
{
    //private int characterLevel; // 캐릭터 강화 
    //private bool isLock; // 캐릭터 해금 

    private StateManager playerStateManager = new StateManager();
    private List<StateBase> playerStateBases = new List<StateBase>();
    private Animator animator;
    private ExcuteAttackPlayer executeHit;
    public AttackDefinition weapon;

    public float playerMoveSpeed = 3f;
    public float backgroundLength = 6f;
    // ??

    public Transform destinationPoint; // 게임 시작시 도착 지점 // 백그라운드 및 설정되면 다해야함
    public float playerStartTime = 0.2f; // 게임시작하자마 플레이어 이동시 위치 계산 // 최종적으로 변경 되어야함 
    private float playerStartTimer; // 시간가질예정

    private float range; // player공격 range 
    public LayerMask layerMask;
    
    public Vector2 initialPos = Vector2.zero;

    public bool IsStartTarget { get; set; }
    public States currentStates;

    public LayerMask enemyLayer;
    
    public void Awake()
    {
        playerStateBases.Add(new PlayerIdleState(this));
        playerStateBases.Add(new PlayerMoveState(this));
        playerStateBases.Add(new PlayerAttackState(this));
        playerStateBases.Add(new PlayerDieState(this));


        animator = GetComponentInChildren<Animator>();

        range = GetComponent<PlayerAttributes>().playerAttackRange;

        SetState(States.Move);

        executeHit = GetComponentInChildren<ExcuteAttackPlayer>();

        initialPos = transform.position;
    }
    private void FixedUpdate()
    {
        /// <summary>
        /// //monster가 스폰이 되었는지에 대한 정보가 필요하면 좋을 것같음
        /// <summary>
        playerStateManager.FixedUpdate();
        //ShootLayMaskCircle();
    }
    public void Update()
    {
        //Debug.Log(playerStateManager.GetCurrentState());
        //Debug.Log(gameObject.GetComponent<ResultPlayerStats>().playerCurrentHp);
        playerStateManager.Update();
        if (!IsStartTarget)
        {
            StartMove();
        }
    }

    public void SetExcuteHit()
    {
        executeHit.weapon = weapon;
        executeHit.attacker = gameObject;
        var currPos = gameObject.transform.position;
        currPos.y += 0.5f;
        var target = Physics2D.Raycast(currPos, Vector2.right, range, enemyLayer);
        Debug.DrawRay(gameObject.transform.position, Vector2.right * range, Color.red, 1f); // 시작점, 방향, 색상, 지속시간
        if (target.collider != null)
        {
            executeHit.target = target.transform.gameObject;
        }
        else
        {
            executeHit.target = null;
        }
        //executeHit.target = target.transform.gameObject;
    }

    /// <summary>
    /// private
    /// <summary>

    private void StartMove()
    {
        if(destinationPoint == null)
        {
            IsStartTarget = true;
            return;
        }
        playerStartTimer += Time.deltaTime;
        if (playerStartTimer > playerStartTime)
        {
            SetState(States.Move);
            transform.position = Vector3.Lerp(transform.position, destinationPoint.position, playerMoveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destinationPoint.position) < 0.1f)
            {
                IsStartTarget = true;
                SetState(States.Idle);
                WaveManager.instance.StartWave();
                playerStartTimer = 0f;
            }
        }
    } // 시작되면 한번 플레이어가 밖에서 앞으로 오는 것으로 변경해둠

    private void ShootLayMaskCircle()
    {
        var findEnemey = Physics2D.OverlapCircle(transform.position, range, layerMask);

        if (findEnemey != null)
            SetState(States.Attack);
    }

    public void SetState(States newState)
    {
        playerStateManager.ChangeState(playerStateBases[(int)newState]);
        currentStates = newState;
        //Debug.Log(playerStateManager.GetCurrentState());
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public float GetPlayerAttackRange()
    {
        return range;
    }

    public StateBase GetPlayerCurrentState()
    {
        return playerStateManager.GetCurrentState();
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

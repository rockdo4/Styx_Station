using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class PlayerController : MonoBehaviour
{
    //private int characterLevel; // ĳ���� ��ȭ 
    //private bool isLock; // ĳ���� �ر� 

    private StateManager playerStateManager = new StateManager();
    private List<StateBase> playerStateBases = new List<StateBase>();
    private Animator animator;
    private ExcuteAttackPlayer executeHit;
    public AttackDefinition weapon;

    public float playerMoveSpeed = 3f;
    public float backgroundLength = 6f;
    // ??

    public Transform destinationPoint; // ���� ���۽� ���� ���� // ��׶��� �� �����Ǹ� ���ؾ���
    public float playerStartTime = 0.2f; // ���ӽ������ڸ� �÷��̾� �̵��� ��ġ ��� // ���������� ���� �Ǿ���� 
    private float playerStartTimer; // �ð���������

    private float range; // player���� range 
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
        /// //monster�� ������ �Ǿ������� ���� ������ �ʿ��ϸ� ���� �Ͱ���
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
        Debug.DrawRay(gameObject.transform.position, Vector2.right * range, Color.red, 1f); // ������, ����, ����, ���ӽð�
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
    } // ���۵Ǹ� �ѹ� �÷��̾ �ۿ��� ������ ���� ������ �����ص�

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
    /// �׽�Ʈ ���� �Լ��� ���� �Լ����� ������ ��������
    /// <summary>

    private void OnDrawGizmos()
    {
        var prevColor = Gizmos.color;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GetPlayerAttackRange());

        Gizmos.color = prevColor;
    }

    
}

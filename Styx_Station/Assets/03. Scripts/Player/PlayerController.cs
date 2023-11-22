using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private int characterLevel; // ĳ���� ��ȭ 
    //private bool isLock; // ĳ���� �ر� 

    private StateManager playerStateManager = new StateManager();
    private List<StateBase> playerStateBases = new List<StateBase>();
    private Animator animator;

    // ?? 
    [Header("�÷��̾� 1�ʴ� �̵��ϴ� �ӵ�")]
    public float playerMoveSpeed = 3f;
    [Header("��׶��� ����-> ��׶��� / �÷��̾� �ӵ��� ���� ����")]
    public float backgroundLength = 6f;
    // ??

    public Transform destinationPoint; // ���� ���۽� ���� ���� // ��׶��� �� �����Ǹ� ���ؾ���
    public float playerStartTime = 0.2f; // ���ӽ������ڸ� �÷��̾� �̵��� ��ġ ��� // ���������� ���� �Ǿ���� 
    private float playerStartTimer; // �ð���������

    private float range; // player���� range 
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
        /// //monster�� ������ �Ǿ������� ���� ������ �ʿ��ϸ� ���� �Ͱ���
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
    } // ���۵Ǹ� �ѹ� �÷��̾ �ۿ��� ������ ���� ������ �����ص�

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

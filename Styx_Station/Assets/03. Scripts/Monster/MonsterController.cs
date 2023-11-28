using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MonsterController : PoolAble //MonoBehaviour
{
    private StateManager stateManager = new StateManager();
    private List<StateBase> states = new List<StateBase>();

    public float startDelay = 1f; //�����̱� ������ �ð�, �����ʿ��� ���� �ÿ� �������� ��.
    public Animator animator { get; private set; }
    public Rigidbody2D rigid { get; private set; }
    public MonsterStats monsterStats { get; private set; }

    public float range; //�ӽû�� - ���� ��Ÿ�
    public AttackDefinition weapon;
    public GameObject target;
    public ExecuteHit executeHit;

    private List<Color> originalColor = new List<Color>(); 

    public void SetState(States newState)
    {
        stateManager.ChangeState(states[(int)newState]);
    }

    public void SetOriginalColor(List<Color> colorList)
    {
        originalColor = colorList;
    }

    public List<Color> GetOriginalColor()
    {
        return originalColor;
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        monsterStats = GetComponentInChildren<MonsterStats>();
        target = GameObject.FindGameObjectWithTag("Player");
        executeHit = GetComponentInChildren<ExecuteHit>();

    }
    private void Start()
    {
        states.Add(new MonsterIdleState(this));
        states.Add(new MonsterMoveState(this));
        states.Add(new MonsterAttackState(this));
        states.Add(new MonsterDieState(this));

        SetState(States.Idle);
    }

    public void SetExcuteHit()
    {
        executeHit.weapon = weapon;
        executeHit.target = target;
        executeHit.attacker = gameObject;
    }

    public void SetSpawnPosition(int spawnYPosCount, float spawnYPosSpacing)
    {
        int randNum = Random.Range(0, spawnYPosCount);
        float newYPos = transform.position.y - randNum * spawnYPosSpacing;
        transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
        SortingGroup sortingGroup = GetComponentInChildren<SortingGroup>();
        if(sortingGroup!=null)
        {
            sortingGroup.sortingOrder += randNum;
        }
        else
        {
            Debug.Log("ERR: No SortingGroup");
        }
    }
    private void FixedUpdate()
    {
        stateManager.FixedUpdate();
    }
    private void Update()
    {
        stateManager.Update();
    }

    //public void Hit()
    //{
    //    if(weapon == null || target == null)
    //    {
    //        return;
    //    }
    //    weapon.ExecuteAttack(gameObject, target);
    //}
}

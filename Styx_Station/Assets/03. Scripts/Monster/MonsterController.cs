using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;

public class MonsterController : PoolAble //MonoBehaviour
{
    private StateManager stateManager = new StateManager();
    private List<StateBase> states = new List<StateBase>();

    public float startDelay = 1f; //움직이기 시작할 시간, 스포너에서 생성 시에 세팅해줄 값.
    public Animator animator { get; private set; }
    public Rigidbody2D rigid { get; private set; }
    public MonsterStats monsterStats { get; private set; }

    public float range; //임시사용 - 공격 사거리
    public AttackDefinition weapon;
    public GameObject target;
    public ExecuteHit executeHit;

    private List<Color> originalColor = new List<Color>();

    public bool isTargetDie = false;

    public Transform idlePos { get; set; }
    private bool isPoisioned { get; set; }
    private float duration;
    private Attack poisionAttack = new Attack();
    private GameObject attacker;
    private float timer = 0f;

    private Coroutine poisonCo;

    public GameObject skullImage;
    public GameObject lightningImage;

    private int initialSortingOrder = 0;

    public int coin;
    public int pomegranate;
    public void SetState(States newState)
    {
        stateManager.ChangeState(states[(int)newState]);
        //Debug.Log(stateManager.GetCurrentState());
    }

    public void SetIdlePoint(Transform idlePos)
    {
        this.idlePos = idlePos;
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

        skullImage.SetActive(false);

        initialSortingOrder = GetComponentInChildren<SortingGroup>().sortingOrder;
    }
    private void Start()
    {
        states.Add(new MonsterIdleState(this));
        states.Add(new MonsterMoveState(this));
        states.Add(new MonsterAttackState(this));
        states.Add(new MonsterDieState(this));

        SetState(States.Idle);
    }

    public void SetMoney(int c, int p)
    {
        coin = c;
        pomegranate = p;
    }

    public void SetExcuteHit()
    {
        if(executeHit == null)
        {
            Debug.Log("ERR: No executeHit");
            return;
        }
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
        if(target.GetComponent<ResultPlayerStats>() != null)
        {
            //Debug.Log(target.GetComponent<ResultPlayerStats>().playerCurrentHp);
            if(target.GetComponent<ResultPlayerStats>().playerCurrentHp <= 0)
            {
                //SetState(States.Idle);
                isTargetDie = true;
            }
        }

        if(isPoisioned)
        {
            if(!skullImage.activeSelf)
            {
                skullImage.SetActive(true);
            }
            timer += Time.deltaTime;
            if(timer >= duration)
            {
                isPoisioned = false;
                timer = 0f;
                StopCoroutine(poisonCo);
                poisonCo = null;
                if (skullImage.activeSelf)
                {
                    skullImage.SetActive(false);
                }
                return;
            }
            if (poisonCo == null)
            {
                poisonCo = StartCoroutine(CoAttackedPoision());
            }
        }
        stateManager.Update();
    }

    public void SetPoision(float du, Attack attack, GameObject a)
    {
        isPoisioned = true;
        duration = du;
        poisionAttack = attack;
        attacker = a;
        timer = 0f;
    }
    IEnumerator CoAttackedPoision()
    {
        while(true)
        {
            if (monsterStats.currHealth <= 0)
            {
                yield break;
            }
            var attackables = gameObject.GetComponents<IAttackable>();
            Debug.Log("hit poison");
            foreach (var attackable in attackables)
            {
                attackable.OnAttack(attacker, poisionAttack);
            }
            yield return new WaitForSeconds(1);
        }
    }
    public override void ReleaseObject()
    {
        GetComponentInChildren<SortingGroup>().sortingOrder = initialSortingOrder;
        base.ReleaseObject();
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

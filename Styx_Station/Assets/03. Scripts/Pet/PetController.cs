using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public enum PetLevel
    {
        A, B, C,D,E,
    }
    public GameObject petObjectScript;
    private Animator animator;
    public GameObject masterPlayer;
    public string petName;
    public PetLevel petlevel;
    public System.Numerics.BigInteger power;
    [Range(0f, 2f)]
    public float attackSpeed;
    public float range;

    private bool isArrive;
    private float delay ;
    private StateManager stateManager = new StateManager();
    private List<StateBase> petStateBases = new List<StateBase>();
    private ExcuteAttackPet executeHit ; 
    public AttackDefinition weapon ;
    public LayerMask layerMask;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        masterPlayer = GameObject.FindGameObjectWithTag("Player");
        executeHit = GetComponentInChildren<ExcuteAttackPet>();

        petStateBases.Add(new PetIdle(this));
        petStateBases.Add(new PetRun(this));
        petStateBases.Add(new PetAttack(this));

       
        SetState(States.Move);
    }

    private void Start()
    {
        if(petObjectScript != null)
        {
            //petName = petObjectScript.name;
            //petlevel = petObjectScript.level;
            //power = petObjectScript.power;
            //attackSpeed =petObjectScript.attackSpeed;
            //range = petObjectScript.range
        }
        
    }
    private void FixedUpdate()
    {
        stateManager.FixedUpdate();
        //petController.GetAnimator().speed = petController.animationSpeed;
    }
    private void Update()
    {
        stateManager.Update();
        if (masterPlayer == null)
        {
            return;
        }
        else
        {
            delay += Time.deltaTime;
            if (delay > 0.3f && !isArrive)
            {
                var pos = masterPlayer.transform.position;
                pos.y = transform.position.y;
                transform.position = Vector2.Lerp(transform.position, pos, masterPlayer.GetComponent<PlayerController>().playerMoveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, pos) <= 0.5f)
                {
                    isArrive = true;
                    SetState(States.Idle);
                }
            }
        }
    }
    public void  SetState(States state)
    {
        stateManager.ChangeState(petStateBases[(int)state]);
    }

    public Animator GetAnimator()
    {
        return animator;    
    }
    public void OnDrawGizmos()
    {

        var prevColor = Gizmos.color;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = prevColor;
    }
    
    public void SetExcuteHit()
    {
        var currPos = gameObject.transform.position;
        currPos.y += 0.5f;
        var target = Physics2D.OverlapCircle(transform.position, range, layerMask);
        if (target != null)
        {
            executeHit.weapon = weapon;
            executeHit.attacker = gameObject;
            power = (masterPlayer.GetComponent<ResultPlayerStats>().GetPlayerPower() * (int)petlevel * 10) / 100;
            executeHit.target = target.gameObject;
        }
    }

    public System.Numerics.BigInteger GetPower()
    {
        if (masterPlayer != null)
            return -1;
        return masterPlayer.GetComponent<ResultPlayerStats>().GetPlayerPower() * (int)petlevel * 10/ 100;
    }
}

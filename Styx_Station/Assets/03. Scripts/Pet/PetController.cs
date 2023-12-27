using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{

    public Pet petObjectScript;
    private Animator animator;
    [HideInInspector]public GameObject masterPlayer;
    public string petName;
    public Tier petTier; // 식 변경예정
    public System.Numerics.BigInteger power;
    [Range(0f, 2f)]
    public float attackSpeed;
    public float range;

    public bool isArrive = false;
    private float delay = 0;
    private StateManager petStateManager = new StateManager();
    private List<StateBase> petStateBases = new List<StateBase>();
    private ExcuteAttackPet executeHit ; 
    public AttackDefinition weapon ;
    public LayerMask layerMask;
    public States currentStates;


    public Vector2 initialPos = Vector2.zero;

    [HideInInspector] public Transform lerpPos;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        masterPlayer = GameObject.FindGameObjectWithTag("Player");
        executeHit = GetComponentInChildren<ExcuteAttackPet>();

        petStateBases.Add(new PetIdle(this));
        petStateBases.Add(new PetRun(this));
        petStateBases.Add(new PetAttack(this));

        initialPos = transform.position;

        
    }

    private void Start()
    {
        if(petObjectScript != null)
        {
            petName = petObjectScript.Pet_GameObjet.name;
            petTier = petObjectScript.Pet_Tier;
            attackSpeed =petObjectScript.Pet_AttackSpeed;
            range = petObjectScript.Pet_AttackRange;
            var ani = GetComponentInChildren<Animator>();
            if(ani != null )
            {
                ani.runtimeAnimatorController = petObjectScript.animation;
            }
        }
        SetState(States.Move);
    }
    private void FixedUpdate()
    {
        petStateManager.FixedUpdate();
        //petController.GetAnimator().speed = petController.animationSpeed;
    }
    private void Update()
    {
        petStateManager.Update();
        if (masterPlayer == null)
        {
            return;
        }
        else
        {
            delay += Time.deltaTime;
            if (delay > 1.8f && !isArrive && petObjectScript == null)
            {
                var pos = masterPlayer.transform.position;
                pos.y = transform.position.y;
                transform.position = Vector2.Lerp(transform.position, pos, masterPlayer.GetComponent<PlayerController>().playerMoveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, pos) <= 0.5f)
                {
                    isArrive = true;
                    //delay = 0;
                    SetState(States.Idle);
                }
            }
            else if(delay > 1.8f && !isArrive && petObjectScript != null)
            {
                var pos = lerpPos.transform.position;
                pos.y = lerpPos.position.y;
                transform.position = Vector2.Lerp(transform.position, pos, masterPlayer.GetComponent<PlayerController>().playerMoveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, pos) <= 0.5f)
                {
                    isArrive = true;
                    transform.position = lerpPos.transform.position;
                    SetState(States.Idle);
                }
            }
        }
    }
    public void  SetState(States state)
    {
        petStateManager.ChangeState(petStateBases[(int)state]);
        currentStates = state;
        //Debug.Log(currentStates);
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
            power = (masterPlayer.GetComponent<ResultPlayerStats>().GetPlayerPower() * (int)petTier * 10) / 100;
            executeHit.target = target.gameObject;
        }
    }

    public System.Numerics.BigInteger GetPower()
    {
        if (masterPlayer == null)
            return -1;
        return masterPlayer.GetComponent<ResultPlayerStats>().GetPlayerPowerByNonInventory() * (int)petTier * 10/ 100;
    }
    public StateManager GetPetStateManager()
    {
        return petStateManager;
    }
}

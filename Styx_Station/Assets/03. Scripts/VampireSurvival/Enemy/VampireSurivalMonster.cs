using UnityEngine;

public class VampireSurivalMonster : PoolAble
{
    [HideInInspector] private float maxHp;
    [HideInInspector] public float currentHp;
    public float speed;
    private float timer;
    public float findPlayerTimer;
    private float damage;
    private Vector3 direction;
    private Rigidbody2D rb;
    private VamprieSurivalPlayerController player;

    public Animator animator;
    private float animatorRunValue;
    public float attackDelay;
    [HideInInspector]public float nowTime;
    [HideInInspector]public bool isAttaking;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (VampireSurvivalGameManager.Instance.isPause || VampireSurvivalGameManager.Instance.isGameover)
        {
            return;
        }
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }

        if(!isAttaking)
        {
            var pos = transform.position;
            pos += (direction * speed * Time.fixedDeltaTime);
            rb.MovePosition(pos);
            animator.SetFloat("RunState", animatorRunValue);
        }
        else
        {
            direction = Vector3.zero;
            animatorRunValue = direction.magnitude;
            animator.SetFloat("RunState", animatorRunValue);
        }
    }

    public void Update()
    {
        if (VampireSurvivalGameManager.Instance.isPause || VampireSurvivalGameManager.Instance.isGameover)
        {
            if (VampireSurvivalGameManager.Instance.isGameover)
            {
                direction = Vector3.zero;
                animatorRunValue = direction.magnitude;
                animator.SetFloat("RunState", animatorRunValue);
            }
            return;
        }
        if (!isAttaking)
        {
            timer += Time.deltaTime;
        }
        if (timer >= findPlayerTimer)
        {
            timer = 0;
            FindPlayer();
        }
    }
    public void FindPlayer()
    {
        PlayerCheck();
        direction = (player.transform.position - gameObject.transform.position).normalized;

        animatorRunValue = direction.magnitude;
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("VampirePlayer"))
    //    {
    //        collision.gameObject.GetComponent<VamprieSurivalPlayerController>().OnCollisonMonster(damage);
    //        isAttaking = true;
    //        direction = Vector3.zero;
    //        timer = 0;
    //        animatorRunValue = direction.magnitude;
    //        animator.SetFloat("RunState", animatorRunValue);
    //    }
    //}
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("VampirePlayer"))
    //    {
    //        if (attackDelay + nowTime < Time.time)
    //        {
    //            nowTime = Time.time;
    //            collision.gameObject.GetComponent<VamprieSurivalPlayerController>().OnCollisonMonster(damage);
    //            animator.SetFloat("RunState", animatorRunValue);
    //        }
    //    }
    //    if(collision.CompareTag("VampireArrow"))
    //    {
    //        GetDamage(collision.GetComponent<VamprieSurivalPlayerAttackType>().damage);
    //        collision.GetComponent<VamprieSurivalPlayerAttackType>().ReleaseObject();
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("VampirePlayer"))
    //    {
    //        isAttaking = false;
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.collider.CompareTag("VampirePlayer"))
        //{
        //    nowTime = Time.time;
        //    collision.gameObject.GetComponent<VamprieSurivalPlayerController>().OnCollisonMonster(damage);
        //    isAttaking = true;
        //    direction = Vector3.zero;
        //    timer = 0;
        //    animatorRunValue = direction.magnitude;
        //    animator.SetFloat("RunState", animatorRunValue);
        //}
        if (collision.collider.CompareTag("VampireArrow"))
        {
            SetDamage(collision.collider.GetComponent<VamprieSurivalPlayerAttackType>().damage);
            collision.collider.GetComponent<VamprieSurivalPlayerAttackType>().ReleaseObject();
        }
    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("VampirePlayer"))
    //    {
    //        if (attackDelay + nowTime < Time.time)
    //        {
    //            nowTime = Time.time;
    //            collision.gameObject.GetComponent<VamprieSurivalPlayerController>().OnCollisonMonster(damage);
    //            animator.SetFloat("RunState", animatorRunValue);
    //        }
    //    }
        
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("VampirePlayer"))
    //    {
    //        isAttaking = false;
    //    }
    //}

    public void BornMonster()
    {
        maxHp = 2 + (1 - 1); // 추후 현재 웨이브로 -1
        damage = 1 + (1 - 1);// 동일
        currentHp = maxHp;
        PlayerCheck();
        FindPlayer();
    }

    public void SetDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            var sliver = ObjectPoolManager.instance.GetGo("VampireSilver_");
            sliver.transform.position = transform.position;
            ReleaseObject();
            VampireSurvivalGameManager.Instance.deathMonsterCount++;
        }
    }
    public override void ReleaseObject()
    {
        if (gameObject.activeSelf)
        {
            ObjPool.Release(gameObject);
        }
    }
    private void PlayerCheck()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("VampirePlayer").GetComponent<VamprieSurivalPlayerController>();
        }
    }

    public float GetDamge()
    {
        return damage;
    }
}

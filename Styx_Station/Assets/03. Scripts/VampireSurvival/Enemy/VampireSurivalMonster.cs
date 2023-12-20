using UnityEngine;

public class VampireSurivalMonster : PoolAble
{
    [HideInInspector] private int maxHp;
    [HideInInspector] public int hp;
    public float speed;
    private float timer;
    public float findPlayerTimer;
    private int damage;
    private Vector3 direction;
    private Rigidbody2D rb;
    private VamprieSurivalPlayerController player;

    public Animator animator;
    private float animatorRunValue;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        maxHp = 2 + (1 - 1); // 추후 현재 웨이브로 -1
        damage = 1 + (1 - 1);// 동일
        hp = maxHp;
        if (player == null)
        {
            player = GameObject.FindWithTag("VampirePlayer").GetComponent<VamprieSurivalPlayerController>();
        }
        FindPlayer();
    }

    private void FixedUpdate()
    {
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }

        var pos = transform.position;
        pos += (direction * speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        animator.SetFloat("RunState", animatorRunValue);
    }

    public void Update()
    {
        if (VampireSurvivalGameManager.Instance.isPause || VampireSurvivalGameManager.Instance.isGameover)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= findPlayerTimer)
        {
            timer = 0;
            FindPlayer();
        }
    }
    public void FindPlayer()
    {

        direction = (player.transform.position - gameObject.transform.position).normalized;

        animatorRunValue = direction.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("VampirePlayer"))
        {
            collision.gameObject.GetComponent<VamprieSurivalPlayerController>().OnCollisonMonster(damage);
            direction = Vector3.zero;
            timer = 0;
            animatorRunValue = direction.magnitude;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSurivalMonster : PoolAble
{
    public int maxHp;
    public int hp;
    public float speed;
    private float timer;
    public float findPlayerTimer;
    private Vector2 direction;
    private Rigidbody2D rb;
    private VamprieSurivalPlayerController player;


    private void Awake()
    {
        rb =GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        maxHp = 2 + (1-1); // 추후 현재 웨이브로 -1
        if(player ==null)
        {
            player = GameObject.FindWithTag("VampirePlayer").GetComponent<VamprieSurivalPlayerController>();
        }
        FindPlayer();
    }

    private void FixedUpdate()
    {
        var velocity = rb.velocity;
        velocity += direction * speed * Time.fixedDeltaTime;
        rb.velocity = velocity;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if(timer >=findPlayerTimer)
        {
            timer = 0;
            FindPlayer();
        }
    }
    public void FindPlayer()
    {
        direction = (player.transform.position - gameObject.transform.position).normalized;
    }
}

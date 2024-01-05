using System.Collections;
using UnityEngine;

public class ArrowRainShooter : Shooter
{
    private Vector2 pointA; //좌하단
    private Vector2 pointB; //우상단
    private LayerMask monsterLayer;

    private GameObject player;

    private float damageMultiplier = 0;
    private float hitBet = 1f;
    private WaitForSeconds waitForHit;

    private float duration;
    private float startTime = 0;

    private float startPosX;

    private GameObject castZone;
    private AudioSource audioSourece;
    public AudioClip arrwoRainClip;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSourece=GetComponent<AudioSource>();
        waitForHit = new WaitForSeconds(hitBet);
    }

    public void SetShooter(/*Vector2 pA, Vector2 pB, */LayerMask mL, float mul, float du, float stP, GameObject castZone)
    {
        monsterLayer = mL;
        damageMultiplier = mul;
        duration = du;
        startPosX = stP;
        this.castZone = castZone;

        pointA = player.transform.position;
        pointA.x = pointA.x + startPosX;
        pointB = new Vector2(pointA.x + 2, pointA.y + 10);

        startTime = Time.time;
        StartCoroutine(CastSkill());
        castZone.gameObject.transform.position = pointA;
        castZone.SetActive(true);
    }

    //private void Start()
    //{
    //    startTime = Time.time;
    //    StartCoroutine(CastSkill());
    //    castZone.gameObject.transform.position = pointA;
    //    castZone.SetActive(true);
    //}

    private void Update()
    {
        if(startTime + duration < Time.time)
        {
            ReleaseObject();
            //castZone.SetActive(false);  
            //Destroy(gameObject);
        }
    }

    public Collider2D[] GetMonsterInZone()
    {
        return Physics2D.OverlapAreaAll(pointA, pointB, monsterLayer);
    }

    IEnumerator CastSkill()
    {
        if (audioSourece == null)
            audioSourece = GetComponent<AudioSource>();
        audioSourece.PlayOneShot(arrwoRainClip);
        while (true)
        {
            var monsters = GetMonsterInZone();
            foreach (var monster in monsters)
            {
                HitMonster(monster.gameObject);
            }

            yield return waitForHit;
        }
    }

    public void HitMonster(GameObject monster)
    {
        if (monster == null)
        {
            return;
        }

        var attackerStats = player.GetComponent<ResultPlayerStats>();
        var target = monster.GetComponent<MonsterStats>();

        if (target.currHealth <= 0)
        {
            return;
        }
        
        Attack attack = CreateAttackToMonster(attackerStats, target, damageMultiplier);

        var attackables = monster.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(player, attack);
        }
    }

    public override void ReleaseObject()
    {
        castZone.SetActive(false);
        base.ReleaseObject();
    }
}

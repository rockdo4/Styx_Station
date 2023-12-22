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

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waitForHit = new WaitForSeconds(hitBet);
    }

    public void SetShooter(/*Vector2 pA, Vector2 pB, */LayerMask mL, float mul, float du, float stP, GameObject castZone)
    {
        //pointA = pA;
        //pointB = pB;
        monsterLayer = mL;
        damageMultiplier = mul;
        duration = du;
        startPosX = stP;
        this.castZone = castZone;

        pointA = player.transform.position;
        pointA.x = pointA.x + startPosX;
        pointB = new Vector2(pointA.x + 2, pointA.y + 10);
    }

    private void Start()
    {
        startTime = Time.time;
        StartCoroutine(CastSkill());
        castZone.gameObject.transform.position = pointA;
        castZone.SetActive(true);
    }

    private void Update()
    {
        if(startTime + duration < Time.time)
        {
            castZone.SetActive(false);  
            Destroy(gameObject);
        }
    }

    public Collider2D[] GetMonsterInZone()
    {
        return Physics2D.OverlapAreaAll(pointA, pointB, monsterLayer);
    }

    IEnumerator CastSkill()
    {
        while(true)
        {
            var monsters = GetMonsterInZone();
            //Debug.Log($"CastSkill, Hit Monster {monsters.Length}");
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
}

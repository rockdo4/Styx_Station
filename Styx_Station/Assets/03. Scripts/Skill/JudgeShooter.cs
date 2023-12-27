using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JudgeShooter : Shooter
{
    private float timeLimit = 0.4f;
    private int hitCount = 0;
    private List<GameObject> monsters = new List<GameObject>();
    private GameObject caster;
    private float multiple;

    private WaitForSeconds wait;
    public void SetJudgeShooter(int h, float m, GameObject attacer)
    {
        hitCount = h;
        caster = attacer;
        multiple = m;

        wait = new WaitForSeconds(timeLimit);

        StartCoroutine(HitAll());
    }

    IEnumerator HitAll()
    {
        while(hitCount > 0)
        {
            FindMonsters();

            Debug.Log("Hit Monster In Judge");
            foreach (GameObject monster in monsters)
            {
                HitMonster(monster);
            }
            hitCount--;
            yield return wait;
        }

        ReleaseObject();
    }

    private void FindMonsters()
    {
        monsters = GameObject.FindGameObjectsWithTag("Enemy")
            .Where(obj => obj.activeSelf)
            .Where(obj => obj.GetComponent<MonsterStats>().currHealth > 0)
            .ToList();
    }

    private void HitMonster(GameObject defender)
    {
        if (defender == null)
            return;
        if (defender.GetComponent<MonsterStats>().currHealth < 0)
            return;

        var attackerStats = caster.GetComponent<ResultPlayerStats>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target, multiple);

        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(caster, attack);
        }
    }

    public override void ReleaseObject()
    {
        monsters = new List<GameObject>();
        base.ReleaseObject(); 
    }
}

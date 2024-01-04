using System.Collections;
using System.Numerics;
using UnityEngine;

public class TripleShotShooter : Shooter
{
    private GameObject attacker;
    private UnityEngine.Vector2 pos;
    private string piercingBowName;
    private float speed;
    private int fireCount = 3;
    private float fireBet = 0.3f;

    private float damageMultiplier = 0;

    private WaitForSeconds wait;
    private void Start()
    {
        wait = new WaitForSeconds(fireBet);
    }

    public void SetShooter(GameObject a, UnityEngine.Vector2 b, string s, float sp, float mul)
    {
        attacker = a;
        pos = b;
        piercingBowName = s;
        speed = sp;
        damageMultiplier = mul;

        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        while (fireCount > 0)
        {
            var arrow = ObjectPoolManager.instance.GetGo(piercingBowName);
            if (arrow == null)
            {
                Debug.Log("ERR: arrow is null");
                yield return null;
            }
            arrow.transform.position = pos;

            var piercingArrow = arrow.GetComponent<PiercingArrow>();
            if (!piercingArrow.CheckOnCollided())
            {
                piercingArrow.OnCollided += OnBowCollided;
            }

            piercingArrow.Fire(attacker, speed);

            fireCount--;

            yield return wait;
        }
        ReleaseObject();
    }

    public void OnBowCollided(GameObject attacker, GameObject defender)
    {
        if (defender == null)
        {
            return;
        }
        var attackerStats = attacker.GetComponent<ResultPlayerStats>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target, damageMultiplier);

        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }

    public override void ReleaseObject()
    {
        fireCount = 3;
        base.ReleaseObject();
    }
}

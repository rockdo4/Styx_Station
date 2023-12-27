using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class impaleShooter : Shooter
{
    private int hitCount = 0;
    private float impaleXPosBet = 1.5f;
    private float multiple;
    private GameObject caster;
    private GameObject impalePrefab;
    private GameObject prevImpale;
    private WaitForSeconds waitSecond = new WaitForSeconds(0.4f);
    private bool isInitialSet = false;
    
    public void SetImpaleShooter(int h, float m, GameObject attacer, GameObject prefab)
    {
        hitCount = h;
        multiple = m;
        caster = attacer;
        impalePrefab = prefab;

        //isInitialSet = true;

        StartCoroutine(FireImpale());
    }

    //private void OnEnable()
    //{
    //    if(isInitialSet)
    //    {
    //        StartCoroutine(FireImpale());
    //    }
    //}
    IEnumerator FireImpale()
    {
        for (int i = 0; i < hitCount; i++)
        {
            var impale = ObjectPoolManager.instance.GetGo(impalePrefab.name);
            if (impale == null)
            {
                Debug.Log("ERR: impale is null");
            }
            var impaleShot = impale.GetComponent<ImplaeShot>();
            if (i == 0)
            {
                prevImpale = impale;
                impaleShot.SetIsPoisitionSet(true);
            }
            else
            {
                var newPos = prevImpale.transform.position;
                newPos.x += impaleXPosBet;
                
                impaleShot.SetImpaleShot(newPos, caster);

                if (!impaleShot.CheckOnCollided())
                {
                    impaleShot.OnCollided += OnBowCollided;
                }

                prevImpale = impale;
            }
            yield return waitSecond;
        }
        //isInitialSet = false;
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
        Attack attack = CreateAttackToMonster(attackerStats, target, multiple);

        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}

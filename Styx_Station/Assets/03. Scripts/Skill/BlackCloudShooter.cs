using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackCloudShooter : Shooter
{
    private float timer = 0f;
    private float timeLimit = 0.5f;
    private int hitCount = 0;
    private GameObject[] monsters = new GameObject[5];
    private GameObject caster;
    private float multiple;
    private GameObject particle;

    public void SetBlackCloudShot(int h, float m, GameObject attacer, GameObject p)
    {
        hitCount = h;
        caster = attacer;
        multiple = m;
        particle = p;
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeLimit)
        {
            timer = 0f;
            FindMonsters();

            foreach(GameObject monster in monsters)
            {
                HitMonster(monster);
            }
            ReleaseObject();
            //ReleaseCloud();
        }
    }

    private void FindMonsters()
    {
        GameObject[] monstersTemp = GameObject.FindGameObjectsWithTag("Enemy")
            .Where(obj => obj.activeSelf)
            .Where(obj => obj.GetComponent<MonsterStats>().currHealth > 0)
            .ToArray();
        

        if(monstersTemp.Length <= hitCount )
        {
            for(int i = 0; i < monstersTemp.Length; i++)
            {
                monsters[i] = monstersTemp[i];
            }
            //monsters = monstersTemp;
        }
        else
        {
            List<int> selNum = GetRandomNumbers(0, monsters.Length, hitCount);
            for(int i = 0; i < selNum.Count; i++)
            {
                if (monstersTemp[selNum[i]].GetComponent<MonsterStats>() != null )
                {
                    monsters[i] = monstersTemp[selNum[i]];
                }
            }
        }
    }

    private List<int> GetRandomNumbers(int min, int max, int count)
    {
        List<int> numbers = new List<int>();
        while (numbers.Count < count)
        {
            int randomNumber = Random.Range(min, max);
            if (!numbers.Contains(randomNumber))
            {
                numbers.Add(randomNumber);
            }
        }
        return numbers;
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

        for(int i = 0; i < defender.transform.childCount; i++)
        {
            if(defender.transform.GetChild(i).name == particle.name)
            {
                defender.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public override void ReleaseObject()
    {
        monsters = new GameObject[5];
        base.ReleaseObject();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public List<MonsterTypeBase> MonsterTypes;
    public int spawnTimeBat;

    private void Start()
    {
        StartCoroutine(spawnMonster());
    }

    IEnumerator spawnMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimeBat);
            GameObject monster = ObjectPoolManager.instance.GetGo(MonsterTypes[0].name);

            monster.GetComponent<MonsterStats>().SetStats(
                MonsterTypes[0].maxHealth, MonsterTypes[0].damage, MonsterTypes[0].attackType, MonsterTypes[0].speed);
        }
        //yield return new WaitForSeconds(3f);
        //GameObject monster = ObjectPoolManager.instance.GetGo(MonsterTypes[0].name);

        //monster.GetComponent<MonsterStats>().SetStats(
        //    MonsterTypes[0].maxHealth, MonsterTypes[0].damage, MonsterTypes[0].attackType, MonsterTypes[0].speed);

    }
}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class MonsterSpawner : MonoBehaviour
{
    public float timer = 0f;
    public int spawnTimeBat;
    public WaitForSeconds WaitSecond;
    
    public List<MonsterTypeBase> MonsterTypes;

    public float spawnYPosSpacing;    //스폰 y좌표 간격
    public int spawnYPosCount;   //스폰 y좌표 갯수
    
    public int spawnMonstercount = 1;
    public GameObject spawnPoint;

    public int monster1Index;
    public int monster2Index;
    public int monster1Count;
    public int monster2Count;

    public int maxMonsterTypeCount = 2;

    public int increaseAttack;
    public int increaseHealth;

    private Coroutine spawnCo;

    public Transform idlePoint;

    private void Start()
    {
        timer = 0;
        WaitSecond = new WaitForSeconds(spawnTimeBat);
    }

    //private void Update()
    //{
    //    if (timer + spawnTimeBat <= Time.time)
    //    {
    //        int randNum = Random.Range(0, MonsterTypes.Count);
    //        timer = Time.time;
    //        GameObject monster = ObjectPoolManager.instance.GetGo(MonsterTypes[randNum].name);
    //        monster.transform.position = spawnPoint.transform.position;

    //        monster.GetComponent<MonsterStats>().SetStats(
    //            MonsterTypes[randNum].maxHealth, MonsterTypes[randNum].damage, MonsterTypes[randNum].attackType, MonsterTypes[randNum].speed);
    //        monster.GetComponent<MonsterController>().weapon = MonsterTypes[randNum].weapon;

    //        monster.GetComponent<MonsterController>().SetExcuteHit();
    //        monster.GetComponent<MonsterController>().SetSpawnPosition(spawnYPosCount, spawnYPosSpacing);
    //    }
    //}

    public void SpawnMonster(string m1Name, int m1Count, string m2Name, int m2Count, int AIncrease, int HIncrease, int SIncrease)
    {
        increaseAttack = AIncrease;
        increaseHealth = HIncrease;

        bool isFindM1 = false;
        bool isFindM2 = false;
        for(int i = 0; i < MonsterTypes.Count; i++)
        {
            if (MonsterTypes[i].name == m1Name && !isFindM1)
            {
                monster1Index = i;
                isFindM1 = true;
            }
            else if(MonsterTypes[i].name == m2Name && !isFindM2)
            {
                monster2Index = i;
                isFindM2 = true;
            }
        }
        if(!isFindM1 || !isFindM2)
        {
            Debug.Log("ERR: Wrong MonsterName. m1 = 0, m2 = 1 로 대신 생성합니다.");
            monster1Index = 0;
            monster2Index = 1;
        }
        monster1Count = m1Count;
        monster2Count = m2Count;

        spawnCo = StartCoroutine(SpawnMonsterCo(monster1Count + monster2Count));
    }
    IEnumerator SpawnMonsterCo(int count)
    {
        int spawnedCount = 0;
        while (spawnedCount < count)
        {
            yield return WaitSecond;
            Debug.Log("SpawnMonster");
            int monsterTypeIndex = -1;
            int randNum;
            if(monster1Count <= 0 || monster2Count <=0)
            {
                if(monster2Count <= 0)
                {
                    monsterTypeIndex = monster1Index;
                }
                else
                {
                    monsterTypeIndex = monster2Index;
                }
            }
            else
            {
                randNum = Random.Range(0, maxMonsterTypeCount);
                switch (randNum)
                {
                    case 0:
                        monsterTypeIndex = monster1Index;
                        monster1Count--;
                        break;

                    case 1:
                        monsterTypeIndex = monster2Index;
                        monster2Count--;
                        break;
                }
            }
            if(monsterTypeIndex < 0 )
            {
                Debug.Log("ERR: wrong MonsterTypeIndex");
            }
            GameObject monster = ObjectPoolManager.instance.GetGo(MonsterTypes[monsterTypeIndex].name);
            monster.transform.position = spawnPoint.transform.position;

            string healths = (BigInteger.Parse(MonsterTypes[monsterTypeIndex].maxHealth) + increaseHealth).ToString();
            string attacks = (BigInteger.Parse(MonsterTypes[monsterTypeIndex].damage) + increaseAttack).ToString();
            monster.GetComponent<MonsterStats>().SetStats(
                healths,
                attacks,
                MonsterTypes[monsterTypeIndex].attackType,
                MonsterTypes[monsterTypeIndex].speed);
            var monsterController = monster.GetComponent<MonsterController>();
            monsterController.weapon = MonsterTypes[monsterTypeIndex].weapon;
            monster.GetComponent<Collider2D>().enabled = true;

            monsterController.SetExcuteHit();
            monsterController.SetSpawnPosition(spawnYPosCount, spawnYPosSpacing);
            monsterController.SetIdlePoint(idlePoint);
            monsterController.isTargetDie = false;
            spawnedCount++;
        }
    }

    public void stopSpawn()
    {
        StopCoroutine(spawnCo);
    }
}

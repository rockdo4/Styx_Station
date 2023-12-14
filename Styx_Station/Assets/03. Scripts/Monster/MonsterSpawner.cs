using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Numerics;
using System.Xml.Linq;
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

    private int[] monsterIndex = new int[4]
    {
        -1, -1, -1, -1
    };
    //public int monster1Index;
    //public int monster2Index;
    //public int monster3Index;
    //public int monster4Index;

    private int[] monsterCount = new int[4]
    {
        -1, -1, -1, -1
    };
    //public int monster1Count;
    //public int monster2Count;
    //public int monster3Count;
    //public int monster4Count;

    public int maxMonsterTypeCount = 4;

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

    private int FindMonsterIndex(string name)
    {
        for (int i = 0; i < MonsterTypes.Count; i++)
        {
            if (MonsterTypes[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }
    public void SpawnMonster(
        string m1Name, int m1Count, 
        int AIncrease, int HIncrease, int SIncrease)
    {
        increaseAttack = AIncrease;
        increaseHealth = HIncrease;

        monsterCount[0] = m1Count;
        monsterCount[1] = -1;
        monsterCount[2] = -1;
        monsterCount[3] = -1;

        monsterIndex[0] = FindMonsterIndex(m1Name);

        spawnCo = StartCoroutine(SpawnMonsterCo(monsterCount[0]));
    }

    public void SpawnMonster(
        string m1Name, int m1Count, 
        string m2Name, int m2Count, 
        int AIncrease, int HIncrease, float SIncrease)
    {
        increaseAttack = AIncrease;
        increaseHealth = HIncrease;

        bool isFindM1 = false;
        bool isFindM2 = false;
        for(int i = 0; i < MonsterTypes.Count; i++)
        {
            if (MonsterTypes[i].name == m1Name && !isFindM1)
            {
                monsterCount[0] = i;
                isFindM1 = true;
            }
            else if(MonsterTypes[i].name == m2Name && !isFindM2)
            {
                monsterCount[1] = i;
                isFindM2 = true;
            }
        }
        if(!isFindM1 || !isFindM2)
        {
            Debug.Log("ERR: Wrong MonsterName. m1 = 0, m2 = 1 로 대신 생성합니다.");
            monsterIndex[0] = 0;
            monsterIndex[1] = 1;
        }
        monsterCount[0] = m1Count;
        monsterCount[1] = m2Count;

        spawnCo = StartCoroutine(SpawnMonsterCo(monsterCount[0] + monsterCount[1]));
    }

    public void SpawnMonster(string m1Name, int m1Count, 
        string m2Name, int m2Count, 
        string m3Name, int m3Count, 
        int AIncrease, int HIncrease, int SIncrease)
    {

    }

    public void SpawnMonster(string m1Name, int m1Count,
        string m2Name, int m2Count,
        string m3Name, int m3Count,
        string m4Name, int m4Count,
        int AIncrease, int HIncrease, int SIncrease)
    {

    }
    IEnumerator SpawnMonsterCo(int count)
    {
        int spawnedCount = 0;
        while (spawnedCount < count)
        {
            yield return WaitSecond;
            //Debug.Log("SpawnMonster");
            int monsterTypeIndex = -1;
            int randNum;
            if(monsterCount[0] <= 0 || monsterCount[1] <= 0 || monsterCount[2] <= 0 || monsterCount[3] <= 0 )
            {
                if(monsterCount[1] <= 0)
                {
                    monsterTypeIndex = monsterIndex[0];
                }
                else
                {
                    monsterTypeIndex = monsterIndex[1];
                }
            }
            else
            {
                randNum = Random.Range(0, maxMonsterTypeCount);
                switch (randNum)
                {
                    case 0:
                        monsterTypeIndex = monsterIndex[0];
                        monsterCount[0]--;
                        break;

                    case 1:
                        monsterTypeIndex = monsterIndex[1];
                        monsterCount[1]--;
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

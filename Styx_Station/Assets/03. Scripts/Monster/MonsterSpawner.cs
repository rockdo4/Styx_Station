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
    
    public MonsterTable monsterTable;
    //private List<MonsterTypeBase> MonsterTypes;

    public float spawnYPosSpacing;    //스폰 y좌표 간격
    public int spawnYPosCount;   //스폰 y좌표 갯수
    
    public int spawnMonstercount = 1;
    public GameObject spawnPoint;

    public int bossSize = 2;
    public float tankSize = 1.5f;
    public float spawnSize = 1f;

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
        0, 0 , 0, 0
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

    private int FindMonsterIndex(string name)
    {
        for (int i = 0; i < monsterTable.GetTableSize(); i++)
        {
            if (monsterTable.GetMonster(i).name == name)
            {
                return i;
            }
        }
        return -1;
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
        for(int i = 0; i < monsterTable.GetTableSize(); i++)
        {
            if (monsterTable.GetMonster(i).name == m1Name && !isFindM1)
            {
                monsterCount[0] = i;
                isFindM1 = true;
            }
            else if(monsterTable.GetMonster(i).name == m2Name && !isFindM2)
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

    public void spawnBoss(string bName)
    {
        increaseAttack = 0;
        increaseHealth = 0;

        monsterCount[0] = 1;
        monsterCount[1] = 0;
        monsterCount[2] = 0;
        monsterCount[3] = 0;

        monsterIndex[0] = FindMonsterIndex(bName);

        spawnCo = StartCoroutine(SpawnMonsterCo(monsterCount[0]));

        spawnSize = bossSize;
    }
    public void SpawnMonster(string m1Name, int m1Count,
        string m2Name, int m2Count,
        string m3Name, int m3Count,
        string m4Name, int m4Count,
        int AIncrease, int HIncrease, float SIncrease)
    {
        increaseAttack = AIncrease;
        increaseHealth = HIncrease;

        monsterCount[0] = m1Count;
        monsterCount[1] = m2Count;
        monsterCount[2] = m3Count;
        monsterCount[3] = m4Count;

        monsterIndex[0] = FindMonsterIndex(m1Name);
        monsterIndex[1] = FindMonsterIndex(m2Name);
        monsterIndex[2] = FindMonsterIndex(m3Name);
        monsterIndex[3] = FindMonsterIndex(m4Name);

        spawnCo = StartCoroutine(SpawnMonsterCo(monsterCount[0] + monsterCount[1] + monsterCount[2] + monsterCount[3]));
    }
    IEnumerator SpawnMonsterCo(int count)
    {
        int spawnedCount = 0;
        while (spawnedCount < count)
        {
            yield return WaitSecond;

            int monsterTypeIndex = -1;
            //int availableMonsterTypes = 0;
            //for (int i = 0; i < maxMonsterTypeCount; i++)
            //{
            //    if (monsterCount[i] > 0)
            //    {
            //        availableMonsterTypes++;
            //    }
            //}
            //if(availableMonsterTypes > 0)
            //{
            //    int randNum = Random.Range(0, availableMonsterTypes);
            //    int countDown = randNum;

            //    for(int i = 0; i< maxMonsterTypeCount; i++)
            //    {
            //        if (monsterCount[i] > 0)
            //        {
            //            if(countDown == 0)
            //            {
            //                monsterTypeIndex = monsterIndex[i];
            //                monsterCount[i]--;
            //                break;
            //            }
            //        }
            //        countDown--;
            //    }
            //}

            List<int> availableMonsterIndexes = new List<int>();
            for(int i =0; i < maxMonsterTypeCount; i++)
            {
                if (monsterCount[i] > 0)
                {
                    availableMonsterIndexes.Add(i);
                }
            }
            if(availableMonsterIndexes.Count == 0)
            {
                Debug.Log("ERR: No available monster types");
                yield break;
            }

            int randIndex = Random.Range(0, availableMonsterIndexes.Count);
            int selectIndex = availableMonsterIndexes[randIndex];

            monsterTypeIndex = monsterIndex[selectIndex];
            monsterCount[selectIndex]--;

            //if (monsterTypeIndex < 0)
            //{
            //    Debug.Log("ERR: wrong MonsterTypeIndex");
            //    yield break;
            //}
            GameObject monster = ObjectPoolManager.instance.GetGo(monsterTable.GetMonster(monsterTypeIndex).name);
            if(monster == null)
            {
                Debug.Log("ERR: 오브젝트 풀에서 받아오기 실패");
                yield break;
            }
            monster.transform.position = spawnPoint.transform.position;

            string healths = (BigInteger.Parse(monsterTable.GetMonster(monsterTypeIndex).maxHealth) + increaseHealth).ToString();
            string attacks = (BigInteger.Parse(monsterTable.GetMonster(monsterTypeIndex).damage) + increaseAttack).ToString();
            monster.GetComponent<MonsterStats>().SetStats(
                healths,
                attacks,
                monsterTable.GetMonster(monsterTypeIndex).attackType,
                monsterTable.GetMonster(monsterTypeIndex).speed);
            var monsterController = monster.GetComponent<MonsterController>();
            monsterController.weapon = monsterTable.GetMonster(monsterTypeIndex).weapon;
            monster.GetComponent<Collider2D>().enabled = true;

            monsterController.SetExcuteHit();
            monsterController.SetSpawnPosition(spawnYPosCount, spawnYPosSpacing);
            monsterController.SetIdlePoint(idlePoint);
            monsterController.isTargetDie = false;
            monsterController.transform.localScale = new UnityEngine.Vector3(spawnSize, spawnSize, 1);
            //monsterController.SetMoney(coinAmount[selectIndex], pomegranateAmount[selectIndex]);
            monsterController.SetMoney(monsterTable.GetMonster(monsterTypeIndex).monster_coin, monsterTable.GetMonster(monsterTypeIndex).monster_pommegrande);
            spawnSize = 1f;
            spawnedCount++;
        }
    }

    public void stopSpawn()
    {
        StopCoroutine(spawnCo);
    }
}

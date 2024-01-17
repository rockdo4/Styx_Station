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
    public float spawnTimeBat;
    public WaitForSeconds WaitSecond;
    
    public MonsterTable monsterTable;

    public float spawnYPosSpacing;    //스폰 y좌표 간격
    public int spawnYPosCount;   //스폰 y좌표 갯수
    
    public int spawnMonstercount = 1;
    public GameObject spawnPoint;

    public int bossSize = 2;
    public float tankSize = 1.5f;
    public float spawnSize = 1f;

    public float idleTimeBet = 0.1f;
    public float idleTimeMax = 5f;

    private int[] monsterIndex = new int[4]
    {
        -1, -1, -1, -1
    };

    private int[] monsterCount = new int[4]
    {
        0, 0 , 0, 0
    };


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

    public void spawnBoss(string bName)
    {
        increaseAttack = 0;
        increaseHealth = 0;

        monsterCount[0] = 1;

        monsterIndex[0] = FindMonsterIndex(bName);

        List<int> availableMonIndexes = new List<int>();
        availableMonIndexes.Add(monsterIndex[0]);

        spawnCo = StartCoroutine(SpawnMonsterCo(monsterCount[0], availableMonIndexes));

        spawnSize = bossSize;
    }

    public void SpawnMonster(List<MonsterTypeBase> monsterTypes, List<int> monsterCounts, Stage stage)
    {
        increaseAttack = stage.monsterAttackIncrease;
        increaseHealth = stage.monsterHealthIncrease;

        for(int i =0; i < monsterTypes.Count; i++)
        {
            monsterIndex[i] = FindMonsterIndex(monsterTypes[i].name);
        }

        List<int> availableMonIndexes = new List<int>();

        for (int i = 0; i < monsterCounts.Count; i++)
        {
            for (int j = 0; j < monsterCounts[i]; j++)
            {
                availableMonIndexes.Add(monsterIndex[i]);
            }
        }
        int count = 0;
        for (int i = 0;i < monsterCounts.Count; i++) { count += monsterCounts[i]; }

        Shuffle(availableMonIndexes);
        spawnCo = StartCoroutine(SpawnMonsterCo(count, availableMonIndexes));
    }

    //public void SpawnMonster(string m1Name, int m1Count,
    //    string m2Name, int m2Count,
    //    string m3Name, int m3Count,
    //    string m4Name, int m4Count,
    //    int AIncrease, int HIncrease, float SIncrease)
    //{
    //    increaseAttack = AIncrease;
    //    increaseHealth = HIncrease;

    //    monsterCount[0] = m1Count;
    //    monsterCount[1] = m2Count;
    //    monsterCount[2] = m3Count;
    //    monsterCount[3] = m4Count;

    //    monsterIndex[0] = FindMonsterIndex(m1Name);
    //    monsterIndex[1] = FindMonsterIndex(m2Name);
    //    monsterIndex[2] = FindMonsterIndex(m3Name);
    //    monsterIndex[3] = FindMonsterIndex(m4Name);

    //    List<int> availableMonIndexes = new List<int>();

    //    for(int i = 0; i < monsterCount.Length; i++)
    //    {
    //        for(int j = 0; j < monsterCount[i]; j++)
    //        {
    //            availableMonIndexes.Add(monsterIndex[i]);
    //        }
    //    }

    //    Shuffle(availableMonIndexes);
    //    spawnCo = StartCoroutine(SpawnMonsterCo(monsterCount[0] + monsterCount[1] + monsterCount[2] + monsterCount[3], availableMonIndexes));
    //}

    IEnumerator SpawnMonsterCo(int count, List<int> monsterIndexList)
    {
        int spawnedCount = 0;

        while (spawnedCount < count)
        {
            yield return WaitSecond;

            int monsterTypeIndex = -1;
            monsterTypeIndex = monsterIndexList[spawnedCount];

            GameObject monster = ObjectPoolManager.instance.GetGo(monsterTable.GetMonster(monsterTypeIndex).name);
            if (monster == null)
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
            monsterController.SetMoney(monsterTable.GetMonster(monsterTypeIndex).monster_coin, monsterTable.GetMonster(monsterTypeIndex).monster_pommegrande);
            monsterController.startDelay = Random.Range(0, idleTimeMax) * idleTimeBet;
            monsterController.range = monsterTable.GetMonster(monsterTypeIndex).monster_range;
            spawnSize = 1f;
            spawnedCount++;
        }
    }
    public void stopSpawn()
    {
        StopCoroutine(spawnCo);
    }

    public void Shuffle(List<int> list)
    {
        int n = list.Count;
        System.Random rng = new System.Random();

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }


}

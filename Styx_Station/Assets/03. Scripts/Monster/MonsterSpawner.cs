using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class MonsterSpawner : MonoBehaviour
{
    public float timer = 0f;
    public List<MonsterTypeBase> MonsterTypes;
    public int spawnTimeBat;
    public float spawnYPosSpacing;    //½ºÆù yÁÂÇ¥ °£°Ý
    public int spawnYPosCount;   //½ºÆù yÁÂÇ¥ °¹¼ö
    public WaitForSeconds WaitSecond;
    public int spawnMonstercount = 1;

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if (timer + spawnTimeBat <= Time.time)
        {
            int randNum = Random.Range(0, MonsterTypes.Count);
            timer = Time.time;
            GameObject monster = ObjectPoolManager.instance.GetGo(MonsterTypes[randNum].name);

            monster.GetComponent<MonsterStats>().SetStats(
                MonsterTypes[randNum].maxHealth, MonsterTypes[randNum].damage, MonsterTypes[randNum].attackType, MonsterTypes[randNum].speed);
            monster.GetComponent<MonsterController>().weapon = MonsterTypes[randNum].weapon;

            monster.GetComponent<MonsterController>().SetExcuteHit();
            monster.GetComponent<MonsterController>().SetSpawnPosition(spawnYPosCount, spawnYPosSpacing);
        }
    }
    IEnumerator SpawnMonster()
    {
        while (true)
        {
            yield return WaitSecond;
            GameObject monster = ObjectPoolManager.instance.GetGo(MonsterTypes[0].name);

            monster.GetComponent<MonsterStats>().SetStats(
                MonsterTypes[0].maxHealth, MonsterTypes[0].damage, MonsterTypes[0].attackType, MonsterTypes[0].speed);
            monster.GetComponent<MonsterController>().weapon = MonsterTypes[0].weapon;

            monster.GetComponent<MonsterController>().SetExcuteHit();
            monster.GetComponent<MonsterController>().SetSpawnPosition(spawnYPosCount, spawnYPosSpacing);

            //int randNum = Random.Range(0, spawnYPosCount);
            //float newYPos =  monster.transform.position.y - randNum * spawnYPosSpacing;
            //monster.transform.position = new Vector3(monster.transform.position.x, newYPos, monster.transform.position.z);
            //monster.GetComponentInChildren<SortingGroup>().sortingOrder += randNum;

            //Debug.Log($"randnum: {randNum}, ypos: {newYPos}");
        }
        //yield return new WaitForSeconds(3f);
        //GameObject monster = ObjectPoolManager.instance.GetGo(MonsterTypes[0].name);

        //monster.GetComponent<MonsterStats>().SetStats(
        //    MonsterTypes[0].maxHealth, MonsterTypes[0].damage, MonsterTypes[0].attackType, MonsterTypes[0].speed);
        //monster.GetComponent<MonsterController>().weapon = MonsterTypes[0].weapon;

        //monster.GetComponent<MonsterController>().GetComponentInChildren<ExecuteHit>().weapon = MonsterTypes[0].weapon;
        //monster.GetComponent<MonsterController>().GetComponentInChildren<ExecuteHit>().target = monster.GetComponent<MonsterController>().target;
    }
}

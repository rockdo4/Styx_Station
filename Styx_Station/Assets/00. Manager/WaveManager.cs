using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<WaveManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static WaveManager m_instance; // 싱글톤이 할당될 static 변수

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }
    public int CurrentStage { get; private set; }  //현재 스테이지
    public int CurrentWave { get; private set; } //현재 웨이브
    public int CurrentChpater { get; private set; } //현재 챕터
    public MonsterSpawner spawner;
    private int spawnMonsterCount = 10; //생성할 몬스터 수

    public StageTable.StageTableData StageData { get; private set; }

    private void Start()
    {
        CurrentStage = 1;
        CurrentWave = 1;
        CurrentChpater = 1;

        var index = GameManager.instance.StageTable.GetIndex(CurrentChpater, CurrentStage, CurrentWave);

        StageData = GameManager.instance.StageTable.GetStageTableData(index);
    }

    public void StartWave()
    {
        spawner.SpawnMonster(spawnMonsterCount);
    }

    public void EndWave()
    {

    }

    public void UpdateCurrentChapter()
    {
        CurrentChpater++;
    }
    public void UpdateCurrentStage()
    {
        CurrentStage++;
    }
    public void UpdateCurrentWave()
    {
        CurrentWave++;
        if(CurrentWave > 5)
        {
            CurrentWave = 1;
        }
    }
}

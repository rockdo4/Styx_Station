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

        //추후 세이브한 것으로 변경해야함. 일단 무조건 시작시 1번 스테이지로 초기화
        currStage = stageList.GetStage(0);

        CurrentStage = currStage.stageId;
        CurrentWave = currStage.waveId;
        CurrentChpater = currStage.chapterId;
    }
    public int CurrentStage { get; private set; }  //현재 스테이지
    public int CurrentWave { get; private set; } //현재 웨이브
    public int CurrentChpater { get; private set; } //현재 챕터
    public int maxWaveLevel = 5; //최대 웨이브 레벨
    public int maxStageLevel = 10; //최대 스테이지 레벨

    public MonsterSpawner spawner;
    public int aliveMonsterCount;
    public StageList stageList;
    public Stage currStage;

    //public StageTable.StageTableData StageData { get; private set; }

    private void Start()
    {
        //var index = GameManager.instance.StageTable.GetIndex(CurrentChpater, CurrentStage, CurrentWave);

        //StageData = GameManager.instance.StageTable.GetStageTableData(index);
    }

    public void StartWave()
    {
        spawner.SpawnMonster(currStage.monster1.name, currStage.monster1Count, currStage.monster2.name, currStage.monster2Count);
        aliveMonsterCount = currStage.monster1Count + currStage.monster2Count;
    }

    public void EndWave()
    {
        aliveMonsterCount = 0;
    }

    public void ChangeWage()
    {
        EndWave();
        UpdateCurrentWave();

        currStage = stageList.GetStageByStageIndex(GetIndex(CurrentChpater, CurrentStage, CurrentWave));
        if(currStage == null)
        {
            Debug.Log("ERR: currStage is null.");
            return;
        }

        StartWave();
    }

    public void UpdateCurrentChapter()
    {
        CurrentChpater++;
    }
    public void UpdateCurrentStage()
    {
        CurrentStage++;
        if(CurrentStage > 10)
        {
            CurrentStage = 1;
            UpdateCurrentChapter();
        }
    }
    public void UpdateCurrentWave()
    {
        CurrentWave++;
        //if(CurrentWave > 5)
        //{
        //    CurrentWave = 1;
        //    UpdateCurrentStage();
        //}
        if(CurrentWave > 4) //임시, 4번째 웨이브 계속 반복하도록
        {
            CurrentWave = 4;
        }
        
    }

    public int GetIndex(int chapterId, int stageId, int waveId)
    {
        return 100000000 + (chapterId * 10000) + ((stageId - 1) * 5) + waveId;
    }

    public void DecreaseAliveMonsterCount()
    {
        aliveMonsterCount--;
        if(aliveMonsterCount <= 0)
        {
            ChangeWage();
        }
    }
}

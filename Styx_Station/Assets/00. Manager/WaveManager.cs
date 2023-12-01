using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

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

        for (int i = 0; i < Background.transform.childCount; i++)
        {
            backgroundList.Add(Background.transform.GetChild(i).GetComponent<ScrollingObject>());
        }

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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

    public GameObject Background;
    private List<ScrollingObject> backgroundList = new List<ScrollingObject>();

    private PlayerController playerController;

    public float waitTime = 1.5f;
    private WaitForSeconds waitForSeconds;

    public TextMeshProUGUI stageText; //임시로 여기서 함. 추후 uimanager로 이동함.

    //public StageTable.StageTableData StageData { get; private set; }

    private void Start()
    {
        //var index = GameManager.instance.StageTable.GetIndex(CurrentChpater, CurrentStage, CurrentWave);

        //StageData = GameManager.instance.StageTable.GetStageTableData(index);
        waitForSeconds = new WaitForSeconds(waitTime);
    }

    public void StartWave()
    {
        spawner.SpawnMonster(currStage.monster1.name, 
            currStage.monster1Count, 
            currStage.monster2.name, 
            currStage.monster2Count,
            currStage.monsterAttackIncrease,
            currStage.monsterHealthIncrease,
            currStage.monsterAttackSpeedIncrease);
        aliveMonsterCount = currStage.monster1Count + currStage.monster2Count;
    }

    public void EndWave()
    {
        aliveMonsterCount = 0;
        playerController.GetAnimator().StopPlayback();
        spawner.stopSpawn();
        StartCoroutine(SetMonstersStop());
        //StartCoroutine(SetArrowStop());
    }

    public void ChangeWage()
    {
        UpdateCurrentWave();
        currStage = stageList.GetStageByStageIndex(GetIndex(CurrentChpater, CurrentStage, CurrentWave));
        if(currStage == null)
        {
            Debug.Log("ERR: currStage is null.");
            return;
        }
        playerController.SetState(States.Move);
        ScrollBackground(true);
        //StartWave();
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
        SetCurrentStageText();
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

    public void ScrollBackground(bool isBool)
    {
        foreach(var back in backgroundList)
        {
            back.enabled = isBool;
        }
    }

    public void SetCurrentStageText()
    {
        string newTxt = $"{CurrentStage} - {CurrentWave}";
        stageText.SetText(newTxt);  
    }

    IEnumerator SetMonstersStop()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy")
            .Where(monster => monster.activeSelf)
            .ToArray();

        foreach(var monster in monsters)
        {
            if(monster.GetComponent<MonsterStats>().currHealth > 0)
            {
                monster.GetComponent<MonsterController>().SetState(States.Idle);
            }
        }

        yield return waitForSeconds;

        foreach (var monster in monsters)
        {
            if (monster.GetComponent<MonsterStats>().currHealth > 0 && monster.activeSelf)
            {
                monster.GetComponent<MonsterController>().ReleaseObject();
            }
        }
    }

    IEnumerator SetArrowStop()
    {
        GameObject[] Arrows = GameObject.FindGameObjectsWithTag("Arrow")
            .Where(arrow => arrow.activeSelf)
            .ToArray();

        foreach (var arrow in Arrows)
        {
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        yield return waitForSeconds;

        foreach (var arrow in Arrows)
        {
            if (arrow.activeSelf)
            {
                arrow.GetComponent<PoolAble>().ReleaseObject();
            }
        }
    }

    public void IncreaseMoney1()
    {
        CurrencyManager.IncreaseMoney1(currStage.rewardCoins);
    }
}

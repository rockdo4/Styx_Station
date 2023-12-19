using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using System.Threading;

public class WaveManager : Singleton<WaveManager> //MonoBehaviour
{
    private void Awake()
    {
        //시작시 1번 스테이지로 초기화
        currStage = stageList.GetStage(0);

        CurrentStage = currStage.stageId;
        CurrentWave = currStage.waveId;
        CurrentChpater = currStage.chapterId;

        for (int i = 0; i < Background.transform.childCount; i++)
        {
            backgroundList.Add(Background.transform.GetChild(i).GetComponent<ScrollingObject>());
        }

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        int tilemapCount = Background.transform.childCount;
        for(int i = 0;i < tilemapCount; i++)
        {
            tileObjects[i] = Background.transform.GetChild(i).gameObject;
            //tileMaps.Add(Background.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < tileObjects[0].transform.childCount; i++)
        {
            leftTileMaps.Add(tileObjects[0].transform.GetChild(i).gameObject);
            rightTileMaps.Add(tileObjects[1].transform.GetChild(i).gameObject);
        }
    }
    public int CurrentStage { get; private set; }  //현재 스테이지
    public int CurrentWave { get; private set; } //현재 웨이브
    public int CurrentChpater { get; private set; } //현재 챕터
    public int maxWaveLevel = 5; //최대 웨이브 레벨
    public int maxStageLevel = 10; //최대 스테이지 레벨

    private bool IsRepeating = false;
    public bool haveToChangeTile = false;

    public MonsterSpawner spawner;
    public int aliveMonsterCount;

    public StageList stageList;
    public Stage currStage;

    private int currTileMapIndex = 0;
    private int clearWaveCount = 0;
    public GameObject Background;
    private GameObject[] tileObjects = new GameObject[2];
    public List<GameObject> leftTileMaps = new List<GameObject>();
    public List<GameObject> rightTileMaps = new List<GameObject>();
    private List<ScrollingObject> backgroundList = new List<ScrollingObject>();

    private PlayerController playerController;

    private float waitTime = 2f;
    private WaitForSeconds waitForSeconds;

    public float timeLimit = 0f;
    public float timer = 0f;
    public bool isWaveInProgress = false;

    //public StageTable.StageTableData StageData { get; private set; }

    private void Start()
    {
        //var index = GameManager.instance.StageTable.GetIndex(CurrentChpater, CurrentStage, CurrentWave);

        //StageData = GameManager.instance.StageTable.GetStageTableData(index);
        waitForSeconds = new WaitForSeconds(waitTime);
    }

    private void Update()
    {
        if(isWaveInProgress)
        {
            timer += Time.deltaTime;
           // Debug.Log(timer);
            UIManager.Instance.SetTimerSlierValue((timeLimit - timer) / timeLimit);
            var timertext = FormatTime(timeLimit - timer);
            UIManager.Instance.SetTimerText(timertext);

            if(timer >= timeLimit)
            {
                playerController.GetAnimator().SetTrigger("Die");
                playerController.SetState(States.Die);
            }
        }   
    }
    public string FormatTime(float time)
    {
        string timeString = string.Format("{0:F1}", time);
        return timeString;
    }
    public void SetWave()
    {
        CurrentStage = currStage.stageId;
        CurrentWave = currStage.waveId;
        CurrentChpater = currStage.chapterId;

        SetCurrentStageText();
    }

    public void StartWave()
    {
        playerController.GetComponent<ResultPlayerStats>().ResetHp();
        timeLimit = currStage.waveTimer;
        timer = 0f;
        isWaveInProgress = true;

        if (currStage.isBossWave)
        {
            spawner.spawnBoss(currStage.bossMonster.name);
        }
        else
        {
            string monName2;
            string monName3;
            string monName4;

            if (currStage.monster2 == null)
            {
                monName2 = "";
            }
            else
            {
                monName2 = currStage.monster2.name;
            }
            if (currStage.monster3 == null)
            {
                monName3 = "";
            }
            else
            {
                monName3 = currStage.monster3.name;
            }
            if (currStage.monster4 == null)
            {
                monName4 = "";
            }
            else
            {
                monName4 = currStage.monster4.name;
            }
            spawner.SpawnMonster(currStage.monster1.name,
                currStage.monster1Count,
                monName2,
                //currStage.monster2.name,
                currStage.monster2Count,
                monName3,
                //currStage.monster3.name,
                currStage.monster3Count,
                monName4,
                //currStage.monster4.name,
                currStage.monster4Count,
                currStage.monsterAttackIncrease,
                currStage.monsterHealthIncrease,
                currStage.monsterAttackSpeedIncrease);
            aliveMonsterCount = currStage.monster1Count + currStage.monster2Count + currStage.monster3Count + currStage.monster4Count;
        }
    }

    public void EndWave()
    {
        isWaveInProgress = false;
        aliveMonsterCount = 0;
        playerController.GetAnimator().StopPlayback();
        spawner.stopSpawn();
        StopArrows();
        StartCoroutine(SetMonstersStop());
        //StartCoroutine(SetArrowStop());
    }

    public void ChangeWave()
    {
        StopArrows();
        isWaveInProgress = false;

        int stageNum = CurrentStage % 5;
        if (stageNum == 0)
        {
            currTileMapIndex = CurrentChpater;
            haveToChangeTile = true;
            ChangeTileMap();
        }

        if (!IsRepeating)
        {
            UpdateCurrentWave();
        }

        currStage = stageList.GetStageByStageIndex(GetIndex(CurrentChpater, CurrentStage, CurrentWave));
        clearWaveCount++;
        timeLimit = currStage.waveTimer;
        timer = 0f;
        if (currStage == null)
        {
            Debug.Log("ERR: currStage is null.");
            return;
        }
        playerController.SetState(States.Move);
        MoveMonPosition();
        ScrollBackground(true);
        SetWavePanel();
        //StartWave();
    }

    public void MoveMonPosition()
    {
        var mons = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in mons)
        {
            item.gameObject.transform.position = item.GetComponent<MonsterController>().idlePos.position;
        }
    }

    public void ChangeTileMap()
    {
        List<GameObject> changeTileList = new List<GameObject>();
        if(clearWaveCount % 2 == 0) //짝수개 클리어, right 바꾸기
        {
            changeTileList = rightTileMaps;
        }
        else //홀수개 클리어, left 바꾸기
        {
            changeTileList = leftTileMaps;
        }

        changeTileList[currTileMapIndex].SetActive(true);
        if(currTileMapIndex > 0)
        {
            changeTileList[currTileMapIndex - 1].SetActive(false);
        }
    }
    public void SetStageByIndexStage(int stageIndex)
    {
        currStage = stageList.GetStageByStageIndex(stageIndex);
        SetWave();
    }

    public void SetWavePanel()
    {
        for (int i = 0; i < CurrentWave - 1; i++)
        {
            UIManager.Instance.SetWavePanelClear(i, true);
        }
        for (int i = CurrentWave - 1; i < 5; i++)
        {
            UIManager.Instance.SetWavePanelClear(i, false);
        }

        UIManager.Instance.SetWavePanelPlayer(CurrentWave - 1);
    }

    public void SetTileMap()
    {
        leftTileMaps[0].SetActive(false);
        rightTileMaps[0].SetActive(false);

        currTileMapIndex = CurrentChpater;

        leftTileMaps[currTileMapIndex -1].SetActive(true);
        rightTileMaps[currTileMapIndex -1].SetActive(true);
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
        if(CurrentChpater == 2 && CurrentStage == 3 && CurrentWave == 5) //최대 40 스테이지까지 제한
        {
            return;
        }
        CurrentWave++;
        if (CurrentWave > 5)
        {
            CurrentWave = 1;
            UpdateCurrentStage();
        }
        //if(CurrentWave > 4) //임시, 4번째 웨이브 계속 반복하도록
        //{
        //    CurrentWave = 4;
        //}
        SetCurrentStageText();
    }

    public void DecreaseCurrentWave()
    {
        CurrentWave--;
        if (CurrentWave <= 0)
        {
            CurrentWave = 1;
        }
        currStage = stageList.GetStageByStageIndex(GetIndex(CurrentChpater, CurrentStage, CurrentWave));
        timeLimit = currStage.waveTimer;
        timer = 0f;
        SetCurrentStageText();
        SetWavePanel();
    }
    public int GetIndex(int chapterId, int stageId, int waveId)
    {
        return 100000000 + 10000 + ((chapterId - 1) * 25) + ((stageId - 1) * 5) + waveId;
    }

    public int GetCurrentIndex()
    {
        return currStage.index;
    }

    public void DecreaseAliveMonsterCount()
    {
        aliveMonsterCount--;
        if(aliveMonsterCount <= 0)
        {
            ChangeWave();
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
        UIManager.Instance.SetCurrentStageText(CurrentStage, CurrentWave);
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
                //monster.GetComponent<MonsterController>().animator.StopPlayback();
                monster.GetComponent<MonsterController>().SetState(States.Idle);
            }
        }

        yield return waitForSeconds;

        foreach (var monster in monsters)
        {
            if (monster.GetComponent<MonsterStats>().currHealth > 0 && monster.activeSelf)
            {
                monster.gameObject.transform.position = monster.GetComponent<MonsterController>().idlePos.position;
                monster.GetComponent<MonsterController>().ReleaseObject();
            }
        }

        var pets = GameObject.FindGameObjectsWithTag("Pet");
        foreach (var pet in pets)
        {
            var initialPos = pet.GetComponentInChildren<PetController>().initialPos;
            pet.gameObject.transform.position = initialPos;
            pet.GetComponentInChildren<PetController>().SetState(States.Idle);
            //pet.GetComponentInChildren<PetController>().isArrive = false;
        }
    }

    private void StopArrows()
    {
        GameObject[] Arrows = GameObject.FindGameObjectsWithTag("Arrow")
            .Where(arrow => arrow.activeSelf)
            .ToArray();
        foreach(var arrow in Arrows)
        {
            arrow.GetComponent<PoolAble>().ReleaseObject();

            if(arrow.GetComponent<PlayerArrow>() != null )
            {
                arrow.GetComponent<PlayerArrow>().isRelease = true;
            }
            else if (arrow.GetComponent<MonsterArrow>() != null)
            {
                arrow.GetComponent<MonsterArrow>().isReleased = true;
            }
            else if (arrow.GetComponent<PetBow>() != null)
            {
                arrow.GetComponent<PetBow>().isRelease = true;
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
        CurrencyManager.GetSilver(currStage.rewardCoins, 0);
    }

    public void SetRepeat(bool isR) //true: 반복, false: 반복X
    {
        IsRepeating = isR;
        UIManager.Instance.SetActiveRepeatButton(isR);
    }
}

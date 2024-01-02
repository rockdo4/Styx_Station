using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossRushManager : Singleton<BossRushManager>
{
    public List<StageList> stages = new List<StageList>();
    public StageList currStageList = new StageList();
    public Stage currStage = null;

    public int CurrentStage { get; private set; } = 0;

    private void Awake()
    {
        currStageList = stages[UIManager.Instance.bossRushIndex];
        currStage = currStageList.GetStage(CurrentStage);
        CurrentStage = 1;
    }

    public MonsterSpawner spawner;
    public int aliveMonsterCount;

    private int currTileMapIndex = 0;
    public GameObject Background;
    private GameObject[] tileObjects = new GameObject[2];
    private List<GameObject> leftTileMaps = new List<GameObject>();
    private List<GameObject> rightTileMaps = new List<GameObject>();
    private List<ScrollingObject> backgroundList = new List<ScrollingObject>();

    public GameObject village;
    public List<GameObject> leftVillage = new List<GameObject>();
    public List<GameObject> rightVillage = new List<GameObject>();

    private PlayerController playerController;

    private float waitTime = 2f;
    private WaitForSeconds waitForSeconds;

    public float timeLimit = 0f;
    public float timer = 0f;
    public bool isWaveInProgress = false;

    public BackgroundMusic BackgroundMusic;

    private void Start()
    {
        waitForSeconds = new WaitForSeconds(waitTime);
    }

    private void Update()
    {
        if (isWaveInProgress)
        {
            timer += Time.deltaTime;
            UIManager.Instance.SetTimerSlierValue((timeLimit - timer) / timeLimit);
            var timertext = FormatTime(timeLimit - timer);
            UIManager.Instance.SetTimerText(timertext);

            if (timer >= timeLimit)
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
            aliveMonsterCount = 1;
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
    }

    public void ChangeWave()
    {
        StopArrows();
        isWaveInProgress = false;

        UpdateCurrentStage();

        currStage = currStageList.GetStage(CurrentStage);
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
        //SetWavePanel();
    }

    public void MoveMonPosition()
    {
        var mons = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in mons)
        {
            item.gameObject.transform.position = item.GetComponent<MonsterController>().idlePos.position;
        }
    }

    //public void ChangeTileMap()
    //{
    //    List<GameObject> changeTileList = new List<GameObject>();
    //    if (clearWaveCount % 2 == 0) //짝수개 클리어, right 바꾸기
    //    {
    //        changeTileList = rightTileMaps;
    //    }
    //    else //홀수개 클리어, left 바꾸기
    //    {
    //        changeTileList = leftTileMaps;
    //    }

    //    changeTileList[currTileMapIndex].SetActive(true);
    //    if (currTileMapIndex > 0)
    //    {
    //        changeTileList[currTileMapIndex - 1].SetActive(false);
    //    }
    //}

    //public void ChangeVillage()
    //{
    //    List<GameObject> changeVillageList = new List<GameObject>();
    //    if (clearWaveCount % 2 == 0) //짝수개 클리어, right 바꾸기
    //    {
    //        changeVillageList = rightVillage;
    //    }
    //    else //홀수개 클리어, left 바꾸기
    //    {
    //        changeVillageList = leftVillage;
    //    }

    //    changeVillageList[currTileMapIndex].SetActive(true);
    //    if (currTileMapIndex > 0)
    //    {
    //        changeVillageList[currTileMapIndex - 1].SetActive(false);
    //    }
    //}

    //public void ChangeAudio()
    //{
    //    BackgroundMusic.StopAudio();
    //    BackgroundMusic.SetAudioClip(CurrentChapter);
    //}

    //public void SetStageByIndexStage(int stageIndex)
    //{
    //    currStage = stageList.GetStageByStageIndex(stageIndex);
    //    SetWave();
    //}

    //public void SetWavePanel()
    //{
    //    for (int i = 0; i < CurrentWave - 1; i++)
    //    {
    //        UIManager.Instance.SetWavePanelClear(i, true);
    //    }
    //    for (int i = CurrentWave - 1; i < 5; i++)
    //    {
    //        UIManager.Instance.SetWavePanelClear(i, false);
    //    }

    //    UIManager.Instance.SetWavePanelPlayer(CurrentWave - 1);
    //}

    //public void SetTileMap()
    //{
    //    leftTileMaps[0].SetActive(false);
    //    rightTileMaps[0].SetActive(false);

    //    currTileMapIndex = CurrentChapter;

    //    leftTileMaps[currTileMapIndex - 1].SetActive(true);
    //    rightTileMaps[currTileMapIndex - 1].SetActive(true);

    //    SetVillage();
    //    SetAudio();
    //}

    public void SetVillage()
    {
        leftVillage[0].SetActive(false);
        rightVillage[0].SetActive(false);

        leftVillage[currTileMapIndex - 1].SetActive(true);
        rightVillage[currTileMapIndex - 1].SetActive(true);
    }

    public void SetAudio()
    {
        //BackgroundMusic.SetAudioClip(CurrentChapter - 1);
    }

    public void UpdateCurrentStage()
    {
        CurrentStage++;
        if (CurrentStage > 5)
        {
            CurrentStage = 1;
            //보스러시 종료 추가
        }
        UpdateCurrentStage();
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
        if (!isWaveInProgress)
        {
            return;
        }
        aliveMonsterCount--;
        if (aliveMonsterCount <= 0)
        {
            ChangeWave();
        }
    }

    public void ScrollBackground(bool isBool)
    {
        foreach (var back in backgroundList)
        {
            back.enabled = isBool;
        }
    }

    public void SetCurrentStageText()
    {
        UIManager.Instance.SetCurrentBossText(CurrentStage);
    }

    IEnumerator SetMonstersStop()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy")
            .Where(monster => monster.activeSelf)
            .ToArray();

        foreach (var monster in monsters)
        {
            if (monster.GetComponent<MonsterStats>().currHealth > 0)
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

        //var pets = GameObject.FindGameObjectsWithTag("Pet");
        //foreach (var pet in pets)
        //{
        //    var initialPos = pet.GetComponentInChildren<PetController>().initialPos;
        //    pet.gameObject.transform.position = initialPos;
        //    pet.GetComponentInChildren<PetController>().SetState(States.Idle);
        //}
        var pets = PetManager.Instance.GetPetGameobjectArray();
        for (int i = 0; i < pets.Length; i++)
        {
            if (pets[i] != null)
            {
                pets[i].transform.position = PetManager.Instance.petStartTransform[i].position;
                pets[i].GetComponent<PetController>().SetState(States.Idle);
            }
            //pets[i].GetComponent<PetController>().isArrive = false;
        }
    }

    public void ReleaseShooter()
    {
        GameObject[] shooters = GameObject.FindGameObjectsWithTag("Shooter")
           .Where(shooter => shooter.activeSelf)
           .ToArray();

        foreach (var shooter in shooters)
        {
            shooter.GetComponent<PoolAble>().ReleaseObject();
        }
    }

    private void StopArrows()
    {
        ReleaseShooter();
        GameObject[] Arrows = GameObject.FindGameObjectsWithTag("Arrow")
            .Where(arrow => arrow.activeSelf)
            .ToArray();
        foreach (var arrow in Arrows)
        {
            if (arrow.activeSelf)
            {
                arrow.GetComponent<PoolAble>().ReleaseObject();
            }
            //if(arrow.GetComponent<PiercingArrow>() != null)
            //{
            //    arrow.GetComponent<PiercingArrow>().isRelease = true;
            //}
            if (arrow.GetComponent<PlayerArrow>() != null)
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System;

public class WaveManager : Singleton<WaveManager> //MonoBehaviour
{
    private void Awake()
    {
        if (CheckInstance())
            return;
        if (!isLoad)
        {
            PrevLoadSetting();
        }
    }

    private bool isLoad;
    public int CurrentStage { get; private set; }  //현재 스테이지
    public int CurrentWave { get; private set; } //현재 웨이브
    public int CurrentChapter { get; private set; } //현재 챕터
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
    public bool isWaveInPro = false;

    public bool isWaveInProgress = false;

    public BackgroundMusic BackgroundMusic;
    public string GameSceneName;

    //BossRush
    public bool isBossRush;

    public List<StageList> stages = new List<StageList>();
    public StageList currStageList;

    public string clearTime;
    private void Start()
    {
        waitForSeconds = new WaitForSeconds(waitTime);
        if(playerController == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if(player !=null)
            {
                playerController = player.GetComponent<PlayerController>();
                SetAudio();
            }
        }
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
        CurrentChapter = currStage.chapterId;

        SetCurrentStageText();
    }

    public void StartWave()
    {
        if (UIManager.Instance.tutorial.GetComponent<TutorialSystem>().stop == true)
        {
            var tutorial = UIManager.Instance.tutorial.GetComponent<TutorialSystem>();
            tutorial.playTutorial = true;
            tutorial.stop = false;
            tutorial.StartTutorial();
            return;
        }
        playerController.GetComponent<ResultPlayerStats>().ResetHp();
        timeLimit = currStage.waveTimer;
        timer = 0f;
        isWaveInProgress = true;

        if (currStage.isBossWave)
        {
            spawner.spawnBoss(currStage.bossMonster.name);
            aliveMonsterCount = 1;
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
        if(!isBossRush)
        {
            aliveMonsterCount = 0;
            playerController.GetAnimator().StopPlayback();
            spawner.stopSpawn();
            StopArrows();
            StartCoroutine(SetMonstersStop());
            SkillManager.Instance.ResetAllSkillCool();
        }
        else
        {
            UIManager.Instance.SetGameOverPopUpActive(true);
            SkillManager.Instance.ResetAllSkillCool();
            UIManager.Instance.windows[3].GetComponent<CleanWindow>().LoadScene(GameSceneName);
        }
    }

    public void ChangeWave()
    {
        StopArrows();
        isWaveInProgress = false;

        if(isBossRush)
        {
            UpdateCurrentWave();
            currStage = currStageList.GetStage(CurrentWave - 1);
            if (currStage == null)
            {
                Debug.Log("ERR: currStage is null.");
                return;
            }
            clearWaveCount++;
            timeLimit = currStage.waveTimer;
            timer = 0f;

            playerController.SetState(States.Move);
            MoveMonPosition();
            ScrollBackground(true);
            SetWavePanel();
        }
        else
        {
            int stageNum = CurrentStage % 5;
            int waveNum = CurrentWave % 5;
            if (stageNum == 0 && waveNum == 0)
            {
                currTileMapIndex = CurrentChapter;
                haveToChangeTile = true;
                ChangeTileMap();
                ChangeVillage();
            }

            if (!IsRepeating)
            {
                UpdateCurrentWave();
            }

            currStage = stageList.GetStageByStageIndex(GetIndex(CurrentChapter, CurrentStage, CurrentWave));
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
            UIManager.Instance.questSystemUi.ClearWave();
        }
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

    public void ChangeVillage()
    {
        List<GameObject> changeVillageList = new List<GameObject>();
        if (clearWaveCount % 2 == 0) //짝수개 클리어, right 바꾸기
        {
            changeVillageList = rightVillage;
        }
        else //홀수개 클리어, left 바꾸기
        {
            changeVillageList = leftVillage;
        }

        changeVillageList[currTileMapIndex].SetActive(true);
        if (currTileMapIndex > 0)
        {
            changeVillageList[currTileMapIndex - 1].SetActive(false);
        }
    }

    public void ChangeAudio()
    {
        BackgroundMusic.StopAudio();
        BackgroundMusic.SetAudioClip(CurrentChapter);
    }
    public void SetStageByIndexStage(int stageIndex)
    {
        currStage = stageList.GetStageByStageIndex(stageIndex);
        SetWave();
    }

    public void SetWavePanel()
    {
        if(CurrentWave == 0)
        {
            return;
        }
        else
        {
            var panel = UIManager.Instance.WavePanels;

            UIManager.Instance.stageText.gameObject.SetActive(true);
            foreach(var wavePanel in panel)
            {
                wavePanel.SetActive(true);
            }
        }
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

        if(!isBossRush)
        {
            currTileMapIndex = CurrentChapter;

            leftTileMaps[currTileMapIndex - 1].SetActive(true);
            rightTileMaps[currTileMapIndex - 1].SetActive(true);

            SetVillage();
        }

        else
        {
            //currTileMapIndex = 0;
            currTileMapIndex = UIManager.Instance.windows[3].GetComponent<CleanWindow>().bossRushIndex / 30;

            leftTileMaps[currTileMapIndex].SetActive(true);
            rightTileMaps[currTileMapIndex].SetActive(true);
        }
    }

    public void SetVillage()
    {
        if(isBossRush)
        {
            leftVillage[currTileMapIndex].SetActive(true);
            rightVillage[currTileMapIndex].SetActive(true);
        }
        else
        {
            leftVillage[0].SetActive(false);
            rightVillage[0].SetActive(false);

            leftVillage[currTileMapIndex - 1].SetActive(true);
            rightVillage[currTileMapIndex - 1].SetActive(true);
        }
    }

    public void SetAudio()
    {
        BackgroundMusic.SetAudioClip(CurrentChapter - 1);
    }

    public void UpdateCurrentChapter()
    {
        CurrentChapter++;
    }
    public void UpdateCurrentStage()
    {
        CurrentStage++;
        if(CurrentStage > 5)
        {
            CurrentStage = 1;
            UpdateCurrentChapter();
        }
    }
    public void UpdateCurrentWave()
    {
        if (CurrentChapter == 5 && CurrentStage == 5 && CurrentWave == 5) //최대 40 스테이지까지 제한
        {
            //UIManager.Instance.questSystemUi.ClearWave();
            return;
        }
        CurrentWave++;
        if (CurrentWave > 5)
        {
            if(isBossRush)
            {
                var clean = UIManager.Instance.windows[3].GetComponent<CleanWindow>();
                if(clean.bossRushIndex == clean.openStage)
                    clean.openStage++;

                clean.currentCount--;
                clearTime = DateTime.Now.ToString(GameData.datetimeString);
                clean.LoadScene(GameSceneName);
                clean.GetReward();
                return;
            }
            else
            {
                CurrentWave = 1;
                UpdateCurrentStage();
            }
        }
        SetCurrentStageText();
    }

    public void DecreaseCurrentWave()
    {
        if (isBossRush)
            return;

        CurrentWave--;
        if (CurrentWave <= 0)
        {
            CurrentWave = 1;
        }
        currStage = stageList.GetStageByStageIndex(GetIndex(CurrentChapter, CurrentStage, CurrentWave));
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
        if(!isWaveInProgress)
        {
            return;
        }
        aliveMonsterCount--;
        if (aliveMonsterCount <= 0)
        {
            isWaveInProgress = false;
            Invoke("ChangeWave", 1f);
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
        if(isBossRush)
        {
            UIManager.Instance.SetCurrentBossText(CurrentStage, CurrentWave);
        }
        else
        {
            UIManager.Instance.SetCurrentStageText(CurrentChapter, CurrentStage, CurrentWave);
        }
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

        //var pets = GameObject.FindGameObjectsWithTag("Pet");
        //foreach (var pet in pets)
        //{
        //    var initialPos = pet.GetComponentInChildren<PetController>().initialPos;
        //    pet.gameObject.transform.position = initialPos;
        //    pet.GetComponentInChildren<PetController>().SetState(States.Idle);
        //}
        var pets = PetManager.Instance.GetPetGameobjectArray();
        for(int i=0;i<pets.Length;i++)
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

    protected void StopArrows()
    {
        ReleaseShooter();
        GameObject[] Arrows = GameObject.FindGameObjectsWithTag("Arrow")
            .Where(arrow => arrow.activeSelf)
            .ToArray();
        foreach(var arrow in Arrows)
        {
            if(arrow.activeSelf)
            {
                arrow.GetComponent<PoolAble>().ReleaseObject();
            }
            //if(arrow.GetComponent<PiercingArrow>() != null)
            //{
            //    arrow.GetComponent<PiercingArrow>().isRelease = true;
            //}
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

    public bool GetIsRepeat()
    {
        return IsRepeating;
    }

    private void PrevLoadSetting()
    {
        if(!isBossRush)
        {
            SetStageByIndexStage(GameData.stageData_WaveManager);
        }
        else
        {
            SetBossRushStage(UIManager.Instance.windows[3].GetComponent<CleanWindow>().bossRushIndex);
        }

        for (int i = 0; i < Background.transform.childCount; i++)
        {
            backgroundList.Add(Background.transform.GetChild(i).GetComponent<ScrollingObject>());
        }

        if (playerController == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerController = player.GetComponent<PlayerController>();
            }
        }

        int tilemapCount = Background.transform.childCount;
        for (int i = 0; i < tilemapCount; i++)
        {
            tileObjects[i] = Background.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < tileObjects[0].transform.childCount; i++)
        {
            leftTileMaps.Add(tileObjects[0].transform.GetChild(i).gameObject);
            rightTileMaps.Add(tileObjects[1].transform.GetChild(i).gameObject);
        }

        SetTileMap();
        if(!isBossRush)
        {
            SetRepeat(GameData.isRepeatData_WaveManager);
        }
        SetWavePanel();
        isLoad = true;
    }

    private void SetBossRushStage(int index)
    {
        currStageList = stages[index];
        currStage = currStageList.GetStage(0);
        CurrentChapter = 0;
        CurrentStage = index + 1;
        CurrentWave = 1;

        SetCurrentStageText();
    }
}

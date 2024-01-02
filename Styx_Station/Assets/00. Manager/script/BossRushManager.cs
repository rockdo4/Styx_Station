//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class BossRushManager : WaveManager//Singleton<BossRushManager>
//{
//    public List<StageList> stages = new List<StageList>();
//    public StageList currStageList;
//    public new Stage currStage = null;

//    public new int CurrentStage { get; private set; } = 0;

//    private void Awake()
//    {
//        currStageList = stages[UIManager.Instance.bossRushIndex];
//        currStage = currStageList.GetStage(CurrentStage);
//        CurrentStage = 1;
//    }

//    private PlayerController playerController;

//    private float waitTime = 2f;
//    private WaitForSeconds waitForSeconds;

//    private void Start()
//    {
//        waitForSeconds = new WaitForSeconds(waitTime);
//        if (playerController == null)
//        {
//            var player = GameObject.FindGameObjectWithTag("Player");
//            if (player != null)
//            {
//                playerController = player.GetComponent<PlayerController>();
//            }
//        }

//    }

//    private void Update()
//    {
//        if (isWaveInProgress)
//        {
//            timer += Time.deltaTime;
//            UIManager.Instance.SetTimerSlierValue((timeLimit - timer) / timeLimit);
//            var timertext = FormatTime(timeLimit - timer);
//            UIManager.Instance.SetTimerText(timertext);

//            if (timer >= timeLimit)
//            {
//                playerController.GetAnimator().SetTrigger("Die");
//                playerController.SetState(States.Die);
//            }
//        }
//    }

//    public override void SetWave()
//    {
//        CurrentStage = currStage.stageId;

//        SetCurrentStageText();
//    }

//    public override void StartWave()
//    {
//        playerController.GetComponent<ResultPlayerStats>().ResetHp();
//        timeLimit = currStage.waveTimer;
//        timer = 0f;
//        isWaveInProgress = true;

//        if (currStage.isBossWave)
//        {
//            spawner.spawnBoss(currStage.bossMonster.name);
//            aliveMonsterCount = 1;
//        }
//    }

//    public override void ChangeWave()
//    {
//        base.StopArrows();
//        isWaveInProgress = false;

//        UpdateCurrentStage();

//        currStage = currStageList.GetStage(CurrentStage);
//        timeLimit = currStage.waveTimer;
//        timer = 0f;
//        if (currStage == null)
//        {
//            Debug.Log("ERR: currStage is null.");
//            return;
//        }
//        playerController.SetState(States.Move);
//        MoveMonPosition();
//        ScrollBackground(true);
//        //SetWavePanel();
//    }

//    public override void UpdateCurrentStage()
//    {
//        CurrentStage++;
//        if (CurrentStage > 5)
//        {
//            CurrentStage = 1;
//            //보스러시 종료 추가
//        }
//        UpdateCurrentStage();
//    }

//    public override void SetCurrentStageText()
//    {
//        UIManager.Instance.SetCurrentBossText(CurrentStage);
//    }

//}

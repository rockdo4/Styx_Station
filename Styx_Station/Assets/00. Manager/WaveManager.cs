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
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<WaveManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static WaveManager m_instance; // �̱����� �Ҵ�� static ����

    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        if (instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }

        //���� ���̺��� ������ �����ؾ���. �ϴ� ������ ���۽� 1�� ���������� �ʱ�ȭ
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
    public int CurrentStage { get; private set; }  //���� ��������
    public int CurrentWave { get; private set; } //���� ���̺�
    public int CurrentChpater { get; private set; } //���� é��
    public int maxWaveLevel = 5; //�ִ� ���̺� ����
    public int maxStageLevel = 10; //�ִ� �������� ����

    public MonsterSpawner spawner;
    public int aliveMonsterCount;

    public StageList stageList;
    public Stage currStage;

    public GameObject Background;
    private List<ScrollingObject> backgroundList = new List<ScrollingObject>();

    private PlayerController playerController;

    public float waitTime = 1.5f;
    private WaitForSeconds waitForSeconds;

    public TextMeshProUGUI stageText; //�ӽ÷� ���⼭ ��. ���� uimanager�� �̵���.

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
        if(CurrentWave > 4) //�ӽ�, 4��° ���̺� ��� �ݺ��ϵ���
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

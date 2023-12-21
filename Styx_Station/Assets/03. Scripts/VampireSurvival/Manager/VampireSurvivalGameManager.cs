using System;
using System.Collections.Generic;
using UnityEngine;

public class VampireSurvivalGameManager : MonoBehaviour
{
    private static VampireSurvivalGameManager instance;
    public static VampireSurvivalGameManager Instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                instance = FindObjectOfType<VampireSurvivalGameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return instance;
        }
    }

    public VamprieSurivalPlayerController player;

    [HideInInspector]public bool isPause;
    [HideInInspector] public bool isPlayerLevelup;

    public float gameMaxTimer;
    private float gameTimer;
    public float normalMonsterSpwanTimer;

    public float minCircleSpwan;
    public float maxCircleSpwan;

    public float spwanTime;
    public float spwanTimeDuration;
    [HideInInspector] public float spwanMaxNormalMonster=30;

    [HideInInspector] public bool isGameover;

    public int sliver = 0;//[HideInInspector]

    public int wave = 1;
    public int waveByMonsterBorn;
    public int maxMonsterBorn;

    public VampireSurivalPlayerSkillInventory vampireSkillInventory;
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }

        gameTimer = gameMaxTimer;
    }

    public void Update()
    {
        if (isPause || isGameover)
        {
            return;
        }
        spwanTime += Time.deltaTime;
        normalMonsterSpwanTimer += Time.deltaTime;
        if (spwanTime>=spwanTimeDuration && normalMonsterSpwanTimer<= spwanMaxNormalMonster)
        {
            spwanTime = 0;
            var random = UnityEngine.Random.Range(wave * waveByMonsterBorn, maxMonsterBorn);
            for(int i =0;i< random; ++i)
            {
                var monster = ObjectPoolManager.instance.GetGo("VampireNormalMonster1");
                var minPos = player.transform.position + new Vector3(player.transform.position.x + minCircleSpwan, player.transform.position.y + minCircleSpwan, 0);
                var maxPos = minPos + new Vector3(minPos.x + maxCircleSpwan, minPos.y + maxCircleSpwan, 0);
                var randomPosx = UnityEngine.Random.Range(minPos.x, maxPos.x);
                var randomPosy = UnityEngine.Random.Range(minPos.y, maxPos.y);
                monster.transform.position = new Vector3(randomPosx, randomPosy, 0);
                monster.GetComponent<VampireSurivalMonster>().BornMonster();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var monster =ObjectPoolManager.instance.GetGo("VampireNormalMonster1");
            monster.transform.position = Vector3.zero;
        }
        gameTimer -= Time.deltaTime;
        normalMonsterSpwanTimer += Time.deltaTime;
        if (gameTimer <=0f)
        {
            isPause = true;
        }
    }

    public void LateUpdate()
    {
        VamprieSurvialUiManager.Instance.gameTimerSlider.value = gameTimer / gameMaxTimer;

        TimeSpan timeSpanMax = TimeSpan.FromSeconds(gameTimer);
        var timerStr = timeSpanMax.ToString(@"mm\:ss");
        VamprieSurvialUiManager.Instance.gameTimerTextMeshProUGUI.text = timerStr;
    }
}

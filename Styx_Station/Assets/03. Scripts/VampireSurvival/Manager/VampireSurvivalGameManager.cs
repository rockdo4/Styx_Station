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
    public List<VamprieSurivalPlayerAttackManager> playerAttackType;

    public bool isPause;
    public bool isPlayerLevelup;

    public float gameMaxTimer;
    private float gameTimer;
    public float timer;

    public float minCircleSpwan;
    public float maxCircleSpwan;

    public float spwanTime;
    public float spwanTimeDuration;
    public float spwanMaxNormalMonster;

    [HideInInspector] public bool isGameover;



    private void Awake()
    {
        if (Instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
        //player.playerAttackType.Add(playerAttackType[0]);

        gameTimer = gameMaxTimer;
    }

    public void Update()
    {
        if (isPause || isGameover)
        {
            return;
        }
        spwanTime += Time.deltaTime;
        timer += Time.deltaTime;
        if (spwanTime>=spwanTimeDuration && timer<= spwanMaxNormalMonster)
        {
            spwanTime = 0;
            var monster = ObjectPoolManager.instance.GetGo("VampireNormalMonster1");
            var minPos = player.transform.position + new Vector3(player.transform.position.x + minCircleSpwan, player.transform.position.y + minCircleSpwan, 0);
            var maxPos = minPos + new Vector3(minPos.x + maxCircleSpwan, minPos.y + maxCircleSpwan, 0);

            var randomPosx = UnityEngine.Random.Range(minPos.x, maxPos.x);
            var randomPosy = UnityEngine.Random.Range(minPos.y, maxPos.y);
            monster.transform.position = new Vector3(randomPosx,randomPosy,0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var monster =ObjectPoolManager.instance.GetGo("VampireNormalMonster1");
            monster.transform.position = Vector3.zero;
        }
        gameTimer -= Time.deltaTime;
        timer += Time.deltaTime;
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

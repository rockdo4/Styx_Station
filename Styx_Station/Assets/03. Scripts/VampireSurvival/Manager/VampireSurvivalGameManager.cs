using System;
using System.Collections;
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
    private float timer;


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
        if(isPause)
        {
            return;
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

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
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                instance = FindObjectOfType<VampireSurvivalGameManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
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
            // �ڽ��� �ı�
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatsUIManager : MonoBehaviour
{
    private static PlayerStatsUIManager instance;
    public static PlayerStatsUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerStatsUIManager>();
            }
            return instance;
        }
    }



    public float clickTime = 0.5f;
    public float decreaseClickTime = 0.1f;
    public float minClickTime = 0.1f;

    [HideInInspector] public ResultPlayerStats playerStats;

    public List<PlayerStatsUiData> playerStatsUiDatas = new List<PlayerStatsUiData>();

    private void Awake()
    {
        var find = GameObject.FindWithTag("Player");
        if (find != null)
        {
            var script = find.GetComponent<ResultPlayerStats>();
            if (script != null)
            {
                playerStats = script;
            }
        }
    }
}
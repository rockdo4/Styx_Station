using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatsUIManager : Singleton<PlayerStatsUIManager>
{
    public float clickTime = 0.5f;
    public float decreaseClickTime = 0.1f;
    public float minClickTime = 0.1f;

    public List<PlayerStatsUiData> playerStatsUiDatas= new List<PlayerStatsUiData>();

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Destroy(gameObject);
    }
}
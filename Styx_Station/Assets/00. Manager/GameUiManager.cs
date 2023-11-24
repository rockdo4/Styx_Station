using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : Singleton<GameUiManager> 
{
    public GameObject playerStatsPanel;
    public List<GameObject> PlayerDatas = new List<GameObject>();
    public float clickTime;
    public float nowTime;
    private Dictionary<Func<bool>, Action> playerStatsActions = new Dictionary<Func<bool>, Action>();
    private List<bool> playerStatsUpgrade = new List<bool>();
    
    private void Awake()
    {
        PlayerDataDisplayOff();
        PlayerStatsDsiplayOff();

        playerStatsUpgrade.Add(false);
        playerStatsActions.Add(() => true, () => CheckAndExecute(clickTime, SharedPlayerStats.IncreasePlayerPower));
    }

    public void PlayerDataDisPlayOn()
    {
        playerStatsPanel.SetActive(true);
    }
    public void PlayerDataDisplayOff()
    {
        playerStatsPanel.SetActive(false);
    }
    public void PlayerStatsDisPlayOn()
    {
        foreach (var player in PlayerDatas)
        {
            player.SetActive(true);
        }
    }
    public void PlayerStatsDsiplayOff()
    {
        foreach (var player in PlayerDatas)
        {
            player.SetActive(false);
        }
    }

    public void PlayerPowerUpgradeButton()
    {
        playerStatsUpgrade[0] = !playerStatsUpgrade[0];

        if (!playerStatsUpgrade[0] ) 
        {
            Debug.Log(SharedPlayerStats.GetPlayerPower());
        }
    }

    private void Update()
    {
        foreach (var action in playerStatsActions)
        {
            if (action.Key.Invoke())
            {
                action.Value.Invoke();
            }
        }
    }

    public void CheckAndExecute(float clickTime, Action executeAction)
    {
        if (Time.time > nowTime + clickTime)
        {
            nowTime = Time.time;
            executeAction.Invoke();
        }
    }

}


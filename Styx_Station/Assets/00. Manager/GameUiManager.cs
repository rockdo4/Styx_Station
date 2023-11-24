using System;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class GameUiManager : Singleton<GameUiManager>
{
    public GameObject playerStatsPanel;
    public List<GameObject> PlayerDatas = new List<GameObject>();
    public List<TextMeshProUGUI> moneyList = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> playerStatsData = new List<TextMeshProUGUI>();
    public float clickTime;
    public float nowTime;
    private List<bool> playerStatsButtonDown = new List<bool>();
    private bool isUpgradeMoney;
    private BigInteger test = new BigInteger(0);
    // test code -> if we will make mainGameLogic ,change this code 

    private Dictionary<int, Func<string>> playerStatsActions = new Dictionary<int, Func<string>>
    {
        { 0, () => UnitConverter.OutString(SharedPlayerStats.GetPlayerPower()) },
        { 1, () => UnitConverter.OutString(SharedPlayerStats.GetPlayerPowerBoost()) },
        { 2 ,()=> UnitConverter.OutString(SharedPlayerStats.GetPlayerAttackSpeed()) },
        { 3 ,()=> UnitConverter.OutString(SharedPlayerStats.GetAttackCritical()) },
        { 4 ,()=> UnitConverter.OutString(SharedPlayerStats.GetAttackCriticlaPower()) },
        { 5 ,()=> UnitConverter.OutString(SharedPlayerStats.GetMonsterDamagePower()) },
        { 6 ,()=> UnitConverter.OutString(SharedPlayerStats.GetHp()) },
        { 7 ,()=> UnitConverter.OutString(SharedPlayerStats.GetHealing()) },
    };

    private void Awake()
    {
        PlayerDataDisplayOff();
        PlayerStatsDsiplayOff();

        for (int i = 0; i < 8; i++)
        {
            playerStatsButtonDown.Add(false);
        }
        UnitConverter.InitUnitConverter();
    }

    private void Start()
    {
        moneyList[0].text = $"{UnitConverter.OutString(SharedPlayerStats.money1)}";
    }

    public void PlayerDataDisPlayOn()
    {
        playerStatsPanel.SetActive(true);
    }
    public void PlayerDataDisplayOff()
    {
        PlayerStatsDsiplayOff();
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
        playerStatsButtonDown[0] = !playerStatsButtonDown[0];
    }
    public void PlayerPowerBoostUpgradeButton()
    {
        playerStatsButtonDown[1] = !playerStatsButtonDown[1];
    }


    public void IncreaseTestMoney1()
    {
        isUpgradeMoney = !isUpgradeMoney;
    }
    private void Update()
    {
        CheckAndExecute(playerStatsButtonDown[0], SharedPlayerStats.IncreasePlayerPower, 0, "공격력 : ");
        CheckAndExecute(playerStatsButtonDown[1], SharedPlayerStats.IncreasePlayerPowerBoost, 1, "공격력 증폭: ");
        CheckAndExecute(playerStatsButtonDown[2], SharedPlayerStats.IncreasePlayerPowerBoost, 2, "공격속도: ");
        CheckAndExecute(playerStatsButtonDown[3], SharedPlayerStats.IncreasePlayerPowerBoost, 2, "치명타 : ");
        CheckAndExecute(playerStatsButtonDown[4], SharedPlayerStats.IncreasePlayerPowerBoost, 2, "치명타 피해: ");
        CheckAndExecute(playerStatsButtonDown[5], SharedPlayerStats.IncreasePlayerPowerBoost, 2, "몬스터 데미지: ");
        CheckAndExecute(playerStatsButtonDown[6], SharedPlayerStats.IncreasePlayerPowerBoost, 2, "HP : ");
        CheckAndExecute(playerStatsButtonDown[7], SharedPlayerStats.IncreasePlayerPowerBoost, 2, "HP 회복 : ");
        CheckAndExecute(isUpgradeMoney, () => SharedPlayerStats.IncreaseMoney(test), -1, "");
    }

    private void CheckAndExecute(bool condition, Action action, int statsIndex, string label)
    {
        if (condition && nowTime + clickTime < Time.time)
        {
            nowTime = Time.time;
            action.Invoke();
            if (statsIndex >= 0)
            {
                playerStatsData[statsIndex].text = $"{label} : {GetStat(statsIndex)}";
            }
            ResetStringMoney1();
        }
    }


    private string GetStat(int index)
    {
        if (playerStatsActions.TryGetValue(index, out Func<string> action))
        {
            return action();
        }
        return "err";
    }
    private void ResetStringMoney1()
    {
        moneyList[0].text = $"{UnitConverter.OutString(SharedPlayerStats.money1)}";
    }
}


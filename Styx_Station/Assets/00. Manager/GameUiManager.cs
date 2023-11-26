using System;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUiManager : Singleton<GameUiManager>
{
    public GameObject playerStatsPanel;
    public List<GameObject> PlayerDatas = new List<GameObject>();
    private bool isPlayerDataOn;

    public List<TextMeshProUGUI> moneyList = new List<TextMeshProUGUI>(); // testcode , change Texture
    private bool isUpgradeMoney;

    public List<Button> playUpgradeButton = new List<Button>();
    private List<bool> playerStatsButtonDown = new List<bool>();
    private Dictionary<string,List<string>> playerStatsButtonText = new Dictionary<string,List<string>>();
    public int playerUpgradeButtonIndex;
    public float clickTime;
    public float nowTime;
    private int prevUpgradeValue;
    private Time prevUpgardeValueTime;
    private int nowUpgrdaeValue;
    private Time nowUpgradeValueTime;

    public List<TextMeshProUGUI> playerStatsData = new List<TextMeshProUGUI>();


    private BigInteger test = new BigInteger(0);
    // test code -> if we will make mainGameLogic ,change this code 

    StringTable stringTable ;
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

    private Dictionary<int, Func<int>> playerUpgradeStatsActions = new Dictionary<int, Func<int>>()
    {
        { 0, () => SharedPlayerStats.GetPlayerPower() },
        { 1, () => SharedPlayerStats.GetPlayerPowerBoost() },
        { 2 ,()=> SharedPlayerStats.GetPlayerAttackSpeed() },
        { 3 ,()=> SharedPlayerStats.GetAttackCritical() },
        { 4 ,()=> SharedPlayerStats.GetAttackCriticlaPower() },
        { 5 ,()=> SharedPlayerStats.GetMonsterDamagePower() },
        { 6 ,()=>SharedPlayerStats.GetHp() },
        { 7 ,()=> SharedPlayerStats.GetHealing() },
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
        SettingPlayerStatsButton();
        stringTable = new StringTable();
        //StringTable.Instance.dic

        foreach(var key in StringTable.Instance.dic)
        {
            if(key.Key.Contains("PlayerStatsButton"))
            {

                if (!playerStatsButtonText.ContainsKey("KOR"))
                    playerStatsButtonText["KOR"] = new List<string>();
                playerStatsButtonText["KOR"].Add(key.Value.KOR);

                if (!playerStatsButtonText.ContainsKey("ENG"))
                    playerStatsButtonText["ENG"] = new List<string>();
                playerStatsButtonText["ENG"].Add(key.Value.ENG);
            }
        }
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
        isPlayerDataOn = true;
        SettingPlayerStatsDisplayInit();
    }
    public void PlayerStatsDsiplayOff()
    {
        foreach (var player in PlayerDatas)
        {
            player.SetActive(false);
        }
        isPlayerDataOn = false;
    }
    public void PlayerUpgrade(PointerEventData data, int index)
    {
        playerStatsButtonDown[index] = !playerStatsButtonDown[index];
        if (playerStatsButtonDown[index])
        {
            var value = GetStatsUpgradeValue(index);
            if (value != -1)
            {
                prevUpgradeValue = value;
                string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
                Debug.Log($"{currentTime}  : 강화를 X 시작했습니다.");
            }

        }
        else 
        {
            var value = GetStatsUpgradeValue(index);
            if (value != -1)
                nowUpgrdaeValue = value;

            var upgradeValue = nowUpgrdaeValue - prevUpgradeValue;
            string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
            Debug.Log($"{currentTime}  : 강화 X {upgradeValue} 만큼 했습니다.");
        }
    }

    public void IncreaseTestMoney1()
    {
        isUpgradeMoney = !isUpgradeMoney;
    }
    private void Update()
    {
        if (isPlayerDataOn)
            SettingPlayerStatsTextUpdate();
    }
    private void SettingPlayerStatsDisplayInit()
    {
        CheckAndExecute(true, SharedPlayerStats.IncreasePlayerPower, 0, "공격력 : ", true);
        CheckAndExecute(true, SharedPlayerStats.IncreasePlayerPowerBoost, 1, "공격력 증폭: ", true);
        CheckAndExecute(true, SharedPlayerStats.IncreasePlayerAttackSpeed, 2, "공격속도: ", true);
        CheckAndExecute(true, SharedPlayerStats.IncreaseAttackCritical, 3, "치명타 : ", true);
        CheckAndExecute(true, SharedPlayerStats.IncreaseAttackCriticalPower, 4, "치명타 피해: ", true);
        CheckAndExecute(true, SharedPlayerStats.IncreaseMonsterDamagePower, 5, "몬스터 데미지: ", true);
        CheckAndExecute(true, SharedPlayerStats.IncreaseHp, 6, "HP : ", true);
        CheckAndExecute(true, SharedPlayerStats.IncreaseHealing, 7, "HP 회복 : ", true);
    }
    private void SettingPlayerStatsTextUpdate()
    {
        CheckAndExecute(playerStatsButtonDown[0], SharedPlayerStats.IncreasePlayerPower, 0, "공격력 : ");
        CheckAndExecute(playerStatsButtonDown[1], SharedPlayerStats.IncreasePlayerPowerBoost, 1, "공격력 증폭: ");
        CheckAndExecute(playerStatsButtonDown[2], SharedPlayerStats.IncreasePlayerAttackSpeed, 2, "공격속도: ");
        CheckAndExecute(playerStatsButtonDown[3], SharedPlayerStats.IncreaseAttackCritical, 3, "치명타 : ");
        CheckAndExecute(playerStatsButtonDown[4], SharedPlayerStats.IncreaseAttackCriticalPower, 4, "치명타 피해: ");
        CheckAndExecute(playerStatsButtonDown[5], SharedPlayerStats.IncreaseMonsterDamagePower, 5, "몬스터 데미지: ");
        CheckAndExecute(playerStatsButtonDown[6], SharedPlayerStats.IncreaseHp, 6, "HP : ");
        CheckAndExecute(playerStatsButtonDown[7], SharedPlayerStats.IncreaseHealing, 7, "HP 회복 : ");
        CheckAndExecute(isUpgradeMoney, () => SharedPlayerStats.IncreaseMoney(test), -1, "");
    }
    private void CheckAndExecute(bool condition, Action action, int statsIndex, string label, bool isInit = false)
    {
        if ((condition && nowTime + clickTime < Time.time) || isInit)
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

    private int GetStatsUpgradeValue(int index)
    {
        if(playerUpgradeStatsActions.TryGetValue(index , out Func<int> action))
        {
            return action();
        }
        return -1;
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

    private void SettingPlayerStatsButton()
    {
        for (int i = 0; i < playUpgradeButton.Count; i++)
        {
            var buttonText = playUpgradeButton[i].GetComponentInChildren<TextMeshProUGUI>();
            var eventTrigger = playUpgradeButton[i].GetComponent<EventTrigger>();
            if (eventTrigger != null)
            {
                // PointerDown 이벤트 추가
                var newIndex = i;
                var pointerDown = new EventTrigger.Entry();
                pointerDown.eventID = EventTriggerType.PointerDown;
                pointerDown.callback.AddListener((data) => { PlayerUpgrade((PointerEventData)data, newIndex); });
                eventTrigger.triggers.Add(pointerDown);

                // PointerUp 이벤트 추가
                var pointerUp = new EventTrigger.Entry();
                pointerUp.eventID = EventTriggerType.PointerUp;
                pointerUp.callback.AddListener((data) => { PlayerUpgrade((PointerEventData)data, newIndex); });
                eventTrigger.triggers.Add(pointerUp);
            }
        }
    }
}
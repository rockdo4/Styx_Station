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
    public struct StringTableLanaguage
    {
        public string kor;
        public string eng;
    }
    public GameObject playerStatsPanel;
    public List<GameObject> PlayerDatas = new List<GameObject>();
    private bool isPlayerDataOn;

    public List<TextMeshProUGUI> moneyList = new List<TextMeshProUGUI>(); // testcode , change Texture
    private bool isUpgradeMoney;

    public List<Button> playUpgradeButton = new List<Button>();
    public Button statsButton;
    public Button statsClostButton;
    public Button inventoryButton;
    private List<bool> isPlayerStatsButtonDown = new List<bool>();
    private Dictionary<string, StringTableLanaguage> playerStatsTexts = new Dictionary<string, StringTableLanaguage>();
    public int playerUpgradeButtonIndex;
    public float clickTime;
    public float nowTime;
    private int prevUpgradeValue;
    private int nowUpgrdaeValue;

    public TextMeshProUGUI playerStatsUpgardeDisplay;
    public List<TextMeshProUGUI> playerStatsData = new List<TextMeshProUGUI>();


    private BigInteger test = new BigInteger(0);
    // test code -> if we will make mainGameLogic ,change this code 

    private Language uiLanaguage;
    StringTable stringTable;
    public GameObject logScrollView;
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
            isPlayerStatsButtonDown.Add(false);
        }
        UnitConverter.InitUnitConverter();
        var lo = GetComponent<SaveLoad>();
        lo.Load();
    }

    private void Start()
    {
        moneyList[0].text = $"{UnitConverter.OutString(SharedPlayerStats.money1)}";
        SettingPlayerStatsButton();
        InitPlayerStatsText();
        ChangeLangugaeButtonText();
        ChangePlayerStatsUpgardeText();
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
    public void ChangeLanguageTestButton()
    {
        switch (Global.language)
        {
            case Language.KOR:
                Global.language = Language.ENG;
                break;
            case Language.ENG:
                Global.language = Language.KOR;
                break;
        }
    }
    public void PlayerUpgrade(PointerEventData data, int index)
    {
        isPlayerStatsButtonDown[index] = !isPlayerStatsButtonDown[index];
        //Test

        string upgradeType = string.Empty;
       
        int numer = index + 1;
        string id = "PlayerStatsButton00" + numer.ToString();
        var table = playerStatsTexts[id];
        upgradeType = table.kor;
        string log = string.Empty;
        if (isPlayerStatsButtonDown[index])
        {
            var value = GetStatsUpgradeValue(index);
            if (value != -1)
            {
                prevUpgradeValue = value;
                string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
                log = $"{currentTime}  : {upgradeType}를  시작했습니다.";
                var findLog = Log.Instance;
                if (findLog.parent == null)
                {
                    findLog.parent = logScrollView;
                }
                findLog.MakeLogText(log);
            }
        }
        else
        {
            var value = GetStatsUpgradeValue(index);
            if (value != -1)
                nowUpgrdaeValue = value;

            var upgradeValue = nowUpgrdaeValue - prevUpgradeValue;
            string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
            log = $"{currentTime}  : {upgradeType}를 {upgradeValue} 만큼 했습니다.";
            var findLog = Log.Instance;
            if (findLog.parent == null)
            {
                findLog.parent = logScrollView;
            }
            findLog.MakeLogText(log);
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

        if (uiLanaguage != Global.language)
        {
            uiLanaguage = Global.language;
            SettingPlayerStatsDisplayInit();
            ChangeLangugaeButtonText();
            ChangePlayerStatsUpgardeText();
        }
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
        CheckAndExecute(isPlayerStatsButtonDown[0], SharedPlayerStats.IncreasePlayerPower, 0, "공격력 : ");
        CheckAndExecute(isPlayerStatsButtonDown[1], SharedPlayerStats.IncreasePlayerPowerBoost, 1, "공격력 증폭: ");
        CheckAndExecute(isPlayerStatsButtonDown[2], SharedPlayerStats.IncreasePlayerAttackSpeed, 2, "공격속도: ");
        CheckAndExecute(isPlayerStatsButtonDown[3], SharedPlayerStats.IncreaseAttackCritical, 3, "치명타 : ");
        CheckAndExecute(isPlayerStatsButtonDown[4], SharedPlayerStats.IncreaseAttackCriticalPower, 4, "치명타 피해: ");
        CheckAndExecute(isPlayerStatsButtonDown[5], SharedPlayerStats.IncreaseMonsterDamagePower, 5, "몬스터 데미지: ");
        CheckAndExecute(isPlayerStatsButtonDown[6], SharedPlayerStats.IncreaseHp, 6, "HP : ");
        CheckAndExecute(isPlayerStatsButtonDown[7], SharedPlayerStats.IncreaseHealing, 7, "HP 회복 : ");
        CheckAndExecute(isUpgradeMoney, () => SharedPlayerStats.IncreaseMoney(test), -1, "");
    }
    private void CheckAndExecute(bool condition, Action action, int statsIndex, string label, bool isInit = false)
    {
        string str = string.Empty;
        if (statsIndex >= 0)
        {
            var id = "PlayerUpgradeStatsDisplay" + "00" + statsIndex.ToString();
            var data = playerStatsTexts[id];
            switch (Global.language)
            {
                case Language.KOR:
                    str = data.kor;
                    break;
                case Language.ENG:
                    str = data.eng;
                    break;
            }
        }
        if ((condition && nowTime + clickTime < Time.time) || isInit)
        {
            nowTime = Time.time;
            action.Invoke();
            if (statsIndex >= 0)
            {
                playerStatsData[statsIndex].text = $"{str} : {GetStat(statsIndex)}";
            }
            ResetStringMoney1();
        }
    }

    private int GetStatsUpgradeValue(int index)
    {
        if (playerUpgradeStatsActions.TryGetValue(index, out Func<int> action))
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
                var newIndex = i;
                var pointerDown = new EventTrigger.Entry();
                pointerDown.eventID = EventTriggerType.PointerDown;
                pointerDown.callback.AddListener((data) => { PlayerUpgrade((PointerEventData)data, newIndex); });
                eventTrigger.triggers.Add(pointerDown);

                var pointerUp = new EventTrigger.Entry();
                pointerUp.eventID = EventTriggerType.PointerUp;
                pointerUp.callback.AddListener((data) => { PlayerUpgrade((PointerEventData)data, newIndex); });
                eventTrigger.triggers.Add(pointerUp);
            }
        }
    }
    private void ChangeLangugaeButtonText()
    {
        var statsButtonTextMeshProUGUI = statsButton.GetComponentInChildren<TextMeshProUGUI>();
        var inventoryButtonTextMeshProUGUI = inventoryButton.GetComponentInChildren<TextMeshProUGUI>();
        var statsCloseButtonTextMeshProUGUI = statsClostButton.GetComponentInChildren<TextMeshProUGUI>();
        var statsText = playerStatsTexts["PlayerStatsButton000"];
        var inventoryText = playerStatsTexts["PlayerStatsButton010"];
        var closeButtonText = playerStatsTexts["PlayerStatsButton009"];
        var playerStatsUpgardeDisplayText = playerStatsTexts["PlayerStats001"];
        switch (Global.language)
        {

            case Language.KOR:

                statsButtonTextMeshProUGUI.text = statsText.kor;
                inventoryButtonTextMeshProUGUI.text = inventoryText.kor;
                statsCloseButtonTextMeshProUGUI.text = closeButtonText.kor;
                playerStatsUpgardeDisplay.text = playerStatsUpgardeDisplayText.kor;
                break;
            case Language.ENG:
                statsButtonTextMeshProUGUI.text = statsText.eng;
                inventoryButtonTextMeshProUGUI.text = inventoryText.eng;
                statsCloseButtonTextMeshProUGUI.text = closeButtonText.eng;
                playerStatsUpgardeDisplay.text = playerStatsUpgardeDisplayText.eng;
                break;
        }
    }
    private void InitPlayerStatsText()
    {
        stringTable = new StringTable();
        uiLanaguage = Global.language;
        foreach (var key in StringTable.Instance.dic)
        {
            if (key.Key.Contains("PlayerStatsButton") || key.Key.Contains("PlayerStats") || key.Key.Contains("PlayerUpgradeStatsDisplay") || key.Key.Contains("PlayerDisplayCloseButton"))
            {
                StringTableLanaguage data = new StringTableLanaguage();
                data.kor = key.Value.KOR;
                data.eng = key.Value.ENG;
                playerStatsTexts.Add(key.Key, data);
            }
        }
    }
    private void ChangePlayerStatsUpgardeText()
    {
        for (int i = 0; i < playUpgradeButton.Count; ++i)
        {
            var button = playUpgradeButton[i].GetComponentInChildren<TextMeshProUGUI>();
            int numer = i + 1;
            string id = "PlayerStatsButton00" + numer.ToString();
            var data = playerStatsTexts[id];
            var str = string.Empty;
            switch (Global.language)
            {
                case Language.KOR:
                    str = data.kor;
                    break;
                case Language.ENG:
                    str = data.eng;
                    break;
            }
            button.text = str;
        }
    }

    public string sibal()
    {
        return "sibal";
    }
}
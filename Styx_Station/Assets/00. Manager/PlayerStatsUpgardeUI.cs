using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
money1 과관련된 코드는 다 테스트 코드로 간주 
text변경은 UiManger코에서 직접 하게변경할 예정임
*/
public class PlayerStatsUpgardeUI : MonoBehaviour//Singleton<PlayerStatsUpgardeUI>
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
    private bool isUpgradeMoney1;
    private bool isUpgradeMoney2;
    private bool isUpgradeMoney3;

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
    StringTable stringTable; // UiManager 싱글톤에서 데이터를 받아오는 형식으로 할것
    public GameObject logScrollView;
    private bool isOneAwkae;
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
    private void OnApplicationQuit()
    {
        var lo = GetComponent<SaveLoad>();
        lo.Save();
    }
    private void Start()
    {
        ResetStringMoney(); //testcode

        InitPlayerStatsText();
        SettingPlayerStatsButton();
        ChangeLangugaeButtonText();
        ChangePlayerStatsUpgardeText();

        //PlayerDataDisplayOff();
        //PlayerStatsDsiplayOff();
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
        if (!SharedPlayerStats.IsPlayerPowerBoostAmplifiable && index ==1)
        {
            // 팝업창 띄우게하기
            return;
        }
        isPlayerStatsButtonDown[index] = !isPlayerStatsButtonDown[index];
        PlayerUpgradeLog(isPlayerStatsButtonDown[index], index);
    }
    private void PlayerUpgradeLog(bool type,int index)
    {
        string upgradeType = string.Empty;

        int numer = index + 1;
        string id = "PlayerStatsButton00" + numer.ToString();
        var table = playerStatsTexts[id];
        upgradeType = table.kor;
        string log = string.Empty;
        if (type)
        {
            var value = GetStatsUpgradeValue(index);
            if (value != -1)
            {
                prevUpgradeValue = value;
                string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
                log = $"{currentTime}  : {upgradeType}를  시작했습니다.";
                //var findLog = Log.Instance;
                //if (findLog.parent == null)
                //{
                //    findLog.parent = logScrollView;
                //}
                Log.Instance.MakeLogText(log);
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
    public void IncreaseTestMoney1() //test code
    {
        isUpgradeMoney1 = !isUpgradeMoney1;
    }
    public void IncreaseTestMoney2() //test code
    {
        isUpgradeMoney2 = !isUpgradeMoney2;
    }
    public void IncreaseTestMoney3() //test code
    {
        isUpgradeMoney3 = !isUpgradeMoney3;
    }
    private void Update()
    {
        if (isPlayerDataOn)
            SettingPlayerStatsTextUpdate();
        CheckAndExecute(isUpgradeMoney1, () => CurrencyManager.IncreaseMoney1(test), -1); //tescode
        CheckAndExecute(isUpgradeMoney2, () => CurrencyManager.IncreaseMoney2(test), -1); //tescode
        CheckAndExecute(isUpgradeMoney3, () => CurrencyManager.IncreaseMoney3(test), -1); //tescode
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
        CheckAndExecute(true, 0);
        CheckAndExecute(true, 1);
        CheckAndExecute(true, 2);
        CheckAndExecute(true, 3);
        CheckAndExecute(true, 4);
        CheckAndExecute(true, 5);
        CheckAndExecute(true, 6);
        CheckAndExecute(true, 7);
    }
    private void CheckAndExecute(bool condition, int statsIndex)
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
        if (statsIndex >= 0)
        {
            playerStatsData[statsIndex].text = $"{str} : {GetStat(statsIndex)}";
        }
    }
    private void SettingPlayerStatsTextUpdate()
    {
        CheckAndExecute(isPlayerStatsButtonDown[0], SharedPlayerStats.IncreasePlayerPower, 0);
        CheckAndExecute(isPlayerStatsButtonDown[1], SharedPlayerStats.IncreasePlayerPowerBoost,1);
        CheckAndExecute(isPlayerStatsButtonDown[2], SharedPlayerStats.IncreasePlayerAttackSpeed, 2);
        CheckAndExecute(isPlayerStatsButtonDown[3], SharedPlayerStats.IncreaseAttackCritical, 3);
        CheckAndExecute(isPlayerStatsButtonDown[4], SharedPlayerStats.IncreaseAttackCriticalPower, 4);
        CheckAndExecute(isPlayerStatsButtonDown[5], SharedPlayerStats.IncreaseMonsterDamagePower, 5);
        CheckAndExecute(isPlayerStatsButtonDown[6], SharedPlayerStats.IncreaseHp, 6);
        CheckAndExecute(isPlayerStatsButtonDown[7], SharedPlayerStats.IncreaseHealing, 7);
    }
    private void CheckAndExecute(bool condition, Action action, int statsIndex)
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
        if (condition && nowTime + clickTime < Time.time)
        {
            nowTime = Time.time;
            action.Invoke();
            if (statsIndex >= 0)
            {
                playerStatsData[statsIndex].text = $"{str} : {GetStat(statsIndex)}";
            }
            ResetStringMoney(); //tescdoe
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
    public void ResetStringMoney() //test code
    {
        moneyList[0].text = $"{UnitConverter.OutString(CurrencyManager.money1)}"; //test code
        moneyList[1].text = $"{UnitConverter.OutString(CurrencyManager.money2)}"; //test code
        moneyList[2].text = $"{UnitConverter.OutString(CurrencyManager.money3)}"; //test code
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
        if (!isOneAwkae)
        {
            stringTable = new StringTable();
            isOneAwkae = false; 
        }

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
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Window[] windows;

    public GameObject panel;

    public WindowType currentWindow;

    public List<Button> windowButtons = new List<Button>();

    public GameObject[] wayPoints;

    public GameObject buttons;

    private bool menu = false;

    private bool first = false;

    private Vector3 wayPoint = Vector3.zero;
    private Vector3 buttonPos = Vector3.zero;

    private Coroutine move;

    public SkillWindow skill;

    public QuestSystemUi questSystemUi;

    //12.18 윤유림 웨이브 반복 버튼
    public GameObject RepeatButton;
    public Slider timerSlider;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI stageText;

    public List<GameObject> WavePanels;

    public GameObject gameOverPop;
    public GameObject[] autoSkill = new GameObject[2];
    private bool isAuto = false;
    public Image HpGauge;

    public TextMeshProUGUI sliverMoney;
    public TextMeshProUGUI pommeMoney;
    public TextMeshProUGUI soulMoney;
    public TextMeshProUGUI memoryMoney;

    private bool isBangchi;
    public BangchiWindow bangchiWindow;

    public PlayerBuffWindow playerBuffWindow;
    public DiningRoomUIManager roomUIManager;
    private void Start() //12.20 Lsw 
    {
        PrintSliverMoney();
        PrintPommeMoney();
        PrintSoulMoney();
        PrintMemoryMoney();

    }
    public void Open(WindowType inventoryType)
    {
        if (!first)
        {
            wayPoint = wayPoints[0].transform.position - wayPoints[1].transform.position;
            buttonPos = buttons.transform.position;
            first = true;
        }

        if (windows[(int)currentWindow].gameObject.activeSelf)
            windows[(int)currentWindow].Close();

        currentWindow = inventoryType;

        windows[(int)inventoryType].Open();
    }

    // 12.25 Button 수정 _IJ
    public void OnClickInfo()
    {
        if ((ButtonList.mainButton & ButtonType.Info) == 0 &&
            (ButtonList.mainButton & ButtonType.TrainMove) == 0 &&
            move == null)
        {
            if ((ButtonList.mainButton & ButtonType.Train) != 0)
            {
                ButtonList.mainButton |= ButtonType.TrainMove;
                OnClickClose();
            }

            if ((ButtonList.mainButton & ButtonType.DiningRoom) != 0)
                ButtonList.mainButton &= ~ButtonType.DiningRoom;

            if ((ButtonList.mainButton & ButtonType.Lab) != 0)
                ButtonList.mainButton &= ~ButtonType.Lab;

            if ((ButtonList.mainButton & ButtonType.Cleaning) != 0)
                ButtonList.mainButton &= ~ButtonType.Cleaning;

            if ((ButtonList.mainButton & ButtonType.Failure) != 0)
                ButtonList.mainButton &= ~ButtonType.Failure;

            if ((ButtonList.mainButton & ButtonType.Shop) != 0)
                ButtonList.mainButton &= ~ButtonType.Shop;

            if ((ButtonList.mainButton & ButtonType.Menu) != 0)
                ButtonList.mainButton &= ~ButtonType.Menu;

            SkillButtonOff();
            Open(WindowType.Info);
            ButtonList.mainButton |= ButtonType.Info;
        }
    }
    public void OnClickDiningRoom()
    {
        if ((ButtonList.mainButton & ButtonType.DiningRoom) == 0 &&
            (ButtonList.mainButton & ButtonType.TrainMove) == 0 &&
            move == null)
        {
            if ((ButtonList.mainButton & ButtonType.Train) != 0)
            {
                ButtonList.mainButton |= ButtonType.TrainMove;
                OnClickClose();
            }

            if ((ButtonList.mainButton & ButtonType.Info) != 0)
                ButtonList.mainButton &= ~ButtonType.Info;

            if ((ButtonList.mainButton & ButtonType.Lab) != 0)
                ButtonList.mainButton &= ~ButtonType.Lab;

            if ((ButtonList.mainButton & ButtonType.Cleaning) != 0)
                ButtonList.mainButton &= ~ButtonType.Cleaning;

            if ((ButtonList.mainButton & ButtonType.Failure) != 0)
                ButtonList.mainButton &= ~ButtonType.Failure;

            if ((ButtonList.mainButton & ButtonType.Shop) != 0)
                ButtonList.mainButton &= ~ButtonType.Shop;

            if ((ButtonList.mainButton & ButtonType.Menu) != 0)
                ButtonList.mainButton &= ~ButtonType.Menu;

            Open(WindowType.DiningRoom);
            ButtonList.mainButton |= ButtonType.DiningRoom;
        }
    }

    public void OnClickLab()
    {
        if ((ButtonList.mainButton & ButtonType.Lab) == 0 &&
            (ButtonList.mainButton & ButtonType.TrainMove) == 0 &&
            move == null)
        {
            if ((ButtonList.mainButton & ButtonType.Train) != 0)
            {
                ButtonList.mainButton |= ButtonType.TrainMove;
                OnClickClose();
            }

            if ((ButtonList.mainButton & ButtonType.Info) != 0)
                ButtonList.mainButton &= ~ButtonType.Info;

            if ((ButtonList.mainButton & ButtonType.DiningRoom) != 0)
                ButtonList.mainButton &= ~ButtonType.DiningRoom;

            if ((ButtonList.mainButton & ButtonType.Cleaning) != 0)
                ButtonList.mainButton &= ~ButtonType.Cleaning;

            if ((ButtonList.mainButton & ButtonType.Failure) != 0)
                ButtonList.mainButton &= ~ButtonType.Failure;

            if ((ButtonList.mainButton & ButtonType.Shop) != 0)
                ButtonList.mainButton &= ~ButtonType.Shop;

            if ((ButtonList.mainButton & ButtonType.Menu) != 0)
                ButtonList.mainButton &= ~ButtonType.Menu;

            Open(WindowType.Lab);
            ButtonList.mainButton |= ButtonType.Lab;
        }
    }

    public void OnClickCleaning()
    {
        if ((ButtonList.mainButton & ButtonType.Cleaning) == 0 &&
            (ButtonList.mainButton & ButtonType.TrainMove) == 0 &&
            move == null)
        {
            if ((ButtonList.mainButton & ButtonType.Train) != 0)
            {
                ButtonList.mainButton |= ButtonType.TrainMove;
                OnClickClose();
            }
            if ((ButtonList.mainButton & ButtonType.Info) != 0)
                ButtonList.mainButton &= ~ButtonType.Info;

            if ((ButtonList.mainButton & ButtonType.DiningRoom) != 0)
                ButtonList.mainButton &= ~ButtonType.DiningRoom;

            if ((ButtonList.mainButton & ButtonType.Lab) != 0)
                ButtonList.mainButton &= ~ButtonType.Lab;

            if ((ButtonList.mainButton & ButtonType.Failure) != 0)
                ButtonList.mainButton &= ~ButtonType.Failure;

            if ((ButtonList.mainButton & ButtonType.Shop) != 0)
                ButtonList.mainButton &= ~ButtonType.Shop;

            if ((ButtonList.mainButton & ButtonType.Menu) != 0)
                ButtonList.mainButton &= ~ButtonType.Menu;

            Open(WindowType.Cleaning);
            ButtonList.mainButton |= ButtonType.Cleaning;
        }
    }

    public void OnClickFailure()
    {
        if ((ButtonList.mainButton & ButtonType.Failure) == 0 &&
            (ButtonList.mainButton & ButtonType.TrainMove) == 0 &&
            move == null)
        {
            if ((ButtonList.mainButton & ButtonType.Train) != 0)
            {
                ButtonList.mainButton |= ButtonType.TrainMove;
                OnClickClose();
            }
            if ((ButtonList.mainButton & ButtonType.Info) != 0)
                ButtonList.mainButton &= ~ButtonType.Info;

            if ((ButtonList.mainButton & ButtonType.DiningRoom) != 0)
                ButtonList.mainButton &= ~ButtonType.DiningRoom;

            if ((ButtonList.mainButton & ButtonType.Lab) != 0)
                ButtonList.mainButton &= ~ButtonType.Lab;

            if ((ButtonList.mainButton & ButtonType.Cleaning) != 0)
                ButtonList.mainButton &= ~ButtonType.Cleaning;

            if ((ButtonList.mainButton & ButtonType.Shop) != 0)
                ButtonList.mainButton &= ~ButtonType.Shop;

            if ((ButtonList.mainButton & ButtonType.Menu) != 0)
                ButtonList.mainButton &= ~ButtonType.Menu;

            Open(WindowType.Failure);
            ButtonList.mainButton |= ButtonType.Failure;
        }
    }

    public void OnClickMenu()
    {
        if ((ButtonList.mainButton & ButtonType.Menu) == 0 &&
            (ButtonList.mainButton & ButtonType.TrainMove) == 0 &&
            move == null)
        {
            if ((ButtonList.mainButton & ButtonType.Train) != 0)
            {
                ButtonList.mainButton |= ButtonType.TrainMove;
                OnClickClose();
            }
            if ((ButtonList.mainButton & ButtonType.Info) != 0)
                ButtonList.mainButton &= ~ButtonType.Info;

            if ((ButtonList.mainButton & ButtonType.DiningRoom) != 0)
                ButtonList.mainButton &= ~ButtonType.DiningRoom;

            if ((ButtonList.mainButton & ButtonType.Lab) != 0)
                ButtonList.mainButton &= ~ButtonType.Lab;

            if ((ButtonList.mainButton & ButtonType.Cleaning) != 0)
                ButtonList.mainButton &= ~ButtonType.Cleaning;

            if ((ButtonList.mainButton & ButtonType.Failure) != 0)
                ButtonList.mainButton &= ~ButtonType.Failure;

            if ((ButtonList.mainButton & ButtonType.Shop) != 0)
                ButtonList.mainButton &= ~ButtonType.Shop;

            Open(WindowType.Menu);
            ButtonList.mainButton |= ButtonType.Menu;

        }
        else if ((ButtonList.mainButton & ButtonType.Menu) != 0 &&
            (ButtonList.mainButton & ButtonType.TrainMove) == 0 &&
            move == null)
        {
            if ((ButtonList.mainButton & ButtonType.Train) != 0)
            {
                ButtonList.mainButton |= ButtonType.TrainMove;
                OnClickClose();
            }
            if ((ButtonList.mainButton & ButtonType.Info) != 0)
                ButtonList.mainButton &= ~ButtonType.Info;

            if ((ButtonList.mainButton & ButtonType.DiningRoom) != 0)
                ButtonList.mainButton &= ~ButtonType.DiningRoom;

            if ((ButtonList.mainButton & ButtonType.Lab) != 0)
                ButtonList.mainButton &= ~ButtonType.Lab;

            if ((ButtonList.mainButton & ButtonType.Cleaning) != 0)
                ButtonList.mainButton &= ~ButtonType.Cleaning;

            if ((ButtonList.mainButton & ButtonType.Failure) != 0)
                ButtonList.mainButton &= ~ButtonType.Failure;

            if ((ButtonList.mainButton & ButtonType.Shop) != 0)
                ButtonList.mainButton &= ~ButtonType.Shop;

            windows[(int)currentWindow].Close();
            ButtonList.mainButton &= ~ButtonType.Menu;
        }
    }

    public void OnClickShop()
    {
        if ((ButtonList.mainButton & ButtonType.Shop) == 0 &&
            (ButtonList.mainButton & ButtonType.TrainMove) == 0 &&
            move == null)
        {
            if ((ButtonList.mainButton & ButtonType.Train) != 0)
            {
                ButtonList.mainButton |= ButtonType.TrainMove;
                OnClickClose();
            }

            if ((ButtonList.mainButton & ButtonType.Info) != 0)
                ButtonList.mainButton &= ~ButtonType.Info;

            if ((ButtonList.mainButton & ButtonType.DiningRoom) != 0)
                ButtonList.mainButton &= ~ButtonType.DiningRoom;

            if ((ButtonList.mainButton & ButtonType.Lab) != 0)
                ButtonList.mainButton &= ~ButtonType.Lab;

            if ((ButtonList.mainButton & ButtonType.Cleaning) != 0)
                ButtonList.mainButton &= ~ButtonType.Cleaning;

            if ((ButtonList.mainButton & ButtonType.Failure) != 0)
                ButtonList.mainButton &= ~ButtonType.Failure;

            if ((ButtonList.mainButton & ButtonType.Menu) != 0)
                ButtonList.mainButton &= ~ButtonType.Menu;

            Open(WindowType.Shop);
            ButtonList.mainButton |= ButtonType.Shop;
        }
    }

    public void OnClickMission()
    {
        //Open(WindowType.Mission);
    }

    public void OnClickClose()
    {
        if (!first)
        {
            wayPoint = wayPoints[0].transform.position - wayPoints[1].transform.position;
            buttonPos = buttons.transform.position;
            first = true;
        }

        windows[(int)currentWindow].Close();

        if ((ButtonList.mainButton & ButtonType.Info) != 0)
            ButtonList.mainButton &= ~ButtonType.Info;

        if ((ButtonList.mainButton & ButtonType.DiningRoom) != 0)
            ButtonList.mainButton &= ~ButtonType.DiningRoom;

        if ((ButtonList.mainButton & ButtonType.Lab) != 0)
            ButtonList.mainButton &= ~ButtonType.Lab;

        if ((ButtonList.mainButton & ButtonType.Cleaning) != 0)
            ButtonList.mainButton &= ~ButtonType.Cleaning;

        if ((ButtonList.mainButton & ButtonType.Failure) != 0)
            ButtonList.mainButton &= ~ButtonType.Failure;

        if ((ButtonList.mainButton & ButtonType.Menu) != 0)
            ButtonList.mainButton &= ~ButtonType.Menu;

        if ((ButtonList.mainButton & ButtonType.Shop) != 0)
            ButtonList.mainButton &= ~ButtonType.Shop;

        CloseMainMenu();
        SkillButtonOn();
    }

    public void CloseTrain()
    {
        if (!first)
        {
            wayPoint = wayPoints[0].transform.position - wayPoints[1].transform.position;
            buttonPos = buttons.transform.position;
            first = true;
        }

        if ((ButtonList.mainButton & ButtonType.Train) != 0 &&
            (ButtonList.mainButton & ButtonType.TrainMove) == 0)
        {
            ButtonList.mainButton |= ButtonType.TrainMove;
            OnClickClose();
        }
        else if ((ButtonList.mainButton & ButtonType.TrainMove) == 0)
        {
            ButtonList.mainButton |= ButtonType.TrainMove;
            OpenMainMenu();
        }
    }
    public void SkillButtonOn()
    {
        skill.Setting();

        foreach (var button in skill.slotButtons)
        {
            button.gameObject.SetActive(true);
        }
        skill.slotButtons[6].SetActive(false);
    }

    public void SkillButtonOff()
    {
        skill.Setting();

        foreach (var button in skill.slotButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void OpenMainMenu()
    {
        //if (!menu && move == null)
        //{
        //    foreach (var button in windowButtons)
        //    {
        //        button.GetComponent<Button>().interactable = false;
        //    }

        //    move = StartCoroutine(RightMove());

        //    menu = true;
        //}
        if ((ButtonList.mainButton & ButtonType.Train) == 0 &&
            (ButtonList.mainButton & ButtonType.TrainMove) != 0)
        {
            foreach (var button in windowButtons)
            {
                button.GetComponent<Button>().interactable = false;
            }
            move = StartCoroutine(RightMove());
        }
    }

    public void CloseMainMenu()
    {
        //if (menu && move == null)
        //{
        //    foreach (var button in windowButtons)
        //    {
        //        button.GetComponent<Button>().interactable = false;
        //    }
        //    move = StartCoroutine(LeftMove());
        //    menu = false;
        //}
        if ((ButtonList.mainButton & ButtonType.Train) != 0 &&
            (ButtonList.mainButton & ButtonType.TrainMove) != 0)
        {
            foreach (var button in windowButtons)
            {
                button.GetComponent<Button>().interactable = false;
            }
            move = StartCoroutine(LeftMove()); // 메뉴 접기
        }
    }

    //12.18 윤유림 웨이브 반복 버튼
    public void SetActiveRepeatButton(bool isActive)
    {
        RepeatButton.SetActive(isActive);
        //WaveManager.Instance.SetRepeat(isActive);
    }

    public void SetTimerSlierValue(float value)
    {
        timerSlider.value = value;
    }

    public void SetTimerText(string text)
    {
        timerText.text = text;
    }

    public void SetCurrentStageText(int chapter, int stage, int wave)
    {
        string newTxt = $"{chapter} - {stage} - {wave}";
        stageText.SetText(newTxt);
    }

    public void SetWavePanelClear(int index, bool clear) //clear: 웨이브 클리어, false: 웨이브 클리어x
    {
        WavePanels[index].transform.GetChild(0).gameObject.SetActive(clear); //clear wave
        WavePanels[index].transform.GetChild(1).gameObject.SetActive(!clear); //unclearwave
        WavePanels[index].transform.GetChild(2).gameObject.SetActive(false); //playerLocation
    }

    public void SetWavePanelPlayer(int index)
    {
        WavePanels[index].transform.GetChild(2).gameObject.SetActive(true);
    }

    public void SetHpGauge(double hp)
    {
        HpGauge.fillAmount = (float)hp;
    }

    IEnumerator LeftMove()
    {
        float timer = 0f;

        while (timer < 1f)
        {
            buttons.transform.position = Vector3.Lerp(buttonPos - wayPoint, buttonPos, timer);
            timer += Time.deltaTime * 2f;
            yield return null;
        }

        foreach (var button in windowButtons)
        {
            button.GetComponent<Button>().interactable = true;
        }
        SkillButtonOn();

        buttons.transform.position = buttonPos;

        move = null;

        ButtonList.mainButton &= ~ButtonType.Train;
        ButtonList.mainButton &= ~ButtonType.TrainMove;
    }
    IEnumerator RightMove()
    {
        float timer = 0f;

        SkillButtonOff();

        while (timer < 1f)
        {
            buttons.transform.position = Vector3.Lerp(buttonPos, buttonPos - wayPoint, timer);
            timer += Time.deltaTime * 2f;
            yield return null;
        }

        foreach (var button in windowButtons)
        {
            button.GetComponent<Button>().interactable = true;
        }

        buttons.transform.position = buttonPos - wayPoint;

        move = null;

        ButtonList.mainButton |= ButtonType.Train;
        ButtonList.mainButton &= ~ButtonType.TrainMove;
    }

    public void SetGameOverPopUpActive(bool isActive)
    {
        gameOverPop.SetActive(isActive);
    }

    //12.20 Lsw
    public void PrintSliverMoney()
    {
        sliverMoney.text = $"{UnitConverter.OutString(CurrencyManager.money1)}";
    }
    public void PrintPommeMoney()
    {
        pommeMoney.text = $"{UnitConverter.OutString(CurrencyManager.money2)}";
    }
    public void PrintSoulMoney()
    {
        soulMoney.text = $"{UnitConverter.OutString(CurrencyManager.money3)}";
    }
    public void PrintMemoryMoney()
    {
        memoryMoney.text = $"{UnitConverter.OutString(CurrencyManager.itemAsh)}";
    }
    //12.20 Lsw


    //12.21 YYL start
    public void OnClickAutoSkillButton()
    {
        if (ButtonList.mainButton == ButtonType.Main)
        {
            autoSkill[0].SetActive(!isAuto);
            autoSkill[1].SetActive(isAuto);

            isAuto = !isAuto;

            SkillManager.Instance.SetIsAuto(isAuto);
        }
    }

    public void SetAutoSkillButton(bool isA)
    {
        autoSkill[0].SetActive(isA);
        autoSkill[1].SetActive(!isA);

        SkillManager.Instance.SetIsAuto(isA);
    }
    //12.21 YYL end

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if ((ButtonList.mainButton == ButtonType.Main))
            {
                ButtonList.mainButton|=ButtonType.Exit;
                Open(WindowType.Exit);
            }
            else if ((ButtonList.mainButton & ButtonType.Exit) != 0)
            {
                ButtonList.mainButton &= ~ButtonType.Exit;
                windows[(int)currentWindow].Close();
            }
            else if ((ButtonList.mainButton & ButtonType.Info) != 0)
            {
                if (ButtonList.infoButton == InfoButton.State)
                {
                    ButtonList.mainButton &= ~ButtonType.Info;
                    windows[(int)currentWindow].Close();
                }
                else if ((ButtonList.infoButton & InfoButton.Inventory) != 0 &&
                    (ButtonList.infoButton & InfoButton.WeaponInfo) == 0 &&
                    (ButtonList.infoButton & InfoButton.ArmorInfo) == 0 &&
                    (ButtonList.infoButton & InfoButton.RingInfo) == 0 &&
                    (ButtonList.infoButton & InfoButton.SymbolInfo) == 0)
                {
                    ButtonList.mainButton &= ~ButtonType.Info;
                    windows[(int)currentWindow].Close();
                }
                else if ((ButtonList.infoButton & InfoButton.Skill) != 0 &&
                    (ButtonList.infoButton & InfoButton.SkillInfo) == 0)
                {
                    ButtonList.mainButton &= ~ButtonType.Info;
                    windows[(int)currentWindow].Close();
                }
                else if (ButtonList.infoButton == InfoButton.Pet)
                {
                    ButtonList.mainButton &= ~ButtonType.Info;
                    windows[(int)currentWindow].Close();
                }
            }
            else if ((ButtonList.mainButton & ButtonType.DiningRoom) != 0)
            {
                ButtonList.mainButton &= ~ButtonType.DiningRoom;
                windows[(int)currentWindow].Close();
            }
            else if ((ButtonList.mainButton & ButtonType.Lab) != 0)
            {
                ButtonList.mainButton &= ~ButtonType.Lab;
                windows[(int)currentWindow].Close();
            }
            else if ((ButtonList.mainButton & ButtonType.Cleaning) != 0)
            {
                ButtonList.mainButton &= ~ButtonType.Cleaning;
                windows[(int)currentWindow].Close();
            }
            else if ((ButtonList.mainButton & ButtonType.Failure) != 0)
            {
                ButtonList.mainButton &= ~ButtonType.Failure;
                windows[(int)currentWindow].Close();
            }
            else if ((ButtonList.mainButton & ButtonType.Shop) != 0)
            {
                ButtonList.mainButton &= ~ButtonType.Shop;
                windows[(int)currentWindow].Close();
            }
            else if ((ButtonList.mainButton & ButtonType.Menu) != 0 &&
                ButtonList.settingButton == SettingButton.None)
            {
                ButtonList.mainButton &= ~ButtonType.Menu;
                windows[(int)currentWindow].Close();
            }
        }
    }

    public void BangchiOpen()
    {
        if (!isBangchi)
        {
            isBangchi = true;
            GameData.GetAccumulateOfflineEarnings();
            if (GameData.GetBanchiCompensationTime())
            {
                bangchiWindow.Open();
            }
        }
    }

    public void OpenPlayerBuffInfo()
    {
        if (PlayerBuff.Instance.buffData.isEatFood)
        {
            playerBuffWindow.Open();
        }
    }
    public void ClosePlayerBuffInfo()
    {
        playerBuffWindow.Close();
    }
}
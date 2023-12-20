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
    //12.14 이승우 퀘스트 오브젝트 가져오기
    public QuestSystemUi questSystemUi;

    //12.18 윤유림 웨이브 반복 버튼
    public GameObject RepeatButton;
    public Slider timerSlider;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI stageText;

    public List<GameObject> WavePanels;

    public GameObject gameOverPop;
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

        OpenMainMenu();
        SkillButtonOff();
    }

    public void OnClickInfo()
    {
        Open(WindowType.Info);
    }
    public void OnClickDiningRoom()
    {
        Open(WindowType.DiningRoom);
    }

    public void OnClickLab()
    {
        Open(WindowType.Lab);
    }

    public void OnClickCleaning()
    {
        Open(WindowType.Cleaning);
    }

    public void OnClickBossRush()
    {
        Open(WindowType.BossRush);
    }

    public void OnClickMenu()
    {
        Open(WindowType.Menu);
    }

    public void OnClickShop()
    {
        Open(WindowType.Shop);
    }

    public void OnClickMission()
    {
        Open(WindowType.Mission);
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

        CloseMainMenu();
        SkillButtonOn();
    }

    public void CloseTrain()
    {
        OnClickClose();

        OpenMainMenu();
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
        if (!menu && move == null)
        {
            foreach (var button in windowButtons)
            {
                button.GetComponent<Button>().interactable = false;
            }

            move = StartCoroutine(RightMove());

            menu = true;
        }
    }

    public void CloseMainMenu()
    {
        if (menu && move == null)
        {
            foreach (var button in windowButtons)
            {
                button.GetComponent<Button>().interactable = false;
            }

            move = StartCoroutine(LeftMove());

            menu = false;
        }
    }

    //12.18 윤유림 웨이브 반복 버튼
    public void SetActiveRepeatButton(bool isActive)
    {
        RepeatButton.SetActive(isActive);
    }

    public void SetTimerSlierValue(float value)
    {
        timerSlider.value = value;
    }

    public void SetTimerText(string text)
    {
        timerText.text = text;
    }

    public void SetCurrentStageText(int stage, int wave)
    {
        string newTxt = $"{stage} - {wave}";
        stageText.SetText(newTxt);
    }

    public void SetWavePanelClear(int index, bool clear) //clear: 웨이브 클리어, false: 웨이브 클리어x
    {
        //WavePanels[index].transform.GetChild(0).gameObject.SetActive(clear); //clear wave
        //WavePanels[index].transform.GetChild(1).gameObject.SetActive(!clear); //unclearwave
        //WavePanels[index].transform.GetChild(2).gameObject.SetActive(false); //playerLocation
    }

    public void SetWavePanelPlayer(int index)
    {
        //WavePanels[index].transform.GetChild(2).gameObject.SetActive(true);
    }

    IEnumerator LeftMove()
    {
        float timer = 0f;

        while (timer<1f)
        {
            buttons.transform.position = Vector3.Lerp(buttonPos - wayPoint, buttonPos, timer);
            timer += Time.deltaTime*2f;
            yield return null;
        }

        foreach (var button in windowButtons)
        {
            button.GetComponent<Button>().interactable = true;
        }
        SkillButtonOn();

        buttons.transform.position = buttonPos;
        move = null;
    }
    IEnumerator RightMove() 
    {
        float timer = 0f;

        SkillButtonOff();

        while (timer < 1f)
        {
            buttons.transform.position = Vector3.Lerp(buttonPos, buttonPos - wayPoint, timer);
            timer += Time.deltaTime*2f;
            yield return null;
        }

        foreach (var button in windowButtons)
        {
            button.GetComponent<Button>().interactable = true;
        }

        buttons.transform.position = buttonPos - wayPoint;
        move = null;
    }

    public void SetGameOverPopUpActive(bool isActive)
    {
        gameOverPop.SetActive(isActive);
    }
}
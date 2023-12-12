using System.Collections;
using System.Collections.Generic;
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

    private SkillWindow skill;

    public void Open(WindowType inventoryType)
    {
        if (windows[(int)currentWindow].gameObject.activeSelf)
            windows[(int)currentWindow].Close();

        currentWindow = inventoryType;

        windows[(int)inventoryType].Open();
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

    public void OnClickClose()
    {
        if (!first)
        {
            wayPoint = wayPoints[0].transform.position - wayPoints[1].transform.position;
            buttonPos = buttons.transform.position;
            skill = windows[(int)WindowType.Info].GetComponent<InfoWindow>().inventorys[2].GetComponent<SkillWindow>();
            first = true;
        }

        windows[(int)currentWindow].Close();

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

    public void CloseTrain()
    {
        OnClickClose();

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
        foreach (var button in skill.equipButtons)
        {
            button.gameObject.SetActive(true);
        }
        buttons.transform.position = buttonPos;
        move = null;
    }
    IEnumerator RightMove() 
    {
        float timer = 0f;

        foreach (var button in skill.equipButtons)
        {
            button.gameObject.SetActive(false);
        }

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
}
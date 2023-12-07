using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Window[] windows;

    public GameObject panel;

    public WindowType currentWindow;

    public List<Button> windowButtons = new List<Button>();

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
        windows[(int)currentWindow].Close();
    }
}
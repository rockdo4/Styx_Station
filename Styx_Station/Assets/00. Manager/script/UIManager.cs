using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Window[] windows;

    public GameObject panel;

    public WindowType currentWindow;

    public Button inventory;
    public Button custom;
    public Button State;
    public Button skill;

    public void Open(WindowType inventoryType)
    {
        if (windows[(int)currentWindow].gameObject.activeSelf)
            windows[(int)currentWindow].Close();

        currentWindow = inventoryType;

        panel.SetActive(true);
        windows[(int)inventoryType].Open();
    }

    public void OnClickInventory()
    {
        Open(WindowType.Inventory);
    }

    public void OnClickCustom()
    {
        Open(WindowType.Custom);
    }

    public void OnClickState()
    {
        Open(WindowType.State);
    }

    public void OnClickSkill()
    {
        Open(WindowType.Skill);
    }

    public void OnClickClose()
    {
        panel.SetActive(false);
        windows[(int)currentWindow].Close();
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : Window
{
    public Button[] equipButtons = new Button[4];

    public TextMeshProUGUI state;

    public SubWindow[] inventorys;

    public InfoWindowType currentSubWindow;

    public List<Button> tabs = new List<Button>();

    public Button exitButton;

    public override void Open()
    {
        Open(InfoWindowType.State);
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void Open(InfoWindowType subType)
    {
        if (inventorys[(int)currentSubWindow].gameObject.activeSelf)
            inventorys[(int)currentSubWindow].Close();

        currentSubWindow = subType;

        inventorys[(int)subType].Open();
    }

    public void OnClickState()
    {
        Open(InfoWindowType.State);
    }

    public void OnClickSkill()
    {
        Open(InfoWindowType.Skill);
    }

    public void OnClickPet()
    {
        Open(InfoWindowType.Pet);
    }
}

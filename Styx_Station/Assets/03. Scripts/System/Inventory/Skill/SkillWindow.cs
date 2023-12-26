using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindow : SubWindow
{
    private SkillInventory inventory;

    public bool equipMode;
    public int selectIndex;

    public GameObject info;

    public List<GameObject> slotButtons = new List<GameObject>();
    public List<Button> equipButtons = new List<Button>();
    public List<Button> skillButtons = new List<Button>();

    private bool first = false;

    public override void Open()
    {
        selectIndex = -1;
        equipMode = false;

        base.Open();

        for (int i = 0; i < skillButtons.Count; ++i)
        {
            var button = skillButtons[i].GetComponent<SkillButton>();
            if (!inventory.skills[i].acquire)
            {
                Color currentColor = button.image.GetComponent<Image>().color;
                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0.3f);
                button.image.GetComponent<Image>().color = newColor;
            }
            else
            {
                Color currentColor = button.image.GetComponent<Image>().color;
                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
                button.image.GetComponent<Image>().color = newColor;
            }
        }
    }

    public override void Close()
    {
        OnClickCloseIfno();

        base.Close();
    }


    public void Setting()
    {
        if (!first)
        {
            inventory = InventorySystem.Instance.skillInventory;
            info.GetComponent<SkillInfoUi>().Inventory();
            equipMode = false;

            for (int i = 0; i < skillButtons.Count; ++i)
            {
                var button = skillButtons[i].GetComponent<SkillButton>();
                if (button == null)
                    continue;

                button.skillIndex = i;
                button.inventory = inventory;
                button.image = button.transform.GetChild(0).gameObject;
                skillButtons[i].onClick.AddListener(() => button.OnClickOpenInfo(this));
            }
            for(int i = 0; i<equipButtons.Count; ++i)
            {
                var button = equipButtons[i].GetComponent<NormalButton>();
                if (button == null) 
                    continue;

                button.skillIndex = -1;
                button.equipIndex = i;
                button.inventory = inventory;
                button.Setting();
                equipButtons[i].onClick.AddListener(() => button.OnClickActive(this));
                equipButtons[i].onClick.AddListener(() => button.OnClickEquip(this));
            }
            first = true;
        }
    }

    public void WindowUpdate()
    {
        ButtonInteractable();

        foreach (var button in equipButtons)
        {
            var ui = button.GetComponent<NormalButton>();
            button.interactable = true;
            if(ui.skillIndex > -1)
                AlphaChange(button, true);
            else
                AlphaChange(button, false);

            ui.UiUpdate();
        }
    }
    public void ButtonInteractable()
    {
        foreach(var skill in equipButtons)
        {
            skill.interactable = true;
        }
        info.GetComponent<SkillInfoUi>().equip.interactable = true;
    }

    public void OnClickCloseIfno()
    {
        selectIndex = -1;
        equipMode = false;
        info.SetActive(false);
        ButtonInteractable();

        if ((ButtonList.infoButton & InfoButton.SkillInfo) != 0)
            ButtonList.infoButton &= ~InfoButton.SkillInfo;
    }

    public void OnClickEquip()
    {
        if (selectIndex < 0)
            return;

        equipMode = true;
        foreach(var slot in slotButtons)
        {
            slot.gameObject.SetActive(true);
        }
    }

    public void OnClickEquipClose()
    {
        equipMode = false;
        foreach (var slot in slotButtons)
        {
            slot.gameObject.SetActive(false);
        }
    }
    public void AlphaChange(Button button, bool value)
    {
        Color color = new Color();
        switch (value)
        {
            case true:
                {
                    color = new Color(1f, 1f, 1f, 1f);
                    button.GetComponent<NormalButton>().skillImage.GetComponent<Image>().color = color;
                }
                break;

            case false:
                {
                    color = new Color(1f, 1f, 1f, 0f);
                    button.GetComponent<NormalButton>().skillImage.GetComponent<Image>().color = color;
                }
                break;
        }
    }
}

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

    public List<Button> equipButtons = new List<Button>();
    public List<Button> skillButtons = new List<Button>();

    public override void Open()
    {
        selectIndex = -1;
        equipMode = false;

        EquipSkillUpdate();

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


    private void Awake()
    {
        inventory = InventorySystem.Instance.skillInventory;
        info.GetComponent<SkillInfoUi>().Inventory();
        equipMode = false;

        for(int i = 0; i<skillButtons.Count;++i)
        { 
            var button = skillButtons[i].GetComponent<SkillButton>();
            if (button == null)
                continue;

            button.skillIndex = i;
            button.inventory = inventory;
            button.image = button.transform.GetChild(0).gameObject;
            skillButtons[i].onClick.AddListener(()=>button.OnClickOpenInfo(this));
        }
    }

    private void EquipSkillUpdate()
    {
    }

    public void WindowUpdate()
    {
        ButtonInteractable();

        foreach (var button in equipButtons)
        {
            var ui = button.GetComponent<NormalButton>();
            button.interactable = true;
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
    }

    public void OnClickEquip()
    {
        if (selectIndex < 0)
            return;

        equipMode = true;
    }
}

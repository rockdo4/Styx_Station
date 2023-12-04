using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindow : Window
{
    private SkillInventory inventory;

    public bool equipMode;
    public int selectIndex;

    public GameObject equips;
    public GameObject skillTabs;
    public GameObject skills;
    public GameObject info;

    public Button equipPrefabs;
    public Button skillPrefabs;

    public List<Button> equipButtons {  get; private set; } = new List<Button>();
    public List<Button> skillButtons { get; private set; } = new List<Button>();

    public override void Open()
    {
        selectIndex = -1;
        equipMode = false;
        EquipSkillUpdate();

        base.Open();
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
        Setting();
    }

    private void EquipSkillUpdate()
    {
        for (int i = 0; i < equipButtons.Count; ++i)
        {
            var equipUi = equipButtons[i].GetComponent<NormalButton>();
            var equipSkill = inventory.equipSkills[i];
            if (equipSkill != null)
            {
                equipUi.skillIndex = equipSkill.skillIndex;
                equipUi.skillName.text = equipSkill.skill.Skill_Name;
            }
            else
            {
                equipUi.skillIndex = -1;
                equipUi.skillName.text = "None";
            }
        }
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

    public void Setting()
    {
        EquipSkillButtonCreate();
        SkillButtonCreate();
    }

    private void EquipSkillButtonCreate()
    {
        int length = inventory.equipSkills.Length;
        int index = 0;

        if (length <= 0)
            return;

        if(length>= equipButtons.Count)
            index = equipButtons.Count;

        for(int i = index; i<length; ++i)
        {
            Button button = Instantiate(equipPrefabs, equips.transform);
            var buttonUi = button.GetComponent<NormalButton>();

            buttonUi.inventory = inventory;
            buttonUi.equipIndex = i;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => buttonUi.OnClickDequip(equipMode));
            button.onClick.AddListener(() => buttonUi.OnClickEquip(this));

            var equipSkill = inventory.equipSkills[i];
            if(equipSkill != null)
            {
                buttonUi.skillIndex = equipSkill.skillIndex;
                buttonUi.skillName.text = equipSkill.skill.Skill_Name;
            }
            else
            {
                buttonUi.skillIndex = -1;
                buttonUi.skillName.text = "None";
            }
            equipButtons.Add(button);
        }
    }
    private void SkillButtonCreate()
    {
        int length = inventory.skills.Count;
        int index = 0;

        if (length <= 0)
            return;

        if (length >= skillButtons.Count)
            index = skillButtons.Count;

        for (int i = index; i < length; ++i)
        {
            Button button = Instantiate(skillPrefabs, skills.transform);
            var buttonUi = button.GetComponent<SkillButton>();
            buttonUi.inventory = inventory;
            buttonUi.skillIndex = i;
            buttonUi.skillName.text = inventory.skills[i].skill.Skill_Name;
            button.onClick.AddListener(() => buttonUi.OnClickOpenInfo(this));
            skillButtons.Add(button);
        }
    }

    public void OnClickCloseIfno()
    {
        selectIndex = -1;
        equipMode = false;
        skillTabs.SetActive(true);
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

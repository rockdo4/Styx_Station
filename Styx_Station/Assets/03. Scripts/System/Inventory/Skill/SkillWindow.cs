using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindow : InventoryWindow
{
    private SkillInventory inventory;

    public bool equipMode;
    public int selectIndex;

    public GameObject normals;
    public GameObject chains;
    public GameObject skillTabs;
    public GameObject skills;
    public GameObject info;

    public Button normalPrefabs;
    public Button chainPrefabs;
    public Button skillPrefabs;

    public List<Button> normalButtons {  get; private set; } = new List<Button>();
    public List<Button> chainButtons { get; private set; } = new List<Button>();
    public List<Button> skillButtons { get; private set; } = new List<Button>();

    public override void Open()
    {
        selectIndex = -1;
        equipMode = false;

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

    public void WindowUpdate()
    {
        ButtonInteractable();

        foreach (var button in normalButtons)
        {
            var ui = button.GetComponent<NormalButton>();
            button.interactable = true;
            ui.UiUpdate();
        }

        foreach(var button in chainButtons)
        {
            var ui = button.GetComponent<ChainButton>();
            button.interactable = true;
            ui.UiUpdate();
        }
    }
    public void ButtonInteractable()
    {
        foreach(var skill in normalButtons)
        {
            skill.interactable = true;
        }

        foreach(var skill in chainButtons)
        {
            skill.interactable = true;
        }
        info.GetComponent<SkillInfoUi>().equip.interactable = true;
    }

    public void Setting()
    {
        NSkillButtonCreate();
        CSkillButtonCreate();
        SkillButtonCreate();
    }

    private void NSkillButtonCreate()
    {
        int length = inventory.equipSkills.Length;
        int index = 0;

        if (length <= 0)
            return;

        if(length>= normalButtons.Count)
            index = normalButtons.Count;

        for(int i = index; i<length; ++i)
        {
            Button button = Instantiate(normalPrefabs, normals.transform);
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
            normalButtons.Add(button);
        }
    }

    private void CSkillButtonCreate()
    {
        int length = inventory.chainSkills.Length;
        int index = 0;

        if (length <= 0)
            return;

        if(length >= chainButtons.Count)
            index = chainButtons.Count;

        for(int i = index; i<length; ++i)
        {
            Button button = Instantiate(chainPrefabs, chains.transform);
            var buttonUi = button.GetComponent<ChainButton>();

            buttonUi.inventory = inventory;
            buttonUi.equipIndex = i;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => buttonUi.OnClickDequip(equipMode));
            button.onClick.AddListener(() => buttonUi.OnClickEquip(this));

            var equipSkill = inventory.chainSkills[i];
            if (equipSkill != null)
            {
                buttonUi.skillIndex = equipSkill.skillIndex;
                buttonUi.skillName.text = equipSkill.skill.Skill_Name;
            }
            else
            {
                buttonUi.skillIndex = -1;
                buttonUi.skillName.text = "None";
            }
            chainButtons.Add(button);
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

        switch(inventory.skills[selectIndex].skill.Skill_Type_2)
        {
            case SkillType_2.Normal:
                foreach(var skill in chainButtons)
                {
                    skill.interactable = false;
                }
                break;
            case SkillType_2.Chain:
                foreach (var skill in normalButtons)
                {
                    skill.interactable = false;
                }
                break;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillInventory : MonoBehaviour
{
    [System.Serializable]
    public class InventorySKill
    {
        public Skill skill;
        public int upgradeLev;
        public bool acquire;
        public bool equip;
        public int stock;
        public int skillIndex;
        public int equipIndex;

        public InventorySKill(Skill skill, int upgradeLev, bool acquire, bool equip, int stock, int skillIndex, int equipIndex)
        {
            this.skill = skill;
            this.upgradeLev = upgradeLev;
            this.acquire = acquire;
            this.equip = equip;
            this.stock = stock;
            this.skillIndex = skillIndex;
            this.equipIndex = equipIndex;
        }
    }

    public List<InventorySKill> skills { get; set; } = new List<InventorySKill>();

    public InventorySKill[] equipSkills { get; private set; }  = new InventorySKill[5];
    public InventorySKill[] chainSkills { get; private set; } = new InventorySKill[5];

    public void SkillSorting()
    {
        for (int i = 0; i < skills.Count; ++i)
        {
            skills[i].skillIndex = i;
        }
    }

    public void AddSkill(Skill skill)
    {
        if (skill == null)
            return;

        var addSkill = skills.Where(x => x.skill.name == skill.name).FirstOrDefault();
        if (addSkill != null)
            return;

        skills.Add(new InventorySKill(skill, 0, true, false, 0, -1, -1));
    }

    public void Reset()
    {
        for (int i = 0; i < equipSkills.Length; ++i)
        {
            if (equipSkills[i] != null && equipSkills[i].skillIndex > -1)
            {
                DequipSkill(equipSkills[i].skillIndex, i, SkillType_2.Normal);
            }
            equipSkills[i] = null;
        }

        for (int i = 0; i < chainSkills.Length; ++i)
        {
            if (chainSkills[i] != null && chainSkills[i].skillIndex > -1)
            {
                DequipSkill(chainSkills[i].skillIndex, i, SkillType_2.Chain);
            }
            chainSkills[i] = null;
        }
    }

    public void EquipSkill(int skillIndex, int equipIndex, SkillType_2 skillType)
    {
        if (skillIndex < 0)
            return;

        if (equipIndex < 0)
            return;

        switch(skillType)
        {
            case SkillType_2.Normal:
                NormalEquip(skillIndex, equipIndex);
                break;
            case SkillType_2.Chain:
                ChainEquip(skillIndex, equipIndex);
                break;
        }
    }

    private void NormalEquip(int skillIndex, int equipIndex)
    {
        if (equipSkills == null)
            return;

        if (skills[skillIndex].equip)
            DequipSkill(skillIndex, skills[skillIndex].equipIndex, SkillType_2.Normal);

        if (equipSkills[equipIndex] == null || equipSkills[equipIndex].skill == null)
            equipSkills[equipIndex] = skills[skillIndex];

        else if (equipSkills[equipIndex] != null || equipSkills[equipIndex].skill != null)
        {
            equipSkills[equipIndex].equip = false;
            equipSkills[equipIndex].equipIndex = -1;
        }
        
        equipSkills[equipIndex] = skills[skillIndex];
        skills[skillIndex].equip = true;
        skills[skillIndex].equipIndex = equipIndex;
    }

    private void ChainEquip(int skillIndex, int equipIndex)
    {
        if (chainSkills == null)
            return;

        if (skills[skillIndex].equip)
            DequipSkill(skillIndex, skills[skillIndex].equipIndex, SkillType_2.Chain);

        if (chainSkills[equipIndex] == null || chainSkills[equipIndex].skill == null)
            chainSkills[equipIndex] = skills[skillIndex];

        else if (chainSkills[equipIndex] != null || chainSkills[equipIndex].skill != null)
        {
            equipSkills[equipIndex].equip = false;
            equipSkills[equipIndex].equipIndex = -1;
        }

        chainSkills[equipIndex] = skills[skillIndex];
        skills[skillIndex].equip = true;
        skills[skillIndex].equipIndex = equipIndex;
    }

    public void DequipSkill(int skillIndex, int equipIndex, SkillType_2 skillType)
    {
        if (skills == null)
            return;

        if (skillIndex < 0)
            return;

        if (equipIndex < 0)
            return;

        if (skills[skillIndex] == null)
            return;

        if (skills[skillIndex].skill == null)
            return;

        if (!skills[skillIndex].acquire)
            return;


        switch(skillType)
        {
            case SkillType_2.Normal:
                NormalDequip(skillIndex, equipIndex);
                break;
            case SkillType_2.Chain:
                ChainDequip(skillIndex, equipIndex);
                break;
        }
    }

    private void NormalDequip(int skillIndex, int equipIndex)
    {
        if (equipSkills[equipIndex] == null)
            return;

        if (equipSkills[equipIndex].skill == null)
            return;

        skills[skillIndex].equip = false;
        skills[skillIndex].equipIndex = -1;
        equipSkills[equipIndex] = null;
    }

    private void ChainDequip(int skillIndex, int equipIndex)
    {
        if (chainSkills[equipIndex] == null)
            return;

        if (chainSkills[equipIndex].skill == null)
            return;

        skills[skillIndex].equip = false;
        skills[skillIndex].equipIndex = -1;
        chainSkills[equipIndex] = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipSkill(0, 1, skills[0].skill.Skill_Type_2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DequipSkill(0, 1, skills[0].skill.Skill_Type_2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EquipSkill(0, 0, skills[0].skill.Skill_Type_2);
        }

        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            EquipSkill(1, 0, skills[1].skill.Skill_Type_2);
        }
    }
}

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

    public List<InventorySKill> skills { get; set; }  = new List<InventorySKill>();

    private InventorySKill[] equipSkills = new InventorySKill[6];

    public void SkillSorting()
    {
        for(int i = 0; i<skills.Count; ++i)
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

        skills.Add(new InventorySKill(skill, 0, false, false, 0, -1, -1));
    }

    public void Reset()
    {
        for(int i= 0; i< equipSkills.Length; ++i)
        {
            if (equipSkills[i] != null && equipSkills[i].skillIndex >-1)
            {
                DequipSkill(equipSkills[i].skillIndex, i, equipSkills[i].skill.Skill_Type);
            }
            equipSkills[i] = null;
        }
    }

    public void EquipSkill(int skillIndex, int equipIndex, SkillType skillType)
    {
        if (equipSkills == null)
            return;

        if (skillIndex < 0)
            return;


    }

    public void DequipSkill(int skillIndex, int equipIndex, SkillType skillType)
    {
        if (skills == null)
            return;

        if (skills[skillIndex] == null)
            return;

        if (skills[skillIndex].skill == null)
            return;

        if (!skills[skillIndex].acquire)
            return;

        if (equipSkills[equipIndex] == null)
            return;

        if (equipSkills[equipIndex].skill == null)
            return;

        skills[skillIndex].equip = false;
        skills[skillIndex].equipIndex = -1;
        equipSkills[equipIndex] = null;
    }
}

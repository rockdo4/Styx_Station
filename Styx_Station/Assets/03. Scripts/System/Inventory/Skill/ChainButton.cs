using TMPro;
using UnityEngine;

public class ChainButton : MonoBehaviour
{
    public SkillInventory inventory;

    public TextMeshProUGUI skillName;

    public int skillIndex;
    public int equipIndex;

    public void UiUpdate()
    {
        if (skillIndex < 0)
        {
            skillName.text = "None";
            return;
        }

        skillName.text = inventory.skills[skillIndex].skill.Skill_Name;
    }

    public void OnClickDequip(bool equipMode)
    {
        if (equipMode)
            return;

        inventory.DequipSkill(skillIndex, equipIndex, SkillType_2.Chain);

        skillIndex = -1;
        skillName.text = "None";
    }

    public void OnClickEquip(SkillWindow window)
    {
        if (!window.equipMode)
            return;

        if (window.selectIndex < 0)
            return;

        if (!inventory.skills[window.selectIndex].acquire)
            return;

        skillIndex = window.selectIndex;
        skillName.text = inventory.skills[skillIndex].skill.Skill_Name;

        window.equipMode = false;
        inventory.EquipSkill(skillIndex, equipIndex, SkillType_2.Chain);
        window.WindowUpdate();
    }
}

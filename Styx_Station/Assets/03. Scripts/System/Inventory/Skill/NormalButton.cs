using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NormalButton : MonoBehaviour
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

        inventory.DequipSkill(skillIndex, equipIndex, SkillType_2.Normal);

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

        if (inventory.skills[window.selectIndex].equip)
        {
            var index = inventory.skills[window.selectIndex].equipIndex;
            window.normalButtons[index].GetComponent<NormalButton>().skillIndex = -1;
        }

        skillIndex = window.selectIndex;
        skillName.text = inventory.skills[skillIndex].skill.Skill_Name;

        window.equipMode = false;
        inventory.EquipSkill(skillIndex, equipIndex, SkillType_2.Normal);
        window.WindowUpdate();
    }
}

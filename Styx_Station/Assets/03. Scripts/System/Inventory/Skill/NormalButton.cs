using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NormalButton : MonoBehaviour
{
    public SkillInventory inventory;

    public GameObject skillImage;

    public int skillIndex;
    public int equipIndex;

    public void UiUpdate()
    {

        if (skillIndex < 0)
        {
            skillImage.GetComponent<Image>().sprite = null;
            return;
        }

        skillImage.GetComponent<Image>().sprite = inventory.skills[skillIndex].skill.image;
    }
    public void OnClickDequip(bool equipMode)
    {
        if (equipMode)
            return;

        inventory.DequipSkill(skillIndex, equipIndex);

        skillIndex = -1;
        skillImage.GetComponent<Image>().sprite = null;
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
            window.equipButtons[index].GetComponent<NormalButton>().skillIndex = -1;
        }

        skillIndex = window.selectIndex;
        skillImage.GetComponent<Image>().sprite = inventory.skills[skillIndex].skill.image;

        window.equipMode = false;
        inventory.EquipSkill(skillIndex, equipIndex);
        window.WindowUpdate();
        window.OnClickEquipClose();
    }
}

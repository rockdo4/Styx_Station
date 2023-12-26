using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;


public class NormalButton : MonoBehaviour
{
    public SkillInventory inventory;
    private SkillManager skillManager;

    public GameObject skillImage;

    public int skillIndex;
    public int equipIndex;

    public Slider cool;

    public void UiUpdate()
    {

        if (skillIndex < 0)
        {
            skillImage.GetComponent<Image>().sprite = null;
            return;
        }

        skillImage.GetComponent<Image>().sprite = inventory.skills[skillIndex].skill.image;
    }

    public void Setting()
    {
        skillManager = SkillManager.Instance;
    }
    public void OnClickActive(SkillWindow window)
    {
        if (ButtonList.mainButton == ButtonType.Main)
        {

            if (window.equipMode)
                return;

            if (skillIndex < 0)
                return;

            switch (equipIndex)
            {
                case 0:
                    skillManager.UseSkill1(cool);
                    break;
                case 1:
                    skillManager.UseSkill2(cool);
                    break;
                case 2:
                    skillManager.UseSkill3(cool);
                    break;
                case 3:
                    skillManager.UseSkill4(cool);
                    break;
                case 4:
                    skillManager.UseSkill5(cool);
                    break;
                case 5:
                    skillManager.UseSkill6(cool);
                    break;
            }
        }
    }

    public void AutoSkillActive(SkillWindow window)
    {
        if (window.equipMode)
            return;

        if (skillIndex < 0)
            return;

        switch (equipIndex)
        {
            case 0:
                skillManager.UseSkill1(cool);
                break;
            case 1:
                skillManager.UseSkill2(cool);
                break;
            case 2:
                skillManager.UseSkill3(cool);
                break;
            case 3:
                skillManager.UseSkill4(cool);
                break;
            case 4:
                skillManager.UseSkill5(cool);
                break;
            case 5:
                skillManager.UseSkill6(cool);
                break;
        }
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
        skillManager.SetEquipSkillByIndex(equipIndex);
        window.info.GetComponent<SkillInfoUi>().InfoUpdate();
        window.WindowUpdate();
        window.OnClickEquipClose();
    }
}

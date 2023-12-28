using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillInventory inventory;

    public GameObject image;

    public int skillIndex;

    public void OnClickOpenInfo(SkillWindow window)
    {
        if (window == null)
            return;

        if ((ButtonList.infoButton & InfoButton.SkillInfo) != 0)
        {
            return;
        }

        ButtonList.infoButton |= InfoButton.SkillInfo;

        window.selectIndex = skillIndex;
        var info = window.info.GetComponent<SkillInfoUi>();
        Color color = new Color();
        switch (inventory.skills[skillIndex].skill.Skill_Tier)
        {
            case Tier.Common:
                {
                    color = new Color(0, 0, 0, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Uncommon:
                {
                    color = new Color(40f / 255f, 1f, 237f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Rare:
                {
                    color = new Color(1f, 0, 221 / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Unique:
                {
                    color = new Color(248f / 255f, 207f / 255f, 41f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Legendry:
                {
                    color = new Color(0, 1, 71f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;
        }
        info.skillName.color = color;
        info.tier.color = color;
        info.selectIndex = skillIndex;
        info.skillImage.GetComponent<Image>().sprite = inventory.skills[skillIndex].skill.image;

        info.InfoUpdate();

        if (inventory.skills[skillIndex].acquire)
            info.equip.interactable = true;

        else if (!inventory.skills[skillIndex].acquire)
            info.equip.interactable = false;

        if (inventory.skills[skillIndex].skill.Skill_Type == SkillType.Passive)
        {
            info.equip.interactable = false;
        }

        //if (skillIndex > 12)
        //{
        //    info.equip.interactable = false;
        //}

        window.info.SetActive(true);
    }
}

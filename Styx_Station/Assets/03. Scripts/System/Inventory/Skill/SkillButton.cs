using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillInventory inventory;

    public Sprite image;

    public int skillIndex;

    public void OnClickOpenInfo(SkillWindow window)
    {
        if (window == null)
            return;

        window.selectIndex = skillIndex;
        var info = window.info.GetComponent<SkillInfoUi>();
        Color color = new Color();
        switch (inventory.skills[skillIndex].skill.Skill_Tier)
        {
            case Tier.Common:
                {
                    color = new Color(137f / 255f, 126f / 255f, 126f / 255f, 128f / 255f);
                    info.skillImage.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Uncommon:
                {
                    color = new Color(0, 0, 0, 128f / 255f);
                    info.skillImage.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Rare:
                {
                    color = new Color(45f / 255f, 148f / 255f, 244f / 255f, 128f / 255f);
                    info.skillImage.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Unique:
                {
                    color = new Color(248f / 255f, 207f / 255f, 41f / 255f, 128f / 255f);
                    info.skillImage.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Legendry:
                {
                    color = new Color(0, 1, 71f / 255f, 128f / 255f);
                    info.skillImage.GetComponent<Outline>().effectColor = color;
                }
                break;
        }
        info.skillName.color = color;
        info.tier.color = color;
        info.selectIndex = skillIndex;
        info.skillImage.GetComponent<Image>().sprite = inventory.skills[skillIndex].skill.image;
        if (inventory.skills[skillIndex].skill.Skill_Type == SkillType.Passive)
        {
            info.equip.interactable = false;
        }

        info.InfoUpdate();
        window.info.SetActive(true);
    }
}

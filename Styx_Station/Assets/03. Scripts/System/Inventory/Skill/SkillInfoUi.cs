using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoUi : MonoBehaviour
{
    private SkillInventory inventory;
    private SkillWindow window;
    private StringTable stringTable;

    public int selectIndex;

    public GameObject skillImage;
    public GameObject outBox;

    public TextMeshProUGUI lev;
    public TextMeshProUGUI tier;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI coolTime;
    public TextMeshProUGUI skillText;
    public Button equip;
    public Button upgrade;

    private bool first = false;
    public void Inventory()
    {
        inventory = InventorySystem.Instance.skillInventory;
        window = UIManager.Instance.skill;
    }
    public void InfoUpdate()
    {
        var skill = inventory.skills[selectIndex];

        if (!first)
        {
            stringTable = MakeTableData.Instance.stringTable;
            first = true;
        }

        if (Global.language == Language.KOR)
        {
            tier.text = $"{stringTable.GetStringTableData(skill.skill.Skill_Tier.ToString()).KOR}";
            skillName.text = $"{stringTable.GetStringTableData(skill.skill.name + "_Name").KOR}";
            coolTime.text = $"{skill.skill.Skill_Cool} {stringTable.GetStringTableData("Playerskill001").KOR}";

            string text = string.Format(stringTable.GetStringTableData(skill.skill.name + "_Info").KOR,
             skill.skill.Skill_ATK + skill.upgradeLev * skill.skill.Skill_ATK_LVUP);
            skillText.text = $"{text}";
        }
        else if (Global.language == Language.ENG)
        {
            tier.text = $"{stringTable.GetStringTableData(skill.skill.Skill_Tier.ToString()).ENG}";
            skillName.text = $"{stringTable.GetStringTableData(skill.skill.name + "_Name").ENG}";
            coolTime.text = $"{skill.skill.Skill_Cool} {stringTable.GetStringTableData("Playerskill001").ENG}";
            string text = string.Format(stringTable.GetStringTableData(skill.skill.name + "_Info").ENG,
            skill.skill.Skill_ATK + skill.upgradeLev * skill.skill.Skill_ATK_LVUP);
            skillText.text = $"{text}";
        }

        if (skill.upgradeLev < skill.skill.Skill_LVUP_NU.Count)
            lev.text = $"Lv.{skill.upgradeLev}\n\n({skill.stock} / {skill.skill.Skill_LVUP_NU[skill.upgradeLev]})";
       
        else
            lev.text = $"Lv.{skill.upgradeLev}\n\n({skill.stock} / {skill.skill.Skill_LVUP_NU[skill.skill.Skill_LVUP_NU.Count-1]})";
    }

    public void OnClickEquip()
    {
        if (inventory.skills[selectIndex].equip)
        {
            var skill = inventory.skills[selectIndex];
            var index = skill.equipIndex;
            inventory.DequipSkill(selectIndex, index);
            SkillManager.Instance.SetDequipSkillByIndex(index);
            var button = UIManager.Instance.skill.equipButtons[index].gameObject.GetComponent<NormalButton>();
            button.skillIndex = -1;
            button.skillImage.GetComponent<Image>().sprite = null;
            window.AlphaChange(UIManager.Instance.skill.equipButtons[index], false);
        }
    }

    public void OnClickUpgrade()
    {
        gameObject.GetComponent<Upgrade>().SkillUpgrade(selectIndex);

        InfoUpdate();
    }


}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoUi : MonoBehaviour
{
    private SkillInventory inventory;

    public int selectIndex;

    public GameObject skillImage;
    public GameObject outBox;

    public TextMeshProUGUI lev;
    public TextMeshProUGUI tier;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI coolTime;
    public Button equip;
    public Button upgrade;

    public void Inventory()
    {
        inventory = InventorySystem.Instance.skillInventory;
    }
    public void InfoUpdate()
    {
        var skill = inventory.skills[selectIndex];

        if (skill.upgradeLev < skill.skill.Skill_LVUP_NU.Count)
            lev.text = $"Lv.{skill.upgradeLev}\n\n({skill.stock} / {skill.skill.Skill_LVUP_NU[skill.upgradeLev]})";
       
        else
            lev.text = $"Lv.{skill.upgradeLev}\n\n({skill.stock} / {skill.skill.Skill_LVUP_NU[skill.skill.Skill_LVUP_NU.Count-1]})";
    }

    public void OnClickUpgrade()
    {
        gameObject.GetComponent<Upgrade>().SkillUpgrade(selectIndex);

        InfoUpdate();
    }
}
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

    public TextMeshProUGUI lev;
    public TextMeshProUGUI tier;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI coolTime;
    public Outline outline;
    public Button equip;
    public Button upgrade;

    public void Inventory()
    {
        inventory = InventorySystem.Instance.skillInventory;
    }
    public void InfoUpdate()
    {
        var skill = inventory.skills[selectIndex];

        lev.text = $"Lv.{skill.upgradeLev}\n\n({skill.stock} / {skill.skill.Skill_LVUP_NU[skill.upgradeLev]})";
    }
}

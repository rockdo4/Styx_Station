using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoUi : MonoBehaviour
{
    private SkillInventory inventory;

    public int selectIndex;

    public TextMeshProUGUI lev;
    public TextMeshProUGUI exp;

    public Button equip;
    public Button upgrade;

    public void Inventory()
    {
        inventory = InventorySystem.Instance.skillInventory;
    }
    public void InfoUpdate()
    {
        var skill = inventory.skills[selectIndex];

        lev.text = $"Lv.{skill.upgradeLev}";
        exp.text = $"({skill.stock} / {skill.skill.Skill_LVUP_NU[skill.upgradeLev]})";
    }
}

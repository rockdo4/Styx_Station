using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillInventory;

public class SkillManager : MonoBehaviour
{
    SkillInventory inventory;
    public InventorySKill[] equipSkills;

    public List<SkillBase> skills; //��ų �κ��丮�� skill index�� index ���߱�

    private void Awake()
    {
        inventory = InventorySystem.Instance.skillInventory;
        equipSkills = inventory.equipSkills;
    }

    private void Start()
    {
        skills.Add(new TripleShot(inventory.skills[0]));
    }

}

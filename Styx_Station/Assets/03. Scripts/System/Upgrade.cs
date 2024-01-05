using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Progress;

public class Upgrade : MonoBehaviour
{
    private Inventory itemInventory;

    private SkillInventory skillInventory;

    private PetInventory petInventory;

    private StateSystem stateSystem;

    private void Awake()
    {
        itemInventory = InventorySystem.Instance.inventory;
        skillInventory = InventorySystem.Instance.skillInventory;
        petInventory = InventorySystem.Instance.petInventory;
        stateSystem = StateSystem.Instance;
    }
    public void ItemUpgrade(int index, ItemType type)
    {
        switch(type)
        {
            case ItemType.Weapon:
                WeaponUpgrade(index);
                break;
            case ItemType.Armor:
                ArmorUpgrade(index);
                break;
            case ItemType.Ring:
                RingUpgrade(index);
                break;
            case ItemType.Symbol:
                SymbolUpgrade(index);
                break;
        }
    }

    private void WeaponUpgrade(int index)
    {
        var item = itemInventory.weapons[index];
        
        if (item == null)
            return;

        if (item.upgradeLev >= 100)
            return;

        int num = 0;

        if (item.item.itemLevUpNum.Count <= item.upgradeLev)
            num = item.item.itemLevUpNum[item.item.itemLevUpNum.Count - 1];

        if (item.item.itemLevUpNum.Count > item.upgradeLev)
            num = item.item.itemLevUpNum[item.upgradeLev];

        if (item.stock < num)
            return;

        item.stock -= num;

        item.upgradeLev += 1;
        UIManager.Instance.questSystemUi.UpgradeQuestSet((int)UpgradeType.Weapon);
        stateSystem.EquipUpdate();
        stateSystem.AcquireUpdate();
        stateSystem.TotalUpdate();
    }

    private void ArmorUpgrade(int index)
    {
        var item = itemInventory.armors[index];

        if (item == null)
            return;

        if (item.upgradeLev >= 100)
            return;

        int num = 0;

        if (item.item.itemLevUpNum.Count <= item.upgradeLev)
            num = item.item.itemLevUpNum[item.item.itemLevUpNum.Count - 1];

        if (item.item.itemLevUpNum.Count > item.upgradeLev)
            num = item.item.itemLevUpNum[item.upgradeLev];

        if (item.stock < num)
            return;

        item.stock -= num;

        item.upgradeLev += 1;
        UIManager.Instance.questSystemUi.UpgradeQuestSet((int)UpgradeType.Weapon);
        stateSystem.EquipUpdate();
        stateSystem.AcquireUpdate();
        stateSystem.TotalUpdate();
    }

    private void RingUpgrade(int index)
    {
        var item = itemInventory.customRings[index].item;

        if (item == null)
            return;

        if (item.upgradeLev >= 100)
            return;

        int num = 0;

        if (item.item.itemLevUpNum.Count <= item.upgradeLev)
            num = item.item.itemLevUpNum[item.item.itemLevUpNum.Count - 1];

        if (item.item.itemLevUpNum.Count > item.upgradeLev)
            num = item.item.itemLevUpNum[item.upgradeLev];


        if (CurrencyManager.itemAsh < num)
            return;

        CurrencyManager.itemAsh -= num;

        item.upgradeLev += 1;
        UIManager.Instance.questSystemUi.UpgradeQuestSet((int)UpgradeType.Weapon);
        stateSystem.EquipUpdate();
        stateSystem.TotalUpdate();
    }

    private void SymbolUpgrade(int index)
    {
        var item = itemInventory.customSymbols[index].item;

        if (item == null)
            return;

        if (item.upgradeLev >= 100)
            return;

        int num = 0;

        if (item.item.itemLevUpNum.Count <= item.upgradeLev)
            num = item.item.itemLevUpNum[item.item.itemLevUpNum.Count - 1];

        if (item.item.itemLevUpNum.Count > item.upgradeLev)
            num = item.item.itemLevUpNum[item.upgradeLev];

        if (CurrencyManager.itemAsh < num)
            return;

        CurrencyManager.itemAsh -= num;

        item.upgradeLev += 1;
        UIManager.Instance.questSystemUi.UpgradeQuestSet((int)UpgradeType.Weapon);
        stateSystem.EquipUpdate();
        stateSystem.TotalUpdate();
    }

    public void SkillUpgrade(int index)
    {
        var skill = skillInventory.skills[index];
        if (skill == null) return;


        if (skill.upgradeLev >= 100)
            return;

        int num = 0;

        if(skill.skill.Skill_LVUP_NU.Count<=skill.upgradeLev)
            num = skill.skill.Skill_LVUP_NU[skill.skill.Skill_LVUP_NU.Count-1];

        if (skill.skill.Skill_LVUP_NU.Count > skill.upgradeLev)
            num = skill.skill.Skill_LVUP_NU[skill.upgradeLev];

        if (skill.stock < num)
            return;

        skill.stock -= num;

        skill.upgradeLev += 1;
        UIManager.Instance.questSystemUi.UpgradeQuestSet((int)UpgradeType.Skill);
        stateSystem.SkillUpdate();
        stateSystem.TotalUpdate();
    }

    public void PetUpgrade(int index)
    {
        var pet = petInventory.pets[index];

        if(pet == null) return;

        if (pet.upgradeLev >= 100)
            return;

        int num = 0;

        if (pet.pet.Pet_UpMatter.Count<=pet.upgradeLev)
            num = pet.pet.Pet_UpMatter[pet.pet.Pet_UpMatter.Count - 1];

        if (pet.pet.Pet_UpMatter.Count > pet.upgradeLev)
            num = pet.pet.Pet_UpMatter[pet.upgradeLev];

        if (pet.stock < num)
            return;

        pet.stock -= num;

        pet.upgradeLev += 1;
    }
}

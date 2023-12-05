using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public Inventory itemInventory;

    public SkillInventory skillInventory;

    public PetInventory petInventory;

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

        if (item.stock < item.item.itemLevUpNum[item.upgradeLev])
            return;

        item.stock -= item.item.itemLevUpNum[item.upgradeLev];

        item.upgradeLev += 1;
    }

    private void ArmorUpgrade(int index)
    {
        var item = itemInventory.armors[index];

        if (item == null)
            return;

        if (item.stock < item.item.itemLevUpNum[item.upgradeLev])
            return;

        item.stock -= item.item.itemLevUpNum[item.upgradeLev];

        item.upgradeLev += 1;
    }

    public int CustomUpgradeWeight(Inventory.InventoryItem item)
    {
        int weight = 0;

        switch (item.upgradeLev / 10)
        {
            case 0:
                weight = 5 * item.upgradeLev;
                break;
            case 1:
                weight = 10 * item.upgradeLev;
                break;
            case 2:
                weight = 20 * item.upgradeLev;
                break;
            case 3:
                weight = 40 * item.upgradeLev;
                break;
            case 4:
                weight = 80 * item.upgradeLev;
                break;
            case 5:
                weight = 160 * item.upgradeLev;
                break;
            case 6:
                weight = 320 * item.upgradeLev;
                break;
            case 7:
                weight = 640 * item.upgradeLev;
                break;
            case 8:
                weight = 1280 * item.upgradeLev;
                break;
            case 9:
                weight = 2560 * item.upgradeLev;
                break;

            default:
                weight = 2560 * item.upgradeLev;
                break;
        }

        return weight;
    }

    private void RingUpgrade(int index)
    {
        var item = itemInventory.customRings[index].item;

        if (item == null)
            return;

        int weight = CustomUpgradeWeight(item); 

        if (CurrencyManager.itemAsh < item.item.itemLevUpNum[item.upgradeLev] + weight)
            return;

        CurrencyManager.itemAsh -= item.item.itemLevUpNum[item.upgradeLev] + weight;

        item.upgradeLev += 1;
    }

    private void SymbolUpgrade(int index)
    {
        var item = itemInventory.customSymbols[index].item;

        if (item == null)
            return;

        int weight = CustomUpgradeWeight(item);

        if (CurrencyManager.itemAsh < item.item.itemLevUpNum[item.upgradeLev]+weight)
            return;

        CurrencyManager.itemAsh -= item.item.itemLevUpNum[item.upgradeLev]+weight;

        item.upgradeLev += 1;
    }

    public void SkillUpgrade(int index)
    {
        var skill = skillInventory.skills[index];

        if (skill == null) return;

        if (skill.stock < skill.skill.Skill_LVUP_NU[skill.upgradeLev])
            return;

        skill.stock -= skill.skill.Skill_LVUP_NU[skill.upgradeLev];

        skill.upgradeLev += 1;
    }

    public void PetUpgrade(int index)
    {
        var pet = petInventory.pets[index];

        if(pet == null) return;

        if (pet.stock < pet.pet.Pet_UpMatter[pet.upgradeLev])
            return;

        pet.stock -= pet.pet.Pet_UpMatter[pet.upgradeLev];

        pet.upgradeLev += 1;
    }
}

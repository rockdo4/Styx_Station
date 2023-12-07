using System.Linq;
using UnityEngine;

public class ShopSystem : Singleton<ShopSystem>
{
    private ItemDropTable itemTable;
    private SkillDropTable skillTable;
    private PetDropTable petTable;

    public int currentItemRank = 0;
    public int currentItemRankUp;
    public int currentSkillRank;
    public int currentSkillRankUp;
    public int currentPetRank;
    public int currentPetRankUp;

    private Inventory inventory;
    private SkillInventory skillInventory;
    private PetInventory petInventory;

    private void Awake()
    {
        itemTable = Resources.Load<ItemDropTable>("Table/GachaTable_Equip");

        skillTable = Resources.Load<SkillDropTable>("Table/SkillDropTable");

        petTable = Resources.Load<PetDropTable>("Table/PetDropTable");

        inventory = InventorySystem.Instance.inventory;
        skillInventory = InventorySystem.Instance.skillInventory;
        petInventory = InventorySystem.Instance.petInventory;
    }

    public void ItemGacha(int count)
    {
        if (count < 1)
            return;

        for(int i = 0; i < count; ++i)
        {
            var item = itemTable.GetItem(currentItemRank);

            if(item == null)
                continue;

            switch(item.type)
            {
                case ItemType.Weapon:
                    WeaponItem(item);
                    break;
                case ItemType.Armor:
                    ArmorItem(item);
                    break;
            }
        }
    }

    private void WeaponItem(Item item)
    {
        var weapon = inventory.weapons.Where(x=>x.item.name == item.name).FirstOrDefault();

        if (weapon == null)
            return;

        if (!weapon.acquire)
        {
            weapon.acquire = true;
            return;
        }

        weapon.stock += 1;
        Debug.Log(weapon.stock);
    }

    private void ArmorItem(Item item)
    {
        var armor = inventory.armors.Where(x => x.item.name == item.name).FirstOrDefault();

        if (armor == null)
            return;

        if(!armor.acquire)
        {
            armor.acquire = true;
            return;
        }

        armor.stock += 1;
    }

    public void SkillGacha(int count)
    {
        if (count < 1)
            return;

        for(int i = 0; i< count; ++i)
        {
            var skill = skillTable.GetSkill(currentSkillRank);

            if (skill == null)
                continue;

            var baseSkill = skillInventory.skills.Where(x=>x.skill.name == skill.name).FirstOrDefault();

            if (baseSkill == null)
                continue;

            if (!baseSkill.acquire)
            {
                baseSkill.acquire = true;
                continue;
            }

            baseSkill.stock += 1;
        }
    }
}

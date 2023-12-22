using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : Singleton<ShopSystem>
{
    public ItemDropTable itemTable { get; private set; }
    public SkillDropTable skillTable { get; private set; }
    public PetDropTable petTable { get; private set; }

    public int currentItemRank = 0;
    public int currentItemRankUp = 0;
    public int currentSkillRank= 0;
    public int currentSkillRankUp = 0;
    public int currentPetRank=0;
    public int currentPetRankUp=0;

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

    public void ItemGacha(GachaInfo info, int count)
    {
        if (count < 1)
            return;

        for(int i = 0; i < count; ++i)
        {
            var item = itemTable.GetItem(currentItemRank);

            if(item == null)
                continue;

            currentItemRankUp += 1;

            switch(item.type)
            {
                case ItemType.Weapon:
                    WeaponItem(info.slots[i].gameObject, item);
                    break;
                case ItemType.Armor:
                    ArmorItem(info.slots[i].gameObject, item);
                    break;
            }
            UIManager.Instance.questSystemUi.GetGatchCount((int)GatchaType.Weapon, 1);
        }
        
        if (currentItemRankUp >= itemTable.drops[currentItemRank].RankUp)
        {
            currentItemRankUp -= itemTable.drops[currentItemRank].RankUp;
            currentItemRank += 1;

            if(currentItemRank> itemTable.drops.Count-1)
            {
                currentItemRank = itemTable.drops.Count - 1;
            }
        }
    }

    private void WeaponItem(GameObject obj, Item item)
    {
        var weapon = inventory.weapons.Where(x=>x.item.name == item.name).FirstOrDefault();

        if (weapon == null)
            return;

        var slot = obj.transform.GetChild(0);
        slot.GetComponent<Image>().sprite = weapon.item.itemIcon;
        Color color = new Color();
        switch (weapon.item.tier)
        {
            case Tier.Common:
                {
                    color = new Color(137f / 255f, 126f / 255f, 126f / 255f, 128f / 255f);
                    obj.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Uncommon:
                {
                    color = new Color(0, 0, 0, 128f / 255f);
                    obj.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Rare:
                {
                    color = new Color(45f / 255f, 148f / 255f, 244f / 255f, 128f / 255f);
                    obj.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Unique:
                {
                    color = new Color(248f / 255f, 207f / 255f, 41f / 255f, 128f / 255f);
                    obj.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Legendry:
                {
                    color = new Color(0, 1, 71f / 255f, 128f / 255f);
                    obj.GetComponent<Outline>().effectColor = color;
                }
                break;
        }
        obj.SetActive(true);
        if (!weapon.acquire)
        {
            weapon.acquire = true;
            return;
        }

        weapon.stock += 1;
    }

    private void ArmorItem(GameObject obj, Item item)
    {
        var armor = inventory.armors.Where(x => x.item.name == item.name).FirstOrDefault();

        if (armor == null)
            return;

        var slot = obj.transform.GetChild(0);
        slot.GetComponent<Image>().sprite = armor.item.itemIcon;
        Color color = new Color();
        switch (armor.item.tier)
        {
            case Tier.Common:
                {
                    color = new Color(137f / 255f, 126f / 255f, 126f / 255f, 128f / 255f);
                    obj.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Uncommon:
                {
                    color = new Color(0, 0, 0, 128f / 255f);
                    obj.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Rare:
                {
                    color = new Color(45f / 255f, 148f / 255f, 244f / 255f, 128f / 255f);
                    obj.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Unique:
                {
                    color = new Color(248f / 255f, 207f / 255f, 41f / 255f, 128f / 255f);
                    obj.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Legendry:
                {
                    color = new Color(0, 1, 71f / 255f, 128f / 255f);
                    obj.GetComponent<Outline>().effectColor = color;
                }
                break;
        }
        obj.SetActive(true);
        if (!armor.acquire)
        {
            armor.acquire = true;
            return;
        }

        armor.stock += 1;
    }

    public void SkillGacha(GachaInfo info, int count)
    {
        if (count < 1)
            return;

        for(int i = 0; i< count; ++i)
        {
            var skill = skillTable.GetSkill(currentSkillRank);

            if (skill == null)
                continue;

            currentSkillRankUp += 1;

            var baseSkill = skillInventory.skills.Where(x=>x.skill.name == skill.name).FirstOrDefault();

            if (baseSkill == null)
                continue;

            var obj = info.slots[i].gameObject;
            var slot = obj.transform.GetChild(0);
            slot.GetComponent<Image>().sprite = baseSkill.skill.image;
            Color color = new Color();
            switch (baseSkill.skill.Skill_Tier)
            {
                case Tier.Common:
                    {
                        color = new Color(137f / 255f, 126f / 255f, 126f / 255f, 128f / 255f);
                        obj.GetComponent<Outline>().effectColor = color;
                    }
                    break;

                case Tier.Uncommon:
                    {
                        color = new Color(0, 0, 0, 128f / 255f);
                        obj.GetComponent<Outline>().effectColor = color;
                    }
                    break;

                case Tier.Rare:
                    {
                        color = new Color(45f / 255f, 148f / 255f, 244f / 255f, 128f / 255f);
                        obj.GetComponent<Outline>().effectColor = color;
                    }
                    break;

                case Tier.Unique:
                    {
                        color = new Color(248f / 255f, 207f / 255f, 41f / 255f, 128f / 255f);
                        obj.GetComponent<Outline>().effectColor = color;
                    }
                    break;

                case Tier.Legendry:
                    {
                        color = new Color(0, 1, 71f / 255f, 128f / 255f);
                        obj.GetComponent<Outline>().effectColor = color;
                    }
                    break;
            }
            obj.SetActive(true);
            UIManager.Instance.questSystemUi.GetGatchCount((int)GatchaType.Skill, 1);
            if (!baseSkill.acquire)
            {
                baseSkill.acquire = true;
                continue;
            }

            baseSkill.stock += 1;
        }

        if (currentSkillRankUp >= skillTable.drops[currentSkillRank].RankUp)
        {
            currentSkillRankUp -= skillTable.drops[currentSkillRank].RankUp;
            currentSkillRank += 1;

            if (currentSkillRank > skillTable.drops.Count - 1)
            {
                currentSkillRank = skillTable.drops.Count - 1;
            }
        }
    }

    public void PetGacha(GachaInfo info, int count)
    {
        if (count < 1)
            return;

        for (int i = 0; i < count; ++i)
        {
            var pet = petTable.GetPet(currentPetRank);

            if (pet == null)
                continue;

            currentPetRankUp += 1;

            var basePet = petInventory.pets.Where(x => x.pet.name == pet.name).FirstOrDefault();

            if (basePet == null)
                continue;

            var obj = info.slots[i].gameObject;
            var slot = obj.transform.GetChild(0);
            //slot.GetComponent<Image>().sprite = basePet;
            Color color = new Color();
            switch (basePet.pet.Pet_Tier)
            {
                case Tier.Common:
                    {
                        color = new Color(137f / 255f, 126f / 255f, 126f / 255f, 128f / 255f);
                        obj.GetComponent<Outline>().effectColor = color;
                    }
                    break;

                case Tier.Uncommon:
                    {
                        color = new Color(0, 0, 0, 128f / 255f);
                        obj.GetComponent<Outline>().effectColor = color;
                    }
                    break;

                case Tier.Rare:
                    {
                        color = new Color(45f / 255f, 148f / 255f, 244f / 255f, 128f / 255f);
                        obj.GetComponent<Outline>().effectColor = color;
                    }
                    break;

                case Tier.Unique:
                    {
                        color = new Color(248f / 255f, 207f / 255f, 41f / 255f, 128f / 255f);
                        obj.GetComponent<Outline>().effectColor = color;
                    }
                    break;

                case Tier.Legendry:
                    {
                        color = new Color(0, 1, 71f / 255f, 128f / 255f);
                        obj.GetComponent<Outline>().effectColor = color;
                    }
                    break;
            }
            obj.SetActive(true);
            UIManager.Instance.questSystemUi.GetGatchCount((int)GatchaType.Pet, 1);
            if (!basePet.acquire)
            {
                basePet.acquire = true;
                continue;
            }

            basePet.stock += 1;
        }

        if (currentPetRankUp >= petTable.drops[currentPetRank].RankUp)
        {
            currentPetRankUp -= petTable.drops[currentPetRank].RankUp;
            currentPetRank += 1;

            if (currentPetRank > petTable.drops.Count - 1)
            {
                currentPetRank = petTable.drops.Count - 1;
            }
        }
    }
}

using System.Collections.Generic;

public class InventoryData
{
    public string itemName;
    public int upgradeLev;
    public bool acquire;
    public bool equip;
    public int stock;

    public InventoryData(string itemName, int upgradeLev, bool acquire, bool equip, int stock)
    {
        this.itemName = itemName;
        this.upgradeLev = upgradeLev;
        this.acquire = acquire;
        this.equip = equip;
        this.stock = stock;
    }
}

public class EquipData
{
    public string itemName;
    public ItemType itemType;

    public EquipData(string itemName, ItemType itemType)
    {
        this.itemName = itemName;
        this.itemType = itemType;
    }
}

public class CustomData
{
    public string baseName;
    public int upgradeLev;
    public List<Item.AddOption> addOptions;

    public CustomData(string baseName, int upgradeLev, List<Item.AddOption> addOptions)
    {
        this.baseName = baseName;
        this.upgradeLev = upgradeLev;
        this.addOptions = addOptions;
    }
}

public class SkillData
{
    public string skillName;
    public int skillLevel;
    public bool acquire;
    public int stock;

    public SkillData(string skillName, int skillLevel, bool acquire, int stock)
    {
        this.skillName = skillName;
        this.skillLevel = skillLevel;
        this.acquire = acquire;
        this.stock = stock;
    }
}

public class EquipSkillData
{
    public string skillName;
    public int equipIndex;

    public EquipSkillData(string skillName, int equipIndex)
    {
        this.skillName = skillName;
        this.equipIndex = equipIndex;
    }
}
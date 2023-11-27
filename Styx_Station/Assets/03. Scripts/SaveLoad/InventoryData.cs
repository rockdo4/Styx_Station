using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
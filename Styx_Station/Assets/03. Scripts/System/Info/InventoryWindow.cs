using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : SubWindow
{
    public ItemType currentType;

    public InventoryType[] inventoryTypes;

    public override void Open()
    {
        base.Open();

        Open(currentType);
    }

    public override void Close()
    {
        currentType = ItemType.Weapon;
        base.Close();
    }

    public void Open(ItemType Type)
    {
        if (inventoryTypes[(int)currentType].gameObject.activeSelf)
            inventoryTypes[(int)currentType].Close();

        currentType = Type;

        inventoryTypes[(int)currentType].Open();
    }

    public void OnClickWeapon()
    {
        Open(ItemType.Weapon);
    }

    public void OnClickArmor()
    {
        Open(ItemType.Armor);
    }
    public void OnClickRing()
    {
        Open(ItemType.Ring);
    }

    public void OnClickSymbol()
    {
        Open(ItemType.Symbol);
    }

    private void Awake()
    {
        var inventory = InventorySystem.Instance.inventory;

        inventoryTypes[0].gameObject.GetComponent<WeaponType>().Setting(inventory);
        inventoryTypes[1].gameObject.GetComponent<ArmorType>().Setting(inventory);
        inventoryTypes[2].gameObject.GetComponent<RingType>().Setting(inventory);
        inventoryTypes[3].gameObject.GetComponent<SymbolType>().Setting(inventory);
    }
}

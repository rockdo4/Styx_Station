using TMPro;
using UnityEngine;

public class EquipButton : MonoBehaviour
{
    public Inventory inventory;

    public TextMeshProUGUI itemname;

    public ItemType type;
    public int itemIndex;

    public void OnClickDequip()
    {
        if (itemIndex<0)
            return;

        Inventory.InventoryItem item;

        switch(type)
        {
            case ItemType.Weapon:
                item = inventory.weapons[itemIndex];
                inventory.DequipItem(item, type);
                break;
            case ItemType.Armor:
                item = inventory.armors[itemIndex];
                inventory.DequipItem(item, type);
                break;
            case ItemType.Ring:
                item = inventory.customRings[itemIndex].item;
                inventory.DequipItem(item, type);
                break;
            case ItemType.Symbol:
                item = inventory.customSymbols[itemIndex].item;
                inventory.DequipItem(item, type);
                break;
        }

        itemIndex = -1;
        itemname.text = "None";
    }
}

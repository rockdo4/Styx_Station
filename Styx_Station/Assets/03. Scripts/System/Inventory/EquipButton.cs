using TMPro;
using UnityEngine;

public class EquipButton : MonoBehaviour
{
    public Inventory inventory;

    public TextMeshProUGUI itemname;

    public ItemType type;
    public int itemIndex;
    public int equipIndex;

    public void OnClickDequip()
    {
        if (itemIndex<0)
            return;

        Inventory.InventoryItem item;

        switch(type)
        {
            case ItemType.Weapon:
                item = inventory.weapons[itemIndex];
                inventory.DequipItem(item, equipIndex);
                break;
            case ItemType.Armor:
                item = inventory.armors[itemIndex];
                inventory.DequipItem(item, equipIndex);
                break;
            case ItemType.Ring:
                item = inventory.customRings[itemIndex].item;
                inventory.DequipItem(item, equipIndex);
                break;
            case ItemType.Symbol:
                item = inventory.customSymbols[itemIndex].item;
                inventory.DequipItem(item, equipIndex);
                break;
        }

        itemIndex = -1;
        itemname.text = "None";

        //baseUi.equipWindow.GetComponent<EquipWindow>().ClearItemInfo();
        //baseUi.stateWindow.GetComponent<StateWindow>().GetState();
    }
}

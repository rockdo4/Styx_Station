using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemButton : MonoBehaviour
{
    public Inventory inventory;

    public TextMeshProUGUI itemname;

    public ItemType type;
    public int itemIndex;

    public bool AcqurieItem()
    {
        bool acquire = false;

        switch(type)
        {
            case ItemType.Weapon:
                acquire = inventory.weapons[itemIndex].acquire;
                break;
            case ItemType.Armor:
                acquire = inventory.armors[itemIndex].acquire;
                break;
            case ItemType.Ring:
                acquire = inventory.customRings[itemIndex].item.acquire;
                break;
            case ItemType.Symbol:
                acquire = inventory.customSymbols[itemIndex].item.acquire;
                break;
        }

        if (!acquire)
        {
            gameObject.GetComponent<Image>().color = Color.red;
            return false;
        }

        gameObject.GetComponent<Image>().color = Color.white;
        return true;
    }

    public void OnClickEquip(GameObject button)
    {
        var equip = button.GetComponent<EquipButton>();

        if (equip == null)
            return;

        if (itemIndex < 0)
            return;

        if (!AcqurieItem())
            return;

        switch (type)
        {
            case ItemType.Weapon:
                equip.itemIndex = itemIndex;
                equip.itemname.text = inventory.weapons[itemIndex].item.itemName;
                break;
            case ItemType.Armor:
                equip.itemIndex = itemIndex;
                equip.itemname.text = inventory.armors[itemIndex].item.itemName;
                break;
            case ItemType.Ring:
                equip.itemIndex = itemIndex;
                equip.itemname.text = inventory.customRings[itemIndex].item.item.itemName;
                break;
            case ItemType.Symbol:
                equip.itemIndex = itemIndex;
                equip.itemname.text = inventory.customSymbols[itemIndex].item.item.itemName;
                break;
        }

        inventory.EquipItem(itemIndex, type);

        //baseUi.equipWindow.GetComponent<EquipWindow>().ViewItemInfo(equipIndex);
        //baseUi.stateWindow.GetComponent<StateWindow>().GetState();
    }
}


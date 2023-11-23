using TMPro;
using UnityEngine;

public class EquipButton : MonoBehaviour
{
    public InventoryUI baseUi;

    public TextMeshProUGUI itemname;
    public Inventory.InventoryItem item;
    public int equipIndex;

    public void OnClickDequip()
    {
        if (item == null)
            return;

        if (item.item == null)
            return;

        InventorySystem.Instance.inventory.DequipItem(item, equipIndex);
        item = null;
        itemname.text = "None";

        baseUi.GetState();
    }
}

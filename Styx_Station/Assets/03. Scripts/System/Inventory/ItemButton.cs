using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public InventoryUI baseUi;

    public TextMeshProUGUI itemname;
    public Inventory.InventoryItem item;
    public int equipIndex;

    public void AcqurieItem()
    {
        if (!item.acquire)
        {
            gameObject.GetComponent<Image>().color = Color.red;
            return;
        }
            gameObject.GetComponent<Image>().color = Color.white;
    }

    public void OnClickEquip()
    {
        if (item == null)
            return;

        if (item.item == null)
            return;

        AcqurieItem();

        if (!item.acquire)
            return;

        var equip = baseUi.equipButtons[equipIndex].GetComponent<EquipButton>();
        equip.item = item;
        equip.itemname.text = item.item.itemName;
        InventorySystem.Instance.inventory.EquipItem(item, equipIndex);

        baseUi.GetState();
    }
}


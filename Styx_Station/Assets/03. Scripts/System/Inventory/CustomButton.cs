using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomButton : MonoBehaviour
{
    public InventoryUI baseUi;

    public TextMeshProUGUI itemname;
    public Inventory.InventoryItem item;
    public int equipIndex;

    public void OnClickEquip()
    {
        if (item == null)
            return;

        if (item.item == null)
            return;

        var equip = baseUi.customBase.GetComponent<BaseCustomButton>();
        equip.item = item;
        equip.itemname.text = item.item.itemName;
    }
}

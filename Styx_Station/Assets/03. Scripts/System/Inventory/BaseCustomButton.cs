using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseCustomButton : MonoBehaviour
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

        item = null;
        itemname.text = "None";
    }
}

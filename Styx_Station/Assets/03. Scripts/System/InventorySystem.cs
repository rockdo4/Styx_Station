using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private static InventorySystem instance;

    public static InventorySystem Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("InventorySystem");
                instance = go.AddComponent<InventorySystem>();
                instance.item = Resources.Load<ItemTable>("Table/ItemTable");
                instance.inventory = go.AddComponent<Inventory>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private ItemTable item;
    private Inventory inventory;

    public void FixedInventoryImport()
    {
        if (item.GetFixedItemCount() <= 0)
        {
            return;
        }

        int index = item.GetFixedItemCount();

        for(int i=0; i<index; ++i)
        {
            inventory.AddFixedInventory(i, item.GetFixedItem(i), 0);
        }
    }
}
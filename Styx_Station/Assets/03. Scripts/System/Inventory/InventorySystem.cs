using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
                instance.optionTable = Resources.Load<CustomOptionTable>("Table/CustomOptionTable");
                instance.inventory = go.AddComponent<Inventory>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private ItemTable item;
    public Inventory inventory;
    private CustomOptionTable optionTable;

    public Item GetRingCustomItem(int index)
    {
        if (inventory == null)
            return null;

        if (inventory.rings.Count < index)
            return null;

        return inventory.rings[index].item;
    }

    public Item GetSymbolCustomItem(int index)
    {
        if (inventory == null)
            return null;

        if (inventory.symbols.Count < index)
            return null;

        return inventory.symbols[index].item;
    }

    public void RingCustom(Item item)
    {
        var dummy = inventory.CreateDummy(item);
    }

    public void OptionCustom(Item item, string tableName)
    {
        if (item == null)
            return;

        var option = optionTable.GetPickCustom(tableName);

        if (option == null)
            return;

        if (option.optionName == AddOptionString.None)
            return;

        item.AddOptions(option.optionName, option.value);
    }
    public void Setting()
    {
        for (int i = 0; i < item.GetTableSize(); ++i)
        {
            var addItem = item.GetItemList(i);
            inventory.AddItem(addItem);
        }
    }
}
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

    public void Setting()
    {
        for (int i = 0; i < item.GetTableSize(); ++i)
        {
            var addItem = item.GetItemList(i);
            inventory.AddItem(addItem);
        }

        inventory.EquipItem(inventory.weapons[0], 0);
        inventory.EquipItem(inventory.armors[0], 1);
    }

    public void test()
    {
        inventory.DequipItem(inventory.weapons[0], 0);
        inventory.EquipItem(inventory.armors[1], 1);
    }
    public void test1()
    {
        inventory.EquipItem(inventory.weapons[0], 0);
        inventory.EquipItem(inventory.armors[0], 1);
    }
}
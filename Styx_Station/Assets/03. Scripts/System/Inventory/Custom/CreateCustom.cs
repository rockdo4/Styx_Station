using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateCustom : MonoBehaviour
{
    public UIManager manager;

    public Button baseItem;

    public Inventory inventory;

    private void Awake()
    {
        inventory = InventorySystem.Instance.inventory;
        manager = UIManager.Instance;
    }

    public void CreateCustomItem(int i)
    {
        var item = ShopSystem.Instance.CustomBase(i);

        if (item == null)
            return;

        var table = InventorySystem.Instance.optionTable;

        var tableName = string.Empty;

        if (item.type == ItemType.Ring)
            tableName = table.table[0].name;

        else if(item.type == ItemType.Symbol)
            tableName = table.table[1].name;

        var custom = table.table.Where(x => x.name == tableName).FirstOrDefault();

        if (custom == null)
            return;

        int optionCount = 0;

        switch (item.tier)
        {
            case Tier.Common:
                optionCount = 1;
                break;
            case Tier.Uncommon:
                optionCount = 2;
                break;

            case Tier.Rare:
            case Tier.Unique:
                optionCount = 3;
                break;

            case Tier.Legendry:
                optionCount = 4;
                break;
        }

        switch (item.type)
        {
            case ItemType.Ring:
                optionCount -= item.addOptions.Count;
                break;
            case ItemType.Symbol:
                optionCount -= item.addOptions.Count;
                break;
        }

        if (optionCount <= 0)
            return;

        switch (item.type)
        {
            case ItemType.Ring:
                {
                    var dummy = InventorySystem.Instance.Custom(item, optionCount, custom.name);
                    manager.windows[0].GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[2].GetComponent<RingType>().AddRing();
                }
                break;

            case ItemType.Symbol:
                {
                    var dummy = InventorySystem.Instance.Custom(item, optionCount, custom.name);
                    manager.windows[0].GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[3].GetComponent<SymbolType>().AddSymbol();
                }
                break;
        }
    }
}

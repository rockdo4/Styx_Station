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

    public void CreateCustomItem()
    {
        var item = baseItem.GetComponent<BaseCustomButton>();

        if (item == null)
            return;

        if (item.itemIndex < 0)
            return;

        var table = InventorySystem.Instance.optionTable;

        var tableName = manager.windows[1].GetComponent<CustomWindow>().tableName;

        var custom = table.table.Where(x => x.name == tableName.text).FirstOrDefault();

        if (custom == null)
            return;

        int optionCount = 0;

        Tier tier = Tier.Common;

        switch (item.type)
        {
            case ItemType.Ring:
                tier = inventory.rings[item.itemIndex].item.tier;
                break;
            case ItemType.Symbol:
                tier = inventory.symbols[item.itemIndex].item.tier;
                break;
        }

        switch (tier)
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
                optionCount -= inventory.rings[item.itemIndex].item.addOptions.Count;
                break;
            case ItemType.Symbol:
                optionCount -= inventory.symbols[item.itemIndex].item.addOptions.Count;
                break;
        }


        if (optionCount <= 0)
            return;

        switch (item.type)
        {
            case ItemType.Ring:
                {
                    var baseItem = inventory.rings[item.itemIndex].item;
                    var dummy = InventorySystem.Instance.Custom(baseItem, optionCount, custom.name);
                    manager.windows[0].GetComponent<EquipWindow>().AddRing(dummy.index, item.itemIndex);
                }
                break;

            case ItemType.Symbol:
                {
                    var baseItem = inventory.symbols[item.itemIndex].item;
                    var dummy = InventorySystem.Instance.Custom(baseItem, optionCount, custom.name);
                    manager.windows[0].GetComponent<EquipWindow>().AddSymbol(dummy.index, item.itemIndex);
                }
                break;
        }
    }
}

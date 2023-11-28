using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateCustom : MonoBehaviour
{
    public InventoryUI baseUi;

    public Button baseItem;

    public Inventory inventory;

    private void Awake()
    {
        inventory = InventorySystem.Instance.inventory;
    }

    public void CreateCustomItem()
    {
        var item = baseItem.GetComponent<BaseCustomButton>();

        if (item == null)
            return;

        if (item.itemIndex < 0)
            return;

        var table = InventorySystem.Instance.optionTable;

        var tableName = baseUi.customWindow.GetComponent<CustomWindow>().tableName;

        var custom = table.table.Where(x => x.name == tableName.text).FirstOrDefault();

        if (custom == null)
            return;

        int optionCount = 0;

        ItemTier tier = ItemTier.Common;

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
            case ItemTier.Common:
                optionCount = 1;
                break;
            case ItemTier.Uncommon:
                optionCount = 2;
                break;

            case ItemTier.Rare:
            case ItemTier.Unique:
                optionCount = 3;
                break;

            case ItemTier.Legendry:
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
                    baseUi.equipWindow.AddRing(dummy.index, item.itemIndex);
                }
                break;

            case ItemType.Symbol:
                {
                    var baseItem = inventory.symbols[item.itemIndex].item;
                    var dummy = InventorySystem.Instance.Custom(baseItem, optionCount, custom.name);
                    baseUi.equipWindow.AddSymbol(dummy.index, item.itemIndex);
                }
                break;
        }
    }
}

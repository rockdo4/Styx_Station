using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateCustom : MonoBehaviour
{
    public InventoryUI baseUi;

    public Button baseItem;

    public void CreateCustomItem()
    {
        var item = baseItem.GetComponent<BaseCustomButton>();

        if (item == null)
            return;

        if (item.item == null)
            return;

        if (item.item.item == null)
            return;

        var table = InventorySystem.Instance.optionTable;

        var tableName = baseUi.customWindow.GetComponent<CustomWindow>().tableName;

        var custom = table.table.Where(x => x.name == tableName.text).FirstOrDefault();

        if (custom == null)
            return;

        int optionCount = 0;

        switch(item.item.item.tier)
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

        optionCount -= item.item.item.addOptions.Count;

        if (optionCount <= 0)
            return;

        var dummy = InventorySystem.Instance.Custom(item.item.item, optionCount, custom.name);

        switch(item.item.item.type)
        {
            case ItemType.Ring:
                baseUi.equipWindow.AddRing(dummy);
                break;
            case ItemType.Symbol:
                baseUi.equipWindow.AddSymbol(dummy);
                break;
        }
    }
}

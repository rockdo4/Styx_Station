using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SymbolType : InventoryType
{
    public GameObject symbols;

    public Button symbolSlot;

    private List<Button> customSymbolButtons = new List<Button>();
    public override void Open()
    {
        base.Open();

        foreach (var symbol in customSymbolButtons)
        {
            var button = symbol.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }
    }

    public override void Close()
    {
        base.Close();
    }

    public void Setting(Inventory inventory)
    {
        for (int i = 0; i < inventory.customRings.Count; ++i)
        {
            Button button = Instantiate(symbolSlot, symbols.transform);
            button.AddComponent<ItemButton>();

            var ui = button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Symbol;
            ui.itemIndex = i;
            ui.image = button.transform.GetChild(0).gameObject;
            ui.itemLv = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            //button.onClick.AddListener(() => ui.OnClickEquip(equipSymbol.gameObject));
            customSymbolButtons.Add(button);
        }
    }
}

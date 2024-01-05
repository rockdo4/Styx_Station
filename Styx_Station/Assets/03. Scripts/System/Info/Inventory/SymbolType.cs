using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SymbolType : InventoryType
{
    private Inventory inventory;

    public GameObject symbols;

    public Button symbolSlot;

    public GameObject info;

    public int selectIndex = -1;

    public List<Button> customSymbolButtons { get; private set; } = new List<Button>();
    
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
        OnClickCloseSymbolInfo();
        
        base.Close();
    }

    public void OnClickCloseSymbolInfo()
    {
        selectIndex = -1;
        info.SetActive(false);

        foreach (var symbol in customSymbolButtons)
        {
            var button = symbol.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }

        if ((ButtonList.infoButton & InfoButton.SymbolInfo) != 0)
            ButtonList.infoButton &= ~InfoButton.SymbolInfo;
    }
    public void Setting(Inventory inventory)
    {
        this.inventory = inventory;
        info.GetComponent<SymbolEquipInfoUi>().Inventory();

        for (int i = 0; i < inventory.customSymbols.Count; ++i)
        {
            Button button = Instantiate(symbolSlot, symbols.transform);
            button.AddComponent<ItemButton>();

            var ui = button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Symbol;
            button.name = i.ToString();
            ui.itemIndex = i;
            ui.image = button.transform.GetChild(0).gameObject;
            ui.itemLv = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            button.onClick.AddListener(() => ui.OnClickSymbolOpenInfo(this));
            customSymbolButtons.Add(button);
        }
    }

    public void infoUpdate()
    {
        foreach (var symbol in customSymbolButtons)
        {
            var button = symbol.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }
    }

    public void AddSymbol()
    {
        Button button = Instantiate(symbolSlot, symbols.transform);
        button.AddComponent<ItemButton>();

        var ui = button.GetComponent<ItemButton>();
        ui.inventory = inventory;
        ui.type = ItemType.Symbol;
        ui.itemIndex = customSymbolButtons.Count;
        ui.image = button.transform.GetChild(0).gameObject;
        ui.itemLv = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        button.onClick.AddListener(() => ui.OnClickSymbolOpenInfo(this));
        customSymbolButtons.Add(button);
    }
}

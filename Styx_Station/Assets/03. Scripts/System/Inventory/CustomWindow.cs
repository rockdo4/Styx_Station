using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomWindow : MonoBehaviour
{
    public Inventory inventory;
    public GameObject baseScroll;
    public GameObject tableScroll;

    public Button customBase;
    public Button customPrefabs;
    public Button talbePrefabs;
    public Button createButton;

    public TextMeshProUGUI tableName;

    private List<Button> customBaseRIng = new List<Button>();
    private List<Button> customBaseSymbol = new List<Button>();
    private List<Button> tableButtons = new List<Button>();

    private void Awake()
    {
        inventory = InventorySystem.Instance.inventory;
    }
    public void Setting()
    {
        var customUi = customBase.GetComponent<BaseCustomButton>();
        if (customUi != null)
        {
            customUi.itemname.text = "None";
            customBase.onClick.RemoveAllListeners();
            customBase.onClick.AddListener(customUi.OnClickDequip);
        }

        var createUi = createButton.GetComponent<CreateCustom>();
        if (createUi != null)
        {
            createUi.baseItem = customBase;
            createButton.onClick.RemoveAllListeners();
            createButton.onClick.AddListener(createUi.CreateCustomItem);
        }

        int baseRingLeng = inventory.rings.Count;
        int baseSymbolLeng = inventory.symbols.Count;
        int tableButtonLeng = InventorySystem.Instance.optionTable.table.Count;

        if (baseRingLeng > 0)
        {
            int index = 0;
            if (baseRingLeng >= customBaseRIng.Count)
                index = customBaseRIng.Count;

            for (int i = index; i < baseRingLeng; ++i)
            {
                Button Button = Instantiate(customPrefabs, baseScroll.transform);
                var ui = Button.GetComponent<CustomButton>();
                ui.itemIndex = i;
                ui.itemName.text = inventory.rings[i].item.itemName;
                ui.type = ItemType.Ring;
                Button.onClick.AddListener(() => ui.OnClickEquip(customUi));
                customBaseRIng.Add(Button);
            }
        }

        if (baseSymbolLeng > 0)
        {
            int index = 0;
            if (baseSymbolLeng >= customBaseSymbol.Count)
                index = customBaseSymbol.Count;

            for (int i = index; i < baseSymbolLeng; ++i)
            {
                Button Button = Instantiate(customPrefabs, baseScroll.transform);
                var ui = Button.GetComponent<CustomButton>();
                ui.itemIndex = i;
                ui.itemName.text = inventory.symbols[i].item.itemName;
                ui.type = ItemType.Symbol;
                Button.onClick.AddListener(() => ui.OnClickEquip(customUi));
                customBaseSymbol.Add(Button);
            }
        }

        if(tableButtonLeng > 0)
        {
            int index = 0;
            if (tableButtonLeng >= tableButtons.Count)
                index = tableButtons.Count;

            for (int i=index; i< tableButtonLeng; ++i)
            {
                Button button = Instantiate(talbePrefabs, tableScroll.transform);
                var ui = button.GetComponent<TableButton>();
                ui.customWindow = this;
                ui.tableName.text = InventorySystem.Instance.optionTable.table[i].name;
                button.onClick.AddListener(ui.OnClickTalbe);
                tableButtons.Add(button);
            }
        }

    }
}

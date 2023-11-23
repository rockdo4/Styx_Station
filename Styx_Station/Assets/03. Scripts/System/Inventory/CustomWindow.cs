using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomWindow : MonoBehaviour
{
    public InventoryUI baseInventory;
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

    public void Setting()
    {
        var customUi = customBase.GetComponent<BaseCustomButton>();
        if (customUi != null)
        {
            customUi.baseUi = baseInventory;
            customUi.itemname.text = "None";
            customBase.onClick.AddListener(customUi.OnClickDequip);
        }

        var createUi = createButton.GetComponent<CreateCustom>();
        if (createUi != null)
        {
            createUi.baseUi = baseInventory;
            createUi.baseItem = customBase;
            createButton.onClick.AddListener(createUi.CreateCustomItem);
        }

        int baseRingLeng = InventorySystem.Instance.inventory.rings.Count;
        int baseSymbolLeng = InventorySystem.Instance.inventory.symbols.Count;
        int tableButtonLeng = InventorySystem.Instance.optionTable.table.Count;

        if (baseRingLeng > 0)
        {
            for (int i = 0; i < baseRingLeng; ++i)
            {
                Button Button = Instantiate(customPrefabs, baseScroll.transform);
                var ui = Button.GetComponent<CustomButton>();
                ui.item = InventorySystem.Instance.inventory.rings[i];
                ui.baseUi = baseInventory;
                ui.itemname.text = ui.item.item.itemName;
                ui.equipIndex = 2;
                Button.onClick.AddListener(ui.OnClickEquip);
                customBaseRIng.Add(Button);
            }
        }

        if (baseSymbolLeng > 0)
        {
            for (int i = 0; i < baseSymbolLeng; ++i)
            {
                Button Button = Instantiate(customPrefabs, baseScroll.transform);
                var ui = Button.GetComponent<CustomButton>();
                ui.item = InventorySystem.Instance.inventory.symbols[i];
                ui.baseUi = baseInventory;
                ui.itemname.text = ui.item.item.itemName;
                ui.equipIndex = 3;
                Button.onClick.AddListener(ui.OnClickEquip);
                customBaseSymbol.Add(Button);
            }
        }

        if(tableButtonLeng > 0)
        {
            for(int i=0; i< tableButtonLeng; ++i)
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

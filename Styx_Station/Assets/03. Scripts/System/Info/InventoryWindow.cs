using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static StageTable;

public class InventoryWindow : SubWindow
{
    private StringTable stringTable;

    public ItemType currentType;

    public InventoryType[] inventoryTypes;

    public Button[] tabs;

    private bool first = false;

    private void Awake()
    {
        if (!first)
            Setting();
    }
    public override void Open()
    { 
        base.Open();

        Open(currentType);

        InventoryTabTextUpdate();
    }

    public void InventoryTabTextUpdate()
    {
        if (Global.language == Language.KOR)
        {
            tabs[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("InventoryCon001").KOR}";
            tabs[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("InventoryCon002").KOR}";
            tabs[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("InventoryCon003").KOR}";
            tabs[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("InventoryCon004").KOR}";

        }
        else if (Global.language == Language.ENG)
        {
            tabs[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("InventoryCon001").ENG}";
            tabs[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("InventoryCon002").ENG}";
            tabs[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("InventoryCon003").ENG}";
            tabs[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("InventoryCon004").ENG}";
        }
    }

    public override void Close()
    {
        inventoryTypes[(int)currentType].Close();

        currentType = ItemType.Weapon;

        base.Close();
    }

    public void Open(ItemType Type)
    {
        if (inventoryTypes[(int)currentType].gameObject.activeSelf)
            inventoryTypes[(int)currentType].Close();

        currentType = Type;

        inventoryTypes[(int)currentType].Open();
    }

    public void OnClickWeapon()
    {
        Open(ItemType.Weapon);
    }

    public void OnClickArmor()
    {
        Open(ItemType.Armor);
    }
    public void OnClickRing()
    {
        Open(ItemType.Ring);
    }

    public void OnClickSymbol()
    {
        Open(ItemType.Symbol);
    }

    public void Setting()
    {
        var inventory = InventorySystem.Instance.inventory;
     
        if (MakeTableData.Instance.stringTable == null)
            MakeTableData.Instance.stringTable = new StringTable();

        stringTable = MakeTableData.Instance.stringTable;

        inventoryTypes[0].gameObject.GetComponent<WeaponType>().Setting(inventory);
        inventoryTypes[1].gameObject.GetComponent<ArmorType>().Setting(inventory);
        inventoryTypes[2].gameObject.GetComponent<RingType>().Setting(inventory);
        inventoryTypes[3].gameObject.GetComponent<SymbolType>().Setting(inventory);

        first = true;
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using static Inventory;

public class EquipWindow : MonoBehaviour
{
    public InventoryUI baseInventory;
    public GameObject equip;
    public GameObject tabs;
    public GameObject itemScroll;

    public Button tabPrefabs;
    public Button equipPrefabs;
    public Button itemPrefabs;

    public List<Button> equipButtons = new List<Button>();
    private List<Button> tabButtons = new List<Button>();
    private List<Button> weaponButtons = new List<Button>();
    private List<Button> armorButtons = new List<Button>();
    private List<Button> customRingButtons = new List<Button>();
    private List<Button> customSymbolButtons = new List<Button>();


    public void Setting()
    {
        InventorySystem.Instance.Setting();

        int length = InventorySystem.Instance.inventory.GetEquipItemsLength();
        int weaponsLeng = InventorySystem.Instance.inventory.weapons.Count;
        int armorsLeng = InventorySystem.Instance.inventory.armors.Count;
        int customRingLeng = InventorySystem.Instance.inventory.customRings.Count;
        int customSymbolLeng = InventorySystem.Instance.inventory.customSymbols.Count;

        for (int i = 0; i < length; i++)
        {
            Button equipButton = Instantiate(equipPrefabs, equip.transform);
            var equipUi = equipButton.GetComponent<EquipButton>();
            equipUi.baseUi = baseInventory;
            equipUi.itemname.text = "None";
            equipUi.equipIndex = i;
            equipButton.onClick.AddListener(equipUi.OnClickDequip);
            equipButtons.Add(equipButton);
        }

        for (int i = 0; i < length; i++)
        {
            Button tabButton = Instantiate(tabPrefabs, tabs.transform);
            ItemType tabType = (ItemType)i;

            tabButton.GetComponentInChildren<TextMeshProUGUI>().text = tabType.ToString();
            tabButton.onClick.AddListener(() => GetTypeEvent(tabType));
            tabButtons.Add(tabButton);
        }

        for (int i = 0; i < weaponsLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.item = InventorySystem.Instance.inventory.weapons[i];
            ui.baseUi = baseInventory;
            ui.itemname.text = ui.item.item.itemName;
            ui.equipIndex = 0;
            ui.AcqurieItem();
            Button.onClick.AddListener(ui.OnClickEquip);
            weaponButtons.Add(Button);
        }

        for (int i = 0; i < armorsLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.item = InventorySystem.Instance.inventory.armors[i];
            ui.baseUi = baseInventory;
            ui.itemname.text = ui.item.item.itemName;
            ui.equipIndex = 1;
            ui.AcqurieItem();
            Button.onClick.AddListener(ui.OnClickEquip);
            Button.gameObject.SetActive(false);
            armorButtons.Add(Button);
        }

        for (int i = 0; i < customRingLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.item = InventorySystem.Instance.inventory.customRings[i].item;
            ui.baseUi = baseInventory;
            ui.itemname.text = ui.item.item.itemName;
            ui.equipIndex = 2;
            ui.AcqurieItem();
            Button.onClick.AddListener(ui.OnClickEquip);
            Button.gameObject.SetActive(false);
            customRingButtons.Add(Button);
        }

        for (int i = 0; i < customSymbolLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.item = InventorySystem.Instance.inventory.customRings[i].item;
            ui.baseUi = baseInventory;
            ui.itemname.text = ui.item.item.itemName;
            ui.equipIndex = 3;
            ui.AcqurieItem();
            Button.onClick.AddListener(ui.OnClickEquip);
            Button.gameObject.SetActive(false);
            customSymbolButtons.Add(Button);
        }
    }

    public void AddRing(InventoryItem item)
    {
        Button Button = Instantiate(itemPrefabs, itemScroll.transform);
        var ui = Button.GetComponent<ItemButton>();
        ui.item = item;
        ui.baseUi = baseInventory;
        ui.itemname.text = ui.item.item.itemName;
        ui.equipIndex = 2;
        ui.AcqurieItem();
        Button.onClick.AddListener(ui.OnClickEquip);
        Button.gameObject.SetActive(false);
        customRingButtons.Add(Button);
        GetTypeEvent(ItemType.Ring);
    }

    public void AddSymbol(InventoryItem item) 
    {
        Button Button = Instantiate(itemPrefabs, itemScroll.transform);
        var ui = Button.GetComponent<ItemButton>();
        ui.item = item;
        ui.baseUi = baseInventory;
        ui.itemname.text = ui.item.item.itemName;
        ui.equipIndex = 3;
        ui.AcqurieItem();
        Button.onClick.AddListener(ui.OnClickEquip);
        Button.gameObject.SetActive(false);
        customSymbolButtons.Add(Button);
        GetTypeEvent(ItemType.Symbol);
    }
    public void GetTypeEvent(ItemType type)
    {
        switch (type)
        {
            case ItemType.Weapon:
                Weapon();
                break;
            case ItemType.Armor:
                Armor();
                break;
            case ItemType.Ring:
                Ring();
                break;
            case ItemType.Symbol:
                Symbol();
                break;
        }
    }
    public void Weapon()
    {
        foreach (Transform child in itemScroll.transform)
        {
            var go = child.gameObject;
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }


        foreach (var weapon in weaponButtons)
        {
            weapon.gameObject.SetActive(true);
        }
    }
    public void Armor()
    {
        foreach (Transform child in itemScroll.transform)
        {
            var go = child.gameObject;
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }

        foreach (var aromr in armorButtons)
        {
            aromr.gameObject.SetActive(true);
        }
    }
    public void Ring()
    {
        foreach (Transform child in itemScroll.transform)
        {
            var go = child.gameObject;
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }

        foreach (var ring in customRingButtons)
        {
            ring.gameObject.SetActive(true);
        }
    }
    public void Symbol()
    {
        foreach (Transform child in itemScroll.transform)
        {
            var go = child.gameObject;
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }


        foreach (var symbol in customSymbolButtons)
        {
            symbol.gameObject.SetActive(true);
        }
    }
}

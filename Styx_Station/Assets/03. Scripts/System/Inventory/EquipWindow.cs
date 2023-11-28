using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class EquipWindow : MonoBehaviour
{
    private Inventory inventory;
    public GameObject equip;
    public GameObject tabs;
    public GameObject itemScroll;

    public TextMeshProUGUI itemText;

    public Button tabPrefabs;
    public Button equipPrefabs;
    public Button itemPrefabs;

    public List<Button> equipButtons = new List<Button>();
    private List<Button> tabButtons = new List<Button>();
    private List<Button> weaponButtons = new List<Button>();
    private List<Button> armorButtons = new List<Button>();
    private List<Button> customRingButtons = new List<Button>();
    private List<Button> customSymbolButtons = new List<Button>();

    private void Awake()
    {
        inventory = InventorySystem.Instance.inventory;
    }

    public void Setting()
    {
        EquipButtonCreate();
        TabsButtonCreate();
        WeaponButtonCreate();
        ArmorButtonCreate();
        RingButtonCreate();
        SymbolButtonCreate();
        EquipItemUpdate();
        GetTypeEvent(ItemType.Weapon);
        ViewItemInfo(0);
    }

    private void EquipItemUpdate()
    {
        for(int i=0; i<equipButtons.Count;++i)
        {
            var equipUi = equipButtons[i].GetComponent<EquipButton>();
            var equipItem = inventory.GetEquipItem(i);
            if (equipItem != null)
            {
                equipUi.itemIndex = equipItem.index;
                equipUi.itemname.text = equipItem.item.itemName;
            }
            else
            {
                equipUi.itemIndex = -1;
                equipUi.itemname.text = "None";
            }
        }
    }
    private void EquipButtonCreate()
    {
        int length = inventory.GetEquipItemsLength();
        int index = 0;

        if (length <= 0)
            return;

        if(length>= equipButtons.Count)
            index = equipButtons.Count;

        for (int i = index; i < length; i++)
        {
            Button equipButton = Instantiate(equipPrefabs, equip.transform);
            var equipUi = equipButton.GetComponent<EquipButton>();
            equipUi.inventory = inventory;
            equipUi.type = (ItemType)i;

            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(equipUi.OnClickDequip);
            equipButton.onClick.AddListener(() => ViewItemInfo((int)equipUi.type));
            var equipItem = inventory.GetEquipItem(i);
            if (equipItem != null)
            {
                equipUi.itemIndex = equipItem.index;
                equipUi.itemname.text = equipItem.item.itemName;
            }
            else
            {
                equipUi.itemIndex = -1;
                equipUi.itemname.text = "None";
            }

            equipButtons.Add(equipButton);
        }
    }

    private void TabsButtonCreate()
    {
        int length = inventory.GetEquipItemsLength();
        int index = 0;

        if (length <= 0)
            return;

        if (length >= tabButtons.Count)
            index = tabButtons.Count;

        for (int i = index; i < length; i++)
        {
            Button tabButton = Instantiate(tabPrefabs, tabs.transform);

            var ui = tabButton.GetComponent<TabsButton>();
            ItemType tabType = (ItemType)i;
            ui.type = tabType;

            tabButton.GetComponentInChildren<TextMeshProUGUI>().text = tabType.ToString();
            tabButton.onClick.AddListener(() => GetTypeEvent(tabType));
            tabButton.onClick.AddListener(() => ViewItemInfo((int)ui.type));
            tabButtons.Add(tabButton);
        }
    }

    private void WeaponButtonCreate()
    {
        int weaponsLeng = inventory.weapons.Count;
        int index = 0;

        if (weaponsLeng <= 0)
            return;

        if(weaponsLeng >= weaponButtons.Count)
            index = weaponButtons.Count;
        else if (weaponsLeng < weaponButtons.Count)
        {
            for (int i = weaponButtons.Count-1; i >= 0; i--)
            {
                weaponButtons.RemoveAt(i);
            }
        }

        for (int i = index; i < weaponsLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Weapon;
            ui.itemname.text = inventory.weapons[i].item.itemName;
            ui.itemIndex = i;
            ui.AcqurieItem();
            Button.onClick.AddListener(() => ui.OnClickEquip(equipButtons[0].gameObject));
            Button.onClick.AddListener(() => ViewItemInfo(0));
            Button.gameObject.SetActive(false);
            weaponButtons.Add(Button);
        }
    }

    private void ArmorButtonCreate()
    {
        int armorsLeng = inventory.armors.Count;
        int index = 0;

        if (armorsLeng <= 0)
            return;

        if (armorsLeng >= armorButtons.Count)
            index = armorButtons.Count;
        else if (armorsLeng < armorButtons.Count)
        {
            for (int i = armorButtons.Count - 1; i >= 0; i--)
            {
                armorButtons.RemoveAt(i);
            }
        }

        for (int i = index; i < armorsLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Armor;
            ui.itemname.text = inventory.armors[i].item.itemName;
            ui.itemIndex = i;
            ui.AcqurieItem();
            Button.onClick.AddListener(() => ui.OnClickEquip(equipButtons[1].gameObject));
            Button.onClick.AddListener(() => ViewItemInfo(1));
            Button.gameObject.SetActive(false);
            armorButtons.Add(Button);
        }
    }

    private void RingButtonCreate()
    {
        int customRingLeng = inventory.customRings.Count;
        int index = 0;

        if (customRingLeng <= 0)
            return;

        if (customRingLeng >= customRingButtons.Count)
            index = customRingButtons.Count;
        else if(customRingLeng<customRingButtons.Count)
        {
            for(int i=customRingButtons.Count-1; i>=0; i--)
            {
                customRingButtons.RemoveAt(i);
            }
        }

        for (int i = index; i < customRingLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Ring;
            ui.itemname.text = inventory.customRings[i].item.item.itemName;
            ui.itemIndex = i;
            ui.AcqurieItem();
            Button.onClick.AddListener(() => ui.OnClickEquip(equipButtons[2].gameObject));
            Button.onClick.AddListener(() => ViewItemInfo(2));
            Button.gameObject.SetActive(false);
            customRingButtons.Add(Button);
        }
    }

    private void SymbolButtonCreate()
    {
        int customSymbolLeng = inventory.customSymbols.Count;
        int index = 0;

        if (customSymbolLeng <= 0)
            return;

        if (customSymbolLeng >= customSymbolButtons.Count)
            index = customSymbolButtons.Count;
        else if (customSymbolLeng < customSymbolButtons.Count)
        {
            for (int i = customSymbolButtons.Count-1; i >= 0; i--)
            {
                customSymbolButtons.RemoveAt(i);
            }
        }

        for (int i = index; i < customSymbolLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Symbol;
            ui.itemname.text = inventory.customSymbols[i].item.item.itemName;
            ui.itemIndex = i;
            ui.AcqurieItem();
            Button.onClick.AddListener(() => ui.OnClickEquip(equipButtons[3].gameObject));
            Button.onClick.AddListener(() => ViewItemInfo(3));
            Button.gameObject.SetActive(false);
            customSymbolButtons.Add(Button);
        }
    }

    public void AddRing(int itemIndex, int baseIndex)
    {
        var inventory = InventorySystem.Instance.inventory;
        Button Button = Instantiate(itemPrefabs, itemScroll.transform);
        var ui = Button.GetComponent<ItemButton>();
        ui.inventory = inventory;
        ui.type = ItemType.Ring;
        ui.itemname.text = inventory.rings[baseIndex].item.itemName;
        ui.itemIndex = itemIndex;
        ui.AcqurieItem();
        Button.onClick.AddListener(() => ui.OnClickEquip(equipButtons[2].gameObject));
        Button.gameObject.SetActive(false);
        customRingButtons.Add(Button);
        GetTypeEvent(ItemType.Ring);
    }

    public void AddSymbol(int itemIndex, int baseIndex) 
    {
        var inventory = InventorySystem.Instance.inventory;
        Button Button = Instantiate(itemPrefabs, itemScroll.transform);
        var ui = Button.GetComponent<ItemButton>();
        ui.inventory = inventory;
        ui.type = ItemType.Symbol;
        ui.itemname.text = inventory.symbols[baseIndex].item.itemName;
        ui.itemIndex = itemIndex;
        ui.AcqurieItem();
        Button.onClick.AddListener(() => ui.OnClickEquip(equipButtons[3].gameObject));
        Button.gameObject.SetActive(false);
        customSymbolButtons.Add(Button);
        GetTypeEvent(ItemType.Symbol);
    }

    public void ViewItemInfo(int equipIndex)
    {
        if (equipButtons[equipIndex] == null)
            return;

        var item = equipButtons[equipIndex].GetComponent<EquipButton>();

        if (item.itemIndex<0)
        {
            itemText.text = "";
            return;
        }

        var text = new StringBuilder();

        text.AppendLine();
        text.AppendLine("Option");
        text.AppendLine();

        switch(item.type)
        {
            case ItemType.Weapon:
                {
                    var equipItem = inventory.weapons[item.itemIndex];

                    foreach (var option in equipItem.item.options)
                    {
                        text.AppendLine($"{option.option} : {option.value + option.upgradeValue * equipItem.upgradeLev}");
                    }
                }
                break;
            case ItemType.Armor:
                {
                    var equipItem = inventory.armors[item.itemIndex];

                    foreach (var option in equipItem.item.options)
                    {
                        text.AppendLine($"{option.option} : {option.value + option.upgradeValue * equipItem.upgradeLev}");
                    }
                }
                break;
            case ItemType.Ring:
                {
                    var equipItem = inventory.customRings[item.itemIndex].item;

                    foreach (var option in equipItem.item.options)
                    {
                        text.AppendLine($"{option.option} : {option.value + option.upgradeValue * equipItem.upgradeLev}");
                    }
                }
                    break;
            case ItemType.Symbol:
                {
                    var equipItem = inventory.customSymbols[item.itemIndex].item;

                    foreach (var option in equipItem.item.options)
                    {
                        text.AppendLine($"{option.option} : {option.value + option.upgradeValue * equipItem.upgradeLev}");
                    }
                }
                    break;
        }


        itemText.text = text.ToString();
    }

    public void ClearItemInfo()
    {
        itemText.ClearMesh();
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

        foreach(var equip in equipButtons)
        {
            var equipUi = equip.GetComponent<EquipButton>();
            if(equipUi.type == ItemType.Weapon)
            {
                equip.gameObject.SetActive(true);
                //ViewItemInfo(0);
                continue;
            }

            equip.gameObject.SetActive(false);
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

        foreach (var equip in equipButtons)
        {
            var equipUi = equip.GetComponent<EquipButton>();
            if (equipUi.type == ItemType.Armor)
            {
                equip.gameObject.SetActive(true);
                //ViewItemInfo(1);
                continue;
            }

            equip.gameObject.SetActive(false);
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

        foreach (var equip in equipButtons)
        {
            var equipUi = equip.GetComponent<EquipButton>();
            if (equipUi.type == ItemType.Ring)
            {
                equip.gameObject.SetActive(true);
                //ViewItemInfo(2);
                continue;
            }

            equip.gameObject.SetActive(false);
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

        foreach (var equip in equipButtons)
        {
            var equipUi = equip.GetComponent<EquipButton>();
            if (equipUi.type == ItemType.Symbol)
            {
                equip.gameObject.SetActive(true);
                //ViewItemInfo(3);
                continue;
            }

            equip.gameObject.SetActive(false);
        }

        foreach (var symbol in customSymbolButtons)
        {
            symbol.gameObject.SetActive(true);
        }
    }
}
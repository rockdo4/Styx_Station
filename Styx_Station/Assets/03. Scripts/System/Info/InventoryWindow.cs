using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : SubWindow
{
    public ItemType currentType;
 
    public GameObject weapons;
    public GameObject armors;
    public GameObject rings;
    public GameObject symbols;

    public Button weaponSlot;
    public Button armorSlot;
    public Button ringSlot;
    public Button symbolSlot;

    private List<Button> weaponButtons = new List<Button>();
    private List<Button> armorButtons = new List<Button>();
    private List<Button> customRingButtons = new List<Button>();
    private List<Button> customSymbolButtons = new List<Button>();

    public override void Open()
    {
        base.Open();

        foreach(var weapon in weaponButtons)
        {
            var button = weapon.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }

        foreach(var armor in armorButtons)
        {
            var button = armor.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }

        foreach (var ring in customRingButtons)
        {
            var button = ring.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }

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

    private void Awake()
    {
        var inventory = InventorySystem.Instance.inventory;

        Button equipWeapon = UIManager.Instance.windows[0].GetComponent<InfoWindow>().equipButtons[0];
        Button equipArmor = UIManager.Instance.windows[0].GetComponent<InfoWindow>().equipButtons[1];
        Button equipRing = UIManager.Instance.windows[0].GetComponent<InfoWindow>().equipButtons[2];
        Button equipSymbol = UIManager.Instance.windows[0].GetComponent<InfoWindow>().equipButtons[3];

        for (int i = 0; i<inventory.weapons.Count;++i)
        {
            Button button = Instantiate(weaponSlot, weapons.transform);
            button.AddComponent<ItemButton>();

            var ui = button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Weapon;
            ui.itemIndex = i;
            ui.image = button.transform.GetChild(0).gameObject;
            ui.itemExp = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            ui.itemLv = button.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            button.onClick.AddListener(() => ui.OnClickEquip(equipWeapon.gameObject));
            weaponButtons.Add(button);
        }

        for(int i = 0; i< inventory.armors.Count;++i)
        {
            Button button = Instantiate (armorSlot, armors.transform);
            button.AddComponent<ItemButton>();

            var ui = button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Armor;
            ui.itemIndex = i;
            ui.image = button.transform.GetChild(0).gameObject;
            ui.itemExp = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            ui.itemLv = button.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            button.onClick.AddListener(() => ui.OnClickEquip(equipArmor.gameObject));
            armorButtons.Add(button);
        }

        for (int i = 0;i<inventory.customRings.Count;++i)
        {
            Button button = Instantiate(ringSlot, rings.transform);
            button.AddComponent<ItemButton>();

            var ui = button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Ring;
            ui.itemIndex = i;
            ui.image = button.transform.GetChild(0).gameObject;
            ui.itemLv = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            button.onClick.AddListener(() => ui.OnClickEquip(equipRing.gameObject));
            customRingButtons.Add(button);
        }

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

            button.onClick.AddListener(() => ui.OnClickEquip(equipSymbol.gameObject));
            customSymbolButtons.Add(button);
        }
    }
}

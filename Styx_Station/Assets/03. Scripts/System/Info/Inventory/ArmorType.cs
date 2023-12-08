using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArmorType : InventoryType
{
    private Inventory inventory;

    public GameObject armors;

    public Button armorSlot;

    public GameObject info;

    public int selectIndex = -1;

    private List<Button> armorButtons = new List<Button>();

    public override void Open()
    {
        base.Open();

        foreach (var armor in armorButtons)
        {
            var button = armor.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }

        for (int i = 0; i < armorButtons.Count; ++i)
        {
            var button = armorButtons[i].GetComponent<ItemButton>();
            if (!inventory.armors[i].acquire)
            {
                Color currentColor = button.image.GetComponent<Image>().color;
                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0.3f);
                button.image.GetComponent<Image>().color = newColor;
            }
            else
            {
                Color currentColor = button.image.GetComponent<Image>().color;
                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
                button.image.GetComponent<Image>().color = newColor;
            }
        }
    }

    public override void Close()
    {
        OnClickCloseArmorInfo();

        base.Close();
    }

    public void OnClickCloseArmorInfo()
    {
        selectIndex = -1;
        info.SetActive(false);

        foreach (var armor in armorButtons)
        {
            var button = armor.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }
    }

    public void Setting(Inventory inventory)
    {
        this.inventory = inventory;

        info.GetComponent<ArmorEquipInfoUi>().Inventory();

        for (int i = 0; i < inventory.armors.Count; ++i)
        {
            Button button = Instantiate(armorSlot, armors.transform);
            button.AddComponent<ItemButton>();

            var ui = button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Armor;
            ui.itemIndex = i;
            ui.image = button.transform.GetChild(0).gameObject;
            ui.itemExp = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            ui.itemLv = button.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            button.onClick.AddListener(() => ui.OnClickArmorOpenInfo(this));
            armorButtons.Add(button);
        }
    }
}

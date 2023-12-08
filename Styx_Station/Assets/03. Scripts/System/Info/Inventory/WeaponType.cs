using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponType : InventoryType
{
    private Inventory inventory;

    public GameObject weapons;

    public Button weaponSlot;

    public GameObject info;

    public int selectIndex = -1;

    private List<Button> weaponButtons = new List<Button>();

    public override void Open()
    {
        base.Open();

        foreach (var weapon in weaponButtons)
        {
            var button = weapon.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }

        for (int i = 0; i < weaponButtons.Count; ++i)
        {
            var button = weaponButtons[i].GetComponent<ItemButton>();
            if (!inventory.weapons[i].acquire)
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
        OnClickCloseWeaponInfo();

        base.Close();
    }

    public void OnClickCloseWeaponInfo()
    {
        selectIndex = -1;
        info.SetActive(false);
        foreach (var weapon in weaponButtons)
        {
            var button = weapon.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }
    }

    public void Setting(Inventory inventory)
    {
        this.inventory = inventory;

        info.GetComponent<WeaponEquipInfoUi>().Inventory();

        for (int i = 0; i < this.inventory.weapons.Count; ++i)
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

            button.onClick.AddListener(() => ui.OnClickWeaponOpenInfo(this));
            weaponButtons.Add(button);
        }
    }
}

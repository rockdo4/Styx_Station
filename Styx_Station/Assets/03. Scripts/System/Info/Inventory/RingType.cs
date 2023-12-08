using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RingType : InventoryType
{
    public GameObject rings;

    public Button ringSlot;

    private List<Button> customRingButtons = new List<Button>();

    public override void Open()
    {
        base.Open();

        foreach (var ring in customRingButtons)
        {
            var button = ring.GetComponent<ItemButton>();
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
            Button button = Instantiate(ringSlot, rings.transform);
            button.AddComponent<ItemButton>();

            var ui = button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Ring;
            ui.itemIndex = i;
            ui.image = button.transform.GetChild(0).gameObject;
            ui.itemLv = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            //button.onClick.AddListener(() => ui.OnClickEquip(equipRing.gameObject));
            customRingButtons.Add(button);
        }
    }
}

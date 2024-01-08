using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RingType : InventoryType
{
    private Inventory inventory;

    public GameObject rings;

    public Button ringSlot;

    public GameObject info;

    public int selectIndex = -1;

    public List<Button> customRingButtons { get; private set; } = new List<Button>();

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
        OnClickCloseRingInfo();

        base.Close();
    }

    public void OnClickCloseRingInfo()
    {
        selectIndex = -1;
        info.SetActive(false);

        foreach (var ring in customRingButtons)
        {
            var button = ring.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }

        if ((ButtonList.infoButton & InfoButton.RingInfo) != 0)
            ButtonList.infoButton &= ~InfoButton.RingInfo;
    }

    public void infoUpdate()
    {
        foreach (var ring in customRingButtons)
        {
            var button = ring.GetComponent<ItemButton>();
            if (button == null)
                continue;

            button.InfoUpdate();
        }
    }

    public void Setting(Inventory inventory)
    {
        this.inventory = inventory;
        info.GetComponent<RingEquipInfoUi>().Inventory();

        for (int i = 0; i < inventory.customRings.Count; ++i)
        {
            Button button = Instantiate(ringSlot, rings.transform);
            button.AddComponent<ItemButton>();

            var ui = button.GetComponent<ItemButton>();
            ui.inventory = inventory;
            ui.type = ItemType.Ring;
            button.name = i.ToString();
            ui.itemIndex = i;
            ui.image = button.transform.GetChild(0).gameObject;
            ui.itemLv = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            button.onClick.AddListener(() => ui.OnClickRingOpenInfo(this));
            customRingButtons.Add(button);
        }
    }

    public void AddRing()
    {
        Button button = Instantiate(ringSlot, rings.transform);
        button.AddComponent<ItemButton>();

        var ui = button.GetComponent<ItemButton>();
        ui.inventory = inventory;
        ui.type = ItemType.Ring;
        ui.itemIndex = customRingButtons.Count;
        button.name = customRingButtons.Count.ToString();
        ui.image = button.transform.GetChild(0).gameObject;
        ui.itemLv = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        button.onClick.AddListener(() => ui.OnClickRingOpenInfo(this));
        customRingButtons.Add(button);
    }
}

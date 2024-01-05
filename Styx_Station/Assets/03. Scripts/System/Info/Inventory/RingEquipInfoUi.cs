using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RingEquipInfoUi : MonoBehaviour
{
    private Inventory inventory;
    private StateSystem state;
    private InfoWindow info;

    public int selectIndex;

    public GameObject itemImage;
    public GameObject outBox;

    public TextMeshProUGUI lev;
    public TextMeshProUGUI tier;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemText;
    public Button equip;
    public Button ringBraek;
    public Button upgrade;

    public void Inventory()
    {
        inventory = InventorySystem.Instance.inventory;
        state = StateSystem.Instance;
        info = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>();
    }

    public void InfoUpdate()
    {
        if (inventory.customRings.Count < 1)
            return;

        var ring = inventory.customRings[selectIndex];

        if (ring.item.acquire)
            equip.interactable = true;

        else if (!ring.item.acquire)
            equip.interactable = false;

        if (MakeTableData.Instance.stringTable == null)
            MakeTableData.Instance.stringTable = new StringTable();

        var stringTable = MakeTableData.Instance.stringTable;

        if(Global.language == Language.KOR)
        {
            if (ring.item.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Dequip").KOR}";
            }
            else if (!ring.item.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Equip").KOR}";
            }
            upgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Upgrade").KOR}";
            ringBraek.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Ring001").KOR}";
            tier.text = $"{stringTable.GetStringTableData(ring.item.item.tier.ToString()).KOR}";
            itemName.text = $"{stringTable.GetStringTableData(ring.copyData.name + "_Name").KOR}";

            StringBuilder infoText = new StringBuilder();
            string text = string.Format(stringTable.GetStringTableData("Ring_Status_0").KOR, 1);
            string option = string.Format("{0:F2}", ring.item.item.options[0].value + ring.item.upgradeLev * ring.item.item.options[0].upgradeValue);
            infoText.Append(text);
            text = string.Format(stringTable.GetStringTableData("Ring_Status_1").KOR, option);
            infoText.Append(text);
            infoText.AppendLine();
            for(int i = 0; i<ring.item.item.addOptions.Count; ++i) 
            {
                text = string.Format(stringTable.GetStringTableData("Ring_Status_0").KOR, i+2);
                infoText.Append(text);
                switch(ring.item.item.addOptions[i].option)
                {
                    case AddOptionString.AttackPer:
                        option = string.Format("{0:F2}", ring.item.item.addOptions[i].value + ring.item.upgradeLev * ring.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Ring_Status_1").KOR, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.Bloodsucking:
                        option = string.Format("{0:F2}", ring.item.item.addOptions[i].value + ring.item.upgradeLev * ring.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Ring_Status_2").KOR, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.BossDamage:
                        option = string.Format("{0:F2}", ring.item.item.addOptions[i].value + ring.item.upgradeLev * ring.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Ring_Status_3").KOR, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.SkillDamage:
                        option = string.Format("{0:F2}", ring.item.item.addOptions[i].value + ring.item.upgradeLev * ring.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Ring_Status_4").KOR, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;
                }
            }

            itemText.text = $"{infoText} {stringTable.GetStringTableData(ring.copyData.name + "_Info").KOR}";
        }
        else if(Global.language == Language.ENG)
        {
            if (ring.item.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Dequip").ENG}";
            }
            else if (!ring.item.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Equip").ENG}";
            }
            upgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Upgrade").ENG}";
            ringBraek.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Ring001").ENG}";
            tier.text = $"{stringTable.GetStringTableData(ring.item.item.tier.ToString()).ENG}";
            itemName.text = $"{stringTable.GetStringTableData(ring.copyData.name + "_Name").ENG}";
            StringBuilder infoText = new StringBuilder();
            string text = string.Format(stringTable.GetStringTableData("Ring_Status_0").ENG, 1);
            string option = string.Format("{0:F2}", ring.item.item.options[0].value + ring.item.upgradeLev * ring.item.item.options[0].upgradeValue);
            infoText.Append(text);
            text = string.Format(stringTable.GetStringTableData("Ring_Status_1").ENG, option);
            infoText.Append(text);
            infoText.AppendLine();
            for (int i = 0; i < ring.item.item.addOptions.Count; ++i)
            {
                text = string.Format(stringTable.GetStringTableData("Ring_Status_0").ENG, i + 2);
                infoText.Append(text);
                switch (ring.item.item.addOptions[i].option)
                {
                    case AddOptionString.AttackPer:
                        option = string.Format("{0:F2}", ring.item.item.addOptions[i].value + ring.item.upgradeLev * ring.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Ring_Status_1").ENG, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.Bloodsucking:
                        option = string.Format("{0:F2}", ring.item.item.addOptions[i].value + ring.item.upgradeLev * ring.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Ring_Status_2").ENG, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.BossDamage:
                        option = string.Format("{0:F2}", ring.item.item.addOptions[i].value + ring.item.upgradeLev * ring.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Ring_Status_3").ENG, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.SkillDamage:
                        option = string.Format("{0:F2}", ring.item.item.addOptions[i].value + ring.item.upgradeLev * ring.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Ring_Status_4").ENG, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;
                }
            }

            itemText.text = $"{infoText} {stringTable.GetStringTableData(ring.copyData.name + "_Info").ENG}";
        }

        lev.text = $"Lv.{ring.item.upgradeLev}";
        upgrade.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{ring.item.item.itemLevUpNum[ring.item.upgradeLev]}";

        switch (ring.item.item.tier)
        {
            case Tier.Common:
                ringBraek.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "50";
                break;

            case Tier.Uncommon:
                ringBraek.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "150";
                break;

            case Tier.Rare:
                ringBraek.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "450";
                break;

            case Tier.Unique:
                ringBraek.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "1350";
                break;

            case Tier.Legendry:
                ringBraek.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "4050";
                break;
        } 
    }

    public void OnClickRingEquip()
    {
        if (!inventory.customRings[selectIndex].item.acquire)
            return;

        var equip = info.equipButtons[2];

        if (equip == null)
            return;

        var item = inventory.GetEquipItem(2);

        if (item == null)
        {
            inventory.EquipItem(selectIndex, ItemType.Ring);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.customRings[selectIndex].item.item.itemIcon;
            AlphaChange(equip, true);
            state.EquipUpdate();
            state.TotalUpdate();
            InfoUpdate();
            info.InfoTextUpdate();
            return;
        }

        if (item.index != selectIndex)
        {
            inventory.DequipItem(item, ItemType.Ring);
            AlphaChange(equip, false);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = null;
        }
        else if (item.index == selectIndex)
        {
            inventory.DequipItem(item, ItemType.Ring);
            AlphaChange(equip, false);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = null;
            state.EquipUpdate();
            state.TotalUpdate();
            InfoUpdate();
            info.InfoTextUpdate();
            return;
        }

        inventory.EquipItem(selectIndex, ItemType.Ring);
        equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.customRings[selectIndex].item.item.itemIcon;
        AlphaChange(equip, true);
        state.EquipUpdate();
        state.TotalUpdate();
        InfoUpdate();
        info.InfoTextUpdate();
    }

    private void AlphaChange(Button button, bool value)
    {
        Color color = new Color();
        switch (value)
        {
            case true:
                {
                    color = new Color(1f, 1f, 1f, 1f);
                    button.transform.GetChild(0).GetComponent<Image>().color = color;
                }
                break;

            case false:
                {
                    color = new Color(1f, 1f, 1f, 0f);
                    button.transform.GetChild(0).GetComponent<Image>().color = color;
                }
                break;
        }
    }

    public void OnClickUpgrade()
    {
        gameObject.GetComponent<Upgrade>().ItemUpgrade(selectIndex, ItemType.Ring);

        state.EquipUpdate();
        state.TotalUpdate();
        InfoUpdate();
        info.InfoTextUpdate();
        info.inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[2].GetComponent<RingType>().infoUpdate();
    }

    public void OnClickBreak()
    {
        var ring = inventory.customRings[selectIndex];

        if (ring == null)
            return;

        if (ring.item.equip)
            return;

        inventory.BreakRing(ring);

        var ringType = info.inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[2].GetComponent<RingType>();
        var obj = ringType.customRingButtons[selectIndex].GetComponent<Button>();

        ringType.customRingButtons.Remove(obj);

        for (int i = 0; i < inventory.customRings.Count; ++i)
        {
            var ui = ringType.customRingButtons[i].GetComponent<ItemButton>();

            if (ui == null)
                continue;

            ui.name = i.ToString();
            ui.itemIndex = i;
        }

        ringType.OnClickCloseRingInfo();

        Destroy(obj.gameObject);

        state.EquipUpdate();
        state.TotalUpdate();
        info.InfoTextUpdate();
    }
}

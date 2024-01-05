using System.Net.NetworkInformation;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SymbolEquipInfoUi : MonoBehaviour
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
    public Button symbolBraek;
    public Button upgrade;

    public void Inventory()
    {
        inventory = InventorySystem.Instance.inventory;
        state = StateSystem.Instance;
        info = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>();
    }

    public void InfoUpdate()
    {
        if (inventory.customSymbols.Count < 1)
            return;

        var symbol = inventory.customSymbols[selectIndex];

        if (symbol.item.acquire)
            equip.interactable = true;

        else if (!symbol.item.acquire)
            equip.interactable = false;


        if (MakeTableData.Instance.stringTable == null)
            MakeTableData.Instance.stringTable = new StringTable();

        var stringTable = MakeTableData.Instance.stringTable;


        if (Global.language == Language.KOR)
        {
            if (symbol.item.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Dequip").KOR}";
            }
            else if (!symbol.item.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Equip").KOR}";
            }
            upgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Upgrade").KOR}";
            symbolBraek.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Ring001").KOR}";
            tier.text = $"{stringTable.GetStringTableData(symbol.item.item.tier.ToString()).KOR}";
            itemName.text = $"{stringTable.GetStringTableData(symbol.copyData.name + "_Name").KOR}";

            StringBuilder infoText = new StringBuilder();
            string text = string.Format(stringTable.GetStringTableData("Symbol_Status_0").KOR, 1);
            string option = string.Format("{0:F2}", symbol.item.item.options[0].value + symbol.item.upgradeLev * symbol.item.item.options[0].upgradeValue);
            infoText.Append(text);
            text = string.Format(stringTable.GetStringTableData("Symbol_Status_1").KOR, option);
            infoText.Append(text);
            infoText.AppendLine();
            for (int i = 0; i < symbol.item.item.addOptions.Count; ++i)
            {
                text = string.Format(stringTable.GetStringTableData("Symbol_Status_0").KOR, i + 2);
                infoText.Append(text);
                switch (symbol.item.item.addOptions[i].option)
                {
                    case AddOptionString.HealthPer:
                        option = string.Format("{0:F2}", symbol.item.item.addOptions[i].value + symbol.item.upgradeLev * symbol.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Symbol_Status_1").KOR, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.Evade:
                        option = string.Format("{0:F2}", symbol.item.item.addOptions[i].value + symbol.item.upgradeLev * symbol.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Symbol_Status_2").KOR, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.DamageReduction:
                        option = string.Format("{0:F2}", symbol.item.item.addOptions[i].value + symbol.item.upgradeLev * symbol.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Symbol_Status_3").KOR, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.CoinAcquire:
                        option = string.Format("{0:F2}", symbol.item.item.addOptions[i].value + symbol.item.upgradeLev * symbol.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Symbol_Status_4").KOR, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;
                }
            }

            itemText.text = $"{infoText} {stringTable.GetStringTableData(symbol.copyData.name + "_Info").KOR}";
        }
        else if (Global.language == Language.ENG)
        {
            if (symbol.item.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Dequip").ENG}";
            }
            else if (!symbol.item.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Equip").ENG}";
            }
            upgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Upgrade").ENG}";
            symbolBraek.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Ring001").ENG}";
            tier.text = $"{stringTable.GetStringTableData(symbol.item.item.tier.ToString()).ENG}";
            itemName.text = $"{stringTable.GetStringTableData(symbol.copyData.name + "_Name").ENG}";
            StringBuilder infoText = new StringBuilder();
            string text = string.Format(stringTable.GetStringTableData("Symbol_Status_0").ENG, 1);
            string option = string.Format("{0:F2}", symbol.item.item.options[0].value + symbol.item.upgradeLev * symbol.item.item.options[0].upgradeValue);
            infoText.Append(text);
            text = string.Format(stringTable.GetStringTableData("Symbol_Status_1").ENG, option);
            infoText.Append(text);
            infoText.AppendLine();
            for (int i = 0; i < symbol.item.item.addOptions.Count; ++i)
            {
                text = string.Format(stringTable.GetStringTableData("Symbol_Status_0").ENG, i + 2);
                infoText.Append(text);
                switch (symbol.item.item.addOptions[i].option)
                {
                    case AddOptionString.HealthPer:
                        option = string.Format("{0:F2}", symbol.item.item.addOptions[i].value + symbol.item.upgradeLev * symbol.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Symbol_Status_1").ENG, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.Evade:
                        option = string.Format("{0:F2}", symbol.item.item.addOptions[i].value + symbol.item.upgradeLev * symbol.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Symbol_Status_2").ENG, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.DamageReduction:
                        option = string.Format("{0:F2}", symbol.item.item.addOptions[i].value + symbol.item.upgradeLev * symbol.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Symbol_Status_3").ENG, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;

                    case AddOptionString.CoinAcquire:
                        option = string.Format("{0:F2}", symbol.item.item.addOptions[i].value + symbol.item.upgradeLev * symbol.item.item.addOptions[i].upgradeValue);
                        text = string.Format(stringTable.GetStringTableData("Symbol_Status_4").ENG, option);
                        infoText.Append(text);
                        infoText.AppendLine();
                        break;
                }

            }
            itemText.text = $"{infoText} {stringTable.GetStringTableData(symbol.copyData.name + "_Info").ENG}";


            }
        upgrade.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{symbol.item.item.itemLevUpNum[symbol.item.upgradeLev]}";

        switch (symbol.item.item.tier)
        {
            case Tier.Common:
                symbolBraek.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "50";
                break;

            case Tier.Uncommon:
                symbolBraek.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "150";
                break;

            case Tier.Rare:
                symbolBraek.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "450";
                break;

            case Tier.Unique:
                symbolBraek.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "1350";
                break;

            case Tier.Legendry:
                symbolBraek.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "4050";
                break;
        }
    }

    public void OnClickSymbolEquip()
    {
        if (!inventory.customSymbols[selectIndex].item.acquire)
            return;

        var equip = info.equipButtons[3];

        if (equip == null)
            return;

        var item = inventory.GetEquipItem(3);

        if (item == null)
        {
            inventory.EquipItem(selectIndex, ItemType.Symbol);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.customSymbols[selectIndex].item.item.itemIcon;
            AlphaChange(equip, true);
            state.EquipUpdate();
            state.TotalUpdate();
            InfoUpdate();
            info.InfoTextUpdate();
            return;
        }

        if (item.index != selectIndex)
        {
            inventory.DequipItem(item, ItemType.Symbol);
            AlphaChange(equip, false);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = null;
        }
        else if (item.index == selectIndex)
        {
            inventory.DequipItem(item, ItemType.Symbol);
            AlphaChange(equip, false);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = null;
            state.EquipUpdate();
            state.TotalUpdate();
            InfoUpdate();
            info.InfoTextUpdate();
            return;
        }

        inventory.EquipItem(selectIndex, ItemType.Symbol);
        equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.customSymbols[selectIndex].item.item.itemIcon;
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
        gameObject.GetComponent<Upgrade>().ItemUpgrade(selectIndex, ItemType.Symbol);

        state.EquipUpdate();
        state.TotalUpdate();
        InfoUpdate();
        info.InfoTextUpdate(); 
        info.inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[3].GetComponent<SymbolType>().infoUpdate();
    }

    public void OnClickBreak()
    {
        var symbol = inventory.customSymbols[selectIndex];

        if (symbol == null)
            return;

        if (symbol.item.equip)
            return;

        inventory.BreakSymbol(symbol);

        var symbolType = info.inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[3].GetComponent<SymbolType>();
        var obj = symbolType.customSymbolButtons[selectIndex].GetComponent<Button>();

        symbolType.customSymbolButtons.Remove(obj);


        for (int i = 0; i < inventory.customSymbols.Count; ++i)
        {
            var ui = symbolType.customSymbolButtons[i].GetComponent<ItemButton>();

            if (ui == null)
                continue;

            ui.name = i.ToString();
            ui.itemIndex = i;
        }

        symbolType.OnClickCloseSymbolInfo();

        Destroy(obj.gameObject);

        state.EquipUpdate();
        state.TotalUpdate();
        info.InfoTextUpdate();
    }
}

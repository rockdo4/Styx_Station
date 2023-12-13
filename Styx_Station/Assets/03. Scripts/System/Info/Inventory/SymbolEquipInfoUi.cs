using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SymbolEquipInfoUi : MonoBehaviour
{
    private Inventory inventory;

    public int selectIndex;

    public GameObject itemImage;
    public GameObject outBox;

    public TextMeshProUGUI lev;
    public TextMeshProUGUI tier;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemText;
    public Button equip;
    public Button ringBraek;
    public Button upragde;

    public void Inventory()
    {
        inventory = InventorySystem.Instance.inventory;
    }

    public void InfoUpdate()
    {
        var symbol = inventory.customSymbols[selectIndex].item;

        if (symbol.acquire)
            equip.interactable = true;

        else if (!symbol.acquire)
            equip.interactable = false;

        if (symbol.upgradeLev < symbol.item.itemLevUpNum.Count)
            lev.text = $"Lv.{symbol.upgradeLev}\n\n({CurrencyManager.itemAsh} / {symbol.item.itemLevUpNum[symbol.upgradeLev]})";

        else
            lev.text = $"Lv.{symbol.upgradeLev}\n\n({CurrencyManager.itemAsh} / {symbol.item.itemLevUpNum[symbol.item.itemLevUpNum.Count - 1]})";
    }

    public void OnClickRingEquip()
    {
        if (!inventory.customSymbols[selectIndex].item.acquire)
            return;

        var equip = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().equipButtons[3];

        if (equip == null)
            return;

        var item = inventory.GetEquipItem(3);

        if (item == null)
        {
            inventory.EquipItem(selectIndex, ItemType.Symbol);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.customSymbols[selectIndex].item.item.itemIcon;
            AlphaChange(equip, true);
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
            return;
        }

        inventory.EquipItem(selectIndex, ItemType.Symbol);
        equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.customSymbols[selectIndex].item.item.itemIcon;
        AlphaChange(equip, true);
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

        InfoUpdate();
    }
}

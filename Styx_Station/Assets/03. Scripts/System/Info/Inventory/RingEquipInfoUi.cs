using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RingEquipInfoUi : MonoBehaviour
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
        var ring = inventory.customRings[selectIndex].item;

        if (ring.acquire)
            equip.interactable = true;

        else if (!ring.acquire)
            equip.interactable = false;

        if (ring.upgradeLev < ring.item.itemLevUpNum.Count)
            lev.text = $"Lv.{ring.upgradeLev}\n\n({CurrencyManager.itemAsh} / {ring.item.itemLevUpNum[ring.upgradeLev]})";

        else
            lev.text = $"Lv.{ring.upgradeLev}\n\n({CurrencyManager.itemAsh} / {ring.item.itemLevUpNum[ring.item.itemLevUpNum.Count-1]})";
    }

    public void OnClickRingEquip()
    {
        if (!inventory.customRings[selectIndex].item.acquire)
            return;

        var equip = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().equipButtons[2];

        if (equip == null)
            return;

        var item = inventory.GetEquipItem(2);

        if (item == null)
        {
            inventory.EquipItem(selectIndex, ItemType.Ring);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.customRings[selectIndex].item.item.itemIcon;
            AlphaChange(equip, true);
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
            return;
        }

        inventory.EquipItem(selectIndex, ItemType.Ring);
        equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.customRings[selectIndex].item.item.itemIcon;
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
        gameObject.GetComponent<Upgrade>().ItemUpgrade(selectIndex, ItemType.Ring);

        InfoUpdate();
    }
}

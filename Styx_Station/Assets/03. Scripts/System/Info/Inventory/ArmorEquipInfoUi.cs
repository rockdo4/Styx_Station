using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmorEquipInfoUi : MonoBehaviour
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
    public Button upgrade;

    public void Inventory()
    {
        inventory = InventorySystem.Instance.inventory;
    }
    public void InfoUpdate()
    {
        var armor = inventory.armors[selectIndex];

        if (armor.acquire)
            equip.interactable = true;

        else if (!armor.acquire)
            equip.interactable = false;

        if (armor.upgradeLev < armor.item.itemLevUpNum.Count)
            lev.text = $"Lv.{armor.upgradeLev}\n\n({armor.stock} / {armor.item.itemLevUpNum[armor.upgradeLev]})";

        else
            lev.text = $"Lv.{armor.upgradeLev}\n\n({armor.stock} / {armor.item.itemLevUpNum[armor.item.itemLevUpNum.Count - 1]})";
    }

    public void OnClickArmorEquip()
    {
        if (!inventory.armors[selectIndex].acquire)
            return;

        var equip = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().equipButtons[1];

        if (equip == null)
            return;

        var item = inventory.GetEquipItem(1);

        if (item == null)
        {
            inventory.EquipItem(selectIndex, ItemType.Armor);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.armors[selectIndex].item.itemIcon;
            AlphaChange(equip, true);
            return;
        }

        if (item.index != selectIndex)
        {
            inventory.DequipItem(item, ItemType.Armor);
            AlphaChange(equip, false);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = null;
        }
        else if (item.index == selectIndex)
        {
            inventory.DequipItem(item, ItemType.Armor);
            AlphaChange(equip, false);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = null;
            return;
        }

        inventory.EquipItem(selectIndex, ItemType.Armor);
        equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.armors[selectIndex].item.itemIcon;
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
        gameObject.GetComponent<Upgrade>().ItemUpgrade(selectIndex, ItemType.Armor);

        InfoUpdate();
    }
}

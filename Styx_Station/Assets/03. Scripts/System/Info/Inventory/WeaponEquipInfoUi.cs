using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEquipInfoUi : MonoBehaviour
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
        var weapon = inventory.weapons[selectIndex];

        if(weapon.acquire)
            equip.interactable = true;

        else if(!weapon.acquire)
            equip.interactable = false;

        if (weapon.upgradeLev < weapon.item.itemLevUpNum.Count)
            lev.text = $"Lv.{weapon.upgradeLev}\n\n({weapon.stock} / {weapon.item.itemLevUpNum[weapon.upgradeLev]})";

        else
            lev.text = $"Lv.{weapon.upgradeLev}\n\n({weapon.stock} / {weapon.item.itemLevUpNum[weapon.item.itemLevUpNum.Count - 1]})";
    }

    public void OnClickWeaponEquip()
    {
        if (!inventory.weapons[selectIndex].acquire)
            return;

        var equip = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().equipButtons[0];

        if (equip == null)
            return;

        var item = inventory.GetEquipItem(0);

        if (item == null)
        {
            inventory.EquipItem(selectIndex, ItemType.Weapon);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.weapons[selectIndex].item.itemIcon;
            AlphaChange(equip, true);
            return;
        }

        if (item.index != selectIndex)
        {
            inventory.DequipItem(item, ItemType.Weapon);
            AlphaChange(equip, false);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = null;
        }
        else if(item.index == selectIndex)
        {
            inventory.DequipItem(item, ItemType.Weapon);
            AlphaChange(equip, false);
            equip.transform.GetChild(0).GetComponent<Image>().sprite = null;
            return;
        }
         
        inventory.EquipItem(selectIndex, ItemType.Weapon);
        equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.weapons[selectIndex].item.itemIcon;
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
        gameObject.GetComponent<Upgrade>().ItemUpgrade(selectIndex, ItemType.Weapon);

        InfoUpdate();
    }
}

using TMPro;
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

        if (MakeTableData.Instance.stringTable == null)
            MakeTableData.Instance.stringTable = new StringTable();

        var stringTable = MakeTableData.Instance.stringTable;

        if (Global.language == Language.KOR)
        {
            if(weapon.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Dequip").KOR}";
            }
            else if(!weapon.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Equip").KOR}";
            }
            upgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Upgrade").KOR}";
            tier.text = $"{stringTable.GetStringTableData(weapon.item.tier.ToString()).KOR}";
            itemName.text = $"{stringTable.GetStringTableData(weapon.item.name + "_Name").KOR}";
            string text = string.Format(stringTable.GetStringTableData(weapon.item.name + "_Info").KOR,
                weapon.item.options[0].value + weapon.upgradeLev * weapon.item.options[0].upgradeValue,
                weapon.item.addOptions[0].value + weapon.upgradeLev * weapon.item.addOptions[0].upgradeValue);
            itemText.text = $"{text}";
        }
        else if(Global.language == Language.ENG)
        {
            if (weapon.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Dequip").ENG}";
            }
            else if (!weapon.equip)
            {
                equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Equip").ENG}";
            }
            upgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Upgrade").ENG}";
            tier.text = $"{stringTable.GetStringTableData(weapon.item.tier.ToString()).ENG}";
            itemName.text = $"{stringTable.GetStringTableData(weapon.item.name + "_Name").ENG}";
            string text = string.Format(stringTable.GetStringTableData(weapon.item.name + "_Info").ENG,
                weapon.item.options[0].value + weapon.upgradeLev * weapon.item.options[0].upgradeValue,
                weapon.item.addOptions[0].value + weapon.upgradeLev * weapon.item.addOptions[0].upgradeValue);
            itemText.text = $"{text}";
        }
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
            InfoUpdate();
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
            InfoUpdate();
            return;
        }
         
        inventory.EquipItem(selectIndex, ItemType.Weapon);
        equip.transform.GetChild(0).GetComponent<Image>().sprite = inventory.weapons[selectIndex].item.itemIcon;
        AlphaChange(equip, true);
        InfoUpdate();
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

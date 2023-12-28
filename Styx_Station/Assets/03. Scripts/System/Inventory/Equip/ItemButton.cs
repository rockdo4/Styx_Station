using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Inventory inventory;

    public GameObject image;

    public TextMeshProUGUI itemExp;
    public TextMeshProUGUI itemLv;

    public ItemType type;
    public int itemIndex;

    public bool AcqurieItem()
    {
        bool acquire = false;

        switch(type)
        {
            case ItemType.Weapon:
                acquire = inventory.weapons[itemIndex].acquire;
                break;
            case ItemType.Armor:
                acquire = inventory.armors[itemIndex].acquire;
                break;
            case ItemType.Ring:
                acquire = inventory.customRings[itemIndex].item.acquire;
                break;
            case ItemType.Symbol:
                acquire = inventory.customSymbols[itemIndex].item.acquire;
                break;
        }

        if (!acquire)
        {
            gameObject.GetComponent<Image>().color = Color.red;
            return false;
        }

        gameObject.GetComponent<Image>().color = Color.white;
        return true;
    }

    public void InfoUpdate()
    {
        switch (type)
        {
            case ItemType.Weapon:
                {
                    var item = inventory.weapons[itemIndex];
                    image.GetComponent<Image>().sprite = item.item.itemIcon;
                    itemLv.text = $"Lv.{item.upgradeLev}";
                    if(item.upgradeLev<item.item.itemLevUpNum.Count)
                        itemExp.text = $"{item.stock} / {item.item.itemLevUpNum[item.upgradeLev]}";     
                    else
                        itemExp.text = $"{item.stock} / {item.item.itemLevUpNum[item.item.itemLevUpNum.Count-1]}";
                }
                break;

            case ItemType.Armor:
                {
                    var item = inventory.armors[itemIndex];
                    image.GetComponent<Image>().sprite = item.item.itemIcon;
                    itemLv.text = $"Lv.{item.upgradeLev}";
                    if (item.upgradeLev < item.item.itemLevUpNum.Count)
                        itemExp.text = $"{item.stock} / {item.item.itemLevUpNum[item.upgradeLev]}";
                    else
                        itemExp.text = $"{item.stock} / {item.item.itemLevUpNum[item.item.itemLevUpNum.Count - 1]}";
                }
                break;

            case ItemType.Ring:
                {
                    var item = inventory.customRings[itemIndex].item;
                    image.GetComponent<Image>().sprite = item.item.itemIcon;
                    itemLv.text = $"Lv.{item.upgradeLev}";
                }
                break;

            case ItemType.Symbol:
                {
                    var item = inventory.customRings[itemIndex].item;
                    image.GetComponent<Image>().sprite = item.item.itemIcon;
                    itemLv.text = $"Lv.{item.upgradeLev}";
                }
                break;
        }
    }

    public void OnClickWeaponOpenInfo(WeaponType window)
    {
        if (window == null)
            return;

        if ((ButtonList.infoButton & InfoButton.WeaponInfo) != 0)
        {
            return;
        }

        if ((ButtonList.infoButton & InfoButton.ArmorInfo) != 0)
            ButtonList.infoButton &= ~InfoButton.ArmorInfo;

        if ((ButtonList.infoButton & InfoButton.RingInfo) != 0)
            ButtonList.infoButton &= ~InfoButton.RingInfo;

        if ((ButtonList.infoButton & InfoButton.SymbolInfo) != 0)
            ButtonList.infoButton &= InfoButton.SymbolInfo;

        ButtonList.infoButton |= InfoButton.WeaponInfo;

        window.selectIndex = itemIndex;
        var info = window.info.GetComponent<WeaponEquipInfoUi>();
        Color color = new Color();
        switch (inventory.weapons[itemIndex].item.tier)
        {
            case Tier.Common:
                {
                    color = new Color(0, 0, 0, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Uncommon:
                {
                    color = new Color(40f/255f, 1f, 237f/255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Rare:
                {
                    color = new Color(1f, 0, 221 / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Unique:
                {
                    color = new Color(248f / 255f, 207f / 255f, 41f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Legendry:
                {
                    color = new Color(0, 1, 71f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;
        }
        info.itemName.color = color;
        info.tier.color = color;
        info.selectIndex = itemIndex;
        info.itemImage.GetComponent<Image>().sprite = inventory.weapons[itemIndex].item.itemIcon;

        info.InfoUpdate();
        window.info.SetActive(true);
    }

    public void OnClickArmorOpenInfo(ArmorType window)
    {
        if (window == null)
            return;

        if ((ButtonList.infoButton & InfoButton.ArmorInfo) != 0)
        {
            return;
        }

        if ((ButtonList.infoButton & InfoButton.WeaponInfo) != 0)
            ButtonList.infoButton &= ~InfoButton.WeaponInfo;

        if ((ButtonList.infoButton & InfoButton.RingInfo) != 0)
            ButtonList.infoButton &= ~InfoButton.RingInfo;

        if ((ButtonList.infoButton & InfoButton.SymbolInfo) != 0)
            ButtonList.infoButton &= InfoButton.SymbolInfo;

        ButtonList.infoButton |= InfoButton.ArmorInfo;

        window.selectIndex = itemIndex;
        var info = window.info.GetComponent<ArmorEquipInfoUi>();
        Color color = new Color();
        switch (inventory.armors[itemIndex].item.tier)
        {
            case Tier.Common:
                {
                    color = new Color(0, 0, 0, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Uncommon:
                {
                    color = new Color(40f / 255f, 1f, 237f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Rare:
                {
                    color = new Color(1f, 0, 221 / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Unique:
                {
                    color = new Color(248f / 255f, 207f / 255f, 41f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Legendry:
                {
                    color = new Color(0, 1, 71f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;
        }
        info.itemName.color = color;
        info.tier.color = color;
        info.selectIndex = itemIndex;
        info.itemImage.GetComponent<Image>().sprite = inventory.armors[itemIndex].item.itemIcon;

        info.InfoUpdate();
        window.info.SetActive(true);
    }

    public void OnClickRingOpenInfo(RingType window)
    {
        if (window == null)
            return;

        if ((ButtonList.infoButton & InfoButton.RingInfo) != 0)
        {
            return;
        }

        if ((ButtonList.infoButton & InfoButton.WeaponInfo) != 0)
            ButtonList.infoButton &= ~InfoButton.WeaponInfo;

        if ((ButtonList.infoButton & InfoButton.ArmorInfo) != 0)
            ButtonList.infoButton &= ~InfoButton.ArmorInfo;

        if ((ButtonList.infoButton & InfoButton.SymbolInfo) != 0)
            ButtonList.infoButton &= InfoButton.SymbolInfo;

        ButtonList.infoButton |= InfoButton.RingInfo;

        window.selectIndex = itemIndex;
        var info = window.info.GetComponent<RingEquipInfoUi>();
        Color color = new Color();
        switch (inventory.customRings[itemIndex].item.item.tier)
        {
            case Tier.Common:
                {
                    color = new Color(0, 0, 0, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Uncommon:
                {
                    color = new Color(40f / 255f, 1f, 237f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Rare:
                {
                    color = new Color(1f, 0, 221 / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Unique:
                {
                    color = new Color(248f / 255f, 207f / 255f, 41f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Legendry:
                {
                    color = new Color(0, 1, 71f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;
        }
        info.itemName.color = color;
        info.tier.color = color;
        info.selectIndex = itemIndex;
        info.itemImage.GetComponent<Image>().sprite = inventory.customRings[itemIndex].item.item.itemIcon;

        info.InfoUpdate();
        window.info.SetActive(true);
    }

    public void OnClickSymbolOpenInfo(SymbolType window)
    {
        if (window == null)
            return;

        if ((ButtonList.infoButton & InfoButton.SymbolInfo) != 0)
        {
            return;
        }

        if ((ButtonList.infoButton & InfoButton.WeaponInfo) != 0)
            ButtonList.infoButton &= ~InfoButton.WeaponInfo;

        if ((ButtonList.infoButton & InfoButton.ArmorInfo) != 0)
            ButtonList.infoButton &= ~InfoButton.ArmorInfo;

        if ((ButtonList.infoButton & InfoButton.RingInfo) != 0)
            ButtonList.infoButton &= InfoButton.RingInfo;

        ButtonList.infoButton |= InfoButton.SymbolInfo;

        window.selectIndex = itemIndex;
        var info = window.info.GetComponent<SymbolEquipInfoUi>();
        Color color = new Color();
        switch (inventory.customSymbols[itemIndex].item.item.tier)
        {
            case Tier.Common:
                {
                    color = new Color(0, 0, 0, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Uncommon:
                {
                    color = new Color(40f / 255f, 1f, 237f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Rare:
                {
                    color = new Color(1f, 0, 221 / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Unique:
                {
                    color = new Color(248f / 255f, 207f / 255f, 41f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;

            case Tier.Legendry:
                {
                    color = new Color(0, 1, 71f / 255f, 128f / 255f);
                    info.outBox.GetComponent<Outline>().effectColor = color;
                }
                break;
        }
        info.itemName.color = color;
        info.tier.color = color;
        info.selectIndex = itemIndex;
        info.itemImage.GetComponent<Image>().sprite = inventory.customSymbols[itemIndex].item.item.itemIcon;

        info.InfoUpdate();
        window.info.SetActive(true);
    }

    public void OnClickEquip(GameObject button)
    {
        var equip = button.GetComponent<EquipButton>();

        if (equip == null)
            return;

        if (itemIndex < 0)
            return;

        if (!AcqurieItem())
            return;

        switch (type)
        {
            case ItemType.Weapon:
                equip.itemIndex = itemIndex;
                equip.itemname.text = inventory.weapons[itemIndex].item.itemName;
                break;
            case ItemType.Armor:
                equip.itemIndex = itemIndex;
                equip.itemname.text = inventory.armors[itemIndex].item.itemName;
                break;
            case ItemType.Ring:
                equip.itemIndex = itemIndex;
                equip.itemname.text = inventory.customRings[itemIndex].item.item.itemName;
                break;
            case ItemType.Symbol:
                equip.itemIndex = itemIndex;
                equip.itemname.text = inventory.customSymbols[itemIndex].item.item.itemName;
                break;
        }

        inventory.EquipItem(itemIndex, type);
    }
}


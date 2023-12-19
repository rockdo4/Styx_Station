using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : Window
{
    private Inventory inventory;
    private StringTable stringTable;
    private StateSystem stateSystem;

    public Button[] equipButtons = new Button[4];

    public TextMeshProUGUI state;

    public SubWindow[] inventorys;

    public InfoWindowType currentSubWindow;

    public List<Button> tabs = new List<Button>();

    public Button exitButton;

    public void Awake()
    {
        inventory = InventorySystem.Instance.inventory;
        stringTable = MakeTableData.Instance.stringTable;
        stateSystem = InventorySystem.Instance.state;
    }

    public override void Open()
    {
        Open(InfoWindowType.State);

        base.Open();

        InfoTextUpdate();
    }

    public void InfoTextUpdate()
    {
        if (Global.language == Language.KOR)
        {
            tabs[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo001").KOR}";
            tabs[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo002").KOR}";
            tabs[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo003").KOR}";
            tabs[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo004").KOR}";
            string text = string.Format(stringTable.GetStringTableData("Playerinfo005").KOR,
                stateSystem.TotalState.AttackPer,
                stateSystem.TotalState.BloodSucking,
                stateSystem.TotalState.SkillDamage,
                stateSystem.TotalState.BossDamage,
                stateSystem.TotalState.Health,
                stateSystem.TotalState.CoinAcquire,
                stateSystem.TotalState.DamageReduction,
                stateSystem.TotalState.Evade);
            state.text = $"{text}";

        }
        else if (Global.language == Language.ENG)
        {
            tabs[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo001").ENG}";
            tabs[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo002").ENG}";
            tabs[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo003").ENG}";
            tabs[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo004").ENG}";
            string text = string.Format(stringTable.GetStringTableData("Playerinfo005").ENG,
                stateSystem.TotalState.AttackPer,
                stateSystem.TotalState.HealHealth,
                stateSystem.TotalState.SkillDamage,
                stateSystem.TotalState.BossDamage,
                stateSystem.TotalState.Health,
                stateSystem.TotalState.CoinAcquire,
                stateSystem.TotalState.DamageReduction,
                stateSystem.TotalState.Evade);
            state.text = $"{text}";
        }
    }
    public override void Close()
    {
        base.Close();
    }

    public void Open(InfoWindowType subType)
    {
        if (inventorys[(int)currentSubWindow].gameObject.activeSelf)
            inventorys[(int)currentSubWindow].Close();

        currentSubWindow = subType;

        inventorys[(int)subType].Open();
    }

    public void OnClickState()
    {
        Open(InfoWindowType.State);
    }

    public void OnClickInventory()
    {
        Open(InfoWindowType.Inventory);
    }
    public void OnClickSkill()
    {
        Open(InfoWindowType.Skill);
    }

    public void OnClickPet()
    {
        Open(InfoWindowType.Pet);
    }

    public void OnClickWeaponType()
    {
        Open(InfoWindowType.Inventory);
        var weapon = inventorys[(int)currentSubWindow].GetComponent<InventoryWindow>();
        weapon.currentType = ItemType.Weapon;
        weapon.Open();

        var item = InventorySystem.Instance.inventory.GetEquipItem(0);

        if (item == null)
            return;

        var weaponInfo = weapon.inventoryTypes[(int)weapon.currentType].GetComponent<WeaponType>();

        weaponInfo.weaponButtons[item.index].GetComponent<ItemButton>().OnClickWeaponOpenInfo(weaponInfo);
    }

    public void OnClickArmorType()
    {
        Open(InfoWindowType.Inventory);
        var armor = inventorys[(int)currentSubWindow].GetComponent<InventoryWindow>();
        armor.currentType = ItemType.Armor;
        armor.Open();

        var item = InventorySystem.Instance.inventory.GetEquipItem(1);

        if(item == null)
            return;

        var armorInfo = armor.inventoryTypes[(int)armor.currentType].GetComponent<ArmorType>();

        armorInfo.armorButtons[item.index].GetComponent<ItemButton>().OnClickArmorOpenInfo(armorInfo);
    }

    public void OnClickRingType()
    {
        Open(InfoWindowType.Inventory);
        var ring = inventorys[(int)currentSubWindow].GetComponent<InventoryWindow>();
        ring.currentType = ItemType.Ring;
        ring.Open();

        var item = InventorySystem.Instance.inventory.GetEquipItem(2);

        if (item == null)
            return;

        var ringInfo = ring.inventoryTypes[(int)ring.currentType].GetComponent<RingType>();

        //ringInfo.customRingButtons[item.index].GetComponent<ItemButton>().OnClickArmorOpenInfo(ringInfo);

    }

    public void OnClickSymbolType()
    {

    }
}

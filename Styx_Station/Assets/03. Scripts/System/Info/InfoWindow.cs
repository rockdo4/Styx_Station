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
        stateSystem = StateSystem.Instance;
    }

    public override void Open()
    {
        Open(InfoWindowType.State);

        ButtonList.infoButton = InfoButton.State;

        base.Open();

        InfoTextUpdate();
    }

    public void InfoTextUpdate()
    {
        if (stringTable == null)
        {
            stringTable = MakeTableData.Instance.stringTable;
        }
        if (stateSystem == null)
        {
            stateSystem = StateSystem.Instance;
        }
        if (Global.language == Language.KOR)
        {
            tabs[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo001").KOR}";
            tabs[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo002").KOR}";
            tabs[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo003").KOR}";
            tabs[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Playerinfo004").KOR}";
            string text = string.Format(stringTable.GetStringTableData("Playerinfo005").KOR,
                stateSystem.TotalState.Attack,
                stateSystem.TotalState.BloodSucking,
                stateSystem.TotalState.SkillDamage,
                stateSystem.TotalState.NormalDamage,
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
                stateSystem.TotalState.Attack,
                stateSystem.TotalState.BloodSucking,
                stateSystem.TotalState.SkillDamage,
                stateSystem.TotalState.NormalDamage,
                stateSystem.TotalState.Health,
                stateSystem.TotalState.CoinAcquire,
                stateSystem.TotalState.DamageReduction,
                stateSystem.TotalState.Evade);
            state.text = $"{text}";
        }
    }
    public override void Close()
    {
        ButtonList.infoButton = InfoButton.None;

        UIManager.Instance.SkillButtonOn();
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
        if ((ButtonList.infoButton & InfoButton.State) == 0)
        {
            ButtonList.infoButton |= InfoButton.State;

            if ((ButtonList.infoButton & InfoButton.Inventory) != 0)
                ButtonList.infoButton &= ~InfoButton.Inventory;

            if ((ButtonList.infoButton & InfoButton.Skill) != 0)
                ButtonList.infoButton &= ~InfoButton.Skill;

            if ((ButtonList.infoButton & InfoButton.Pet) != 0)
                ButtonList.infoButton &= ~InfoButton.Pet;

            Open(InfoWindowType.State);
        }
    }
    public void OnClickInventory()
    {
        if ((ButtonList.infoButton & InfoButton.Inventory) == 0)
        {
            ButtonList.infoButton |= InfoButton.Inventory;

            if ((ButtonList.infoButton & InfoButton.State) != 0)
                ButtonList.infoButton &= ~InfoButton.State;

            if ((ButtonList.infoButton & InfoButton.Skill) != 0)
                ButtonList.infoButton &= ~InfoButton.Skill;

            if ((ButtonList.infoButton & InfoButton.Pet) != 0)
                ButtonList.infoButton &= ~InfoButton.Pet;

            Open(InfoWindowType.Inventory);
        }
    }
    public void OnClickSkill()
    {
        if ((ButtonList.infoButton & InfoButton.Skill) == 0)
        {
            ButtonList.infoButton |= InfoButton.Skill;

            if ((ButtonList.infoButton & InfoButton.State) != 0)
                ButtonList.infoButton &= ~InfoButton.State;

            if ((ButtonList.infoButton & InfoButton.Inventory) != 0)
                ButtonList.infoButton &= ~InfoButton.Inventory;

            if ((ButtonList.infoButton & InfoButton.Pet) != 0)
                ButtonList.infoButton &= ~InfoButton.Pet;

            Open(InfoWindowType.Skill);
        }
    }
    public void OnClickPet()
    {
        if ((ButtonList.infoButton & InfoButton.Pet) == 0)
        {
            ButtonList.infoButton |= InfoButton.Pet;

            if ((ButtonList.infoButton & InfoButton.State) != 0)
                ButtonList.infoButton &= ~InfoButton.State;

            if ((ButtonList.infoButton & InfoButton.Inventory) != 0)
                ButtonList.infoButton &= ~InfoButton.Inventory;

            if ((ButtonList.infoButton & InfoButton.Skill) != 0)
                ButtonList.infoButton &= ~InfoButton.Skill;

            Open(InfoWindowType.Pet);
        }
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

        if((ButtonList.infoButton & InfoButton.WeaponInfo) != 0)
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (ButtonList.infoButton & InfoButton.Inventory) != 0)
        {
            if ((ButtonList.infoButton & InfoButton.WeaponInfo) != 0)
            {
                inventorys[(int)currentSubWindow].GetComponent<InventoryWindow>().
                    inventoryTypes[(int)ItemType.Weapon].GetComponent<WeaponType>().
                    OnClickCloseWeaponInfo();
            }
            else if ((ButtonList.infoButton & InfoButton.ArmorInfo) != 0)
            {
                inventorys[(int)currentSubWindow].GetComponent<InventoryWindow>().
                    inventoryTypes[(int)ItemType.Armor].GetComponent<ArmorType>().
                    OnClickCloseArmorInfo();
            }
            else if ((ButtonList.infoButton & InfoButton.RingInfo) != 0)
            {
                inventorys[(int)currentSubWindow].GetComponent<InventoryWindow>().
                    inventoryTypes[(int)ItemType.Ring].GetComponent<RingType>().
                    OnClickCloseRingInfo();
            }
            else if ((ButtonList.infoButton & InfoButton.SymbolInfo) != 0)
            {
                inventorys[(int)currentSubWindow].GetComponent<InventoryWindow>().
                    inventoryTypes[(int)ItemType.Symbol].GetComponent<SymbolType>().
                    OnClickCloseSymbolInfo();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) &&
            (ButtonList.infoButton & InfoButton.Skill) != 0)
        {
            var skillinfo = inventorys[(int)currentSubWindow].GetComponent<SkillWindow>();
            if ((ButtonList.infoButton & InfoButton.SkillInfo) != 0 && !skillinfo.equipMode)
            {
                skillinfo.OnClickCloseIfno();
            }
            else if((ButtonList.infoButton & InfoButton.SkillInfo) != 0 && skillinfo.equipMode)
            {
                skillinfo.OnClickEquipClose();
            }
        }
    }
}

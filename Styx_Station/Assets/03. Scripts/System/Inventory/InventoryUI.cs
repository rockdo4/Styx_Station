using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject equip;
    public GameObject tabs;
    public GameObject itemScroll;
    public GameObject basescroll;
    
    public Button customBase;
    public Button customPrefabs;
    public List<Button> customBaseRIng = new List<Button>();
    public List<Button> customBaseSymbol = new List<Button>();

    public GameObject stateText;

    public Button tabPrefabs;
    public Button equipPrefabs;
    public Button itemPrefabs;

    public List<Button> equipButtons = new List<Button>();
    public List<Button> tabButtons = new List<Button>();
    public List<Button> weaponButtons = new List<Button>();
    public List<Button> armorButtons = new List<Button>();
    public List<Button> customRingButtons = new List<Button>();
    public List<Button> customSymbolButtons = new List<Button>();

    private List<TextMeshProUGUI> state = new List<TextMeshProUGUI>();

    private void Awake()
    {
        InventorySystem.Instance.Setting();

        int length = InventorySystem.Instance.inventory.GetEquipItemsLength();
        int weaponsLeng = InventorySystem.Instance.inventory.weapons.Count;
        int armorsLeng = InventorySystem.Instance.inventory.armors.Count;
        int customRingLeng = InventorySystem.Instance.inventory.customRings.Count;
        int customSymbolLeng = InventorySystem.Instance.inventory.customSymbols.Count;

        int baseRingLeng = InventorySystem.Instance.inventory.rings.Count;
        int baseSymbolLeng = InventorySystem.Instance.inventory.symbols.Count;


        for (int i = 0; i < length; i++)
        {
            Button equipButton = Instantiate(equipPrefabs, equip.transform);
            var equipUi = equipButton.GetComponent<EquipButton>();
            equipUi.baseUi = this;
            equipUi.itemname.text = "None";
            equipUi.equipIndex = i;
            equipButton.onClick.AddListener(equipUi.OnClickDequip);
            equipButtons.Add(equipButton);
        }

        for (int i = 0; i < length; i++)
        {
            Button tabButton = Instantiate(tabPrefabs, tabs.transform);
            ItemType tabType = (ItemType)i;

            tabButton.GetComponentInChildren<TextMeshProUGUI>().text = tabType.ToString();
            tabButton.onClick.AddListener(()=> GetTypeEvent(tabType));
            tabButtons.Add(tabButton);
        }

        for (int i = 0; i < weaponsLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.item = InventorySystem.Instance.inventory.weapons[i];
            ui.baseUi = this;
            ui.itemname.text = ui.item.item.itemName;
            ui.equipIndex = 0;
            ui.AcqurieItem();
            Button.onClick.AddListener(ui.OnClickEquip);
            weaponButtons.Add(Button);
        }

        for (int i = 0; i < armorsLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.item = InventorySystem.Instance.inventory.armors[i];
            ui.baseUi = this;
            ui.itemname.text = ui.item.item.itemName;
            ui.equipIndex = 1;
            ui.AcqurieItem();
            Button.onClick.AddListener(ui.OnClickEquip);
            Button.gameObject.SetActive(false);
            armorButtons.Add(Button);
        }

        for (int i = 0; i < customRingLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.item = InventorySystem.Instance.inventory.customRings[i].item;
            ui.baseUi = this;
            ui.itemname.text = ui.item.item.itemName;
            ui.equipIndex = 2;
            ui.AcqurieItem();
            Button.onClick.AddListener(ui.OnClickEquip);
            Button.gameObject.SetActive(false);
            customRingButtons.Add(Button);
        }

        for (int i = 0; i < customSymbolLeng; ++i)
        {
            Button Button = Instantiate(itemPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<ItemButton>();
            ui.item = InventorySystem.Instance.inventory.customRings[i].item;
            ui.baseUi = this;
            ui.itemname.text = ui.item.item.itemName;
            ui.equipIndex = 3;
            ui.AcqurieItem();
            Button.onClick.AddListener(ui.OnClickEquip);
            Button.gameObject.SetActive(false);
            customSymbolButtons.Add(Button);
        }

        for (int i = 0; i < baseRingLeng; ++i)
        {
            Button Button = Instantiate(customPrefabs, basescroll.transform);
            var ui = Button.GetComponent<CustomButton>();
            ui.item = InventorySystem.Instance.inventory.rings[i];
            ui.baseUi = this;
            ui.itemname.text = ui.item.item.itemName;
            ui.equipIndex = 2;
            Button.onClick.AddListener(ui.OnClickEquip);
            customBaseRIng.Add(Button);
        }

        for (int i = 0; i < baseSymbolLeng; ++i)
        {
            Button Button = Instantiate(customPrefabs, itemScroll.transform);
            var ui = Button.GetComponent<CustomButton>();
            ui.item = InventorySystem.Instance.inventory.symbols[i];
            ui.baseUi = this;
            ui.itemname.text = ui.item.item.itemName;
            ui.equipIndex = 3;
            Button.onClick.AddListener(ui.OnClickEquip);
            customBaseSymbol.Add(Button);
        }

        foreach (Transform child in stateText.transform)
        {
            state.Add(child.gameObject.GetComponent<TextMeshProUGUI>());
        }

        var customUi = customBase.GetComponent<BaseCustomButton>();
        if (customUi != null)
        {
            customUi.baseUi = this;
            customUi.itemname.text = "None";
            customBase.onClick.AddListener(customUi.OnClickDequip);
        }
        GetState();
    }
    public void GetState()
    {
        state[1].text = "Attack : " + InventorySystem.Instance.inventory.t_Attack.ToString();
        state[2].text = "Health : " + InventorySystem.Instance.inventory.t_Health.ToString();
        state[3].text = "AttackSpeed : " + InventorySystem.Instance.inventory.t_AttackSpeed.ToString();
        state[4].text = "HealHealth : " + InventorySystem.Instance.inventory.t_HealHealth.ToString();
        state[5].text = "AttackPer : " + InventorySystem.Instance.inventory.t_AttackPer.ToString();
        state[6].text = "Evade : " + InventorySystem.Instance.inventory.t_Evade.ToString();
        state[7].text = "DamageReduction : " + InventorySystem.Instance.inventory.t_DamageReduction.ToString();
        state[8].text = "BloodSucking : " + InventorySystem.Instance.inventory.t_BloodSucking.ToString();
        state[9].text = "CoinAcquire : " + InventorySystem.Instance.inventory.t_CoinAcquire.ToString();
        state[10].text = "NormalDamage : " + InventorySystem.Instance.inventory.t_NormalDamage.ToString();
        state[11].text = "SkillDamage : " + InventorySystem.Instance.inventory.t_SkillDamage.ToString();
        state[12].text = "BossDamage : " + InventorySystem.Instance.inventory.t_BossDamage.ToString();
    }
    public void GetTypeEvent(ItemType type)
    {
        switch(type)
        {
            case ItemType.Weapon:
                Weapon();
                break;
            case ItemType.Armor:
                Armor();
                break;
            case ItemType.Ring: 
                Ring();
                break;
             case ItemType.Symbol:
                Symbol(); 
                break;
        }
    }
    public void Weapon()
    {
        foreach (Transform child in itemScroll.transform)
        {
            var go = child.gameObject;
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }


        foreach (var weapon in weaponButtons)
        {
            weapon.gameObject.SetActive(true);
        }
    }
    public void Armor()
    {
        foreach (Transform child in itemScroll.transform)
        {
            var go = child.gameObject;
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }

        foreach (var aromr in armorButtons)
        {
            aromr.gameObject.SetActive(true);
        }
    }
    public void Ring()
    {
        foreach (Transform child in itemScroll.transform)
        {
            var go = child.gameObject;
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }

        foreach (var ring in customRingButtons)
        {
            ring.gameObject.SetActive(true);
        }
    }
    public void Symbol()
    {
        foreach (Transform child in itemScroll.transform)
        {
            var go = child.gameObject;
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }


        foreach (var symbol in customSymbolButtons)
        {
            symbol.gameObject.SetActive(true);
        }
    }
}

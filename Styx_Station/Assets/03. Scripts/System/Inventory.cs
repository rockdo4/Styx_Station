using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float t_Attack;
    public float t_Health;
    public float t_AttackSpeed;
    public float t_HealHealth;
    public float t_AttackPer;
    public float t_Evade;
    public float t_DamageReduction;
    public float t_BloodSucking;
    public float t_CoinAcquire;
    public float t_NormalDamage;
    public float t_SkillDamage;
    public float t_BossDamage;

    [System.Serializable]
    public class InventoryItem
    {
        public Item item;
        public int upgradeLev;
        public bool acquire;
        public bool equip;

        public InventoryItem(Item item, int upgradeLev, bool acquire, bool equip)
        {
            this.item = item;
            this.upgradeLev = upgradeLev;
            this.acquire = acquire;
            this.equip = equip;
        }
    }

    public List<InventoryItem> weapons {  get; private set; } = new List<InventoryItem>();
    public List<InventoryItem> armors { get; private set; } = new List<InventoryItem>();
    public List<InventoryItem> rings  { get; private set; } = new List<InventoryItem>();
    public List<InventoryItem> customRings { get; private set; } = new List<InventoryItem>();
    public List<InventoryItem> symbols { get; private set; } = new List<InventoryItem>();
    public List<InventoryItem> customSymbols { get; private set; } = new List<InventoryItem>();

    private InventoryItem[] equipItems  = new InventoryItem[4];

    public void AddItem(Item item)
    {
        if (item == null)
            return;

        switch(item.type)
        {
            case ItemType.Weapon:
                AddWeapon(item); 
                break;
            case ItemType.Armor:
                AddArmor(item);
                break;
            case ItemType.Ring:
                AddRing(item); 
                break;
            case ItemType.Symbol:
                AddSymbol(item); 
                break;
        }
    }

    private void AddWeapon(Item item)
    {
        var addItem = weapons.Where(x => x.item.name == item.name).FirstOrDefault();
        if (addItem != null)
            return;

        weapons.Add(new InventoryItem(item, 3, true, false));
    }

    private void AddArmor(Item item)
    {
        var addItem = armors.Where(x => x.item.name == item.name).FirstOrDefault();
        if (addItem != null)
            return;

        armors.Add(new InventoryItem(item, 0, false, false));
    }

    private void AddRing(Item item)
    {
        var addItem = rings.Where(x => x.item.name == item.name).FirstOrDefault();
        if (addItem != null)
            return;

        rings.Add(new InventoryItem(item, 0, false, false));
    }

    private void CustomRing(Item item)
    {
        var dummy = new InventoryItem(item, 0, true, false);

        customRings.Add(dummy);

        item.name = customRings.IndexOf(dummy).ToString();
    }
    
    private void AddSymbol(Item item)
    {
        var addItem = symbols.Where(x => x.item.name == item.name).FirstOrDefault();
        if (addItem != null)
            return;

        symbols.Add(new InventoryItem(item, 0, false, false));
    }

    private void CustomSymbol(Item item)
    {
        var dummy = new InventoryItem(item, 0, true, false);

        customSymbols.Add(dummy);

        item.name = customSymbols.IndexOf(dummy).ToString();
    }

    private Item CreateDummy(Item dummyDate)
    {
        Item dummy = ScriptableObject.CreateInstance<Item>();

        dummy.itemName = dummyDate.itemName;
        dummy.type = dummyDate.type;
        dummy.tier = dummyDate.tier;
        dummy.options = dummyDate.options;
        dummy.itemIcon = dummyDate.itemIcon;
       
        return dummy;
    }

    public void EquipItem(InventoryItem item, int equipIndex)
    {
        if (equipItems == null)
            return;

        if (item == null)
            return;

        if (!item.acquire)
            return;

        switch (equipIndex)
        {
            case 0:
                WeaponEquip(item);
                break;
            case 1:
                ArmorEquip(item);
                break;
            case 2:
                RingEquip(item);
                break;
            case 3:
                SymbolEquip(item);
                break;
        }
    }

    public void DequipItem(InventoryItem item, int equipIndex)
    {
        if (equipItems == null)
            return;

        if (item == null)
            return;

        if (!item.acquire)
            return;

        switch (equipIndex)
        {
            case 0:
                WeaponDequip(item);
                break;
            case 1:
                ArmorDequip(item);
                break;
            case 2:
                RingDequip(item);
                break;
            case 3:
                SymbolDequip(item);
                break;
        }
    }

    private void WeaponEquip(InventoryItem item)
    {
        if (equipItems[0] == null)
            equipItems[0] = item;

        else if (equipItems[0].item == null)
            equipItems[0] = item;

        else if(equipItems[0] != null)
            equipItems[0].item.OnDequip(this, equipItems[0].upgradeLev);

        equipItems[0] = item;
        item.equip = true;
        item.item.OnEquip(this, item.upgradeLev);
    }

    private void ArmorEquip(InventoryItem item)
    {
        if (equipItems[1] == null)
            equipItems[1] = item;

        else if (equipItems[1].item == null)
            equipItems[1] = item;

        else if (equipItems[1] != null)
            equipItems[1].item.OnDequip(this, equipItems[1].upgradeLev);

        equipItems[1] = item;
        item.equip = true;
        item.item.OnEquip(this, item.upgradeLev);
    }

    private void RingEquip(InventoryItem item)
    {
        if (equipItems[2] == null)
            equipItems[2] = item;

        else if (equipItems[2].item == null)
            equipItems[2] = item;

        else if (equipItems[2] != null)
            equipItems[2].item.OnDequip(this, equipItems[2].upgradeLev);

        equipItems[2] = item;
        item.equip = true;
        item.item.OnEquip(this, item.upgradeLev);
    }

    private void SymbolEquip(InventoryItem item)
    {
        if (equipItems[3] == null)
            equipItems[3] = item;

        else if (equipItems[3].item == null)
            equipItems[3] = item;

        else if (equipItems[3] != null)
            equipItems[3].item.OnDequip(this, equipItems[3].upgradeLev);

        equipItems[3] = item;
        item.equip = true;
        item.item.OnEquip(this, item.upgradeLev);
    }

    private void WeaponDequip(InventoryItem item)
    {
        if (equipItems[0] == null)
            return;

        if (equipItems[0].item == null)
            return;

        equipItems[0].item.OnDequip(this, equipItems[0].upgradeLev);
        item.equip = false;
        equipItems[0] = null;
    }

    private void ArmorDequip(InventoryItem item)
    {
        if (equipItems[1] == null)
            return;

        if (equipItems[1].item == null)
            return;

        equipItems[1].item.OnDequip(this, equipItems[1].upgradeLev);
        item.equip = false;
        equipItems[1] = null;
    }

    private void RingDequip(InventoryItem item)
    {
        if (equipItems[2] == null)
            return;

        if (equipItems[2].item == null)
            return;

        equipItems[2].item.OnDequip(this, equipItems[2].upgradeLev);
        item.equip = false;
        equipItems[2] = null;
    }

    private void SymbolDequip(InventoryItem item)
    {
        if (equipItems[3] == null)
            return;

        if (equipItems[3].item == null)
            return;

        equipItems[3].item.OnDequip(this, equipItems[3].upgradeLev);
        item.equip = false;
        equipItems[3] = null;
    }
}

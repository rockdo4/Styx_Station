using System.Collections.Generic;
using System.Linq;
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

    public float a_Attack;
    public float a_Health;
    public float a_AttackSpeed;
    public float a_HealHealth;
    public float a_AttackPer;
    public float a_Evade;
    public float a_DamageReduction;
    public float a_BloodSucking;
    public float a_CoinAcquire;
    public float a_NormalDamage;
    public float a_SkillDamage;
    public float a_BossDamage;

    [System.Serializable]
    public class InventoryItem
    {
        public Item item;
        public int upgradeLev;
        public bool acquire;
        public bool equip;
        public int stock;
        public int index;

        public InventoryItem(Item item, int upgradeLev, bool acquire, bool equip, int stock, int index)
        {
            this.item = item;
            this.upgradeLev = upgradeLev;
            this.acquire = acquire;
            this.equip = equip;
            this.stock = stock;
            this.index = index;
        }
    }

    [System.Serializable]
    public class CustomInventoryItem
    {
        public InventoryItem item;
        public Item copyData;

        public CustomInventoryItem(InventoryItem item, Item copyData)
        {
            this.item = item;
            this.copyData = copyData;
        }
    }

    public List<InventoryItem> weapons {  get; set; } = new List<InventoryItem>();
    public List<InventoryItem> armors { get;  set; } = new List<InventoryItem>();
    public List<InventoryItem> rings  { get;  set; } = new List<InventoryItem>();
    public List<CustomInventoryItem> customRings { get; set; } = new List<CustomInventoryItem>();
    public List<InventoryItem> symbols { get;  set; } = new List<InventoryItem>();
    public List<CustomInventoryItem> customSymbols { get;  set; } = new List<CustomInventoryItem>();

    private InventoryItem[] equipItems  = new InventoryItem[4];

    public void CustomReset()
    {
        //세이브 로드 용

        for(int i=customRings.Count-1; i>=0; i--)
        {
            customRings.RemoveAt(i);
        }
        for(int i=customSymbols.Count-1; i>=0;i--)
        {
            customSymbols.RemoveAt(i);
        }
        for(int i= 0; i<equipItems.Length; ++i)
        {   
            if (equipItems[i] != null && equipItems[i].index>-1)
            {
                DequipItem(equipItems[i], (ItemType)i);
            }
            equipItems[i] = null;
        }
    }

    public int GetEquipItemsLength()
    {
        return equipItems.Length;
    }

    public InventoryItem GetEquipItem(int index)
    {
        return equipItems[index];
    }

    public void ItemSorting(ItemType type)
    {
        switch (type)
        {
            case ItemType.Weapon:
                for (int i = 0; i < weapons.Count; ++i)
                {
                    weapons[i].index = i;
                }
                break;

            case ItemType.Armor:
                for (int i = 0; i < armors.Count; ++i)
                {
                    armors[i].index = i;
                }
                break;

            case ItemType.Ring:
                for (int i = 0; i < rings.Count; ++i)
                {
                    rings[i].index = i;
                }

                for(int i = 0; i< customRings.Count; ++i)
                {
                    customRings[i].item.index = i;
                }
                break;
            case ItemType.Symbol:
                for (int i = 0; i < symbols.Count; ++i)
                {
                    symbols[i].index = i;
                }

                for(int i = 0; i<customSymbols.Count; ++i)
                {
                    customSymbols[i].item.index = i;
                }
                break;
        }
    }

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

        weapons.Add(new InventoryItem(item,0, false, false, 0, -1));
    }

    private void AddArmor(Item item)
    {
        var addItem = armors.Where(x => x.item.name == item.name).FirstOrDefault();
        if (addItem != null)
            return;

        armors.Add(new InventoryItem(item, 0, false, false, 0, -1));
    }

    private void AddRing(Item item)
    {
        var addItem = rings.Where(x => x.item.name == item.name).FirstOrDefault();
        if (addItem != null)
            return;

        rings.Add(new InventoryItem(item, 0, false, false, 0, -1));
    }

    public InventoryItem AddCustom(Item item, Item dummy)
    {
        switch (item.type)
        {
            case ItemType.Ring:
                
                    var dummys = CustomRing(item, dummy);
                    return dummys;
                
            case ItemType.Symbol:
                
                    var dummys_1 = CustomSymbol(item, dummy);
                    return dummys_1;

        }
        return null;
    }

    private InventoryItem CustomRing(Item item, Item dummy)
    {
        var dummys = new InventoryItem(dummy, 0, true, false, 0, -1);
        var dummyItem = new CustomInventoryItem(dummys, item);

        customRings.Add(dummyItem);

        dummy.name = customRings.IndexOf(dummyItem).ToString();
        dummys.index = customRings.IndexOf(dummyItem);

        return dummys;
    }
    
    private void AddSymbol(Item item)
    {
        var addItem = symbols.Where(x => x.item.name == item.name).FirstOrDefault();
        if (addItem != null)
            return;

        symbols.Add(new InventoryItem(item, 0, false, false, 0, -1));
    }

    private InventoryItem CustomSymbol(Item item, Item dummy)
    { 
        var dummys = new InventoryItem(dummy, 0, true, false,0, -1);
        var dummyItem = new CustomInventoryItem(dummys, item);

        customSymbols.Add(dummyItem);

        dummy.name = customSymbols.IndexOf(dummyItem).ToString();
        dummys.index = customSymbols.IndexOf(dummyItem);

        return dummys;
    }

    public Item CreateDummy(Item dummyDate)
    {
        Item dummy = ScriptableObject.CreateInstance<Item>();

        dummy.itemName = dummyDate.itemName;
        dummy.type = dummyDate.type;
        dummy.tier = dummyDate.tier;
        dummy.options = dummyDate.options;
        dummy.itemIcon = dummyDate.itemIcon;
       
        return dummy;
    }

    public void EquipItem(int itemIndex, ItemType type)
    {
        if (equipItems == null)
            return;

        if (itemIndex < 0)
            return;

        switch (type)
        {
            case ItemType.Weapon:
                WeaponEquip(weapons[itemIndex]);
                break;
            case ItemType.Armor:
                ArmorEquip(armors[itemIndex]);
                break;
            case ItemType.Ring:
                RingEquip(customRings[itemIndex].item);
                break;
            case ItemType.Symbol:
                SymbolEquip(customSymbols[itemIndex].item);
                break;
        }

        var find = GameObject.FindWithTag("Player");
        if(find !=null)
        {
            var stats = find.GetComponent<ResultPlayerStats>();
            if(stats != null)
                stats.SettingPlayerMaxHP();
        }
    }

    public void DequipItem(InventoryItem item, ItemType type)
    {
        if (equipItems == null)
            return;

        if (item == null)
            return;

        if (!item.acquire)
            return;

        switch (type)
        {
            case ItemType.Weapon:
                WeaponDequip(item);
                break;
            case ItemType.Armor:
                ArmorDequip(item);
                break;
            case ItemType.Ring:
                RingDequip(item);
                break;
            case ItemType.Symbol:
                SymbolDequip(item);
                break;
        }
        var find = GameObject.FindWithTag("Player");
        if (find != null)
        {
            var stats = find.GetComponent<ResultPlayerStats>();
            if (stats != null)
                stats.SettingPlayerMaxHP();
        }
    }

    private void WeaponEquip(InventoryItem item)
    {
        if (equipItems[0] == null)
            equipItems[0] = item;

        else if (equipItems[0].item == null)
            equipItems[0] = item;

        else if (equipItems[0] != null)
        {
            equipItems[0].equip = false;
            equipItems[0].item.OnDequip(this, equipItems[0].upgradeLev);
        }
        else if (equipItems[0].item != null)
        {
            equipItems[0].equip = false;
            equipItems[0].item.OnDequip(this, equipItems[0].upgradeLev);
        }

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
        {
            equipItems[1].equip = false;
            equipItems[1].item.OnDequip(this, equipItems[1].upgradeLev);
        }
        else if (equipItems[1].item != null)
        {
            equipItems[1].equip = false;
            equipItems[1].item.OnDequip(this, equipItems[1].upgradeLev);
        }

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
        {
            equipItems[2].equip = false;
            equipItems[2].item.OnDequip(this, equipItems[2].upgradeLev);
        }
        else if (equipItems[2].item != null)
        {
            equipItems[2].equip = false;
            equipItems[2].item.OnDequip(this, equipItems[2].upgradeLev);
        }

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
        {
            equipItems[3].equip = false;
            equipItems[3].item.OnDequip(this, equipItems[3].upgradeLev);
        }
        else if (equipItems[3].item != null)
        {
            equipItems[3].equip = false;
            equipItems[3].item.OnDequip(this, equipItems[3].upgradeLev);
        }

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

    public void AcquireState()
    {
        foreach(var item in weapons)
        {
            item.item.AcquireValue();
        }

        foreach(var item in armors)
        {
            item.item.AcquireValue();
        }
    }
}
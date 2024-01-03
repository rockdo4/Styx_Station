using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
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

    private StateSystem state;

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
        dummy.enchant = dummyDate.enchant;
        dummy.itemLevUpNum = dummyDate.itemLevUpNum;
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

        if(state == null)
            state = StateSystem.Instance;

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
    }

    public void DequipItem(InventoryItem item, ItemType type)
    {
        if (equipItems == null)
            return;

        if (item == null)
            return;

        if (!item.acquire)
            return;

        if (state == null)
            state = StateSystem.Instance;

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
        }
        else if (equipItems[0].item != null)
        {
            equipItems[0].equip = false;
        }

        equipItems[0] = item;
        item.equip = true;
        state.EquipUpdate();
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
        }
        else if (equipItems[1].item != null)
        {
            equipItems[1].equip = false;
        }

        equipItems[1] = item;
        item.equip = true;
        state.EquipUpdate();

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
        }
        else if (equipItems[2].item != null)
        {
            equipItems[2].equip = false;
        }

        equipItems[2] = item;
        item.equip = true;
        state.EquipUpdate();
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
        }
        else if (equipItems[3].item != null)
        {
            equipItems[3].equip = false;
        }

        equipItems[3] = item;
        item.equip = true;
        state.EquipUpdate();
    }

    private void WeaponDequip(InventoryItem item)
    {
        if (equipItems[0] == null)
            return;

        if (equipItems[0].item == null)
            return;

        item.equip = false;
        equipItems[0] = null;
        state.EquipUpdate();
    }

    private void ArmorDequip(InventoryItem item)
    {
        if (equipItems[1] == null)
            return;

        if (equipItems[1].item == null)
            return;

        item.equip = false;
        equipItems[1] = null;
        state.EquipUpdate();
    }

    private void RingDequip(InventoryItem item)
    {
        if (equipItems[2] == null)
            return;

        if (equipItems[2].item == null)
            return;

        item.equip = false;
        equipItems[2] = null;
        state.EquipUpdate();
    }

    private void SymbolDequip(InventoryItem item)
    {
        if (equipItems[3] == null)
            return;

        if (equipItems[3].item == null)
            return;

        item.equip = false;
        equipItems[3] = null;
        state.EquipUpdate();
    }

    public void BreakRing(CustomInventoryItem item)
    {
        switch(item.item.item.tier)
        {
            case Tier.Common:
                CurrencyManager.itemAsh += 50;
                break;

            case Tier.Uncommon:
                CurrencyManager.itemAsh += 150;
                break;

            case Tier.Rare:
                CurrencyManager.itemAsh += 450;
                break;

            case Tier.Unique:
                CurrencyManager.itemAsh += 1350;
                break;

            case Tier.Legendry:
                CurrencyManager.itemAsh += 4050;
                break;
        }

        customRings.Remove(item);

        ItemSorting(ItemType.Ring);
    }

    public void BreakSymbol(CustomInventoryItem item)
    {
        switch (item.item.item.tier)
        {
            case Tier.Common:
                CurrencyManager.itemAsh += 50;
                break;

            case Tier.Uncommon:
                CurrencyManager.itemAsh += 150;
                break;

            case Tier.Rare:
                CurrencyManager.itemAsh += 450;
                break;

            case Tier.Unique:
                CurrencyManager.itemAsh += 1350;
                break;

            case Tier.Legendry:
                CurrencyManager.itemAsh += 4050;
                break;
        }

        customSymbols.Remove(item);

        ItemSorting(ItemType.Symbol);
    }
}
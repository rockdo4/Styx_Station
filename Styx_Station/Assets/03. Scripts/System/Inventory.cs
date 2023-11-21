using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<int, (FixedItem, int)> fixedInventory = new Dictionary<int, (FixedItem, int)>();
    private Dictionary<int, (FloatingItem, int)> floatingInventory = new Dictionary<int, (FloatingItem, int)>();

    private Dictionary<int, (Item, int)> equipItems = new Dictionary<int, (Item, int)>(4);

    public void EquipItem(int index, int equipIndex)
    {
        if (equipItems == null)
            return;

        Item item = new Item();
        int upgrade = 0;

        switch (equipIndex)
        {
            case 0:
                item = fixedInventory[index].Item1;
                upgrade = fixedInventory[index].Item2;
                break;
            case 1:
                item = fixedInventory[index].Item1;
                upgrade = fixedInventory[index].Item2;
                break;
            case 2:
                item = floatingInventory[index].Item1;
                upgrade = floatingInventory[index].Item2;
                break;
            case 3:
                item = floatingInventory[index].Item1;
                upgrade = floatingInventory[index].Item2;
                break;
        }

        if (equipItems[equipIndex].Item1 != null)
            DequipItem(equipIndex);

        equipItems[equipIndex] = (item, upgrade);

        EquipItemsOption();
    }

    public void DequipItem(int index)
    {
        if (equipItems[index].Item1 == null)
            return;

        DequipItemOption(index);
        equipItems[index] = (null, 0); 
    }

    private void EquipItemsOption()
    {
        foreach(var equipItem in equipItems)
        {
            if (equipItem.Value.Item1 == null)
                continue;

            equipItem.Value.Item1.OnEquip(this, equipItem.Value.Item2);
        }
    }

    private void DequipItemOption(int index)
    {
        if (equipItems[index].Item1 == null)
            return;

        equipItems[index].Item1.OnDequip(this, equipItems[index].Item2);
    }

    public void AddFixedInventory(int index, FixedItem item, int upgradeLev)
    {
        if (item == null)
            return;

        if (upgradeLev < 0)
            return;

        fixedInventory.Add(index, (item, upgradeLev));
    }

    public void AddFloatingInventory(int index,  FloatingItem item, int upgradeLev)
    {
        if(item == null)
            return;

        if (upgradeLev < 0)
            return;

        floatingInventory.Add(index, (item, upgradeLev));
    }
}

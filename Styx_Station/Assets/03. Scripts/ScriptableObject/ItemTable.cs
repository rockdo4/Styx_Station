using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTable")]
public class ItemTable : ScriptableObject
{
    [SerializeField]
    private List<FixedItem> fixedItemTable = new List<FixedItem>();

    [SerializeField]
    private List<FloatingItem> floatingItemTable = new List<FloatingItem>();

    public int GetFixedItemCount()
    {
        return fixedItemTable.Count;
    }

    public FixedItem GetFixedItem(int index)
    {
        if (fixedItemTable.Count == 0)
            return null;

        if (fixedItemTable.Count < index)
            return null;

        return fixedItemTable[index];
    }

    public FloatingItem GetFloatingItem(int index)
    {
        if (floatingItemTable.Count == 0)
            return null;

        if (floatingItemTable.Count < index)
            return null;

        return floatingItemTable[index];
    }
}

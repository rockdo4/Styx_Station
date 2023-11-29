using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ItemTable")]
public class ItemTable : ScriptableObject
{
    [SerializeField]
    private List<Item> table = new List<Item>();
    
    public int GetTableSize()
    {
        return table.Count;
    }

    public Item GetItemList(int index)
    {
        return table[index];
    }

    public Item GetItem(string name)
    {
        return table.Where(x => x.name == name).FirstOrDefault();
    }
}

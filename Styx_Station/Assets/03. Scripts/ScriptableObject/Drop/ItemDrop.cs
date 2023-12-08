using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Drops/ItemDrop")]
public class ItemDrop: ScriptableObject
{
    [Tooltip("드롭 아이템 테이블")]
    public List<AddItem> items = new List<AddItem>();

    [System.Serializable]
    public class AddItem
    {
        public float weight;
        public Item item;
    }

    public Item PickUp()
    {
        if (items == null)
            return null;

        if (items.Count <= 0)
            return null;

        float sum = 0;
        foreach (var item in items)
        {
            sum += item.weight;
        }

        if (sum <= 0)
            return null;

        if ((int)sum > 1)
            return null;

        var random = Random.Range(0, sum);

        for(int i =0; i< items.Count; ++i)
        {
            var item = items[i];
            if (item.weight > random)
                return item.item;

            else
            {
                random -= item.weight;
            }
        }

        return null;
    }
}

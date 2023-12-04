using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drops/ItemDropTable")]
public class ItemDropTable : ScriptableObject
{
    public List<DropTable> drops = new List<DropTable>();

    [System.Serializable]
    public class DropTable
    {
        public ItemDrop item;
        public int RankUp;
    }

    public Item GetItem(int rank)
    {
        if(drops == null)
            return null;

        if(drops.Count<=0)
            return null;

        if(rank>=drops.Count)
            return null;

        return drops[rank].item.PickUp();
    }
}

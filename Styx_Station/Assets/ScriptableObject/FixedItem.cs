using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FixedItem.asset", menuName = "Item/FixedItem")]
public class FixedItem : Item
{
    public FixedItemType type;

    [System.Serializable]
    public struct FixedAcqureOption
    {
        public FixedOptionString option;
        public float value;
    }

    public List<FixedAcqureOption> AcquireOption = new List<FixedAcqureOption>();

    public bool acquire = false;
}

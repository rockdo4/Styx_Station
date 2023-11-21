using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FixedItem.asset", menuName = "Item/FixedItem")]
public class FixedItem : Item
{
    [Tooltip("장비 종류")]
    public FixedItemType type;

    [System.Serializable]
    public struct FixedAcqureOption
    {
        [Tooltip("보유 시 증가 옵션")]
        public ItemOptionString option;
        [Tooltip("증가량")]
        public float value;
    }

    [Tooltip("획득 시 아이템 보유 옵션")]
    public List<FixedAcqureOption> AcquireOption = new List<FixedAcqureOption>();

    [Tooltip("아이템 획득 여부")]
    public bool acquire = false;
}

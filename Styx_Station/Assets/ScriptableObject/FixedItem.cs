using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FixedItem.asset", menuName = "Item/FixedItem")]
public class FixedItem : Item
{
    [Tooltip("��� ����")]
    public FixedItemType type;

    [System.Serializable]
    public struct FixedAcqureOption
    {
        [Tooltip("���� �� ���� �ɼ�")]
        public ItemOptionString option;
        [Tooltip("������")]
        public float value;
    }

    [Tooltip("ȹ�� �� ������ ���� �ɼ�")]
    public List<FixedAcqureOption> AcquireOption = new List<FixedAcqureOption>();

    [Tooltip("������ ȹ�� ����")]
    public bool acquire = false;
}

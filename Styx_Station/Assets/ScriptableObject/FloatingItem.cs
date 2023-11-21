using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatingItem.asset", menuName = "Item/FloatingItem")]
public class FloatingItem : Item
{
    [Tooltip("장비 종류")]
    public FloatingItemType type;

    [System.Serializable]
    public struct FloatingOption
    {
        [Tooltip("장착 시 증가 특수 옵션")]
        public FloatingOptionString option;
        [Tooltip("증가량")]
        public float value;
    }

    [SerializeField]
    [Tooltip("장착 시 아이템 특수 옵션")]
    private List<FloatingOption> FloatingOptions = new List<FloatingOption>();

    public void AddOption(FloatingOptionString option, float value)
    {
        if (FloatingOptions == null)
            return;

        FloatingOption fOption = new FloatingOption();
        fOption.option = option;
        fOption.value = value;

        FloatingOptions.Add(fOption);
    }
}

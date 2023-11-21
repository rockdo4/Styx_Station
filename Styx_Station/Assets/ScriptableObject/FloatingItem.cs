using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatingItem.asset", menuName = "Item/FloatingItem")]
public class FloatingItem : Item
{
    public FloatingItemType type;

    [System.Serializable]
    public struct FloatingOption
    {
        public FloatingOptionString option;
        public float value;
    }

    [SerializeField]
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

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatingItem.asset", menuName = "Item/FloatingItem")]
public class FloatingItem : Item
{
    [Tooltip("��� ����")]
    public FloatingItemType type;

    [System.Serializable]
    public struct FloatingOption
    {
        [Tooltip("���� �� ���� Ư�� �ɼ�")]
        public FloatingOptionString option;
        [Tooltip("������")]
        public float value;
    }

    [SerializeField]
    [Tooltip("���� �� ������ Ư�� �ɼ�")]
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

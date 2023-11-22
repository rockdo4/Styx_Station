using System.Collections.Generic;
using UnityEngine;
using static Item;

[CreateAssetMenu(menuName = "CustomOption")]
public class CustomOption : ScriptableObject
{
    public Item item;

    [Tooltip("Ŀ���� ���̺�")]
    public List<AddOption> addOptions = new List<AddOption>();

    [System.Serializable]
    public struct AddOption
    {
        [Tooltip("Ȯ��")]
        public float weight;
        [Tooltip("�߰� �ɼ�")]
        public AddOptionString option;
        [Tooltip("������")]
        public float value;
    }
}

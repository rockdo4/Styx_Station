using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Tooltip("������ �̸�")]
    public string name;

    [Tooltip("������ ���")]
    public ItemTier tier;

    [System.Serializable]
    public struct Option
    {
        [Tooltip("���� �� ���� �ɼ�")]
        public ItemOptionString option;
        [Tooltip("�⺻ ����")]
        public float value;
        [Tooltip("��ȭ �� ���� �߰� ������")]
        public float upgradeValue;
    }

    [Tooltip("������ ���� �ɼ�")]
    public List<Option> options = new List<Option>();
}

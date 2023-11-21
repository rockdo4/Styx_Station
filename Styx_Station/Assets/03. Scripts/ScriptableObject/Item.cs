using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject, IItemEquiptable, IItemDequiptable
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

    public void OnEquip(Inventory inventory, int upgadeLev)
    {
        if (options == null)
            return;

        if (options.Count <= 0)
            return;

        foreach(var option in options)
        {
            switch(option.option)
            {
                case ItemOptionString.Attack:
                    break;

                case ItemOptionString.Health:
                    break;
            }
        }
    }

    public void OnDequip(Inventory inventory, int upgadeLev)
    {
        if (options == null)
            return;

        if (options.Count <= 0)
            return;

        foreach (var option in options)
        {
            switch (option.option)
            {
                case ItemOptionString.Attack:
                    break;

                case ItemOptionString.Health:
                    break;
            }
        }
    }
}

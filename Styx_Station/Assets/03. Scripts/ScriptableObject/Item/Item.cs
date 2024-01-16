using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Item")]
public class Item : ScriptableObject
{
    public ItemType type;

    public Tier tier;

    public Enchant enchant;

    public List<int> itemLevUpNum = new List<int>();

    [System.Serializable]
    public struct Option
    {
        public ItemOptionString option;
        public float value;
        public float upgradeValue;
    }

    public List<Option> options = new List<Option>();

    [System.Serializable]
    public struct AddOption
    {
        public AddOptionString option;
        public float value;
        public float upgradeValue;
    }

    public List<AddOption> addOptions = new List<AddOption>();

    public Sprite itemIcon;

    public void AddOptions(AddOptionString option, float value)
    {
        if (addOptions == null)
            return;

        AddOption fOption = new AddOption();
        fOption.option = option;
        fOption.value = value;

        addOptions.Add(fOption);
    }
}

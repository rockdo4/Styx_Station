using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Item : ScriptableObject
{
    public string name;
    public ItemTier tier;

    [System.Serializable]
    public struct Option
    {
        public ItemOptionString option;
        public float value;
        public float upgradeValue;
    }

    public List<Option> options = new List<Option>();
}

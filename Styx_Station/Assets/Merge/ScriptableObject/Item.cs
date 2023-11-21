using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Tooltip("아이템 이름")]
    public string name;

    [Tooltip("아이템 등급")]
    public ItemTier tier;

    [System.Serializable]
    public struct Option
    {
        [Tooltip("장착 시 증가 옵션")]
        public ItemOptionString option;
        [Tooltip("기본 스탯")]
        public float value;
        [Tooltip("강화 시 스탯 추가 증가량")]
        public float upgradeValue;
    }

    [Tooltip("아이템 장착 옵션")]
    public List<Option> options = new List<Option>();
}

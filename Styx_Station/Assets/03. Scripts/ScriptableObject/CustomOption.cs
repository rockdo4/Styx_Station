using System.Collections.Generic;
using UnityEngine;
using static Item;

[CreateAssetMenu(menuName = "CustomOption")]
public class CustomOption : ScriptableObject
{
    public Item item;

    [Tooltip("커스텀 테이블")]
    public List<AddOption> addOptions = new List<AddOption>();

    [System.Serializable]
    public struct AddOption
    {
        [Tooltip("확률")]
        public float weight;
        [Tooltip("추가 옵션")]
        public AddOptionString option;
        [Tooltip("증가량")]
        public float value;
    }
}

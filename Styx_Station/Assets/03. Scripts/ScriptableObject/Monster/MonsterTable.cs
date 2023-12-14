using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Monster/MonsterTable")]
public class MonsterTable : ScriptableObject
{
    [SerializeField]
    private List<MonsterTypeBase> table = new List<MonsterTypeBase>();
    public int GetTableSize()
    {
        return table.Count;
    }

    public MonsterTypeBase GetMonster(int index)
    {
        return table[index];
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName ="Skills/SkillTable")]
public class SkillTable : ScriptableObject
{
    [SerializeField]
    private List<Skill> table = new List<Skill>();

    public int GetTableSize()
    {
        return table.Count;
    }

    public Skill GetSkill(int index)
    {
        return table[index];
    }

    public Skill GetSkill (string name)
    {
        return table.Where(x => x.name == name).FirstOrDefault();
    }
}

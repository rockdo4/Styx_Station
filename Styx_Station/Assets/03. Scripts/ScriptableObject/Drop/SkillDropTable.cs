using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drops/SkillDropTable")]
public class SkillDropTable : ScriptableObject
{
    public List<DropTable> drops = new List<DropTable>();

    [System.Serializable]
    public class DropTable
    {
        public SkillDrop skill;
        public int RankUp;
    }

    public Skill GetSkll(int rank)
    {
        if (drops == null)
            return null;

        if (drops.Count <= 0)
            return null;

        if (rank >= drops.Count)
            return null;

        return drops[rank].skill.PickUp();
    }
}

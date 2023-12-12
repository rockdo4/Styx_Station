using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drops/SkillDrop")]
public class SkillDrop : ScriptableObject
{
    public List<AddSkill> skills = new List<AddSkill>();

    [System.Serializable]
    public class AddSkill
    {
        public Skill skill;
        public float weight;
    }

    public Skill PickUp()
    {
        if (skills == null)
            return null;

        if (skills.Count <= 0)
            return null;

        float sum = 0;
        foreach (var skill in skills)
        {
            sum += skill.weight;
        }

        if (sum <= 0)
            return null;

        if (sum > 1)
            return null;

        var random = Random.Range(0, sum);

        for (int i = 0; i < skills.Count; ++i)
        {
            var skill = skills[i];
            if (skill.weight > random)
                return skill.skill;

            else
                random -= skill.weight;
        }

        return null;
    }
}

using UnityEngine;

public class BlackCloud : SkillBase
{
    private SkillInventory.InventorySKill blackCloud;
    private GameObject blackCloudPrefab;
    private GameObject caster;
    private float damageMultiplier;

    public BlackCloud(SkillInventory.InventorySKill skill, GameObject blackCloudPrefab)
    {
        blackCloud = skill;
        this.blackCloudPrefab = blackCloudPrefab;

        damageMultiplier = blackCloud.skill.Skill_ATK;
    }
    public override void UseSkill(GameObject attacker)
    {
        
    }
}

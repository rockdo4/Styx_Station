using UnityEngine;

public class Impale : SkillBase
{
    private SkillInventory.InventorySKill impale;
    private GameObject impaleShooterPrefab;
    private GameObject impalePrefab;
    private float damageMultiplier;

    public Impale(SkillInventory.InventorySKill skill, GameObject prefab, GameObject impaleShooterPrefab)
    {
        impale = skill;
        impalePrefab = prefab;

        damageMultiplier = impale.skill.Skill_ATK + impale.skill.Skill_ATK_LVUP * (impale.upgradeLev - 1);
        this.impaleShooterPrefab = impaleShooterPrefab;
    }
    public override void UseSkill(GameObject attacker)
    {
        var impaleShooter = ObjectPoolManager.instance.GetGo(impaleShooterPrefab.name);
        if (impaleShooter == null)
        {
            Debug.Log("ERR: impaleShooter is null");
            return;
        }
        impaleShooter.GetComponent<impaleShooter>().
            SetImpaleShooter(
            impale.skill.Skill_ATK_NUM,
            damageMultiplier,
            attacker,
            impalePrefab);
    }
}

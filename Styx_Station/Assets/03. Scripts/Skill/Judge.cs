using UnityEngine;

public class Judge : SkillBase
{
    private SkillInventory.InventorySKill judge;
    private GameObject judgePrefab;
    private float damageMultiplier;
    private GameObject particlePrefab;

    public Judge(SkillInventory.InventorySKill skill, GameObject Prefab, GameObject particle)
    {
        judge = skill;
        judgePrefab = Prefab;
        particlePrefab = particle;

        damageMultiplier = judge.skill.Skill_ATK + (judge.upgradeLev * judge.skill.Skill_ATK_LVUP);
    }
    public override void UseSkill(GameObject attacker)
    {
        var judgeShooter = ObjectPoolManager.instance.GetGo(judgePrefab.name);
        //var tornado = Object.Instantiate(tornadoShotPrefab);
        if (judgeShooter == null)
        {
            Debug.Log("ERR: judgeShooter is null");
            return;
        }
        judgeShooter.GetComponent<JudgeShooter>().
            SetJudgeShooter(
            judge.skill.Skill_ATK_NUM,
            damageMultiplier,
            attacker,
            particlePrefab);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SoulDamage : SkillBase
{
    private SkillInventory.InventorySKill soulDamage;
    private GameObject particle;
    private GameObject caster;
    private float multiple;
    public SoulDamage(SkillInventory.InventorySKill skill, GameObject particle, GameObject c)
    {
        soulDamage = skill;
        this.particle = particle;
        caster = c;

        multiple = soulDamage.skill.Skill_ATK + (soulDamage.upgradeLev - 1) * soulDamage.skill.Skill_ATK_LVUP;
    }
    public override void UseSkill(GameObject attacker)
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy")
             .Where(obj => obj.activeSelf)
             .Where(obj => obj.GetComponent<MonsterStats>().currHealth > 0)
             .ToArray();
        foreach (var monster in monsters)
        {
            monster.GetComponent<MonsterController>().isStunned = true;
            HitMonster(monster);
        }
    }

    private void HitMonster(GameObject defender)
    {
        if (defender == null)
            return;
        if (defender.GetComponent<MonsterStats>().currHealth <= 0)
            return;

        var attackerStats = caster.GetComponent<ResultPlayerStats>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target, multiple);

        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(caster, attack);
        }

        Debug.Log("Hit Monster in SoulDamage");
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SoulDamage : SkillBase
{
    private SkillInventory.InventorySKill soulDamage;
    private GameObject particle;
    private GameObject stunParticle;
    private GameObject caster;
    private float multiple;
    public SoulDamage(SkillInventory.InventorySKill skill, GameObject particle, GameObject sParticle, GameObject c)
    {
        soulDamage = skill;
        this.particle = particle;
        caster = c;
        stunParticle = sParticle;

        multiple = soulDamage.skill.Skill_ATK + (soulDamage.upgradeLev * soulDamage.skill.Skill_ATK_LVUP);
    }
    public override void UseSkill(GameObject attacker)
    {
        ObjectPoolManager.instance.GetGo(particle.name);

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy")
             .Where(obj => obj.activeSelf)
             .Where(obj => obj.GetComponent<MonsterStats>().currHealth > 0)
             .ToArray();
        foreach (var monster in monsters)
        {
            monster.GetComponent<MonsterController>().SetStun(); // = true;
            HitMonster(monster);
        }
    }

    private void HitMonster(GameObject defender)
    {
        if (defender == null)
            return;
        if (defender.GetComponent<MonsterStats>().currHealth <= 0)
            return;
        if(caster == null)
        {
            caster = GameObject.FindGameObjectWithTag("Player");
        }
        var attackerStats = caster.GetComponent<ResultPlayerStats>(); ///caster 이 플레이어임
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

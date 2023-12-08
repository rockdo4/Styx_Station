using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrowShot : SkillBase
{
    private SkillInventory.InventorySKill poisonArrowShot;
    private GameObject poisonArrowPrefab;
    private float speed;
    private GameObject caster;
    private float damageMultiplier;

    public PoisonArrowShot(SkillInventory.InventorySKill skill, GameObject prefab)
    {
        poisonArrowShot = skill;
        poisonArrowPrefab = prefab;

        damageMultiplier = poisonArrowShot.skill.Skill_ATK;
        speed = 1 / this.poisonArrowShot.skill.Skill_Speed;
    }
    public override void UseSkill(GameObject attacker)
    {
        var poisonArrow = ObjectPoolManager.instance.GetGo(poisonArrowPrefab.name);
        if (poisonArrow == null)
        {
            Debug.Log("ERR: poisonArrow is null");
            return;
        }
        var rects = attacker.GetComponentsInChildren<RectTransform>();
        if (rects[1] == null)
        {
            Debug.Log("ERR: No firePoint");
            return;
        }
        var startPos = rects[1].transform.position;

        poisonArrow.transform.position = startPos;

        var arrow = poisonArrow.GetComponent<PoisonArrow>();
        if (!arrow.CheckOnCollided())
        {
            arrow.OnCollided += OnArrowCollided;
        }

        arrow.Fire(attacker, speed);
    }

    public void OnArrowCollided(GameObject attacker, GameObject defender)
    {
        if (defender == null)
        {
            return;
        }
        var attackerStats = attacker.GetComponent<ResultPlayerStats>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target, damageMultiplier);

        defender.GetComponent<MonsterController>().SetPoision(poisonArrowShot.skill.Skill_Du, attack, attacker);
    }
}

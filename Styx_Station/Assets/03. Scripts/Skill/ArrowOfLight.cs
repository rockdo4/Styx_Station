using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowOfLight : SkillBase
{
    GameObject arrowOfLightShotPrefab;
    private SkillInventory.InventorySKill arrowOfLight;
    private GameObject caster;
    private float multiple;
    private float speed = 0;

    public ArrowOfLight(SkillInventory.InventorySKill skill, GameObject prefab, GameObject c)
    {
        arrowOfLight = skill;
        arrowOfLightShotPrefab = prefab;
        caster = c;

        multiple = arrowOfLight.skill.Skill_ATK + (arrowOfLight.upgradeLev - 1) * arrowOfLight.skill.Skill_ATK_LVUP;
        speed = 1 / arrowOfLight.skill.Skill_Speed;
    }
    public override void UseSkill(GameObject attacker)
    {
        var arrow = ObjectPoolManager.instance.GetGo(arrowOfLightShotPrefab.name);
        if(arrow == null)
        {
            Debug.Log("ERR: ArrowOfLight, arrow is null");
            return;
        }

        var rects = attacker.GetComponentsInChildren<RectTransform>();
        if (rects[2] == null)
        {
            Debug.Log("ERR: No skillPoint");
            return;
        }
        var startPos = rects[2].transform.position;

        arrow.transform.position = startPos;

        var arrowLight = arrow.GetComponent<ArrowOfLightArrow>();
        if (!arrowLight.CheckOnCollided())
        {
            arrowLight.OnCollided += Oncollided;
        }

        arrowLight.Fire(attacker, speed);
    }

    public void Oncollided(GameObject attacker, GameObject defender)
    {
        if (defender == null)
        {
            return;
        }
        var attackerStats = attacker.GetComponent<ResultPlayerStats>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target, multiple);

        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}

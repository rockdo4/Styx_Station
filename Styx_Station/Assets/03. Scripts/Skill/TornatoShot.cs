using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TornatoShot : SkillBase
{
    private SkillInventory.InventorySKill tornatoShot;
    private GameObject tornadoShotPrefab;
    private float speed;
    private GameObject caster;
    private float damageMultiplier;


    public TornatoShot(SkillInventory.InventorySKill skill, GameObject prefab)
    {
        tornatoShot = skill;
        tornadoShotPrefab = prefab;

        damageMultiplier = tornatoShot.skill.Skill_ATK;
        speed = 1 / this.tornatoShot.skill.Skill_Speed;
    }
    public override void UseSkill(GameObject attacker)
    {
        var tornado = ObjectPoolManager.instance.GetGo(tornadoShotPrefab.name);
        //var tornado = Object.Instantiate(tornadoShotPrefab);
        if (tornado == null)
        {
            Debug.Log("ERR: tornado is null");
            return;
        }

        var rects = attacker.GetComponentsInChildren<RectTransform>();
        if (rects[2] == null)
        {
            Debug.Log("ERR: No skillPoint");
            return;
        }
        var startPos = rects[2].transform.position;

        tornado.transform.position = startPos;

        var tornadoPiercing = tornado.GetComponent<PiercingArrow>();
        if (!tornadoPiercing.CheckOnCollided())
        {
            tornadoPiercing.OnCollided += OnTornadoCollided;
        }

        tornadoPiercing.Fire(attacker, speed);
    }

    public void OnTornadoCollided(GameObject attacker, GameObject defender)
    {
        if (defender == null)
        {
            return;
        }
        var attackerStats = attacker.GetComponent<ResultPlayerStats>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target, damageMultiplier);

        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}

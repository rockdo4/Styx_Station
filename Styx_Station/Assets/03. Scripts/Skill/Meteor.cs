using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : SkillBase
{
    private SkillInventory.InventorySKill meteor;
    private GameObject meteorPrefab;
    private GameObject caster;
    
    private float speed;
    private float damageMultiplier;
    private float skillStartPos;

    private int meteorCount = 5;
    private int posY = 2;

    public Meteor(SkillInventory.InventorySKill skill, GameObject prefab)
    {
        meteor = skill;
        meteorPrefab = prefab;

        damageMultiplier = meteor.skill.Skill_ATK + (meteor.upgradeLev * meteor.skill.Skill_ATK_LVUP);
        speed = 1 / meteor.skill.Skill_Speed;
        skillStartPos = meteor.skill.Skill_Start_Pos;
    }
    public override void UseSkill(GameObject attacker)
    {
        for(int i = 0; i < meteorCount; i++)
        {
            var meteor = ObjectPoolManager.instance.GetGo(meteorPrefab.name);

            //1À¯´Ö °£°ÝÀ¸·Î »ý¼ºµÊ
            meteor.transform.position = new Vector2(attacker.transform.position.x + skillStartPos + i, posY);
            var meteorite = meteor.GetComponent<Meteorite>();
            meteorite.Fire(attacker, speed);
            if(i ==0)
            {
                meteorite.AudioPlay();
            }
            if (!meteorite.CheckOnCollided())
            {
                meteorite.OnCollided += OnMeteoritecollided;
            }
        }
    }

    public void OnMeteoritecollided(GameObject attacker, GameObject defender)
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

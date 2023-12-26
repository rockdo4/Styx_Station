using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDamage : SkillBase
{
    private SkillInventory.InventorySKill soulDamage;
    private LayerMask enemyLayer;
    private GameObject particle;
    public SoulDamage(SkillInventory.InventorySKill skill, LayerMask enemyLayer, GameObject particle)
    {
        soulDamage = skill;
        this.enemyLayer = enemyLayer;
        this.particle = particle;
    }
    public override void UseSkill(GameObject attacker)
    {
        //var monsters = gam
    }

}

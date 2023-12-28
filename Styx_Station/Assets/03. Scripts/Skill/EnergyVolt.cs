using UnityEngine;

public class EnergyVolt : SkillBase
{
    private SkillInventory.InventorySKill energeyVolt;
    private GameObject energeyVoltPrefab;
    private GameObject caster;

    private float speed;
    private float damageMultiplier;
    private float skillStartPos;

    private int meteorCount = 5;
    private int posY = 2;

    public EnergyVolt(SkillInventory.InventorySKill skill, GameObject prefab)
    {
        energeyVolt = skill;
        energeyVoltPrefab = prefab;

        damageMultiplier = energeyVolt.skill.Skill_ATK + (energeyVolt.upgradeLev * energeyVolt.skill.Skill_ATK_LVUP);
        speed = 1 / energeyVolt.skill.Skill_Speed;
        skillStartPos = energeyVolt.skill.Skill_Start_Pos;
    }
    public override void UseSkill(GameObject attacker)
    {
        var volt = ObjectPoolManager.instance.GetGo(energeyVoltPrefab.name);
        if (volt == null)
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

        volt.transform.position = startPos;

        var voltPiering = volt.GetComponent<PiercingArrow>();
        if (!voltPiering.CheckOnCollided())
        {
            voltPiering.OnCollided += Oncollided;
        }

        voltPiering.Fire(attacker, speed);
    }

    public void Oncollided(GameObject attacker, GameObject defender)
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

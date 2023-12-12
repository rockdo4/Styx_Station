using UnityEngine;

public class ArrowRain : SkillBase
{
    private SkillInventory.InventorySKill arrowRain;
    private GameObject shooterPrefab;
    private GameObject castZone;
    private LayerMask enemyLayer;
    

    public ArrowRain(SkillInventory.InventorySKill skill, GameObject shooterPrefab, LayerMask enemyLayer, GameObject cz)
    {
        arrowRain = skill;
        this.shooterPrefab = shooterPrefab;
        this.enemyLayer = enemyLayer;
        castZone = cz;
    }
    public override void UseSkill(GameObject attacker)
    {
        var rects = attacker.GetComponentsInChildren<RectTransform>();
        if (rects[1] == null)
        {
            Debug.Log("ERR: No FirePoint");
            return;
        }
        var startPos = rects[1].transform.position;

        var shooter = Object.Instantiate(shooterPrefab);
        shooter.GetComponent<ArrowRainShooter>().SetShooter(enemyLayer,
            arrowRain.skill.Skill_ATK,
            arrowRain.skill.Skill_Du,
            arrowRain.skill.Skill_Start_Pos,
            castZone);

    }
}

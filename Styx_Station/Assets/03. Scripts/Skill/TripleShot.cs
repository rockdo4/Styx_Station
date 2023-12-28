using UnityEngine;

public class TripleShot : SkillBase
{
    private string piercingBowName = "PlayerPiercingArrow";
    private SkillInventory.InventorySKill tripleShot;
    private float speed;
    private GameObject shooterPrefab;
    private float multiple = 0f;

    public TripleShot(SkillInventory.InventorySKill t, GameObject shooterPrefab)
    {
        tripleShot = t;
        speed = 1 / this.tripleShot.skill.Skill_Speed; //1���� ���޿� �ɸ��� �ð� -> �ʴ� �̵��ӵ� ��ȯ
        this.shooterPrefab = shooterPrefab;

        multiple = tripleShot.skill.Skill_ATK + (tripleShot.upgradeLev * tripleShot.skill.Skill_ATK_LVUP);
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
        shooter.GetComponent<TripleShotShooter>().SetShooter(attacker, startPos, piercingBowName, speed, multiple);
    }
}

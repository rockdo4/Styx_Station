using UnityEngine;

public class TripleShot : SkillBase
{
    private string piercingBowName = "PlayerPiercingArrow";
    private SkillInventory.InventorySKill tripleShot;
    private float speed;
    private GameObject shooterPrefab;

    public TripleShot(SkillInventory.InventorySKill tripleShot, GameObject shooterPrefab)
    {
        this.tripleShot = tripleShot;
        speed = 1 / this.tripleShot.skill.Skill_Speed; //1���� ���޿� �ɸ��� �ð� -> �ʴ� �̵��ӵ� ��ȯ
        this.shooterPrefab = shooterPrefab;
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
        shooter.GetComponent<TripleShotShooter>().SetShooter(attacker, startPos, piercingBowName, speed, tripleShot.skill.Skill_ATK);
    }
}

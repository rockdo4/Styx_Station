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

    public override void UseSkill(GameObject attacker, GameObject defender)
    {
        var rects = attacker.GetComponentsInChildren<RectTransform>();
        if (rects[1] == null)
        {
            Debug.Log("ERR: No FirePoint");
            return;
        }
        var startPos = rects[1].transform.position;

        var shooter = Instantiate(shooterPrefab);
        shooter.GetComponent<Shooter>().SetShooter(attacker, startPos, piercingBowName, speed);

    }


    private void OnBowCollided(GameObject attacker, GameObject defender)
    {
        //�ִϸ��̼� ���� -> ���� Ÿ�� ���̿� �Ÿ��� ������ ����� �� �ֱ� ������ üũ �ʿ�
        if (defender == null) //���������� ���� ������ ���� ����
        {
            return;
        }
        var attackerStats = attacker.GetComponent<ResultPlayerStats>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target);

        //Iattackable�� ��� ���� ���� ������ �� ���� �ֱ� ������ ��� ȣ��
        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}

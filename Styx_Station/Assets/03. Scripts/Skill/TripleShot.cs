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
        speed = 1 / this.tripleShot.skill.Skill_Speed; //1유닛 도달에 걸리는 시간 -> 초당 이동속도 변환
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
        //애니메이션 시작 -> 실제 타격 사이에 거리와 방향이 변경될 수 있기 때문에 체크 필요
        if (defender == null) //남아있지만 죽은 상태일 수도 있음
        {
            return;
        }
        var attackerStats = attacker.GetComponent<ResultPlayerStats>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target);

        //Iattackable을 상속 받은 것이 여러개 일 수도 있기 때문에 모두 호출
        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}

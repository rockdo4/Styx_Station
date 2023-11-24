using UnityEngine;


[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/RangeWeapon")]
public class RangeWeapon : AttackDefinition
{
    public ProjectileBow bowPrefab;
    public float speed; //�߻�ü �̵� �ӵ�
    public AttackerType attackerType;

    public override void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        var rects = attacker.GetComponentsInChildren<RectTransform>();
        if (rects[1] == null)
        {
            Debug.Log("ERR: No BezierPoint");
            return;
        }
        if (rects[2] == null)
        {
            Debug.Log("ERR: No FirePoint");
            return;
        }

        var startPos = rects[2].transform.position;
        var targetPos = defender.transform.position;
        targetPos.y = startPos.y;

        var direction = Vector2.left;
        //var bow = Instantiate(bowPrefab, startPos, Quaternion.identity);
        var bow = ObjectPoolManager.instance.GetGo(bowPrefab.name);
        bow.transform.position = startPos;
        var projectileBow = bow.GetComponent<ProjectileBow>();

        projectileBow.OnCollided += OnBowCollided;
        projectileBow.Fire(attacker, speed, targetPos, rects[1].transform.position);

    }

    private void OnBowCollided(GameObject attacker, GameObject defender)
    {
        //�ִϸ��̼� ���� -> ���� Ÿ�� ���̿� �Ÿ��� ������ ����� �� �ֱ� ������ üũ �ʿ�
        if (defender == null) //���������� ���� ������ ���� ����
        {
            return;
        }

        Attack attack = new Attack();
        switch(attackerType)
        {
            case AttackerType.Enemy:
                var characterStats = attacker.GetComponent<MonsterStats>();
                var targetStats = defender.GetComponent<ResultPlayerStats>();
                attack = CreateAttackToPlayer(characterStats, targetStats);
                break;
            case AttackerType.Player:
                var attackerStats = attacker.GetComponent<ResultPlayerStats>();
                var target = defender.GetComponent<MonsterStats>();
                attack = CreateAttackToMonster(attackerStats, target);
                break;
            default:
                break;
        }
        //Iattackable�� ��� ���� ���� ������ �� ���� �ֱ� ������ ��� ȣ��
        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}

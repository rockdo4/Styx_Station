using UnityEngine;


[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/RangeWeapon")]
public class RangeWeapon : AttackDefinition
{
    public PlayerBow bowPrefab;
    public float speed; //발사체 이동 속도
    public AttackerType attackerType;
    public int maxShotAngle;
    public int minShotAngle;
    public int shotAngleOffset;

    public override void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        var rects = attacker.GetComponentsInChildren<RectTransform>();
        if (rects[1] == null)
        {
            Debug.Log("ERR: No FirePoint");
            return;
        }

        var startPos = rects[1].transform.position;

        var angle = Random.Range(minShotAngle, maxShotAngle + 1);
        Quaternion rotaion = Quaternion.Euler(0, 0, angle*shotAngleOffset);
        Debug.Log(angle * shotAngleOffset);
        var newDirection = rotaion * Vector2.right;
        var targetPos = startPos + newDirection.normalized * 10f;

        var bow = ObjectPoolManager.instance.GetGo(bowPrefab.name);
        bow.transform.position = startPos;
        var playerBow = bow.GetComponent<PlayerBow>();

        playerBow.OnCollided += OnBowCollided;
        playerBow.Fire(attacker, speed, targetPos);

    }

    private void OnBowCollided(GameObject attacker, GameObject defender)
    {
        //애니메이션 시작 -> 실제 타격 사이에 거리와 방향이 변경될 수 있기 때문에 체크 필요
        if (defender == null) //남아있지만 죽은 상태일 수도 있음
        {
            return;
        }

        //Attack attack = new Attack();
        //switch(attackerType)
        //{
        //    case AttackerType.Enemy:
        //        var characterStats = attacker.GetComponent<MonsterStats>();
        //        var targetStats = defender.GetComponent<ResultPlayerStats>();
        //        attack = CreateAttackToPlayer(characterStats, targetStats);
        //        break;
        //    case AttackerType.Player:
        //        var attackerStats = attacker.GetComponent<ResultPlayerStats>();
        //        var target = defender.GetComponent<MonsterStats>();
        //        attack = CreateAttackToMonster(attackerStats, target);
        //        break;
        //    default:
        //        break;
        //}

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

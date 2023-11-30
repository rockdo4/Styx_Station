using UnityEngine;


[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/MonsterMeleeWeapon")]
public class MonsterMeleeWeapon : AttackDefinition
{
    public override void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        //애니메이션 시작 -> 실제 타격 사이에 거리와 방향이 변경될 수 있기 때문에 체크 필요
        if (defender.GetComponent<ResultPlayerStats>().playerCurrentHp <= 0) //남아있지만 죽은 상태일 수도 있음
        {
            return;
        }

        //거리 검사
        if(Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
        {
            return;
        }

        var characterStats = attacker.GetComponent<MonsterStats>();
        var targetStats = defender.GetComponent<ResultPlayerStats>();
        Attack attack = CreateAttackToPlayer(characterStats, targetStats);

        //Iattackable을 상속 받은 것이 여러개 일 수도 있기 때문에 모두 호출
        var attackables = defender.GetComponents<IAttackable>();
        foreach(var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}

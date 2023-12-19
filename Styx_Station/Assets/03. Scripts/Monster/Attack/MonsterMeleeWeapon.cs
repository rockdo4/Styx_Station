using UnityEngine;


[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/MonsterMeleeWeapon")]
public class MonsterMeleeWeapon : AttackDefinition
{
    public override void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if(attacker.GetComponent<MonsterController>() != null)
        {
            range = attacker.GetComponent<MonsterController>().range;
        }
        else
        {
            Debug.Log("monsterController is null");
        }
        //�ִϸ��̼� ���� -> ���� Ÿ�� ���̿� �Ÿ��� ������ ����� �� �ֱ� ������ üũ �ʿ�
        if (defender.GetComponent<ResultPlayerStats>().playerCurrentHp <= 0) //���������� ���� ������ ���� ����
        {
            return;
        }

        //�Ÿ� �˻�
        if(Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
        {
            return;
        }

        var characterStats = attacker.GetComponent<MonsterStats>();
        var targetStats = defender.GetComponent<ResultPlayerStats>();
        Attack attack = CreateAttackToPlayer(characterStats, targetStats);

        //Iattackable�� ��� ���� ���� ������ �� ���� �ֱ� ������ ��� ȣ��
        var attackables = defender.GetComponents<IAttackable>();
        foreach(var attackable in attackables)
        {
            if (defender.GetComponent<PlayerController>().currentStates == States.Die)
            {
                return;
            }
            attackable.OnAttack(attacker, attack);
        }
    }
}

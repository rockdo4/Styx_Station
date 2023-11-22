using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/MonsterMeleeWeapon")]
public class MonsterMeleeWeapon : AttackDefinition
{
    public override void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        //�ִϸ��̼� ���� -> ���� Ÿ�� ���̿� �Ÿ��� ������ ����� �� �ֱ� ������ üũ �ʿ�
        if (defender == null) //���������� ���� ������ ���� ����
        {
            return;
        }

        //�Ÿ� �˻�
        if(Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
        {
            return;
        }

        ////���� �˻�
        //var dir = defender.transform.position - attacker.transform.position;
        //dir.Normalize();
        //var dot = Vector3.Dot(dir, attacker.transform.forward);
        //if (dot < 0.5f)
        //    return;
        
        
        var characterStats = attacker.GetComponent<MonsterStats>();
        var targetStats = defender.GetComponent<ResultPlayerStats>();
        Attack attack = CreateAttackToPlayer(characterStats, targetStats);

        //Iattackable�� ��� ���� ���� ������ �� ���� �ֱ� ������ ��� ȣ��
        var attackables = defender.GetComponents<IAttackable>();
        foreach(var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}

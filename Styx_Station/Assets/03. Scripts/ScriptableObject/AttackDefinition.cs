using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")] //��Ʈ����Ʈ
//������ ���� �̸�, �Һз�
public class AttackDefinition : ScriptableObject
    //���⼭ �����Ǵ� �ν��Ͻ����� asset ���� �ȿ� ���� asset
{
    public float cooldown; //������ �� �ٽ� ������ �������� �ð�
    public float range; //���� ����
    public float minDamage;
    public float maxDamage;
    public float criticalChance; //ġ��Ÿ Ȯ�� 0.0 ~ 1.0
    public float criticalMultiplier; //ġ��Ÿ ���

    
    public Attack CreateAttackToPlayer(MonsterStats attacker, ResultPlayerStats defender)
    {
        float damage = attacker.damage;
        //if (defender != null) //���� �߰��Ǹ� ����
        //{
        //    damage -= defender.armor; //������ �� ����
        //}

        return new Attack((int)damage, false);
    }

    //public Attack CreateAttackToPlayer(CharacterStats attacker, CharacterStats defender)
    //{
    //    //attacker�� �������� �⺻ ������. �⺻ �������� �߰� �������� ���ϴ� ���
    //    float damage = attacker.damage;
    //    damage += Random.Range(minDamage, maxDamage);

    //    bool critical = Random.value < criticalChance; //value�� 0.0 ~ 1.0 ������ �Ǽ� ����
    //    if (critical)
    //    {
    //        damage *= criticalMultiplier;
    //    }

    //    if (defender != null)
    //    {
    //        damage -= defender.armor; //������ �� ����
    //    }

    //    return new Attack((int)damage, critical);
    //}

    public virtual void ExecuteAttack(GameObject attacker, GameObject defender)
    {

    }
}

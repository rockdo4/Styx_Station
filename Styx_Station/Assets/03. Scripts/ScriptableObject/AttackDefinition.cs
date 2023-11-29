using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
    public BigInteger currentDamage =new BigInteger(0);
    
    public Attack CreateAttackToPlayer(MonsterStats attacker, ResultPlayerStats defender)
    {
        BigInteger damage = attacker.damage;
        //if (defender != null) //���� �߰��Ǹ� ����
        //{
        //    damage -= defender.armor; //������ �� ����
        //}

        return new Attack(damage, false);
    }

    public Attack CreateAttackToMonster(ResultPlayerStats attacker, MonsterStats defender)
    {
        float criticalChance = attacker.GetCritical();   //0~100���� ġȮ
        float randomFloat = Random.Range(0f, 100f);
        randomFloat = Mathf.Round(randomFloat * 10f) / 10f;
        bool isCritical = randomFloat <= criticalChance;

        currentDamage = attacker.ResultMonsterNormalDamage(isCritical, 0);

        return new Attack(currentDamage, false);
    }
    public Attack CreateAttackToMonster(PetController attacker, MonsterStats defender)
    {
        currentDamage = attacker.GetPower();
        if(currentDamage == -1)
        {
            currentDamage = 0;
        }
        return new Attack(currentDamage, false);
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

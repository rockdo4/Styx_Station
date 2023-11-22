using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")] //어트리뷰트
//생성할 파일 이름, 소분류
public class AttackDefinition : ScriptableObject
    //여기서 생성되는 인스턴스들은 asset 폴더 안에 들어가는 asset
{
    public float cooldown; //공격한 후 다시 공격할 때까지의 시간
    public float range; //공격 범위
    public float minDamage;
    public float maxDamage;
    public float criticalChance; //치명타 확률 0.0 ~ 1.0
    public float criticalMultiplier; //치명타 계수

    
    public Attack CreateAttackToPlayer(MonsterStats attacker, ResultPlayerStats defender)
    {
        float damage = attacker.damage;
        //if (defender != null) //방어력 추가되면 변경
        //{
        //    damage -= defender.armor; //데미지 방어량 제외
        //}

        return new Attack((int)damage, false);
    }

    //public Attack CreateAttackToPlayer(CharacterStats attacker, CharacterStats defender)
    //{
    //    //attacker의 데미지는 기본 데미지. 기본 데미지에 추가 데미지를 더하는 방식
    //    float damage = attacker.damage;
    //    damage += Random.Range(minDamage, maxDamage);

    //    bool critical = Random.value < criticalChance; //value는 0.0 ~ 1.0 랜덤한 실수 리턴
    //    if (critical)
    //    {
    //        damage *= criticalMultiplier;
    //    }

    //    if (defender != null)
    //    {
    //        damage -= defender.armor; //데미지 방어량 제외
    //    }

    //    return new Attack((int)damage, critical);
    //}

    public virtual void ExecuteAttack(GameObject attacker, GameObject defender)
    {

    }
}

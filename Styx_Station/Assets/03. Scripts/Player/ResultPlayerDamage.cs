using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResultPlayerDamage : MonoBehaviour
{

    private PlayerAttributes playerAttribute;

    private int weaponPower = 1; // 인벤토리후 재적용
    private float weaponBuff =1f; // 인벤토리후 재적용

    private float normalMonsterDamage;
    private float skillMonsterDamage;

    private void Awake()
    {
        playerAttribute = GetComponent<PlayerAttributes>();
    }

    private int GetPlayerPower()
    {
        return (int)((playerAttribute.attackPower + SharedPlayerStats.GetPlayerPower() + weaponPower) * weaponBuff);
    }

    private float GetPowerBoost()
    {
        var damage = GetPlayerPower();
        float upgrad = 0.01f;
        var boost = SharedPlayerStats.GetPlayerPowerBoost() * 0.1f;
        
        if (damage > UnitConverter.A1 && damage <= UnitConverter.B1)
        {
            boost = UnitConverter.A1 * upgrad;
        }
        else if(damage > UnitConverter.B1 && damage < UnitConverter.C1)
        {
            boost = UnitConverter.B1 * upgrad;
        }
        else if (damage > UnitConverter.C1 && damage < UnitConverter.D1)
        {
            boost = UnitConverter.C1 * upgrad;
        }
        return damage + (damage * (boost));
    }

    private void GetNormalDamage()
    {
        var power = GetPlayerPower();
        normalMonsterDamage = power + (power * GetPowerBoost() *
            GetMonsterDamage());
    }




    
    private float GetMonsterDamage()
    {
        var damage = GetPlayerPower();
        return damage + (damage * (SharedPlayerStats.GetMonsterDamagePower() / 100));
    }

   
    private void GetNoramlCriticalDamage()
    {
        normalMonsterDamage = (GetPlayerPower() + (GetPlayerPower() * GetPowerBoost() *
            SharedPlayerStats.GetAttackCriticlaPower()))* GetMonsterDamage();
    }

    private void GetSkillDamage(float skillCount)
    {
        skillMonsterDamage = (GetPlayerPower() + (GetPlayerPower() * SharedPlayerStats.GetPlayerPowerBoost()) * skillCount
            * SharedPlayerStats.GetMonsterDamagePower());
    }
    private void GetSkillCriticalDamage(float skillCount, float skillPower)
    {
        skillMonsterDamage = (GetPlayerPower() + (GetPlayerPower() * SharedPlayerStats.GetPlayerPowerBoost()) * skillCount
            *SharedPlayerStats.GetAttackCriticlaPower()* (SharedPlayerStats.GetMonsterDamagePower()+ skillPower));
    }

    public float ResultMonsterNormalDamage(bool isCritical ,float monsterDefense) // 몬스터 노멀 최종 데미지 계산
    {
        // 크리티컬 유무 : isCritical, 몬스터가 받는 피해 감소 : monsterDefense
        if (isCritical)
        {
            GetNoramlCriticalDamage();
        }
        else
        {
            GetNormalDamage();
        }
        return normalMonsterDamage - (normalMonsterDamage* (1- monsterDefense));
    }

    public float ResultMonsterSkillDamage(bool isCritical ,float monsterDefense , float a , float t)
    // a 는 스킬 계수 t는 스킬피해량 임시 용 
    {
        if (isCritical)
        {
            GetSkillCriticalDamage(a,t);
        }
        else
        {
            GetSkillDamage(a);
        }
        return skillMonsterDamage - (skillMonsterDamage * (1 - monsterDefense));
    }
}
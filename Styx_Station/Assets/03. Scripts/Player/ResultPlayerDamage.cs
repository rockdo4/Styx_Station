using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResultPlayerDamage : MonoBehaviour
{

    private PlayerAttributes playerAttribute;

    private int weaponPower = 1; // �κ��丮�� ������
    private float weaponBuff =1f; // �κ��丮�� ������

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

    public float ResultMonsterNormalDamage(bool isCritical ,float monsterDefense) // ���� ��� ���� ������ ���
    {
        // ũ��Ƽ�� ���� : isCritical, ���Ͱ� �޴� ���� ���� : monsterDefense
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
    // a �� ��ų ��� t�� ��ų���ط� �ӽ� �� 
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
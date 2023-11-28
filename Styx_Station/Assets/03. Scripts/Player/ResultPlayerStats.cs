using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResultPlayerStats : MonoBehaviour
{

    private PlayerAttributes playerAttribute;

    private int weaponPower = 1; // 인벤토리후 재적용
    private float weaponBuff =1f; // 인벤토리후 재적용

    private Dictionary<string, int> attackPowerTabel = new Dictionary<string, int>(); 
    //단위별의 값들을 저장


    private float normalMonsterDamage;
    private float skillMonsterDamage;

    private void Awake()
    {
        playerAttribute = GetComponent<PlayerAttributes>();
        attackPowerTabel.Add("None", 0);
        attackPowerTabel.Add("A1", 0);
        attackPowerTabel.Add("B1", 0);
        attackPowerTabel.Add("C1", 0);
    }

    private int GetPlayerPower() // player 공격력 계산 
    // 계산식 (기본스탯 공격력 + 업그레이드공격력  +장비 공격력 ) * 버프량 
    // 업그레이드 공겨격은 전체 공격력이 1000이하일때는 10증가 , 이상일때는 A B C  단위 * 0.01 
    // 대체안으로 * 10으로 증가로 일단 변경 

    {
        //int power = (int)((playerAttribute.attackPower + (attackPowerTabel["None"] * 10) +
        //      (attackPowerTabel["A1"] * UnitConverter.A1 * 0.01) + (attackPowerTabel["B1"] * UnitConverter.B1 * 0.01) +
        //      (attackPowerTabel["C1"] * UnitConverter.C1 * 0.01) + weaponPower) * weaponBuff);
        //var playerPower = SharedPlayerStats.GetPlayerPower() - 1;
        //if (power < UnitConverter.A1)
        //{
        //    attackPowerTabel["None"] = playerPower;
        //    return power;
        //}
        //else if (power > UnitConverter.A1 && power < UnitConverter.B1)
        //{
        //    attackPowerTabel["A1"] = playerPower - attackPowerTabel["None"]-1;
        //    return (int)((playerAttribute.attackPower + (attackPowerTabel["None"] * 10) +
        //        (attackPowerTabel["A1"] * UnitConverter.A1 * 0.01) + weaponPower) * weaponBuff);
        //}
        //else if (power > UnitConverter.B1 && power < UnitConverter.C1)
        //{
        //    attackPowerTabel["B1"] = playerPower - attackPowerTabel["None"] - attackPowerTabel["A1"];
        //    return (int)((playerAttribute.attackPower + (attackPowerTabel["None"] * 10) +
        //      (attackPowerTabel["A1"] * UnitConverter.A1 * 0.01) + (attackPowerTabel["B1"] * UnitConverter.B1 * 0.01) +
        //      weaponPower) * weaponBuff);
        //}
        //else if (power > UnitConverter.C1 && power < UnitConverter.D1)
        //{
        //    attackPowerTabel["B1"] = playerPower - attackPowerTabel["None"] - attackPowerTabel["A1"];
        //    return (int)((playerAttribute.attackPower + (attackPowerTabel["None"] * 10) +
        //      (attackPowerTabel["A1"] * UnitConverter.A1 * 0.01) + (attackPowerTabel["B1"] * UnitConverter.B1 * 0.01) +
        //      (attackPowerTabel["C1"] * UnitConverter.C1 * 0.01)+weaponPower) * weaponBuff);
        //}
        // 위는 가중치 적용

        // 아래는 가중치 적용이 없는 상태

        // + 를 *로 변경 될수 있음
        return (int)((playerAttribute.attackPower +((SharedPlayerStats.GetPlayerPower()-1) * 10) + weaponPower) * weaponBuff);
    }

    private float GetPowerBoost()
        //공격력증폭
    {
        float upgrad = 0.1f;
        var boost = (SharedPlayerStats.GetPlayerPowerBoost() -1)* upgrad;
        return boost;
    }

    private float GetCritclaPower()
    {
        return (SharedPlayerStats.GetAttackCriticlaPower() - 1) * 0.1f + 1.5f;
    }

    private float GetMonsterDamage()
    {
        return (SharedPlayerStats.GetMonsterDamagePower() - 1) * 0.1f;
    }

    //private float GetMonsterDamage()
    //    // 일반 몬스터 피해량 계산 
    //{
    //    var damage = GetPlayerPower();
    //    return damage + (damage * ((SharedPlayerStats.GetMonsterDamagePower()-1) / 100));
    //}


    private void GetNormalDamage()
    {
        var power = GetPlayerPower();
        normalMonsterDamage = power +(power * GetPowerBoost()) * GetMonsterDamage();
    }
    private void GetNoramlCriticalDamage()
    {
        var power = GetPlayerPower();
        normalMonsterDamage = power + (power * GetPowerBoost())*
            GetCritclaPower() * GetMonsterDamage();
    }


    public float ResultMonsterNormalDamage(bool isCritical, float monsterDefense) // 몬스터 노멀 최종 데미지 계산
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
        //Debug.Log(normalMonsterDamage);
        return normalMonsterDamage - (normalMonsterDamage * monsterDefense);
    }

    // 몬스터 일반 공격임

    private void GetSkillDamage(float skillCount)
        //skillCount 스킬 계수임
    {
        var power = GetPlayerPower();
        skillMonsterDamage = power + (power * GetPowerBoost()) * skillCount
            * GetMonsterDamage();
    }


    private void GetSkillCriticalDamage(float skillCount, float skillPower)
    {
        var power = GetPlayerPower();
        skillMonsterDamage = (power + (power * GetPowerBoost()) * skillCount
            * GetCritclaPower() * 0.1f + 1.5f) * (GetMonsterDamage() + skillPower);
    }

    public float ResultMonsterSkillDamage(bool isCritical, float monsterDefense, float a, float t)
    // a 는 스킬 계수 t는 스킬피해량 임시 용 
    {
        if (isCritical)
        {
            GetSkillCriticalDamage(a, t);
        }
        else
        {
            GetSkillDamage(a);
        }
        return skillMonsterDamage - (skillMonsterDamage * (1 - monsterDefense));
    }

    // 보스피해량은 현재 일반 몬스터 피해량과 같게 해둔 상태 공식이 보이지가 않음

    public float GetCritical()
    {
        return (SharedPlayerStats.GetAttackCritical() - 1) * 0.1f;
    }
}
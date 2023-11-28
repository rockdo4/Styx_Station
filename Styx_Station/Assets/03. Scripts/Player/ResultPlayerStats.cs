using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class ResultPlayerStats : MonoBehaviour
{
    private Inventory inventory;
    private PlayerAttributes playerAttribute;

    public BigInteger playerMaxHp = new BigInteger(0);
    public BigInteger playerCurrentHp = new BigInteger(0);

    private BigInteger normalMonsterDamage = new BigInteger(0);
    private BigInteger skillMonsterDamage = new BigInteger(0);

    public int percentInt = 100;
    public float percentFloat = 100f;
    [Header("공격력 1강당")]
    public int increaseUpgradePower = 10;
    [Header("공격력 증폭 1강당")]
    public float increaseUpgradePowerBoost = 0.1f;
    [Header("플레이어 공격력 증폭 부스트 N*0.1 /100 계산 ")]
    public int playerPowerBoostPercent = 1000;
    [Header("크리티컬 1강당")]
    public float increaseUpgradeCritical = 0.01f;
    [Header("크리티컬 고정값")]
    public float increaseUpgradeCriticalDefault = 150f;
    [Header("몬스터 N 수치 ")]
    public float monsterDamageFloat = 0.1f;
    [Header("몬스터 N*0.1 /100 계산 ")]
    public int monsterDamagePercent = 1000;
    [Header("치명타피해  N*0.01  + 150% 계산 ")]
    public int criticlDamage = 100;
    [Header("스킬 계수 100율로  계산 ")]
    public int skillDamage = 100;
    [Header("체력 1강당")]
    public int increaseUpgradeHp= 5;
    [Header("체력 회복 1강당")]
    public int increaseUpgradeHealing = 10;
    private void Awake()
    {
        playerAttribute = GetComponent<PlayerAttributes>();
        inventory = InventorySystem.Instance.inventory;
        SettingPlayerMaxHP();
    }

    private void OnEnable()
    {
        SharedPlayerStats.resultPlayerStats = this;
    }
    private void Update()
    {
        playerCurrentHp += SharedPlayerStats.GetHealing() * increaseUpgradeHealing;
        if(playerCurrentHp >=playerMaxHp)
        {
            playerCurrentHp = playerMaxHp;
        }
    }
    public BigInteger GetPlayerPowerByNonInventory()
    {
        return (int)playerAttribute.attackPower + (SharedPlayerStats.GetPlayerPower() - 1);
    }
    public BigInteger GetPlayerPower() 
    {
        return (int)((playerAttribute.attackPower + ((SharedPlayerStats.GetPlayerPower() - 1) * increaseUpgradePower) + (int)inventory.t_Attack) * (int)inventory.t_AttackPer);
    }

    private float GetPowerBoost()
        //공격력증폭
    {
        var boost = ((SharedPlayerStats.GetPlayerPowerBoost() -1)* increaseUpgradePowerBoost) / percentFloat;
        return boost;
    }

    private float GetCritclaPower()
    {
        return (SharedPlayerStats.GetAttackCriticlaPower() - 1) * increaseUpgradeCritical + increaseUpgradeCriticalDefault;
    }

    private float GetMonsterDamage()
    {
        return (SharedPlayerStats.GetMonsterDamagePower() - 1) * monsterDamageFloat;
    }

    private void GetNormalDamage()
    {
        var power = GetPlayerPower();
        var powerBoostResult = (int)(GetPowerBoost() * playerPowerBoostPercent) / playerPowerBoostPercent;
        var monsterDamageResult = (int)(GetMonsterDamage() * monsterDamagePercent) / monsterDamagePercent;
        if (powerBoostResult == 0) powerBoostResult = 1;
        if (monsterDamageResult == 0) monsterDamageResult = 1;
        normalMonsterDamage = power + (power * powerBoostResult) * monsterDamageResult;
    }
    private void GetNoramlCriticalDamage()
    {
        var power = GetPlayerPower();
        var powerBoostResult = (int)(GetPowerBoost() * playerPowerBoostPercent) / playerPowerBoostPercent;
        var critclaPowerResult = (int)(GetCritclaPower() * criticlDamage) / criticlDamage;
        var monsterDamageResult = (int)(GetMonsterDamage() * monsterDamagePercent) / monsterDamagePercent;
        if (critclaPowerResult == 0) critclaPowerResult = 1;
        if (powerBoostResult == 0) powerBoostResult = 1;
        if (monsterDamageResult == 0) monsterDamageResult = 1;
        normalMonsterDamage = power + (power * powerBoostResult)* critclaPowerResult * monsterDamageResult;
    }


    public BigInteger ResultMonsterNormalDamage(bool isCritical, float monsterDefense) // 몬스터 노멀 최종 데미지 계산
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
        Debug.Log(normalMonsterDamage);
        var monsterDefenseResult = (int)(monsterDefense * percentInt) / percentInt;
        return normalMonsterDamage - (normalMonsterDamage * monsterDefenseResult);

    }

    // 몬스터 일반 공격임
    private void GetSkillDamage(float skillCount)
        //skillCount 스킬 계수임
    {
        var power = GetPlayerPower();
        var powerBoostResult = (int)(GetPowerBoost() * playerPowerBoostPercent) / playerPowerBoostPercent;
        var monsterDamageResult = (int)(GetMonsterDamage() * monsterDamagePercent) / monsterDamagePercent;
        var skillCountResult = (int)(skillCount * skillDamage) / skillDamage;
        if (powerBoostResult == 0) powerBoostResult = 1;
        if (monsterDamageResult == 0) monsterDamageResult = 1;
        if (skillCountResult == 0) skillCountResult = 1;
        skillMonsterDamage = power + (power * powerBoostResult) * skillCountResult * monsterDamageResult;
    }


    private void GetSkillCriticalDamage(float skillCount, float skillPower)
    {
        var power = GetPlayerPower();
        var powerBoostResult = (int)(GetPowerBoost() * playerPowerBoostPercent) / playerPowerBoostPercent;
        var skillCountResult = (int)(skillCount * skillDamage) / skillDamage;
        var critclaPowerResult = (int)(GetCritclaPower() * criticlDamage) / criticlDamage;
        var monsterDamageResult = (int)(GetMonsterDamage() * monsterDamagePercent) / monsterDamagePercent;
        var sillPowerResult = (int)(skillPower * skillDamage) / skillDamage;
        if (critclaPowerResult == 0) critclaPowerResult = 1;
        if (powerBoostResult == 0) powerBoostResult = 1;
        if (monsterDamageResult == 0) monsterDamageResult = 1;
        if (skillCountResult == 0) skillCountResult = 1;
        skillMonsterDamage = (power + (power * powerBoostResult) * skillCountResult * critclaPowerResult * (monsterDamageResult + sillPowerResult));
    }

    public BigInteger ResultMonsterSkillDamage(bool isCritical, float monsterDefense, float a, float t)
    {
        if (isCritical)
        {
            GetSkillCriticalDamage(a, t);
        }
        else
        {
            GetSkillDamage(a);
        }
        var monsterDefenseResult = (int)(monsterDefense * percentInt) / percentInt;
        return skillMonsterDamage - (skillMonsterDamage * monsterDefenseResult);
    }

    // 보스피해량은 현재 일반 몬스터 피해량과 같게 해둔 상태 공식이 보이지가 않음

    public float GetCritical()
    {
        return (SharedPlayerStats.GetAttackCritical() - 1) * 0.1f;
    }

    public void SettingPlayerMaxHP()
    {
        playerMaxHp = playerAttribute.MaxHp + ((SharedPlayerStats.GetHp() - 1) * increaseUpgradeHp);
    }

    public void TakeDamage(BigInteger damage)
    {
        playerCurrentHp -= damage;
        if(playerCurrentHp <= 0) 
        {
            Debug.Log("PlayerDie");
        }
    }
    
}
using System.Numerics;
using UnityEngine;


public class ResultPlayerStats : MonoBehaviour
{
    private StateSystem state;
    public PlayerAttributes playerAttribute;

    public BigInteger playerMaxHp = new BigInteger(0);
    public BigInteger playerUpgradeMaxHP = new BigInteger(0);
    public BigInteger playerCurrentHp = new BigInteger(0);

    private BigInteger playerPower =new BigInteger(0);
    private int normalHp;
    private int healing;

    private BigInteger normalMonsterDamage = new BigInteger(0);
    private BigInteger skillMonsterDamage = new BigInteger(0);
    private int prevUpgradeHp; 
    public int percentInt = 100;
    public float percentFloat = 100f;
    private float nowTime;
    public float healingTimer =1f;
    public int increaseUpgradePower = 10;
    public float increaseUpgradePowerBoost = 0.1f;
    public int playerPowerBoostPercent = 1000;
    public float increaseUpgradeCritical = 0.01f;
    public float increaseUpgradeCriticalDefault = 150f;
    public float monsterDamageFloat = 0.1f;
    public int monsterDamagePercent = 1000;
    public int criticlDamage = 100;
    public int skillDamage = 100;
    public int increaseUpgradeHp= 5;
    public int increaseUpgradeHealing = 10;
    private void Awake()
    {
        state = StateSystem.Instance;
        state.state = this;
        SettingPlayerMaxHP();
    }
    private void Start()
    {
        SharedPlayerStats.resultPlayerStats = this;
        CurrentMaxHpSet();
        playerCurrentHp = playerMaxHp;
    }
    private void OnEnable()
    {
        SharedPlayerStats.resultPlayerStats = this;
        prevUpgradeHp = SharedPlayerStats.GetHp() - 1;
        playerCurrentHp = playerAttribute.MaxHp + (prevUpgradeHp * increaseUpgradeHp);
        playerMaxHp = playerAttribute.MaxHp + (prevUpgradeHp * increaseUpgradeHp);
        //CurrentMaxHpSet();
        //playerCurrentHp = playerMaxHp;
    }
    private void FixedUpdate()
    {
        if (nowTime + healingTimer < Time.time)
        {
            nowTime = Time.time;
            Healing();
            //playerCurrentHp += state.TotalState.HealHealth * increaseUpgradeHealing / 10;// + (int)inventory.t_HealHealth;
            //if (playerCurrentHp >= playerMaxHp)
            //{
            //    playerCurrentHp = playerMaxHp;
            //}
        }
        //if(inventory != null && inventory.t_Health <=0f)
        //{
        //    SettingPlayerMaxHP();
        //}
    }

    public double GetNormalizedHealth()
    {
        return (double)playerCurrentHp / (double)playerMaxHp;
    }
    private void Healing()
    {
        playerCurrentHp += state.TotalState.HealHealth * increaseUpgradeHealing / 10;// + (int)inventory.t_HealHealth;
        if (playerCurrentHp >= playerMaxHp)
        {
            playerCurrentHp = playerMaxHp;
        }

        var normalHp = GetNormalizedHealth();
        UIManager.Instance.SetHpGauge(normalHp);
    }
    public BigInteger GetPlayerPowerByNonInventory()
    {
        return (int)playerAttribute.attackPower + ((SharedPlayerStats.GetPlayerPower() - 1)* increaseUpgradePower);
    }

    public BigInteger GetPlayerPower() 
    {
        playerPower = state.TotalState.Attack;
        return playerPower;
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
        normalMonsterDamage = power + (power * (int)state.TotalState.NormalDamage / 100);
    }
    private void GetNoramlCriticalDamage()
    {
        var power = GetPlayerPower();
        var critclaPowerResult = (int)(GetCritclaPower() * criticlDamage) / criticlDamage;
        critclaPowerResult += PlayerBuff.Instance.buffData.criticalPowerBuff / PlayerBuff.Instance.percent;
        normalMonsterDamage = (power + (power * critclaPowerResult)) + (((power + (power * critclaPowerResult)) * (int)state.TotalState.NormalDamage) / 100);
    }


    public BigInteger ResultMonsterNormalDamage(bool isCritical, float monsterDefense)
    {
        if (isCritical)
        {
            GetNoramlCriticalDamage();
        }
        else
        {
            GetNormalDamage();
        }
        var monsterDefenseResult = (int)(monsterDefense * percentInt) / percentInt;
        return normalMonsterDamage - (normalMonsterDamage * monsterDefenseResult);

    }

    private void GetSkillDamage(float skillCount)
    {
        var power = GetPlayerPower();
        skillMonsterDamage = (power * (int)skillCount / 100) + ((power * (int)skillCount / 100) * ((int)state.TotalState.NormalDamage + (int)state.TotalState.SkillDamage) / 100);
    }


    private void GetSkillCriticalDamage(float skillCount)
    {
        var power = GetPlayerPower();

        var critclaPowerResult = (int)(GetCritclaPower() * criticlDamage) / criticlDamage;
        critclaPowerResult += PlayerBuff.Instance.buffData.criticalPowerBuff / PlayerBuff.Instance.percent;

        skillMonsterDamage = (power * (int)skillCount / 100)
            + ((power * (int)skillCount / 100) * ((power * (int)skillCount / 100)
            + power * (int)skillCount / 100) * critclaPowerResult)
            + ((power * (int)skillCount / 100)
            + (power * (int)skillCount / 100) * (power * (int)skillCount / 100) * critclaPowerResult) * (((int)state.TotalState.NormalDamage + (int)state.TotalState.SkillDamage) / 100);
    }

    public BigInteger ResultMonsterSkillDamage(bool isCritical, float monsterDefense, float a)
    {
        if (isCritical)
        {
            GetSkillCriticalDamage(a);
        }
        else
        {
            GetSkillDamage(a);
        }
        var monsterDefenseResult = (int)(monsterDefense * percentInt) / percentInt;
        return skillMonsterDamage - (skillMonsterDamage * monsterDefenseResult);
    }

    public float GetCritical()
    {
        return (SharedPlayerStats.GetAttackCritical() - 1) * 0.1f;
    }

    public void SettingPlayerMaxHP()
    {
        CurrentMaxHpSet();
        playerUpgradeMaxHP = playerAttribute.MaxHp + ((SharedPlayerStats.GetHp() - 1) * increaseUpgradeHp);
    }

    public void TakeDamage(BigInteger damage)
    {
        var hp = SharedPlayerStats.GetHp() - 1;
        if (prevUpgradeHp != hp)
        {
            prevUpgradeHp = hp;

            CurrentMaxHpSet();
        }
        //Debug.Log(playerCurrentHp);
        playerCurrentHp -= damage;
        if(playerCurrentHp <= 0) 
        {
            CurrentMaxHpSet();
            Debug.Log("PlayerDie");
        }

        var normal = GetNormalizedHealth();
        UIManager.Instance.SetHpGauge(normal);
    }

    public void ResetHp()
    {
        playerCurrentHp = playerMaxHp;

        var normal = GetNormalizedHealth();
        UIManager.Instance.SetHpGauge(normal);
    }

    public void CurrentMaxHpSet()
    {
        playerMaxHp = state.TotalState.Health;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeStats : MonoBehaviour
{
    // UI매니저에 옮겨야함
    public void PowerUpgrade()
    {
        Debug.Log("PowerUpgrade");
        SharedPlayerStats.IncreasePlayerPower();
    }

    public void PowerBoostUpgrade()
    {
        Debug.Log("PowerBoostUpgrade");
        SharedPlayerStats.IncreasePlayerPowerBoost();
    }

    public void AttackSpeedUpgrade()
    {
        Debug.Log("AttackSpeedUpgrade");
        SharedPlayerStats.IncreaseAttackSpeed();
    }

    public void CritcalUpgrade()
    {
        Debug.Log("CritcalUpgrade");
        SharedPlayerStats.IncreaseAttackCritical();
    }

    public void CrticalPowerUpgrade()
    {
        Debug.Log("CrticalPowerUpgrade");
        SharedPlayerStats.IncreaseAttackCriticalPower();
    }

    public void MonsterDamageUpgrade()
    {
        Debug.Log("MonsterDamageUpgrade");
        SharedPlayerStats.IncreaseMonsterDamagePower();
    }
 
    public void HpUpgrade()
    {
        Debug.Log("HpUpgrade");
        SharedPlayerStats.IncreaseHp();
    }

    public void HealthHpUpragde()
    {
        Debug.Log("HealthHpUpragde");
        SharedPlayerStats.IncreaseHealing();
    }
    public void AllStats()
    {
        Debug.Log($"power :{SharedPlayerStats.GetPlayerPower()} \t powerBoost :{SharedPlayerStats.GetPlayerPowerBoost()}\n" +
            $"attackSpeed :{SharedPlayerStats.GetAttackSpeed()} \t critical :{SharedPlayerStats.GetAttackCritical()} \n" +
            $"critcalPower : {SharedPlayerStats.GetAttackCriticlaPower()} \t monsterDamage :{SharedPlayerStats.GetMonsterDamagePower()}\n" +
            $"Hp : {SharedPlayerStats.GetHp()} \t healing:{SharedPlayerStats.GetHealing()}");
    }
}

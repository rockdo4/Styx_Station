using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Numerics;
using UnityEditor.Animations;

public class PlayerUpgradeStats : MonoBehaviour
{

    // test code

    private float nowTime;

    public TextMeshProUGUI moneyText1;
    //private BigInteger money1 = new BigInteger();
    private bool isMoney1Click;
    public float money1CkickTime = 0.2f;

    public TextMeshProUGUI moneyText2;
    //private BigInteger money2 = new BigInteger();
    private bool isMoney2Click;
    public float money2CkickTime = 0.2f;

    public TextMeshProUGUI moneyText3;
    //private BigInteger money3 = new BigInteger();
    private bool isMoney3Click;
    public float money3CkickTime = 0.2f;

    private readonly BigInteger percentage = new BigInteger(100);
    public int money1GainRate;
    private BigInteger test = new BigInteger(500000000); // test code               

    //[Tooltip("a")]
    public RuntimeAnimatorController playerAnimator;

    private void Awake()
    {
        UnitConverter.InitUnitConverter();
        IncreaseMoney1(0);
        IncreaseMoney2(0);
        IncreaseMoney3(0);
    }

    public void PowerUpgrade()
    {

        SharedPlayerStats.IncreasePlayerPower();
    }

    public void PowerBoostUpgrade()
    {

        SharedPlayerStats.IncreasePlayerPowerBoost();
    }

    public void AttackSpeedUpgrade()
    {

        SharedPlayerStats.IncreasePlayerAttackSpeed();
    }

    public void CritcalUpgrade()
    {

        SharedPlayerStats.IncreaseAttackCritical();
    }

    public void CrticalPowerUpgrade()
    {

        SharedPlayerStats.IncreaseAttackCriticalPower();
    }

    public void MonsterDamageUpgrade()
    {

        SharedPlayerStats.IncreaseMonsterDamagePower();
    }

    public void HpUpgrade()
    {

        SharedPlayerStats.IncreaseHp();
    }

    public void HealthHpUpragde()
    {

        SharedPlayerStats.IncreaseHealing();
    }
    public void AllStats()
    {
        Debug.Log($"power :{SharedPlayerStats.GetPlayerPower()} \t powerBoost :{SharedPlayerStats.GetPlayerPowerBoost()}\n" +
            $"attackSpeed :{SharedPlayerStats.GetPlayerAttackSpeed()} \t critical :{SharedPlayerStats.GetAttackCritical()} \n" +
            $"critcalPower : {SharedPlayerStats.GetAttackCriticlaPower()} \t monsterDamage :{SharedPlayerStats.GetMonsterDamagePower()}\n" +
            $"Hp : {SharedPlayerStats.GetHp()} \t healing:{SharedPlayerStats.GetHealing()}");
    }

    public void IncreaseMoney1(BigInteger money)
    {
        SharedPlayerStats.money1 += money * (money * money1GainRate / percentage);
        moneyText1.text = UnitConverter.OutString(SharedPlayerStats.money1);
    }
    public void OutPutMoney1()
    {
        moneyText1.text = UnitConverter.OutString(SharedPlayerStats.money1);
    }
    public void IncreaseMoney2(BigInteger money)
    {
        SharedPlayerStats.money2 += money * (money * money1GainRate / percentage);
        moneyText2.text = UnitConverter.OutString(SharedPlayerStats.money2);
    }
    public void OutPutMoney2()
    {
        moneyText2.text = UnitConverter.OutString(SharedPlayerStats.money2);
    }
    public void IncreaseMoney3(BigInteger money)
    {
        SharedPlayerStats.money3 += money * (money * money1GainRate / percentage);
        moneyText3.text = UnitConverter.OutString(SharedPlayerStats.money3);
    }
    public void OutPutMoney3()
    {
        moneyText3.text = UnitConverter.OutString(SharedPlayerStats.money3);
    }

    public void UPMoney1TestButton()
    {
        isMoney1Click = true;
    }
    public void UpMoney1TestButtonUp()
    {
        isMoney1Click = false;
    }

    public void UPMoney2TestButton()
    {
        isMoney2Click = true;
    }
    public void UpMoney2TestButtonUp()
    {
        isMoney2Click = false;
    }

    public void UPMoney3TestButton()
    {
        isMoney3Click = true;
    }
    public void UpMoney3TestButtonUp()
    {
        isMoney3Click = false;
    }

    private void Update()
    {
        if (isMoney1Click)
        {
            if (Time.time > nowTime + money1CkickTime)
            {
                nowTime = Time.time;
                IncreaseMoney1(test);
            }
        }
        if (isMoney2Click)
        {
            if (Time.time > nowTime + money2CkickTime)
            {
                nowTime = Time.time;
                IncreaseMoney2(test);
            }
        }
        if (isMoney3Click)
        {
            if (Time.time > nowTime + money3CkickTime)
            {
                nowTime = Time.time;
                IncreaseMoney3(test);
            }
        }

    }
}



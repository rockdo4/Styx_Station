using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Numerics;

public class PlayerUpgradeStats : MonoBehaviour
{

    // test code

    private float nowTime;
    private int Money1=0;
    private int Money2=0;
    private int Money3=0;

    public TextMeshProUGUI moneyText1;
    private BigInteger money1 = new BigInteger();
    private bool isMoney1Click;

    public float money1CkickTime = 0.2f;
    public TextMeshProUGUI moneyText2;
    private int money2;
    public TextMeshProUGUI moneyText3;
    private int money3;

    public UnityEvent<int,float> onClickAttackable;


    private void Awake()
    {
        //UnitConverter.InitUnitConverter();
    }
    private void Start()
    {
        PrintMoney1();
    }

    // UI매니저에 옮겨야함
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
       
        SharedPlayerStats.IncreaseAttackSpeed();
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
            $"attackSpeed :{SharedPlayerStats.GetAttackSpeed()} \t critical :{SharedPlayerStats.GetAttackCritical()} \n" +
            $"critcalPower : {SharedPlayerStats.GetAttackCriticlaPower()} \t monsterDamage :{SharedPlayerStats.GetMonsterDamagePower()}\n" +
            $"Hp : {SharedPlayerStats.GetHp()} \t healing:{SharedPlayerStats.GetHealing()}");
    }
    public float n;
    public void IncreaseMoney1(int money)
    {
        // n강화 확률이라 어디서 할지 몰라서 일단 이렇게 해둠

        money1 += money + (int)(money * (n / 100));
        PrintMoney1();
    }

    private void PrintMoney1()
    {

        //if (money1 < UnitConverter.A1)
        //{
        //    moneyText1.text = $"{money1}";
        //}
        //else if (money1 > UnitConverter.A1 && money1 < UnitConverter.B1)
        //{
        //    var temp = money1;
        //    var result = temp / UnitConverter.A1;
        //    var result1 = temp % UnitConverter.A1 / 10;
        //    moneyText1.text = $"{result}.{result1}A";
        //}
        //else if (money1 > UnitConverter.B1 && money1 < UnitConverter.C1)
        //{
        //    var temp = money1;
        //    var result = temp / UnitConverter.B1;
        //    var result1 = temp % UnitConverter.B1 / (UnitConverter.A1 * 10);
        //    moneyText1.text = $"{result}.{result1}B";
        //}
    }

    public int test;
    public void UPMoney1TestButton()
    {
        isMoney1Click = true;
    }
    public void UpMoney1TestButtonUp()
    {
        isMoney1Click = false;
    }

    private void Update()
    {
        if(isMoney1Click)
        {
            if(Time.time > nowTime + money1CkickTime)
            {
                nowTime = Time.time;
                IncreaseMoney1(test);
            }
        }
    }
}

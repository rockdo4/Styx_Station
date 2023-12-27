using System;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class MoneyTest : MonoBehaviour
{
    private bool isUpgradeMoney1;
    private bool isUpgradeMoney2;
    private bool isUpgradeMoney3;
    private bool isUpgradeMoney4;
    private BigInteger test = new BigInteger(0);
    private float nowTime;
    public float clickTime = 0.05f;
    
    public List<TextMeshProUGUI> ll = new List<TextMeshProUGUI>();

    private void Awake()
    {
        UnitConverter.InitUnitConverter();
    }
    private void Start()
    {
        ll[0].text = $"{UnitConverter.OutString(CurrencyManager.money1)}";
        ll[1].text = $"{UnitConverter.OutString(CurrencyManager.money2)}";
        ll[2].text = $"{UnitConverter.OutString(CurrencyManager.money3)}";
        ll[3].text = $"{UnitConverter.OutString(CurrencyManager.itemAsh)}";
    }

    public void IncreaseTestMoney1() //test code
    {
        isUpgradeMoney1 = !isUpgradeMoney1;
        isUpgradeMoney2 = false;
        isUpgradeMoney3 = false;
        isUpgradeMoney4 = false;
    }
    public void IncreaseTestMoney2() //test code
    {
        isUpgradeMoney2 = !isUpgradeMoney2;
        isUpgradeMoney1 = false;
        isUpgradeMoney3 = false;
        isUpgradeMoney4 = false;
    }
    public void IncreaseTestMoney3() //test code
    {
        isUpgradeMoney3 = !isUpgradeMoney3;
        isUpgradeMoney1 = false;
        isUpgradeMoney2 = false;
        isUpgradeMoney4 = false;
    }
    public void IncreaseTestMoney4()
    {
        isUpgradeMoney4 = !isUpgradeMoney4;
        isUpgradeMoney1 = false;
        isUpgradeMoney2 = false;
        isUpgradeMoney3 = false;
    }
    private void Update()
    {
        CheckAndExecute(isUpgradeMoney1, () => CurrencyManager.IncreaseMoney1(test), 0); 
        CheckAndExecute(isUpgradeMoney2, () => CurrencyManager.IncreaseMoney2(test), 1); 
        CheckAndExecute(isUpgradeMoney3, () => CurrencyManager.IncreaseMoney3(test), 2);
        CheckAndExecute(isUpgradeMoney4, () => CurrencyManager.IncreaseMoney4(test), 3);
    }
    private void CheckAndExecute(bool condition, Action action, int statsIndex)
    {
        if (condition && nowTime + clickTime < Time.time)
        {
            clickTime -= 0.1f;
            if (clickTime <= 0f)
            {
                clickTime = 0.05f;
            }
            nowTime = Time.time;
            action.Invoke();
        }
        ll[0].text = $"{UnitConverter.OutString(CurrencyManager.money1)}";
        ll[1].text = $"{UnitConverter.OutString(CurrencyManager.money2)}";
        ll[2].text = $"{UnitConverter.OutString(CurrencyManager.money3)}";
        ll[3].text = $"{UnitConverter.OutString(CurrencyManager.itemAsh)}";
    }

    public void PrintText()
    {
        ll[0].text = $"{UnitConverter.OutString(CurrencyManager.money1)}";
        ll[1].text = $"{UnitConverter.OutString(CurrencyManager.money2)}";
        ll[2].text = $"{UnitConverter.OutString(CurrencyManager.money3)}";
        ll[3].text = $"{UnitConverter.OutString(CurrencyManager.itemAsh)}";
    }
}

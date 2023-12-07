using System;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerStatsUiData : MonoBehaviour
{
    public Button button;
    public int index;
    [HideInInspector] public bool onClickButton;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI upgradeLevelText;
    public TextMeshProUGUI upgradeLevelUPPriceText;
    private Dictionary<int, Func<int>> currentLevelTextFunc;
    private Dictionary<int, Action> upgradePlayerStatsAction;
    private Dictionary<int, Func<string>> upgradeLevel;
    private Dictionary<int, Func<string>> upgradePrice;

    private float time;
    private float clickTime = 0.5f;// 추후 변경예정
    private float decreaseTime;

    private BigInteger stats=new BigInteger();

    private void Start()
    {
        SettingAction();
        SettingPlayerStatsUIBUtton();
        SetTextLevelAndPrice(index);

        clickTime = PlayerStatsUIManager.Instance.clickTime;
        decreaseTime = PlayerStatsUIManager.Instance.decreaseClickTime;
    }
    private void PlayerUpgrade(PointerEventData data, int newIndex)
    {
        onClickButton = !onClickButton;
        if(!onClickButton)
        {
            clickTime = PlayerStatsUIManager.Instance.clickTime;
        }
    }

    private void Update()
    {
        if (onClickButton)
        {
            if (clickTime + time < Time.time)
            {
                time = Time.time;
                clickTime -= decreaseTime;
                if(clickTime <= PlayerStatsUIManager.Instance.minClickTime)
                {
                    clickTime = PlayerStatsUIManager.Instance.minClickTime;
                }
                if (upgradePlayerStatsAction.TryGetValue(index, out var action))
                {
                    action.Invoke();
                }
                SetTextLevelAndPrice(index);
                Debug.Log($"time :{clickTime}");
            }
        }
    }

    private void SettingPlayerStatsUIBUtton()
    {

        var eventTrigger = button.GetComponent<EventTrigger>();
        if (eventTrigger != null)
        {
            var newIndex = index;
            var pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.PointerDown;
            pointerDown.callback.AddListener((data) => { PlayerUpgrade((PointerEventData)data, newIndex); });
            eventTrigger.triggers.Add(pointerDown);

            var pointerUp = new EventTrigger.Entry();
            pointerUp.eventID = EventTriggerType.PointerUp;
            pointerUp.callback.AddListener((data) => { PlayerUpgrade((PointerEventData)data, newIndex); });
            eventTrigger.triggers.Add(pointerUp);
        }
    }

    private void SettingAction()
    {
        upgradePlayerStatsAction = new Dictionary<int, Action>
        {
            {0,SharedPlayerStats.IncreasePlayerPower },
            {1,SharedPlayerStats.IncreasePlayerPowerBoost },
            {2,SharedPlayerStats.IncreasePlayerAttackSpeed },
            {3,SharedPlayerStats.IncreaseAttackCritical },
            {4,SharedPlayerStats.IncreaseAttackCriticalPower },
            {5,SharedPlayerStats.IncreaseMonsterDamagePower },
            {6,SharedPlayerStats.IncreaseHp },
            {7,SharedPlayerStats.IncreaseHealing },
        };
        upgradeLevel = new Dictionary<int, Func<string>>
        {
            { 0, () => UnitConverter.OutString(SharedPlayerStats.GetPlayerPower()) },
            { 1, () =>
            {
                int boost = SharedPlayerStats.GetPlayerPowerBoost();
                float f= boost/10f;
                return $"{f:F1}%";
            }},
            { 2 ,()=> UnitConverter.OutString(SharedPlayerStats.GetPlayerAttackSpeed()) },
            { 3 ,()=> UnitConverter.OutString(SharedPlayerStats.GetAttackCritical()) },
            { 4 ,()=> UnitConverter.OutString(SharedPlayerStats.GetAttackCriticlaPower()) },
            { 5 ,()=> UnitConverter.OutString(SharedPlayerStats.GetMonsterDamagePower()) },
            { 6 ,()=> UnitConverter.OutString(SharedPlayerStats.GetHp()) },
            { 7 ,()=> UnitConverter.OutString(SharedPlayerStats.GetHealing()) },
        };
        upgradePrice = new Dictionary<int, Func<string>>
        {
            { 0, () => UnitConverter.OutString(CurrencyManager.playerPowerPrice) },
            { 1, () => UnitConverter.OutString(CurrencyManager.playerPowerBoostPrice) },
            { 2 ,()=> UnitConverter.OutString(CurrencyManager.playerAttackSpeedPrice) },
            { 3 ,()=> UnitConverter.OutString(CurrencyManager.criticalPrice)},
            { 4 ,()=> UnitConverter.OutString(CurrencyManager.criticalPowerPrice)},
            { 5 ,()=> UnitConverter.OutString(CurrencyManager.monsterDamagerPrice) },
            { 6 ,()=> UnitConverter.OutString(CurrencyManager.maxHpPrice) },
            { 7 ,()=> UnitConverter.OutString(CurrencyManager.healingPrice) },
        };
    }
    private void SetTextLevelAndPrice(int index)
    {
        if (upgradeLevel.TryGetValue(index, out Func<string> action1))
        {
            upgradeLevelText.text = $"{action1()}";
        }
        if (upgradePrice.TryGetValue(index, out Func<string> action2))
        {
            upgradeLevelUPPriceText.text = $"+ {action2()}";
        }
    }

}


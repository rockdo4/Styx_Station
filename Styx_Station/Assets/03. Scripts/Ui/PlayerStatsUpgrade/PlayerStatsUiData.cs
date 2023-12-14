using System;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerStatsUiData : MonoBehaviour
{
    private bool isSet;
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
    private Dictionary<int, Action> buttonCheckingAction;

    private float time;
    private float clickTime = 0.5f;// 추후 변경예정
    private float decreaseTime;

    private BigInteger stats = new BigInteger();

    private void Start()
    {
        if (!isSet)
        {
            SettingAction();
            SettingPlayerStatsUIBUtton();
            isSet = true;
        }
        SetTextLevelAndPrice(index);

        clickTime = PlayerStatsUIManager.Instance.clickTime;
        decreaseTime = PlayerStatsUIManager.Instance.decreaseClickTime;
    }
    private void PlayerUpgrade(PointerEventData data, int newIndex)
    {
        onClickButton = !onClickButton;
        if (!onClickButton)
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
                if (clickTime <= PlayerStatsUIManager.Instance.minClickTime)
                {
                    clickTime = PlayerStatsUIManager.Instance.minClickTime;
                }
                if (upgradePlayerStatsAction.TryGetValue(index, out var action))
                {
                    action.Invoke();
                   
                }
                if (button.interactable)
                {
                    UIManager.Instance.questSystemUi.UpgradeQuestSet(index);
                }
                SetTextLevelAndPrice(index);
            }
        }
    }
    private void LateUpdate()
    {
        ButtonCheckingAction(index);
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
        if (upgradePlayerStatsAction == null)
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
        }
        if(upgradeLevel ==null)
        {
            upgradeLevel = new Dictionary<int, Func<string>>
        {
            { 0, () =>
            {
                if(PlayerStatsUIManager.Instance.playerStats != null)
                {
                    return UnitConverter.OutString(PlayerStatsUIManager.Instance.playerStats.GetPlayerPowerByNonInventory());
                }
                else  return UnitConverter.OutString(SharedPlayerStats.GetPlayerPower()-1);
            }
            },
            { 1, () =>
            {
                int boost = SharedPlayerStats.GetPlayerPowerBoost()-1;
                float f= boost/10f;
                return $"{f:F1}%";
            }},
            { 2 ,()=> UnitConverter.OutString(SharedPlayerStats.GetPlayerAttackSpeed()-1) },
            { 3 ,()=>
            {
                int critcial =SharedPlayerStats.GetAttackCritical()-1;
                float f = critcial/10f;
                return $"{f:F1}%";
            }},
            { 4 ,
            ()=>
            {
                int critcalPower= SharedPlayerStats.GetAttackCriticlaPower()-1;
                float f = 150+(critcalPower/100f);
                return $"{f:F2}%";
            }},
            { 5 ,()=>
            {
                int monster =SharedPlayerStats.GetMonsterDamagePower() - 1;
                float f = monster/10f;
                return $"{f:f1}%";
            }},
            { 6 ,()=>
            {
                if(PlayerStatsUIManager.Instance.playerStats != null)
                {
                    PlayerStatsUIManager.Instance.playerStats.SettingPlayerMaxHP();
                    return UnitConverter.OutString(PlayerStatsUIManager.Instance.playerStats.playerMaxHp-1);
                }
                else return UnitConverter.OutString(SharedPlayerStats.GetHp() - 1);
            }},
            { 7 ,()=>UnitConverter.OutString(10 + SharedPlayerStats.GetHealing() - 1)},
        };
        }
        if(upgradePrice ==null)
        {
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
            currentLevelTextFunc = new Dictionary<int, Func<int>>
        {
             { 0, () => SharedPlayerStats.GetPlayerPower() },
             { 1, () => SharedPlayerStats.GetPlayerPowerBoost() },
             { 2, () => SharedPlayerStats.GetPlayerAttackSpeed() },
             { 3, () => SharedPlayerStats.GetAttackCritical() },
             { 4, () => SharedPlayerStats.GetAttackCriticlaPower() },
             { 5, () => SharedPlayerStats.GetMonsterDamagePower() },
             { 6, () => SharedPlayerStats.GetHp() },
             { 7, () => SharedPlayerStats.GetHealing() },
        };
        }
        if(buttonCheckingAction ==null)
        {
                buttonCheckingAction = new Dictionary<int, Action>
            {
                {0,()=>PlayerPowerButtonCheck() },
                {1,()=>PlayerPowerBoostButtonCheck()},
                {2,()=>PlayerAttackSpeedButton() },
                {3,()=>PlayerCriticalButton() },
                {4,()=>PlayerCriticalPowerButton() },
                {5,()=>MonsterDamageButton()},
                {6,()=>PlayerMaxHpButton()},
                {7,()=>PlayerHealingButton()},
            };
        }
    }
    public void SetTextLevelAndPrice(int index)
    {
        if (!isSet)
        {
            SettingAction();
            SettingPlayerStatsUIBUtton();
            isSet = true;
        }
        if (upgradeLevel.TryGetValue(index, out Func<string> action1))
        {
            upgradeLevelText.text = $"{action1()}";
        }
        if (upgradePrice.TryGetValue(index, out Func<string> action2))
        {
            upgradeLevelUPPriceText.text = $"+ {action2()}";
        }
        if (currentLevelTextFunc.TryGetValue(index, out Func<int> action3))
        {
            currentLevelText.text = $"{action3()}";
        }
    }
    public void ButtonCheckingAction(int index)
    {
        if (buttonCheckingAction == null)
        {
            SettingAction();
        }
        if (buttonCheckingAction.TryGetValue(index, out Action action))
        {
            action();
        }
    }
    private void PlayerPowerButtonCheck()
    {
        if (CurrencyManager.money1 > CurrencyManager.playerPowerPrice)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
    private void PlayerPowerBoostButtonCheck()
    {
        if(SharedPlayerStats.IsPlayerPowerBoostAmplifiable && !SharedPlayerStats.IsPlayerPowerBoostMax)
        {
            if (CurrencyManager.money2 < CurrencyManager.playerPowerBoostPrice)
            {
                button.interactable = false;
            }
            else if (CurrencyManager.money2 > CurrencyManager.playerPowerBoostPrice )
            {
                button.interactable = true;
            }
        }
        else if(SharedPlayerStats.IsPlayerPowerBoostMax)
        {
            button.interactable = false;
            ButtonTextMax();
        }
        else
        {
            button.interactable = false;
        }
    }
    private void PlayerAttackSpeedButton()
    {
        if (CurrencyManager.money1 > CurrencyManager.playerAttackSpeedPrice && !SharedPlayerStats.IsAttackSpeedMax)
        {
            button.interactable = true;
        }
        else if(SharedPlayerStats.IsAttackSpeedMax)
        {
            button.interactable = false;
            ButtonTextMax(); 
        }
        else
        {
            button.interactable = false;
        }
    }
    private void PlayerCriticalButton()
    {
        if(CurrencyManager.money1 > CurrencyManager.criticalPrice && !SharedPlayerStats.IsAttackCriticalMax)
        {
            button.interactable = true;
        }
        else if(SharedPlayerStats.IsAttackCriticalMax)
        {
            button.interactable = false;
            ButtonTextMax();
        }
        else
        {
            button.interactable = false;
        }
    }
    private void PlayerCriticalPowerButton()
    {
        if (CurrencyManager.money2 > CurrencyManager.criticalPowerPrice)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
    private void MonsterDamageButton()
    {
        if (CurrencyManager.money2 > CurrencyManager.monsterDamagerPrice && !SharedPlayerStats.IsMonsterDamagePowerMax)
        {
            button.interactable = true;
        }
        else if (SharedPlayerStats.IsMonsterDamagePowerMax)
        {
            button.interactable = false;
            ButtonTextMax();
        }
        else
        {
            button.interactable = false;
        }
    }
    private  void PlayerMaxHpButton()
    {
        if (CurrencyManager.money1 > CurrencyManager.maxHpPrice)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    private void PlayerHealingButton()
    {
        if (CurrencyManager.money1 > CurrencyManager.healingPrice)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }    

    private void ButtonTextMax()
    {
        currentLevelText.text = "Max";
        var getText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (getText != null)
        {
            getText.text = "Max";
        }
    }
}


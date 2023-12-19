using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class StateSystem : MonoBehaviour
{
    public class State
    {
        public float Attack = 0f;
        public float Health = 0f;
        public float AttackSpeed = 0f;
        public float HealHealth = 0f;
        public float AttackPer = 0f;
        public float Evade = 0f;
        public float DamageReduction = 0f;
        public float BloodSucking = 0f;
        public float CoinAcquire = 0f;
        public float NormalDamage = 0f;
        public float SkillDamage = 0f;
        public float BossDamage = 0f;
    }

    public State EquipState { get; private set; }
    public State AcquireState { get; private set; }

    public State TotalState { get; private set; }

    private Inventory item;
    private PetInventory pet;

    public void Setting()
    {
        if(EquipState == null)
            EquipState = new State();

        if(AcquireState == null)
            AcquireState = new State();

        if(TotalState == null)
            TotalState = new State();

        item = InventorySystem.Instance.inventory;
        pet = InventorySystem.Instance.petInventory;
    }

    private void ResetState(State state)
    {
        state.Attack = 0f;
        state.Health = 0f;
        state.AttackSpeed = 0f;
        state.HealHealth = 0f;
        state.AttackPer = 0f;
        state.Evade = 0f;
        state.DamageReduction = 0f;
        state.BloodSucking = 0f;
        state.CoinAcquire = 0f;
        state.NormalDamage = 0f;
        state.SkillDamage = 0f;
        state.BossDamage = 0f;
    }
    private void TotalStateSet()
    {
        TotalState.Attack = EquipState.Attack + AcquireState.Attack;
        TotalState.Health = EquipState.Health + AcquireState.Health;
        TotalState.AttackSpeed = EquipState.AttackSpeed + AcquireState.AttackSpeed;
        TotalState.HealHealth = EquipState.HealHealth + AcquireState.HealHealth;
        TotalState.AttackPer = EquipState.AttackPer + AcquireState.AttackPer;
        TotalState.Evade = EquipState.Evade + AcquireState.Evade;
        TotalState.DamageReduction = EquipState.DamageReduction + AcquireState.DamageReduction;
        TotalState.BloodSucking = EquipState.BloodSucking + AcquireState.BloodSucking;
        TotalState.CoinAcquire = EquipState.CoinAcquire + AcquireState.CoinAcquire;
        TotalState.NormalDamage = EquipState.NormalDamage + AcquireState.NormalDamage;
        TotalState.SkillDamage = EquipState.SkillDamage + AcquireState.SkillDamage;
        TotalState.BossDamage = EquipState.BossDamage + AcquireState.BossDamage;
    }
    public void EquipUpdate()
    {
        ResetState(EquipState);

        for(int i = 0; i<item.GetEquipItemsLength();++i)
        {
            var equips = item.GetEquipItem(i);

            if(equips == null)
                continue;

            if (equips.item.options.Count <= 0)
                continue;

            EquipOptions(equips);
        }

        TotalStateSet();
    }

    public void AcquireUpdate()
    {
        ResetState(AcquireState);

        foreach(var weapon in item.weapons)
        {
            if(!weapon.acquire)
                continue;

            AcquireOptions(weapon);
        }

        TotalStateSet();
    }
    private void AcquireOptions(Inventory.InventoryItem acquireItem)
    {
        foreach (var option in acquireItem.item.addOptions)
        {
            switch (option.option)
            {
                case AddOptionString.AttackPer:
                    EquipState.AttackPer += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.Evade:
                    EquipState.Evade += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.DamageReduction:
                    EquipState.DamageReduction += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.Bloodsucking:
                    EquipState.BloodSucking += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.CoinAcquire:
                    EquipState.CoinAcquire += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.SkillDamage:
                    EquipState.SkillDamage += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.BossDamage:
                    EquipState.BossDamage += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;
            }
        }
    }
    private void EquipOptions(Inventory.InventoryItem equipItem)
    {
        float weight = 0f;
        switch (equipItem.item.enchant)
        {
            case Enchant.Old:
                weight = 0.1f;
                break;
            case Enchant.EntryLevel:
                weight = 0.2f;
                break;
            case Enchant.Creation:
                weight = 0.3f;
                break;
            case Enchant.Masters:
                weight = 0.4f;
                break;
            case Enchant.MasterPiece:
                weight = 0.5f;
                break;
        }
        foreach (var option in equipItem.item.options)
        {
            switch (option.option)
            {
                case ItemOptionString.Attack:
                    EquipState.Attack += option.value + (option.upgradeValue * equipItem.upgradeLev) + ((option.value + (option.upgradeValue * equipItem.upgradeLev)) * weight);
                    break;

                case ItemOptionString.Health:
                    EquipState.Health += option.value + (option.upgradeValue * equipItem.upgradeLev) + ((option.value + (option.upgradeValue * equipItem.upgradeLev)) * weight);
                    break;
            }
        }

        if (equipItem.item.type == ItemType.Weapon || equipItem.item.type == ItemType.Armor)
            return;

        if (equipItem.item.addOptions != null)
        {
            if (equipItem.item.addOptions.Count > 0)
            {
                weight = 0f;
                switch (equipItem.item.enchant)
                {
                    case Enchant.Old:
                        weight = 1f;
                        break;
                    case Enchant.EntryLevel:
                        weight = 1.2f;
                        break;
                    case Enchant.Creation:
                        weight = 1.4f;
                        break;
                    case Enchant.Masters:
                        weight = 1.6f;
                        break;
                    case Enchant.MasterPiece:
                        weight = 2.0f;
                        break;
                }
                foreach (var option in equipItem.item.addOptions)
                {
                    switch (option.option)
                    {
                        case AddOptionString.AttackPer:
                            EquipState.AttackPer += option.value * weight;
                            break;

                        case AddOptionString.Evade:
                            EquipState.Evade += option.value * weight;
                            break;

                        case AddOptionString.DamageReduction:
                            EquipState.DamageReduction += option.value * weight;
                            break;

                        case AddOptionString.Bloodsucking:
                            EquipState.BloodSucking += option.value * weight;
                            break;

                        case AddOptionString.CoinAcquire:
                            EquipState.CoinAcquire += option.value * weight;
                            break;

                        case AddOptionString.SkillDamage:
                            EquipState.SkillDamage += option.value * weight;
                            break;

                        case AddOptionString.BossDamage:
                            EquipState.BossDamage += option.value * weight;
                            break;
                    }
                }
            }
        }
    }
}

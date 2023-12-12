using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Item")]
public class Item : ScriptableObject, IItemEquiptable, IItemDequiptable
{
    public string itemName;

    public ItemType type;

    public Tier tier;

    public Enchant enchant;

    public List<int> itemLevUpNum = new List<int>();

    [System.Serializable]
    public struct Option
    {
        public ItemOptionString option;
        public float value;
        public float upgradeValue;
    }

    public List<Option> options = new List<Option>();

    [System.Serializable]
    public struct AddOption
    {
        public AddOptionString option;
        public float value;
        public float upgradeValue;
    }

    public List<AddOption> addOptions = new List<AddOption>();

    public Sprite itemIcon;

    public void AcquireValue()
    {
        if (type == ItemType.Ring || type == ItemType.Symbol)
            return;

        if (addOptions != null)
        {
            if (addOptions.Count > 0)
            {
                var inventory = InventorySystem.Instance.inventory;
                if (inventory == null)
                    return;

                foreach (var option in addOptions)
                {
                    switch (option.option)
                    {
                        case AddOptionString.AttackPer:
                            inventory.a_AttackPer += option.value;
                            break;

                        case AddOptionString.Evade:
                            inventory.a_Evade += option.value;
                            break;

                        case AddOptionString.DamageReduction:
                            inventory.a_DamageReduction += option.value;
                            break;

                        case AddOptionString.Bloodsucking:
                            inventory.a_BloodSucking += option.value;
                            break;

                        case AddOptionString.CoinAcquire:
                            inventory.a_CoinAcquire += option.value;
                            break;

                        case AddOptionString.SkillDamage:
                            inventory.a_SkillDamage += option.value;
                            break;

                        case AddOptionString.BossDamage:
                            inventory.a_BossDamage += option.value;
                            break;
                    }
                }
            }
        }
    }

    public void AddOptions(AddOptionString option, float value)
    {
        if (addOptions == null)
            return;

        AddOption fOption = new AddOption();
        fOption.option = option;
        fOption.value = value;

        addOptions.Add(fOption);
    }

    public void OnEquip(Inventory inventory, int upgadeLev)
    {
        if (options != null)
        {
            if (options.Count > 0)
            {
                float weight = 0f;
                switch(enchant)
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
                foreach (var option in options)
                {
                    switch (option.option)
                    {
                        case ItemOptionString.Attack:
                            inventory.t_Attack += option.value + (option.upgradeValue * upgadeLev) + ((option.value + (option.upgradeValue * upgadeLev)) * weight);
                            break;

                        case ItemOptionString.Health:
                            inventory.t_Health += option.value + (option.upgradeValue * upgadeLev) + ((option.value + (option.upgradeValue * upgadeLev)) * weight);
                            break;
                    }
                }
            }
        }

        if (type == ItemType.Weapon || type == ItemType.Armor)
            return;

        if (addOptions != null)
        {
            if (addOptions.Count > 0)
            {
                float weight = 0f;
                switch (enchant)
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
                foreach (var option in addOptions)
                {
                    switch (option.option)
                    {
                        case AddOptionString.AttackPer:
                            inventory.t_AttackPer += option.value * weight;
                            break;

                        case AddOptionString.Evade:
                            inventory.t_Evade += option.value * weight;
                            break;

                        case AddOptionString.DamageReduction:
                            inventory.t_DamageReduction += option.value * weight;
                            break;

                        case AddOptionString.Bloodsucking:
                            inventory.t_BloodSucking += option.value * weight;
                            break;

                        case AddOptionString.CoinAcquire:
                            inventory.t_CoinAcquire += option.value * weight;
                            break;

                        case AddOptionString.SkillDamage:
                            inventory.t_SkillDamage += option.value * weight;
                            break;

                        case AddOptionString.BossDamage:
                            inventory.t_BossDamage += option.value * weight;
                            break;
                    }
                }
            }
        }
    }

    public void OnDequip(Inventory inventory, int upgadeLev)
    {
        if (options != null)
        {
            if (options.Count > 0)
            {
                foreach (var option in options)
                {
                    switch (option.option)
                    {
                        case ItemOptionString.Attack:
                            inventory.t_Attack -= option.value + (option.upgradeValue * upgadeLev);
                            break;

                        case ItemOptionString.Health:
                            inventory.t_Health -= option.value + (option.upgradeValue * upgadeLev);
                            break;
                    }
                }
            }
        }

        if (type == ItemType.Weapon || type == ItemType.Armor)
            return;

        if (addOptions != null)
        {
            if (addOptions.Count > 0)
            {
                float weight = 0f;
                switch (enchant)
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
                foreach (var option in addOptions)
                {
                    switch (option.option)
                    { 
                        case AddOptionString.AttackPer:
                            inventory.t_AttackPer -= option.value * weight;
                            break;

                        case AddOptionString.Evade:
                            inventory.t_Evade -= option.value * weight;
                            break;

                        case AddOptionString.DamageReduction:
                            inventory.t_DamageReduction -= option.value * weight;
                            break;

                        case AddOptionString.Bloodsucking:
                            inventory.t_BloodSucking -= option.value * weight;
                            break;

                        case AddOptionString.CoinAcquire:
                            inventory.t_CoinAcquire -= option.value * weight;
                            break;

                        case AddOptionString.SkillDamage:
                            inventory.t_SkillDamage -= option.value * weight;
                            break;

                        case AddOptionString.BossDamage:
                            inventory.t_BossDamage -= option.value * weight;
                            break;
                    }
                }
            }
        }
    }
}

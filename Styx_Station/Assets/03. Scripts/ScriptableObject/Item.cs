using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Item")]
public class Item : ScriptableObject, IItemEquiptable, IItemDequiptable
{
    [Tooltip("아이템 이름 - Inspector 창에서만 적용")]
    public string itemName;

    [Tooltip("장비 종류")]
    public ItemType type;

    [Tooltip("아이템 등급")]
    public ItemTier tier;

    [System.Serializable]
    public struct Option
    {
        [Tooltip("기본 옵션")]
        public ItemOptionString option;
        [Tooltip("기본 스탯")]
        public float value;
        [Tooltip("강화 시 스탯 추가 증가량")]
        public float upgradeValue;
    }

    [Tooltip("아이템 기본 옵션")]
    public List<Option> options = new List<Option>();

    [System.Serializable]
    public struct AddOption
    {
        [Tooltip("추가 옵션")]
        public AddOptionString option;
        [Tooltip("증가량")]
        public float value;
    }

    [Tooltip("아이템 추가 옵션")]
    public List<AddOption> addOptions = new List<AddOption>();

    [Tooltip("아이템 아이콘")]
    public Sprite itemIcon;

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
                foreach (var option in options)
                {
                    switch (option.option)
                    {
                        case ItemOptionString.Attack:
                            inventory.t_Attack += option.value + (option.upgradeValue * upgadeLev);
                            break;

                        case ItemOptionString.Health:
                            inventory.t_Health += option.value + (option.upgradeValue * upgadeLev);
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
                foreach (var option in addOptions)
                {
                    switch (option.option)
                    {
                        case AddOptionString.Attack:
                            inventory.t_Attack += option.value;
                            break;

                        case AddOptionString.Health:
                            inventory.t_Health += option.value;
                            break;

                        case AddOptionString.AttackSpeed:
                            inventory.t_AttackSpeed += option.value;
                            break;

                        case AddOptionString.HealingHealth:
                            inventory.t_HealHealth += option.value;
                            break;

                        case AddOptionString.AttackPer:
                            inventory.t_AttackPer += option.value;
                            break;

                        case AddOptionString.Evade:
                            inventory.t_Evade += option.value;
                            break;

                        case AddOptionString.DamageReduction:
                            inventory.t_DamageReduction += option.value;
                            break;

                        case AddOptionString.Bloodsucking:
                            inventory.t_BloodSucking += option.value;
                            break;

                        case AddOptionString.CoinAcquire:
                            inventory.t_CoinAcquire += option.value;
                            break;

                        case AddOptionString.NormalDamage:
                            inventory.t_NormalDamage += option.value;
                            break;

                        case AddOptionString.SkillDamage:
                            inventory.t_SkillDamage += option.value;
                            break;

                        case AddOptionString.BossDamage:
                            inventory.t_BossDamage += option.value;
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
                foreach (var option in addOptions)
                {
                    switch (option.option)
                    {
                        case AddOptionString.Attack:
                            inventory.t_Attack -= option.value;
                            break;

                        case AddOptionString.Health:
                            inventory.t_Health -= option.value;
                            break;

                        case AddOptionString.AttackSpeed:
                            inventory.t_AttackSpeed -= option.value;
                            break;

                        case AddOptionString.HealingHealth:
                            inventory.t_HealHealth -= option.value;
                            break;

                        case AddOptionString.AttackPer:
                            inventory.t_AttackPer -= option.value;
                            break;

                        case AddOptionString.Evade:
                            inventory.t_Evade -= option.value;
                            break;

                        case AddOptionString.DamageReduction:
                            inventory.t_DamageReduction -= option.value;
                            break;

                        case AddOptionString.Bloodsucking:
                            inventory.t_BloodSucking -= option.value;
                            break;

                        case AddOptionString.CoinAcquire:
                            inventory.t_CoinAcquire -= option.value;
                            break;

                        case AddOptionString.NormalDamage:
                            inventory.t_NormalDamage -= option.value;
                            break;

                        case AddOptionString.SkillDamage:
                            inventory.t_SkillDamage -= option.value;
                            break;

                        case AddOptionString.BossDamage:
                            inventory.t_BossDamage -= option.value;
                            break;
                    }
                }
            }
        }
    }
}

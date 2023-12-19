using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Item")]
public class Item : ScriptableObject
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
}

using UnityEngine;

public class StateSystem : MonoBehaviour
{
    public class State
    {
        public float Attack = 0f;
        public float Health = 0f;
        public float AttackSpeed = 0f;
        public float HealHealth = 0f;
        public float AttackPer = 0f;
        public float HealthPer = 0f;
        public float Evade = 0f;
        public float DamageReduction = 0f;
        public float BloodSucking = 0f;
        public float CoinAcquire = 0f;
        public float NormalDamage = 0f;
        public float SkillDamage = 0f;
        public float BossDamage = 0f;
    }

    public State EquipItemState { get; private set; } = new State();
    public State AcquireItemState { get; private set; } =new State();
    public State SkillState { get; private set; } = new State();
    public State TotalState { get; private set; } = new State();

    private Inventory item;
    private SkillInventory skill;
    private PetInventory pet;

    private bool first = false;
    public void Setting()
    {
        if (first)
            return;

        item = InventorySystem.Instance.inventory;
        pet = InventorySystem.Instance.petInventory;
        skill = InventorySystem.Instance.skillInventory;

        first = true;
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
        TotalState.AttackPer = EquipItemState.AttackPer + AcquireItemState.AttackPer;
        TotalState.HealthPer = EquipItemState.HealthPer + AcquireItemState.HealthPer;

        TotalState.Attack = EquipItemState.Attack + AcquireItemState.Attack + (((EquipItemState.Attack + AcquireItemState.Attack) * TotalState.AttackPer)/100);
        TotalState.Health = EquipItemState.Health + AcquireItemState.Health + (((EquipItemState.Health + AcquireItemState.Health) * TotalState.HealthPer)/100);
        TotalState.AttackSpeed = EquipItemState.AttackSpeed + AcquireItemState.AttackSpeed;
        TotalState.HealHealth = EquipItemState.HealHealth + AcquireItemState.HealHealth;
        TotalState.Evade = EquipItemState.Evade + AcquireItemState.Evade;
        TotalState.DamageReduction = EquipItemState.DamageReduction + AcquireItemState.DamageReduction;
        TotalState.BloodSucking = EquipItemState.BloodSucking + AcquireItemState.BloodSucking;
        TotalState.CoinAcquire = EquipItemState.CoinAcquire + AcquireItemState.CoinAcquire;
        TotalState.NormalDamage = EquipItemState.NormalDamage + AcquireItemState.NormalDamage;
        TotalState.SkillDamage = EquipItemState.SkillDamage + AcquireItemState.SkillDamage;
        TotalState.BossDamage = EquipItemState.BossDamage + AcquireItemState.BossDamage;
    }
    public void EquipUpdate()
    {
        ResetState(EquipItemState);

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
        ResetState(AcquireItemState);

        foreach(var weapon in item.weapons)
        {
            if(!weapon.acquire)
                continue;

            AcquireOptions(weapon);
        }

        foreach(var armor in item.armors)
        {
            if(!armor.acquire)
                continue; 
            
            AcquireOptions(armor);
        }
        TotalStateSet();
    }

    public void SkillUpdate()
    {
        ResetState(SkillState);

        foreach(var passive in skill.skills)
        {
            if (!passive.acquire)
                continue;

            if (passive.skill.Skill_Type == SkillType.Active)
                continue;

            SkillOptions(passive);
        }

        TotalStateSet();
    }
    private void SkillOptions(SkillInventory.InventorySKill passive)
    {
        foreach(var option in passive.skill.Skill_Res)
        {
            switch(option.Skill_RE_Option)
            {
                case AddOptionString.AttackPer:
                    SkillState.AttackPer += option.Skill_RE_EFF + (passive.upgradeLev * option.Skill_RE_LVUP);
                    break;

                case AddOptionString.HealthPer:
                    SkillState.HealthPer += option.Skill_RE_EFF + (passive.upgradeLev * option.Skill_RE_LVUP);
                    break;
            }
        }
    }
    private void AcquireOptions(Inventory.InventoryItem acquireItem)
    {
        foreach (var option in acquireItem.item.addOptions)
        {
            switch (option.option)
            {
                case AddOptionString.AttackPer:
                    EquipItemState.AttackPer += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.HealthPer:
                        EquipItemState.HealthPer += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.Evade:
                    EquipItemState.Evade += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.DamageReduction:
                    EquipItemState.DamageReduction += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.Bloodsucking:
                    EquipItemState.BloodSucking += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.CoinAcquire:
                    EquipItemState.CoinAcquire += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.SkillDamage:
                    EquipItemState.SkillDamage += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.BossDamage:
                    EquipItemState.BossDamage += option.value + (acquireItem.upgradeLev * option.upgradeValue);
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
                    EquipItemState.Attack += option.value + (option.upgradeValue * equipItem.upgradeLev) + ((option.value + (option.upgradeValue * equipItem.upgradeLev)) * weight);
                    break;

                case ItemOptionString.Health:
                    EquipItemState.Health += option.value + (option.upgradeValue * equipItem.upgradeLev) + ((option.value + (option.upgradeValue * equipItem.upgradeLev)) * weight);
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
                            EquipItemState.AttackPer += option.value * weight;
                            break;

                        case AddOptionString.HealthPer:
                            EquipItemState.HealthPer += option.value * weight;
                            break;

                        case AddOptionString.Evade:
                            EquipItemState.Evade += option.value * weight;
                            break;

                        case AddOptionString.DamageReduction:
                            EquipItemState.DamageReduction += option.value * weight;
                            break;

                        case AddOptionString.Bloodsucking:
                            EquipItemState.BloodSucking += option.value * weight;
                            break;

                        case AddOptionString.CoinAcquire:
                            EquipItemState.CoinAcquire += option.value * weight;
                            break;

                        case AddOptionString.SkillDamage:
                            EquipItemState.SkillDamage += option.value * weight;
                            break;

                        case AddOptionString.BossDamage:
                            EquipItemState.BossDamage += option.value * weight;
                            break;
                    }
                }
            }
        }
    }
}

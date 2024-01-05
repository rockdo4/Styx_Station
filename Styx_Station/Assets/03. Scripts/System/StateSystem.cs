using UnityEngine;
using System.Numerics;

public class StateSystem : Singleton<StateSystem>
{
    [System.Serializable]
    public class State
    {
        public BigInteger Attack = new BigInteger(0);
        public BigInteger Health = new BigInteger(0);
        public BigInteger HealHealth = new BigInteger(0);
        public float AttackPer = 0f;
        public float HealthPer = 0f;
        public float Evade = 0f;
        public float DamageReduction = 0f;
        public float BloodSucking = 0f;
        public float CoinAcquire = 0f;
        public float NormalDamage = 0f;
        public float SkillDamage = 0f;
    }

    public State EquipItemState { get; private set; } = new State();
    public State AcquireItemState { get; private set; } = new State();
    public State SkillState { get; private set; } = new State();
    public State TotalState { get; set; } = new State();

    private Inventory item;
    private SkillInventory skill;

    public ResultPlayerStats state;

    private bool first = false;
    private BigInteger acquireState =new BigInteger(0);
    public void Setting()
    {
        if (first)
            return;

        item = InventorySystem.Instance.inventory;
        skill = InventorySystem.Instance.skillInventory;

        first = true;
    }

    public void TotalUpdate()
    {
        if(state== null)
        {
            state = GameObject.FindWithTag("Player").GetComponent<ResultPlayerStats>();
        }
        GetPlayerInfoPower();
        GetPlayerInfoHealth();
        GetPlayerInfoSiling();
        GetPlayerInfoEvade();
        GetPlayerInfoAbsorption();
        GetPlayerInfoReduction();
        GetPlayerInfoHealing();
        GetPlayerInfoNormalDamage();
        GetPlayerInfoSkillDamage();
    }
    public void GetPlayerInfoPower()
    {
        var equipState = ((state.playerAttribute.attackPower + (SharedPlayerStats.GetPlayerPower() - 1) * state.increaseUpgradePower) + EquipItemState.Attack) +
        (((state.playerAttribute.attackPower + (SharedPlayerStats.GetPlayerPower() - 1) * state.increaseUpgradePower) * (int)EquipItemState.AttackPer) / 100);

        acquireState = ((state.playerAttribute.attackPower + (SharedPlayerStats.GetPlayerPower() - 1) * state.increaseUpgradePower) * (int)AcquireItemState.AttackPer) / 100;

        var passiveState = (state.playerAttribute.attackPower + (SharedPlayerStats.GetPlayerPower() - 1) * state.increaseUpgradePower) * (int)SkillState.AttackPer / 100;

        var attributePower = ((state.playerAttribute.attackPower + (SharedPlayerStats.GetPlayerPower() - 1) * state.increaseUpgradePower) * GameData.labBuffData.re_Atk1) / GameData.labBuffDataPercent;

        var boost = ((SharedPlayerStats.GetPlayerPowerBoost() - 1) * state.increaseUpgradePowerBoost) / state.percentFloat;

        var powerBoostResult = (int)(boost * state.playerPowerBoostPercent) / state.playerPowerBoostPercent;

        var labBuff = GameData.labBuffData.re_Atk2 / GameData.labBuffDataPercent;

        TotalState.Attack = equipState + acquireState + passiveState + attributePower;

        TotalState.Attack = TotalState.Attack + ((TotalState.Attack * PlayerBuff.Instance.buffData.playerPowerBuff) / PlayerBuff.Instance.percent);

        TotalState.Attack += TotalState.Attack * (powerBoostResult + labBuff);
    }

    public void GetPlayerInfoNormalDamage()
    {
        var attributePower = (SharedPlayerStats.GetMonsterDamagePower() - 1) * state.monsterDamageFloat;

        var equipState = EquipItemState.NormalDamage;

        var acquireState = AcquireItemState.NormalDamage;

        var food = PlayerBuff.Instance.buffData.bossAttackBuff;

        TotalState.NormalDamage = attributePower + equipState + acquireState + food;
    }

    public void GetPlayerInfoSkillDamage()
    {
        var equipState = EquipItemState.SkillDamage;

        var acquireState = AcquireItemState.SkillDamage;

        var food = PlayerBuff.Instance.buffData.skillBuff;

        TotalState.SkillDamage = equipState + acquireState + food;
    }

    public void GetPlayerInfoHealth()
    {
        var attributeHp = ((state.playerAttribute.MaxHp * GameData.labBuffData.re_Hp1) / GameData.labBuffDataPercent);

        var playerHp = state.playerAttribute.MaxHp + attributeHp;

        var equipState = ((playerHp + (SharedPlayerStats.GetHp() - 1) * state.increaseUpgradeHp) + EquipItemState.Health) +
            (((playerHp + (SharedPlayerStats.GetHp() - 1) * state.increaseUpgradeHp) * (int)EquipItemState.HealthPer) / 100);

        var acquireState = (playerHp + (SharedPlayerStats.GetHp() - 1) * state.increaseUpgradeHp) * (int)AcquireItemState.HealthPer / 100;

        var passiveState = (playerHp + (SharedPlayerStats.GetHp() - 1) * state.increaseUpgradeHp) * (int)SkillState.HealthPer / 100;

        var labBuff = GameData.labBuffData.re_Hp2 / GameData.labBuffDataPercent;

        TotalState.Health = equipState + acquireState + passiveState;

        TotalState.Health += TotalState.Health * labBuff;
    }

    public void GetPlayerInfoSiling()
    {
        var equipState = EquipItemState.CoinAcquire / 100f;

        var acquireState = AcquireItemState.CoinAcquire / 100f;

        var labBuff = (float)GameData.labBuffData.re_Sliup / GameData.labBuffDataPercent;

        var food = (float)PlayerBuff.Instance.buffData.silingBuff / PlayerBuff.Instance.percent;

        TotalState.CoinAcquire = (equipState + acquireState + labBuff + food) * 100;
    }

    public void GetPlayerInfoEvade()
    {
        var equipState = EquipItemState.Evade;
        var acquireState = AcquireItemState.Evade;

        TotalState.Evade = equipState + acquireState;
    }

    public void GetPlayerInfoReduction()
    {
        var equipState = EquipItemState.DamageReduction;
        var acquireState = AcquireItemState.DamageReduction;

        TotalState.DamageReduction = equipState + acquireState;
    }

    public void GetPlayerInfoAbsorption()
    {
        var equipState = EquipItemState.BloodSucking;
        var acquireState = AcquireItemState.BloodSucking;

        TotalState.BloodSucking = equipState + acquireState;
    }

    public void GetPlayerInfoHealing()
    {
        var attributeHeal = 10 + ((SharedPlayerStats.GetHealing() - 1) * state.increaseUpgradeHealing / 100);

        var equipState = EquipItemState.HealHealth;

        var acquireState = AcquireItemState.HealHealth;

        TotalState.HealHealth = equipState + acquireState + attributeHeal;
    }


    private void ResetState(State state)
    {
        state.Attack = 0;
        state.Health = 0;
        state.HealHealth = 0;
        state.HealthPer = 0f;
        state.AttackPer = 0f;
        state.Evade = 0f;
        state.DamageReduction = 0f;
        state.BloodSucking = 0f;
        state.CoinAcquire = 0f;
        state.NormalDamage = 0f;
        state.SkillDamage = 0f;
    }
    public void EquipUpdate()
    {
        ResetState(EquipItemState);

        for(int i = 0; i<item.GetEquipItemsLength();++i)
        {
            var equips = item.GetEquipItem(i);

            if(equips == null)
                continue;

            if (equips.item == null)
                continue;

            if (equips.item.options.Count <= 0)
                continue;

            EquipOptions(equips);
        }
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
                    AcquireItemState.AttackPer += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.HealthPer:
                    AcquireItemState.HealthPer += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.Evade:
                    AcquireItemState.Evade += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.DamageReduction:
                    AcquireItemState.DamageReduction += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.Bloodsucking:
                    AcquireItemState.BloodSucking += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.CoinAcquire:
                    AcquireItemState.CoinAcquire += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.SkillDamage:
                    AcquireItemState.SkillDamage += option.value + (acquireItem.upgradeLev * option.upgradeValue);
                    break;

                case AddOptionString.BossDamage:
                    AcquireItemState.NormalDamage += option.value + (acquireItem.upgradeLev * option.upgradeValue);
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
                    EquipItemState.Attack += (int)(option.value + (option.upgradeValue * equipItem.upgradeLev) + ((option.value + (option.upgradeValue * equipItem.upgradeLev)) * weight));
                    break;

                case ItemOptionString.Health:
                    EquipItemState.Health += (int)(option.value + (option.upgradeValue * equipItem.upgradeLev) + ((option.value + (option.upgradeValue * equipItem.upgradeLev)) * weight));
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
                            EquipItemState.NormalDamage += option.value * weight;
                            break;
                    }
                }
            }
        }
    }
}

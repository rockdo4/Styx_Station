using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SaveDataVersionCurrent = SaveDataV3;
using System.Numerics;
using System.Linq;

public class SaveLoad : MonoBehaviour
{
    public void Save()
    {
        SaveDataVersionCurrent data = new SaveDataVersionCurrent();
        
        data.playerdata.playerPower = SharedPlayerStats.GetPlayerPower();
        data.playerdata.playerPowerboost = SharedPlayerStats.GetPlayerPowerBoost();
        data.playerdata.playerAttackSpeed = SharedPlayerStats.GetPlayerAttackSpeed();
        data.playerdata.critical=SharedPlayerStats.GetAttackCritical();
        data.playerdata.criticalPower =SharedPlayerStats.GetAttackCriticlaPower();
        data.playerdata.monsterDamage = SharedPlayerStats.GetMonsterDamagePower();
        data.playerdata.maxHp = SharedPlayerStats.GetHp();
        data.playerdata.healing = SharedPlayerStats.GetHealing();
        data.playerdata.money1 = CurrencyManager.money1.ToString();
        data.playerdata.money2 = CurrencyManager.money2.ToString();
        data.playerdata.money3 = CurrencyManager.money3.ToString();

        var inventory = InventorySystem.Instance.inventory;

        foreach (var item in inventory.weapons)
        {
            InventoryData weapon = new InventoryData(item.item.name, item.upgradeLev, item.acquire, item.equip, item.stock);
            data.weaponData.Add(weapon);
        }

        foreach (var item in inventory.armors)
        {
            InventoryData aromr = new InventoryData(item.item.name, item.upgradeLev, item.acquire, item.equip, item.stock);
            data.armorData.Add(aromr);
        }

        foreach(var item in inventory.customRings)
        {
            CustomData ring = new CustomData(item.copyData.name, item.item.upgradeLev, item.item.item.addOptions);
            data.customRingData.Add(ring);
        }

        foreach (var item in inventory.customSymbols)
        {
            CustomData symbol = new CustomData(item.copyData.name, item.item.upgradeLev, item.item.item.addOptions);
            data.customSymbolData.Add(symbol);
        }

        for (int i = 0; i < inventory.GetEquipItemsLength(); ++i)
        {
            var equip = inventory.GetEquipItem(i);

            if(equip == null)
                continue;

            if(equip.item == null)
                continue; 

            EquipData equips = new EquipData(equip.item.name, equip.item.type);
            data.equipItem.Add(equips);
        }

        var skillInventory = InventorySystem.Instance.skillInventory;

        foreach (var skill in skillInventory.skills)
        {
            SkillData skillData = new SkillData(skill.skill.name, skill.upgradeLev, skill.acquire, skill.equip, skill.stock);
            data.skillData.Add(skillData);
        }

        for(int i = 0; i < skillInventory.equipSkills.Length; ++i)
        {
            var equip = skillInventory.equipSkills[i];

            if (equip == null)
                continue;

            if(equip.skill == null)
                continue;

            EquipSkillData equips = new EquipSkillData(equip.skill.name, equip.equipIndex);
            data.equipSkill.Add(equips);
        }

        SaveLoadSystem.JsonSave(data, "Test.json");
        Debug.Log("Save ");
    }

    public void Load()
    {

        var path = Path.Combine(Application.persistentDataPath, "Test.json");
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);

            JObject jsonObject = JObject.Parse(json);
            string dataString = jsonObject["playerdata"].ToString();
            var data = JsonConvert.DeserializeObject<PlayerData>(dataString);
            SharedPlayerStats.PlayerPower = data.playerPower;
            SharedPlayerStats.PlayerPowerBoost = data.playerPowerboost;
            SharedPlayerStats.PlayerAttackSpeed = data.playerAttackSpeed;
            SharedPlayerStats.Critical = data.critical;
            SharedPlayerStats.CriticalPower = data.criticalPower;
            SharedPlayerStats.MonsterDamage = data.monsterDamage;
            SharedPlayerStats.MaxHp = data.maxHp;
            SharedPlayerStats.Healing = data.healing;

            CurrencyManager.money1 = BigInteger.Parse(data.money1);
            CurrencyManager.money2 = BigInteger.Parse(data.money2);
            CurrencyManager.money3 = BigInteger.Parse(data.money3);


            var inventory = InventorySystem.Instance.inventory;
            var t = testSingle.Instance;

            if (jsonObject.TryGetValue("weaponData", out JToken weaponToken))
            {
                string weapons = weaponToken.ToString();
                var wData = JsonConvert.DeserializeObject<List<InventoryData>>(weapons);
                foreach (var item in wData)
                {
                    var itemData = inventory.weapons.Where(x => x.item.name == item.itemName).FirstOrDefault();
                    if (itemData != null)
                    {
                        itemData.upgradeLev = item.upgradeLev;
                        itemData.acquire = item.acquire;
                        itemData.equip = item.equip;
                        itemData.stock = item.stock;
                    }
                }
            }

            if (jsonObject.TryGetValue("armorData", out JToken armorToken))
            {
                string armors = armorToken.ToString();
                var aData = JsonConvert.DeserializeObject<List<InventoryData>>(armors);
                foreach (var item in aData)
                {
                    var itemData = inventory.armors.Where(x => x.item.name == item.itemName).FirstOrDefault();
                    if (itemData != null)
                    {
                        itemData.upgradeLev = item.upgradeLev;
                        itemData.acquire = item.acquire;
                        itemData.equip = item.equip;
                        itemData.stock = item.stock;
                    }
                }
            }

            inventory.CustomReset();

            if (jsonObject.TryGetValue("customRingData", out JToken ringToken))
            {
                string customRings = ringToken.ToString();
                var rData = JsonConvert.DeserializeObject<List<CustomData>>(customRings);
                foreach (var item in rData)
                {
                    var baseItem = inventory.rings.Where(x => x.item.name == item.baseName).FirstOrDefault();
                    var dummy = InventorySystem.Instance.LoadCustom(baseItem.item, item.addOptions);
                    dummy.upgradeLev = item.upgradeLev;
                }
            }

            if (jsonObject.TryGetValue("customSymbolData", out JToken symbolToken))
            {
                string customSymbols = symbolToken.ToString();
                var sData = JsonConvert.DeserializeObject<List<CustomData>>(customSymbols);       
                foreach (var item in sData)
                {
                    var baseItem = inventory.symbols.Where(x => x.item.name == item.baseName).FirstOrDefault();
                    var dummy = InventorySystem.Instance.LoadCustom(baseItem.item, item.addOptions);
                    dummy.upgradeLev = item.upgradeLev;
                }
            }

            if (jsonObject.TryGetValue("equipItem", out JToken equipToken))
            {
                string equipItem = equipToken.ToString();
                var equipItemData = JsonConvert.DeserializeObject<List<EquipData>>(equipItem);

                foreach (var item in equipItemData)
                {
                    switch (item.itemType)
                    {
                        case ItemType.Weapon:
                            var weapon = inventory.weapons.Where(x => x.item.name == item.itemName).FirstOrDefault();
                            if (weapon != null)
                                inventory.EquipItem(weapon.index, item.itemType);                         
                            break;

                        case ItemType.Armor:
                            var aromr = inventory.armors.Where(x => x.item.name == item.itemName).FirstOrDefault();
                            if (aromr != null)
                                inventory.EquipItem(aromr.index, item.itemType);
                            break;

                        case ItemType.Ring:
                            var ring = inventory.customRings.Where(x => x.item.item.name == item.itemName).FirstOrDefault();
                            if (ring != null)
                                inventory.EquipItem(ring.item.index, item.itemType);
                                break;

                        case ItemType.Symbol:
                            var symbol = inventory.customSymbols.Where(x => x.item.item.name == item.itemName).FirstOrDefault();
                            if (symbol != null)
                                inventory.EquipItem(symbol.item.index, item.itemType);
                                break;
                    }
                }
            }

            var skillInventory = InventorySystem.Instance.skillInventory;

            if(jsonObject.TryGetValue("skillData", out JToken skillToken))
            {
                string skills = skillToken.ToString();
                var sData = JsonConvert.DeserializeObject<List<SkillData>>(skills);
                foreach(var skill in sData)
                {
                    var skillData = skillInventory.skills.Where(x => x.skill.name == skill.skillName).FirstOrDefault();
                    if(skillData != null)
                    {
                        skillData.upgradeLev = skill.skillLevel;
                        skillData.acquire = skill.acquire;
                        skillData.equip = skill.equip;
                        skillData.stock = skill.stock;
                    }
                }
            }

            if(jsonObject.TryGetValue("equipSkill", out JToken equipSkillToken))
            {
                string equipSkill = equipSkillToken.ToString();
                var equipSkillData = JsonConvert.DeserializeObject<List<EquipSkillData>>(equipSkill);

                foreach(var equip in equipSkillData)
                {
                    var skill = skillInventory.skills.Where(x=>x.skill.name ==  equip.skillName).FirstOrDefault();
                    if (skill != null)
                        skillInventory.EquipSkill(skill.skillIndex, equip.equipIndex);
                }
            }
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Save();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Load();
        }
    }
}
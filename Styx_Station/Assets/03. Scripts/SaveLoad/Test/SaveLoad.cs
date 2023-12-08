using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SaveDataVersionCurrent = SaveDataV4;
using System.Numerics;
using System.Linq;
using System;

public class SaveLoad : MonoBehaviour
{
    public void Save()
    {
        SaveDataVersionCurrent data = new SaveDataVersionCurrent();

        data.gameSaveDatas.playerdata.playerPower = SharedPlayerStats.GetPlayerPower();
        data.gameSaveDatas.playerdata.playerPowerboost = SharedPlayerStats.GetPlayerPowerBoost();
        data.gameSaveDatas.playerdata.playerAttackSpeed = SharedPlayerStats.GetPlayerAttackSpeed();
        data.gameSaveDatas.playerdata.critical = SharedPlayerStats.GetAttackCritical();
        data.gameSaveDatas.playerdata.criticalPower = SharedPlayerStats.GetAttackCriticlaPower();
        data.gameSaveDatas.playerdata.monsterDamage = SharedPlayerStats.GetMonsterDamagePower();
        data.gameSaveDatas.playerdata.maxHp = SharedPlayerStats.GetHp();
        data.gameSaveDatas.playerdata.healing = SharedPlayerStats.GetHealing();
        data.gameSaveDatas.playerdata.money1 = CurrencyManager.money1.ToString();
        data.gameSaveDatas.playerdata.money2 = CurrencyManager.money2.ToString();
        data.gameSaveDatas.playerdata.money3 = CurrencyManager.money3.ToString();

        var inventory = InventorySystem.Instance.inventory;

        foreach (var item in inventory.weapons)
        {
            InventoryData weapon = new InventoryData(item.item.name, item.upgradeLev, item.acquire, item.equip, item.stock);
            data.gameSaveDatas.weaponData.Add(weapon);
        }

        foreach (var item in inventory.armors)
        {
            InventoryData aromr = new InventoryData(item.item.name, item.upgradeLev, item.acquire, item.equip, item.stock);
            data.gameSaveDatas.armorData.Add(aromr);
        }

        foreach (var item in inventory.customRings)
        {
            CustomData ring = new CustomData(item.copyData.name, item.item.upgradeLev, item.item.item.addOptions);
            data.gameSaveDatas.customRingData.Add(ring);
        }

        foreach (var item in inventory.customSymbols)
        {
            CustomData symbol = new CustomData(item.copyData.name, item.item.upgradeLev, item.item.item.addOptions);
            data.gameSaveDatas.customSymbolData.Add(symbol);
        }

        for (int i = 0; i < inventory.GetEquipItemsLength(); ++i)
        {
            var equip = inventory.GetEquipItem(i);

            if (equip == null)
                continue;

            if (equip.item == null)
                continue;

            EquipData equips = new EquipData(equip.item.name, equip.item.type);
            data.gameSaveDatas.equipItem.Add(equips);
        }

        var skillInventory = InventorySystem.Instance.skillInventory;

        foreach (var skill in skillInventory.skills)
        {
            SkillData skillData = new SkillData(skill.skill.name, skill.upgradeLev, skill.acquire, skill.equip, skill.stock);
            data.gameSaveDatas.skillData.Add(skillData);
        }

        for (int i = 0; i < skillInventory.equipSkills.Length; ++i)
        {
            var equip = skillInventory.equipSkills[i];

            if (equip == null)
                continue;

            if (equip.skill == null)
                continue;

            EquipSkillData equips = new EquipSkillData(equip.skill.name, equip.equipIndex);
            data.gameSaveDatas.equipSkill.Add(equips);
        }
        data.gameSaveDatas.exitTime = DateTime.Now.ToString($"{GameData.datetimeString}");

        data.gameSaveDatas.keyAccumulateTime = GameData.keyPrevAccumlateTime.ToString();


        if (GameData.stageData.chapter == 0)
            GameData.stageData.chapter = 1;
        if (GameData.stageData.stage == 0)
            GameData.stageData.stage = 1;
        if (GameData.stageData.wave == 0)
            GameData.stageData.wave = 1;

        data.gameSaveDatas.stageData = GameData.stageData;

        ////// ui �Ŵ������� ��������;
        //var getCode = GetComponent<PlayerStatsUpgardeUI>();
        //var datas = getCode.thisIsTestCode.diningRoomUiButton.foodDatas;
        //for(int i =0;i<datas.Length;++i)
        //{
        //    var texture = getCode.thisIsTestCode.diningRoomUiButton.foodButton[i].GetComponent<Image>();
        //    if (datas[i] !=null && texture.sprite != getCode.thisIsTestCode.diningRoomUiButton.cookImage)
        //    {
        //        SaveFoodData foodData =new SaveFoodData();
        //        foodData.Food_Type = datas[i].Food_Type;
        //        foodData.Food_Name_ID = datas[i].Food_Name_ID;
        //        data.gameSaveDatas.diningRoomSaveFoodData[i] = foodData;
        //    }
        //}

        //data.gameSaveDatas.foodTimerUpgradeLevelUp = getCode.thisIsTestCode.foodTimerUpgradeLevelUp;
        //data.gameSaveDatas.foodSelectUpgradeLevelUp = getCode.thisIsTestCode.foodSelectUpgradeLevelUp;
        //data.gameSaveDatas.diningRoomTimer = getCode.thisIsTestCode.timer;

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
            if (jsonObject.TryGetValue("gameSaveDatas", out JToken gameSaveDatas))
            {
                if (gameSaveDatas["playerdata"] is JToken data)
                {
                    string player = data.ToString();
                    var playerD = JsonConvert.DeserializeObject<PlayerData>(player);
                    SharedPlayerStats.PlayerPower = playerD.playerPower;
                    SharedPlayerStats.PlayerPowerBoost = playerD.playerPowerboost;
                    SharedPlayerStats.PlayerAttackSpeed = playerD.playerAttackSpeed;
                    SharedPlayerStats.Critical = playerD.critical;
                    SharedPlayerStats.CriticalPower = playerD.criticalPower;
                    SharedPlayerStats.MonsterDamage = playerD.monsterDamage;
                    SharedPlayerStats.MaxHp = playerD.maxHp;
                    SharedPlayerStats.Healing = playerD.healing;

                    CurrencyManager.money1 = BigInteger.Parse(playerD.money1);
                    CurrencyManager.money2 = BigInteger.Parse(playerD.money2);
                    CurrencyManager.money3 = BigInteger.Parse(playerD.money3);
                }

                var inventory = InventorySystem.Instance.inventory;

                if (gameSaveDatas["weaponData"] is JToken weaponToken)
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
                if (gameSaveDatas["armorData"] is JToken armorToken)
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
                if (gameSaveDatas["customRingData"] is JToken ringToken)
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
                if (gameSaveDatas["customSymbolData"] is JToken symbolToken)
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
                if (gameSaveDatas["equipItem"] is JToken equipToken)
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

                if (gameSaveDatas["skillData"] is JToken skillToken)
                {
                    string skills = skillToken.ToString();
                    var sData = JsonConvert.DeserializeObject<List<SkillData>>(skills);
                    foreach (var skill in sData)
                    {
                        var skillData = skillInventory.skills.Where(x => x.skill.name == skill.skillName).FirstOrDefault();
                        if (skillData != null)
                        {
                            skillData.upgradeLev = skill.skillLevel;
                            skillData.acquire = skill.acquire;
                            skillData.equip = skill.equip;
                            skillData.stock = skill.stock;
                        }
                    }
                }

                if (gameSaveDatas["equipSkill"] is JToken equipSkillToken)
                {
                    string equipSkill = equipSkillToken.ToString();
                    var equipSkillData = JsonConvert.DeserializeObject<List<EquipSkillData>>(equipSkill);
                    foreach (var equip in equipSkillData)
                    {
                        var skill = skillInventory.skills.Where(x => x.skill.name == equip.skillName).FirstOrDefault();
                        if (skill != null)
                            skillInventory.EquipSkill(skill.skillIndex, equip.equipIndex);
                    }
                }
                if (gameSaveDatas["exitTime"] is JToken exitTime)
                {
                    string exit = exitTime.ToString();
                    GameData.exitTime.Clear();
                    GameData.exitTime.Append(exit);
                }

                if (gameSaveDatas["keyAccumulateTime"] is JToken accumlateTime)
                {
                    string accumlatesString = accumlateTime.ToString();
                    if (accumlatesString != "")
                    {
                        GameData.keyPrevAccumlateTime.Clear();
                        GameData.keyPrevAccumlateTime.Append(accumlatesString);
                    }
                    else
                    {
                        GameData.keyPrevAccumlateTime.Append(DateTime.Now.ToString($"{GameData.datetimeString}"));
                    }
                }
                else
                {
                    GameData.keyPrevAccumlateTime.Append(DateTime.Now.ToString($"{GameData.datetimeString}"));
                }
                if(gameSaveDatas["foodTimerUpgradeLevelUp"] is JToken foodtimerUpgradeLevel)
                {
                    //var getCode = GetComponent<PlayerStatsUpgardeUI>();
                    //getCode.thisIsTestCode.foodTimerUpgradeLevelUp = int.Parse(foodtimerUpgradeLevel.ToString());
                }
                if (gameSaveDatas["foodSelectUpgradeLevelUp"] is JToken foodSelect)
                {
                    //var getCode = GetComponent<PlayerStatsUpgardeUI>();
                    //var number = getCode.thisIsTestCode.foodSelectUpgradeLevelUp = int.Parse(foodSelect.ToString());
                    //getCode.thisIsTestCode.diningRoomUiButton.upgradeSelectFoodCount = number+2;
                }
                if (gameSaveDatas["diningRoomSaveFoodData"] is JToken diningRoomSaveFoodDatats)
                {
                    //string str = diningRoomSaveFoodDatats.ToString();
                    //var saveFoodData = JsonConvert.DeserializeObject<SaveFoodData[]>(str);
                    //var getCode = GetComponent<PlayerStatsUpgardeUI>();
                    //for (int i=0;i<saveFoodData.Length;i++)
                    //{
                    //    if (saveFoodData[i] != null)
                    //    {
                    //        getCode.thisIsTestCode.diningRoomUiButton.loadCurrentIndex = i;
                    //        getCode.thisIsTestCode.LoadFood(saveFoodData[i]);
                    //    }
                    //}
                       
                }
                if (gameSaveDatas["diningRoomTimer"] is JToken timer)
                {
                    //string str = timer.ToString();
                    //var getCode = GetComponent<PlayerStatsUpgardeUI>();
                    //var timerData = JsonConvert.DeserializeObject<float>(str);
                    //getCode.thisIsTestCode.timer = timerData;

                }
            }
        }
    }
    /*
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

            if (jsonObject.TryGetValue("skillData", out JToken skillToken))
            {
                string skills = skillToken.ToString();
                var sData = JsonConvert.DeserializeObject<List<SkillData>>(skills);
                foreach (var skill in sData)
                {
                    var skillData = skillInventory.skills.Where(x => x.skill.name == skill.skillName).FirstOrDefault();
                    if (skillData != null)
                    {
                        skillData.upgradeLev = skill.skillLevel;
                        skillData.acquire = skill.acquire;
                        skillData.equip = skill.equip;
                        skillData.stock = skill.stock;
                    }
                }
            }

            if (jsonObject.TryGetValue("equipSkill", out JToken equipSkillToken))
            {
                string equipSkill = equipSkillToken.ToString();
                var equipSkillData = JsonConvert.DeserializeObject<List<EquipSkillData>>(equipSkill);

                foreach (var equip in equipSkillData)
                {
                    var skill = skillInventory.skills.Where(x => x.skill.name == equip.skillName).FirstOrDefault();
                    if (skill != null)
                        skillInventory.EquipSkill(skill.skillIndex, equip.equipIndex);
                }
            }



            if (jsonObject.ContainsKey("exitTime"))
            {
                string str = jsonObject["exitTime"].ToString();
                GameData.exitTime.Clear();
                GameData.exitTime.Append(str);
            }

            if (jsonObject.ContainsKey("keyAccumulateTime"))
            {
                string str = jsonObject["keyAccumulateTime"].ToString();
                if (str != "")
                {
                    GameData.keyPrevAccumlateTime.Clear();
                    GameData.keyPrevAccumlateTime.Append(str);
                }
            }
            else
            {
                GameData.keyPrevAccumlateTime.Append(DateTime.Now.ToString($"{GameData.datetimeString}"));
            }

            if (jsonObject.ContainsKey("diningRoomSaveFoodData"))
            {
                string str = jsonObject["diningRoomSaveFoodData"].ToString();
                var saveFoodData = JsonConvert.DeserializeObject<SaveFoodData[]>(str);

                var getCode = GetComponent<PlayerStatsUpgardeUI>();
                foreach (var item in saveFoodData)
                {
                    if (item != null)
                    {
                        getCode.thisIsTestCode.LoadFood(item);  
                    }
                }
               
            }

        }
    }*/

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Save();
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Load();
        //}
    }
}
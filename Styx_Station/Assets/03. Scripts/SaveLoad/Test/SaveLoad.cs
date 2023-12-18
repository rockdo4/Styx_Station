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
    private DiningRoomSystem diningRoomsystem;
    private PlayerBuff playerbuff;
    private WaveManager waveManager;
    private ShopSystem shop;
    private LabSystem labSystem;
    private void Start()
    {
        playerbuff= PlayerBuff.Instance;
        waveManager = WaveManager.Instance;
        shop = ShopSystem.Instance;
        labSystem = LabSystem.Instance;
    }
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

        data.gameSaveDatas.itemRank = shop.currentItemRank;
        data.gameSaveDatas.itemRankUp = shop.currentItemRankUp;
        data.gameSaveDatas.skillRank = shop.currentSkillRank;
        data.gameSaveDatas.skillRankUp = shop.currentSkillRankUp;

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
            SkillData skillData = new SkillData(skill.skill.name, skill.upgradeLev, skill.acquire, skill.stock);
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


        //if (GameData.stageData.chapter == 0)
        //    GameData.stageData.chapter = 1;
        //if (GameData.stageData.stage == 0)
        //    GameData.stageData.stage = 1;
        //if (GameData.stageData.wave == 0)
        //    GameData.stageData.wave = 1;

        //data.gameSaveDatas.stageData = GameData.stageData;

        data.gameSaveDatas.stageIndex = waveManager.GetCurrentIndex();

        if (diningRoomsystem != null)
        {
            var foodDatas = diningRoomsystem.foodDatas;
            for (int i = 0; i < foodDatas.Length; ++i)
            {
                if (foodDatas[i] != null)
                {
                    SaveFoodData foodData = new SaveFoodData();
                    foodData.Food_Type = foodDatas[i].Food_Type;
                    foodData.Food_Name_ID = foodDatas[i].Food_Name_ID;
                    data.gameSaveDatas.diningRoomSaveFoodData[i] = foodData;
                }
            }
            data.gameSaveDatas.foodTimerUpgradeLevelUp = diningRoomsystem.timerUpgradeLevel;
            data.gameSaveDatas.foodSelectUpgradeLevelUp = diningRoomsystem.selectFoodCount;
            data.gameSaveDatas.diningRoomTimer = diningRoomsystem.timer;
        }

        data.gameSaveDatas.playerBuff = playerbuff.buffData;


        foreach (var datas in labSystem.Re001_Vertex)
        {
            data.gameSaveDatas.Re001_Lab_SaveDatas.Add(new LabSaveData(datas.vertexID, datas.isClear));
        }
        foreach (var datas in labSystem.Re002_Vertex)
        {
            data.gameSaveDatas.Re002_Lab_SaveDatas.Add(new LabSaveData(datas.vertexID, datas.isClear));
        }
        foreach (var datas in labSystem.Re003_Vertex)
        {
            data.gameSaveDatas.Re003_Lab_SaveDatas.Add(new LabSaveData(datas.vertexID, datas.isClear));
        }
        foreach (var datas in labSystem.Re004_Vertex)
        {
            data.gameSaveDatas.Re004_Lab_SaveDatas.Add(new LabSaveData(datas.vertexID, datas.isClear));
        }
        foreach (var datas in labSystem.Re005_Vertex)
        {
            data.gameSaveDatas.Re005_Lab_SaveDatas.Add(new LabSaveData(datas.vertexID, datas.isClear));
        }
        foreach (var datas in labSystem.Re006_Vertex)
        {
            data.gameSaveDatas.Re006_Lab_SaveDatas.Add(new LabSaveData(datas.vertexID, datas.isClear));
        }
        if(labSystem.isResearching)
        {
            data.gameSaveDatas.currentLavSaveData = new CurrentLavSaveData(labSystem.isResearching, labSystem.timerTic, labSystem.maxTimerTic, labSystem.labType, labSystem.level, labSystem.labStringTableName, labSystem.labBuffStringTable, labSystem.labTalbeData);
        }
        SaveLoadSystem.JsonSave(data, "Test.json");
        Debug.Log("Save ");
    }
    public void Load()
    {
        if (diningRoomsystem == null)
        {
            diningRoomsystem = DiningRoomSystem.Instance;
        }

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

                    SharedPlayerStats.CheckLimitAll();

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

                var uiInvenvtory = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>();
                uiInvenvtory.Setting();
                var uiSkill = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[2].GetComponent<SkillWindow>();
                uiSkill.Setting();

                if (gameSaveDatas["equipItem"] is JToken equipToken)
                {
                    string equipItem = equipToken.ToString();
                    var equipItemData = JsonConvert.DeserializeObject<List<EquipData>>(equipItem);

                    var weaponInfo = uiInvenvtory.inventoryTypes[0].gameObject.GetComponent<WeaponType>().info.GetComponent<WeaponEquipInfoUi>();
                    var armorInfo = uiInvenvtory.inventoryTypes[1].gameObject.GetComponent<ArmorType>().info.GetComponent<ArmorEquipInfoUi>();

                    foreach (var item in equipItemData)
                    {
                        switch (item.itemType)
                        {
                            case ItemType.Weapon:
                                var weapon = inventory.weapons.Where(x => x.item.name == item.itemName).FirstOrDefault();
                                if (weapon != null)
                                {
                                    weaponInfo.selectIndex = weapon.index;
                                    weaponInfo.OnClickWeaponEquip();
                                }
                                    break;

                            case ItemType.Armor:
                                var armor = inventory.armors.Where(x => x.item.name == item.itemName).FirstOrDefault();
                                if (armor != null)
                                {
                                    armorInfo.selectIndex = armor.index;
                                    armorInfo.OnClickArmorEquip();
                                }
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
                        {
                            uiSkill.selectIndex = skill.skillIndex;
                            uiSkill.equipMode = true;
                            uiSkill.equipButtons[equip.equipIndex].GetComponent<NormalButton>().OnClickEquip(uiSkill);
                            uiSkill.selectIndex = -1;
                        }
                    }
                    UIManager.Instance.SkillButtonOn();
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
                    diningRoomsystem.timerUpgradeLevel = int.Parse(foodtimerUpgradeLevel.ToString());
                }
                if (gameSaveDatas["foodSelectUpgradeLevelUp"] is JToken foodSelect)
                {
                    var t = int.Parse(foodSelect.ToString());
                    if (t <= 0)
                        t = 1;

                    diningRoomsystem.selectFoodCount = t;
                }
                if (gameSaveDatas["diningRoomSaveFoodData"] is JToken diningRoomSaveFoodDatats)
                {
                    string str = diningRoomSaveFoodDatats.ToString();
                    var saveFoodData = JsonConvert.DeserializeObject<SaveFoodData[]>(str);
                    for (int i = 0; i < saveFoodData.Length; i++)
                    {
                        if (saveFoodData[i] != null)
                        {
                            diningRoomsystem.LoadFoodData(saveFoodData[i], i);
                        }
                    }

                }
                if (gameSaveDatas["diningRoomTimer"] is JToken timer)
                {
                    string str = timer.ToString();
                    var timerData = JsonConvert.DeserializeObject<float>(str);
                    if(timerData <=0f)
                    {
                        diningRoomsystem.isLoad = false;
                    }
                    else
                    {
                        diningRoomsystem.timer = timerData;
                        diningRoomsystem.isLoad = true;
                    }
                    int count = 0;
                    for(int i =0;i < DiningRoomSystem.Instance.saveFood.Length; ++i)
                    {
                        if ( DiningRoomSystem.Instance.saveFood[i] != null)
                        {
                            count++;
                        }
                    }
                    if(count >= DiningRoomSystem.Instance.selectFoodCount)
                    {
                        diningRoomsystem.timer = 0f;
                        diningRoomsystem.isFullFood = true;
                        diningRoomsystem.isLoad = true;
                    }
                    diningRoomsystem.LoadMaxTimer();

                    diningRoomsystem.CalculateTimer(count);
                }
                var shop = ShopSystem.Instance;

                if (gameSaveDatas["itemRank"] is JToken rank)
                {
                    string str = rank.ToString();
                    var itemRank = JsonConvert.DeserializeObject<int>(str);
                    shop.currentItemRank = itemRank;
                }
                if (gameSaveDatas["itemRankUp"]is JToken rankUp)
                {
                    string str = rankUp.ToString();
                    var itemRankUp = JsonConvert.DeserializeObject<int>(str);
                    shop.currentItemRankUp = itemRankUp;
                }
                if (gameSaveDatas["skillRank"] is JToken rank_s)
                {
                    string str = rank_s.ToString();
                    var skillRank = JsonConvert.DeserializeObject<int>(str);
                    shop.currentSkillRank = skillRank;
                }
                if (gameSaveDatas["skillRankUp"]is JToken rankUp_s)
                {
                    string str = rankUp_s.ToString();
                    var skillRankUp = JsonConvert.DeserializeObject<int>(str);
                    shop.currentSkillRankUp = skillRankUp;
                }
                if (gameSaveDatas["playerBuff"] is JToken buffTimer)
                {
                    string str = buffTimer.ToString();
                    var buffData = JsonConvert.DeserializeObject<PlayerBuffData>(str);
                    if(buffData.isEatFood) 
                        PlayerBuff.Instance.buffData = buffData;
                    else
                    {
                        PlayerBuff.Instance.Reset();
                    }

                }
                if (gameSaveDatas["stageIndex"] is JToken stageIndex)
                {
                    string str = stageIndex.ToString();
                    var stageData = JsonConvert.DeserializeObject<int>(str);
                    
                    WaveManager.Instance.SetStageByIndexStage(stageData);
                }


                if (gameSaveDatas["Re001_Lab_SaveDatas"] is JToken labATK1)
                {
                    string str = labATK1.ToString();
                    var labData = JsonConvert.DeserializeObject<List<LabSaveData>>(str);

                    GameData.Re_AtkSaveDataList=labData;
                }
                if (gameSaveDatas["Re002_Lab_SaveDatas"] is JToken labHp1)
                {
                    string str = labHp1.ToString();
                    var labData = JsonConvert.DeserializeObject<List<LabSaveData>>(str);
                    GameData.Re_HPSaveDataList=labData;
                }
                if (gameSaveDatas["Re003_Lab_SaveDatas"] is JToken labCri)
                {
                    string str = labCri.ToString();
                    var labData = JsonConvert.DeserializeObject<List<LabSaveData>>(str);
                    GameData.Re_CriSaveDataList=labData;
                }
                if (gameSaveDatas["Re004_Lab_SaveDatas"] is JToken labSil)
                {
                    string str = labSil.ToString();
                    var labData = JsonConvert.DeserializeObject<List<LabSaveData>>(str);
                    GameData.Re_SilupSaveDataList=labData;
                }
                if (gameSaveDatas["Re005_Lab_SaveDatas"] is JToken labATK2)
                {
                    string str = labATK2.ToString();
                    var labData = JsonConvert.DeserializeObject<List<LabSaveData>>(str);
                    GameData.Re_MidAtkSaveDataList=labData;
                }
                if (gameSaveDatas["Re006_Lab_SaveDatas"] is JToken labHp2)
                {
                    string str = labHp2.ToString();
                    var labData = JsonConvert.DeserializeObject<List<LabSaveData>>(str);
                    GameData.Re_MidHPSaveDataList=labData;
                }
                if (gameSaveDatas["currentLavSaveData"] is JToken currentLabSaveData)
                {
                    string str = currentLabSaveData.ToString();
                    var currentLab = JsonConvert.DeserializeObject<CurrentLavSaveData>(str);

                    if(currentLab.isResearching)
                    {
                        GameData.currnetLabSaveData = currentLab;
                        LabSystem.Instance.maxTimerTic = currentLab.maxTimer;
                    }
                }
            }
        }
    }
}
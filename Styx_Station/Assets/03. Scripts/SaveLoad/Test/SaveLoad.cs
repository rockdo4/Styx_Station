using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SaveDataVersionCurrent = SaveDataV4;
using System.Numerics;
using System.Linq;
using System;
using UnityEngine.Tilemaps;
using UnityEditor.U2D.Aseprite;
using Unity.VisualScripting;
using UnityEngine.AI;
using static Tutorial;

public class SaveLoad : MonoBehaviour
{
    private DiningRoomSystem diningRoomsystem;
    public PlayerBuff playerbuff;
    public WaveManager waveManager;
    public ShopSystem shop;
    public LabSystem labSystem;
    public SkillManager skillManager;
    //12.26 ÀÌ½Â¿ì Ãß°¡
    public UIManager UIManager;
    public MakeTableData MakeTableData;
    private void Start()
    {
        playerbuff = PlayerBuff.Instance;
        //waveManager = WaveManager.Instance;
        shop = ShopSystem.Instance;
        labSystem = LabSystem.Instance;
        //skillManager = SkillManager.Instance;
        UIManager = UIManager.Instance;
        MakeTableData = MakeTableData.Instance;
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
        data.gameSaveDatas.playerdata.money4 = CurrencyManager.itemAsh.ToString();

        data.gameSaveDatas.itemRank = shop.currentItemRank;
        data.gameSaveDatas.itemRankUp = shop.currentItemRankUp;
        data.gameSaveDatas.skillRank = shop.currentSkillRank;
        data.gameSaveDatas.skillRankUp = shop.currentSkillRankUp;
        data.gameSaveDatas.petRank = shop.currentPetRank;
        data.gameSaveDatas.petRankUp = shop.currentPetRankUp;

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
        var petInventory = InventorySystem.Instance.petInventory;
        foreach (var pet in petInventory.pets)
        {
            PetData petData = new PetData(pet.pet.name, pet.upgradeLev, pet.acquire, pet.stock);
            data.gameSaveDatas.petData.Add(petData);
        }
        for (int i = 0; i < petInventory.equipPets.Length; ++i)
        {
            var equip = petInventory.equipPets[i];

            if (equip == null)
                continue;

            if (equip.pet == null)
                continue;

            EquipPetData equips = new EquipPetData(equip.pet.name, equip.equipIndex);
            data.gameSaveDatas.equipPet.Add(equips);
        }

        var UiSetting = UIManager.windows[5].gameObject.GetComponent<MenuWindow>().settingBox.GetComponent<SettingBox>();

        data.gameSaveDatas.sound = UiSetting.soundValue;

        data.gameSaveDatas.language = Global.language;

        data.gameSaveDatas.exitTime = TestServerTime.Instance.GetCurrentDateTime().ToString($"{GameData.datetimeString}");// DateTime.Now.ToString($"{GameData.datetimeString}");

        data.gameSaveDatas.keyAccumulateTime = GameData.keyPrevAccumlateTime.ToString();


        if (waveManager != null)
        {
            data.gameSaveDatas.stageIndex = waveManager.GetCurrentIndex();
            data.gameSaveDatas.isRepeat = waveManager.GetIsRepeat();
        }


        if (skillManager != null)
        {
            data.gameSaveDatas.isAuto = skillManager.isAuto;
        }


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
        data.gameSaveDatas.buffFoddID = playerbuff.foodId.ToString();


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
        if (labSystem.isResearching)
        {
            data.gameSaveDatas.currentLavSaveData = new CurrentLavSaveData(labSystem.isResearching, labSystem.timerTic, labSystem.maxTimerTic, labSystem.labType,
                labSystem.level, labSystem.labStringTableName, labSystem.labBuffStringTable, labSystem.labTalbeData);
        }

        data.gameSaveDatas.labBuffData = GameData.labBuffData;
        data.gameSaveDatas.currentQuestIndex = MakeTableData.currentQuestIndex;
        data.gameSaveDatas.currentQuestType = (int)UIManager.questSystemUi.currentQuestType;
        data.gameSaveDatas.currentLoopQuestIndex = MakeTableData.loppCurrentQuestIndex;
        data.gameSaveDatas.currentQuestSystemData = UIManager.questSystemUi.questData;

        var tutorialData = UIManager.tutorial.GetComponent<TutorialSystem>();
        if (tutorialData.tutorialIndex == 0 && tutorialData.playIndex < 3)
        {
            data.gameSaveDatas.tutorialIndex = 0;
            data.gameSaveDatas.playIndex = 0;
        }
        else if (tutorialData.tutorialIndex == 0 && tutorialData.playIndex >= 3 && tutorialData.playIndex < 11)
        {
            data.gameSaveDatas.tutorialIndex = 0;
            data.gameSaveDatas.playIndex = 3;
        }
        else if (tutorialData.tutorialIndex == 0 && tutorialData.playIndex >= 11 && tutorialData.playIndex < 14)
        {
            data.gameSaveDatas.tutorialIndex = 0;
            data.gameSaveDatas.playIndex = 14;
        }
        else if (tutorialData.tutorialIndex == 0 && tutorialData.playIndex >= 15 && tutorialData.playIndex < 19)
        {
            data.gameSaveDatas.tutorialIndex = 0;
            data.gameSaveDatas.playIndex = 15;
        }
        else if (tutorialData.tutorialIndex == 1 && tutorialData.playIndex < 4)
        {
            data.gameSaveDatas.tutorialIndex = 1;
            data.gameSaveDatas.playIndex = 0;
        }
        else if (tutorialData.tutorialIndex == 1 && tutorialData.playIndex >= 4 && tutorialData.playIndex < 11)
        {
            data.gameSaveDatas.tutorialIndex = 1;
            data.gameSaveDatas.playIndex = 6;
        }
        else if (tutorialData.tutorialIndex == 1 && tutorialData.playIndex >= 11)
        {
            data.gameSaveDatas.tutorialIndex = 2;
            data.gameSaveDatas.playIndex = 0;
        }
        else if (tutorialData.tutorialIndex == 2 && tutorialData.playIndex <10)
        {
            data.gameSaveDatas.tutorialIndex = 2;
            data.gameSaveDatas.playIndex = 0;
        }
        else if (tutorialData.tutorialIndex == 2 && tutorialData.playIndex >= 10)
        {
            data.gameSaveDatas.tutorialIndex = 3;
            data.gameSaveDatas.playIndex = 0;
        }
        else if (tutorialData.tutorialIndex == 3 && tutorialData.playIndex < 9)
        {
            data.gameSaveDatas.tutorialIndex = 3;
            data.gameSaveDatas.playIndex = 0;
        }
        else if (tutorialData.tutorialIndex == 4 && tutorialData.playIndex < 6)
        {
            data.gameSaveDatas.tutorialIndex = 4;
            data.gameSaveDatas.playIndex = 0;
        }
        else if(tutorialData.tutorialIndex == 5)
        {
            data.gameSaveDatas.tutorialIndex = 5;
            data.gameSaveDatas.playIndex = 0;
        }
        data.gameSaveDatas.shop = tutorialData.shop;
        data.gameSaveDatas.dining = tutorialData.dining;
        data.gameSaveDatas.lab = tutorialData.lab;
        data.gameSaveDatas.clean = tutorialData.clean;
        data.gameSaveDatas.fail = tutorialData.failrue;
        data.gameSaveDatas.loadTutorial = tutorialData.loadTutorial;

        data.gameSaveDatas.bossRushIndex = UIManager.windows[3].GetComponent<CleanWindow>().openStage;
        data.gameSaveDatas.bossRushCount = UIManager.windows[3].GetComponent<CleanWindow>().currentCount;

        //data.gameSaveDatas.Version = data.GetVersion();
        SaveLoadSystem.JsonSave(data, "Test.json");

        SaveLoadSystem.TxtFileSave(data);
    }
    public void Load()
    {

        if (diningRoomsystem == null)
        {
            diningRoomsystem = DiningRoomSystem.Instance;
        }
        string paths = Path.Combine(SaveLoadSystem.SaveDirectory, "TestText.dat");
        var test = SaveLoadSystem.BinaryToTxtAndJson("TestBinary.bin");

        if (test != null)
        {
            LoadByPlayerDataSet(test.gameSaveDatas.playerdata);
            var inventory = InventorySystem.Instance;
            LoadByInventoryWeaponData(inventory, test.gameSaveDatas.weaponData);
            LoadByInventoryArmorData(inventory, test.gameSaveDatas.armorData);
            inventory.inventory.CustomReset();
            LoadByInventoryCustomRingData(inventory, test.gameSaveDatas.customRingData);
            LoadByInventoryCustomSymbolData(inventory, test.gameSaveDatas.customSymbolData);
            LoadByEquipItem(test.gameSaveDatas.equipItem);
            LoadBySkillData(test.gameSaveDatas.skillData);
            LoadByEquipSkill(test.gameSaveDatas.equipSkill);
            LoadByPetData(test.gameSaveDatas.petData);
            LoadByEquipPet(test.gameSaveDatas.equipPet);
            LoadByExitTime(test.gameSaveDatas.exitTime);
            LoadByKeyAccumulateTime(test.gameSaveDatas.keyAccumulateTime);
            LoadByFoodUpgradeType(test.gameSaveDatas.foodTimerUpgradeLevelUp, test.gameSaveDatas.foodSelectUpgradeLevelUp);
            LoadByDiningRoomSaveFoodData(test.gameSaveDatas.diningRoomSaveFoodData);
            LoadByDiningRoomTimer(test.gameSaveDatas.diningRoomTimer);
            var shop = ShopSystem.Instance;
            LoadByItemRankAndRankUP(shop, test.gameSaveDatas.itemRank, test.gameSaveDatas.itemRankUp);
            LoadBySkillRankAndSkillRankUp(shop, test.gameSaveDatas.skillRank, test.gameSaveDatas.skillRankUp);
            LoadByPetRankAndPetRankUp(shop, test.gameSaveDatas.petRank, test.gameSaveDatas.petRankUp);
            var loadState = StateSystem.Instance;

            loadState.EquipUpdate();
            loadState.AcquireUpdate();
            loadState.SkillUpdate();

            LoadByPlayerBuff(test.gameSaveDatas.playerBuff);
            LoadByBuffFoddID(test.gameSaveDatas.buffFoddID);
            LoadByStageIndex(test.gameSaveDatas.stageIndex);
            LoadByLanguage(test.gameSaveDatas.language);
            LoadByIsRepeatAndIsAuto(test.gameSaveDatas.isRepeat, test.gameSaveDatas.isAuto);
            LoadByLabSaveData(test.gameSaveDatas.Re001_Lab_SaveDatas, test.gameSaveDatas.Re002_Lab_SaveDatas, test.gameSaveDatas.Re003_Lab_SaveDatas,
                test.gameSaveDatas.Re004_Lab_SaveDatas, test.gameSaveDatas.Re005_Lab_SaveDatas, test.gameSaveDatas.Re006_Lab_SaveDatas);
            LoadByCurrentLavSaveData(test.gameSaveDatas.currentLavSaveData);
            LoadByLabBuffData(test.gameSaveDatas.labBuffData);

            var tutorialUi = UIManager.Instance.tutorial.GetComponent<TutorialSystem>();

            LoadByTutorialIndex(tutorialUi, test.gameSaveDatas.tutorialIndex,test.gameSaveDatas.playIndex);
            LoadByTutorialDone(tutorialUi, test.gameSaveDatas.shop, test.gameSaveDatas.lab, test.gameSaveDatas.dining, test.gameSaveDatas.clean,
                test.gameSaveDatas.fail, test.gameSaveDatas.loadTutorial);
            LoadByQuestType(test.gameSaveDatas.currentQuestIndex, test.gameSaveDatas.currentQuestType, test.gameSaveDatas.currentLoopQuestIndex,
                test.gameSaveDatas.currentQuestSystemData);

            var UiSetting = UIManager.Instance.windows[5].gameObject.GetComponent<MenuWindow>().settingBox.GetComponent<SettingBox>();
            LoadBySound(UiSetting, test.gameSaveDatas.sound);

            var cleanWindow = UIManager.Instance.windows[3].GetComponent<CleanWindow>();
            LoadByBosshInfo(cleanWindow, test.gameSaveDatas.bossRushIndex, test.gameSaveDatas.bossRushCount);

            GameData.isLoad = true;
        }
        else
        {
            var str = TestServerTime.Instance.GetCurrentDateTime().ToString($"{GameData.datetimeString}");
            GameData.keyPrevAccumlateTime.Append(str);

            Debug.Log("Non Binary");
        }
    }



    private void LoadByPlayerDataSet(PlayerData playerData)
    {
        SharedPlayerStats.PlayerPower = playerData.playerPower;
        SharedPlayerStats.PlayerPowerBoost = playerData.playerPowerboost;
        SharedPlayerStats.PlayerAttackSpeed = playerData.playerAttackSpeed;
        SharedPlayerStats.Critical = playerData.critical;
        SharedPlayerStats.CriticalPower = playerData.criticalPower;
        SharedPlayerStats.MonsterDamage = playerData.monsterDamage;
        SharedPlayerStats.MaxHp = playerData.maxHp;
        SharedPlayerStats.Healing = playerData.healing;

        SharedPlayerStats.CheckLimitAll();

        CurrencyManager.money1 = BigInteger.Parse(playerData.money1);
        CurrencyManager.money2 = BigInteger.Parse(playerData.money2);
        CurrencyManager.money3 = BigInteger.Parse(playerData.money3);
        CurrencyManager.itemAsh = BigInteger.Parse(playerData.money4);
    }

    private void LoadByInventoryWeaponData(InventorySystem inventory, List<InventoryData> wData)
    {
        foreach (var item in wData)
        {
            var itemData = inventory.inventory.weapons.Where(x => x.item.name == item.itemName).FirstOrDefault();
            if (itemData != null)
            {
                itemData.upgradeLev = item.upgradeLev;
                itemData.acquire = item.acquire;
                itemData.equip = item.equip;
                itemData.stock = item.stock;
            }
        }
    }
    private void LoadByInventoryArmorData(InventorySystem inventory, List<InventoryData> aData)
    {
        foreach (var item in aData)
        {
            var itemData = inventory.inventory.armors.Where(x => x.item.name == item.itemName).FirstOrDefault();
            if (itemData != null)
            {
                itemData.upgradeLev = item.upgradeLev;
                itemData.acquire = item.acquire;
                itemData.equip = item.equip;
                itemData.stock = item.stock;
            }
        }
    }
    private void LoadByInventoryCustomRingData(InventorySystem inventory, List<CustomData> rData)
    {
        foreach (var item in rData)
        {
            var baseItem = inventory.inventory.rings.Where(x => x.item.name == item.baseName).FirstOrDefault();
            var dummy = InventorySystem.Instance.LoadCustom(baseItem.item, item.addOptions);
            dummy.upgradeLev = item.upgradeLev;
        }
    }
    private void LoadByInventoryCustomSymbolData(InventorySystem inventory, List<CustomData> rData)
    {
        foreach (var item in rData)
        {
            var baseItem = inventory.inventory.symbols.Where(x => x.item.name == item.baseName).FirstOrDefault();
            var dummy = InventorySystem.Instance.LoadCustom(baseItem.item, item.addOptions);
            dummy.upgradeLev = item.upgradeLev;
        }
    }
    private void LoadByEquipItem(List<EquipData> equipItemData)
    {
        GameData.equipItemData = equipItemData;
    }
    private void LoadBySkillData(List<SkillData> sData)
    {
        GameData.sData = sData;
    }
    private void LoadByEquipSkill(List<EquipSkillData> equipSkillData)
    {
        GameData.equipSkillDatas = equipSkillData;
    }
    private void LoadByPetData(List<PetData> petData)
    {
        GameData.pData = petData;
    }
    private void LoadByEquipPet(List<EquipPetData> equipPetData)
    {
        GameData.equipPetData = equipPetData;
    }
    private void LoadByExitTime(string exit)
    {
        GameData.exitTime.Clear();
        GameData.exitTime.Append(exit);
    }
    private void LoadByKeyAccumulateTime(string accumlatesString)
    {
        GameData.keyPrevAccumlateTime.Clear();
        GameData.keyPrevAccumlateTime.Append(accumlatesString);
    }
    private void LoadByFoodUpgradeType(int timerLevel, int selectCount)
    {
        diningRoomsystem.timerUpgradeLevel = timerLevel;
        diningRoomsystem.selectFoodCount = selectCount; ;
    }
    private void LoadByDiningRoomSaveFoodData(SaveFoodData[] saveFoodData)
    {
        for (int i = 0; i < saveFoodData.Length; i++)
        {
            if (saveFoodData[i] != null)
            {
                diningRoomsystem.LoadFoodData(saveFoodData[i], i);
            }
        }
    }
    private void LoadByDiningRoomTimer(float timerData)
    {
        if (timerData <= 0f)
        {
            diningRoomsystem.isLoad = false;
        }
        else
        {
            diningRoomsystem.timer = timerData;
            diningRoomsystem.isLoad = true;
        }
        int count = 0;
        for (int i = 0; i < DiningRoomSystem.Instance.saveFood.Length; ++i)
        {
            if (DiningRoomSystem.Instance.saveFood[i] != null)
            {
                count++;
            }
        }
        if (count >= DiningRoomSystem.Instance.selectFoodCount)
        {
            diningRoomsystem.timer = 0f;
            diningRoomsystem.isFullFood = true;
            diningRoomsystem.isLoad = true;
        }
        diningRoomsystem.LoadMaxTimer();

        diningRoomsystem.CalculateTimer(count);
    }
    private void LoadByItemRankAndRankUP(ShopSystem shop, int itemRank, int itemRankUp)
    {
        shop.currentItemRank = itemRank;
        shop.currentItemRankUp = itemRankUp;
    }
    private void LoadBySkillRankAndSkillRankUp(ShopSystem shop, int skillRank, int skillRankUp)
    {
        shop.currentSkillRank = skillRank;
        shop.currentSkillRankUp = skillRankUp;
    }
    private void LoadByPetRankAndPetRankUp(ShopSystem shop, int petRank, int petRankUp)
    {
        shop.currentPetRank = petRank;
        shop.currentPetRankUp = petRankUp;
    }
    private void LoadByPlayerBuff(PlayerBuffData buffData)
    {
        if (buffData.isEatFood)
            PlayerBuff.Instance.buffData = buffData;
        else
        {
            PlayerBuff.Instance.Reset();
        }
    }
    private void LoadByBuffFoddID(string str)
    {
        if (str != string.Empty)
        {
            PlayerBuff.Instance.SetFoodId(str);
        }
    }
    private void LoadByStageIndex(int stageData)
    {
        GameData.stageData_WaveManager = stageData;
    }
    private void LoadByLanguage(Language languageValue)
    {
        Global.language = languageValue;
    }
    private void LoadByIsRepeatAndIsAuto(bool isRepeatData, bool isAuto)
    {
        GameData.isRepeatData_WaveManager = isRepeatData;
        GameData.isAutoData = isAuto;
    }
    private void LoadByLabSaveData(List<LabSaveData> a, List<LabSaveData> b, List<LabSaveData> c, List<LabSaveData> d, List<LabSaveData> e, List<LabSaveData> f)
    {
        GameData.Re_AtkSaveDataList = a;
        GameData.Re_HPSaveDataList = b;
        GameData.Re_CriSaveDataList = c;
        GameData.Re_SilupSaveDataList = d;
        GameData.Re_MidAtkSaveDataList = e;
        GameData.Re_MidHPSaveDataList = f;
    }
    private void LoadByCurrentLavSaveData(CurrentLavSaveData currentLab)
    {
        if (currentLab.isResearching)
        {
            GameData.currnetLabSaveData = currentLab;
            LabSystem.Instance.maxTimerTic = currentLab.maxTimer;
            LabSystem.Instance.isResearching = currentLab.isResearching;

            var exitTimer = DateTime.ParseExact(GameData.exitTime.ToString(), GameData.datetimeString, null);
            var nowTimeStr = DateTime.Now.ToString(GameData.datetimeString);
            var nowTimeSpan = DateTime.ParseExact(nowTimeStr, GameData.datetimeString, null);

            TimeSpan timeDifference = nowTimeSpan.Subtract(exitTimer);
            var tic = GameData.tic;
            if (timeDifference.TotalSeconds > 0)
            {
                if (timeDifference.TotalSeconds < (currentLab.maxTimer / tic))
                    currentLab.timer -= (int)(timeDifference.TotalSeconds * tic);
            }
            if (currentLab.timer <= 0)
            {
                currentLab.timer = 0;
                LabSystem.Instance.isTimerZero = true;
            }

            LabSystem.Instance.timerTic = currentLab.timer;
            LabSystem.Instance.labType = (LabType)currentLab.LabType;
            LabSystem.Instance.level = currentLab.level;

            LabSystem.Instance.SaveDataSet(currentLab.labTypeNameStringDatas, currentLab.labTypeBuffStringDatas, currentLab.labTableData);
        }
    }
    private void LoadByLabBuffData(LabBuffData datas)
    {
        GameData.labBuffData = datas;
    }

    private void LoadByTutorialIndex(TutorialSystem tutorialUi, int tutorialIndex, int playIndex)
    {
        tutorialUi.tutorialIndex = tutorialIndex;
        tutorialUi.playIndex = playIndex;
    }

    private void LoadByTutorialDone(TutorialSystem tutorialUi, bool a, bool b, bool c, bool d, bool e, bool f)
    {
        tutorialUi.shop = a;
        tutorialUi.lab = b;
        tutorialUi.dining = c;
        tutorialUi.clean = d;
        tutorialUi.failrue = e;
        tutorialUi.loadTutorial = f;
    }
    private void LoadByQuestType(int currentQuestIndex, int currentQuestType, int loppCurrentQuestIndex, QuestSystemData datas)
    {
        MakeTableData.Instance.currentQuestIndex = currentQuestIndex;
        UIManager.Instance.questSystemUi.currentQuestType = (QuestType)currentQuestType;
        MakeTableData.Instance.loppCurrentQuestIndex = loppCurrentQuestIndex;
        UIManager.Instance.questSystemUi.QuestLoad(datas);
    }
    private void LoadBySound(SettingBox UiSetting, bool sound)
    {
        UiSetting.soundValue = sound;
    }

    private void LoadByBosshInfo(CleanWindow cleanWindow,int openStage, int currentCount)
    {
        cleanWindow.openStage = openStage;
        cleanWindow.currentCount = currentCount;
    }
}
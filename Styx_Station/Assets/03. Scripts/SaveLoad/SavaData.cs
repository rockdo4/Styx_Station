using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveData
{
    public int Version { get; set; }

    public abstract SaveData VersionUp();
}

public class SaveDataV1 : SaveData
{
    public SaveDataV1()
    {
        Version = 1;
    }
    public int GetVersion()
    {
        return Version;
    }

    public PlayerData playerdata = new PlayerData();

    public override SaveData VersionUp()
    {
        SaveDataV2 v2 = new SaveDataV2();
        v2.playerdata = playerdata;
        return v2;
    }
}

public class SaveDataV2 : SaveData
{
    public SaveDataV2()
    {
        Version = 2;
    }
    public int GetVersion()
    {
        return Version;
    }

    public PlayerData playerdata = new PlayerData();
    public List<InventoryData> weaponData = new List<InventoryData>();
    public List<InventoryData> armorData = new List<InventoryData>();
    public List<CustomData> customRingData = new List<CustomData>();
    public List<CustomData> customSymbolData = new List<CustomData>();

    public List<EquipData> equipItem = new List<EquipData>();
 
    public override SaveData VersionUp()
    {
        SaveDataV3 v3 = new SaveDataV3();
        v3.playerdata = playerdata;
        v3.weaponData = weaponData;
        v3.armorData = armorData;
        v3.customRingData = customRingData;
        v3.customSymbolData = customSymbolData;
        v3.equipItem = equipItem;

        return v3;
    }
}

public class SaveDataV3 : SaveData
{
    public SaveDataV3()
    {
        Version = 3;
    }
    public int GetVersion()
    {
        return Version;
    }

    public PlayerData playerdata = new PlayerData();
    public List<InventoryData> weaponData = new List<InventoryData>();
    public List<InventoryData> armorData = new List<InventoryData>();
    public List<CustomData> customRingData = new List<CustomData>();
    public List<CustomData> customSymbolData = new List<CustomData>();

    public List<EquipData> equipItem = new List<EquipData>();

    public List<SkillData> skillData = new List<SkillData>();

    public List<EquipSkillData> equipSkill = new List<EquipSkillData>();

    public string exitTime = string.Empty;
    public string keyAccumulateTime = string.Empty;
    //public StageData stageData;
    public int stageData;

    public SaveFoodData[] diningRoomSaveFoodData = new SaveFoodData[6]; // sprite고려 -> 안되어서 이미지 FileName으로 작업해볼예정


    public override SaveData VersionUp()
    {
        SaveDataV4 v4 = new SaveDataV4();
        v4.gameSaveDatas.playerdata = playerdata;
        v4.gameSaveDatas.weaponData = weaponData;
        v4.gameSaveDatas.armorData= armorData;
        v4.gameSaveDatas.customRingData = customRingData;
        v4.gameSaveDatas.customSymbolData= customSymbolData;
        v4.gameSaveDatas.equipItem = equipItem;
        v4.gameSaveDatas.skillData= skillData;
        v4.gameSaveDatas.equipSkill = equipSkill;
        v4.gameSaveDatas.exitTime = exitTime;
        v4.gameSaveDatas.keyAccumulateTime = keyAccumulateTime;
        v4.gameSaveDatas.stageIndex = stageData;
        //v4.gameSaveDatas.stageData= stageData;
        v4.gameSaveDatas.diningRoomSaveFoodData = diningRoomSaveFoodData;


        return v4;
    }
}

public class SaveDataV4 : SaveData
{
    public SaveDataV4()
    {
        Version = 4;
    }
    public int GetVersion()
    {
        return Version;
    }

    public GameSaveData gameSaveDatas = new GameSaveData();

    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}


public class GameSaveData
{
    public PlayerData playerdata = new PlayerData();
    public List<InventoryData> weaponData = new List<InventoryData>();
    public List<InventoryData> armorData = new List<InventoryData>();
    public List<CustomData> customRingData = new List<CustomData>();
    public List<CustomData> customSymbolData = new List<CustomData>();

    public List<EquipData> equipItem = new List<EquipData>();

    public List<SkillData> skillData = new List<SkillData>();

    public List<EquipSkillData> equipSkill = new List<EquipSkillData>();

    public List<PetData> petData = new List<PetData>();

    public List<EquipPetData> equipPet = new List<EquipPetData>();

    public string exitTime = string.Empty;
    public string keyAccumulateTime = string.Empty;
    //public StageData stageData;
    public int stageIndex;
    public bool isRepeat;

    public int foodTimerUpgradeLevelUp;
    public int foodSelectUpgradeLevelUp;
    public SaveFoodData[] diningRoomSaveFoodData = new SaveFoodData[6];
    public float diningRoomTimer;

    public int itemRank;
    public int itemRankUp;
    public int skillRank;
    public int skillRankUp;
    public int petRank;
    public int petRankUp;

    public PlayerBuffData playerBuff;


    public List<LabSaveData> Re001_Lab_SaveDatas = new List<LabSaveData>();
    public List<LabSaveData> Re002_Lab_SaveDatas = new List<LabSaveData>();
    public List<LabSaveData> Re003_Lab_SaveDatas = new List<LabSaveData>();
    public List<LabSaveData> Re004_Lab_SaveDatas = new List<LabSaveData>();
    public List<LabSaveData> Re005_Lab_SaveDatas = new List<LabSaveData>();
    public List<LabSaveData> Re006_Lab_SaveDatas = new List<LabSaveData>();
    public CurrentLavSaveData currentLavSaveData;
    public LabBuffData labBuffData;

}
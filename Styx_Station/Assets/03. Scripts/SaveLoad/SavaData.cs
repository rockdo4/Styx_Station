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

    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}
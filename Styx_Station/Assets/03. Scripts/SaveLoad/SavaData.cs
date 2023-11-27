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
        throw new System.NotImplementedException();
    }
}
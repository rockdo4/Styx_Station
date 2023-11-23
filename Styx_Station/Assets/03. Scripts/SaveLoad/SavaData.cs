using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
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
        throw new System.NotImplementedException();
    }
}
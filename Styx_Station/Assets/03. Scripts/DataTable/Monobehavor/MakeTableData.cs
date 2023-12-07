using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTableData : Singleton<MakeTableData>
{
    private SaveLoad gameSaveLoad;
    [HideInInspector] public StringTable stringTable;
    [HideInInspector] public DiningTable diningRoomTable;
    [HideInInspector] public StageTable stageTable;

    private void Awake()
    {
        gameSaveLoad =GetComponent<SaveLoad>();
        gameSaveLoad.Load();
        UnitConverter.InitUnitConverter();
    }
    private void Start()
    {
        stringTable = new StringTable();
        diningRoomTable = new DiningTable();
        stageTable = new StageTable();
    }
    private void OnApplicationQuit() => gameSaveLoad.Save();
}

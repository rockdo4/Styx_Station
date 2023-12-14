using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTableData : Singleton<MakeTableData>
{
    private SaveLoad gameSaveLoad;
    [HideInInspector] public StringTable stringTable;
    [HideInInspector] public DiningTable diningRoomTable;
    [HideInInspector] public StageTable stageTable;
    [HideInInspector] public LabTable labTable;
    [HideInInspector] public QuestListTable questTable;
    public int currentIndex = 0;

    private void Awake()
    {
        gameSaveLoad = gameObject.AddComponent<SaveLoad>();
        gameSaveLoad.Load();
    }
    private void Start()
    {
        if(stringTable ==null)
            stringTable = new StringTable();
        if(diningRoomTable == null)
            diningRoomTable = new DiningTable();
        if(stageTable == null)
            stageTable = new StageTable();
        if(labTable == null)
            labTable = new LabTable();
        if(questTable == null)
            questTable = new QuestListTable();  

        UnitConverter.InitUnitConverter();
    }
    private void Update()
    {
      
    }
    private void OnApplicationQuit() => gameSaveLoad.Save();
}

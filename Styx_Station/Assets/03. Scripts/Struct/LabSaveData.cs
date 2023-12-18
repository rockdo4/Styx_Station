using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LabSaveData 
{
    public string Id { get; set; }
    public bool isClear { get; set; }

    public LabSaveData(string id, bool isClear)
    {
        Id = id;
        this.isClear = isClear;
    }
}

public struct CurrentLavSaveData
{
    public bool isResearching { get; set; }
    public int timer { get; set; }
    public int maxTimer { get; set; }
    public int LabType { get; set; }
    public int level { get; set; }
    public StringTableData labTypeNameStringDatas { get; set; }
    public StringTableData labTypeBuffStringDatas { get; set; }
    public LabTableDatas labTableData { get; set; }

    public CurrentLavSaveData(bool isResearching,int timer,int maxTimer,LabType labtye,int level,StringTableData NameStringData,StringTableData labTypeBuff,LabTableDatas labTableDatas)
    {
        this.isResearching = isResearching;
        this.timer = timer;
        this.maxTimer = maxTimer;
        LabType= (int)labtye;
        this.level = level;
        labTypeNameStringDatas= NameStringData;
        labTypeBuffStringDatas = labTypeBuff;
       labTableData = labTableDatas;
    }
}
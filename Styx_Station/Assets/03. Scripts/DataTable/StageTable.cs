    using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class StageTable : DataTable<StageTable>
{
    public struct StageTableData
    {
        public int index { get; set; }
        public int difficulty { get; set; }
        public int chapterId { get; set; }  
        public int stageId { get; set; }
        public int waveId { get; set; }
        public int preWaveId { get; set; }
        public float waveTimer { get; set; } 
        public int monsterAttackIncrease { get; set; }
        public int monsterHealthIncrease { get; set; }
        public int monsterAttackSpeedIncrease { get; set; }
        public int monster1Id { get; set; }
        public int monster1Count { get; set; }
        public int monster2Id { get; set; }
        public int monster2Count { get; set; }
        public bool isBossWave { get; set; }
        public int bossMonsterId { get; set; }
        public int rewardExperience { get; set; }
        public int rewardCoins { get; set; }
        public int rewardSpecialCurrency1 { get; set; }
        public int specialCurrency1Amount { get; set; }
        public int rewardSpecialCurrency2 { get; set; }
        public int specialCurrency2Amount { get; set; } 
        public int rewardItem1Id { get; set; }
        public int rewardItem1Amount { get; set; }
        public int rewardItem2Id { get; set; }
        public int rewardItem2Amount { get; set; }
        public int rewardItem3Id { get; set; }
        public int rewardItem3Amount { get; set; }
        public int rewardItem4Id { get; set; }  
        public int rewardItem4Amount { get; set; }
        public int linkedQuestId { get; set; }

    }

    protected Dictionary<int, StageTableData> dic = new Dictionary<int, StageTableData>(); //key: index

    public StageTable()
    {
        path = Resources.Load<TextAsset>(Path.Combine("CSV", "StageTable")).text;
        Load();
    }
    public override void Load()
    {
        TextReader reader = new StringReader(path);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        var records = csv.GetRecords<StageTableData>();

        foreach (var record in records)
        {
            dic.Add(record.index, record);
        }
    }

    public StageTableData GetStageTableData(int index)
    {
        if(!dic.ContainsKey(index))
        {
            return default;
        }
        return dic[index];
    }

    public int GetIndex(int chapterId, int stageId, int waveId)
    {
        return 100000000 + (chapterId * 10000) + ((stageId - 1) * 5) + waveId;
    }
}

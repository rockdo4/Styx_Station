using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class QuestListTable : DataTable<QuestListTable>
{

    public QuestListTable()
    {
        path = "CSV/QuestTable";
        Load();
    }

    public List<QuestTableDatas> questList = new List<QuestTableDatas>();

    public int currentIndex = 1;

    public override void Load()
    {
        var csvFileText = Resources.Load<TextAsset>(path);
        TextReader reader = new StringReader(csvFileText.text);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        var records = csv.GetRecords<QuestTableDatas>();

        foreach (var record in records)
        {
            questList.Add(record);
        }
        questList.Sort((quest1, quest2) => quest1.quest_number.CompareTo(quest2.quest_number));
    }
}

public struct QuestTableDatas
{
    public int Index { get; set; }
    public int quest_type { get; set; } //enum?
    public int quest_number { get; set; }
    public string quest_name { get; set; }
    public int clear_enemy { get; set; }
    public int clear_wave { get; set; }
    public int type_dungeon { get; set; } //enum
    public int clear_dungeon { get; set; }
    public int type_gatcha { get; set; } // enum
    public int clear_gatcha { get; set; }
    public int type_upgrade { get; set; } // enum
    public int clear_upgrade { get; set; }
    public int reward_special01 { get; set; } //enum
    public int currency_special01 { get; set; }
    public int reward_special02 { get; set; }   //enum
    public int currency_speclai2 { get; set; }
}


//type_upgrade,clear_upgrade,reward_special01,currency_ special01,reward_special02,currency_speclai2
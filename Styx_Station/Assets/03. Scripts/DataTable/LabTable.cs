using CsvHelper.Configuration;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class LabTable : DataTable<LabTable>
{


    public LabTable()
    {
        path = "CSV/LabTable";
        Load();
    }
    public Dictionary<string, LabTableDatas> dic = new Dictionary<string, LabTableDatas>();

    public override void Load()
    {
        var csvFileText = Resources.Load<TextAsset>(path);
        TextReader reader = new StringReader(csvFileText.text);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        var records = csv.GetRecords<LabTableDatas>();

        foreach (var record in records)
        {
            dic.Add(record.Re_ID, record);
        }
    }

    public LabTableDatas GetLabTableData(string key)
    {
        return dic[key];
    }
}

[System.Serializable]
public struct LabTableDatas
{
    public string Re_ID { get; set; }
    public string Re_Name_ID { get; set; }
    public int Re_ATK { get; set; }
    public int Re_ATKUP { get; set; }
    public int Re_HP { get; set; }
    public int Re_HPUP { get; set; }
    public int Re_Cri { get; set; }
    public int Re_CriUP { get; set; }
    public int Re_Sil { get; set; }
    public int Re_SilUP { get; set; }
    public int Re_Time { get; set; }
    public int Re_TimeUP { get; set; }
    public int Re_Pom { get; set; }
    public int Re_PomUp { get; set; }
}
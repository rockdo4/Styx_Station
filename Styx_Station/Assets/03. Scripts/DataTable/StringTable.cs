using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System;
using System.Diagnostics;
using Unity.VisualScripting;
using static StageTable;
using UnityEngine;
using CsvHelper.Configuration;

public class StringTable : DataTable<StringTable>
{
    
    public StringTable()
    {
        path = "CSV/StringTable";
        //path = Resources.Load<TextAsset>(Path.Combine("CSV", "StringTable"));
        Load();
    }
    public Dictionary<string, StringTableData> dic = new Dictionary<string, StringTableData>(); 

    public override void Load()
    {
        var csvFileText = Resources.Load<TextAsset>(path);
        TextReader reader = new StringReader(csvFileText.text);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        var records = csv.GetRecords<StringTableData>();

        foreach (var record in records)
        {
            dic.Add(record.ID, record);
        }
   
    }
    public StringTableData GetStringTableData(string id)
    {
        return dic[id];
    }

    public List<StringTableData> GetListStringTableDataContains(string str)
    {
        List<StringTableData> strList = new List<StringTableData>();
        foreach (var data in dic)
        {
            if(data.Key.Contains(str))
            {
                strList.Add(data.Value);
            }
        }
        return strList;
    }
}

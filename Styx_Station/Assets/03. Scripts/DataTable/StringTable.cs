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
            var t = record;
            var korstr = record.KOR;
            string newkorstr = korstr.Replace("\\n", "\n");
            var engstr = record.ENG;
            string newengstr = engstr.Replace("\\n", "\n");
            t.KOR = newkorstr;
            t.ENG= newengstr;
            dic.Add(t.ID, t);
        }
        // 100012_Buff,
        // ���� �� ���ݷ� {0}%\n���� ���ط� {1}%\n��ų ���� {2}%\nġ��Ÿ ���� {3}% ����,
        // Increase Damage {0}%\nBoss damage {1}%\nSkill Damage {2}%\nCritical damage {3}% when ingested

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

using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System;
using System.Diagnostics;
using Unity.VisualScripting;

public class StringTable : DataTable<StringTable>
{
    public struct StringTableData
    {
        public string ID { get; set; }
        public  string KOR { get; set; }
        public string ENG { get; set; }
    }
    public StringTable()
    {
        path = "Assets/07. DataTable/StringTable.csv";
        Load();
    }
    public Dictionary<string, StringTableData> dic = new Dictionary<string, StringTableData>();
    public override void Load()
    {
        using (var streamReader = new StreamReader(path))
        {
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<StringTableData>();
                foreach (var record in records)
                {
                    dic[record.ID] = record; // ID를 key로 사용하여 사전에 추가
                }
            }
        }
    }
}

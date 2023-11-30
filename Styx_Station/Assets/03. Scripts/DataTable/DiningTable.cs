using CsvHelper;
using CsvHelper.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using UnityEngine;
using static StageTable;

public class DiningTable : DataTable<DiningTable>
{

    public DiningTable()
    {
        //Resources.Load<TextAsset>(Path.Combine("DiningRoomTable"));
        //path = Resources.Load<TextAsset>(Path.Combine("CSV", "DiningRoomTable")).text;
        path = "CSV/DiningRoomTable";
        Load();
    }
    public Dictionary<string, FoodTableData> dic = new Dictionary<string, FoodTableData>();
    private List<string> foodID = new List<string>();   
    public override void Load()
    {
        var csvFileText = Resources.Load<TextAsset>(path);
        TextReader reader = new StringReader(csvFileText.text);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        var records = csv.GetRecords<FoodTableData>();

        foreach (var record in records)
        {
            dic.Add(record.Food_ID, record);
            foodID.Add(record.Food_ID);
        }
        //using (var streamReader = new StreamReader(path))
        //{
        //    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        //    {
        //        var records = csvReader.GetRecords<FoodTableData>();
        //        foreach (var record in records)
        //        {
        //            dic.Add(record.Food_ID, record);
        //            foodID.Add(record.Food_ID);
        //        }
        //    }
        //}
    }
    public List<string> GetFoodTableID()
    {
        return foodID; 
    }
    public FoodTableData GetFoodTableData(string str)
    {
        return dic[str];
    }
}

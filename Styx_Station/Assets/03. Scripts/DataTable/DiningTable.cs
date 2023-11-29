using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using static StageTable;

public class DiningTable : DataTable<DiningTable>
{

    public DiningTable()
    {
        path = "Assets/07. DataTable/DiningRoomTable.csv";
        Load();
    }
    public Dictionary<string, FoodTableData> dic = new Dictionary<string, FoodTableData>();
    private List<string> foodID = new List<string>();   
    public override void Load()
    {
        using (var streamReader = new StreamReader(path))
        {
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<FoodTableData>();
                foreach (var record in records)
                {
                    dic.Add(record.Food_ID, record);
                    foodID.Add(record.Food_ID);
                }
            }
        }
    }
    public List<string> GetFoodTableID()
    {
        return foodID; 
    }
}

using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class StageTable : DataTable<StageTable>
{
    public struct StageTableData
    {
        public string ID { get; set; }
        public string KOR { get; set; }
        public string ENG { get; set; }
    }
    public override void Load()
    {
        
    }
}

using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using static SoonsoonData;


public static class SaveLoadSystem
{
    public static int SaveDataVersion { get; private set; } = 2;
    public static string SaveDirectory
    {
        get
        {
            return $"{Application.persistentDataPath}";
        }
    }
    public static void JsonSave(SaveData data, string fileName)
    {
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }

        var path = Path.Combine(SaveDirectory, fileName);
        using (var writer = new JsonTextWriter(new StreamWriter(path)))
        {
            var serialize = new JsonSerializer();

            serialize.Serialize(writer, data);
        }
    }
    public static SaveData JsonLoad(string fileName)
    {
        var path = Path.Combine(SaveDirectory, fileName);
        if (!File.Exists(path))
            return null;

        SaveData data = null;
        int version = 0;

        var json = File.ReadAllText(path);

        using (var reader = new JsonTextReader(new StringReader(json)))
        {
            var jobj = JObject.Load(reader);
            version = jobj["Version"].Value<int>();
        }
        using (var reader = new JsonTextReader(new StringReader(json)))
        {
            var serializer = new JsonSerializer();
            switch (version)
            {
                case 1:
                    data = serializer.Deserialize<SaveDataV1>(reader);
                    break;
                case 2:
                    data = serializer.Deserialize<SaveDataV2>(reader);
                    break;
                    //case 3:
                    //    data = serializer.Deserialize<SaveDataV3>(reader);
                    //    break;
            }

            while (data.Version < SaveDataVersion)
            {
                data = data.VersionUp();
            }
        }
        return data;
    }
}

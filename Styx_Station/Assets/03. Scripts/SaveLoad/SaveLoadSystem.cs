using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using SaveDataVersionCurrent = SaveDataV4;

public static class SaveLoadSystem
{
    public static int SaveDataVersion { get; private set; } = 4;
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
                case 3:
                    data = serializer.Deserialize<SaveDataV3>(reader);
                    break;
                case 4:
                    data = serializer.Deserialize<SaveDataV4>(reader);
                    break;
            }

            while (data.Version < SaveDataVersion)
            {
                data = data.VersionUp();
            }
        }
        return data;
    }


    public static void TxtFileSave(SaveData data)
    {
        bool saveSuccese = false;
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        string textFilePath = Path.Combine(SaveDirectory, "TestText.txt");
        try
        {
            File.WriteAllText(textFilePath, json);
            saveSuccese = true;
        }
        catch (Exception e)
        {
            Debug.LogError("데이터를 저장하는 중 오류 발생: " + e.Message);
        }
        if (saveSuccese)
        {
            ConvertTxtByBinary(textFilePath);
        }
    }

    public static void ConvertTxtByBinary(string str)
    {

        string binaryFilePath = Path.Combine(str);
        if (File.Exists(binaryFilePath))
        {
            string content = File.ReadAllText(binaryFilePath);

            // 문자열이 널인지 확인
            if (!string.IsNullOrEmpty(content))
            {
                // 문자열을 바이너리로 변환
                byte[] binaryData = Encoding.UTF8.GetBytes(content);

                // 이진 데이터를 새로운 파일에 쓰기
                string binaryPath = SaveDirectory+"\\TestBinary.bin";
                File.WriteAllBytes(binaryPath, binaryData);

               Debug.Log("텍스트 파일이 성공적으로 이진 파일로 변환되었습니다.");
            }
            else
            {
                Debug.Log("텍스트 파일의 내용이 비어있습니다.");
            }
        }
        else
        {
            Debug.Log("지정된 텍스트 파일이 존재하지 않습니다.");
        }
    }

    public static SaveDataVersionCurrent BinaryToTxtAndJson(string binaryFileName)
    {
        var binaryPath = Path.Combine(SaveDirectory, binaryFileName);
        if (File.Exists(binaryPath))
        {
            byte[] binaryData = File.ReadAllBytes(binaryPath);
            string content = Encoding.UTF8.GetString(binaryData);

            string txtFilePath = Path.Combine(SaveDirectory, "TestText.txt");
            File.WriteAllText(txtFilePath, content);

            Debug.Log("Binary file successfully converted to text.");

            // Load the converted text file back to SaveData
            SaveDataVersionCurrent loadedData = (SaveDataVersionCurrent)JsonLoad(txtFilePath);
            return loadedData;
            // Do something with the loaded data
        }
        else
        {
            Debug.Log("Specified binary file does not exist.");
        }
        return null;
    }
}

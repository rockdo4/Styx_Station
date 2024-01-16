using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using SaveDataVersionCurrent = SaveDataV4;
using System.Security.Cryptography;
using System.Net.NetworkInformation;

public static class SaveLoadSystem
{
    private static readonly string Key = "YourSecretKey123"; // 16, 24, or 32 bytes
    private static readonly string IV = "YourIV1234567890"; // 16 bytes
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
            if (!string.IsNullOrEmpty(content))
            {
                //byte[] binaryData = Encoding.UTF8.GetBytes(content);
                //string binaryPath = SaveDirectory + "\\TestBinary.bin";
                //File.WriteAllBytes(binaryPath, binaryData);

                byte[] binaryData = EncryptText(content);
                string binaryPath = Path.Combine(SaveDirectory, "TestBinary.bin");
                File.WriteAllBytes(binaryPath, binaryData);

                Debug.Log("텍스트 파일이 성공적으로 암호화되어 이진 파일로 변환되었습니다.");
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

    public static SaveDataVersionCurrent BinaryToTxt(string binaryFileName)
    {
        var binaryPath = Path.Combine(SaveDirectory, binaryFileName);
        if (File.Exists(binaryPath))
        {
            byte[] binaryData = File.ReadAllBytes(binaryPath);
            string content = DecryptText(binaryData);

            string txtFilePath = Path.Combine(SaveDirectory, "TestText.txt");
            File.WriteAllText(txtFilePath, content);

            Debug.Log("암호화된 바이너리 파일이 성공적으로 복호화되어 텍스트 파일로 변환되었습니다.");

            SaveDataVersionCurrent loadedData = (SaveDataVersionCurrent)JsonLoad(txtFilePath);
            return loadedData;
            //byte[] binaryData = File.ReadAllBytes(binaryPath);

            //string content = Encoding.UTF8.GetString(binaryData);

            //string txtFilePath = Path.Combine(SaveDirectory, "TestText.txt");
            //File.WriteAllText(txtFilePath, content);

            //Debug.Log("Binary file successfully converted to text.");

            //SaveDataVersionCurrent loadedData = (SaveDataVersionCurrent)JsonLoad(txtFilePath);
            //return loadedData;
        }
        else
        {
            Debug.Log("Specified binary file does not exist.");
        }
        return null;
    }



    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="textToDecrypt"></param>
    ///// <param name="key"></param>
    ///// <returns></returns>
    //public static string EncryptAes(string textToEncrypt, string key)
    //{
    //    using (Aes aesAlg = Aes.Create())
    //    {
    //        byte[] saltBytes = GenerateRandomBytes(32);
    //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, saltBytes, 100);

    //        // 블록 크기는 AES의 고정된 128 비트로 설정됩니다.
    //        aesAlg.Key = pdb.GetBytes(16);

    //        // IV(초기화 벡터) 생성
    //        aesAlg.GenerateIV();

    //        // 암호화 변환기 객체 생성
    //        ICryptoTransform encryptor = aesAlg.CreateEncryptor();

    //        // 평문을 바이트 배열로 변환
    //        byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);

    //        // 암호화
    //        byte[] cipherBytes = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);

    //        //솔트와 암호화된 데이터와 IV(초기화 벡터)를 함께 저장
    //        byte[] combinedData = new byte[saltBytes.Length + aesAlg.IV.Length + cipherBytes.Length];
    //        Array.Copy(saltBytes, 0, combinedData, 0, saltBytes.Length);
    //        Array.Copy(aesAlg.IV, 0, combinedData, saltBytes.Length, aesAlg.IV.Length);
    //        Array.Copy(cipherBytes, 0, combinedData, saltBytes.Length + aesAlg.IV.Length, cipherBytes.Length);


    //        // Base64 문자열로 변환하여 출력
    //        return Convert.ToBase64String(combinedData);
    //    }

    //}

    //public static string DecryptAes(string textToDecrypt, string key)
    //{
    //    byte[] combinedData = Convert.FromBase64String(textToDecrypt);

    //    // 솔트 추출 (첫 32바이트)
    //    byte[] saltBytes = new byte[32];
    //    Array.Copy(combinedData, 0, saltBytes, 0, saltBytes.Length);

    //    // 솔트 이후의 데이터가 IV와 암호화된 데이터
    //    byte[] ivAndCipherText = new byte[combinedData.Length - saltBytes.Length];
    //    Array.Copy(combinedData, saltBytes.Length, ivAndCipherText, 0, ivAndCipherText.Length);

    //    // IV 추출
    //    byte[] iv = new byte[16];
    //    Array.Copy(ivAndCipherText, 0, iv, 0, iv.Length);

    //    // 키 파생
    //    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, saltBytes, 100);

    //    using (Aes aesAlg = Aes.Create())
    //    {
    //        aesAlg.Key = pdb.GetBytes(16);
    //        aesAlg.IV = iv;

    //        // 복호화 변환기 객체 생성
    //        ICryptoTransform decryptor = aesAlg.CreateDecryptor();

    //        // 암호화된 데이터 추출 (IV를 제외한 나머지 부분)
    //        byte[] cipherText = new byte[ivAndCipherText.Length - iv.Length];
    //        Array.Copy(ivAndCipherText, iv.Length, cipherText, 0, cipherText.Length);

    //        // 복호화
    //        byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

    //        // 복호화된 바이트 배열을 문자열로 변환하여 반환
    //        return Encoding.UTF8.GetString(decryptedBytes);
    //    }
    //}

    ////무작위 바이트 생성
    //private static byte[] GenerateRandomBytes(int length)
    //{
    //    //RNGCryptoServiceProvider(.NET의 보안 강화된 난수 생성기)
    //    using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
    //    {
    //        byte[] randomBytes = new byte[length];
    //        rng.GetBytes(randomBytes);
    //        return randomBytes;
    //    }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="key"></param>
    ///// <param name="plainText"></param>
    ///// <returns></returns>

    //public static string EncryptString(string key, string plainText)
    //{
    //    byte[] iv = new byte[16];
    //    byte[] array;

    //    using (Aes aes = Aes.Create())
    //    {
    //        aes.Key = Encoding.UTF8.GetBytes(key);
    //        aes.IV = iv;

    //        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

    //        using (MemoryStream memoryStream = new MemoryStream())
    //        {
    //            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
    //            {
    //                using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
    //                {
    //                    streamWriter.Write(plainText);
    //                }

    //                array = memoryStream.ToArray();
    //            }
    //        }
    //    }

    //    return Convert.ToBase64String(array);
    //}

    //public static string DecryptString(string key, string cipherText)
    //{
    //    byte[] iv = new byte[16];
    //    byte[] buffer = Convert.FromBase64String(cipherText);

    //    using (Aes aes = Aes.Create())
    //    {
    //        aes.Key = Encoding.UTF8.GetBytes(key);
    //        aes.IV = iv;
    //        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

    //        using (MemoryStream memoryStream = new MemoryStream(buffer))
    //        {
    //            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
    //            {
    //                using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
    //                {
    //                    return streamReader.ReadToEnd();
    //                }
    //            }
    //        }
    //    }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="data"></param>
    ///// <returns></returns>
    //private static byte[] EncryptData(byte[] data)
    //{
    //    using (Aes aesAlg = Aes.Create())
    //    {
    //        aesAlg.Key = Encoding.UTF8.GetBytes(Key);
    //        aesAlg.IV = Encoding.UTF8.GetBytes(IV);

    //        using (MemoryStream msEncrypt = new MemoryStream())
    //        {
    //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
    //            {
    //                csEncrypt.Write(data, 0, data.Length);
    //            }
    //            return msEncrypt.ToArray();
    //        }
    //    }
    //}
    //private static byte[] DecryptData(byte[] encryptedData)
    //{
    //    using (Aes aesAlg = Aes.Create())
    //    {
    //        aesAlg.Key = Encoding.UTF8.GetBytes(Key);
    //        aesAlg.IV = Encoding.UTF8.GetBytes(IV);

    //        using (MemoryStream msDecrypt = new MemoryStream())
    //        {
    //            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Write))
    //            {
    //                csDecrypt.Write(encryptedData, 0, encryptedData.Length);
    //            }

    //            return msDecrypt.ToArray();
    //        }
    //    }
    //}


    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="data"></param>
    ///// <returns></returns>
    ///// 
    private static byte[] EncryptText(string text)
    {
        using (RijndaelManaged rijAlg = new RijndaelManaged())
        {
            rijAlg.Key = Encoding.UTF8.GetBytes(Key);
            rijAlg.IV = Encoding.UTF8.GetBytes(IV);

            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }
                }
                return msEncrypt.ToArray();
            }
        }
    }

    private static string DecryptText(byte[] cipherText)
    {
        using (RijndaelManaged rijAlg = new RijndaelManaged())
        {
            rijAlg.Key = Encoding.UTF8.GetBytes(Key);
            rijAlg.IV = Encoding.UTF8.GetBytes(IV);

            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}


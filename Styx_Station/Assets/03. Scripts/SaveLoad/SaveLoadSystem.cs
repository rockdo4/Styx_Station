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
            Debug.LogError("�����͸� �����ϴ� �� ���� �߻�: " + e.Message);
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

                Debug.Log("�ؽ�Ʈ ������ ���������� ��ȣȭ�Ǿ� ���� ���Ϸ� ��ȯ�Ǿ����ϴ�.");
            }
            else
            {
                Debug.Log("�ؽ�Ʈ ������ ������ ����ֽ��ϴ�.");
            }
        }
        else
        {
            Debug.Log("������ �ؽ�Ʈ ������ �������� �ʽ��ϴ�.");
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

            Debug.Log("��ȣȭ�� ���̳ʸ� ������ ���������� ��ȣȭ�Ǿ� �ؽ�Ʈ ���Ϸ� ��ȯ�Ǿ����ϴ�.");

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

    //        // ��� ũ��� AES�� ������ 128 ��Ʈ�� �����˴ϴ�.
    //        aesAlg.Key = pdb.GetBytes(16);

    //        // IV(�ʱ�ȭ ����) ����
    //        aesAlg.GenerateIV();

    //        // ��ȣȭ ��ȯ�� ��ü ����
    //        ICryptoTransform encryptor = aesAlg.CreateEncryptor();

    //        // ���� ����Ʈ �迭�� ��ȯ
    //        byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);

    //        // ��ȣȭ
    //        byte[] cipherBytes = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);

    //        //��Ʈ�� ��ȣȭ�� �����Ϳ� IV(�ʱ�ȭ ����)�� �Բ� ����
    //        byte[] combinedData = new byte[saltBytes.Length + aesAlg.IV.Length + cipherBytes.Length];
    //        Array.Copy(saltBytes, 0, combinedData, 0, saltBytes.Length);
    //        Array.Copy(aesAlg.IV, 0, combinedData, saltBytes.Length, aesAlg.IV.Length);
    //        Array.Copy(cipherBytes, 0, combinedData, saltBytes.Length + aesAlg.IV.Length, cipherBytes.Length);


    //        // Base64 ���ڿ��� ��ȯ�Ͽ� ���
    //        return Convert.ToBase64String(combinedData);
    //    }

    //}

    //public static string DecryptAes(string textToDecrypt, string key)
    //{
    //    byte[] combinedData = Convert.FromBase64String(textToDecrypt);

    //    // ��Ʈ ���� (ù 32����Ʈ)
    //    byte[] saltBytes = new byte[32];
    //    Array.Copy(combinedData, 0, saltBytes, 0, saltBytes.Length);

    //    // ��Ʈ ������ �����Ͱ� IV�� ��ȣȭ�� ������
    //    byte[] ivAndCipherText = new byte[combinedData.Length - saltBytes.Length];
    //    Array.Copy(combinedData, saltBytes.Length, ivAndCipherText, 0, ivAndCipherText.Length);

    //    // IV ����
    //    byte[] iv = new byte[16];
    //    Array.Copy(ivAndCipherText, 0, iv, 0, iv.Length);

    //    // Ű �Ļ�
    //    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, saltBytes, 100);

    //    using (Aes aesAlg = Aes.Create())
    //    {
    //        aesAlg.Key = pdb.GetBytes(16);
    //        aesAlg.IV = iv;

    //        // ��ȣȭ ��ȯ�� ��ü ����
    //        ICryptoTransform decryptor = aesAlg.CreateDecryptor();

    //        // ��ȣȭ�� ������ ���� (IV�� ������ ������ �κ�)
    //        byte[] cipherText = new byte[ivAndCipherText.Length - iv.Length];
    //        Array.Copy(ivAndCipherText, iv.Length, cipherText, 0, cipherText.Length);

    //        // ��ȣȭ
    //        byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

    //        // ��ȣȭ�� ����Ʈ �迭�� ���ڿ��� ��ȯ�Ͽ� ��ȯ
    //        return Encoding.UTF8.GetString(decryptedBytes);
    //    }
    //}

    ////������ ����Ʈ ����
    //private static byte[] GenerateRandomBytes(int length)
    //{
    //    //RNGCryptoServiceProvider(.NET�� ���� ��ȭ�� ���� ������)
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


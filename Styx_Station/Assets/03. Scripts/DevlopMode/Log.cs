using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Log : Singleton<Log>
{
    public GameObject parent;
    public GameObject child;
    private bool isParentDraw;
    private static int numering;
    public TMP_FontAsset customFont;
    [SerializeField]
    public GameObject textLogPrefab;
    private List<string> logString = new List<string>();
    private void Awake()
    {
        foreach (Transform t in parent.transform)
        {
            if (t.name == "Content")
            {
                child = t.gameObject;
                return;
            }
        }
    }

    public void LogDisplay()
    {
        isParentDraw = !isParentDraw;
        parent.SetActive(isParentDraw);
    }

    public void MakeLogText(string str)
    {
        logString.Add(str);
        GameObject newTextObj = Instantiate(textLogPrefab);
        newTextObj.name = "LogText" + numering++.ToString();
        TextMeshProUGUI textMesh = newTextObj.GetComponent<TextMeshProUGUI>();
        textMesh.text = str;
        newTextObj.transform.SetParent(child.transform);
    }

    private void OnApplicationQuit()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameLog.txt");
        StreamWriter file;
        if (!File.Exists(path))
        {
            file = new StreamWriter(path);
        }
        else
        {
            file = File.AppendText(path);
        }
        foreach (var str in logString)
        {
            file.WriteLine(str); 
        }

        file.Close();
    }
    private List<string> LoadLogData(string path)
    {
        List<string> loadedData = new List<string>();
        if (File.Exists(path))
        {
            using (StreamReader file = new StreamReader(path))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    loadedData.Add(line); // 파일의 각 줄을 리스트에 추가
                }
            }
        }

        return loadedData;
    }
}
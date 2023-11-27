using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class Log : Singleton<Log>
{
    public GameObject parent;
    public GameObject child;
    private bool isParentDraw;
    public GameObject textLogPrefab;
    private LinkedList<string> logString = new LinkedList<string>();
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
        logString.AddLast(str);
        string a = str + "\n";
        var t = textLogPrefab.GetComponentInChildren<TextMeshProUGUI>();
        t.text += a;
        if (logString.Count > 1000 && logString.Count > 0)
        {
            RemoveLog();
        }
    }
    private void RemoveLog()
    {

        for (int i = 0; i < 500; i++)
        {
            logString.RemoveFirst();
        }
        var t = textLogPrefab.GetComponentInChildren<TextMeshProUGUI>();
        t.text ="";
        logString.ToList<string>().ForEach(p =>
        {
            string a = p + "\n";
            t.text += a;
        });
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